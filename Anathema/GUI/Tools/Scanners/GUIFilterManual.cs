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
    public partial class GUIFilterManual : DockContent, IFilterManualScanView
    {
        private FilterManualScanPresenter FilterManualScanPresenter;

        private List<ToolStripButton> ScanOptionButtons;

        public GUIFilterManual()
        {
            InitializeComponent();

            ScanOptionButtons = new List<ToolStripButton>();
            FilterManualScanPresenter = new FilterManualScanPresenter(this, new FilterManualScan());

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
            ValueConstraintsEnum ValueConstraint = ValueConstraintsEnum.Invalid;

            foreach (ToolStripButton Button in ScanOptionButtons)
                if (Button != Sender)
                    Button.Checked = false;
                else
                    Button.Checked = true;

            if (Sender == EqualButton)
            {
                ValueConstraint = ValueConstraintsEnum.Equal;
            }
            else if (Sender == NotEqualButton)
            {
                ValueConstraint = ValueConstraintsEnum.NotEqual;
            }
            else if (Sender == ChangedButton)
            {
                ValueConstraint = ValueConstraintsEnum.Changed;
            }
            else if (Sender == UnchangedButton)
            {
                ValueConstraint = ValueConstraintsEnum.Unchanged;
            }
            else if (Sender == IncreasedButton)
            {
                ValueConstraint = ValueConstraintsEnum.Increased;
            }
            else if (Sender == DecreasedButton)
            {
                ValueConstraint = ValueConstraintsEnum.Decreased;
            }
            else if (Sender == GreaterThanButton)
            {
                ValueConstraint = ValueConstraintsEnum.GreaterThan;
            }
            else if (Sender == LessThanButton)
            {
                ValueConstraint = ValueConstraintsEnum.LessThan;
            }
            else if (Sender == IncreasedByXButton)
            {
                ValueConstraint = ValueConstraintsEnum.IncreasedByX;
            }
            else if (Sender == DecreasedByXButton)
            {
                ValueConstraint = ValueConstraintsEnum.DecreasedByX;
            }

            switch (ValueConstraint)
            {
                case ValueConstraintsEnum.Changed:
                case ValueConstraintsEnum.Unchanged:
                case ValueConstraintsEnum.Decreased:
                case ValueConstraintsEnum.Increased:
                    ValueTextBox.Enabled = false;
                    ValueTextBox.Text = "";
                    break;
                case ValueConstraintsEnum.Invalid:
                case ValueConstraintsEnum.GreaterThan:
                case ValueConstraintsEnum.LessThan:
                case ValueConstraintsEnum.Equal:
                case ValueConstraintsEnum.NotEqual:
                case ValueConstraintsEnum.IncreasedByX:
                case ValueConstraintsEnum.DecreasedByX:
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

            FilterManualScanPresenter.SetValueConstraints(ValueConstraint);
            this.CompareTypeLabel.Text = ValueConstraint.ToString();
        }

        private Boolean IsCurrentScanSetting(params ToolStripButton[] Controls)
        {
            List<ToolStripButton> CandidateControls = new List<ToolStripButton>(Controls);

            // Controls in consideration must be checked
            foreach (ToolStripButton Button in CandidateControls)
                if (!Button.Checked)
                    return false;

            // Controls not in consideration must not be checked.
            foreach (ToolStripButton Button in ScanOptionButtons)
            {
                if (CandidateControls.Contains(Button))
                    continue;

                if (Button.Checked)
                    return false;
            }

            return true;
        }

        #region Events

        private void StartScanButton_Click(Object Sender, EventArgs E)
        {
            FilterManualScanPresenter.BeginScan();
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
            FilterManualScanPresenter.AddConstraint(ValueTextBox.Text.ToString());
        }

        private void ClearConstraintsButton_Click(Object Sender, EventArgs E)
        {
            FilterManualScanPresenter.ClearConstraints();
        }

        private void RemoveConstraintButton_Click(Object Sender, EventArgs E)
        {
            FilterManualScanPresenter.RemoveConstraints(ConstraintsListView.SelectedIndices.Cast<Int32>().ToArray());
        }

        private void ValueTypeComboBox_SelectedIndexChanged(Object Sender, EventArgs E)
        {
            FilterManualScanPresenter.SetElementType(ValueTypeComboBox.SelectedItem.ToString());
        }

        #endregion
    }
}
