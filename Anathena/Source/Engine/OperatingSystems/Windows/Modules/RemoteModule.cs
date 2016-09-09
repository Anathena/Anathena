using Anathena.Source.Engine.OperatingSystems.Windows.Memory;
using System;
using System.Diagnostics;

namespace Anathena.Source.Engine.OperatingSystems.Windows.Modules
{
    /// <summary>
    /// Class repesenting a module in the remote process.
    /// </summary>
    public class RemoteModule : RemoteVirtualPage
    {
        /// <summary>
        /// The name of the module.
        /// </summary>
        public String Name { get { return Native.ModuleName; } }

        /// <summary>
        /// The native <see cref="ProcessModule"/> object corresponding to this module.
        /// </summary>
        public ProcessModule Native { get; private set; }

        /// <summary>
        /// The size of the module in the memory of the remote process.
        /// </summary>
        public Int32 Size
        {
            get { return Native.ModuleMemorySize; }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RemoteModule"/> class.
        /// </summary>
        /// <param name="WindowsOperatingSystem">The reference of the <see cref="WindowsOperatingSystem"/> object.</param>
        /// <param name="Module">The native <see cref="ProcessModule"/> object corresponding to this module.</param>
        internal RemoteModule(ProcessModule Module) : base(Module.BaseAddress)
        {
            // Save the parameter
            Native = Module;
        }

    } // End class

} // End namespace