using System;
using System.Collections.Generic;
using System.Text;

namespace Anathema.MemoryManagement.Assembly.CallingConvention
{
    /// <summary>
    /// Define the Fast Calling Convention (aka __msfastcall).
    /// </summary>
    public class FastcallCallingConvention : ICallingConvention
    {
        /// <summary>
        /// The name of the calling convention.
        /// </summary>
        public string Name { get { return "Fastcall"; } }

        /// <summary>
        /// Defines which function performs the clean-up task.
        /// </summary>
        public CleanupTypes Cleanup { get { return CleanupTypes.Callee; } }

        /// <summary>
        /// Formats the given parameters to call a function.
        /// </summary>
        /// <param name="Parameters">An array of parameters.</param>
        /// <returns>The mnemonics to pass the parameters.</returns>
        public string FormatParameters(IntPtr[] Parameters)
        {
            // Declare a var to store the mnemonics
            StringBuilder FormattedParameters = new StringBuilder();
            List<IntPtr> ParameterList = new List<IntPtr>(Parameters);

            // Store the first parameter in the ECX register
            if (ParameterList.Count > 0)
            {
                FormattedParameters.AppendLine("mov ecx, " + ParameterList[0]);
                ParameterList.RemoveAt(0);
            }

            // Store the second parameter in the EDX register
            if (ParameterList.Count > 0)
            {
                FormattedParameters.AppendLine("mov edx, " + ParameterList[0]);
                ParameterList.RemoveAt(0);
            }

            // For each parameters (in reverse order)
            ParameterList.Reverse();
            foreach (var parameter in ParameterList)
            {
                FormattedParameters.AppendLine("push " + parameter);
            }

            // Return the mnemonics
            return FormattedParameters.ToString();
        }
        /// <summary>
        /// Formats the call of a given function.
        /// </summary>
        /// <param name="Function">The function to call.</param>
        /// <returns>The mnemonics to call the function.</returns>
        public string FormatCalling(IntPtr Function)
        {
            return "call " + Function;
        }

        /// <summary>
        /// Formats the cleaning of a given number of parameters.
        /// </summary>
        /// <param name="ParameterCount">The number of parameters to clean.</param>
        /// <returns>The mnemonics to clean a given number of parameters.</returns>
        public string FormatCleaning(int ParameterCount)
        {
            return String.Empty;
        }

    } // End class

} // End namespace