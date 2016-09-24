namespace Ana.Source.Engine.Processes
{
    using System;
    using System.Drawing;

    /// <summary>
    /// A platform independent object to store process information
    /// </summary>
    internal class NormalizedProcess
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NormalizedProcess" /> class
        /// </summary>
        /// <param name="processId">The process id</param>
        /// <param name="processName">The process name</param>
        /// <param name="startTime">The time the process was created</param>
        /// <param name="isSystemProcess">Whether or not the process is a system process</param>
        /// <param name="icon">The icon associated with the process</param>
        public NormalizedProcess(Int32 processId, String processName, DateTime startTime, Boolean isSystemProcess, Icon icon)
        {
            this.ProcessId = processId;
            this.ProcessName = processName;
            this.StartTime = startTime;
            this.IsSystemProcess = isSystemProcess;
            this.Icon = icon;
        }

        /// <summary>
        /// Gets the id of the running process
        /// </summary>
        public Int32 ProcessId { get; private set; }

        /// <summary>
        /// Gets the name of the running process
        /// </summary>
        public String ProcessName { get; private set; }

        /// <summary>
        /// Gets the time that the process began execution
        /// </summary>
        public DateTime StartTime { get; private set; }

        /// <summary>
        /// Gets a value indicating whether the process is a system process
        /// </summary>
        public Boolean IsSystemProcess { get; private set; }

        /// <summary>
        /// Gets the icon associated with this process
        /// </summary>
        public Icon Icon { get; private set; }

        /*
            override this.Equals(otherObject) =
                   match otherObject with
                     | :? NormalizedProcess as otherProcess -> (this.processId = otherProcess.processId)
                     | _ -> false

            override this.GetHashCode() =
                hash(this.processId)

            /// <summary>
            /// Priority for sorting is as follows:
            /// - Sort by time since the processes started (newest first)
            ///     - Since this property is not accessable for system processes, sort those by id
            /// - Place system processes below standard processes
            /// </summary>
                interface System.IComparable with
                member this.CompareTo otherObject =
                    match otherObject with
                        | :? NormalizedProcess as otherProcess ->
                            if (this.isSystemProcess && not otherProcess.isSystemProcess) then 1
                            else if (otherProcess.isSystemProcess && not this.isSystemProcess) then -1
                            else if (not this.isSystemProcess && not otherProcess.isSystemProcess) then -this.startTime.CompareTo(otherProcess.startTime)
                            else this.processId.CompareTo(otherProcess.processId)
                        | _ -> invalidArg "otherProcess" "Objects do not have the same type" 
                */
    }
    //// End class
}
//// End namespace