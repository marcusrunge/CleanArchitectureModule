using MarcusRunge.CleanArchitectureModule.Contracts;

namespace MarcusRunge.CleanArchitectureModule.Bases
{
    internal abstract class CleanArchitectureModuleBase : ICleanArchitectureModuleBase, ICleanArchitectureModule
    {
        protected IServiceA? _serviceA;
        protected IServiceB? _serviceB;
        protected IServiceI? _serviceI;

        public IServiceA? ServiceA => _serviceA;

        public IServiceB? ServiceB => _serviceB;

        IServiceI? ICleanArchitectureModuleBase.ServiceI => _serviceI;
    }
}