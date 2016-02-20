using Anathema.MemoryManagement;
using Anathema.MemoryManagement.Memory;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Anathema
{
    delegate void TableEventHandler(Object Sender, TableEventArgs Args);
    class TableEventArgs : EventArgs
    {
        public Int32 ItemCount = 0;
        public Int32 ClearCacheIndex = -1;
    }

    interface ITableView : IView
    {
        // Methods invoked by the presenter (upstream)
    }

    interface ITableModel : IModel
    {
        Boolean SaveTable(String Path);
        Boolean OpenTable(String Path);
        Boolean MergeTable(String Path);
    }
    
    class TablePresenter : Presenter<ITableView, ITableModel>
    {
        protected new ITableView View { get; set; }
        protected new ITableModel Model { get; set; }

        public TablePresenter(ITableView View, ITableModel Model) : base(View, Model)
        {
            this.View = View;
            this.Model = Model;
        }

        #region Method definitions called by the view (downstream)

        public Boolean SaveTable(String Path)
        {
            if (Path == String.Empty)
                return false;

            return Model.SaveTable(Path);
        }

        public Boolean OpenTable(String Path)
        {
            if (Path == String.Empty)
                return false;

            return Model.OpenTable(Path);
        }

        public Boolean MergeTable(String Path)
        {
            if (Path == String.Empty)
                return false;

            return Model.MergeTable(Path);
        }

        #endregion

        #region Event definitions for events triggered by the model (upstream)

        #endregion

    } // End class

} // End namespace