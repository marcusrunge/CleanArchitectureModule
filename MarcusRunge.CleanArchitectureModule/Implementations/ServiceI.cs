using MarcusRunge.CleanArchitectureModule.Bases;
using MarcusRunge.CleanArchitectureModule.Contracts;

namespace MarcusRunge.CleanArchitectureModule.Implementations
{
    internal class ServiceI : CreatableBase<IServiceI, ServiceI, ICleanArchitectureModuleBase>, IServiceI
    {
        protected override void OnCreate(ICleanArchitectureModuleBase @base)
        {
            throw new NotImplementedException();
        }

        protected override Task OnCreateAsync(ICleanArchitectureModuleBase @base, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}