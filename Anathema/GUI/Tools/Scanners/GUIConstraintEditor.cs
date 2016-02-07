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

namespace Anathema.GUI.Tools.MemoryScanners
{
    public partial class GUIConstraintEditor : UserControl, IScanConstraintEditorView
    {
        private ScanConstraintEditorPresenter ScanConstraintEditorPresenter;

        public GUIConstraintEditor()
        {
            InitializeComponent();

            ScanConstraintEditorPresenter = new ScanConstraintEditorPresenter(this, new ScanConstraintEditor());

            InitializeValueTypeComboBox();
            UpdateScanOptions(EqualToToolStripMenuItem, ConstraintsEnum.Equal);
        }

        public ToolStrip AcquireToolStrip()
        {
            if (this.Controls.Contains(ConstraintToolStrip))
            {
                this.Controls.Remove(ConstraintToolStrip);

                ValueTextBox.Location = new Point(ValueTextBox.Location.X, ValueTextBox.Location.Y - ConstraintToolStrip.Height);
                ValueTypeComboBox.Location = new Point(ValueTypeComboBox.Location.X, ValueTypeComboBox.Location.Y - ConstraintToolStrip.Height);
                ConstraintsListView.Location = new Point(ConstraintsListView.Location.X, ConstraintsListView.Location.Y - ConstraintToolStrip.Height);
                ConstraintsListView.Height += ConstraintToolStrip.Height;

                return ConstraintToolStrip;
            }
            return null;
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

            ScanConstraintEditorPresenter.SetCurrentValueConstraint(ValueConstraint);
        }

        private void InitializeValueTypeComboBox()
        {
            foreach (Type Primitive in PrimitiveTypes.GetPrimitiveTypes())
                ValueTypeComboBox.Items.Add(Primitive.Name);

            ValueTypeComboBox.SelectedIndex = ValueTypeComboBox.Items.IndexOf(typeof(Int32).Name);
        }

        public void UpdateDisplay(ListViewItem[] ListViewItems, ImageList ImageList)
        {
            ControlThreadingHelper.InvokeControlAction(ConstraintsListView, () =>
            {
                ConstraintsListView.Items.Clear();
                ConstraintsListView.Items.AddRange(ListViewItems);
                ConstraintsListView.SmallImageList = ImageList;
            });
        }

        #region Events


        private void AddConstraintButton_Click(Object Sender, EventArgs E)
        {
            if (!ValueTextBox.Enabled || ValueTextBox.IsValid())
                ScanConstraintEditorPresenter.AddConstraint(ValueTextBox.GetValueAsDecimal());
        }

        private void RemoveConstraintButton_Click(Object Sender, EventArgs E)
        {
            ScanConstraintEditorPresenter.RemoveConstraints(ConstraintsListView.SelectedIndices.Cast<Int32>().ToArray());
        }

        private void ClearConstraintsButton_Click(Object Sender, EventArgs E)
        {
            ScanConstraintEditorPresenter.ClearConstraints();
        }

        private void ValueTypeComboBox_SelectedIndexChanged(Object Sender, EventArgs E)
        {
            ScanConstraintEditorPresenter.SetElementType(ValueTypeComboBox.SelectedItem.ToString());
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