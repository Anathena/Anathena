using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Capture.Interface
{
    [Serializable]
    public class ScreenshotReceivedEventArgs: MarshalByRefObject
    {
        public Int32 ProcessId { get; set; }
        public Screenshot Screenshot { get; set; }

        public ScreenshotReceivedEventArgs(Int32 processId, Screenshot screenshot)
        {
            ProcessId = processId;
            Screenshot = screenshot;
        }
    }
}
