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

        private const Int32 MaxHorizontalBytes = 64;
        private const Int32 HexBoxChunkSize = 2;
        private Int64 BaseLine;

        public GUIMemoryView()
        {
            InitializeComponent();

            MemoryViewPresenter = new MemoryViewPresenter(this, new MemoryView());

            MemoryViewPresenter.RefreshVirtualPages();

            InitializeHexBox();
            UpdateHexBoxChunks();
            UpdateDisplayRange();
        }

        private void InitializeHexBox()
        {
            HexEditorBox.ByteProvider = MemoryViewPresenter;
            HexEditorBox.ByteCharConverter = new DefaultByteCharConverter();
            HexEditorBox.LineInfoOffset = 0;
            HexEditorBox.UseFixedBytesPerLine = true;
            HexEditorBox.Select(HexEditorBox.ByteProvider.Length / 2, 1);
            BaseLine = HexEditorBox.CurrentLine;

            MemoryViewPresenter.QuickNavigate(QuickNavComboBox.SelectedIndex);
        }

        public void ReadValues()
        {
            ControlThreadingHelper.InvokeControlAction<Control>(HexEditorBox, () =>
            {
                HexEditorBox.Invalidate();
            });
        }

        public void GoToAddress(IntPtr Address)
        {
            ControlThreadingHelper.InvokeControlAction<Control>(HexEditorBox, () =>
            {
                if (HexEditorBox.ByteProvider == null)
                    return;
                HexEditorBox.LineInfoOffset = (Address.Subtract(HexEditorBox.ByteProvider.Length / 2).Subtract(Address.Mod(HexEditorBox.HorizontalByteCount)).ToInt64());
                BaseLine = HexEditorBox.TopLine;
                UpdateDisplayRange();
            });
        }

        public void UpdateVirtualPages(List<String> VirtualPages)
        {
            ControlThreadingHelper.InvokeControlAction<Control>(GUIToolStrip, () =>
            {
                QuickNavComboBox.Items.Clear();
                if (VirtualPages != null)
                    QuickNavComboBox.Items.AddRange(VirtualPages.ToArray());

                if (QuickNavComboBox.Items.Count > 0)
                    QuickNavComboBox.SelectedIndex = 0;
            });
        }

        private void UpdateDisplayRange()
        {
            HexEditorBox.LineInfoOffset = unchecked(HexEditorBox.LineInfoOffset + (HexEditorBox.TopLine - BaseLine) * HexEditorBox.HorizontalByteCount);
            HexEditorBox.ScrollByteIntoCenter(HexEditorBox.ByteProvider.Length / 2);

            BaseLine = HexEditorBox.TopLine;

            MemoryViewPresenter.UpdateBaseAddress(unchecked((IntPtr)HexEditorBox.LineInfoOffset));
            MemoryViewPresenter.UpdateStartReadAddress(unchecked((IntPtr)HexEditorBox.LineInfoOffset).Add(HexEditorBox.TopIndex));
        }

        private void UpdateHexBoxChunks()
        {
            MemoryViewPresenter.UpdateReadLength(HexEditorBox.HorizontalByteCount * HexEditorBox.VerticalByteCount);

            // Assume the maximum number of bytes we can display
            HexEditorBox.BytesPerLine = MaxHorizontalBytes;

            // Decrease this value iteratively until we can fit the content on the screen
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
            MemoryViewPresenter.QuickNavigate(QuickNavComboBox.SelectedIndex);
        }

        private void HexEditorBox_Resize(Object Sender, EventArgs E)
        {
            UpdateHexBoxChunks();
        }

        private void HexEditorBox_EndScroll(object sender, EventArgs e)
        {
            UpdateDisplayRange();
        }

        #endregion

    } // End class

} // End namespace