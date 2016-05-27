using Anathema.Source.Scanners.ScanConstraints;
using Anathema.Source.Utils;
using Anathema.Source.Utils.Extensions;
using Anathema.Source.Utils.MVP;
using Anathema.Source.Utils.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Anathema.GUI
{
    public partial class GUIConstraintEditor : UserControl, IScanConstraintEditorView
    {
        private ScanConstraintEditorPresenter ScanConstraintEditorPresenter;
        private Object AccessLock;

        private Boolean ValueRequired;

        private Boolean _HideElementType;
        public Boolean HideElementType
        {
            get { return _HideElementType; }
            set
            {
                using (TimedLock.Lock(AccessLock))
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
                }
                Invalidate();
            }
        }

        public GUIConstraintEditor()
        {
            InitializeComponent();

            ScanConstraintEditorPresenter = new ScanConstraintEditorPresenter(this, new ScanConstraintEditor());
            AccessLock = new Object();

            InitializeValueTypeComboBox();
            UpdateScanOptions(EqualToToolStripMenuItem, ConstraintsEnum.Equal, false);
        }

        private void InitializeValueTypeComboBox()
        {
            foreach (Type Primitive in PrimitiveTypes.GetPrimitiveTypes())
                ValueTypeComboBox.Items.Add(Primitive.Name);

            ValueTypeComboBox.SelectedIndex = ValueTypeComboBox.Items.IndexOf(typeof(Int32).Name);
        }

        private void UpdateScanOptions(ToolStripMenuItem Sender, ConstraintsEnum ValueConstraint, Boolean AddNewConstraint = true)
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

            if (AddNewConstraint)
                AddConstraint();
        }

        public void UpdateDisplay(IEnumerable<ListViewItem> ListViewItems, ImageList ImageList)
        {
            ControlThreadingHelper.InvokeControlAction(ConstraintsListView, () =>
            {
                using (TimedLock.Lock(AccessLock))
                {
                    ConstraintsListView.Items.Clear();
                    ListViewItems?.ForEach(X => ConstraintsListView.Items.Add(X));
                    ConstraintsListView.SmallImageList = ImageList;
                }
            });
        }

        public void RemoveRelativeScans()
        {
            ControlThreadingHelper.InvokeControlAction(ScanOptionsToolStripDropDownButton.GetCurrentParent(), () =>
            {
                using (TimedLock.Lock(AccessLock))
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
            using (TimedLock.Lock(AccessLock))
            {
                if (!this.Controls.Contains(ConstraintToolStrip))
                    return null;

                ControlThreadingHelper.InvokeControlAction(this, () =>
                {
                    using (TimedLock.Lock(AccessLock))
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
            return ScanConstraintEditorPresenter.GetScanConstraintManager();
        }

        public void SetElementType(Type ElementType)
        {
            ControlThreadingHelper.InvokeControlAction(ValueTextBox, () =>
            {
                using (TimedLock.Lock(AccessLock))
                {
                    ScanConstraintEditorPresenter.SetElementType(ElementType);
                    ValueTextBox.SetElementType(ElementType);
                }
            });
        }

        private void AddConstraint()
        {
            ControlThreadingHelper.InvokeControlAction(ValueTextBox, () =>
            {
                using (TimedLock.Lock(AccessLock))
                {
                    if (!ValueRequired || ValueTextBox.IsValid())
                        ScanConstraintEditorPresenter.AddConstraint(ValueTextBox.GetValueAsDecimal());
                }
            });
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
            using (TimedLock.Lock(AccessLock))
            {
                ListViewHitTestInfo HitTest = ConstraintsListView.HitTest(E.Location);
                ListViewItem SelectedItem = HitTest.Item;

                if (SelectedItem == null)
                    return;

                SelectedItem.BeginEdit();
            }
        }

        private void ConstraintContextMenuStrip_Opening(Object Sender, CancelEventArgs E)
        {
            using (TimedLock.Lock(AccessLock))
            {
                ListViewHitTestInfo HitTest = ConstraintsListView.HitTest(RightClickLocation);
                ListViewItem SelectedItem = HitTest.Item;

                if (SelectedItem == null)
                    E.Cancel = true;
            }
        }

        private void EditConstraintToolStripMenuItem_Click(Object Sender, EventArgs E)
        {
            using (TimedLock.Lock(AccessLock))
            {
                ListViewHitTestInfo HitTest = ConstraintsListView.HitTest(RightClickLocation);
                ListViewItem SelectedItem = HitTest.Item;

                if (SelectedItem == null)
                    return;

                SelectedItem.BeginEdit();
            }
        }

        private void DeleteToolStripMenuItem_Click(Object Sender, EventArgs E)
        {
            using (TimedLock.Lock(AccessLock))
            {
                ListViewHitTestInfo HitTest = ConstraintsListView.HitTest(RightClickLocation);
                ListViewItem SelectedItem = HitTest.Item;

                if (SelectedItem == null)
                    return;

                ScanConstraintEditorPresenter.RemoveConstraints(SelectedItem.Index);
            }
        }

        private void ConstraintsListView_AfterLabelEdit(Object Sender, LabelEditEventArgs E)
        {
            using (TimedLock.Lock(AccessLock))
            {
                // Does not support hex, highlighting, nor GetValueAsDec like adding constraints. Just try and see if it fails.
                if (ScanConstraintEditorPresenter.TryUpdateConstraint(E.Item, E.Label))
                    return;

                // Could not update constraint, revert changes.
                E.CancelEdit = true;
            }
        }

        private void RemoveConstraintButton_Click(Object Sender, EventArgs E)
        {
            using (TimedLock.Lock(AccessLock))
            {
                ScanConstraintEditorPresenter.RemoveConstraints(ConstraintsListView.SelectedIndices.Cast<Int32>());
            }
        }

        private void ClearConstraintsButton_Click(Object Sender, EventArgs E)
        {
            using (TimedLock.Lock(AccessLock))
            {
                ScanConstraintEditorPresenter.ClearConstraints();
            }
        }

        private void ValueTypeComboBox_SelectedIndexChanged(Object Sender, EventArgs E)
        {
            using (TimedLock.Lock(AccessLock))
            {
                Type ElementType = Conversions.StringToPrimitiveType(ValueTypeComboBox.SelectedItem.ToString());
                SetElementType(ElementType);
            }
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