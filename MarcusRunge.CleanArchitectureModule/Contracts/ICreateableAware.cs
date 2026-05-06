namespace MarcusRunge.CleanArchitectureModule.Contracts
{

    /// <summary>
    /// Provides a contract for components that expose their creation state and a creation notification event.
    /// </summary>
    public interface ICreateableAware
    {

        /// <summary>
        /// Gets a value indicating whether the instance has been created.
        /// </summary>        
        public bool IsCreated { get; }

        /// <summary>
        /// Occurs when the instance is created.
        /// </summary>

        public event EventHandler OnCreated;
    }
}