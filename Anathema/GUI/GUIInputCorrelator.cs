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
    public partial class GUIInputCorrelator : UserControl, GUIUpdateableControl
    {
        private InputCorrelator InputCorrelator;
        private readonly Anathema Anathema;

        public GUIInputCorrelator()
        {
            InitializeComponent();
            InputCorrelator = new InputCorrelator();
            Anathema = Anathema.GetAnathemaInstance();

            UpdateVariableSizeLabel();

            HandleResize();
        }

        public void UpdateGUI()
        {

        }

        private void HandleResize()
        {

        }

        private void UpdateVariableSizeLabel()
        {
            int Value = (int)Math.Pow(2, GranularityTrackBar.Value);
            string LabelText = Value.ToString();

            if (Value == 1)
                LabelText += " Byte";
            else
                LabelText += " Bytes";

            FragmentSizeValueLabel.Text = LabelText;
        }

        private void GranularityTrackBar_Scroll(object sender, EventArgs e)
        {
            UpdateVariableSizeLabel();
        }

        private void GUIInputCorrelator_Load(object sender, EventArgs e)
        {

        }

        private void GUIInputCorrelator_Resize(object sender, EventArgs e)
        {
            HandleResize();
        }

        private void StartSSAButton_Click(object sender, EventArgs e)
        {
            Anathema.BeginLabeler(InputCorrelator);
        }

        private void StopSSAButton_Click(object sender, EventArgs e)
        {
            Anathema.EndLabeler();
        }
    }
}
