using Anathema.Source.MemoryView;
using Anathema.Source.Utils;
using Anathema.Source.Utils.Extensions;
using Anathema.Source.Utils.MVP;
using Be.Windows.Forms;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace Anathema.GUI
{
    public partial class GUIMemoryView : DockContent, IMemoryViewView
    {
        private MemoryViewPresenter MemoryViewPresenter;
        private Object AccessLock;

        private const Int32 MaxHorizontalBytes = 64;
        private const Int32 HexBoxChunkSize = 2;
        private Int64 BaseLine;

        public GUIMemoryView()
        {
            InitializeComponent();

            MemoryViewPresenter = new MemoryViewPresenter(this, new MemoryView());
            AccessLock = new Object();

            MemoryViewPresenter.RefreshVirtualPages();

            InitializeHexBox();
            UpdateHexBoxChunks();
            UpdateDisplayRange();
        }

        private void InitializeHexBox()
        {
            ControlThreadingHelper.InvokeControlAction<Control>(HexEditorBox, () =>
            {
                using (TimedLock.Lock(AccessLock))
                {
                    HexEditorBox.ByteProvider = MemoryViewPresenter;
                    HexEditorBox.ByteCharConverter = new DefaultByteCharConverter();
                    HexEditorBox.LineInfoOffset = 0;
                    HexEditorBox.UseFixedBytesPerLine = true;
                    HexEditorBox.Select(HexEditorBox.ByteProvider.Length / 2, 1);
                    BaseLine = HexEditorBox.CurrentLine;

                    MemoryViewPresenter.QuickNavigate(QuickNavComboBox.SelectedIndex);
                }
            });
        }

        public void ReadValues()
        {
            ControlThreadingHelper.InvokeControlAction<Control>(HexEditorBox, () =>
            {
                using (TimedLock.Lock(AccessLock))
                {
                    HexEditorBox.Invalidate();
                }
            });
        }

        public void GoToAddress(IntPtr Address)
        {
            ControlThreadingHelper.InvokeControlAction<Control>(HexEditorBox, () =>
            {
                using (TimedLock.Lock(AccessLock))
                {
                    if (HexEditorBox.ByteProvider == null)
                        return;

                    HexEditorBox.LineInfoOffset = (Address.Subtract(HexEditorBox.ByteProvider.Length / 2).Subtract(Address.Mod(HexEditorBox.HorizontalByteCount)).ToInt64());
                    BaseLine = HexEditorBox.TopLine;
                }
            });

            UpdateDisplayRange();
        }

        public void UpdateVirtualPages(IEnumerable<String> VirtualPages)
        {
            ControlThreadingHelper.InvokeControlAction<Control>(GUIToolStrip, () =>
            {
                using (TimedLock.Lock(AccessLock))
                {
                    QuickNavComboBox.Items.Clear();
                    VirtualPages?.ForEach(X => QuickNavComboBox.Items.Add(X));

                    if (QuickNavComboBox.Items.Count > 0)
                        QuickNavComboBox.SelectedIndex = 0;
                }
            });
        }

        private void UpdateDisplayRange()
        {
            ControlThreadingHelper.InvokeControlAction<Control>(GUIToolStrip, () =>
            {
                using (TimedLock.Lock(AccessLock))
                {
                    HexEditorBox.LineInfoOffset = unchecked(HexEditorBox.LineInfoOffset + (HexEditorBox.TopLine - BaseLine) * HexEditorBox.HorizontalByteCount);
                    HexEditorBox.ScrollByteIntoCenter(HexEditorBox.ByteProvider.Length / 2);

                    BaseLine = HexEditorBox.TopLine;

                    MemoryViewPresenter.UpdateBaseAddress(HexEditorBox.LineInfoOffset.ToIntPtr());
                    MemoryViewPresenter.UpdateStartReadAddress(HexEditorBox.LineInfoOffset.ToIntPtr().Add(HexEditorBox.TopIndex));
                }
            });
        }

        private void UpdateHexBoxChunks()
        {
            ControlThreadingHelper.InvokeControlAction<Control>(GUIToolStrip, () =>
            {
                using (TimedLock.Lock(AccessLock))
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
            });
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