using MarcusRunge.CleanArchitectureModule;
using MarcusRunge.CleanArchitectureModule.Contracts;

namespace TestCleanArchitectureModule
{
    public class CleanArchitectureModuleFactoryTest
    {
        [Fact]
        public async Task CreateSuccessfullTestAsync()
        {
            ICleanArchitectureModule module = CleanArchitectureModuleFactory.Instance.Create();
            Assert.NotNull(module);
            Assert.NotNull(module.ServiceA);
            Assert.NotNull(module.ServiceB);
            Assert.True(module.ServiceA is ICreateableAware createableAwareA && createableAwareA.IsCreated);
            var awareB = module.ServiceB as ICreateableAware;
            Assert.NotNull(awareB);
            Assert.True(module.ServiceB is ICreateableAware);
            
            var tcs = new TaskCompletionSource(TaskCreationOptions.RunContinuationsAsynchronously);
            int callCount = 0;

            void handler(object? _, EventArgs __)
            {
                Interlocked.Increment(ref callCount);
                tcs.TrySetResult();
            }

            try
            {
                awareB!.OnCreated += handler;

                var completed = await Task.WhenAny(tcs.Task, Task.Delay(TimeSpan.FromSeconds(2), TestContext.Current.CancellationToken));
                Assert.Same(tcs.Task, completed);
                Assert.Equal(1, callCount);
                Assert.True(awareB.IsCreated);
            }
            finally
            {
                awareB!.OnCreated -= handler;
            }
        }
    }
}