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
    public partial class GUIAssembler : DockContent, IDebuggerView
    {
        private DebuggerPresenter DebuggerPresenter;

        public GUIAssembler()
        {
            InitializeComponent();

            DebuggerPresenter = new DebuggerPresenter(this, new Debugger());
        }

        public void DisableDebugger()
        {
            throw new NotImplementedException();
        }

        public void EnableDebugger()
        {
            throw new NotImplementedException();
        }

        public void ReadValues()
        {
            throw new NotImplementedException();
        }

        public void UpdateItemCount(Int32 ItemCount)
        {
            throw new NotImplementedException();
        }

        public void UpdateMemorySizeLabel(String MemorySize, String ItemCount)
        {
            throw new NotImplementedException();
        }

        #region Events

        #endregion

    } // End class

} // End namespace