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

            PropertyViewerPresenter = new PropertyViewerPresenter(this, PropertyViewer.GetInstance());
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
                if (GetFocusedObjectType(this)?.Name == "GridViewEdit")
                    return;

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

    } // End class

} // End namespace