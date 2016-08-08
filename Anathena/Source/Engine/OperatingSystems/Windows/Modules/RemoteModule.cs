using Anathena.Source.Engine.OperatingSystems.Windows.Memory;
using Anathena.Source.Engine.OperatingSystems.Windows.Native;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;

namespace Anathena.Source.Engine.OperatingSystems.Windows.Modules
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
        internal readonly static IDictionary<Tuple<String, SafeMemoryHandle>, RemoteFunction> CachedFunctions = new Dictionary<Tuple<String, SafeMemoryHandle>, RemoteFunction>();
        #endregion

        /// <summary>
        /// State if this is the main module of the remote process.
        /// </summary>
        public Boolean IsMainModule
        {
            get { return WindowsOperatingSystem.Native.MainModule.BaseAddress == BaseAddress; }
        }

        /// <summary>
        /// Gets if the <see cref="RemoteModule"/> is valid.
        /// </summary>
        public override bool IsValid
        {
            get
            {
                return base.IsValid && WindowsOperatingSystem.Native.Modules.Cast<ProcessModule>().Any(m => m.BaseAddress == BaseAddress && m.ModuleName == Name);
            }
        }

        /// <summary>
        /// The name of the module.
        /// </summary>
        public string Name { get { return Native.ModuleName; } }

        /// <summary>
        /// The native <see cref="ProcessModule"/> object corresponding to this module.
        /// </summary>
        public ProcessModule Native { get; private set; }

        /// <summary>
        /// The full path of the module.
        /// </summary>
        public string Path { get { return Native.FileName; } }

        /// <summary>
        /// The size of the module in the memory of the remote process.
        /// </summary>
        public int Size { get { return Native.ModuleMemorySize; } }

        /// <summary>
        /// Gets the specified function in the remote module.
        /// </summary>
        /// <param name="functionName">The name of the function.</param>
        /// <returns>A new instance of a <see cref="RemoteFunction"/> class.</returns>
        public RemoteFunction this[string functionName]
        {
            get { return FindFunction(functionName); }
        }

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="RemoteModule"/> class.
        /// </summary>
        /// <param name="WindowsOperatingSystem">The reference of the <see cref="WindowsOperatingSystem"/> object.</param>
        /// <param name="Module">The native <see cref="ProcessModule"/> object corresponding to this module.</param>
        internal RemoteModule(WindowsOperatingSystem WindowsOperatingSystem, ProcessModule Module) : base(WindowsOperatingSystem, Module.BaseAddress)
        {
            // Save the parameter
            Native = Module;
        }

        #endregion

        #region Methods
        #region FindFunction
        /// <summary>
        /// Finds the specified function in the remote module.
        /// </summary>
        /// <param name="FunctionName">The name of the function (case sensitive).</param>
        /// <returns>A new instance of a <see cref="RemoteFunction"/> class.</returns>
        /// <remarks>
        /// Interesting article on how DLL loading works: http://msdn.microsoft.com/en-us/magazine/bb985014.aspx
        /// </remarks>
        public RemoteFunction FindFunction(String FunctionName)
        {
            // Create the tuple
            Tuple<String, SafeMemoryHandle> Tuple = System.Tuple.Create(FunctionName, WindowsOperatingSystem.Handle);

            // Check if the function is already cached
            if (CachedFunctions.ContainsKey(Tuple))
                return CachedFunctions[Tuple];

            // If the function is not cached
            // Check if the local process has this module loaded
            ProcessModule LocalModule = Process.GetCurrentProcess().Modules.Cast<ProcessModule>().FirstOrDefault(m => m.FileName.ToLower() == Path.ToLower());
            Boolean IsManuallyLoaded = false;

            try
            {
                // If this is not the case, load the module inside the local process
                if (LocalModule == null)
                {
                    IsManuallyLoaded = true;
                    LocalModule = ModuleCore.LoadLibrary(Native.FileName);
                }

                // Get the offset of the function
                Int64 Offset = ModuleCore.GetProcAddress(LocalModule, FunctionName).ToInt64() - LocalModule.BaseAddress.ToInt64();

                // Rebase the function with the remote module
                RemoteFunction Function = new RemoteFunction(WindowsOperatingSystem, new IntPtr(Native.BaseAddress.ToInt64() + Offset), FunctionName);

                // Store the function in the cache
                CachedFunctions.Add(Tuple, Function);

                // Return the function rebased with the remote module
                return Function;
            }
            finally
            {
                // Free the module if it was manually loaded
                if (IsManuallyLoaded)
                    ModuleCore.FreeLibrary(LocalModule);
            }
        }

        #endregion
        #region ToString (override)
        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        public override String ToString()
        {
            return String.Format("BaseAddress = 0x{0:X} Name = {1}", BaseAddress.ToInt64(), Name);
        }

        #endregion
        #endregion

    } // End class

} // End namespace