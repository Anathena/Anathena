using Anathema.MemoryManagement.Internals;
using Anathema.MemoryManagement.Memory;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Anathema.MemoryManagement.Modules
{
    /// <summary>
    /// Class providing tools for manipulating modules and libraries.
    /// </summary>
    public class ModuleFactory : IFactory
    {
        /// <summary>
        /// The reference of the <see cref="MemoryManagement.WindowsOSInterface"/> object.
        /// </summary>
        protected readonly WindowsOSInterface MemorySharp;

        /// <summary>
        /// Gets the main module for the remote process.
        /// </summary>
        public RemoteModule MainModule { get { return FetchModule(MemorySharp.Native.MainModule); } }

        /// <summary>
        /// Gets the modules that have been loaded in the remote process.
        /// </summary>
        public IEnumerable<RemoteModule> RemoteModules { get { return NativeModules.Select(FetchModule); } }

        /// <summary>
        /// Gets the native modules that have been loaded in the remote process.
        /// </summary>
        internal IEnumerable<ProcessModule> NativeModules { get { return MemorySharp.Native.Modules.Cast<ProcessModule>(); } }

        #region This
        /// <summary>
        /// Gets a pointer from the remote process.
        /// </summary>
        /// <param name="address">The address of the pointer.</param>
        /// <returns>A new instance of a <see cref="RemotePointer"/> class.</returns>
        [Obfuscation(Exclude = true)]
        public RemotePointer this[IntPtr address]
        {
            get { return new RemotePointer(MemorySharp, address); }
        }

        /// <summary>
        /// Gets the specified module in the remote process.
        /// </summary>
        /// <param name="moduleName">The name of module (not case sensitive).</param>
        /// <returns>A new instance of a <see cref="RemoteModule"/> class.</returns>
        [Obfuscation(Exclude = true)]
        public RemoteModule this[string moduleName]
        {
            get { return FetchModule(moduleName); }
        }

        #endregion

        #region Constructor/Destructor
        /// <summary>
        /// Initializes a new instance of the <see cref="ModuleFactory"/> class.
        /// </summary>
        /// <param name="MemorySharp">The reference of the <see cref="MemoryManagement.WindowsOSInterface"/> object.</param>
        internal ModuleFactory(WindowsOSInterface MemorySharp)
        {
            // Save the parameter
            this.MemorySharp = MemorySharp;
        }

        /// <summary>
        /// Frees resources and perform other cleanup operations before it is reclaimed by garbage collection.
        /// </summary>
        ~ModuleFactory()
        {
            Dispose();
        }

        #endregion

        #region Methods
        #region Dispose (implementation of IFactory)
        /// <summary>
        /// Releases all resources used by the <see cref="ModuleFactory"/> object.
        /// </summary>
        public virtual void Dispose()
        {
            // Clean the cached functions related to this process
            foreach (var CachedFunction in RemoteModule.CachedFunctions.ToArray())
            {
                if (CachedFunction.Key.Item2 == MemorySharp.Handle)
                    RemoteModule.CachedFunctions.Remove(CachedFunction);
            }

            // Avoid the finalizer
            GC.SuppressFinalize(this);
        }

        #endregion
        
        #region FetchModule (protected)
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
            return new RemoteModule(MemorySharp, NativeModules.First(m => m.ModuleName.ToLower() == ModuleName));
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

        #endregion
        #endregion

    } // End class

} // End namespace