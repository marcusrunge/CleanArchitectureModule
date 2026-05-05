using MarcusRunge.CleanArchitectureModule.Contracts;

namespace MarcusRunge.CleanArchitectureModule
{
    public interface ICleanArchitectureModuleFactory
    {
        ICleanArchitectureModule Create();
    }

    public class CleanArchitectureModuleFactory : ICleanArchitectureModuleFactory
    {
        private static ICleanArchitectureModuleFactory? _factoryInstance;
        private static ICleanArchitectureModule? _moduleInstance;
        public static ICleanArchitectureModuleFactory Instance => _factoryInstance ??= new CleanArchitectureModuleFactory();

        public ICleanArchitectureModule Create() => _moduleInstance ??= new Implementations.CleanArchitectureModule();
    }
}