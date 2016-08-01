using Anathema.Source.Scanners.LabelThresholder;
using Anathema.Source.Utils;
using Anathema.Source.Utils.MVP;
using Anathema.Source.Utils.Validation;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Reflection;
using WeifenLuo.WinFormsUI.Docking;

namespace Anathema.GUI.Tools.Scanners
{
    public partial class GUILabelThresholder : DockContent, ILabelThresholderView
    {
        private LabelThresholderPresenter LabelThresholderPresenter;
        private Object AccessLock;

        public GUILabelThresholder()
        {
            InitializeComponent();

            AccessLock = new Object();

            LabelThresholderPresenter = new LabelThresholderPresenter(this, new LabelThresholder());
            LabelThresholderPresenter.Begin();
        }

        [Obfuscation(Exclude = true)]
        public void DisplayHistogram(SortedDictionary<dynamic, Int64> SortedDictionary)
        {
            Int32 BarCount = 0;

            using (TimedLock.Lock(AccessLock))
            {
                ControlThreadingHelper.InvokeControlAction(LabelFrequencyChart, () =>
                {
                    foreach (KeyValuePair<dynamic, Int64> Item in SortedDictionary)
                        LabelFrequencyChart.Series["Frequency"].Points.AddXY(Item.Key, Item.Value);
                    BarCount = LabelFrequencyChart.Series["Frequency"].Points.Count;
                });
            }

            UpdateTrackBarRanges(BarCount);
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
                Int32 BarCount = 0;
                UInt64 FrequencyTotal = 0;
                UInt64 FrequencySelected = 0;
                Int32 MinimumIndex = 0;
                Int32 MaximumIndex = 0;
                Int32 HighlightMinValue = 0;
                Int32 HighlightMaxvalue = 0;
                Boolean SelectionInverted = false;

                ControlThreadingHelper.InvokeControlAction(MinValueTrackBar, () =>
                {
                    MinimumIndex = MinValueTrackBar.Value;
                });

                ControlThreadingHelper.InvokeControlAction(MaxValueTrackBar, () =>
                {
                    MaximumIndex = MaxValueTrackBar.Value;
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
                        if (!SelectionInverted && (Index < MinimumIndex || Index > MaximumIndex) ||
                            SelectionInverted && (Index >= MinimumIndex && Index <= MaximumIndex))
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
                    if (MinimumIndex < BarCount)
                        HighlightMinValue = (Int32)LabelFrequencyChart.Series["Frequency"].Points[MinimumIndex].XValue;
                    if (MaximumIndex < BarCount)
                        HighlightMaxvalue = (Int32)LabelFrequencyChart.Series["Frequency"].Points[MaximumIndex].XValue;
                });

                // Update min/max values in presenter
                LabelThresholderPresenter.UpdateThreshold(MinimumIndex, MaximumIndex);

                // Update trackbar value text
                ControlThreadingHelper.InvokeControlAction(MinLabelLabel, () =>
                {
                    if (MinimumIndex < BarCount)
                        MinLabelLabel.Text = "Min: " + HighlightMinValue.ToString();
                });

                ControlThreadingHelper.InvokeControlAction(MaxLabelLabel, () =>
                {
                    if (MaximumIndex < BarCount)
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
        }

        private void ClearGraph()
        {
            using (TimedLock.Lock(AccessLock))
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

        private void UpdateTrackBarRanges(Int32 BarCount)
        {
            using (TimedLock.Lock(AccessLock))
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