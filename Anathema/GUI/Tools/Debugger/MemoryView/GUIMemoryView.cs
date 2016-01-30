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
        private const Int32 HexBoxChunkSize = 2;
        private Int64 BaseLine;

        public GUIMemoryView()
        {
            InitializeComponent();

            MemoryViewPresenter = new MemoryViewPresenter(this, new MemoryView());

            MemoryViewPresenter.RefreshVirtualPages();

            InitializeHexBox();
            UpdateHexBoxChunks();
            UpdateStartAddress();
            UpdateDisplayRange();
        }

        private void InitializeHexBox()
        {
            HexEditorBox.ByteProvider = MemoryViewPresenter;
            HexEditorBox.ByteCharConverter = new DefaultByteCharConverter();
            HexEditorBox.LineInfoOffset = 0x1005000;
            HexEditorBox.UseFixedBytesPerLine = true;
            HexEditorBox.Select(HexEditorBox.ByteProvider.Length / 2, 1);
            BaseLine = HexEditorBox.CurrentLine;
        }

        public void ReadValues()
        {
            ControlThreadingHelper.InvokeControlAction<Control>(HexEditorBox, () =>
            {
                HexEditorBox.Invalidate();
            });
        }

        public void UpdateVirtualPages(List<String> VirtualPages)
        {
            ControlThreadingHelper.InvokeControlAction<Control>(GUIToolStrip, () =>
            {
                QuickNavComboBox.Items.Clear();
                QuickNavComboBox.Items.AddRange(VirtualPages.ToArray());
            });
        }

        private void UpdateStartAddress()
        {
            MemoryViewPresenter.UpdateStartReadAddress(unchecked((UInt64)HexEditorBox.LineInfoOffset));
        }

        private void UpdateDisplayRange()
        {
            HexEditorBox.LineInfoOffset = unchecked(HexEditorBox.LineInfoOffset + (HexEditorBox.TopLine - BaseLine));
            HexEditorBox.ScrollByteIntoCenter(HexEditorBox.ByteProvider.Length / 2);
            BaseLine = HexEditorBox.CurrentLine;
        }

        private void UpdateHexBoxChunks()
        {
            MemoryViewPresenter.UpdateReadLength(HexEditorBox.HorizontalByteCount * HexEditorBox.VerticalByteCount);

            HexEditorBox.BytesPerLine += HexBoxChunkSize;

            while (HexEditorBox.Width < HexEditorBox.RequiredWidth)
            {
                if (HexEditorBox.BytesPerLine <= HexBoxChunkSize)
                    break;
                HexEditorBox.BytesPerLine -= HexBoxChunkSize;
            }
        }

        #region Events

        private void RefreshNavigationButton_Click(Object Sender, EventArgs E)
        {
            MemoryViewPresenter.RefreshVirtualPages();
        }

        private void QuickNavComboBox_SelectedIndexChanged(Object Sender, EventArgs E)
        {

        }

        private void HexEditorBox_Resize(Object Sender, EventArgs E)
        {
            UpdateHexBoxChunks();
        }

        private void HexEditorBox_CurrentLineChanged(Object Sender, EventArgs E)
        {
            UpdateStartAddress();
        }

        private void HexEditorBox_UpdateScroll(object sender, EventArgs e)
        {
            UpdateDisplayRange();
        }

        #endregion

    } // End class

} // End namespace