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
            TablePresenter = new TablePresenter(this, new Table());

            ViewCheatTable();
        }

        public void RefreshResults()
        {
            AddressTableListView.BeginUpdate();
            AddressTableListView.EndUpdate();

            ScriptTableListView.BeginUpdate();
            ScriptTableListView.EndUpdate();
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

        private void SaveTableButton_Click(Object Sender, EventArgs E)
        {

        }

        private void LoadTableButton_Click(Object Sender, EventArgs E)
        {

        }

        private void AddressTableListView_RetrieveVirtualItem(Object Sender, RetrieveVirtualItemEventArgs E)
        {

        }

        private void ScriptTableListView_RetrieveVirtualItem(Object Sender, RetrieveVirtualItemEventArgs E)
        {

        }

        private void CheatTableButton_Click(object sender, EventArgs e)
        {
            ViewCheatTable();
        }

        private void FSMTableButton_Click(object sender, EventArgs e)
        {
            ViewFSMTable();
        }
    }
}
