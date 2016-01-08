using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Linq;

namespace Anathema
{
    public partial class GUISettings : Form, ISettingsView
    {
        public GUISettings()
        {
            InitializeComponent();
        }

        private void SaveSettings()
        {

        }
        
        #region Events
        
        private void AcceptButton_Click(Object Sender, EventArgs E)
        {
            SaveSettings();
            this.Close();
        }

        #endregion
    }
}
