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
    public partial class GUIMemoryTreeFilter : UserControl
    {
        private MemoryTreeFilter MemoryTreeFilter;

        public GUIMemoryTreeFilter()
        {
            InitializeComponent();
            MemoryTreeFilter = new MemoryTreeFilter();
        }

        private void GUIMemoryTreeFilter_Load(object sender, EventArgs e)
        {

        }

        private void StartButton_Click(object sender, EventArgs e)
        {
            MemoryTreeFilter.BeginFilter();
        }

        private void StopButton_Click(object sender, EventArgs e)
        {
            MemoryTreeFilter.EndFilter();
        }
    }
}
