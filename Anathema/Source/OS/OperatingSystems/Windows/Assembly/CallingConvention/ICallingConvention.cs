using System;

namespace Anathema.MemoryManagement.Assembly.CallingConvention
{
    /// <summary>
    /// Interface defining a calling convention.
    /// </summary>
    public interface ICallingConvention
    {
        /// <summary>
        /// The name of the calling convention.
        /// </summary>
        string Name { get; }
        /// <summary>
        /// Defines which function performs the clean-up task.
        /// </summary>
        CleanupTypes Cleanup { get; }
        /// <summary>
        /// Formats the given parameters to call a function.
        /// </summary>
        /// <param name="Parameters">An array of parameters.</param>
        /// <returns>The mnemonics to pass the parameters.</returns>
        string FormatParameters(IntPtr[] Parameters);
        /// <summary>
        /// Formats the call of a given function.
        /// </summary>
        /// <param name="Function">The function to call.</param>
        /// <returns>The mnemonics to call the function.</returns>
        string FormatCalling(IntPtr Function);
        /// <summary>
        /// Formats the cleaning of a given number of parameters.
        /// </summary>
        /// <param name="ParameterCount">The number of parameters to clean.</param>
        /// <returns>The mnemonics to clean a given number of parameters.</returns>
        string FormatCleaning(int ParameterCount);

    } // End class

} // End namespace