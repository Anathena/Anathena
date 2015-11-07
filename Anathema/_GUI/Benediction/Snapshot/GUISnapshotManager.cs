using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Binarysharp.MemoryManagement;

namespace Anathema
{
    public partial class GUISnapshotManager : UserControl, ISnapshotManagerView, IProcessObserver
    {
        private SnapshotManagerPresenter SnapshotManagerPresenter;

        public GUISnapshotManager()
        {
            InitializeComponent();

            SnapshotManagerPresenter = new SnapshotManagerPresenter(this, SnapshotManager.GetSnapshotManagerInstance());
        }

        public void UpdateProcess(MemorySharp MemoryEditor)
        {
            SnapshotManagerPresenter.UpdateProcess(MemoryEditor);
        }

        public void UpdateSnapshotDisplay()
        {

        }
    }
}
