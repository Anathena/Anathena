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

        private Boolean ValueRequired;

        private Boolean _HideElementType;
        public Boolean HideElementType
        {
            get { return _HideElementType; }
            set
            {
                _HideElementType = value;
                if (_HideElementType)
                {
                    ValueTypeComboBox.Visible = false;
                    ValueTextBox.Width = ValueTypeComboBox.Location.X + ValueTypeComboBox.Width - ValueTextBox.Location.X;
                }
                else
                {
                    const Int32 Spacing = 6;
                    ValueTypeComboBox.Visible = true;
                    ValueTextBox.Width = ValueTypeComboBox.Location.X - Spacing - ValueTextBox.Location.X;
                }
                Invalidate();
            }
        }

        public GUIConstraintEditor()
        {
            InitializeComponent();

            ScanConstraintEditorPresenter = new ScanConstraintEditorPresenter(this, new ScanConstraintEditor());

            InitializeValueTypeComboBox();
            UpdateScanOptions(EqualToToolStripMenuItem, ConstraintsEnum.Equal);
        }

        public void RemoveRelativeScans()
        {
            ScanOptionsToolStripDropDownButton.DropDownItems.Remove(ChangedToolStripMenuItem);
            ScanOptionsToolStripDropDownButton.DropDownItems.Remove(UnchangedToolStripMenuItem);
            ScanOptionsToolStripDropDownButton.DropDownItems.Remove(IncreasedToolStripMenuItem);
            ScanOptionsToolStripDropDownButton.DropDownItems.Remove(DecreasedByToolStripMenuItem);
            ScanOptionsToolStripDropDownButton.DropDownItems.Remove(IncreasedByToolStripMenuItem);
            ScanOptionsToolStripDropDownButton.DropDownItems.Remove(DecreasedByToolStripMenuItem);
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

        public ScanConstraintManager GetScanConstraintManager()
        {
            return ScanConstraintEditorPresenter.GetScanConstraintManager();
        }

        public void SetElementType(Type ElementType)
        {
            ScanConstraintEditorPresenter.SetElementType(ElementType);
            ValueTextBox.SetElementType(ElementType);
        }

        private void UpdateScanOptions(ToolStripMenuItem Sender, ConstraintsEnum ValueConstraint)
        {
            switch (ValueConstraint)
            {
                case ConstraintsEnum.Changed:
                case ConstraintsEnum.Unchanged:
                case ConstraintsEnum.Decreased:
                case ConstraintsEnum.Increased:
                case ConstraintsEnum.NotScientificNotation:
                    ValueRequired = false;
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
                    ValueRequired = true;
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

        private void AddConstraint()
        {
            if (!ValueRequired || ValueTextBox.IsValid())
                ScanConstraintEditorPresenter.AddConstraint(ValueTextBox.GetValueAsDecimal());
        }

        #region Events

        private Point RightClickLocation = Point.Empty;

        private void ConstraintsListView_MouseClick(Object Sender, MouseEventArgs E)
        {
            if (E.Button != MouseButtons.Right)
                return;

            RightClickLocation = E.Location;
        }

        private void ConstraintsListView_MouseDoubleClick(Object Sender, MouseEventArgs E)
        {
            ListViewHitTestInfo HitTest = ConstraintsListView.HitTest(E.Location);
            ListViewItem SelectedItem = HitTest.Item;

            if (SelectedItem == null)
                return;

            SelectedItem.BeginEdit();
        }

        private void ConstraintContextMenuStrip_Opening(Object Sender, CancelEventArgs E)
        {
            ListViewHitTestInfo HitTest = ConstraintsListView.HitTest(RightClickLocation);
            ListViewItem SelectedItem = HitTest.Item;

            if (SelectedItem == null)
                E.Cancel = true;
        }

        private void EditConstraintToolStripMenuItem_Click(Object Sender, EventArgs E)
        {
            ListViewHitTestInfo HitTest = ConstraintsListView.HitTest(RightClickLocation);
            ListViewItem SelectedItem = HitTest.Item;

            if (SelectedItem == null)
                return;

            SelectedItem.BeginEdit();
        }

        private void DeleteToolStripMenuItem_Click(Object Sender, EventArgs E)
        {
            ListViewHitTestInfo HitTest = ConstraintsListView.HitTest(RightClickLocation);
            ListViewItem SelectedItem = HitTest.Item;

            if (SelectedItem == null)
                return;

            ScanConstraintEditorPresenter.RemoveConstraints(SelectedItem.Index);
        }

        private void ConstraintsListView_AfterLabelEdit(Object Sender, LabelEditEventArgs E)
        {
            // Does not support hex, highlighting, nor GetValueAsDec like adding constraints. Just try and see if it fails.
            if (ScanConstraintEditorPresenter.TryUpdateConstraint(E.Item, E.Label))
                return;

            // Could not update constraint, revert changes.
            E.CancelEdit = true;
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
            Type ElementType = Conversions.StringToPrimitiveType(ValueTypeComboBox.SelectedItem.ToString());
            SetElementType(ElementType);
        }

        private void ChangedToolStripMenuItem_Click(Object Sender, EventArgs E)
        {
            UpdateScanOptions((ToolStripMenuItem)Sender, ConstraintsEnum.Changed);
            AddConstraint();
        }

        private void UnchangedToolStripMenuItem_Click(Object Sender, EventArgs E)
        {
            UpdateScanOptions((ToolStripMenuItem)Sender, ConstraintsEnum.Unchanged);
            AddConstraint();
        }

        private void IncreasedToolStripMenuItem_Click(Object Sender, EventArgs E)
        {
            UpdateScanOptions((ToolStripMenuItem)Sender, ConstraintsEnum.Increased);
            AddConstraint();
        }

        private void DecreasedToolStripMenuItem_Click(Object Sender, EventArgs E)
        {
            UpdateScanOptions((ToolStripMenuItem)Sender, ConstraintsEnum.Decreased);
            AddConstraint();
        }

        private void EqualToToolStripMenuItem_Click(Object Sender, EventArgs E)
        {
            UpdateScanOptions((ToolStripMenuItem)Sender, ConstraintsEnum.Equal);
            AddConstraint();
        }

        private void NotEqualToToolStripMenuItem_Click(Object Sender, EventArgs E)
        {
            UpdateScanOptions((ToolStripMenuItem)Sender, ConstraintsEnum.NotEqual);
            AddConstraint();
        }

        private void IncreasedByToolStripMenuItem_Click(Object Sender, EventArgs E)
        {
            UpdateScanOptions((ToolStripMenuItem)Sender, ConstraintsEnum.IncreasedByX);
            AddConstraint();
        }

        private void DecreasedByToolStripMenuItem_Click(Object Sender, EventArgs E)
        {
            UpdateScanOptions((ToolStripMenuItem)Sender, ConstraintsEnum.DecreasedByX);
            AddConstraint();
        }

        private void GreaterThanToolStripMenuItem_Click(Object Sender, EventArgs E)
        {
            UpdateScanOptions((ToolStripMenuItem)Sender, ConstraintsEnum.GreaterThan);
            AddConstraint();
        }

        private void GreaterThanOrEqualToToolStripMenuItem_Click(Object Sender, EventArgs E)
        {
            UpdateScanOptions((ToolStripMenuItem)Sender, ConstraintsEnum.GreaterThanOrEqual);
            AddConstraint();
        }

        private void LessThanToolStripMenuItem_Click(Object Sender, EventArgs E)
        {
            UpdateScanOptions((ToolStripMenuItem)Sender, ConstraintsEnum.LessThan);
            AddConstraint();
        }

        private void LessThanOrEqualToToolStripMenuItem_Click(Object Sender, EventArgs E)
        {
            UpdateScanOptions((ToolStripMenuItem)Sender, ConstraintsEnum.LessThanOrEqual);
            AddConstraint();
        }

        private void NotScientificNotationToolStripMenuItem_Click(Object Sender, EventArgs E)
        {
            UpdateScanOptions((ToolStripMenuItem)Sender, ConstraintsEnum.NotScientificNotation);
            AddConstraint();
        }

        #endregion

    } // End class

} // End namespace