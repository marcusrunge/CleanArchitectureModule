namespace MarcusRunge.CleanArchitectureModule.Bases
{
    internal sealed class OneShotObservable<T> : IObservable<T>
    {
        private readonly Lock _sync = new();
        private Exception? _error;
        private List<IObserver<T>> _observers = [];

        private int _state;
        private T? _value;

        public IDisposable Subscribe(IObserver<T> observer)
        {
            ArgumentNullException.ThrowIfNull(observer);

            var state = Volatile.Read(ref _state);

            if (state == 1)
            {
                observer.OnNext(_value!);
                observer.OnCompleted();
                return EmptyDisposable.Instance;
            }
            if (state == 2)
            {
                observer.OnError(_error!);
                return EmptyDisposable.Instance;
            }

            lock (_sync)
            {
                state = _state;
                if (state == 1)
                {
                    observer.OnNext(_value!);
                    observer.OnCompleted();
                    return EmptyDisposable.Instance;
                }
                if (state == 2)
                {
                    observer.OnError(_error!);
                    return EmptyDisposable.Instance;
                }

                _observers.Add(observer);
                return new Subscription(this, observer);
            }
        }

        public void TrySetError(Exception ex)
        {
            ArgumentNullException.ThrowIfNull(ex);
            if (Interlocked.CompareExchange(ref _state, 2, 0) != 0)
                return;

            _error = ex;

            List<IObserver<T>> snapshot;
            lock (_sync)
            {
                snapshot = _observers;
                _observers = [];
            }

            foreach (var o in snapshot)
                o.OnError(ex);
        }

        public void TrySetResult(T value)
        {
            if (Interlocked.CompareExchange(ref _state, 1, 0) != 0)
                return;

            _value = value;

            List<IObserver<T>> snapshot;
            lock (_sync)
            {
                snapshot = _observers;
                _observers = [];
            }

            foreach (var o in snapshot)
            {
                o.OnNext(value);
                o.OnCompleted();
            }
        }

        private void Unsubscribe(IObserver<T> observer)
        {
            lock (_sync)
                _observers.Remove(observer);
        }

        private sealed class EmptyDisposable : IDisposable
        {
            public static readonly EmptyDisposable Instance = new();

            public void Dispose()
            { }
        }

        private sealed class Subscription(OneShotObservable<T> parent, IObserver<T> observer) : IDisposable
        {
            private IObserver<T>? _observer = observer;
            private OneShotObservable<T>? _parent = parent;

            public void Dispose()
            {
                var p = Interlocked.Exchange(ref _parent, null);
                var o = Interlocked.Exchange(ref _observer, null);
                if (p is not null && o is not null) p.Unsubscribe(o);
            }
        }
    }
}