using MarcusRunge.CleanArchitectureModule;
using MarcusRunge.CleanArchitectureModule.Contracts;
using MarcusRunge.CleanArchitectureModule.Reactive;

namespace TestCleanArchitectureModule
{
    public class CleanArchitectureModuleFactoryTest
    {
        [Fact]
        public void CreateSuccessfullTest()
        {
            ICleanArchitectureModule module = CleanArchitectureModuleFactory.Instance.Create();
            Assert.NotNull(module);
            Assert.NotNull(module.ServiceA);
            Assert.NotNull(module.ServiceB);
            Assert.True(module.ServiceA is ICreateableAware<IServiceA> createableAwareA && createableAwareA.IsCreated);
            Assert.True(module.ServiceB is ICreateableAware<IServiceB>);
            (module.ServiceB as ICreateableAware<IServiceB>)?.OnCreated?.Subscribe(serviceB =>
            {
                Assert.NotNull(serviceB);
            });
        }
    }
}