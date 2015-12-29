using Binarysharp.MemoryManagement;
using Binarysharp.MemoryManagement.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anathema
{
    interface IFilterView : IView
    {
        // Methods invoked by the presenter (upstream)
        void EventFilterFinished(List<RemoteRegion> MemoryRegions);
    }

    interface IFilterModel : IModel, IProcessObserver
    {
        // Events triggered by the model (upstream)
        event EventHandler EventFilterFinished;

        // Functions invoked by presenter (downstream)
        void BeginScan();
        void EndScan();
    }

    class FilterPresenter : Presenter<IFilterView, IFilterModel>
    {
        public FilterPresenter(IFilterView View, IFilterModel Model) : base(View, Model)
        {
            // Bind events triggered by the model
            Model.EventFilterFinished += EndFilterRequired;
        }
        
        #region Method definitions called by the view (downstream)

        public void BeginFilter()
        {
            Model.BeginScan();
        }

        public void EndFilter()
        {
            Model.EndScan();
        }

        #endregion

        #region Event definitions for events triggered by the model (upstream)

        private void EndFilterRequired(Object Sender, EventArgs E)
        {
            View.EventFilterFinished(null);
        }

        #endregion
    }
}
