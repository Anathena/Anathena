using Anathena.Source.Scanners.LabelThresholder;
using Anathena.Source.Utils;
using Anathena.Source.Utils.MVP;
using Anathena.Source.Utils.Validation;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Reflection;
using WeifenLuo.WinFormsUI.Docking;

namespace Anathena.GUI.Tools.Scanners
{
    public partial class GUILabelThresholder : DockContent, ILabelThresholderView
    {
        private LabelThresholderPresenter labelThresholderPresenter;
        private Object accessLock;

        public GUILabelThresholder()
        {
            InitializeComponent();

            accessLock = new Object();

            labelThresholderPresenter = new LabelThresholderPresenter(this, new LabelThresholder());
            labelThresholderPresenter.Begin();
        }

        [Obfuscation(Exclude = true)]
        public void DisplayHistogram(SortedDictionary<dynamic, Int64> sortedDictionary)
        {
            Int32 barCount = 0;

            using (TimedLock.Lock(accessLock))
            {
                ControlThreadingHelper.InvokeControlAction(LabelFrequencyChart, () =>
                {
                    foreach (KeyValuePair<dynamic, Int64> item in sortedDictionary)
                        LabelFrequencyChart.Series["Frequency"].Points.AddXY(item.Key, item.Value);
                    barCount = LabelFrequencyChart.Series["Frequency"].Points.Count;
                });
            }

            UpdateTrackBarRanges(barCount);
            VisualizeSelection();

            ControlThreadingHelper.InvokeControlAction(LabelFrequencyChart, () =>
            {
                LabelFrequencyChart.Invalidate();
            });
        }

        private void VisualizeSelection()
        {
            // using (TimedLock.Lock(AccessLock))
            {
                Int32 barCount = 0;
                UInt64 frequencyTotal = 0;
                UInt64 frequencySelected = 0;
                Int32 minimumIndex = 0;
                Int32 maximumIndex = 0;
                Int32 highlightMinValue = 0;
                Int32 highlightMaxvalue = 0;
                Boolean selectionInverted = false;

                ControlThreadingHelper.InvokeControlAction(MinValueTrackBar, () =>
                {
                    minimumIndex = MinValueTrackBar.Value;
                });

                ControlThreadingHelper.InvokeControlAction(MaxValueTrackBar, () =>
                {
                    maximumIndex = MaxValueTrackBar.Value;
                });

                ControlThreadingHelper.InvokeControlAction(ScanToolStrip, () =>
                {
                    selectionInverted = InvertSelectionButton.Checked;
                });

                // Update chart colors
                ControlThreadingHelper.InvokeControlAction(LabelFrequencyChart, () =>
                {
                    barCount = LabelFrequencyChart.Series["Frequency"].Points.Count;

                    for (Int32 Index = 0; Index < barCount; Index++)
                    {
                        if (!selectionInverted && (Index < minimumIndex || Index > maximumIndex) ||
                            selectionInverted && (Index >= minimumIndex && Index <= maximumIndex))
                        {
                            LabelFrequencyChart.Series["Frequency"].Points[Index].Color = Color.Red;
                        }
                        else
                        {
                            LabelFrequencyChart.Series["Frequency"].Points[Index].Color = Color.Blue;
                            frequencySelected += (UInt64)LabelFrequencyChart.Series["Frequency"].Points[Index].YValues[0];
                        }
                        frequencyTotal += (UInt64)LabelFrequencyChart.Series["Frequency"].Points[Index].YValues[0];
                    }
                    if (minimumIndex < barCount)
                        highlightMinValue = (Int32)LabelFrequencyChart.Series["Frequency"].Points[minimumIndex].XValue;
                    if (maximumIndex < barCount)
                        highlightMaxvalue = (Int32)LabelFrequencyChart.Series["Frequency"].Points[maximumIndex].XValue;
                });

                // Update min/max values in presenter
                labelThresholderPresenter.UpdateThreshold(minimumIndex, maximumIndex);

                // Update trackbar value text
                ControlThreadingHelper.InvokeControlAction(MinLabelLabel, () =>
                {
                    if (minimumIndex < barCount)
                        MinLabelLabel.Text = "Min: " + highlightMinValue.ToString();
                });

                ControlThreadingHelper.InvokeControlAction(MaxLabelLabel, () =>
                {
                    if (maximumIndex < barCount)
                        MaxLabelLabel.Text = "Max: " + highlightMaxvalue.ToString();
                });

                // Update threshold size text
                ControlThreadingHelper.InvokeControlAction(ScanToolStrip, () =>
                {
                    ReductionLabel.Text = frequencySelected.ToString() + " / " + frequencyTotal.ToString() +
                    " (~" + Conversions.BytesToMetric(frequencySelected * (UInt64)labelThresholderPresenter.GetElementSize()) +
                    " / ~" + Conversions.BytesToMetric(frequencyTotal * (UInt64)labelThresholderPresenter.GetElementSize()) + ")";
                });
            }
        }

        private void ClearGraph()
        {
            using (TimedLock.Lock(accessLock))
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
        }

        private void UpdateTrackBarRanges(Int32 barCount)
        {
            using (TimedLock.Lock(accessLock))
            {
                ControlThreadingHelper.InvokeControlAction(MinValueTrackBar, () =>
                {
                    MinValueTrackBar.Maximum = barCount == 0 ? 0 : barCount - 1;
                    MinValueTrackBar.Value = 0;
                });

                ControlThreadingHelper.InvokeControlAction(MaxValueTrackBar, () =>
                {
                    MaxValueTrackBar.Maximum = barCount == 0 ? 0 : barCount - 1;
                    MaxValueTrackBar.Value = MaxValueTrackBar.Maximum;
                });
            }
        }

        #region Events

        private void ApplyThresholdButton_Click(Object sender, EventArgs e)
        {
            labelThresholderPresenter.ApplyThreshold();
            ClearGraph();
            this.Close();
        }

        private void RefreshButton_Click(Object sender, EventArgs e)
        {
            labelThresholderPresenter.Begin();
        }

        private void InvertSelectionButton_Click(Object sender, EventArgs e)
        {
            VisualizeSelection();
            labelThresholderPresenter.SetInverted(InvertSelectionButton.Checked);
        }

        private void MinValueTrackBar_Scroll(Object sender, EventArgs e)
        {
            if (MinValueTrackBar.Value > MaxValueTrackBar.Value)
                MaxValueTrackBar.Value = MinValueTrackBar.Value;

            VisualizeSelection();
        }

        private void MaxValueTrackBar_Scroll(Object sender, EventArgs e)
        {
            if (MaxValueTrackBar.Value < MinValueTrackBar.Value)
                MinValueTrackBar.Value = MaxValueTrackBar.Value;

            VisualizeSelection();
        }

        #endregion

    } // End class

} // End namespace