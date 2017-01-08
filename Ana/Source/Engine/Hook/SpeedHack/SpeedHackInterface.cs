namespace Ana.Source.Engine.Hook.SpeedHack
{
    using EasyHook;
    using System;
    using System.Runtime.InteropServices;

    /// <summary>
    /// Client event used to notify the hook to exit.
    /// </summary>
    [Serializable]
    public delegate void DisconnectedEvent();

    /// <summary>
    /// Interface to a hook that controls speed in an external process.
    /// </summary>
    [Serializable]
    internal class SpeedHackInterface : MarshalByRefObject, ISpeedHackInterface
    {
        private static Int64 queryPerformanceBase;

        /// <summary>
        /// Initializes a new instance of the <see cref="SpeedHackInterface" /> class.
        /// </summary>
        public SpeedHackInterface()
        {
            SpeedHackInterface.QueryPerformanceCounter(out queryPerformanceBase);
            this.Hook = LocalHook.Create(LocalHook.GetProcAddress("kernel32.dll", "QueryPerformanceCounter"), new QueryPerformanceCounter2(QueryPerformanceCounter3), this);
        }

        [UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Unicode, SetLastError = true)]
        public delegate IntPtr QueryPerformanceCounter2(out Int64 performanceCount);

        /// <summary>
        /// Gets or sets the hook to environment methods in the external process.
        /// </summary>
        private LocalHook Hook { get; set; }

        /// <summary>
        /// Sets the speed in the external process.
        /// </summary>
        /// <param name="speed">The speed multiplier.</param>
        public void SetSpeed(Double speed)
        {
        }

        private static IntPtr QueryPerformanceCounter3(out Int64 performanceCount)
        {
            performanceCount = queryPerformanceBase;
            return IntPtr.Zero;
        }

        [DllImport("kernel32.dll")]
        private static extern Boolean QueryPerformanceCounter(out Int64 performanceCount);

        [DllImport("Kernel32.dll")]
        private static extern Boolean QueryPerformanceFrequency(out Int64 performanceFrequency);

        /// <summary>
        /// Client event proxy for marshalling event handlers.
        /// </summary>
        internal class SpeedHackEventProxy : MarshalByRefObject
        {
            /// <summary>
            /// Client event used to notify the hook to exit.
            /// </summary>
            public event DisconnectedEvent Disconnected;

            public override Object InitializeLifetimeService()
            {
                // Returning null holds the object alive until it is explicitly destroyed
                return null;
            }

            public void DisconnectedProxyHandler()
            {
                this.Disconnected?.Invoke();
            }
        }
        //// End class
    }
    //// End class
}
//// End namespace