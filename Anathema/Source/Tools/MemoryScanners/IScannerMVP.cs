using Binarysharp.MemoryManagement;
using Binarysharp.MemoryManagement.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Anathema
{
    delegate void ScannerEventHandler(Object Sender, ScannerEventArgs Args);
    class ScannerEventArgs : EventArgs
    {
        public Int32 ScanCount;
        public ScannerEventArgs() { }
        public ScannerEventArgs(Int32 ScanCount) { this.ScanCount = ScanCount; }
    }

    interface IScannerView : IView
    {
        // Methods invoked by the presenter (upstream)
        void DisplayScanCount(Int32 ScanCount);
    }

    abstract class IScannerModel : RepeatedTask, IModel
    {
        public Int32 ScanCount;
        public event ScannerEventHandler EventUpdateScanCount;
        protected virtual void OnEventUpdateScanCount(ScannerEventArgs E)
        {
            EventUpdateScanCount(this, E);
        }

        public override void Begin()
        {
            ScanCount = 0;
            WaitTime = Settings.GetInstance().GetRescanInterval();
            base.Begin();
        }

        protected override void Update()
        {
            ScanCount++;
        }
    }

    class ScannerPresenter : Presenter<IScannerView, IScannerModel>
    {
        public ScannerPresenter(IScannerView View, IScannerModel Model) : base(View, Model)
        {
            // Bind events triggered by the model
            Model.EventUpdateScanCount += EventDisplayScanCount;
        }

        #region Method definitions called by the view (downstream)

        public void BeginScan()
        {
            Model.Begin();
        }

        public void EndScan()
        {
            Model.End();
        }

        #endregion

        #region Event definitions for events triggered by the model (upstream)

        protected void EventDisplayScanCount(Object Sender, ScannerEventArgs E)
        {
            View.DisplayScanCount(E.ScanCount);
        }

        #endregion
    }
}
