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
    public partial class GUIResults : DockContent, IResultsView
    {
        public GUIResults()
        {
            InitializeComponent();
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint, true);
            this.DoubleBuffered = true;

            ResultsPresenter ResultsPresenter = new ResultsPresenter(this, new Results());
        }

        public void DisplayResults(ListViewItem[] Items)
        {
            ControlThreadingHelper.InvokeControlAction(ResultsListView, () =>
            {
                // Pause drawing and updating (removes flicker)
                ResultsListView.SuspendLayout();
                ResultsListView.BeginUpdate();

                // Save the top item in the list
                Int32 TopItemIndex = 0;
                if (ResultsListView.TopItem != null)
                    TopItemIndex = ResultsListView.TopItem.Index;
                
                // Update current results
                ResultsListView.Items.Clear();
                ResultsListView.Items.AddRange(Items);

                // Restore scroll location
                if (TopItemIndex >= 0 && TopItemIndex < ResultsListView.Items.Count)
                    ResultsListView.TopItem = ResultsListView.Items[TopItemIndex];
                
                // Resume drawing and updating
                ResultsListView.EndUpdate();
                ResultsListView.ResumeLayout();

            });
        }
    }
}
