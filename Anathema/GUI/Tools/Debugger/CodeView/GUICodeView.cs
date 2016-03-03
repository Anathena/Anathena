using System;
using WeifenLuo.WinFormsUI.Docking;

namespace Anathema.GUI
{
    public partial class GUICodeView : DockContent
    {
        //private DebuggerPresenter DebuggerPresenter;

        public GUICodeView()
        {
            InitializeComponent();

            //DebuggerPresenter = new DebuggerPresenter(this, new Debugger());
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