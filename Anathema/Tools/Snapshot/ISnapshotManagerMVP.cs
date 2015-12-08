using Binarysharp.MemoryManagement;
using Binarysharp.MemoryManagement.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anathema
{
    interface ISnapshotManagerView : IView
    {
        // Methods invoked by the presenter (upstream)
        void UpdateSnapshotDisplay();
    }

    interface ISnapshotManagerModel : IModel, IProcessObserver
    {
        // Events triggered by the model (upstream)
        event EventHandler UpdateSnapshotDisplay;

        // Functions invoked by presenter (downstream)
        void DeleteSnapshot();
    }

    class SnapshotManagerPresenter : Presenter<ISnapshotManagerView, ISnapshotManagerModel>, IProcessObserver
    {
        public SnapshotManagerPresenter(ISnapshotManagerView View, ISnapshotManagerModel Model) : base(View, Model)
        {
            // Bind events triggered by the model
            Model.UpdateSnapshotDisplay += UpdateSnapshotDisplay;
        }

        #region Method definitions called by the view (downstream)

        public void UpdateProcess(MemorySharp MemoryEditor)
        {
            Model.UpdateProcess(MemoryEditor);
        }
        
        #endregion

        #region Event definitions for events triggered by the model (upstream)

        private void UpdateSnapshotDisplay(object sender, EventArgs e)
        {

        }

        #endregion
    }
}
