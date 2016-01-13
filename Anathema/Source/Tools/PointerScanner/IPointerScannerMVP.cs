using Binarysharp.MemoryManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anathema
{
    delegate void PointerScannerEventHandler(Object Sender, PointerScannerEventArgs Args);
    class PointerScannerEventArgs : EventArgs
    {
        public String ProcessTitle = String.Empty;
    }

    interface IPointerScannerView : IView
    {
        // Methods invoked by the presenter (upstream)
    }

    interface IPointerScannerModel : IModel, IProcessObserver
    {
        // Events triggered by the model (upstream)
        event PointerScannerEventHandler EventUpdateProcessTitle;

        // Functions invoked by presenter (downstream)
    }

    class PointerScannerPresenter : Presenter<IPointerScannerView, IPointerScannerModel>
    {
        public PointerScannerPresenter(IPointerScannerView View, IPointerScannerModel Model) : base(View, Model)
        {
            // Bind events triggered by the model
        }

        #region Method definitions called by the view (downstream)

        #endregion

        #region Event definitions for events triggered by the model (upstream)
        

        #endregion
    }
}
