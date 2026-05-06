using MarcusRunge.CleanArchitectureModule.Contracts;

namespace MarcusRunge.CleanArchitectureModule.Bases
{
    // Internal base for modules; holds optional service references for derived types.
    internal abstract class CleanArchitectureModuleBase : ICleanArchitectureModuleBase, ICleanArchitectureModule
    {
        // Backing field for IServiceA (assigned by derived modules).
        protected IServiceA? _serviceA;

        // Backing field for IServiceB (assigned by derived modules)
        protected IServiceB? _serviceB;

        // Backing field for IServiceI (assigned by derived modules)
        protected IServiceI? _serviceI;

        /// <inheritdoc/>
        public IServiceA? ServiceA => _serviceA;

        /// <inheritdoc/>
        public IServiceB? ServiceB => _serviceB;

        /// <inheritdoc/>
        public IServiceI? ServiceI => _serviceI;
    }
}