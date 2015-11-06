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
    public partial class GUIFilterManualScan : UserControl
    {
        private Benediction BenedictionInstance;

        public GUIFilterManualScan()
        {
            InitializeComponent();

            BenedictionInstance = Benediction.GetBenedictionInstance();
        }

        public void UpdateGUI()
        {

        }
    }
}
