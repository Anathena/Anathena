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
    public partial class GUIDebugger : Form, IDebuggerView
    {
        private static readonly String ConfigFile = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "DebuggerLayout.config");

        // VIEW MENU ITEMS
        private GUIAssembler GUIAssembler;
        private GUIMemoryViewer GUIMemoryViewer;

        private DeserializeDockContent DockContentDeserializer;

        private DebuggerPresenter DebuggerPresenter;

        public GUIDebugger()
        {
            InitializeComponent();

            DebuggerPresenter = new DebuggerPresenter(this, new Debugger());

            // Update theme so that everything looks cool
            this.ContentPanel.Theme = new VS2013BlueTheme();

            // Set default dock space sizes
            ContentPanel.DockRightPortion = 0.4;
            ContentPanel.DockBottomPortion = 0.5;

            // Initialize tools
            CreateTools();
        }

        private void SaveConfiguration()
        {
            return; // DISABLED FOR NOW
            ContentPanel.SaveAsXml(ConfigFile);
        }

        private void CreateTools()
        {
            if (false && File.Exists(ConfigFile))
            {
                try
                {
                    // DISABLED FOR NOW
                    ContentPanel.LoadFromXml(ConfigFile, DockContentDeserializer);
                }
                catch
                {
                    CreateDefaultTools();
                }
            }
            else
            {
                CreateDefaultTools();
            }
        }

        private void CreateDefaultTools()
        {
            CreateAssembler();
            CreateMemoryViewer();
        }

        private void CreateAssembler()
        {
            if (GUIAssembler == null || GUIAssembler.IsDisposed)
                GUIAssembler = new GUIAssembler();
            GUIAssembler.Show(ContentPanel);
        }

        private void CreateMemoryViewer()
        {
            if (GUIMemoryViewer == null || GUIMemoryViewer.IsDisposed)
                GUIMemoryViewer = new GUIMemoryViewer();
            GUIMemoryViewer.Show(ContentPanel, DockState.DockBottom);
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