using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;
using System.IO;
using Binarysharp.MemoryManagement;
using Binarysharp.MemoryManagement.Memory;

namespace Anathema
{
    public partial class GUILabelThresholder : DockContent, ILabelThresholderView
    {
        private LabelThresholderPresenter LabelThresholderPresenter;

        public GUILabelThresholder()
        {
            InitializeComponent();

            LabelThresholderPresenter = new LabelThresholderPresenter(this, new LabelThresholder());

            LabelThresholderPresenter.GatherData();
        }

        public void DisplayHistogram(SortedDictionary<dynamic, Int64> SortedDictionary)
        {
            foreach (KeyValuePair<dynamic, Int64> Item in SortedDictionary)
                LabelFrequencyChart.Series["Frequency"].Points.AddXY(Item.Key, Item.Value);

            UpdateTrackBarRanges();
            UpdateSelection();
        }

        private void UpdateSelection()
        {
            UInt64 FrequencyTotal = 0;
            UInt64 FrequencySelected = 0;

            // Update colors
            for (Int32 Index = 0; Index < LabelFrequencyChart.Series["Frequency"].Points.Count; Index++)
            {
                if (!InvertSelectionButton.Checked && (Index < MinValueTrackBar.Value || Index > MaxValueTrackBar.Value) ||
                    InvertSelectionButton.Checked && (Index >= MinValueTrackBar.Value && Index <= MaxValueTrackBar.Value))
                {
                    LabelFrequencyChart.Series["Frequency"].Points[Index].Color = Color.Red;
                }
                else
                {
                    LabelFrequencyChart.Series["Frequency"].Points[Index].Color = Color.Blue;
                    FrequencySelected += (UInt64)LabelFrequencyChart.Series["Frequency"].Points[Index].YValues[0];
                }
                FrequencyTotal += (UInt64)LabelFrequencyChart.Series["Frequency"].Points[Index].YValues[0];
            }

            // Update trackbar value text
            if (MinValueTrackBar.Value < LabelFrequencyChart.Series["Frequency"].Points.Count)
                MinLabelLabel.Text = "Min: " + LabelFrequencyChart.Series["Frequency"].Points[MinValueTrackBar.Value].XValue.ToString();
            if (MaxValueTrackBar.Value < LabelFrequencyChart.Series["Frequency"].Points.Count)
                MaxLabelLabel.Text = "Max: " + LabelFrequencyChart.Series["Frequency"].Points[MaxValueTrackBar.Value].XValue.ToString();

            ReductionLabel.Text = FrequencySelected.ToString() + " / " + FrequencyTotal.ToString() + " (" + Conversions.BytesToMetric(FrequencySelected) + " / " + Conversions.BytesToMetric(FrequencyTotal) + ")";
        }

        private void ClearGraph()
        {
            LabelFrequencyChart.Series["Frequency"].Points.Clear();
            ReductionLabel.Text = String.Empty;
            MinLabelLabel.Text = "Min: ";
            MaxLabelLabel.Text = "Max: ";
        }

        private void UpdateTrackBarRanges()
        {
            Int32 BarCount = LabelFrequencyChart.Series["Frequency"].Points.Count;

            MinValueTrackBar.Maximum = BarCount == 0 ? 0 : BarCount - 1;
            MaxValueTrackBar.Maximum = BarCount == 0 ? 0 : BarCount - 1;
            MinValueTrackBar.Value = 0;
            MaxValueTrackBar.Value = MaxValueTrackBar.Maximum;
        }

        #region Events

        private void ApplyThresholdButton_Click(Object Sender, EventArgs E)
        {
            LabelThresholderPresenter.ApplyThreshold(MinValueTrackBar.Value, MaxValueTrackBar.Value);
            ClearGraph();
            this.Close();
        }

        private void RefreshButton_Click(Object Sender, EventArgs E)
        {
            LabelThresholderPresenter.GatherData();
        }

        private void InvertSelectionButton_Click(Object Sender, EventArgs E)
        {
            UpdateSelection();
            LabelThresholderPresenter.SetInverted(InvertSelectionButton.Checked);
        }

        private void MinValueTrackBar_Scroll(Object Sender, EventArgs E)
        {
            if (MinValueTrackBar.Value > MaxValueTrackBar.Value)
                MaxValueTrackBar.Value = MinValueTrackBar.Value;

            UpdateSelection();
        }

        private void MaxValueTrackBar_Scroll(Object Sender, EventArgs E)
        {
            if (MaxValueTrackBar.Value < MinValueTrackBar.Value)
                MinValueTrackBar.Value = MaxValueTrackBar.Value;

            UpdateSelection();
        }

        #endregion

    } // End class

} // End namespace