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
        }

        /// <summary>
        /// Properly places and resizes necessary controls when the GUI is resized
        /// </summary>
        private void HandleGUIResize()
        {
            // Position the snapshot GUI
            GUISnapshotManager.Location = new Point(MarginSize, MarginSize);
            GUISnapshotManager.Height = GUITable.Location.Y - GUISnapshotManager.Location.Y - MarginSize;

            // Position the labeler GUI
            GUILabeler.Location = new Point(this.Width - GUILabeler.Width - MarginSize, MarginSize);
            GUILabeler.Height = GUITable.Location.Y - GUILabeler.Location.Y - MarginSize;

            // Position the filter GUI
            GUIFilter.Location = new Point(GUISnapshotManager.Location.X + GUISnapshotManager.Width + MarginSize, MarginSize);
            GUIFilter.Width = GUILabeler.Location.X - GUIFilter.Location.X - MarginSize;
            GUIFilter.Height = GUITable.Location.Y - GUIFilter.Location.Y - MarginSize;

            // Position the display GUI
            GUIDisplay.Location = new Point(MarginSize, GUIDisplay.Location.Y);
            GUIDisplay.Height = this.Height - GUIDisplay.Location.Y - MarginSize;

            // Position the table GUI
            GUITable.Location = new Point(GUIDisplay.Location.X + GUIDisplay.Width + MarginSize, GUITable.Location.Y);
            GUITable.Width = this.Width - GUITable.Location.X - MarginSize;
            GUITable.Height = this.Height - GUITable.Location.Y - MarginSize;
        }

        private void GUIBenediction_Resize(object sender, EventArgs e)
        {
            HandleGUIResize();
        }
    }
}
