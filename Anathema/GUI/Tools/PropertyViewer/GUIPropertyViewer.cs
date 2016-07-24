using Anathema.Source.PropertyEditor;
using Anathema.Source.Utils.MVP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace Anathema.GUI
{
    public partial class GUIPropertyViewer : DockContent, IPropertyViewerView
    {
        private PropertyViewerPresenter PropertyViewerPresenter;
        private Object AccessLock;

        public GUIPropertyViewer()
        {
            InitializeComponent();

            AccessLock = new Object();

            MergeToolStrips();
            PropertyViewerPresenter = new PropertyViewerPresenter(this, PropertyViewer.GetInstance());
        }

        private void MergeToolStrips()
        {
            // Standard method of ToolStripManager.Merge does not seem to be working, so we take a manual approach
            Controls.Remove(GUIToolStrip);
            ToolStrip PropertyToolStrip = GetPropertyGridToolStrip();
            List<ToolStripItem> ToolStripItems = new List<ToolStripItem>();

            foreach (ToolStripItem ToolStripItem in GUIToolStrip.Items)
                ToolStripItems.Add(ToolStripItem);

            PropertyToolStrip.Items.AddRange(ToolStripItems.ToArray());
        }

        private ToolStrip GetPropertyGridToolStrip()
        {
            foreach (Control Control in PropertyGrid.Controls)
                if (Control is ToolStrip)
                    return Control as ToolStrip;

            return null;
        }

        public void SetTargetObjects(IEnumerable<Object> SelectedObjects)
        {
            ControlThreadingHelper.InvokeControlAction<PropertyGrid>(PropertyGrid, () =>
            {
                PropertyGrid.SelectedObjects = SelectedObjects?.ToArray();
            });
        }

        public void RefreshProperties()
        {
            ControlThreadingHelper.InvokeControlAction<PropertyGrid>(PropertyGrid, () =>
            {
                // if (GetFocusedObjectType(this)?.Name == "GridViewEdit")
                //    return;

                PropertyGrid.Refresh();
            });
        }

        private Type GetFocusedObjectType(Control Control)
        {
            ContainerControl Container = Control as ContainerControl;

            while (Container != null)
            {
                Control = Container.ActiveControl;
                Container = Control as ContainerControl;
            }

            return Control?.GetType();

        }

        #region Events

        private void RefreshButton_Click(Object Sender, EventArgs E)
        {
            RefreshProperties();
        }

        #endregion

    } // End class

} // End namespace