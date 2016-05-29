using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Anathema.Source.OS.OperatingSystems.Windows.Helpers
{
    /// <summary>
    /// Static helper class providing tools for finding applications.
    /// </summary>
    public static class ApplicationFinder
    {
        #region FromProcessId
        /// <summary>
        /// Returns a new <see cref="Process"/> component, given the identifier of a process.
        /// </summary>
        /// <param name="processId">The system-unique identifier of a process resource.</param>
        /// <returns>A <see cref="Process"/> component that is associated with the local process resource identified by the processId parameter.</returns>
        public static Process FromProcessId(Int32 processId)
        {
            return Process.GetProcessById(processId);
        }

        #endregion

        #region FromProcessName
        /// <summary>
        /// Creates an collection of new <see cref="Process"/> components and associates them with all the process resources that share the specified process name.
        /// </summary>
        /// <param name="processName">The friendly name of the process.</param>
        /// <returns>A collection of type <see cref="Process"/> that represents the process resources running the specified application or file.</returns>
        public static IEnumerable<Process> FromProcessName(String processName)
        {
            return Process.GetProcessesByName(processName);
        }

        #endregion

    } // End class

} // End namespace