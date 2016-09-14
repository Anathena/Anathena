using Anathena.Source.MemoryView;
using Anathena.Source.Utils;
using Anathena.Source.Utils.Extensions;
using Anathena.Source.Utils.MVP;
using Be.Windows.Forms;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace Anathena.GUI.Tools
{
    public partial class GUIMemoryView : DockContent, IMemoryViewView
    {
        private MemoryViewPresenter memoryViewPresenter;
        private Object accessLock;

        private const Int32 maxHorizontalBytes = 64;
        private const Int32 hexBoxChunkSize = 2;
        private Int64 baseLine;

        public GUIMemoryView()
        {
            InitializeComponent();

            memoryViewPresenter = new MemoryViewPresenter(this, new MemoryView());
            accessLock = new Object();

            memoryViewPresenter.RefreshVirtualPages();

            InitializeHexBox();
            UpdateHexBoxChunks();
            UpdateDisplayRange();
        }

        private void InitializeHexBox()
        {
            ControlThreadingHelper.InvokeControlAction<Control>(HexEditorBox, () =>
            {
                using (TimedLock.Lock(accessLock))
                {
                    HexEditorBox.ByteProvider = memoryViewPresenter;
                    HexEditorBox.ByteCharConverter = new DefaultByteCharConverter();
                    HexEditorBox.LineInfoOffset = 0;
                    HexEditorBox.UseFixedBytesPerLine = true;
                    HexEditorBox.Select(HexEditorBox.ByteProvider.Length / 2, 1);
                    baseLine = HexEditorBox.CurrentLine;

                    memoryViewPresenter.QuickNavigate(QuickNavComboBox.SelectedIndex);
                }
            });
        }

        public void ReadValues()
        {
            ControlThreadingHelper.InvokeControlAction<Control>(HexEditorBox, () =>
            {
                using (TimedLock.Lock(accessLock))
                {
                    HexEditorBox.Invalidate();
                }
            });
        }

        public void GoToAddress(IntPtr Address)
        {
            ControlThreadingHelper.InvokeControlAction<Control>(HexEditorBox, () =>
            {
                using (TimedLock.Lock(accessLock))
                {
                    if (HexEditorBox.ByteProvider == null)
                        return;

                    HexEditorBox.LineInfoOffset = (Address.Subtract(HexEditorBox.ByteProvider.Length / 2).Subtract(Address.Mod(HexEditorBox.HorizontalByteCount)).ToInt64());
                    baseLine = HexEditorBox.TopLine;
                }
            });

            UpdateDisplayRange();
        }

        public void UpdateVirtualPages(IEnumerable<String> virtualPages)
        {
            ControlThreadingHelper.InvokeControlAction<Control>(GUIToolStrip, () =>
            {
                using (TimedLock.Lock(accessLock))
                {
                    QuickNavComboBox.Items.Clear();
                    virtualPages?.ForEach(X => QuickNavComboBox.Items.Add(X));

                    if (QuickNavComboBox.Items.Count > 0)
                        QuickNavComboBox.SelectedIndex = 0;
                }
            });
        }

        private void UpdateDisplayRange()
        {
            ControlThreadingHelper.InvokeControlAction<Control>(GUIToolStrip, () =>
            {
                using (TimedLock.Lock(accessLock))
                {
                    HexEditorBox.LineInfoOffset = unchecked(HexEditorBox.LineInfoOffset + (HexEditorBox.TopLine - baseLine) * HexEditorBox.HorizontalByteCount);
                    HexEditorBox.ScrollByteIntoCenter(HexEditorBox.ByteProvider.Length / 2);

                    baseLine = HexEditorBox.TopLine;

                    memoryViewPresenter.UpdateBaseAddress(HexEditorBox.LineInfoOffset.ToIntPtr());
                    memoryViewPresenter.UpdateStartReadAddress(HexEditorBox.LineInfoOffset.ToIntPtr().Add(HexEditorBox.TopIndex));
                }
            });
        }

        private void UpdateHexBoxChunks()
        {
            ControlThreadingHelper.InvokeControlAction<Control>(GUIToolStrip, () =>
            {
                using (TimedLock.Lock(accessLock))
                {
                    memoryViewPresenter.UpdateReadLength(HexEditorBox.HorizontalByteCount * HexEditorBox.VerticalByteCount);

                    // Assume the maximum number of bytes we can display
                    HexEditorBox.BytesPerLine = maxHorizontalBytes;

                    // Decrease this value iteratively until we can fit the content on the screen
                    while (HexEditorBox.Width < HexEditorBox.RequiredWidth)
                    {
                        if (HexEditorBox.BytesPerLine <= hexBoxChunkSize)
                            break;
                        HexEditorBox.BytesPerLine -= hexBoxChunkSize;
                    }
                }
            });
        }

        #region Events

        private void RefreshNavigationButton_Click(Object sender, EventArgs e)
        {
            memoryViewPresenter.RefreshVirtualPages();
        }

        private void QuickNavComboBox_SelectedIndexChanged(Object sender, EventArgs e)
        {
            memoryViewPresenter.QuickNavigate(QuickNavComboBox.SelectedIndex);
        }

        private void HexEditorBox_Resize(Object sender, EventArgs e)
        {
            UpdateHexBoxChunks();
        }

        private void HexEditorBox_EndScroll(Object sender, EventArgs e)
        {
            UpdateDisplayRange();
        }

        #endregion

    } // End class

} // End namespace