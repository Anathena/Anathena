namespace Ana.Source.Engine.Processes
{
    using System.Collections.Generic;

    /// <summary>
    /// An interface for an object that enumerates and selects processes running on the system
    /// </summary>
    public interface IProcesses
    {
        /// <summary>
        /// Gets all running processes on the system
        /// </summary>
        /// <returns>An enumeration of see <see cref="NormalizedProcess" /></returns>
        IEnumerable<NormalizedProcess> GetProcesses();
    }
    //// End interface
}
//// End namespace