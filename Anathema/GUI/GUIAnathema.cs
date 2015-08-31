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
    public partial class GUIAnathema : Form
    {
        private Anathema Anathema { get; set; }

        public GUIAnathema()
        {
            InitializeComponent();
            Anathema = new Anathema();
        }

        /// <summary>
        /// Updates the main display panel to show the given user control
        /// </summary>
        /// <param name="UserControl"></param>
        private void UpdatePanelDisplay(UserControl UserControl)
        {
            ModePanel.Controls.Clear();
            ModePanel.Controls.Add(UserControl);
            HandleGUIResize();
        }

        /// <summary>
        /// Properly places and resizes necessary controls when the GUI is resized
        /// </summary>
        private void HandleGUIResize()
        {
            for (Int32 Index = 0; Index < ModePanel.Controls.Count; Index++)
            {
                ModePanel.Controls[Index].Width = ModePanel.Width;
                ModePanel.Controls[Index].Height = ModePanel.Height;
            }
        }

        /// <summary>
        /// Informs that a new process has been selected
        /// </summary>
        /// <param name="TargetProcess"></param>
        private void ProcessSelected(Process TargetProcess)
        {
            Anathema.UpdateTargetProcess(TargetProcess);
            SelectedProcessLabel.Text = TargetProcess.ProcessName;
            UpdatePanelDisplay(new GUIMemoryTreeFilter());
        }

        #region Event Handlers
        private void GUIAnathema_Load(object sender, EventArgs e)
        {
            // I will not use this load event, but I usually double click my forms to quickly
            // get to my code, which will auto generate this function, so I will keep it here
        }

        private void GUIAnathema_Resize(object sender, EventArgs e)
        {
            HandleGUIResize();
        }

        private void SelectProcessButton_Click(object sender, EventArgs e)
        {
            UpdatePanelDisplay(new GUIProcessSelector(ProcessSelected));
        }

        private void SearchSpaceAnalysisButton_Click(object sender, EventArgs e)
        {
            UpdatePanelDisplay(new GUIMemoryTreeFilter());
        }

        private void FiniteStateMachineButton_Click(object sender, EventArgs e)
        {
            UpdatePanelDisplay(new GUIFiniteStateMachinePanel());
        }

        private void InputCorrelatorButton_Click(object sender, EventArgs e)
        {
            UpdatePanelDisplay(new GUIInputCorrelator());
        }

        private void ManualScanButton_Click(object sender, EventArgs e)
        {
            UpdatePanelDisplay(new GUIManualScan());
        }

        #endregion
    }
}
