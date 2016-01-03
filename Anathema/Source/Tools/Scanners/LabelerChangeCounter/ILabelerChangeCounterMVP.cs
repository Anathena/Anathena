using Binarysharp.MemoryManagement;
using Binarysharp.MemoryManagement.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anathema
{
    interface ILabelerChangeCounterView : IScannerView
    {
        // Methods invoked by the presenter (upstream)
    }

    abstract class ILabelerChangeCounterModel : IScannerModel
    {
        // Events triggered by the model (upstream)

        // Functions invoked by presenter (downstream)
        public abstract void SetMinChanges(Int32 MinChanges);
        public abstract void SetMaxChanges(Int32 MaxChanges);
        public abstract void SetVariableSize(Int32 VariableSize);
        public abstract void SetScanModeChanging();
        public abstract void SetScanModeIncreasing();
        public abstract void SetScanModeDecreasing();
    }

    class LabelerChangeCounterPresenter : ScannerPresenter
    {
        new ILabelerChangeCounterView View;
        new ILabelerChangeCounterModel Model;

        public LabelerChangeCounterPresenter(ILabelerChangeCounterView View, ILabelerChangeCounterModel Model) : base(View, Model)
        {
            this.View = View;
            this.Model = Model;
            // Bind events triggered by the model
        }

        #region Method definitions called by the view (downstream)

        public void SetMinChanges(Int32 MinChanges)
        {
            if (MinChanges < 0)
                return;

            Model.SetMinChanges(MinChanges);
        }

        public void SetMaxChanges(Int32 MaxChanges)
        {
            if (MaxChanges < 0)
                return;

            Model.SetMaxChanges(MaxChanges);
        }

        public void SetVariableSize(Int32 VariableSize)
        {
            if (VariableSize <= 0)
                return;

            Model.SetVariableSize(VariableSize);
        }
        
        public void SetScanModeChanging()
        {
            Model.SetScanModeChanging();
        }

        public void SetScanModeIncreasing()
        {
            Model.SetScanModeIncreasing();
        }

        public void SetScanModeDecreasing()
        {
            Model.SetScanModeDecreasing();
        }


        #endregion

        #region Event definitions for events triggered by the model (upstream)

        #endregion
    }
}
