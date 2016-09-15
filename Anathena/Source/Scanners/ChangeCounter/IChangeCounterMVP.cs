using System;

namespace Anathena.Source.Scanners.ChangeCounter
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
        private new IChangeCounterView view { get; set; }
        private new IChangeCounterModel model { get; set; }

        public ChangeCounterPresenter(IChangeCounterView view, IChangeCounterModel model) : base(view, model)
        {
            this.view = view;
            this.model = model;

            // Bind events triggered by the model


            model.OnGUIOpen();
        }

        #region Method definitions called by the view (downstream)

        public void SetMinChanges(UInt16 MinChanges)
        {
            model.SetMinChanges(MinChanges);
        }

        public void SetMaxChanges(UInt16 MaxChanges)
        {
            model.SetMaxChanges(MaxChanges);
        }

        public void SetVariableSize(Int32 VariableSize)
        {
            if (VariableSize <= 0)
                return;

            model.SetVariableSize(VariableSize);
        }

        #endregion

        #region Event definitions for events triggered by the model (upstream)

        #endregion

    } // End class

} // End namespace