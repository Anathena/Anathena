using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;
using System.IO;

namespace Anathema
{
    public partial class GUILabelerInputCorrelator : DockContent
    {
        /*
        May want in the future:

        Control on variable reactivity (before, after, both)

        Custom variable size? If we are looking for chunk correlation this could be useful.
        Honestly who would need this though?

        Custom alignment? Probably not, the users don't need this. I have been using
        CE for 10 years and have only ever done: No alignment, or 4 byte alignment.
        */

        public GUILabelerInputCorrelator()
        {
            InitializeComponent();

            SetVariableSize();
            SetAlignmentSizeVariable();
        }

        private void SetVariableSize()
        {
            UInt64 Value = (UInt64)Math.Pow(2, VariableSizeTrackBar.Value);
            String LabelText = Conversions.ByteCountToMetricSize(Value).ToString();

            VariableSizeValueLabel.Text = LabelText;
        }

        private void SetAlignmentSizeVariable()
        {
            UInt64 Value = (UInt64)Math.Pow(2, AlignmentSizeTrackBar.Value);
            String LabelText = Value.ToString();

            AlignmentSizeValueLabel.Text = LabelText;
        }

        private void HandleResize()
        {
            AlignmentSizeTrackBar.Location = new Point(this.Width / 2, AlignmentSizeTrackBar.Location.Y);
            AlignmentSizeTrackBar.Width = this.Width - AlignmentSizeTrackBar.Location.X;
            VariableSizeTrackBar.Width = AlignmentSizeTrackBar.Location.X - VariableSizeTrackBar.Location.X;
        }

        private void CustomSizeCheckBox_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void VariableSizeTrackBar_Scroll(object sender, EventArgs e)
        {
            SetVariableSize();
        }

        private void AlignmentTrackBar_Scroll(object sender, EventArgs e)
        {
            SetAlignmentSizeVariable();
        }

        private void GUILabelerInputCorrelator_Resize(object sender, EventArgs e)
        {
            HandleResize();
        }
    }
}
