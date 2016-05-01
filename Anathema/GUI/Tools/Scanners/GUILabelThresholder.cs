using Anathema.Scanners.LabelThresholder;
using Anathema.Utils.MVP;
using Anathema.Utils.Validation;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Reflection;
using WeifenLuo.WinFormsUI.Docking;

namespace Anathema.GUI
{
    public partial class GUILabelThresholder : DockContent, ILabelThresholderView
    {
        private LabelThresholderPresenter LabelThresholderPresenter;

        public GUILabelThresholder()
        {
            InitializeComponent();

            LabelThresholderPresenter = new LabelThresholderPresenter(this, new LabelThresholder());

            LabelThresholderPresenter.Begin();
        }

        [Obfuscation(Exclude = true)]
        public void DisplayHistogram(SortedDictionary<dynamic, Int64> SortedDictionary)
        {
            Int32 BarCount = 0;

            ControlThreadingHelper.InvokeControlAction(LabelFrequencyChart, () =>
            {
                foreach (KeyValuePair<dynamic, Int64> Item in SortedDictionary)
                    LabelFrequencyChart.Series["Frequency"].Points.AddXY(Item.Key, Item.Value);
                BarCount = LabelFrequencyChart.Series["Frequency"].Points.Count;
            });

            UpdateTrackBarRanges(BarCount);
            VisualizeSelection();
        }

        private void VisualizeSelection()
        {
            Int32 BarCount = 0;
            UInt64 FrequencyTotal = 0;
            UInt64 FrequencySelected = 0;
            Int32 MinValue = 0;
            Int32 MaxValue = 0;
            Int32 HighlightMinValue = 0;
            Int32 HighlightMaxvalue = 0;
            Boolean SelectionInverted = false;

            ControlThreadingHelper.InvokeControlAction(MinValueTrackBar, () =>
            {
                MinValue = MinValueTrackBar.Value;
            });

            ControlThreadingHelper.InvokeControlAction(MaxValueTrackBar, () =>
            {
                MaxValue = MaxValueTrackBar.Value;
            });

            ControlThreadingHelper.InvokeControlAction(ScanToolStrip, () =>
            {
                SelectionInverted = InvertSelectionButton.Checked;
            });

            // Update chart colors
            ControlThreadingHelper.InvokeControlAction(LabelFrequencyChart, () =>
            {
                BarCount = LabelFrequencyChart.Series["Frequency"].Points.Count;

                for (Int32 Index = 0; Index < BarCount; Index++)
                {
                    if (!SelectionInverted && (Index < MinValue || Index > MaxValue) ||
                        SelectionInverted && (Index >= MinValue && Index <= MaxValue))
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
                if (MinValue < BarCount)
                    HighlightMinValue = (Int32)LabelFrequencyChart.Series["Frequency"].Points[MinValue].XValue;
                if (MaxValue < BarCount)
                    HighlightMaxvalue = (Int32)LabelFrequencyChart.Series["Frequency"].Points[MaxValue].XValue;
            });

            // Update min/max values in presenter
            LabelThresholderPresenter.UpdateThreshold(MinValue, MaxValue);

            // Update trackbar value text
            ControlThreadingHelper.InvokeControlAction(MinLabelLabel, () =>
            {
                if (MinValue < BarCount)
                    MinLabelLabel.Text = "Min: " + HighlightMinValue.ToString();
            });

            ControlThreadingHelper.InvokeControlAction(MaxLabelLabel, () =>
            {
                if (MaxValue < BarCount)
                    MaxLabelLabel.Text = "Max: " + HighlightMaxvalue.ToString();
            });

            // Update threshold size text
            ControlThreadingHelper.InvokeControlAction(ScanToolStrip, () =>
            {
                ReductionLabel.Text = FrequencySelected.ToString() + " / " + FrequencyTotal.ToString() +
                " (~" + Conversions.BytesToMetric(FrequencySelected * (UInt64)LabelThresholderPresenter.GetElementSize()) +
                " / ~" + Conversions.BytesToMetric(FrequencyTotal * (UInt64)LabelThresholderPresenter.GetElementSize()) + ")";
            });
        }

        private void ClearGraph()
        {
            ControlThreadingHelper.InvokeControlAction(LabelFrequencyChart, () =>
            {
                LabelFrequencyChart.Series["Frequency"].Points.Clear();
            });

            ControlThreadingHelper.InvokeControlAction(ScanToolStrip, () =>
            {
                ReductionLabel.Text = String.Empty;
            });

            ControlThreadingHelper.InvokeControlAction(MinLabelLabel, () =>
            {
                MinLabelLabel.Text = "Min: ";
            });

            ControlThreadingHelper.InvokeControlAction(MaxLabelLabel, () =>
            {
                MaxLabelLabel.Text = "Max: ";
            });
        }

        private void UpdateTrackBarRanges(Int32 BarCount)
        {
            ControlThreadingHelper.InvokeControlAction(MinValueTrackBar, () =>
            {
                MinValueTrackBar.Maximum = BarCount == 0 ? 0 : BarCount - 1;
                MinValueTrackBar.Value = 0;
            });

            ControlThreadingHelper.InvokeControlAction(MaxValueTrackBar, () =>
            {
                MaxValueTrackBar.Maximum = BarCount == 0 ? 0 : BarCount - 1;
                MaxValueTrackBar.Value = MaxValueTrackBar.Maximum;
            });
        }

        #region Events

        private void ApplyThresholdButton_Click(Object Sender, EventArgs E)
        {
            LabelThresholderPresenter.ApplyThreshold();
            ClearGraph();
            this.Close();
        }

        private void RefreshButton_Click(Object Sender, EventArgs E)
        {
            LabelThresholderPresenter.Begin();
        }

        private void InvertSelectionButton_Click(Object Sender, EventArgs E)
        {
            VisualizeSelection();
            LabelThresholderPresenter.SetInverted(InvertSelectionButton.Checked);
        }

        private void MinValueTrackBar_Scroll(Object Sender, EventArgs E)
        {
            if (MinValueTrackBar.Value > MaxValueTrackBar.Value)
                MaxValueTrackBar.Value = MinValueTrackBar.Value;

            VisualizeSelection();
        }

        private void MaxValueTrackBar_Scroll(Object Sender, EventArgs E)
        {
            if (MaxValueTrackBar.Value < MinValueTrackBar.Value)
                MinValueTrackBar.Value = MaxValueTrackBar.Value;

            VisualizeSelection();
        }

        #endregion

    } // End class

} // End namespace