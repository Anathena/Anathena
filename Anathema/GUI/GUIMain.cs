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
    public partial class GUIMain : Form
    {
        private UserControl GUIProcessSelector;
        private UserControl GUIAnathema;
        private UserControl GUIBenediction;
        private UserControl GUICelestial;
        
        public GUIMain()
        {
            InitializeComponent();

            // Instantiate all primary GUI components
            GUIProcessSelector = new GUIProcessSelector();
            GUIAnathema = new GUIAnathema();
            GUIBenediction = new GUIBenediction();
            GUICelestial = new GUICelestial();

            // All GUI components should fill the panel
            GUIProcessSelector.Dock = DockStyle.Fill;
            GUIAnathema.Dock = DockStyle.Fill;
            GUIBenediction.Dock = DockStyle.Fill;
            GUICelestial.Dock = DockStyle.Fill;
        }

        private void GUIMain_Load(object sender, EventArgs e)
        {

        }

        private void DeselectAll(ToolStripButton Sender)
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
            Sender.Checked = true;
        }

        private void MakeSelection(UserControl CurrentView)
        {
            ComponentPanel.Controls.Add(CurrentView);
        }

        private void SelectProcessButton_Click(object sender, EventArgs e)
        {
            DeselectAll((ToolStripButton)sender);
            MakeSelection(GUIProcessSelector);
        }

        private void ViewAnathemaButton_Click(object sender, EventArgs e)
        {
            DeselectAll((ToolStripButton)sender);
            MakeSelection(GUIAnathema);
        }

        private void ViewBenedictionButton_Click(object sender, EventArgs e)
        {
            DeselectAll((ToolStripButton)sender);
            MakeSelection(GUIBenediction);
        }

        private void ViewCelestialButton_Click(object sender, EventArgs e)
        {
            DeselectAll((ToolStripButton)sender);
            MakeSelection(GUICelestial);
        }
    }
}
