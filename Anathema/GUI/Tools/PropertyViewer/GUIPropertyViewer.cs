using Aga.Controls.Tree.NodeControls;
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

        public void RefreshStructure(IEnumerable<Object> SelectedObjects)
        {
            ControlThreadingHelper.InvokeControlAction<PropertyGrid>(PropertyGrid, () =>
            {
                PropertyGrid.SelectedObjects = SelectedObjects?.ToArray();
            });
        }

        void CheckIndex(Object Sender, NodeControlValueEventArgs E)
        {
            E.Value = true;
        }

    } // End class

} // End namespace