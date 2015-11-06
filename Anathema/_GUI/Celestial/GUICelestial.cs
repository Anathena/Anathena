using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Binarysharp.MemoryManagement;
using Be.Windows.Forms;

namespace Anathema
{
    public partial class GUICelestial : UserControl, IProcessObserver
    {
        public GUICelestial()
        {
            InitializeComponent();

            HexEditorBox.ByteProvider = new TestByteProvider();
            HexEditorBox.ByteCharConverter = new DefaultByteCharConverter();
            HexEditorBox.LineInfoOffset = 50;
        }

        public void UpdateProcess(MemorySharp MemoryEditor)
        {

        }
    }
}
