/*
 * MemorySharp Library
 * http://www.binarysharp.com/
 *
 * Copyright (C) 2012-2014 Jämes Ménétrey (a.k.a. ZenLulz).
 * This library is released under the MIT License.
 * See the file LICENSE for more information.
*/

using System;
using Binarysharp.MemoryManagement.Memory;

namespace Binarysharp.MemoryManagement.Modules
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
        public RemoteFunction(MemoryEditor MemorySharp, IntPtr Address, String FunctionName) : base(MemorySharp, Address)
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