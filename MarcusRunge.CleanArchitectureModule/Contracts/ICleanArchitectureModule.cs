namespace MarcusRunge.CleanArchitectureModule.Contracts
{
    /// <summary>
    /// Defines the public contract of a module that exposes services.
    /// </summary>
    public interface ICleanArchitectureModule
    {
        /// <summary>
        /// Gets the IServiceA instance exposed by the module, if available.
        /// </summary>
        IServiceA? ServiceA { get; }

        /// <summary>
        /// Gets the IServiceB instance exposed by the module, if available.
        /// </summary>
        IServiceB? ServiceB { get; }
    }
}