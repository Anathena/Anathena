using System;
using System.Collections.Generic;
using Anathema.Utils.OS;
using Anathema.Utils;
using Anathema.Services.ProcessManager;

namespace Anathema.Services.Divine
{
    class Divine : IProcessObserver
    {
        private static Divine DivineInstance;

        private OSInterface OSInterface;

        private List<NormalizedRegion> VirtualPages;

        private PagePolling PagePollingTask;
        private UpdatePageStatus UpdatePageStatusTask;
        
        private Divine()
        {
            VirtualPages = new List<NormalizedRegion>();
        }

        public static Divine GetInstance()
        {
            if (DivineInstance == null)
                DivineInstance = new Divine();
            return DivineInstance;
        }

        public void InitializeProcessObserver()
        {
            ProcessSelector.GetInstance().Subscribe(this);
        }

        public void UpdateOSInterface(OSInterface OSInterface)
        {
            this.OSInterface = OSInterface;
        }

        internal class ExternalPage : NormalizedRegion
        {
            private UInt64 Checksum;
            private DateTime LastQuery;
            private Boolean Valid;

            public ExternalPage(IntPtr BaseAddress, Int32 RegionSize) : base(BaseAddress, RegionSize)
            {

            }

        } // End class

        internal class PagePolling : RepeatedTask
        {
            public PagePolling()
            {

            }

            public override void Begin()
            {
                base.Begin();
            }

            protected override void Update()
            {

            }

            public override void End()
            {
                base.End();
            }

        } // End class

        internal class UpdatePageStatus : RepeatedTask
        {
            public UpdatePageStatus()
            {

            }

            public override void Begin()
            {
                base.Begin();
            }

            protected override void Update()
            {

            }

            public override void End()
            {
                base.End();
            }

        } // End class

    } // End class

} // End namespace