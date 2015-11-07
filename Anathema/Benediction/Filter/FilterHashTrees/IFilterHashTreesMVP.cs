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

    }

    interface IFilterHashTreesModel : IFilterModel
    {

    }

    class FilterHashTreesPresenter : FilterPresenter
    {
        public FilterHashTreesPresenter(IFilterView View, IFilterModel Model) : base(View, Model)
        {
            // Bind events triggered by the model
            //Model.EndFilterRequired += EndFilterRequired;
        }

        #region Method definitions called by the view (downstream)

        public void k(MemorySharp MemoryEditor)
        {
            Model.UpdateProcess(MemoryEditor);
        }

        #endregion

        #region Event definitions for events triggered by the model (upstream)

        private void Nice(object sender, EventArgs e)
        {
            View.EndFilterRequired();
        }

        #endregion
    }
}
