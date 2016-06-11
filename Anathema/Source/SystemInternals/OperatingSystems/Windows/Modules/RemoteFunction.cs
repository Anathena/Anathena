using Anathema.Source.SystemInternals.OperatingSystems.Windows.Memory;
using System;

namespace Anathema.Source.SystemInternals.OperatingSystems.Windows.Modules
{
    /// <summary>
    /// Class representing a function in the remote process.
    /// </summary>
    public class RemoteFunction : RemotePointer
    {
        /// <summary>
        /// The name of the function.
        /// </summary>
        public String Name { get; private set; }

        #region Constructor
        public RemoteFunction(WindowsOperatingSystem WindowsOSInterface, IntPtr Address, String FunctionName) : base(WindowsOSInterface, Address)
        {
            // Save the parameter
            Name = FunctionName;
        }

        #endregion

        #region Methods
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