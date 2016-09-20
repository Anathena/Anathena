using Ana.Source.PropertyView;
using Ana.Source.Utils.MVP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace Ana.GUI.Tools
{
    public partial class GUIPropertyViewer : DockContent, IPropertyViewerView
    {
        private PropertyViewerPresenter propertyViewerPresenter;
        private Object accessLock;

        public GUIPropertyViewer()
        {
            InitializeComponent();

            accessLock = new Object();

            MergeToolStrips();
            propertyViewerPresenter = new PropertyViewerPresenter(this, PropertyViewer.GetInstance());
        }

        private void MergeToolStrips()
        {
            // Standard method of ToolStripManager.Merge does not seem to be working, so we take a manual approach
            Controls.Remove(GUIToolStrip);
            ToolStrip propertyToolStrip = GetPropertyGridToolStrip();
            List<ToolStripItem> toolStripItems = new List<ToolStripItem>();

            foreach (ToolStripItem toolStripItem in GUIToolStrip.Items)
                toolStripItems.Add(toolStripItem);

            propertyToolStrip.Items.AddRange(toolStripItems.ToArray());
        }

        private ToolStrip GetPropertyGridToolStrip()
        {
            foreach (Control control in PropertyGrid.Controls)
                if (control is ToolStrip)
                    return control as ToolStrip;

            return null;
        }

        public void SetTargetObjects(IEnumerable<Object> selectedObjects)
        {
            ControlThreadingHelper.InvokeControlAction<PropertyGrid>(PropertyGrid, () =>
            {
                PropertyGrid.SelectedObjects = selectedObjects?.ToArray();
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

        private Type GetFocusedObjectType(Control control)
        {
            ContainerControl container = control as ContainerControl;

            while (container != null)
            {
                control = container.ActiveControl;
                container = control as ContainerControl;
            }

            return control?.GetType();

        }

        #region Events

        private void RefreshButton_Click(Object sender, EventArgs e)
        {
            RefreshProperties();
        }

        #endregion

    } // End class

} // End namespace