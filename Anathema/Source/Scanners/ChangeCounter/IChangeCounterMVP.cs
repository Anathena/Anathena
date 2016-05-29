using System;

namespace Anathema.Source.Scanners.ChangeCounter
{
    interface IChangeCounterView : IScannerView
    {
        // Methods invoked by the presenter (upstream)
    }

    abstract class IChangeCounterModel : IScannerModel
    {
        // Events triggered by the model (upstream)

        // Functions invoked by presenter (downstream)
        public abstract void SetMinChanges(UInt16 MinChanges);
        public abstract void SetMaxChanges(UInt16 MaxChanges);
        public abstract void SetVariableSize(Int32 VariableSize);
    }

    class ChangeCounterPresenter : ScannerPresenter
    {
        private new IChangeCounterView View { get; set; }
        private new IChangeCounterModel Model { get; set; }

        public ChangeCounterPresenter(IChangeCounterView View, IChangeCounterModel Model) : base(View, Model)
        {
            this.View = View;
            this.Model = Model;

            // Bind events triggered by the model


            Model.OnGUIOpen();
        }

        #region Method definitions called by the view (downstream)

        public void SetMinChanges(UInt16 MinChanges)
        {
            Model.SetMinChanges(MinChanges);
        }

        public void SetMaxChanges(UInt16 MaxChanges)
        {
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

    } // End class

} // End namespace