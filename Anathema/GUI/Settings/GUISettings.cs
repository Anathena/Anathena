using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Linq;

namespace Anathema
{
    public partial class GUISettings : Form
    {

        public GUISettings(Int32 MainSelection, Int32[] AddressTableItemIndicies)
        {
            InitializeComponent();
        }
        
        #region Events
        
        private void OkButton_Click(Object Sender, EventArgs E)
        {
            this.Close();
        }

        #endregion
    }
}
