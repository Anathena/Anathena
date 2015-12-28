using Binarysharp.MemoryManagement;
using Binarysharp.MemoryManagement.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anathema
{
    delegate void FilterTreeScanEventHandler(Object Sender, FilterTreesEventArgs Args);
    class FilterTreesEventArgs : EventArgs
    {
        public UInt64? FilterResultSize = null;
    }

    interface IFilterTreeScanView : IFilterView
    {
        // Methods invoked by the presenter (upstream)
        void DisplayResultSize(UInt64 FilterResultSize);
    }

    interface IFilterTreeScanModel : IFilterModel
    {
        // Events triggered by the model (upstream)
        event FilterTreeScanEventHandler EventUpdateMemorySize;

        // Functions invoked by presenter (downstream)
    }

    class FilterTreeScanPresenter : FilterPresenter
    {
        new IFilterTreeScanView View;
        new IFilterTreeScanModel Model;

        public FilterTreeScanPresenter(IFilterTreeScanView View, IFilterTreeScanModel Model) : base(View, Model)
        {
            this.View = View;
            this.Model = Model;
            // Bind events triggered by the model
            Model.EventUpdateMemorySize += EventUpdateMemorySize;
        }

        #region Method definitions called by the view (downstream)

        #endregion

        #region Event definitions for events triggered by the model (upstream)

        private void EventUpdateMemorySize(Object Sender, FilterTreesEventArgs E)
        {
            if (E.FilterResultSize.HasValue)
                View.DisplayResultSize(E.FilterResultSize.Value);
        }

        #endregion
    }
}
