namespace MarcusRunge.CleanArchitectureModule.Contracts
{
    public interface ICreateableAware
    {
        public bool IsCreated { get; }
        public event EventHandler OnCreated;
    }
}