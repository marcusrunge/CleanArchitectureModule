using MarcusRunge.CleanArchitectureModule.Contracts;
using MarcusRunge.CleanArchitectureModule.Reactive;

namespace MarcusRunge.CleanArchitectureModule.Bases
{
    public abstract class CreatableBase<TInterface, TClass, TBase> : ICreateableAware<TInterface>
        where TClass : CreatableBase<TInterface, TClass, TBase>, TInterface, new()
    {
        private static readonly Lock _sync = new();
        private static Task? _initTask;
        private static TClass? _instance;
        private readonly OneShotObservable<TInterface> _onCreated = new();
        private int _isCreated;
        public static Task? Initialization => Volatile.Read(ref _initTask);
        public static Exception? InitializationException { get; private set; }
        public IObservable<TInterface> OnCreated => _onCreated;
        public bool IsCreated => Volatile.Read(ref _isCreated) == 1;

        public static TInterface Create(TBase @base)
        {
            EnsureCreated(@base);
            EnsureAsyncInitStarted(@base);
            return _instance!;
        }

        protected abstract void OnCreate(TBase @base);

        protected abstract Task OnCreateAsync(TBase @base, CancellationToken cancellationToken);

        private static void EnsureAsyncInitStarted(TBase @base)
        {
            if (Volatile.Read(ref _initTask) is not null) return;

            lock (_sync)
            {
                if (_initTask is not null) return;
                if (_instance is null) throw new InvalidOperationException("Instance not created.");

                _initTask = _instance.InitializeAsync(@base);

                _initTask.ContinueWith(
                    static t => InitializationException = t.Exception?.GetBaseException(),
                    CancellationToken.None,
                    TaskContinuationOptions.OnlyOnFaulted | TaskContinuationOptions.ExecuteSynchronously,
                    TaskScheduler.Default
                );
            }
        }

        private static void EnsureCreated(TBase @base)
        {
            if (_instance is not null) return;

            lock (_sync)
            {
                if (_instance is not null) return;

                var inst = new TClass();

                inst.OnCreate(@base);

                _instance = inst;
            }
        }

        private async Task InitializeAsync(TBase @base)
        {
            try
            {
                await OnCreateAsync(@base, CancellationToken.None).ConfigureAwait(false);

                if (Interlocked.Exchange(ref _isCreated, 1) == 0)
                {
                    _onCreated.TrySetResult((TInterface)(object)this);
                }
            }
            catch (Exception ex)
            {
                InitializationException = ex;
                _onCreated.TrySetError(ex);
                throw;
            }
        }
    }
}