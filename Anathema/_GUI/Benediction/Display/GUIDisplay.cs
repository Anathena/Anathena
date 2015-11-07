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
    public partial class GUIDisplay : UserControl
    {
        // Display constants
        private const Int32 MarginSize = 4;
        private const Int32 MaximumDisplayed = 4000;

        public GUIDisplay()
        {
            InitializeComponent();
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
    }
}
