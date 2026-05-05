namespace MarcusRunge.CleanArchitectureModule.Reactive
{
    public static class ObservableExtensions
    {
        public static IDisposable Subscribe<T>(
            this IObservable<T> source,
            Action<T> onNext,
            Action<Exception>? onError = null,
            Action? onCompleted = null)
        {
            return source is null
                ? throw new ArgumentNullException(nameof(source))
                : source.Subscribe(new LambdaObserver<T>(onNext, onError, onCompleted));
        }
    }
}