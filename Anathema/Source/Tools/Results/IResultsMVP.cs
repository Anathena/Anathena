using Binarysharp.MemoryManagement;
using Binarysharp.MemoryManagement.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Anathema
{
    delegate void ResultsEventHandler(Object Sender, ResultsEventArgs Args);
    class ResultsEventArgs : EventArgs
    {
        public List<String> Addresses = null;
        public List<String> Values = null;
    }

    interface IResultsView : IView
    {
        void DisplayResults(List<String> Addresses, List<String> Values);
    }

    interface IResultsModel : IModel, IProcessObserver
    {
        // Events triggered by the model (upstream)
        event ResultsEventHandler EventUpdateDisplay;

        // Functions invoked by presenter (downstream)

    }

    class ResultsPresenter : Presenter<IResultsView, IResultsModel>
    {
        public ResultsPresenter(IResultsView View, IResultsModel Model) : base(View, Model)
        {
            // Bind events triggered by the model
            Model.EventUpdateDisplay += EventUpdateDisplay;
        }
        
        #region Method definitions called by the view (downstream)

        public void UpdateMemoryEditor(MemorySharp MemoryEditor)
        {
            Model.UpdateMemoryEditor(MemoryEditor);
        }

        #endregion

        #region Event definitions for events triggered by the model (upstream)

        private void EventUpdateDisplay(Object sender, ResultsEventArgs e)
        {
            if (e.Addresses.Count != e.Values.Count)
                return;

            View.DisplayResults(e.Addresses, e.Values);
        }

        #endregion
    }
}
