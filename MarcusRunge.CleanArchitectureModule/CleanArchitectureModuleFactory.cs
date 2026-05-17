using MarcusRunge.CleanArchitectureModule.Contracts;
using Microsoft.Extensions.Logging;

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

        // Logger reference for potential logging; can be null if not provided.
        private readonly ILogger? _logger;

        public CleanArchitectureModuleFactory()
        {
        }

        public CleanArchitectureModuleFactory(ILogger? logger)
        {
            _logger = logger;
        }

        /// <inheritdoc/>
        public ICleanArchitectureModule Create() =>
            /* What happens here:
               - Lazy initialization of the module instance.
               - If _moduleInstance is null, a new Implementations.CleanArchitectureModule is created and cached.
               - If it is already set, the cached instance is returned.

               Purpose/intent:
               - Ensures consumers get a single shared module instance per process/app-domain-like context,
                 created on first demand. */
            _moduleInstance ??= new Implementations.CleanArchitectureModule(_logger);
    }
}