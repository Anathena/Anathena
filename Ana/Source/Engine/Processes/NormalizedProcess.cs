namespace Ana.Source.Engine.Processes
{
    using System;
    using System.Drawing;

    internal class NormalizedProcess
    {
        public Int32 processId { get; set; }
        public String processName { get; set; }
        public DateTime startTime { get; set; }
        public Boolean isSystemProcess { get; set; }
        public Icon icon { get; set; }

        public NormalizedProcess(Int32 processId, String processName, DateTime startTime, Boolean isSystemProcess, Icon icon)
        {
            this.processId = processId;
            this.processName = processName;
            this.startTime = startTime;
            this.isSystemProcess = isSystemProcess;
            this.icon = icon;
        }

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