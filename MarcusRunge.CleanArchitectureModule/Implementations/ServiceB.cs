using MarcusRunge.CleanArchitectureModule.Bases;
using MarcusRunge.CleanArchitectureModule.Contracts;

namespace MarcusRunge.CleanArchitectureModule.Implementations
{
    // Concrete IServiceB implementation using the CreatableBase lifecycle (sync create + optional async init).
    internal class ServiceB : CreatableBase<IServiceB, ServiceB, ICleanArchitectureModuleBase>, IServiceB
    {
        protected override void OnCreate(ICleanArchitectureModuleBase @base)
        {
            // What happens here:
            // - This is the synchronous creation hook executed exactly once for the singleton-like instance.
            // - Use this method to perform quick, non-async setup that must happen before the instance is published
            //   to other callers (e.g., assigning references, initializing cheap state, wiring non-async dependencies).
            //
            // Current behavior:
            // - Intentionally empty: ServiceB requires no synchronous initialization at creation time.
            //
            // Notes:
            // - Avoid long-running or blocking work here; that belongs into OnCreateAsync to keep creation fast
            //   and reduce lock hold time during instance publication.
        }

        protected override Task OnCreateAsync(ICleanArchitectureModuleBase @base, CancellationToken cancellationToken) =>
            /*What happens here:
              - This is the asynchronous initialization hook that runs after the instance exists.
              - It is invoked by the base lifecycle to perform potentially expensive/IO work without blocking creation.
              - Returning Task.CompletedTask signals: "no async initialization required" for ServiceB.
              - The provided cancellationToken is not used here because there is nothing to cancel. */
            Task.CompletedTask;
    }
}