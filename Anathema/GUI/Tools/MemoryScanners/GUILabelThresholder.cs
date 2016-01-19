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
        }

        #region Events

        #endregion

    } // End class

} // End namespace