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
        private String DocumentTitle;

        public GUILabelThresholder()
        {
            InitializeComponent();

            DocumentTitle = this.Text;

            LabelThresholderPresenter = new LabelThresholderPresenter(this, new LabelThresholder());
        }

        #region Events

        #endregion

    } // End class

} // End namespace