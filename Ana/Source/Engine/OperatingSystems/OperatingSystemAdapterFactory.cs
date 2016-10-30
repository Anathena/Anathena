namespace Ana.Source.Engine.OperatingSystems
{
    using System;
    using System.Threading;
    using Windows;

    /// <summary>
    /// Factory for obtaining an object that allows access to the underlying operating system
    /// </summary>
    internal class OperatingSystemAdapterFactory
    {
        /// <summary>
        /// Singleton instance of the <see cref="WindowsAdapter"/> class
        /// </summary>
        private static Lazy<WindowsAdapter> windowsAdapterInstance = new Lazy<WindowsAdapter>(
            () => { return new WindowsAdapter(); },
            LazyThreadSafetyMode.ExecutionAndPublication);

        /// <summary>
        /// Gets an adapter to the operating system
        /// </summary>
        /// <returns>An adapter that provides access to the operating system</returns>
        public static IOperatingSystemAdapter GetOperatingSystemAdapter()
        {
            return windowsAdapterInstance.Value;
        }
    }
    //// End class
}
//// End namespace