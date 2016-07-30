using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Anathema.GUI.Tools.Projects.TypeEditors
{
    public partial class GUIArrayEditor : Form
    {
        public IEnumerable<Int32> Value { get; set; }

        public GUIArrayEditor()
        {
            InitializeComponent();
        }

        private void GUIArrayEditor_FormClosing(Object Sender, FormClosingEventArgs E)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

    } // End class

} // End namespace