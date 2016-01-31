/*
 * MemorySharp Library
 * http://www.binarysharp.com/
 *
 * Copyright (C) 2012-2014 Jämes Ménétrey (a.k.a. ZenLulz).
 * This library is released under the MIT License.
 * See the file LICENSE for more information.
*/

using System;
using System.Linq;
using System.Text;

namespace Binarysharp.MemoryManagement.Assembly.CallingConvention
{
    /// <summary>
    /// Define the C Declaration Calling Convention.
    /// </summary>
    public class CdeclCallingConvention : ICallingConvention
    {
        /// <summary>
        /// The name of the calling convention.
        /// </summary>
        public string Name
        {
            get { return "Cdecl"; }
        }

        /// <summary>
        /// Defines which function performs the clean-up task.
        /// </summary>
        public CleanupTypes Cleanup
        {
            get { return CleanupTypes.Caller; }
        }

        /// <summary>
        /// Formats the given parameters to call a function.
        /// </summary>
        /// <param name="Parameters">An array of parameters.</param>
        /// <returns>The mnemonics to pass the parameters.</returns>
        public string FormatParameters(IntPtr[] Parameters)
        {
            // Declare a var to store the mnemonics
            StringBuilder FormattedParameters = new StringBuilder();

            // For each parameters (in reverse order)
            foreach (IntPtr Parameter in Parameters.Reverse())
            {
                FormattedParameters.AppendLine("push " + Parameter);
            }

            // Return the mnemonics
            return FormattedParameters.ToString();
        }

        /// <summary>
        /// Formats the call of a given function.
        /// </summary>
        /// <param name="function">The function to call.</param>
        /// <returns>The mnemonics to call the function.</returns>
        public string FormatCalling(IntPtr function)
        {
            return "call " + function;
        }

        /// <summary>
        /// Formats the cleaning of a given number of parameters.
        /// </summary>
        /// <param name="ParameterCount">The number of parameters to clean.</param>
        /// <returns>The mnemonics to clean a given number of parameters.</returns>
        public String FormatCleaning(Int32 ParameterCount)
        {
            return "add esp, " + ParameterCount * 4;
        }

    } // End class

} // End namespace