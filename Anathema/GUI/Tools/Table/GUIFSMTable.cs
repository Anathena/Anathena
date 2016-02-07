using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Anathema.Properties;

namespace Anathema
{
    public partial class GUIFSMTable : UserControl, IFSMTableView
    {
        private FSMTablePresenter FSMTablePresenter;

        public GUIFSMTable()
        {
            InitializeComponent();

            FSMTablePresenter = new FSMTablePresenter(this, FSMTable.GetInstance());
        }

        public void UpdateFSMTableItemCount(Int32 ItemCount)
        {
            ControlThreadingHelper.InvokeControlAction(FSMTableListView, () =>
            {
                FSMTableListView.BeginUpdate();
                FSMTableListView.VirtualListSize = ItemCount;
                FSMTableListView.EndUpdate();
            });
        }

        #region Events

        private void FSMTableListView_RetrieveVirtualItem(Object Sender, RetrieveVirtualItemEventArgs E)
        {

        }

        #endregion

    } // End class

} // End namespace