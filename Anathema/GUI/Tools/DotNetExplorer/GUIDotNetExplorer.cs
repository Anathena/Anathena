using Anathema.Source.Engine.DotNetObjectCollector;
using Anathema.Source.Utils.DotNetExplorer;
using System.Collections.Generic;
using WeifenLuo.WinFormsUI.Docking;

namespace Anathema.GUI
{
    public partial class GUIDotNetExplorer : DockContent, IDotNetExplorerView
    {
        private DotNetExplorerPresenter GDotNetExplorerPresenter;

        public GUIDotNetExplorer()
        {
            InitializeComponent();

            // Initialize presenter
            GDotNetExplorerPresenter = new DotNetExplorerPresenter(this, new DotNetExplorer());
        }

        public void UpdateVirtualPages(List<DotNetObject> ObjectTrees)
        {

        }

        #region Events


        #endregion

    } // End class

} // End namespace