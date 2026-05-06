using MarcusRunge.CleanArchitectureModule.Contracts;

namespace MarcusRunge.CleanArchitectureModule
{
    /// <summary>
    /// Defines a factory contract for creating a clean architecture module instance.
    /// </summary>
    public interface ICleanArchitectureModuleFactory
    {
        /// <summary>
        /// Creates (or returns) a module instance.
        /// </summary>
        ICleanArchitectureModule Create();
    }

    /// <summary>
    /// Default factory implementation that provides a singleton-like factory and module instance.
    /// </summary>
    public class CleanArchitectureModuleFactory : ICleanArchitectureModuleFactory
    {
        // Stores the singleton-like factory instance (lazy-created).
        private static ICleanArchitectureModuleFactory? _factoryInstance;

        // Stores the singleton-like module instance created by this factory (lazy-created).
        private static ICleanArchitectureModule? _moduleInstance;

        public static ICleanArchitectureModuleFactory Instance =>
            /* What happens here:
               - Lazy initialization of the factory itself using the null-coalescing assignment operator (??=).
               - If _factoryInstance is null, a new CleanArchitectureModuleFactory is created and cached.
               - If it is already set, the existing instance is returned.

               Notes on logic/behavior:
               - This provides a "singleton-like" access pattern without explicit locking.
               - In highly concurrent scenarios, more than one instance could be constructed transiently,
                 but the field will end up holding one of them (depending on timing). */
            _factoryInstance ??= new CleanArchitectureModuleFactory();

        /// <inheritdoc/>
        public ICleanArchitectureModule Create() =>
            /* What happens here:
               - Lazy initialization of the module instance.
               - If _moduleInstance is null, a new Implementations.CleanArchitectureModule is created and cached.
               - If it is already set, the cached instance is returned.

               Purpose/intent:
               - Ensures consumers get a single shared module instance per process/app-domain-like context,
                 created on first demand. */
            _moduleInstance ??= new Implementations.CleanArchitectureModule();
    }
}