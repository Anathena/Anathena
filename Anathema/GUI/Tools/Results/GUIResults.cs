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

            ResultsPresenter ResultsPresenter = new ResultsPresenter(this, new Results());
        }

        public void DisplayResults(List<String> Addresses, List<String> Values)
        {
            ResultsListView.Items.Clear();
            for (Int32 Index = 0; Index < Addresses.Count; Index++)
            {
                ResultsListView.Items.Add(Addresses[Index]).SubItems.Add(Values[Index]);
            }
        }
    }
}
