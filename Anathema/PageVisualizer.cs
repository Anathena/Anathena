using System;
using System.Collections;
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
    partial class PageVisualizer : Form
    {
        private List<SearchSpaceAnalyzer.MemoryChangeData> MemoryChangeData;
        private bool[] HideItem;

        public PageVisualizer(List<SearchSpaceAnalyzer.MemoryChangeData> MemoryChangeData)
        {
            this.MemoryChangeData = MemoryChangeData;
            InitializeComponent();
        }

        private void PageVisualizer_Load(object sender, EventArgs e)
        {
            UpdateGraph();
        }

        private void UpdateGraph()
        {
            PageChart.Series.Clear();

            int[] HideShift = new int[MemoryChangeData.Count];
            HideItem = new bool[MemoryChangeData.Count];

            // Filter out pages that never change or change every single cycle (if the settings ask to do so)
            for (int PageIndex = 0; PageIndex < MemoryChangeData.Count; PageIndex++)
            {
                bool IsDynamic = true;
                bool IsConstant = false; // TEMP TEMP TEMP TEMP (should be true)

                for (int ChangeIndex = 0; ChangeIndex < MemoryChangeData[PageIndex].ChangeHistory.Count; ChangeIndex++)
                {
                    if (MemoryChangeData[PageIndex].ChangeHistory[ChangeIndex] == true)
                    {
                        IsConstant = false;
                    }
                    else
                    {
                        IsDynamic = false;
                    }
                }

                if ((CheckBoxHideConstant.Checked && IsConstant) || (CheckBoxHideDynamic.Checked && IsDynamic))
                {
                    HideItem[PageIndex] = true;
                    HideShift[PageIndex] = 1;
                }

                if (PageIndex > 0)
                {
                    HideShift[PageIndex] += HideShift[PageIndex - 1];
                }

            }

            // Add each page and configure it
            for (int PageIndex = 0; PageIndex < MemoryChangeData.Count; PageIndex++)
            {
                if (HideItem[PageIndex])
                    continue;

                String ItemID = (PageIndex - HideShift[PageIndex]).ToString();

                PageChart.Series.Add(ItemID);
                PageChart.Series[ItemID].XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Int32;
                PageChart.Series[ItemID].YValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Single;
                PageChart.Series[ItemID].BorderWidth = 1;
                PageChart.Series[ItemID].IsVisibleInLegend = false;
                PageChart.Series[ItemID].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.StepLine;
            }

            for (int PageIndex = 0; PageIndex < MemoryChangeData.Count; PageIndex++)
            {
                if (HideItem[PageIndex])
                    continue;

                String ItemID = (PageIndex - HideShift[PageIndex]).ToString();

                    for (int ChangeIndex = 0; ChangeIndex < MemoryChangeData[PageIndex].ChangeHistory.Count; ChangeIndex++)
                    {
                        PageChart.Series[ItemID].Points.AddXY(ChangeIndex,
                            Convert.ToSingle(MemoryChangeData[PageIndex].ChangeHistory[ChangeIndex]) +
                            (Single)(PageIndex - HideShift[PageIndex]) * 1.1f);
                    }
            }

            //PageChart.Series["Changed"].Points.AddXY(1, 1);
            //PageChart.Series["Changed"].Points.AddXY(2, 1);
        }

        private void CheckBoxHideConstant_CheckedChanged(object sender, EventArgs e)
        {
            UpdateGraph();
        }

        private void CheckBoxHideDynamic_CheckedChanged(object sender, EventArgs e)
        {
            UpdateGraph();
        }
    }
}
