using Binarysharp.MemoryManagement;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Anathema
{
    partial class GUIMain : Form
    {
        private MemorySharp MemoryEditor;

        private GUIProcessSelector GUIProcessSelector;
        private GUIAnathema GUIAnathema;
        private GUIBenediction GUIBenediction;
        private GUICelestial GUICelestial;

        public GUIMain()
        {
            InitializeComponent();

            // Instantiate all primary GUI components
            GUIProcessSelector = new GUIProcessSelector(UpdateTargetProcess);
            GUIAnathema = new GUIAnathema();
            GUIBenediction = new GUIBenediction();
            GUICelestial = new GUICelestial();

            // All GUI components should fill the panel
            GUIProcessSelector.Dock = DockStyle.Fill;
            GUIAnathema.Dock = DockStyle.Fill;
            GUIBenediction.Dock = DockStyle.Fill;
            GUICelestial.Dock = DockStyle.Fill;
        }

        /// <summary>
        /// Update the target process 
        /// </summary>
        /// <param name="TargetProcess"></param>
        public void UpdateTargetProcess(Process TargetProcess)
        {
            // Instantiate a new memory editor with the new target process
            MemoryEditor = new MemorySharp(TargetProcess);

            // Update components with new process
            GUIBenediction.UpdateProcess(MemoryEditor);
            GUICelestial.UpdateProcess(MemoryEditor);

            // Switch selection to benediction
            MakeSelection(ViewBenedictionButton, GUIBenediction);

            // Update process text
            ProcessSelectedLabel.Text = TargetProcess.ProcessName;
        }

        private void MakeSelection(ToolStripButton Sender, UserControl NewView)
        {
            // Clear GUI elements in main panel
            foreach (Control NextControl in ComponentPanel.Controls)
                ComponentPanel.Controls.Remove(NextControl);

            // Uncheck all GUI options
            SelectProcessButton.Checked = false;
            ViewAnathemaButton.Checked = false;
            ViewBenedictionButton.Checked = false;
            ViewCelestialButton.Checked = false;

            // Check selection
            if (Sender != null)
                Sender.Checked = true;

            // Add the new view
            if (NewView != null)
                ComponentPanel.Controls.Add(NewView);
        }


        private void SelectProcessButton_Click(object sender, EventArgs e)
        {
            MakeSelection((ToolStripButton)sender, GUIProcessSelector);
        }

        private void ViewAnathemaButton_Click(object sender, EventArgs e)
        {
            MakeSelection((ToolStripButton)sender, GUIAnathema);
        }

        private void ViewBenedictionButton_Click(object sender, EventArgs e)
        {
            MakeSelection((ToolStripButton)sender, GUIBenediction);
        }

        private void ViewCelestialButton_Click(object sender, EventArgs e)
        {
            MakeSelection((ToolStripButton)sender, GUICelestial);
        }
    }
}
