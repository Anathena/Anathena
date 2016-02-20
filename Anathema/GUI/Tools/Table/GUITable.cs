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

        private String ActiveTablePath;

        public GUITable()
        {
            InitializeComponent();
            TablePresenter = new TablePresenter(this, Table.GetInstance());

            ActiveTablePath = String.Empty;

            ViewCheatTable();
        }
        
        private void ViewCheatTable()
        {
            CheatTableButton.Checked = true;
            FSMTableButton.Checked = false;
            CheatTableSplitContainer.Visible = true;
            GUIFSMTable.Visible = false;
        }

        private void ViewFSMTable()
        {
            CheatTableButton.Checked = false;
            FSMTableButton.Checked = true;
            CheatTableSplitContainer.Visible = false;
            GUIFSMTable.Visible = true;
        }

        public void BeginSaveTable()
        {
            if (ActiveTablePath == null || ActiveTablePath == String.Empty)
            { 
                BeginSaveAsTable();
                return;
            }

            TablePresenter.SaveTable(ActiveTablePath);
        }

        public void BeginSaveAsTable()
        {
            SaveFileDialog SaveFileDialog = new SaveFileDialog();
            SaveFileDialog.Filter = "Anathema Table | *.ana";
            SaveFileDialog.Title = "Save Cheat Table";
            SaveFileDialog.ShowDialog();

            TablePresenter.SaveTable(SaveFileDialog.FileName);
        }

        public void BeginOpenTable()
        {
            OpenFileDialog OpenFileDialog = new OpenFileDialog();
            OpenFileDialog.Filter = "Anathema Table | *.ana";
            OpenFileDialog.Title = "Open Cheat Table";
            OpenFileDialog.ShowDialog();

            TablePresenter.OpenTable(OpenFileDialog.FileName);
        }

        public void BeginMergeTable()
        {
            OpenFileDialog OpenFileDialog = new OpenFileDialog();
            OpenFileDialog.Filter = "Anathema Table | *.ana";
            OpenFileDialog.Title = "Open and Merge Cheat Table";
            OpenFileDialog.ShowDialog();

            TablePresenter.MergeTable(OpenFileDialog.FileName);
        }

        private Boolean AskSaveChanges()
        {
            // Check if there are even changes to save
            if (!TablePresenter.HasChanges())
                return false;

            DialogResult Result = MessageBoxEx.Show(this, "This script has not been saved, save the changes to the table before closing?", "Save Changes?", MessageBoxButtons.YesNoCancel);

            switch (Result)
            {
                case DialogResult.Yes:
                    BeginSaveTable();
                    return false;
                case DialogResult.No:
                    return false;
                case DialogResult.Cancel:
                    break;
            }

            // User wishes to cancel
            return true;
        }

        #region Events

        private Point LastRightClickLocation = Point.Empty;

        private void SaveTableButton_Click(Object Sender, EventArgs E)
        {
            BeginSaveTable();
        }

        private void OpenTableButton_Click(Object Sender, EventArgs E)
        {
            BeginOpenTable();
        }

        private void OpenAndMergeTableButton_Click(Object Sender, EventArgs E)
        {
            BeginMergeTable();
        }

        private void CheatTableButton_Click(Object Sender, EventArgs E)
        {
            ViewCheatTable();
        }

        private void FSMTableButton_Click(Object Sender, EventArgs E)
        {
            ViewFSMTable();
        }

        private void AddAddressButton_Click(Object Sender, EventArgs E)
        {
            GUIAddressTable.AddNewAddressItem();
        }

        private void GUITable_FormClosing(Object Sender, FormClosingEventArgs E)
        {
            if (AskSaveChanges())
                E.Cancel = true;
        }

        #endregion

    } // End class

} // End namespace