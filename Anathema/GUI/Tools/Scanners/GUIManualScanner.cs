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

        private List<ToolStripButton> ScanOptionButtons;

        public GUIManualScanner()
        {
            InitializeComponent();

            ScanOptionButtons = new List<ToolStripButton>();
            ManualScannerPresenter = new ManualScannerPresenter(this, new ManualScanner());

            InitializeValueTypeComboBox();
            InitializeScanOptionButtons();
            EvaluateScanOptions(EqualButton);
        }

        private void InitializeValueTypeComboBox()
        {
            foreach (Type Primitive in PrimitiveTypes.GetPrimitiveTypes())
                ValueTypeComboBox.Items.Add(Primitive.Name);

            ValueTypeComboBox.SelectedIndex = ValueTypeComboBox.Items.IndexOf(typeof(Int32).Name);
        }

        private void InitializeScanOptionButtons()
        {
            ScanOptionButtons.Add(UnchangedButton);
            ScanOptionButtons.Add(ChangedButton);
            ScanOptionButtons.Add(IncreasedButton);
            ScanOptionButtons.Add(DecreasedButton);
            ScanOptionButtons.Add(NotEqualButton);
            ScanOptionButtons.Add(EqualButton);
            ScanOptionButtons.Add(GreaterThanButton);
            ScanOptionButtons.Add(LessThanButton);
            ScanOptionButtons.Add(IncreasedByXButton);
            ScanOptionButtons.Add(DecreasedByXButton);
        }

        public void UpdateDisplay(List<String[]> ScanConstraintItems)
        {
            ConstraintsListView.Items.Clear();
            foreach (String[] Item in ScanConstraintItems)
                ConstraintsListView.Items.Add(new ListViewItem(Item));
        }

        private void EvaluateScanOptions(ToolStripButton Sender)
        {
            ConstraintsEnum ValueConstraint = ConstraintsEnum.Invalid;

            foreach (ToolStripButton Button in ScanOptionButtons)
                if (Button != Sender)
                    Button.Checked = false;
                else
                    Button.Checked = true;

            if (Sender == EqualButton)
                ValueConstraint = ConstraintsEnum.Equal;
            else if (Sender == NotEqualButton)
                ValueConstraint = ConstraintsEnum.NotEqual;
            else if (Sender == ChangedButton)
                ValueConstraint = ConstraintsEnum.Changed;
            else if (Sender == UnchangedButton)
                ValueConstraint = ConstraintsEnum.Unchanged;
            else if (Sender == IncreasedButton)
                ValueConstraint = ConstraintsEnum.Increased;
            else if (Sender == DecreasedButton)
                ValueConstraint = ConstraintsEnum.Decreased;
            else if (Sender == GreaterThanButton)
                ValueConstraint = ConstraintsEnum.GreaterThan;
            else if (Sender == LessThanButton)
                ValueConstraint = ConstraintsEnum.LessThan;
            else if (Sender == IncreasedByXButton)
                ValueConstraint = ConstraintsEnum.IncreasedByX;
            else if (Sender == DecreasedByXButton)
                ValueConstraint = ConstraintsEnum.DecreasedByX;

            switch (ValueConstraint)
            {
                case ConstraintsEnum.Changed:
                case ConstraintsEnum.Unchanged:
                case ConstraintsEnum.Decreased:
                case ConstraintsEnum.Increased:
                    ValueTextBox.Enabled = false;
                    ValueTextBox.Text = "";
                    break;
                case ConstraintsEnum.Invalid:
                case ConstraintsEnum.GreaterThan:
                case ConstraintsEnum.LessThan:
                case ConstraintsEnum.Equal:
                case ConstraintsEnum.NotEqual:
                case ConstraintsEnum.IncreasedByX:
                case ConstraintsEnum.DecreasedByX:
                    ValueTextBox.Enabled = true;
                    break;
            }

            if (Conversions.StringToPrimitiveType(ValueTypeComboBox.SelectedItem.ToString()) == typeof(Single) ||
                Conversions.StringToPrimitiveType(ValueTypeComboBox.SelectedItem.ToString()) == typeof(Double))
            {
                FilterScientificNotationCheckBox.Visible = true;
            }
            else
            {
                FilterScientificNotationCheckBox.Visible = false;
            }

            ManualScannerPresenter.SetValueConstraints(ValueConstraint);
            this.CompareTypeLabel.Text = ValueConstraint.ToString();
        }

        #region Events

        private void StartScanButton_Click(Object Sender, EventArgs E)
        {
            ManualScannerPresenter.BeginScan();
        }

        private void ChangedButton_Click(Object Sender, EventArgs E)
        {
            EvaluateScanOptions((ToolStripButton)Sender);
        }

        private void NotEqualButton_Click(Object Sender, EventArgs E)
        {
            EvaluateScanOptions((ToolStripButton)Sender);
        }

        private void UnchangedButton_Click(Object Sender, EventArgs E)
        {
            EvaluateScanOptions((ToolStripButton)Sender);
        }

        private void IncreasedButton_Click(Object Sender, EventArgs E)
        {
            EvaluateScanOptions((ToolStripButton)Sender);
        }

        private void DecreasedButton_Click(Object Sender, EventArgs E)
        {
            EvaluateScanOptions((ToolStripButton)Sender);
        }

        private void EqualButton_Click(Object Sender, EventArgs E)
        {
            EvaluateScanOptions((ToolStripButton)Sender);
        }

        private void GreaterThanButton_Click(Object Sender, EventArgs E)
        {
            EvaluateScanOptions((ToolStripButton)Sender);
        }

        private void LessThanButton_Click(Object Sender, EventArgs E)
        {
            EvaluateScanOptions((ToolStripButton)Sender);
        }

        private void IncreasedByXButton_Click(Object Sender, EventArgs E)
        {
            EvaluateScanOptions((ToolStripButton)Sender);
        }

        private void DecreasedByXButton_Click(Object Sender, EventArgs E)
        {
            EvaluateScanOptions((ToolStripButton)Sender);
        }

        private void AddConstraintButton_Click(Object Sender, EventArgs E)
        {
            ManualScannerPresenter.AddConstraint(ValueTextBox.Text.ToString());
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
        }

        #endregion
    }
}
