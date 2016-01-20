using Binarysharp.MemoryManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anathema
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

    interface ILabelThresholderModel : IModel, IProcessObserver
    {
        // Events triggered by the model (upstream)
        event LabelThresholderEventHandler EventUpdateHistogram;

        // Functions invoked by presenter (downstream)
        void GatherData();
        void ApplyThreshold(Int32 MinimumIndex, Int32 MaximumIndex);
        void SetInverted(Boolean Inverted);
    }

    class LabelThresholderPresenter : Presenter<ILabelThresholderView, ILabelThresholderModel>
    {
        public LabelThresholderPresenter(ILabelThresholderView View, ILabelThresholderModel Model) : base(View, Model)
        {
            // Bind events triggered by the model
            Model.EventUpdateHistogram += EventUpdateHistogram;
        }

        #region Method definitions called by the view (downstream)
        
        public void GatherData()
        {
            Model.GatherData();
        }

        public void ApplyThreshold(Int32 MinimumIndex, Int32 MaximumIndex)
        {
            Model.ApplyThreshold(MinimumIndex, MaximumIndex);
        }

        public void SetInverted(Boolean Inverted)
        {
            Model.SetInverted(Inverted);
        }

        #endregion

        #region Event definitions for events triggered by the model (upstream)

        void EventUpdateHistogram(Object Sender, LabelThresholderEventArgs E)
        {
            View.DisplayHistogram(E.SortedDictionary);
        }

        #endregion
    }
}
