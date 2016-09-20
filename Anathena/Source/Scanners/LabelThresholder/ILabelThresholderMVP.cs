using Ana.Source.Utils.MVP;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace Ana.Source.Scanners.LabelThresholder
{
    delegate void LabelThresholderEventHandler(Object Sender, LabelThresholderEventArgs Args);
    class LabelThresholderEventArgs : EventArgs
    {
        public SortedDictionary<dynamic, Int64> SortedDictionary = null;
    }

    interface ILabelThresholderView : IView
    {
        // Methods invoked by the presenter (upstream)
        void DisplayHistogram(SortedDictionary<dynamic, Int64> SortedDictionary);
    }

    abstract class ILabelThresholderModel : IScannerModel
    {
        // Events triggered by the model (upstream)
        public event LabelThresholderEventHandler EventUpdateHistogram;
        protected virtual void OnEventUpdateHistogram(LabelThresholderEventArgs E)
        {
            EventUpdateHistogram?.Invoke(this, E);
        }

        // Functions invoked by presenter (downstream)
        public abstract void ApplyThreshold();
        public abstract void UpdateThreshold(Int32 MinimumIndex, Int32 MaximumIndex);
        public abstract void SetInverted(Boolean Inverted);
        public abstract Type GetElementType();
    }

    class LabelThresholderPresenter : Presenter<ILabelThresholderView, ILabelThresholderModel>
    {
        private new ILabelThresholderView view { get; set; }
        private new ILabelThresholderModel model { get; set; }

        public LabelThresholderPresenter(ILabelThresholderView view, ILabelThresholderModel model) : base(view, model)
        {
            this.view = view;
            this.model = model;

            // Bind events triggered by the model
            model.EventUpdateHistogram += EventUpdateHistogram;

            model.OnGUIOpen();
        }

        #region Method definitions called by the view (downstream)

        public void Begin()
        {
            model.Begin();
        }

        public void ApplyThreshold()
        {
            model.ApplyThreshold();
        }

        public void UpdateThreshold(Int32 MinimumIndex, Int32 MaximumIndex)
        {
            model.UpdateThreshold(MinimumIndex, MaximumIndex);
        }

        public void SetInverted(Boolean Inverted)
        {
            model.SetInverted(Inverted);
        }

        public Int32 GetElementSize()
        {
            if (model.GetElementType() == null)
                return 0;

            return Marshal.SizeOf(model.GetElementType());
        }

        #endregion

        #region Event definitions for events triggered by the model (upstream)

        void EventUpdateHistogram(Object Sender, LabelThresholderEventArgs E)
        {
            view.DisplayHistogram(E.SortedDictionary);
        }

        #endregion

    } // End class

} // End namespace