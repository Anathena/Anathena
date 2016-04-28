using System;
using System.Drawing;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;
using Anathema.Utils;
using Anathema.Utils.MVP;
using Anathema.User.UserTable;

namespace Anathema
{
    public partial class GUITable : DockContent, ITableView
    {
        private TablePresenter TablePresenter;

        private String Title;
        private String ActiveTablePath;

        public GUITable()
        {
            InitializeComponent();
            Title = this.Text;

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

        public void UpdateHasChanges(Boolean HasChanges)
        {
            ControlThreadingHelper.InvokeControlAction(this, () =>
            {
                this.Text = Title + " - " + ActiveTablePath;
                if (HasChanges)
                    this.Text += "*";
            });
        }

        public void BeginSaveTable()
        {
            if (ActiveTablePath == String.Empty)
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

            ActiveTablePath = SaveFileDialog.FileName;

            TablePresenter.SaveTable(SaveFileDialog.FileName);
        }

        public void BeginOpenTable()
        {
            OpenFileDialog OpenFileDialog = new OpenFileDialog();
            OpenFileDialog.Filter = "Anathema Table | *.ana";
            OpenFileDialog.Title = "Open Cheat Table";
            OpenFileDialog.ShowDialog();

            ActiveTablePath = OpenFileDialog.FileName;

            TablePresenter.OpenTable(OpenFileDialog.FileName);
        }

        public void BeginMergeTable()
        {
            OpenFileDialog OpenFileDialog = new OpenFileDialog();
            OpenFileDialog.Filter = "Anathema Table | *.ana";
            OpenFileDialog.Title = "Open and Merge Cheat Table";
            OpenFileDialog.ShowDialog();

            // Prioritize whatever is open already. If nothing, use the merge filename.
            if (ActiveTablePath == String.Empty)
                ActiveTablePath = OpenFileDialog.FileName;

            TablePresenter.MergeTable(OpenFileDialog.FileName);
        }

        private Boolean AskSaveChanges()
        {
            // Check if there are even changes to save
            if (!TablePresenter.HasChanges())
                return false;

            DialogResult Result = MessageBoxEx.Show(this, "This table has not been saved. Save the changes before closing?", "Save Changes?", MessageBoxButtons.YesNoCancel);

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