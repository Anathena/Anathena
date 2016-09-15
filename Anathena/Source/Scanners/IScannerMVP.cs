using Anathena.Source.UserSettings;
using Anathena.Source.Utils;
using Anathena.Source.Utils.MVP;
using System;

namespace Anathena.Source.Scanners
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
            UpdateInterval = Settings.GetInstance().GetRescanInterval();
            base.Begin();
        }

        protected override void Update()
        {
            ScanCount++;
            UpdateInterval = Settings.GetInstance().GetRescanInterval();
        }
    }

    class ScannerPresenter : Presenter<IScannerView, IScannerModel>
    {
        private new IScannerView view { get; set; }
        private new IScannerModel model { get; set; }

        public ScannerPresenter(IScannerView view, IScannerModel model) : base(view, model)
        {
            this.view = view;
            this.model = model;

            // Bind events triggered by the model
            model.EventUpdateScanCount += EventDisplayScanCount;
        }

        #region Method definitions called by the view (downstream)

        public void BeginScan()
        {
            model.Begin();
        }

        public void EndScan()
        {
            model.TriggerEnd();
        }

        #endregion

        #region Event definitions for events triggered by the model (upstream)

        protected void EventDisplayScanCount(Object Sender, ScannerEventArgs E)
        {
            view.DisplayScanCount(E.ScanCount);
        }

        #endregion

    } // End class

} // End namespace