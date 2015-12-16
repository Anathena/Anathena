/*
 * MemorySharp Library
 * http://www.binarysharp.com/
 *
 * Copyright (C) 2012-2014 Jämes Ménétrey (a.k.a. ZenLulz).
 * This library is released under the MIT License.
 * See the file LICENSE for more information.
*/

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Binarysharp.MemoryManagement.Memory;
using Binarysharp.MemoryManagement.Native;

namespace Binarysharp.MemoryManagement.Modules
{
    /// <summary>
    /// Class repesenting a module in the remote process.
    /// </summary>
    public class RemoteModule : RemoteVirtualPage
    {
        #region Fields
        /// <summary>
        /// The dictionary containing all cached functions of the remote module.
        /// </summary>
        internal readonly static IDictionary<Tuple<string, SafeMemoryHandle>, RemoteFunction> CachedFunctions = new Dictionary<Tuple<string, SafeMemoryHandle>, RemoteFunction>();
        #endregion

        #region Properties
        #region IsMainModule
        /// <summary>
        /// State if this is the main module of the remote process.
        /// </summary>
        public bool IsMainModule
        {
            get { return MemorySharp.Native.MainModule.BaseAddress == BaseAddress; }
        }
        #endregion
        #region IsValid
        /// <summary>
        /// Gets if the <see cref="RemoteModule"/> is valid.
        /// </summary>
        public override bool IsValid
        {
            get
            {
                return base.IsValid && MemorySharp.Native.Modules.Cast<ProcessModule>().Any(m => m.BaseAddress == BaseAddress && m.ModuleName == Name);
            }
        }
        #endregion
        #region Name
        /// <summary>
        /// The name of the module.
        /// </summary>
        public string Name
        {
            get { return Native.ModuleName; }
        }
        #endregion
        #region Native
        /// <summary>
        /// The native <see cref="ProcessModule"/> object corresponding to this module.
        /// </summary>
        public ProcessModule Native { get; private set; }
        #endregion
        #region Path
        /// <summary>
        /// The full path of the module.
        /// </summary>
        public string Path
        {
            get { return Native.FileName; }
        }
        #endregion
        #region Size
        /// <summary>
        /// The size of the module in the memory of the remote process.
        /// </summary>
        public int Size
        {
            get { return Native.ModuleMemorySize; }
        }
        #endregion
        #region This
        /// <summary>
        /// Gets the specified function in the remote module.
        /// </summary>
        /// <param name="functionName">The name of the function.</param>
        /// <returns>A new instance of a <see cref="RemoteFunction"/> class.</returns>
        public RemoteFunction this[string functionName]
        {
            get { return FindFunction(functionName); }
        }
        #endregion
        #endregion

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="RemoteModule"/> class.
        /// </summary>
        /// <param name="memorySharp">The reference of the <see cref="MemorySharp"/> object.</param>
        /// <param name="module">The native <see cref="ProcessModule"/> object corresponding to this module.</param>
        internal RemoteModule(MemorySharp memorySharp, ProcessModule module) : base(memorySharp, module.BaseAddress)
        {
            // Save the parameter
            Native = module;
        }
        #endregion

        #region Methods
        #region Eject
        /// <summary>
        /// Ejects the loaded dynamic-link library (DLL) module.
        /// </summary>
        public void Eject()
        {
            // Eject the module
            MemorySharp.Modules.Eject(this);
            // Remove the pointer
            BaseAddress = IntPtr.Zero;
        }
        #endregion
        #region FindFunction
        /// <summary>
        /// Finds the specified function in the remote module.
        /// </summary>
        /// <param name="functionName">The name of the function (case sensitive).</param>
        /// <returns>A new instance of a <see cref="RemoteFunction"/> class.</returns>
        /// <remarks>
        /// Interesting article on how DLL loading works: http://msdn.microsoft.com/en-us/magazine/bb985014.aspx
        /// </remarks>
        public RemoteFunction FindFunction(string functionName)
        {
            // Create the tuple
            var tuple = Tuple.Create(functionName, MemorySharp.Handle);

            // Check if the function is already cached
            if (CachedFunctions.ContainsKey(tuple))
                return CachedFunctions[tuple];

            // If the function is not cached
            // Check if the local process has this module loaded
            var localModule = Process.GetCurrentProcess().Modules.Cast<ProcessModule>().FirstOrDefault(m => m.FileName.ToLower() == Path.ToLower());
            var isManuallyLoaded = false;

            try
            {
                // If this is not the case, load the module inside the local process
                if (localModule == null)
                {
                    isManuallyLoaded = true;
                    localModule = ModuleCore.LoadLibrary(Native.FileName);
                }

                // Get the offset of the function
                var offset = ModuleCore.GetProcAddress(localModule, functionName).ToInt64() - localModule.BaseAddress.ToInt64();

                // Rebase the function with the remote module
                var function = new RemoteFunction(MemorySharp, new IntPtr(Native.BaseAddress.ToInt64() + offset), functionName);

                // Store the function in the cache
                CachedFunctions.Add(tuple, function);

                // Return the function rebased with the remote module
                return function;
            }
            finally
            {
                // Free the module if it was manually loaded
                if(isManuallyLoaded)
                    ModuleCore.FreeLibrary(localModule);
            }
        }
        #endregion
        #region InternalEject (internal)
        /// <summary>
        /// Frees the loaded dynamic-link library (DLL) module and, if necessary, decrements its reference count.
        /// </summary>
        /// <param name="memorySharp">The reference of the <see cref="MemorySharp"/> object.</param>
        /// <param name="module">The module to eject.</param>
        internal static void InternalEject(MemorySharp memorySharp, RemoteModule module)
        {
            // Call FreeLibrary remotely
            memorySharp.Threads.CreateAndJoin(memorySharp["kernel32"]["FreeLibrary"].BaseAddress, module.BaseAddress);
        }
        #endregion
        #region ToString (override)
        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        public override string ToString()
        {
            return string.Format("BaseAddress = 0x{0:X} Name = {1}", BaseAddress.ToInt64(), Name);
        }
        #endregion
        #endregion
    }
}
