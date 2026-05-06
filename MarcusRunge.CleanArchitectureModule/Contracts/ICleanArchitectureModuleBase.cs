namespace MarcusRunge.CleanArchitectureModule.Contracts
{
    /// <summary>
    /// Internal base contract for exposing services to internal consumers.
    /// </summary>
    internal interface ICleanArchitectureModuleBase
    {
        /// <summary>
        /// Gets the IServiceI instance used for internal module operations.
        /// </summary>
        IServiceI? ServiceI { get; }
    }
}