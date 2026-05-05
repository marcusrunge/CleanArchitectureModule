namespace MarcusRunge.CleanArchitectureModule.Contracts
{
    public interface ICleanArchitectureModule
    {
        IServiceA? ServiceA { get; }
        IServiceB? ServiceB { get; }
    }
}