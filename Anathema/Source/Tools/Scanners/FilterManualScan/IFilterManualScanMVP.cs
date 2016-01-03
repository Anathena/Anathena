using Binarysharp.MemoryManagement;
using Binarysharp.MemoryManagement.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anathema
{
    interface IFilterManualScanView : IScannerView
    {
        // Methods invoked by the presenter (upstream)
    }

    abstract class IFilterManualScanModel : IScannerModel
    {
        // Events triggered by the model (upstream)

        // Functions invoked by presenter (downstream)
    }

    class FilterManualScanPresenter : ScannerPresenter
    {
        new IFilterManualScanView View;
        new IFilterManualScanModel Model;

        public FilterManualScanPresenter(IFilterManualScanView View, IFilterManualScanModel Model) : base(View, Model)
        {
            this.View = View;
            this.Model = Model;
            // Bind events triggered by the model
        }

        #region Method definitions called by the view (downstream)
        
        public void SetValueConstraints(ValueConstraintsEnum ValueConstraints)
        {

        }

        #endregion

        #region Event definitions for events triggered by the model (upstream)

        #endregion
    }
}
