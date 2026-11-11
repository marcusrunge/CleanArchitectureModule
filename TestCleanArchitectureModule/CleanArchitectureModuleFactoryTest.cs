using MarcusRunge.Base;
using MarcusRunge.CleanArchitectureModule;
using MarcusRunge.CleanArchitectureModule.Contracts;

namespace TestCleanArchitectureModule
{
    /// <summary>
    /// Contains tests for the CleanArchitectureModuleFactory class, ensuring that it creates instances of ICleanArchitectureModule correctly and that the created services are properly initialized and functional.
    /// </summary>
    public class CleanArchitectureModuleFactoryTest
    {
        /// <summary>
        /// Verifies that the factory creates a module instance and that the exposed services complete their lifecycle correctly.
        /// </summary>
        [Fact]
        public async Task CreateSuccessfullTestAsync()
        {
            ICleanArchitectureModule module = new CleanArchitectureModuleFactory().Create();

            Assert.NotNull(module);
            Assert.NotNull(module.ServiceA);
            Assert.NotNull(module.ServiceB);

            var awareA = Assert.IsType<ICreateableAware>(module.ServiceA, exactMatch: false);
            var awareB = Assert.IsType<ICreateableAware>(module.ServiceB, exactMatch: false);

            if (awareA.Initialization is not null)
                await awareA.Initialization;

            if (awareB.Initialization is not null)
                await awareB.Initialization;

            Assert.True(awareA.IsCreated);
            Assert.True(awareB.IsCreated);

            var tcs = new TaskCompletionSource(TaskCreationOptions.RunContinuationsAsynchronously);
            int callCount = 0;

            void handler(object? _, EventArgs __)
            {
                Interlocked.Increment(ref callCount);
                tcs.TrySetResult();
            }

            try
            {
                awareB.OnCreated += handler;

                var completed = await Task.WhenAny(
                    tcs.Task,
                    Task.Delay(TimeSpan.FromSeconds(2), TestContext.Current.CancellationToken));

                Assert.Same(tcs.Task, completed);
                Assert.Equal(1, callCount);
                Assert.True(awareB.IsCreated);
            }
            finally
            {
                awareB.OnCreated -= handler;
            }
        }
    }
}