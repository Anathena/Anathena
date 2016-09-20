namespace Ana.Source.Engine.Processes
{
    using OperatingSystems.Windows.Native;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Drawing;
    using System.Linq;

    /// <summary>
    /// A class responsible for collecting all running processes on the system
    /// </summary>
    public class ProcessCollector
    {
        /// <summary>
        /// Retrieves all running processes
        /// </summary>
        /// <returns>A collection of normalized processes</returns>
        public IEnumerable<NormalizedProcess> GetProcesses()
        {
            return Process.GetProcesses()
                .Select(externalProcess => new IntermediateProcess(this.IsProcessSystemProcess(externalProcess), externalProcess))
                .Select(intermediateProcess => new NormalizedProcess(
                        intermediateProcess.ExternalProcess.Id,
                        intermediateProcess.ExternalProcess.ProcessName,
                        intermediateProcess.ExternalProcess.StartTime,
                        intermediateProcess.IsSystemProcess,
                        this.GetIcon(intermediateProcess)))
                .OrderByDescending(normalizedProcess => normalizedProcess.processId);
        }

        /// <summary>
        /// Determines if the provided process is a system process
        /// </summary>
        /// <param name="externalProcess">The process to check</param>
        /// <returns>A value indicating whether or not the given process is a system process</returns>
        private Boolean IsProcessSystemProcess(Process externalProcess)
        {
            if (externalProcess.SessionId == 0 || externalProcess.BasePriority == 13)
            {
                return true;
            }

            try
            {
                if (externalProcess.PriorityBoostEnabled)
                {
                    // Accessing this field will cause an access exception for system processes. This saves
                    // time because handling the exception is faster than failing to fetch the icon later
                }

                return false;
            }
            catch
            {
                return true;
            }
        }

        /// <summary>
        /// Fetches the icon associated with the provided process
        /// </summary>
        /// <param name="intermediateProcess">An intermediate process structure</param>
        /// <returns>An Icon associated with the given process. Returns null if there is no icon</returns>
        private Icon GetIcon(IntermediateProcess intermediateProcess)
        {
            const Icon NoIcon = null;

            if (intermediateProcess.IsSystemProcess)
            {
                return NoIcon;
            }

            try
            {
                // TODO: This is a violation of the abstraction of native methods into just the OS adaptor. Either all process functions go into the OS Adapter,
                // or this portion must be moved into the Windows Adapter
                IntPtr iconHandle = NativeMethods.ExtractIcon(intermediateProcess.ExternalProcess.Handle, intermediateProcess.ExternalProcess.MainModule.FileName, 0);

                if (iconHandle == IntPtr.Zero)
                {
                    return NoIcon;
                }

                return Icon.FromHandle(iconHandle);
            }
            catch
            {
                return NoIcon;
            }
        }

        /// <summary>
        /// Temporary structure used in collecting all running processes
        /// </summary>
        private struct IntermediateProcess
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="IntermediateProcess" /> struct
            /// </summary>
            /// <param name="isSystemProcess">Whether or not the process is a system process</param>
            /// <param name="externalProcess">The external process</param>
            public IntermediateProcess(Boolean isSystemProcess, Process externalProcess)
            {
                this.IsSystemProcess = isSystemProcess;
                this.ExternalProcess = externalProcess;
            }

            /// <summary>
            /// Gets a value indicating whether or not the process is a system process
            /// </summary>
            public Boolean IsSystemProcess { get; private set; }

            /// <summary>
            /// Gets the process associated with this intermediate structure
            /// </summary>
            public Process ExternalProcess { get; private set; }
        }
    }
    //// End class
}
//// End namespace