using System;

namespace ScintillaNET
{
    // For internal use only
    internal sealed class SCNotificationEventArgs : EventArgs
    {
        public NativeMethods.SCNotification SCNotification { get; private set; }

        public SCNotificationEventArgs(NativeMethods.SCNotification scn)
        {
            this.SCNotification = scn;
        }
    }
}
