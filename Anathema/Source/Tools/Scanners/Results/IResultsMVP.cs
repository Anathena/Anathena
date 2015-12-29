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
        public List<String> Labels = null;
    }

    interface IResultsView : IView
    {
        void DisplayResults(List<String> Addresses, List<String> Values);
    }

    abstract class IResultsModel : IScannerModel
    {
        // Events triggered by the model (upstream)
        public event ResultsEventHandler EventUpdateDisplay;
        protected virtual void OnEventUpdateDisplay(ResultsEventArgs E)
        {
            EventUpdateDisplay(this, E);
        }

        // Functions invoked by presenter (downstream)

    }

    class ResultsPresenter : Presenter<IResultsView, IResultsModel>
    {
        new IResultsView View;
        new IResultsModel Model;

        public ResultsPresenter(IResultsView View, IResultsModel Model) : base(View, Model)
        {
            this.View = View;
            this.Model = Model;

            // Bind events triggered by the model
            Model.EventUpdateDisplay += EventUpdateDisplay;
        }
        
        #region Method definitions called by the view (downstream)
        
        #endregion

        #region Event definitions for events triggered by the model (upstream)

        private void EventUpdateDisplay(Object Sender, ResultsEventArgs E)
        {
            if (E.Addresses.Count != E.Values.Count)
                return;

            View.DisplayResults(E.Addresses, E.Values);
        }

        #endregion
    }
}
