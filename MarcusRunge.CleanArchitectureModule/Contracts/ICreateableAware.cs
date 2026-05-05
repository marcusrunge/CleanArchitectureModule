namespace MarcusRunge.CleanArchitectureModule.Contracts
{
    public interface ICreateableAware<TInterface>
    {
        public bool IsCreated { get; }
        public IObservable<TInterface> OnCreated { get; }
    }
}