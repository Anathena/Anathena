using System;
using WeifenLuo.WinFormsUI.Docking;

namespace Ana.GUI.Tools
{
    public partial class GUICodeView : DockContent
    {
        private Object accessLock;

        public GUICodeView()
        {
            InitializeComponent();

            accessLock = new Object();
        }

        #region Events

        #endregion

    } // End class

} // End namespace