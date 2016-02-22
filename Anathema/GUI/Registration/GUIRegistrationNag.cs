using System;
using System.Windows.Forms;

namespace Anathema
{
    public partial class GUIRegistrationNag : Form, ISettingsView
    {
        private SettingsPresenter SettingsPresenter;

        public GUIRegistrationNag()
        {
            InitializeComponent();
        }

        #region Events

        private void AcceptButton_Click(Object Sender, EventArgs E)
        {

        }

        private void ScanUserModeRadioButton_CheckedChanged(Object Sender, EventArgs E)
        {
        }

        private void ScanCustomRangeRadioButton_CheckedChanged(Object Sender, EventArgs E)
        {
        }

        private void RequiredWriteCheckBox_CheckedChanged(Object Sender, EventArgs E)
        { 

        }

        private void RequiredExecuteCheckBox_CheckedChanged(Object Sender, EventArgs E)
        {

        }

        private void RequiredCopyOnWriteCheckBox_CheckedChanged(Object Sender, EventArgs E)
        {

        }

        private void ExcludedWriteCheckBox_CheckedChanged(Object Sender, EventArgs E)
        {

        }

        private void ExcludedExecuteCheckBox_CheckedChanged(Object Sender, EventArgs E)
        {

        }

        private void ExcludedCopyOnWriteCheckBox_CheckedChanged(Object Sender, EventArgs E)
        {

        }

        #endregion

    } // End class

} // End namespace