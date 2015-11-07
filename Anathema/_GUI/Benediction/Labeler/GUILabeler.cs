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
    public partial class GUILabeler : UserControl
    {
        // Display constants
        private const Int32 MarginSize = 4;
        private const Int32 MaximumDisplayed = 4000;

        public GUILabeler()
        {
            InitializeComponent();
        }
    }
}
