namespace Ana.Source.Engine.Processes
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// An interface for an object that enumerates and selects processes running on the system
    /// </summary>
    internal interface IProcesses
    {
        /// <summary>
        /// Gets all running processes on the system
        /// </summary>
        /// <returns>An enumeration of see <see cref="NormalizedProcess" /></returns>
        IEnumerable<NormalizedProcess> GetProcesses();

        /// <summary>
        /// Opens a process for editing
        /// </summary>
        /// <param name="process">The process to be opened</param>
        void OpenProcess(NormalizedProcess process);

        /// <summary>
        /// Gets the process that has been opened
        /// </summary>
        /// <returns>The opened process</returns>
        NormalizedProcess GetOpenedProcess();

        /// <summary>
        /// Determines if the opened process is 32 bit
        /// </summary>
        /// <returns>Returns true if the opened process is 32 bit, otherwise false</returns>
        Boolean IsOpenedProcess32Bit();

        /// <summary>
        /// Determines if the opened process is 64 bit
        /// </summary>
        /// <returns>Returns true if the opened process is 64 bit, otherwise false</returns>
        Boolean IsOpenedProcess64Bit();

        /// <summary>
        /// Determines if a process is 32 bit
        /// </summary>
        /// <param name="process">The process to check</param>
        /// <returns>Returns true if the process is 32 bit, otherwise false</returns>
        Boolean IsProcess32Bit(NormalizedProcess process);

        /// <summary>
        /// Determines if a process is 64 bit
        /// </summary>
        /// <param name="process">The process to check</param>
        /// <returns>Returns true if the process is 64 bit, otherwise false</returns>
        Boolean IsProcess64Bit(NormalizedProcess process);
    }
    //// End interface
}
//// End namespace