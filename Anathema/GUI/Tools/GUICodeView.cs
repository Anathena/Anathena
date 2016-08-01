using System;
using WeifenLuo.WinFormsUI.Docking;

namespace Anathema.GUI.Tools
{
    public partial class GUICodeView : DockContent
    {
        //private DebuggerPresenter DebuggerPresenter;
        private Object AccessLock;

        public GUICodeView()
        {
            InitializeComponent();

            //DebuggerPresenter = new DebuggerPresenter(this, new Debugger());
            AccessLock = new Object();
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