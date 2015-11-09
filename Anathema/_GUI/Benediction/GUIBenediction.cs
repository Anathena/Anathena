using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using Binarysharp.MemoryManagement;

namespace Anathema
{
    partial class GUIBenediction : UserControl, IBenedictionView, IProcessObserver
    {
        // Display constants
        private const Int32 MarginSize = 4;

        // Presenters for GUI components
        private BenedictionPresenter BenedictionPresenter;

        public GUIBenediction()
        {
            InitializeComponent();

            // Initialize presenter
            BenedictionPresenter = new BenedictionPresenter(this, Benediction.GetBenedictionInstance());
        }

        public void UpdateProcess(MemorySharp MemoryEditor)
        {
            BenedictionPresenter.UpdateProcess(MemoryEditor);
            GUIFilter.UpdateProcess(MemoryEditor);
            GUISnapshotManager.UpdateProcess(MemoryEditor);
        }

        /// <summary>
        /// Properly places and resizes necessary controls when the GUI is resized
        /// </summary>
        private void HandleGUIResize()
        {
            // Position the snapshot GUI
            GUISnapshotManager.Location = new Point(this.Width - GUISnapshotManager.Width - MarginSize, MarginSize);

            // Position the display GUI
            GUIDisplay.Location = new Point(GUISnapshotManager.Location.X - GUIDisplay.Width - MarginSize, MarginSize);
            
            // Position the table GUI
            GUITable.Location = new Point(GUIDisplay.Location.X, GUIDisplay.Location.Y + GUIDisplay.Height + MarginSize);
            GUITable.Width = this.Width - GUITable.Location.X - MarginSize;
            GUITable.Height = this.Height - GUITable.Location.Y - MarginSize;

            // Position Filter/Labeler GUI
            GUIFilterLabeler.Location = new Point(MarginSize, MarginSize);
            GUIFilterLabeler.Height = this.Height - GUIFilterLabeler.Location.Y - MarginSize;
            GUIFilterLabeler.Width = GUITable.Location.X - GUIFilterLabeler.Location.X - MarginSize;
        }

        private void GUIBenediction_Resize(object sender, EventArgs e)
        {
            HandleGUIResize();
        }

        private void GUIFilter_Load(object sender, EventArgs e)
        {

        }
    }
}
