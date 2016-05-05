using Anathema.Scanners.ScanConstraints;
using System;

namespace Anathema.Scanners.ManualScanner
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
        new IManualScannerView View;
        new IManualScannerModel Model;

        public ManualScannerPresenter(IManualScannerView View, IManualScannerModel Model) : base(View, Model)
        {
            this.View = View;
            this.Model = Model;

            // Bind events triggered by the model
            Model.EventScanFinished += EventScanFinished;
        }

        #region Method definitions called by the view (downstream)

        public void SetScanConstraintManager(ScanConstraintManager ScanConstraintManager)
        {
            Model.SetScanConstraintManager(ScanConstraintManager);
        }

        #endregion

        #region Event definitions for events triggered by the model (upstream)

        public void EventScanFinished(Object Sender, ManualScannerEventArgs E)
        {
            View.ScanFinished();
        }

        #endregion

    } // End class

} // End namespace