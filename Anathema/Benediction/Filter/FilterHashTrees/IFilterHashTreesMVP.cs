using Binarysharp.MemoryManagement;
using Binarysharp.MemoryManagement.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anathema
{
    interface IFilterHashTreesView : IFilterView
    {
        // Methods invoked by the presenter (upstream)

        void DisplaySplitCount(UInt64 SplitCount);
    }

    interface IFilterHashTreesModel : IFilterModel
    {

        // Events triggered by the model (upstream)
        event EventHandler EventSplitCountChanged;

        // Functions invoked by presenter (downstream)

    }

    class FilterHashTreesPresenter : FilterPresenter
    {
        public FilterHashTreesPresenter(IFilterHashTreesView View, IFilterHashTreesModel Model) : base(View, Model)
        {
            // Bind events triggered by the model
            Model.EventSplitCountChanged += EventSplitCountChanged;
        }

        #region Method definitions called by the view (downstream)

        #endregion

        #region Event definitions for events triggered by the model (upstream)

        private void EventSplitCountChanged(object sender, EventArgs e)
        {
            View.EventFilterFinished(null);
        }

        #endregion
    }
}
