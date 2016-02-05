using Binarysharp.MemoryManagement;
using Binarysharp.MemoryManagement.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anathema
{
    interface ITreeScannerView : IScannerView
    {
        // Methods invoked by the presenter (upstream)

    }

    abstract class ITreeScannerModel : IScannerModel
    {
        // Events triggered by the model (upstream)

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
        }

        #region Method definitions called by the view (downstream)

        #endregion

        #region Event definitions for events triggered by the model (upstream)
        
        #endregion
    }
}
