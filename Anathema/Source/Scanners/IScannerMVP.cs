using Anathema.Source.Utils;
using Anathema.Source.Utils.MVP;
using Anathema.Source.Utils.Setting;
using System;

namespace Anathema.Source.Scanners
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

        public virtual void OnGUIOpen() { }

        protected virtual void OnEventUpdateScanCount(ScannerEventArgs E)
        {
            EventUpdateScanCount?.Invoke(this, E);
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
            WaitTime = Settings.GetInstance().GetRescanInterval();
        }
    }

    class ScannerPresenter : Presenter<IScannerView, IScannerModel>
    {
        private new IScannerView View { get; set; }
        private new IScannerModel Model { get; set; }

        public ScannerPresenter(IScannerView View, IScannerModel Model) : base(View, Model)
        {
            this.View = View;
            this.Model = Model;

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
            Model.TriggerEnd();
        }

        #endregion

        #region Event definitions for events triggered by the model (upstream)

        protected void EventDisplayScanCount(Object Sender, ScannerEventArgs E)
        {
            View.DisplayScanCount(E.ScanCount);
        }

        #endregion

    } // End class

} // End namespace