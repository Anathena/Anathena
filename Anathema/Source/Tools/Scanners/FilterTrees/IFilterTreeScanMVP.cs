using Binarysharp.MemoryManagement;
using Binarysharp.MemoryManagement.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anathema
{
    delegate void TreeScannerEventHandler(Object Sender, TreeScannerEventArgs Args);
    class TreeScannerEventArgs : EventArgs
    {
        public UInt64? FilterResultSize = null;
    }

    interface ITreeScannerView : IScannerView
    {
        // Methods invoked by the presenter (upstream)
        void DisplayResultSize(UInt64 FilterResultSize);
    }

    abstract class ITreeScannerModel : IScannerModel
    {
        // Events triggered by the model (upstream)
        public event TreeScannerEventHandler EventUpdateMemorySize;
        protected virtual void OnEventUpdateMemorySize(TreeScannerEventArgs E)
        {
            EventUpdateMemorySize(this, E);
        }

        // Functions invoked by presenter (downstream)
    }

    class TreeScannerPresenter : ScannerPresenter
    {
        new ITreeScannerView View;
        new ITreeScannerModel Model;

        public TreeScannerPresenter(ITreeScannerView View, ITreeScannerModel Model) : base(View, Model)
        {
            this.View = View;
            this.Model = Model;
            // Bind events triggered by the model
            Model.EventUpdateMemorySize += EventUpdateMemorySize;
        }

        #region Method definitions called by the view (downstream)

        #endregion

        #region Event definitions for events triggered by the model (upstream)

        private void EventUpdateMemorySize(Object Sender, TreeScannerEventArgs E)
        {
            if (E.FilterResultSize.HasValue)
                View.DisplayResultSize(E.FilterResultSize.Value);
        }

        #endregion
    }
}
