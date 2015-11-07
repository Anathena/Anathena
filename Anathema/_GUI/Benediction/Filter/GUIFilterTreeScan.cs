using System;
using System.Windows.Forms;

namespace Anathema
{
    public partial class GUIFilterTreeScan : UserControl
    {
        private FilterHashTrees FilterTreeScan;
        private readonly Benediction BenedictionInstance;

        public GUIFilterTreeScan()
        {
            InitializeComponent();

            FilterTreeScan = new FilterHashTrees();
            //BenedictionInstance = Benediction.GetBenedictionInstance();

            UpdateFragmentSizeLabel();
        }

        private void GUIMemoryTreeFilter_Load(object sender, EventArgs e)
        {

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

            FilterHashTrees.UpdatePageSplitThreshold(Value);
        }

        private void UpdateHashTreeSize(UInt64 Size)
        {
            HashTreeSizeValueLabel.Text = Conversions.ByteToMetricSize(Size).ToString();

            // Force a redraw
            Invalidate();
            Update();
            Refresh();
            Application.DoEvents();
        }

        private void UpdateTreeSplits(Int32 Splits)
        {
            TreeSplitsValueLabel.Text = Splits.ToString();
        }

        private void GUIMemoryTreeFilter_Resize(object sender, EventArgs e)
        {
            AdvancedSettingsGroupBox.SetBounds(4, this.Height / 2 + 4, this.Width - 8, this.Height / 2 - 8);
        }

        private void DisableGUI()
        {
            AdvancedSettingsGroupBox.Enabled = false;
        }

        private void EnableGUI()
        {
            AdvancedSettingsGroupBox.Enabled = true;
        }
        
        private void StartButton_Click(object sender, EventArgs e)
        {
            BenedictionInstance.BeginFilter(FilterTreeScan);
            UpdateGUITimer.Start();
            DisableGUI();
        }

        private void StopButton_Click(object sender, EventArgs e)
        {
            BenedictionInstance.EndFilter();
            UpdateGUITimer.Stop();
            UpdateHashTreeSize(FilterTreeScan.GetFinalSize());
            EnableGUI();
        }

        private void GranularityTrackBar_Scroll(object sender, EventArgs e)
        {
            UpdateFragmentSizeLabel();
        }

        public void UpdateGUI()
        {
            UpdateHashTreeSize(FilterTreeScan.GetHashTreeSize());
            UpdateTreeSplits(FilterTreeScan.GetHashTreeSplits());
        }
    }
}
