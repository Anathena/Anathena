using Binarysharp.MemoryManagement;
using Binarysharp.MemoryManagement.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anathema
{
    interface IChangeCounterView : IScannerView
    {
        // Methods invoked by the presenter (upstream)
    }

    abstract class IChangeCounterModel : IScannerModel
    {
        // Events triggered by the model (upstream)

        // Functions invoked by presenter (downstream)
        public abstract void SetMinChanges(Int32 MinChanges);
        public abstract void SetMaxChanges(Int32 MaxChanges);
        public abstract void SetVariableSize(Int32 VariableSize);
    }

    class ChangeCounterPresenter : ScannerPresenter
    {
        new IChangeCounterView View;
        new IChangeCounterModel Model;

        public ChangeCounterPresenter(IChangeCounterView View, IChangeCounterModel Model) : base(View, Model)
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

        #endregion

        #region Event definitions for events triggered by the model (upstream)

        #endregion
    }
}
