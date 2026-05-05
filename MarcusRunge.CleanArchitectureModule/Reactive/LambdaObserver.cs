namespace MarcusRunge.CleanArchitectureModule.Reactive
{
    public sealed class LambdaObserver<T>(Action<T> onNext, Action<Exception>? onError = null, Action? onCompleted = null) : IObserver<T>
    {
        private readonly Action? _onCompleted = onCompleted;
        private readonly Action<Exception>? _onError = onError;
        private readonly Action<T> _onNext = onNext ?? throw new ArgumentNullException(nameof(onNext));

        public void OnCompleted() => _onCompleted?.Invoke();

        public void OnError(Exception error) => _onError?.Invoke(error);

        public void OnNext(T value) => _onNext(value);
    }
}