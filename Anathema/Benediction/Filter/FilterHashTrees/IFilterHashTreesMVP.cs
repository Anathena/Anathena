using Binarysharp.MemoryManagement;
using Binarysharp.MemoryManagement.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anathema
{
    delegate void FilterHashTreesEventHandler(object sender, FilterHashTreesEventArgs args);
    class FilterHashTreesEventArgs : EventArgs
    {
        public UInt64? SplitCount = null;
        public UInt64? TreeSize = null;
    }

    interface IFilterHashTreesView : IFilterView
    {
        // Methods invoked by the presenter (upstream)
        void DisplaySplitCount(UInt64 SplitCount);

        void DisplayTreeSize(UInt64 SplitCount);
    }

    interface IFilterHashTreesModel : IFilterModel
    {
        // Events triggered by the model (upstream)
        event FilterHashTreesEventHandler EventSplitCountChanged;
        event FilterHashTreesEventHandler EventTreeSizeChanged;

        // Functions invoked by presenter (downstream)

    }

    class FilterHashTreesPresenter : FilterPresenter
    {
        public FilterHashTreesPresenter(IFilterHashTreesView View, IFilterHashTreesModel Model) : base(View, Model)
        {
            // Bind events triggered by the model
            Model.EventSplitCountChanged += EventSplitCountChanged;
            Model.EventTreeSizeChanged += EventTreeSizeChanged;
        }

        #region Method definitions called by the view (downstream)

        #endregion

        #region Event definitions for events triggered by the model (upstream)

        private void EventSplitCountChanged(object sender, FilterHashTreesEventArgs e)
        {
            if (e.SplitCount.HasValue)
                ((IFilterHashTreesView)View).DisplaySplitCount(e.SplitCount.Value);
        }

        private void EventTreeSizeChanged(object sender, FilterHashTreesEventArgs e)
        {
            if (e.TreeSize.HasValue)
                ((IFilterHashTreesView)View).DisplayTreeSize(e.TreeSize.Value);
        }

        #endregion
    }
}
