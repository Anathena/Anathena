using Anathena.Source.Scanners.ScanConstraints;
using Anathena.Source.Utils;
using Anathena.Source.Utils.Extensions;
using Anathena.Source.Utils.MVP;
using Anathena.Source.Utils.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Anathena.GUI.Tools.Scanners
{
    public partial class GUIConstraintEditor : UserControl, IScanConstraintEditorView
    {
        private ScanConstraintEditorPresenter scanConstraintEditorPresenter;
        private Object accessLock;

        private Boolean valueRequired;

        private Boolean _hideElementType;
        public Boolean HideElementType
        {
            get { return _hideElementType; }
            set
            {
                using (TimedLock.Lock(accessLock))
                {
                    _hideElementType = value;
                    if (_hideElementType)
                    {
                        ValueTypeComboBox.Visible = false;
                        ValueTextBox.Width = ValueTypeComboBox.Location.X + ValueTypeComboBox.Width - ValueTextBox.Location.X;
                    }
                    else
                    {
                        const Int32 spacing = 6;
                        ValueTypeComboBox.Visible = true;
                        ValueTextBox.Width = ValueTypeComboBox.Location.X - spacing - ValueTextBox.Location.X;
                    }
                }
                Invalidate();
            }
        }

        public GUIConstraintEditor()
        {
            InitializeComponent();

            scanConstraintEditorPresenter = new ScanConstraintEditorPresenter(this, new ScanConstraintEditor());
            accessLock = new Object();

            InitializeValueTypeComboBox();
            UpdateScanOptions(EqualToToolStripMenuItem, ConstraintsEnum.Equal, false);
        }

        private void InitializeValueTypeComboBox()
        {
            foreach (Type primitive in PrimitiveTypes.GetScannablePrimitiveTypes())
                ValueTypeComboBox.Items.Add(primitive.Name);

            ValueTypeComboBox.SelectedIndex = ValueTypeComboBox.Items.IndexOf(typeof(Int32).Name);
        }

        private void UpdateScanOptions(ToolStripMenuItem sender, ConstraintsEnum valueConstraint, Boolean addNewConstraint = true)
        {
            switch (valueConstraint)
            {
                case ConstraintsEnum.Changed:
                case ConstraintsEnum.Unchanged:
                case ConstraintsEnum.Decreased:
                case ConstraintsEnum.Increased:
                case ConstraintsEnum.NotScientificNotation:
                    valueRequired = false;
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
                    valueRequired = true;
                    break;
            }

            scanConstraintEditorPresenter.SetCurrentValueConstraint(valueConstraint);

            if (addNewConstraint)
                AddConstraint();
        }

        public void UpdateDisplay(IEnumerable<ListViewItem> listViewItems, ImageList imageList)
        {
            ControlThreadingHelper.InvokeControlAction(ConstraintsListView, () =>
            {
                using (TimedLock.Lock(accessLock))
                {
                    ConstraintsListView.Items.Clear();
                    listViewItems?.ForEach(X => ConstraintsListView.Items.Add(X));
                    ConstraintsListView.SmallImageList = imageList;
                }
            });
        }

        public void RemoveRelativeScans()
        {
            ControlThreadingHelper.InvokeControlAction(ConstraintToolStrip, () =>
            {
                using (TimedLock.Lock(accessLock))
                {
                    ScanOptionsToolStripDropDownButton.DropDownItems.Remove(ChangedToolStripMenuItem);
                    ScanOptionsToolStripDropDownButton.DropDownItems.Remove(UnchangedToolStripMenuItem);
                    ScanOptionsToolStripDropDownButton.DropDownItems.Remove(IncreasedToolStripMenuItem);
                    ScanOptionsToolStripDropDownButton.DropDownItems.Remove(DecreasedByToolStripMenuItem);
                    ScanOptionsToolStripDropDownButton.DropDownItems.Remove(IncreasedByToolStripMenuItem);
                    ScanOptionsToolStripDropDownButton.DropDownItems.Remove(DecreasedByToolStripMenuItem);
                }
            });
        }

        public ToolStrip AcquireToolStrip()
        {
            using (TimedLock.Lock(accessLock))
            {
                if (!this.Controls.Contains(ConstraintToolStrip))
                    return null;

                ControlThreadingHelper.InvokeControlAction(this, () =>
                {
                    using (TimedLock.Lock(accessLock))
                    {
                        this.Controls.Remove(ConstraintToolStrip);

                        ValueTextBox.Location = new Point(ValueTextBox.Location.X, ValueTextBox.Location.Y - ConstraintToolStrip.Height);
                        ValueTypeComboBox.Location = new Point(ValueTypeComboBox.Location.X, ValueTypeComboBox.Location.Y - ConstraintToolStrip.Height);
                        ConstraintsListView.Location = new Point(ConstraintsListView.Location.X, ConstraintsListView.Location.Y - ConstraintToolStrip.Height);
                        ConstraintsListView.Height += ConstraintToolStrip.Height;
                    }
                });

                return ConstraintToolStrip;
            }
        }

        public ScanConstraintManager GetScanConstraintManager()
        {
            return scanConstraintEditorPresenter.GetScanConstraintManager();
        }

        public void SetElementType(Type elementType)
        {
            ControlThreadingHelper.InvokeControlAction(ValueTextBox, () =>
            {
                using (TimedLock.Lock(accessLock))
                {
                    scanConstraintEditorPresenter.SetElementType(elementType);
                    ValueTextBox.SetElementType(elementType);
                }
            });
        }

        private void AddConstraint()
        {
            ControlThreadingHelper.InvokeControlAction(ValueTextBox, () =>
            {
                using (TimedLock.Lock(accessLock))
                {
                    if (!valueRequired || ValueTextBox.IsValid())
                        scanConstraintEditorPresenter.AddConstraint(ValueTextBox.GetValueAsDecimal());
                }
            });
        }

        #region Events

        private Point rightClickLocation = Point.Empty;

        private void ConstraintsListView_MouseClick(Object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Right)
                return;

            rightClickLocation = e.Location;
        }

        private void ConstraintsListView_MouseDoubleClick(Object sender, MouseEventArgs e)
        {
            using (TimedLock.Lock(accessLock))
            {
                ListViewHitTestInfo HitTest = ConstraintsListView.HitTest(e.Location);
                ListViewItem SelectedItem = HitTest.Item;

                if (SelectedItem == null)
                    return;

                SelectedItem.BeginEdit();
            }
        }

        private void ConstraintContextMenuStrip_Opening(Object sender, CancelEventArgs e)
        {
            using (TimedLock.Lock(accessLock))
            {
                ListViewHitTestInfo HitTest = ConstraintsListView.HitTest(rightClickLocation);
                ListViewItem SelectedItem = HitTest.Item;

                if (SelectedItem == null)
                    e.Cancel = true;
            }
        }

        private void EditConstraintToolStripMenuItem_Click(Object sender, EventArgs e)
        {
            using (TimedLock.Lock(accessLock))
            {
                ListViewHitTestInfo HitTest = ConstraintsListView.HitTest(rightClickLocation);
                ListViewItem SelectedItem = HitTest.Item;

                if (SelectedItem == null)
                    return;

                SelectedItem.BeginEdit();
            }
        }

        private void DeleteToolStripMenuItem_Click(Object sender, EventArgs e)
        {
            using (TimedLock.Lock(accessLock))
            {
                ListViewHitTestInfo HitTest = ConstraintsListView.HitTest(rightClickLocation);
                ListViewItem SelectedItem = HitTest.Item;

                if (SelectedItem == null)
                    return;

                scanConstraintEditorPresenter.RemoveConstraints(SelectedItem.Index);
            }
        }

        private void ConstraintsListView_AfterLabelEdit(Object sender, LabelEditEventArgs e)
        {
            using (TimedLock.Lock(accessLock))
            {
                // Does not support hex, highlighting, nor GetValueAsDec like adding constraints. Just try and see if it fails.
                if (scanConstraintEditorPresenter.TryUpdateConstraint(e.Item, e.Label))
                    return;

                // Could not update constraint, revert changes.
                e.CancelEdit = true;
            }
        }

        private void RemoveConstraintButton_Click(Object sender, EventArgs e)
        {
            using (TimedLock.Lock(accessLock))
            {
                scanConstraintEditorPresenter.RemoveConstraints(ConstraintsListView.SelectedIndices.Cast<Int32>());
            }
        }

        private void ClearConstraintsButton_Click(Object sender, EventArgs e)
        {
            using (TimedLock.Lock(accessLock))
            {
                scanConstraintEditorPresenter.ClearConstraints();
            }
        }

        private void ValueTypeComboBox_SelectedIndexChanged(Object sender, EventArgs e)
        {
            using (TimedLock.Lock(accessLock))
            {
                Type ElementType = Conversions.StringToPrimitiveType(ValueTypeComboBox.SelectedItem.ToString());
                SetElementType(ElementType);
            }
        }

        private void ChangedToolStripMenuItem_Click(Object sender, EventArgs e)
        {
            UpdateScanOptions((ToolStripMenuItem)sender, ConstraintsEnum.Changed);
        }

        private void UnchangedToolStripMenuItem_Click(Object sender, EventArgs e)
        {
            UpdateScanOptions((ToolStripMenuItem)sender, ConstraintsEnum.Unchanged);
        }

        private void IncreasedToolStripMenuItem_Click(Object sender, EventArgs e)
        {
            UpdateScanOptions((ToolStripMenuItem)sender, ConstraintsEnum.Increased);
        }

        private void DecreasedToolStripMenuItem_Click(Object sender, EventArgs e)
        {
            UpdateScanOptions((ToolStripMenuItem)sender, ConstraintsEnum.Decreased);
        }

        private void EqualToToolStripMenuItem_Click(Object sender, EventArgs e)
        {
            UpdateScanOptions((ToolStripMenuItem)sender, ConstraintsEnum.Equal);
        }

        private void NotEqualToToolStripMenuItem_Click(Object sender, EventArgs e)
        {
            UpdateScanOptions((ToolStripMenuItem)sender, ConstraintsEnum.NotEqual);
        }

        private void IncreasedByToolStripMenuItem_Click(Object sender, EventArgs e)
        {
            UpdateScanOptions((ToolStripMenuItem)sender, ConstraintsEnum.IncreasedByX);
        }

        private void DecreasedByToolStripMenuItem_Click(Object sender, EventArgs e)
        {
            UpdateScanOptions((ToolStripMenuItem)sender, ConstraintsEnum.DecreasedByX);
        }

        private void GreaterThanToolStripMenuItem_Click(Object sender, EventArgs e)
        {
            UpdateScanOptions((ToolStripMenuItem)sender, ConstraintsEnum.GreaterThan);
        }

        private void GreaterThanOrEqualToToolStripMenuItem_Click(Object sender, EventArgs e)
        {
            UpdateScanOptions((ToolStripMenuItem)sender, ConstraintsEnum.GreaterThanOrEqual);
        }

        private void LessThanToolStripMenuItem_Click(Object sender, EventArgs e)
        {
            UpdateScanOptions((ToolStripMenuItem)sender, ConstraintsEnum.LessThan);
        }

        private void LessThanOrEqualToToolStripMenuItem_Click(Object sender, EventArgs e)
        {
            UpdateScanOptions((ToolStripMenuItem)sender, ConstraintsEnum.LessThanOrEqual);
        }

        private void NotScientificNotationToolStripMenuItem_Click(Object sender, EventArgs e)
        {
            UpdateScanOptions((ToolStripMenuItem)sender, ConstraintsEnum.NotScientificNotation);
        }

        #endregion

    } // End class

} // End namespace