using Binarysharp.MemoryManagement;
using Binarysharp.MemoryManagement.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Anathema
{
    delegate void SnapshotManagerEventHandler(Object Sender, SnapshotManagerEventArgs Args);
    class SnapshotManagerEventArgs : EventArgs
    {
        public List<Snapshot> SnapshotList = null;
    }

    interface ISnapshotManagerView : IView
    {
        // Methods invoked by the presenter (upstream)
        void UpdateSnapshotDisplay(TreeNode[] Snapshots);
    }

    interface ISnapshotManagerModel : IModel, IProcessObserver
    {
        // Events triggered by the model (upstream)
        event SnapshotManagerEventHandler UpdateSnapshotDisplay;

        // Functions invoked by presenter (downstream)
        void DeleteSnapshot();
    }

    class SnapshotManagerPresenter : Presenter<ISnapshotManagerView, ISnapshotManagerModel>
    {
        public SnapshotManagerPresenter(ISnapshotManagerView View, ISnapshotManagerModel Model) : base(View, Model)
        {
            // Bind events triggered by the model
            Model.UpdateSnapshotDisplay += UpdateSnapshotDisplay;
        }
        
        #region Method definitions called by the view (downstream)

        public void UpdateMemoryEditor(MemorySharp MemoryEditor)
        {
            Model.UpdateMemoryEditor(MemoryEditor);
        }
        
        #endregion

        #region Event definitions for events triggered by the model (upstream)

        private void UpdateSnapshotDisplay(Object Sender, SnapshotManagerEventArgs E)
        {
            List<TreeNode> TreeNodes = new List<TreeNode>();

            for (Int32 Index = 0; Index < E.SnapshotList.Count; Index++)
                TreeNodes.Add(new TreeNode(E.SnapshotList[Index].GetTimeStamp().ToLongTimeString()));

            View.UpdateSnapshotDisplay(TreeNodes.ToArray());
        }

        #endregion
    }
}
