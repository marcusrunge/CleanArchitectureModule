using MarcusRunge.CleanArchitectureModule.Contracts;

namespace MarcusRunge.CleanArchitectureModule
{
    public interface ICleanArchitectureModuleFactory
    {
        ICleanArchitectureModule Create();
    }

    public class CleanArchitectureModuleFactory : ICleanArchitectureModuleFactory
    {
        private static ICleanArchitectureModule? _instance;

        public ICleanArchitectureModule Create() => _instance ??= new Implementations.CleanArchitectureModule();
    }
}