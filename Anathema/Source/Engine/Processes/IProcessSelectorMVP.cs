using Anathema.Source.Utils.MVP;
using Anathema.Source.Utils.Validation;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace Anathema.Source.Engine.Processes
{
    delegate void ProcessSelectorEventHandler(Object Sender, ProcessSelectorEventArgs Args);
    class ProcessSelectorEventArgs : EventArgs
    {
        public List<Process> ProcessList = null;
        public List<Icon> ProcessIcons = null;
        public Process SelectedProcess = null;
    }

    interface IProcessSelectorView : IView
    {
        // Methods invoked by the presenter (upstream)
        void DisplayProcesses(IEnumerable<ListViewItem> Items, ImageList ImageList);
        void SelectProcess(Process TargetProcess);
    }

    interface IProcessSelectorModel : IModel, IProcessSubject
    {
        // Events triggered by the model (upstream)
        event ProcessSelectorEventHandler EventDisplayProcesses;
        event ProcessSelectorEventHandler EventSelectProcess;

        // Functions invoked by presenter (downstream)
        void RefreshProcesses(IntPtr ProcessSelectorHandle);
        void SelectProcess(Int32 Index);
    }

    class ProcessSelectorPresenter : Presenter<IProcessSelectorView, IProcessSelectorModel>
    {
        private new IProcessSelectorView View { get; set; }
        private new IProcessSelectorModel Model { get; set; }

        public ProcessSelectorPresenter(IProcessSelectorView View, IProcessSelectorModel Model) : base(View, Model)
        {
            this.View = View;
            this.Model = Model;

            // Bind events triggered by the model
            Model.EventDisplayProcesses += EventDisplayProcesses;
            Model.EventSelectProcess += EventSelectProcess;

            Model.OnGUIOpen();
        }

        private IEnumerable<ListViewItem> GetProcessListViewItems(List<Process> Processes, List<Icon> ProcessIcons, out ImageList ImageList)
        {
            ImageList = new ImageList();

            List<ListViewItem> ListViewItems = new List<ListViewItem>();
            Int32 ImageIndex = 0;

            for (Int32 Index = 0; Index < Math.Min(Processes.Count, ProcessIcons.Count); Index++)
            {
                ListViewItems.Add(new ListViewItem(GetProcessTitle(Processes[Index])));
                if (ProcessIcons[Index] == null)
                    continue;

                ImageList.Images.Add(ProcessIcons[Index]);

                ListViewItems[Index].ImageIndex = ImageIndex++;
            }

            return ListViewItems;
        }

        private String GetProcessTitle(Process Process)
        {
            String ProcessTitles;

            String ProcessName = Process.ProcessName;
            String MainWindowTitle = Process.MainWindowTitle;
            Int32 Id = Process.Id;

            if (MainWindowTitle != String.Empty)
            {
                // Include title window name
                ProcessTitles = Conversions.ToAddress(Id) + " - " + ProcessName + " - (" + MainWindowTitle + ")";
            }
            else
            {
                // No name, just add process name
                ProcessTitles = Conversions.ToAddress(Id) + " - " + ProcessName;
            }

            return ProcessTitles;
        }

        #region Method definitions called by the view (downstream)

        public void RefreshProcesses(IntPtr ProcessSelectorHandle)
        {
            Model.RefreshProcesses(ProcessSelectorHandle);
        }

        public void SelectProcess(Int32 Index)
        {
            Model.SelectProcess(Index);
        }

        #endregion

        #region Event definitions for events triggered by the model (upstream)
        public void EventDisplayProcesses(Object Sender, ProcessSelectorEventArgs E)
        {
            ImageList ImageList = null;
            View.DisplayProcesses(GetProcessListViewItems(E.ProcessList, E.ProcessIcons, out ImageList), ImageList);
        }

        public void EventSelectProcess(Object Sender, ProcessSelectorEventArgs E)
        {
            View.SelectProcess(E.SelectedProcess);
        }

        #endregion

    } // End class

} // End namespace