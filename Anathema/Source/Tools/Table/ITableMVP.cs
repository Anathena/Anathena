using Binarysharp.MemoryManagement;
using Binarysharp.MemoryManagement.Memory;
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
        Boolean LoadTable(String Path);
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

        public Boolean LoadTable(String Path)
        {
            if (Path == String.Empty)
                return false;

            return Model.LoadTable(Path);
        }
        
        #endregion

        #region Event definitions for events triggered by the model (upstream)
        
        #endregion

    } // End class

} // End namespace