using MarcusRunge.CleanArchitectureModule.Contracts;
using Microsoft.Extensions.Logging;

namespace MarcusRunge.CleanArchitectureModule.Bases
{
    // Internal base for modules; holds optional service references for derived types.
    internal abstract class CleanArchitectureModuleBase(ILogger? logger) : ICleanArchitectureModuleBase, ICleanArchitectureModule
    {
        // Backing field for IServiceA (assigned by derived modules).
        protected IServiceA? _serviceA;

        // Backing field for IServiceB (assigned by derived modules)
        protected IServiceB? _serviceB;

        // Backing field for IServiceI (assigned by derived modules)
        protected IServiceI? _serviceI;

        /// <inheritdoc/>
        ILogger? ICleanArchitectureModuleBase.Logger => logger;

        /// <inheritdoc/>
        public IServiceA? ServiceA => _serviceA;

        /// <inheritdoc/>
        public IServiceB? ServiceB => _serviceB;

        /// <inheritdoc/>
        IServiceI? ICleanArchitectureModuleBase.ServiceI => _serviceI;
    }
}