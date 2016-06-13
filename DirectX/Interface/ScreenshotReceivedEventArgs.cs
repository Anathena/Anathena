using System;

namespace DirectXShell.Interface
{
    [Serializable]
    public class ScreenshotReceivedEventArgs : MarshalByRefObject
    {
        public Int32 ProcessId { get; set; }
        public Screenshot Screenshot { get; set; }

        public ScreenshotReceivedEventArgs(Int32 ProcessId, Screenshot Screenshot)
        {
            this.ProcessId = ProcessId;
            this.Screenshot = Screenshot;
        }

    } // End class

} // End namespace