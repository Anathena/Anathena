using Anathema.Utils.MVP;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace Anathema.Scanners.LabelThresholder
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
        private new ILabelThresholderView View;
        private new ILabelThresholderModel Model;

        public LabelThresholderPresenter(ILabelThresholderView View, ILabelThresholderModel Model) : base(View, Model)
        {
            this.View = View;
            this.Model = Model;

            // Bind events triggered by the model
            Model.EventUpdateHistogram += EventUpdateHistogram;

            Model.OnGUIOpen();
        }

        #region Method definitions called by the view (downstream)

        public void Begin()
        {
            Model.Begin();
        }

        public void ApplyThreshold()
        {
            Model.ApplyThreshold();
        }

        public void UpdateThreshold(Int32 MinimumIndex, Int32 MaximumIndex)
        {
            Model.UpdateThreshold(MinimumIndex, MaximumIndex);
        }

        public void SetInverted(Boolean Inverted)
        {
            Model.SetInverted(Inverted);
        }

        public Int32 GetElementSize()
        {
            if (Model.GetElementType() == null)
                return 0;

            return Marshal.SizeOf(Model.GetElementType());
        }

        #endregion

        #region Event definitions for events triggered by the model (upstream)

        void EventUpdateHistogram(Object Sender, LabelThresholderEventArgs E)
        {
            View.DisplayHistogram(E.SortedDictionary);
        }

        #endregion

    } // End class

} // End namespace