namespace Ana.Source.Engine.Processes
{
    using System;
    using System.Drawing;

    internal class NormalizedProcess
    {
        public NormalizedProcess(Int32 processId, String processName, DateTime startTime, Boolean isSystemProcess, IntPtr handle, Icon icon)
        {
            this.processId = processId;
            this.ProcessName = processName;
            this.StartTime = startTime;
            this.IsSystemProcess = isSystemProcess;
            this.Handle = handle;
            this.Icon = icon;
        }

        public Int32 processId { get; set; }

        public String ProcessName { get; set; }

        public DateTime StartTime { get; set; }

        public Boolean IsSystemProcess { get; set; }

        public IntPtr Handle { get; set; }

        public Icon Icon { get; set; }

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