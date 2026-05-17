using Microsoft.Extensions.Logging;

namespace MarcusRunge.CleanArchitectureModule.Contracts
{
    /// <summary>
    /// Internal base contract for exposing services to internal consumers.
    /// </summary>
    internal interface ICleanArchitectureModuleBase
    {
        /// <summary>
        /// Gets the ILogger instance used for logging within the module.
        /// </summary>
        internal ILogger? Logger { get; }

        /// <summary>
        /// Gets the IServiceI instance used for internal module operations.
        /// </summary>
        internal IServiceI? ServiceI { get; }
    }
}