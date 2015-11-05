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
        private UserControl GUIAnathema;
        private UserControl GUIBenediction;
        private UserControl GUICelestial;

        private enum CurrentViewEnum
        {
            SelectProcess,
            Anathema,
            Benediction,
            Celestial
        }

        CurrentViewEnum CurrentView;
        
        public GUIMain()
        {
            InitializeComponent();

            // Instantiate all primary GUI components
            GUIAnathema = new GUIAnathema();
            GUIBenediction = new GUIBenediction();
            GUICelestial = new GUICelestial();

            // All GUI components should fill the panel
            GUIAnathema.Dock = DockStyle.Fill;
            GUIBenediction.Dock = DockStyle.Fill;
            GUICelestial.Dock = DockStyle.Fill;

            // Default view will be Anathema
            CurrentView = CurrentViewEnum.Anathema;

            // Apply view
            UpdateDisplay();
        }

        private void GUIMain_Load(object sender, EventArgs e)
        {

        }

        private void UpdateDisplay()
        {
            // Clear GUI elements in main panel
            foreach (Control NextControl in ComponentPanel.Controls)
                ComponentPanel.Controls.Remove(NextControl);

            switch (CurrentView)
            {
                case CurrentViewEnum.SelectProcess:

                    break;
                case CurrentViewEnum.Anathema:
                    ComponentPanel.Controls.Add(GUIAnathema);
                    break;
                case CurrentViewEnum.Benediction:
                    ComponentPanel.Controls.Add(GUIBenediction);
                    break;
                case CurrentViewEnum.Celestial:
                    ComponentPanel.Controls.Add(GUICelestial);
                    break;
            }
        }
    }
}
