using EasyHook;
using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Anathema.Source.SystemInternals.SpeedHack
{
    [Serializable]
    public class SpeedHackInterface : MarshalByRefObject, ISpeedHackInterface
    {
        [UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Unicode, SetLastError = true)]
        public delegate IntPtr QueryPerformanceCounter2(out Int64 LPPerformanceCount);

        public Int32 ProcessId { get; set; }
        private LocalHook Hook;


        public event DisconnectedEvent Disconnected;

        public SpeedHackInterface()
        {
            MessageBox.Show("okay then");
            //Hook = LocalHook.Create(LocalHook.GetProcAddress("kernel32.dll", "QueryPerformanceCounter"), new QueryPerformanceCounter2(QueryPerformanceCounter3), this);
        }

        public void SetSpeed(Double Speed)
        {

        }

        public void Disconnect()
        {

        }

        public void Ping() { }

        private static IntPtr QueryPerformanceCounter3(out Int64 LPPerformanceCount)
        {
            LPPerformanceCount = 0;
            return IntPtr.Zero;
        }


        [DllImport("kernel32.dll")]
        private static extern Boolean QueryPerformanceCounter(out Int64 LPPerformanceCount);
        [DllImport("Kernel32.dll")]
        private static extern Boolean QueryPerformanceFrequency(out Int64 LPFrequency);

    } // End class

    [Serializable]
    public delegate void DisconnectedEvent();

    /// <summary>
    /// Client event proxy for marshalling event handlers
    /// </summary>
    public class SpeedHackEventProxy : MarshalByRefObject
    {
        /// <summary>
        /// Client event used to notify the hook to exit
        /// </summary>
        public event DisconnectedEvent Disconnected;

        public override Object InitializeLifetimeService()
        {
            //Returning null holds the object alive
            //until it is explicitly destroyed
            return null;
        }

        public void DisconnectedProxyHandler()
        {
            Disconnected?.Invoke();
        }

    } // End class

} // End namespace