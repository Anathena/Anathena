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
        public ScriptItem ScriptItem;
    }

    interface ILabelThresholderView : IView
    {
        // Methods invoked by the presenter (upstream)
    }

    interface ILabelThresholderModel : IModel, IProcessObserver
    {
        // Events triggered by the model (upstream)
        event LabelThresholderEventHandler EventDisplayScript;

        // Functions invoked by presenter (downstream)
    }

    class LabelThresholderPresenter : Presenter<ILabelThresholderView, ILabelThresholderModel>
    {
        public LabelThresholderPresenter(ILabelThresholderView View, ILabelThresholderModel Model) : base(View, Model)
        {
            // Bind events triggered by the model
            Model.EventDisplayScript += EventDisplayScript;
        }

        #region Method definitions called by the view (downstream)
        
        #endregion

        #region Event definitions for events triggered by the model (upstream)

        void EventDisplayScript(Object Sender, LabelThresholderEventArgs E)
        {

        }

        #endregion
    }
}
