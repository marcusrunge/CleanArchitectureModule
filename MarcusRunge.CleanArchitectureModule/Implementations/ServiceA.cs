using MarcusRunge.CleanArchitectureModule.Bases;
using MarcusRunge.CleanArchitectureModule.Contracts;

namespace MarcusRunge.CleanArchitectureModule.Implementations
{
    internal class ServiceA : CreatableBase<IServiceA, ServiceA, ICleanArchitectureModuleBase>, IServiceA
    {
        protected override void OnCreate(ICleanArchitectureModuleBase @base)
        {            
        }

        protected override Task OnCreateAsync(ICleanArchitectureModuleBase @base, CancellationToken cancellationToken) => Task.CompletedTask;
    }
}