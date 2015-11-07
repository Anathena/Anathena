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
        void EndFilterRequired();
    }

    interface IFilterModel : IModel
    {
        // Events triggered by the model (upstream)
        event EventHandler EndFilterRequired;

        // Functions invoked by presenter (downstream)
        void UpdateProcess(MemorySharp MemoryEditor);
        void BeginFilter(MemorySharp MemoryEditor, List<RemoteRegion> MemoryRegions);
        List<RemoteRegion> EndFilter();
        void AbortFilter();
    }

    class FilterPresenter : Presenter<IFilterView, IFilterModel>
    {
        public FilterPresenter(IFilterView View, IFilterModel Model) : base(View, Model)
        {
            // Bind events triggered by the model
            Model.EndFilterRequired += EndFilterRequired;
        }

        #region Method definitions called by the view (downstream)

        public void UpdateProcess(MemorySharp MemoryEditor)
        {
            Model.UpdateProcess(MemoryEditor);
        }

        #endregion

        #region Event definitions for events triggered by the model (upstream)

        private void EndFilterRequired(object sender, EventArgs e)
        {
            View.EndFilterRequired();
        }

        #endregion
    }
}
