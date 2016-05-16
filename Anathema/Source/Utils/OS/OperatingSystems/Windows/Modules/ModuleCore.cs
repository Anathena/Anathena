using Anathema.MemoryManagement.Native;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace Anathema.MemoryManagement.Modules
{
    /// <summary>
    /// Static core class providing tools for manipulating modules and libraries.
    /// </summary>
    public static class ModuleCore
    {
        #region GetProcAddress
        /// <summary>
        /// Retrieves the address of an exported function or variable from the specified dynamic-link library (DLL).
        /// </summary>
        /// <param name="ModuleName">The module name (not case-sensitive).</param>
        /// <param name="FunctionName">The function or variable name, or the function's ordinal value.</param>
        /// <returns>The address of the exported function.</returns>
        public static IntPtr GetProcAddress(String ModuleName, String FunctionName)
        {
            // Get the module
            ProcessModule Module = Process.GetCurrentProcess().Modules.Cast<ProcessModule>().FirstOrDefault(m => m.ModuleName.ToLower() == ModuleName.ToLower());

            // Check whether there is a module loaded with this name
            if (Module == null)
                throw new ArgumentException(String.Format("Couldn't get the module {0} because it doesn't exist in the current process.", ModuleName));

            // Get the function address
            IntPtr ProcessAddress = NativeMethods.GetProcAddress(Module.BaseAddress, FunctionName);

            // Check whether the function was found
            if (ProcessAddress != IntPtr.Zero)
                return ProcessAddress;

            // Else the function was not found, throws an exception
            throw new Win32Exception(string.Format("Couldn't get the function address of {0}.", FunctionName));
        }

        /// <summary>
        /// Retrieves the address of an exported function or variable from the specified dynamic-link library (DLL).
        /// </summary>
        /// <param name="Module">The <see cref="ProcessModule"/> object corresponding to the module.</param>
        /// <param name="FunctionName">The function or variable name, or the function's ordinal value.</param>
        /// <returns>If the function succeeds, the return value is the address of the exported function.</returns>
        public static IntPtr GetProcAddress(ProcessModule Module, String FunctionName)
        {
            return GetProcAddress(Module.ModuleName, FunctionName);
        }

        #endregion

        #region FreeLibrary
        /// <summary>
        /// Frees the loaded dynamic-link library (DLL) module and, if necessary, decrements its reference count.
        /// </summary>
        /// <param name="LibraryName">The name of the library to free (not case-sensitive).</param>
        public static void FreeLibrary(String LibraryName)
        {
            // Get the module
            ProcessModule Module = Process.GetCurrentProcess().Modules.Cast<ProcessModule>().FirstOrDefault(m => m.ModuleName.ToLower() == LibraryName.ToLower());

            // Check whether there is a library loaded with this name
            if(Module == null)
                throw new ArgumentException(String.Format("Couldn't free the library {0} because it doesn't exist in the current process.", LibraryName));

            // Free the library
            if(!NativeMethods.FreeLibrary(Module.BaseAddress))
                throw new Win32Exception(String.Format("Couldn't free the library {0}.", LibraryName));
        }

        /// <summary>
        /// Frees the loaded dynamic-link library (DLL) module and, if necessary, decrements its reference count.
        /// </summary>
        /// <param name="Module">The <see cref="ProcessModule"/> object corresponding to the library to free.</param>
        public static void FreeLibrary(ProcessModule Module)
        {
            FreeLibrary(Module.ModuleName);
        }

        #endregion

        #region LoadLibrary
        /// <summary>
        /// Loads the specified module into the address space of the calling process.
        /// </summary>
        /// <param name="LibraryPath">The name of the module. This can be either a library module (a .dll file) or an executable module (an .exe file).</param>
        /// <returns>A <see cref="ProcessModule"/> corresponding to the loaded library.</returns>
        public static ProcessModule LoadLibrary(String LibraryPath)
        {
            // Check whether the file exists
            if(!File.Exists(LibraryPath))
                throw new FileNotFoundException(String.Format("Couldn't load the library {0} because the file doesn't exist.", LibraryPath));
            
            // Load the library
            if(NativeMethods.LoadLibrary(LibraryPath) == IntPtr.Zero)
                throw new Win32Exception(String.Format("Couldn't load the library {0}.", LibraryPath));

            // Enumerate the loaded modules and return the one newly added
            return Process.GetCurrentProcess().Modules.Cast<ProcessModule>().First(x => x.FileName == LibraryPath);
        }

        #endregion

    } // End class

} // End namespace