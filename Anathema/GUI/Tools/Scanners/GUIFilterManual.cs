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
    public partial class GUIFilterManual : DockContent, IFilterManualScanView
    {
        private FilterManualScanPresenter FilterManualScanPresenter;

        private List<ToolStripButton> ScanOptionButtons;

        public GUIFilterManual()
        {
            InitializeComponent();

            ScanOptionButtons = new List<ToolStripButton>();

            InitializeValueTypeComboBox();
            InitializeScanOptionButtons();

            FilterManualScanPresenter = new FilterManualScanPresenter(this, new FilterManualScan());
        }

        private void InitializeValueTypeComboBox()
        {
            foreach (Type Primitive in PrimitiveTypes.GetPrimitiveTypes())
                ValueTypeComboBox.Items.Add(Primitive.Name);

            ValueTypeComboBox.SelectedIndex = ValueTypeComboBox.Items.IndexOf(typeof(Int32).Name);
        }

        private void InitializeScanOptionButtons()
        {
            ScanOptionButtons.Add(NegateButton);
            ScanOptionButtons.Add(ChangedButton);
            ScanOptionButtons.Add(IncreasedButton);
            ScanOptionButtons.Add(DecreasedButton);
            ScanOptionButtons.Add(EqualButton);
            ScanOptionButtons.Add(GreaterThanButton);
            ScanOptionButtons.Add(LessThanButton);
        }

        private void EvaluateScanOptions(ToolStripButton Sender)
        {
            ValueConstraintsEnum ValueConstraint = ValueConstraintsEnum.Invalid;

            if (IsCurrentScanSetting(EqualButton))
            {
                ValueConstraint = ValueConstraintsEnum.Equal;
            }
            else if (IsCurrentScanSetting(NegateButton, EqualButton))
            {
                ValueConstraint = ValueConstraintsEnum.NotEqual;
            }
            else if (IsCurrentScanSetting(ChangedButton))
            {
                ValueConstraint = ValueConstraintsEnum.Changed;
            }
            else if (IsCurrentScanSetting(NegateButton, ChangedButton))
            {
                ValueConstraint = ValueConstraintsEnum.Unchanged;
            }
            else if (IsCurrentScanSetting(IncreasedButton) || IsCurrentScanSetting(NegateButton, DecreasedButton))
            {
                ValueConstraint = ValueConstraintsEnum.Increased;
            }
            else if (IsCurrentScanSetting(DecreasedButton) || IsCurrentScanSetting(NegateButton, IncreasedButton))
            {
                ValueConstraint = ValueConstraintsEnum.Decreased;
            }
            else if (IsCurrentScanSetting(IncreasedButton, EqualButton))
            {
                ValueConstraint = ValueConstraintsEnum.IncreasedByX;
            }
            else if (IsCurrentScanSetting(DecreasedButton, EqualButton))
            {
                ValueConstraint = ValueConstraintsEnum.DecreasedByX;
            }
            else if (IsCurrentScanSetting(GreaterThanButton))
            {
                ValueConstraint = ValueConstraintsEnum.GreaterThanExclusive;
            }
            else if (IsCurrentScanSetting(LessThanButton))
            {
                ValueConstraint = ValueConstraintsEnum.LessThanExclusive;
            }
            else if (IsCurrentScanSetting(GreaterThanButton, EqualButton) || IsCurrentScanSetting(LessThanButton, NegateButton))
            {
                ValueConstraint = ValueConstraintsEnum.GreaterThanInclusive;
            }
            else if (IsCurrentScanSetting(LessThanButton, EqualButton) || IsCurrentScanSetting(GreaterThanButton, NegateButton))
            {
                ValueConstraint = ValueConstraintsEnum.LessThanInclusive;
            }
            else if (IsCurrentScanSetting(GreaterThanButton, LessThanButton))
            {
                ValueConstraint = ValueConstraintsEnum.BetweenExclusive;
            }
            else if (IsCurrentScanSetting(GreaterThanButton, LessThanButton, EqualButton))
            {
                ValueConstraint = ValueConstraintsEnum.BetweenInclusive;
            }
            else if (Sender != null)
            {
                if (Sender == null)
                    return;

                foreach (ToolStripButton Button in ScanOptionButtons)
                    if (Button != Sender)
                        Button.Checked = false;

                EvaluateScanOptions(null);
                return;
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

        private void NegateButton_Click(Object Sender, EventArgs E)
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

        #endregion
    }
}
