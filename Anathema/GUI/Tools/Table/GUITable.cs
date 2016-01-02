using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;
using System.IO;

namespace Anathema
{
    public partial class GUITable : DockContent, ITableView
    {
        private TablePresenter TablePresenter;

        public GUITable()
        {
            InitializeComponent();
            TablePresenter = new TablePresenter(this, Table.GetInstance());

            ViewCheatTable();
        }

        public void UpdateAddressTableItemCount(Int32 ItemCount)
        {
            ControlThreadingHelper.InvokeControlAction(AddressTableListView, () =>
            {
                AddressTableListView.VirtualListSize = ItemCount;
            });
        }

        public void UpdateScriptTableItemCount(Int32 ItemCount)
        {
            ControlThreadingHelper.InvokeControlAction(ScriptTableListView, () =>
            {
                ScriptTableListView.VirtualListSize = ItemCount;
            });
        }

        public void UpdateFSMTableItemCount(Int32 ItemCount)
        {
            ControlThreadingHelper.InvokeControlAction(FSMTableListView, () =>
            {
                FSMTableListView.VirtualListSize = ItemCount;
            });
        }

        public void RefreshDisplay()
        {
            ControlThreadingHelper.InvokeControlAction(AddressTableListView, () =>
            {
                AddressTableListView.BeginUpdate();
                AddressTableListView.EndUpdate();
            });
            ControlThreadingHelper.InvokeControlAction(CheatTableSplitContainer, () =>
            {
                ControlThreadingHelper.InvokeControlAction(ScriptTableListView, () =>
                {
                    ScriptTableListView.BeginUpdate();
                    ScriptTableListView.EndUpdate();
                });

                ControlThreadingHelper.InvokeControlAction(FSMTableListView, () =>
                {
                    FSMTableListView.BeginUpdate();
                    FSMTableListView.EndUpdate();
                });
            });
        }

        private void ViewCheatTable()
        {
            CheatTableButton.Checked = true;
            FSMTableButton.Checked = false;
            CheatTableSplitContainer.Visible = true;
            FSMTableListView.Visible = false;
        }

        private void ViewFSMTable()
        {
            CheatTableButton.Checked = false;
            FSMTableButton.Checked = true;
            CheatTableSplitContainer.Visible = false;
            FSMTableListView.Visible = true;
        }

        #region Events

        private void SaveTableButton_Click(Object Sender, EventArgs E)
        {

        }

        private void LoadTableButton_Click(Object Sender, EventArgs E)
        {

        }
        
        private void AddressTableListView_RetrieveVirtualItem(Object Sender, RetrieveVirtualItemEventArgs E)
        {
            E.Item = TablePresenter.GetAddressTableItemAt(E.ItemIndex);
        }

        private void ScriptTableListView_RetrieveVirtualItem(Object Sender, RetrieveVirtualItemEventArgs E)
        {
            E.Item = TablePresenter.GetScriptTableItemAt(E.ItemIndex);
        }

        private void FSMTableListView_RetrieveVirtualItem(Object Sender, RetrieveVirtualItemEventArgs E)
        {
            E.Item = TablePresenter.GetFSMTableItemAt(E.ItemIndex);
        }

        private void AddressTableListView_ItemChecked(Object Sender, ItemCheckedEventArgs E)
        {

        }

        private void AddressTableListView_MouseDoubleClick(Object Sender, MouseEventArgs E)
        {
            if (AddressTableListView.HitTest(E.Location).Item == null)
                return;

            GUIAddressTableEntryEdit Test = new GUIAddressTableEntryEdit(AddressTableListView.HitTest(E.Location).Item.Index);
            Test.ShowDialog();
        }

        private void CheatTableButton_Click(Object Sender, EventArgs E)
        {
            ViewCheatTable();
        }

        private void FSMTableButton_Click(Object Sender, EventArgs E)
        {
            ViewFSMTable();
        }

        #endregion
    }
}
