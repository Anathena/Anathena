namespace Anathema.MemoryManagement.Helpers
{
    /// <summary>
    /// Static helper used to create or get a singleton from another class.
    /// </summary>
    /// <typeparam name="T">The type to create or get a singleton.</typeparam>
    public static class Singleton<T> where T : new()
    {
        /// <summary>
        /// Gets the singleton of the given type.
        /// </summary>
        public static readonly T Instance = new T();

    } // End class

} // End namespace