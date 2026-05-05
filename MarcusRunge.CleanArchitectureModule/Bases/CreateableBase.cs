using MarcusRunge.CleanArchitectureModule.Contracts;

namespace MarcusRunge.CleanArchitectureModule.Bases
{
    internal abstract class CreatableBase<TInterface, TClass, TBase> : ICreateableAware
        where TClass : CreatableBase<TInterface, TClass, TBase>, TInterface, new()
    {
        private static readonly Lock _sync = new();
        private static Task? _initTask;
        private static TClass? _instance;

        private readonly Lock _createdLock = new();
        private EventHandler? _createdHandlers;

        private int _isCreated;

        public event EventHandler OnCreated
        {
            add
            {
                if (value is null) return;

                if (IsCreated)
                {
                    value(this, EventArgs.Empty);
                    return;
                }

                lock (_createdLock)
                {
                    if (!IsCreated)
                    {
                        _createdHandlers += value;
                        return;
                    }
                }

                value(this, EventArgs.Empty);
            }
            remove
            {
                if (value is null) return;
                lock (_createdLock)
                {
                    _createdHandlers -= value;
                }
            }
        }

        public bool IsCreated => Volatile.Read(ref _isCreated) == 1;
        internal static Task? Initialization => Volatile.Read(ref _initTask);
        internal static Exception? InitializationException { get; private set; }

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

                // exakt einmal "created"-Übergang
                if (Interlocked.Exchange(ref _isCreated, 1) == 0)
                {
                    EventHandler? handlers;

                    // Handler "abholen" und löschen => feuert für Früh-Abonnenten genau einmal
                    lock (_createdLock)
                    {
                        handlers = _createdHandlers;
                        _createdHandlers = null;
                    }

                    handlers?.Invoke(this, EventArgs.Empty);
                }
            }
            catch (Exception ex)
            {
                InitializationException = ex;

                // Wie bisher: bei Fehler kein "created"-Signal (vorher TrySetError im Observable) [2](https://mrsoftwaretechnik-my.sharepoint.com/personal/marcus_runge_mrsoftwaretechnik_onmicrosoft_com/Documents/Microsoft%20Copilot%20Chat-Dateien/CreateableBase.cs)
                throw;
            }
        }
    }
}