using Binarysharp.MemoryManagement;
using Binarysharp.MemoryManagement.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anathema
{
    delegate void FilterTreeScanEventHandler(Object Sender, FilterHashTreesEventArgs Args);
    class FilterHashTreesEventArgs : EventArgs
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
        event FilterTreeScanEventHandler EventSplitCountChanged;
        event FilterTreeScanEventHandler EventTreeSizeChanged;

        // Functions invoked by presenter (downstream)
        void SetLeafSize(UInt64 LeafSize);
        void SetVariableSize(UInt64 VariableSize);
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
            Model.EventTreeSizeChanged += EventTreeSizeChanged;
        }

        #region Method definitions called by the view (downstream)

        public void SetLeafSize(UInt64 LeafSize)
        {
            if (LeafSize <= 0)
                return;

            Model.SetLeafSize(LeafSize);
        }

        public void SetVariableSize(UInt64 VariableSize)
        {
            if (VariableSize <= 0)
                return;

            Model.SetVariableSize(VariableSize);
        }

        #endregion

        #region Event definitions for events triggered by the model (upstream)

        private void EventTreeSizeChanged(object sender, FilterHashTreesEventArgs e)
        {
            if (e.FilterResultSize.HasValue)
                View.DisplayResultSize(e.FilterResultSize.Value);
        }

        #endregion
    }
}
