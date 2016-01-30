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

            HexEditorBox.ByteProvider = new TestByteProvider();
            HexEditorBox.ByteCharConverter = new DefaultByteCharConverter();
            HexEditorBox.LineInfoOffset = 50;

            MemoryViewPresenter = new MemoryViewPresenter(this, new MemoryView());
        }

        public void DisableResults()
        {
            throw new NotImplementedException();
        }

        public void EnableResults()
        {
            throw new NotImplementedException();
        }

        public void ReadValues()
        {
            throw new NotImplementedException();
        }

        public void UpdateItemCount(int ItemCount)
        {
            throw new NotImplementedException();
        }

        public void UpdateMemorySizeLabel(string MemorySize, string ItemCount)
        {
            throw new NotImplementedException();
        }

        #region Events

        #endregion

    } // End class

} // End namespace