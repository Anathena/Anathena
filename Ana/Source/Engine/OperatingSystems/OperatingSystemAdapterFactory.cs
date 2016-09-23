namespace Ana.Source.Engine.OperatingSystems
{
    using Windows;

    /// <summary>
    /// Factory for obtaining an object that allows access to the underlying operating system
    /// </summary>
    internal class OperatingSystemAdapterFactory
    {
        /// <summary>
        /// Gets an adapter to the operating system
        /// </summary>
        /// <returns>An adapter that provides access to the operating system</returns>
        public static IOperatingSystemAdapter GetOperatingSystemAdapter()
        {
            return new WindowsAdapter();
        }
    }
    //// End class
}
//// End namespace