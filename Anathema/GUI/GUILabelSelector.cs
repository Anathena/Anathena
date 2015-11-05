using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Anathema
{
    public partial class GUILabelSelector : UserControl
    {
        private const Int32 MaximumDisplayed = 4000;
        public GUILabelSelector(List<Tuple<IntPtr, Object>> MemoryLabels)
        {
            InitializeComponent();

            UpdateMemoryLabels(MemoryLabels);
        }

        public void UpdateGUI()
        {

        }

        public void UpdateMemoryLabels(List<Tuple<IntPtr, Object>> MemoryLabels)
        {
            for (Int32 Index = 0; Index < Math.Min(MemoryLabels.Count, MaximumDisplayed); Index++)
            {
                ListViewItem NextEntry = new ListViewItem(MemoryLabels[Index].Item1.ToString());
                NextEntry.SubItems.Add(MemoryLabels[Index].Item2.ToString());
                NextEntry.SubItems.Add("");
                AddressListView.Items.Add(NextEntry);
            }
        }

        private void GUILabelSelector_Load(object sender, EventArgs e)
        {

        }

        private void AddressListView_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void AddressCount_Click(object sender, EventArgs e)
        {

        }
    }
}
