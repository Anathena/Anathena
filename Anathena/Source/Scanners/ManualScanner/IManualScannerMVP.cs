using Ana.Source.Scanners.ScanConstraints;
using System;

namespace Ana.Source.Scanners.ManualScanner
{
    delegate void ManualScannerEventHandler(Object Sender, ManualScannerEventArgs Args);
    class ManualScannerEventArgs : EventArgs
    {

    }

    interface IManualScannerView : IScannerView
    {
        // Methods invoked by the presenter (upstream)
        void ScanFinished();
    }

    abstract class IManualScannerModel : IScannerModel
    {
        // Events triggered by the model (upstream)
        public event ManualScannerEventHandler EventScanFinished;
        protected virtual void OnEventScanFinished(ManualScannerEventArgs E)
        {
            EventScanFinished?.Invoke(this, E);
        }

        // Functions invoked by presenter (downstream)
        public abstract void SetScanConstraintManager(ScanConstraintManager ScanConstraintManager);
    }

    class ManualScannerPresenter : ScannerPresenter
    {
        private new IManualScannerView view { get; set; }
        private new IManualScannerModel model { get; set; }

        public ManualScannerPresenter(IManualScannerView view, IManualScannerModel model) : base(view, model)
        {
            this.view = view;
            this.model = model;

            // Bind events triggered by the model
            model.EventScanFinished += EventScanFinished;

            model.OnGUIOpen();
        }

        #region Method definitions called by the view (downstream)

        public void SetScanConstraintManager(ScanConstraintManager ScanConstraintManager)
        {
            model.SetScanConstraintManager(ScanConstraintManager);
        }

        #endregion

        #region Event definitions for events triggered by the model (upstream)

        public void EventScanFinished(Object Sender, ManualScannerEventArgs E)
        {
            view.ScanFinished();
        }

        #endregion

    } // End class

} // End namespace