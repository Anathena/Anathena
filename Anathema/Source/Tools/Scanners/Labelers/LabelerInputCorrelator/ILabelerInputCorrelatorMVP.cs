using Binarysharp.MemoryManagement;
using Binarysharp.MemoryManagement.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anathema
{
    interface ILabelerInputCorrelatorView : IScannerView
    {
        // Methods invoked by the presenter (upstream)
    }

    abstract class ILabelerInputCorrelatorModel : IScannerModel
    {
        // Events triggered by the model (upstream)

        // Functions invoked by presenter (downstream)
        public abstract void SetVariableSize(Int32 VariableSize);
    }

    class LabelerInputCorrelatorPresenter : ScannerPresenter
    {
        new ILabelerInputCorrelatorView View;
        new ILabelerInputCorrelatorModel Model;

        public LabelerInputCorrelatorPresenter(ILabelerInputCorrelatorView View, ILabelerInputCorrelatorModel Model) : base(View, Model)
        {
            this.View = View;
            this.Model = Model;
            // Bind events triggered by the model
        }

        #region Method definitions called by the view (downstream)

        public void SetVariableSize(Int32 VariableSize)
        {
            if (VariableSize <= 0)
                return;

            Model.SetVariableSize(VariableSize);
        }

        #endregion

        #region Event definitions for events triggered by the model (upstream)

        #endregion
    }
}
