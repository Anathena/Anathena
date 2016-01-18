using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;
using System.IO;
using Anathema.Properties;

namespace Anathema
{
    public partial class GUIFiniteStateScanner : DockContent, IFiniteStateScannerView
    {
        private FiniteStateScannerPresenter FiniteStateScannerPresenter;
        
        private FiniteStateMachine FiniteStateMachine;
        
        public GUIFiniteStateScanner()
        {
            InitializeComponent();

            FiniteStateScannerPresenter = new FiniteStateScannerPresenter(this, new FiniteStateScanner());
            
            EnableGUI();
        }
        
        public void ScanFinished()
        {
            EnableGUI();
        }

        

        private void DisableGUI()
        {
            StartScanButton.Enabled = false;
            StopScanButton.Enabled = true;
        }

        private void EnableGUI()
        {
            StartScanButton.Enabled = true;
            StopScanButton.Enabled = false;
        }

        #region Events

       
        
        private void StartScanButton_Click(Object Sender, EventArgs E)
        {
            FiniteStateScannerPresenter.BeginScan();
            DisableGUI();
        }

        private void StopScanButton_Click(object sender, EventArgs e)
        {
            FiniteStateScannerPresenter.EndScan();
            EnableGUI();
        }

        public void UpdateDisplay(FiniteStateMachine FiniteStateMachine, FiniteState MousedOverState, Point[] SelectionLine)
        {
            throw new NotImplementedException();
        }


        #endregion

    } // End class

} // End namespace