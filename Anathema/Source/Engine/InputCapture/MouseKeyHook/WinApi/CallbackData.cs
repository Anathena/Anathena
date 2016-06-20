using System;

namespace Anathema.Source.Engine.InputCapture.MouseKeyHook.WinApi
{
    internal struct CallbackData
    {
        private readonly IntPtr MLParam;
        private readonly IntPtr MWParam;

        public CallbackData(IntPtr WParam, IntPtr LParam)
        {
            MWParam = WParam;
            MLParam = LParam;
        }

        public IntPtr WParam
        {
            get { return MWParam; }
        }

        public IntPtr LParam
        {
            get { return MLParam; }
        }

    } // End class

} // End namespace