using MarcusRunge.CleanArchitectureModule.Bases;

namespace MarcusRunge.CleanArchitectureModule.Implementations
{
    internal class CleanArchitectureModule : CleanArchitectureModuleBase
    {
        internal CleanArchitectureModule()
        {
            _serviceA = Implementations.ServiceA.Create(this);
            _serviceB = Implementations.ServiceB.Create(this);
            _serviceI = ServiceI.Create(this);
        }
    }
}