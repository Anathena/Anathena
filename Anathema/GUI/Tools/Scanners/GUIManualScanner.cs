using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;
using System.IO;
using System.Linq;

namespace Anathema
{
    public partial class GUIManualScanner : DockContent, IManualScannerView
    {
        private ManualScannerPresenter ManualScannerPresenter;

        public GUIManualScanner()
        {
            InitializeComponent();

            ManualScannerPresenter = new ManualScannerPresenter(this, new ManualScanner());

            InitializeValueTypeComboBox();
            UpdateScanOptions(ChangedToolStripMenuItem, ConstraintsEnum.Equal);

            EnableGUI();
        }

        public void DisplayScanCount(Int32 ScanCount) { /* Manual scan will always have 1 scan so we need not implement this */ }

        private void InitializeValueTypeComboBox()
        {
            foreach (Type Primitive in PrimitiveTypes.GetPrimitiveTypes())
                ValueTypeComboBox.Items.Add(Primitive.Name);

            ValueTypeComboBox.SelectedIndex = ValueTypeComboBox.Items.IndexOf(typeof(Int32).Name);
        }

        public void UpdateDisplay(String[] ScanConstraintItems, ImageList Images)
        {
            ControlThreadingHelper.InvokeControlAction(ConstraintsListView, () =>
            {
                ConstraintsListView.Items.Clear();
                foreach (String Item in ScanConstraintItems)
                    ConstraintsListView.Items.Add(new ListViewItem(Item));
            });
        }

        public void ScanFinished()
        {
            EnableGUI();
        }

        private void UpdateScanOptions(ToolStripMenuItem Sender, ConstraintsEnum ValueConstraint)
        {
            ScanOptionsToolStripDropDownButton.Image = Sender.Image;

            switch (ValueConstraint)
            {
                case ConstraintsEnum.Changed:
                case ConstraintsEnum.Unchanged:
                case ConstraintsEnum.Decreased:
                case ConstraintsEnum.Increased:
                case ConstraintsEnum.NotScientificNotation:
                    ValueTextBox.Enabled = false;
                    ValueTextBox.Text = "";
                    break;
                case ConstraintsEnum.Invalid:
                case ConstraintsEnum.GreaterThan:
                case ConstraintsEnum.GreaterThanOrEqual:
                case ConstraintsEnum.LessThan:
                case ConstraintsEnum.LessThanOrEqual:
                case ConstraintsEnum.Equal:
                case ConstraintsEnum.NotEqual:
                case ConstraintsEnum.IncreasedByX:
                case ConstraintsEnum.DecreasedByX:
                    ValueTextBox.Enabled = true;
                    break;
            }

            ManualScannerPresenter.SetValueConstraints(ValueConstraint);
        }

        private void EnableGUI()
        {
            ControlThreadingHelper.InvokeControlAction(ScanToolStrip, () =>
            {
                ScanToolStrip.Items[ScanToolStrip.Items.IndexOf(StartScanButton)].Enabled = true;
            });
        }

        private void DisableGUI()
        {
            ControlThreadingHelper.InvokeControlAction(ScanToolStrip, () =>
            {
                ScanToolStrip.Items[ScanToolStrip.Items.IndexOf(StartScanButton)].Enabled = false;
            });
        }

        #region Events

        private void StartScanButton_Click(Object Sender, EventArgs E)
        {
            DisableGUI();
            ManualScannerPresenter.BeginScan();
        }

        private void AddConstraintButton_Click(Object Sender, EventArgs E)
        {
            if (ValueTextBox.IsValid())
                ManualScannerPresenter.AddConstraint(ValueTextBox.GetValueAsDecimal());
        }

        private void ClearConstraintsButton_Click(Object Sender, EventArgs E)
        {
            ManualScannerPresenter.ClearConstraints();
        }

        private void RemoveConstraintButton_Click(Object Sender, EventArgs E)
        {
            ManualScannerPresenter.RemoveConstraints(ConstraintsListView.SelectedIndices.Cast<Int32>().ToArray());
        }

        private void ValueTypeComboBox_SelectedIndexChanged(Object Sender, EventArgs E)
        {
            ManualScannerPresenter.SetElementType(ValueTypeComboBox.SelectedItem.ToString());
            ValueTextBox.SetElementType(Conversions.StringToPrimitiveType(ValueTypeComboBox.SelectedItem.ToString()));
        }

        private void ChangedToolStripMenuItem_Click(Object Sender, EventArgs E)
        {
            UpdateScanOptions((ToolStripMenuItem)Sender, ConstraintsEnum.Changed);
        }

        private void UnchangedToolStripMenuItem_Click(Object Sender, EventArgs E)
        {
            UpdateScanOptions((ToolStripMenuItem)Sender, ConstraintsEnum.Unchanged);
        }

        private void IncreasedToolStripMenuItem_Click(Object Sender, EventArgs E)
        {
            UpdateScanOptions((ToolStripMenuItem)Sender, ConstraintsEnum.Increased);
        }

        private void DecreasedToolStripMenuItem_Click(Object Sender, EventArgs E)
        {
            UpdateScanOptions((ToolStripMenuItem)Sender, ConstraintsEnum.Decreased);
        }

        private void EqualToToolStripMenuItem_Click(Object Sender, EventArgs E)
        {
            UpdateScanOptions((ToolStripMenuItem)Sender, ConstraintsEnum.Equal);
        }

        private void NotEqualToToolStripMenuItem_Click(Object Sender, EventArgs E)
        {
            UpdateScanOptions((ToolStripMenuItem)Sender, ConstraintsEnum.NotEqual);
        }

        private void IncreasedByToolStripMenuItem_Click(Object Sender, EventArgs E)
        {
            UpdateScanOptions((ToolStripMenuItem)Sender, ConstraintsEnum.IncreasedByX);
        }

        private void DecreasedByToolStripMenuItem_Click(Object Sender, EventArgs E)
        {
            UpdateScanOptions((ToolStripMenuItem)Sender, ConstraintsEnum.DecreasedByX);
        }

        private void GreaterThanToolStripMenuItem_Click(Object Sender, EventArgs E)
        {
            UpdateScanOptions((ToolStripMenuItem)Sender, ConstraintsEnum.GreaterThan);
        }

        private void GreaterThanOrEqualToToolStripMenuItem_Click(Object Sender, EventArgs E)
        {
            UpdateScanOptions((ToolStripMenuItem)Sender, ConstraintsEnum.GreaterThanOrEqual);
        }

        private void LessThanToolStripMenuItem_Click(Object Sender, EventArgs E)
        {
            UpdateScanOptions((ToolStripMenuItem)Sender, ConstraintsEnum.LessThan);
        }

        private void LessThanOrEqualToToolStripMenuItem_Click(Object Sender, EventArgs E)
        {
            UpdateScanOptions((ToolStripMenuItem)Sender, ConstraintsEnum.LessThanOrEqual);
        }

        private void NotScientificNotationToolStripMenuItem_Click(Object Sender, EventArgs E)
        {
            UpdateScanOptions((ToolStripMenuItem)Sender, ConstraintsEnum.NotScientificNotation);
        }

        #endregion

    } // End class

} // End namespace