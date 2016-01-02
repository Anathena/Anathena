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
    delegate void TableAddressEntryEditorEventHandler(Object Sender, TableAddressEntryEditorEventArgs Args);
    class TableAddressEntryEditorEventArgs : EventArgs
    {

    }

    interface ITableAddressEntryEditorView : IView
    {
        // Methods invoked by the presenter (upstream)
    }

    interface ITableAddressEntryEditorModel : IModel
    {
        // Events triggered by the model (upstream)

        // Functions invoked by presenter (downstream)
    }

    class TableEntryEditorPresenter : Presenter<ITableAddressEntryEditorView, ITableAddressEntryEditorModel>
    {
        protected new ITableAddressEntryEditorView View { get; set; }
        protected new ITableAddressEntryEditorModel Model { get; set; }
        
        public TableEntryEditorPresenter(ITableAddressEntryEditorView View, ITableAddressEntryEditorModel Model) : base(View, Model)
        {
            this.View = View;
            this.Model = Model;
        }

        #region Method definitions called by the view (downstream)
        
        #endregion

        #region Event definitions for events triggered by the model (upstream)
        
        #endregion
    }
}