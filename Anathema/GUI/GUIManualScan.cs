using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Anathema
{
    public partial class GUIManualScan : UserControl
    {
        private Anathema AnathemaInstance;

        public GUIManualScan()
        {
            InitializeComponent();

            AnathemaInstance = Anathema.GetAnathemaInstance();
        }

        public void UpdateGUI()
        {

        }
    }
}
