using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace Anathena.Source.Engine.OperatingSystems.Windows.Modules
{
    /// <summary>
    /// Class providing tools for manipulating modules and libraries.
    /// </summary>
    public class ModuleFactory
    {
        /// <summary>
        /// The reference of the <see cref="MemoryManagement.WindowsOSInterface"/> object.
        /// </summary>
        protected readonly WindowsOperatingSystem WindowsOperatingSystem;

        /// <summary>
        /// Gets the main module for the remote process.
        /// </summary>
        public RemoteModule MainModule { get { return FetchModule(WindowsOperatingSystem?.Native?.MainModule); } }

        /// <summary>
        /// Gets the modules that have been loaded in the remote process.
        /// </summary>
        public IEnumerable<RemoteModule> RemoteModules { get { return NativeModules?.Select(FetchModule); } }

        /// <summary>
        /// Gets the native modules that have been loaded in the remote process.
        /// </summary>
        internal IEnumerable<ProcessModule> NativeModules { get { return WindowsOperatingSystem?.Native?.Modules?.Cast<ProcessModule>(); } }

        /// <summary>
        /// Gets the specified module in the remote process.
        /// </summary>
        /// <param name="ModuleName">The name of module (not case sensitive).</param>
        /// <returns>A new instance of a <see cref="RemoteModule"/> class.</returns>
        public RemoteModule this[String ModuleName]
        {
            get { return FetchModule(ModuleName); }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ModuleFactory"/> class.
        /// </summary>
        /// <param name="WindowsOperatingSystem">The reference of the <see cref="MemoryManagement.WindowsOSInterface"/> object.</param>
        internal ModuleFactory(WindowsOperatingSystem WindowsOperatingSystem)
        {
            // Save the parameter
            this.WindowsOperatingSystem = WindowsOperatingSystem;
        }

        /// <summary>
        /// Fetches a module from the remote process.
        /// </summary>
        /// <param name="ModuleName">A module name (not case sensitive). If the file name extension is omitted, the default library extension .dll is appended.</param>
        /// <returns>A new instance of a <see cref="RemoteModule"/> class.</returns>
        protected RemoteModule FetchModule(String ModuleName)
        {
            // Convert module name with lower chars
            ModuleName = ModuleName.ToLower();

            // Check if the module name has an extension
            if (!Path.HasExtension(ModuleName))
                ModuleName += ".dll";

            // Fetch and return the module
            return new RemoteModule(NativeModules.First(X => X.ModuleName.ToLower() == ModuleName));
        }

        /// <summary>
        /// Fetches a module from the remote process.
        /// </summary>
        /// <param name="Module">A module in the remote process.</param>
        /// <returns>A new instance of a <see cref="RemoteModule"/> class.</returns>
        private RemoteModule FetchModule(ProcessModule Module)
        {
            return FetchModule(Module.ModuleName);
        }

    } // End class

} // End namespace