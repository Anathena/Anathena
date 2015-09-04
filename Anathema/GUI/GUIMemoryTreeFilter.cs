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
        private readonly Anathema Anathema;

        public GUIMemoryTreeFilter()
        {
            InitializeComponent();

            MemoryTreeFilter = new MemoryTreeFilter();
            Anathema = Anathema.GetAnathemaInstance();
            UpdateFragmentSizeLabel();
        }

        private void UpdateFragmentSizeLabel()
        {
            UInt64 Value = (UInt64)Math.Pow(2, GranularityTrackBar.Value);
            string LabelText = Value.ToString();

            if (Value == 1)
                LabelText += " Byte";
            else
                LabelText += " Bytes";

            FragmentSizeValueLabel.Text = LabelText;

            MemoryTreeFilter.UpdatePageSplitThreshold(Value);
        }

        private void GUIMemoryTreeFilter_Load(object sender, EventArgs e)
        {

        }

        private void StartButton_Click(object sender, EventArgs e)
        {
            Anathema.BeginFilter(MemoryTreeFilter);
        }

        private void StopButton_Click(object sender, EventArgs e)
        {
            Anathema.EndFilter();
        }

        private void GranularityTrackBar_Scroll(object sender, EventArgs e)
        {
            UpdateFragmentSizeLabel();
        }
    }
}
