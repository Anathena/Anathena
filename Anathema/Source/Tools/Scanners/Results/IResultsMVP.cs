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
        void DisplayResults(ListViewItem[] Items);
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
            if (E.Addresses == null || E.Values == null || E.Labels == null)
                return;

            if (E.Addresses.Count != E.Values.Count && E.Addresses.Count != E.Labels.Count)
                return;

            List<ListViewItem> Items = new List<ListViewItem>();

            for (Int32 Index = 0; Index < E.Addresses.Count; Index++)
                Items.Add(new ListViewItem(new String[] { E.Addresses[Index], E.Values[Index], E.Labels[Index] }));

            View.DisplayResults(Items.ToArray());
        }

        #endregion
    }
}
