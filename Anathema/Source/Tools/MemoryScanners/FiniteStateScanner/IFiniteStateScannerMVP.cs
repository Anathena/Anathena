using Binarysharp.MemoryManagement;
using Binarysharp.MemoryManagement.Memory;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anathema
{
    delegate void FiniteStateScannerEventHandler(Object Sender, FiniteStateScannerEventArgs Args);
    class FiniteStateScannerEventArgs : EventArgs
    {

    }

    interface IFiniteStateScannerView : IScannerView
    {
        // Methods invoked by the presenter (upstream)
        void ScanFinished();
    }

    abstract class IFiniteStateScannerModel : IScannerModel
    {
        // Events triggered by the model (upstream)
        public event FiniteStateScannerEventHandler EventUpdateDisplay;
        protected virtual void OnEventUpdateDisplay(FiniteStateScannerEventArgs E)
        {
            EventUpdateDisplay(this, E);
        }

        public event FiniteStateScannerEventHandler EventScanFinished;
        protected virtual void OnEventScanFinished(FiniteStateScannerEventArgs E)
        {
            EventScanFinished(this, E);
        }

        // Functions invoked by presenter (downstream)

    }

    class FiniteStateScannerPresenter : ScannerPresenter
    {
        new IFiniteStateScannerView View;
        new IFiniteStateScannerModel Model;

        public FiniteStateScannerPresenter(IFiniteStateScannerView View, IFiniteStateScannerModel Model) : base(View, Model)
        {
            this.View = View;
            this.Model = Model;

            // Bind events triggered by the model
            Model.EventScanFinished += EventScanFinished;
            Model.EventUpdateDisplay += EventUpdateDisplay;
        }

        #region Method definitions called by the view (downstream)

        private void ScanFinished()
        {

        }

        private void UpdateDisplay()
        {

        }

        #endregion

        #region Event definitions for events triggered by the model (upstream)

        public void EventScanFinished(Object Sender, FiniteStateScannerEventArgs E)
        {
            ScanFinished();
        }

        public void EventUpdateDisplay(Object Sender, FiniteStateScannerEventArgs E)
        {
            UpdateDisplay();
        }

        #endregion
    }
}
