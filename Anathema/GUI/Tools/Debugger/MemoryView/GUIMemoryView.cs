using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;
using System.IO;
using Be.Windows.Forms;

namespace Anathema
{
    public partial class GUIMemoryView : DockContent, IMemoryViewView
    {
        private MemoryViewPresenter MemoryViewPresenter;

        public GUIMemoryView()
        {
            InitializeComponent();

            MemoryViewPresenter = new MemoryViewPresenter(this, new MemoryView());

            HexEditorBox.ByteProvider = MemoryViewPresenter;
            HexEditorBox.ByteCharConverter = new DefaultByteCharConverter();
            HexEditorBox.LineInfoOffset = 5000;

            MemoryViewPresenter.RefreshVirtualPages();
        }

        public void ReadValues()
        {
            throw new NotImplementedException();
        }

        public void UpdateItemCount(Int32 ItemCount)
        {
            throw new NotImplementedException();
        }

        public void UpdateVirtualPages(List<String> VirtualPages)
        {
            ControlThreadingHelper.InvokeControlAction<Control>(GUIToolStrip, () =>
            {
                QuickNavComboBox.Items.Clear();
                QuickNavComboBox.Items.AddRange(VirtualPages.ToArray());
            });
        }

        #region Events

        private void RefreshNavigationButton_Click(Object Sender, EventArgs E)
        {
            MemoryViewPresenter.RefreshVirtualPages();
        }

        private void QuickNavComboBox_SelectedIndexChanged(Object Sender, EventArgs E)
        {

        }

        #endregion

    } // End class

} // End namespace