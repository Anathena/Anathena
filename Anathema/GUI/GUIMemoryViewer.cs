using Be.Windows.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Anathema
{
    public partial class GUIMemoryViewer : Form
    {
        public GUIMemoryViewer()
        {
            InitializeComponent();

            HexEditorBox.ByteProvider = new TestByteProvider();
            HexEditorBox.ByteCharConverter = new DefaultByteCharConverter();
            HexEditorBox.LineInfoOffset = 50;
        }

        private void MemoryViewer_Load(object sender, EventArgs e)
        {
            
        }

        private void HandleResize()
        {

        }
    }
}
