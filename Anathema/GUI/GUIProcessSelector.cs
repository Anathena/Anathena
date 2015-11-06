using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Concurrent;

namespace Anathema
{
    /*  TODO: Remove functionality from GUI and move to a separate class
     * In this class we fetch a process and store it in the target process passed by reference. The method of grabbing
     * processes and sorting them based on time since execution is as follows:
     * 1) Grab all available processes
     * 2) Sort them into two categories -- 'session0' (important system processes) and 'standard'.
     * It is worth noting here that we cannot access the 'time since execution' for 'session0' unless anathema is running
     * as admin. Trying to access the time results in errors and creates noticable overhead with a try/catch statement,
     * thus we have to sort them into the two formerly mentioned categories in advanced, and only fetch icons
     * for those in the 'standard' category.
     * 3) Sort the 'standard' list based on time since execution, and the 'session0' list based on processID
     * 4) Merge lists into one list, placing the 'standard' before the 'session0'.
     * 5) Loop over the 'standard' portion of the list, fetching icons.
     * Here it is also worth noting that there are issues trying to access an icon of a 64-bit process from a 32-bit
     * version of A. If we are 64-bit, we call a function that doesn't have to worry about this stuff. If we are
     * 32-bit, again try/catches again create too much overhead, so we use the function IsWow64Process to determine if
     * each process is compatable (also 32-bit), and if so THEN we can make a proper request.
     * 6) Update the target process in the static class TargetProcess
     * 
     * -------------------------------------------------------------------------------
     * 
     * Further implamentations:
     * - Icon fetching for session0 items (~3-5 have icons)
     */

    public partial class GUIProcessSelector : UserControl
    {
        #region Variables & Initialization
        public delegate void SelectCallBack(Process TargetProcess); // CallBack after making process selection
        private SelectCallBack CallBack;
        private List<Process> ProcessList;                   // Complete list of running processes

        public unsafe GUIProcessSelector(SelectCallBack CallBack = null)
        {
            InitializeComponent();

            // Set non standard process list view properties
            ProcessListView.Columns.Add("     Processes"); // TODO: I don't like this
            ProcessListView.View = View.Details;
            
            this.CallBack = CallBack;
            GatherProcessInfo();
            UpdateResize();
        }
        
        public void UpdateGUI()
        {

        }

        #endregion

        #region Methods

        private void UpdateResize()
        {
            ProcessListView.Columns[0].Width = ProcessListView.Width - 24;
        }

        private void GatherProcessInfo()
        {
            // Update preparation
            ProcessListView.SuspendLayout();
            ProcessListView.BeginUpdate();
            ProcessListView.Items.Clear();

            // Grab all processes
            ProcessList = FetchAllProcesses(Process.GetProcesses().ToList());

            // Clean up
            ProcessListView.ResumeLayout();
            ProcessListView.EndUpdate();
        }

        // Determines if Anathema is able to perform certain actions on the target process, such as fetching icons
        public static bool IsProcessOSCompatable(IntPtr ProcessHandle)
        {
            if (OSInterface.IsAnthema64Bit())
                return true;

            if (OSInterface.IsProcess64Bit(ProcessHandle))
                return true;

            // Target uses higher addressing than Anathema, thus Anathema is not compatable
            return false;
        }

        // Determines if a process is a session0 or not and adds it to the appropriate list
        private List<Process> FetchAllProcesses(List<Process> UnsortedProcesses)
        {
            ConcurrentBag<Process> SystemProcessBag = new ConcurrentBag<Process>(); // Important system processes
            ConcurrentBag<Process> StandardProcessBag = new ConcurrentBag<Process>(); // Generic processes

            // Fetch processes in parallel
            //Parallel.For(0, UnsortedProcesses.Count, ProcessIndex =>
            for (int ProcessIndex = 0; ProcessIndex < UnsortedProcesses.Count; ProcessIndex++)
            {
                // Guarenteed session0, but misses some things
                if (UnsortedProcesses[ProcessIndex].SessionId == 0) // NO possible access violation
                    SystemProcessBag.Add(UnsortedProcesses[ProcessIndex]);

                // This seems to grab only system processes. This is semi-incorrect since
                // that doesn't have to be true, but it seems to always be the case.
                else if (UnsortedProcesses[ProcessIndex].BasePriority == 13) // NO possible access violation
                {
                    SystemProcessBag.Add(UnsortedProcesses[ProcessIndex]);
                }
                else
                {
                    try
                    {
                        // This boolean will throw an error if it is a Session0 process
                        if (UnsortedProcesses[ProcessIndex].PriorityBoostEnabled) // POSSIBLE access violation
                            StandardProcessBag.Add(UnsortedProcesses[ProcessIndex]);
                    }
                    catch (Win32Exception) // Session0 access denied error
                    {
                        SystemProcessBag.Add(UnsortedProcesses[ProcessIndex]); // Collect any missed session0 targets
                    }
                    catch (InvalidOperationException)
                    {

                    }
                }
            }//);

            // Convert concurrent bags to lists
            List<Process> SystemProcessList = new List<Process>(SystemProcessBag);
            List<Process> StandardProcessList = new List<Process>(StandardProcessBag);

            // Sort the lists
            StandardProcessList.Sort(ProcessTimeComparer.Default);  // Sort our standard list based on execution time
            SystemProcessList.Sort(ProcessIDComparer.Default);      // Sort session0 items by ID

            // Combine the lists
            List<Process> ProcessList = new List<Process>();
            ProcessList.AddRange(StandardProcessList); // Start by copying all standard processes first
            ProcessList.AddRange(SystemProcessList);   // Copy in session0 / system processes last

            // Update the list view with the found processes
            WriteProcessesToList(ProcessList);

            // Grab icons for the just the standard processes
            FetchIcons(StandardProcessList);

            return ProcessList;
        }

        private void WriteProcessesToList(List<Process> ProcessList)
        {
            // Add all processes to list
            for (int Index = 0; Index < ProcessList.Count; Index++)
            {
                AddProcessToList(ProcessList[Index].ProcessName, ProcessList[Index].MainWindowTitle, ProcessList[Index].Id);
            }

        }

        // Process icon fetching optimized for AE 32-bit on a 64-bit OS
        private void FetchIcons(List<Process> ProcessList)
        {
            ImageList ImageList = new ImageList();  // Icons to correspond to our processes
            Icon GrabbedIcon = null;                // Current icon being added
            Int32 ImageCount = 0;

            // Try to grab icons for the main list only
            for (Int32 ProcessIndex = 0; ProcessIndex < ProcessList.Count; ProcessIndex++)
            {
                // Try to grab icon
                GrabbedIcon = GetIcon(ProcessList[ProcessIndex]);

                if (GrabbedIcon == null)
                    continue;

                ImageList.Images.Add(GrabbedIcon);
                ProcessListView.Items[ProcessIndex].ImageIndex = ImageCount++;
            }

            // Apply our image list
            ProcessListView.SmallImageList = ImageList;
        }

        private void AddProcessToList(String ProcessName, String MainWindowTitle, Int32 Id)
        {
            String ProcessString = "";

            if (MainWindowTitle != "")
            {
                // Include title window name
                ProcessString = Conversions.ToAddress(Convert.ToString(Id)) + " - " + ProcessName + " - (" + MainWindowTitle + ")";
            }
            else
            {
                // No name, just add process name
                ProcessString = Conversions.ToAddress(Convert.ToString(Id)) + " - " + ProcessName;
            }

            ProcessListView.Items.Add(ProcessString);
        }

        // Applies a selection made either via
        private void MakeSelection()
        {
            try
            {
                if (CallBack == null || ProcessListView.SelectedIndices.Count <= 0)
                    return;

                // Send the target process to the caller
                CallBack(ProcessList[ProcessListView.SelectedIndices[0]]);
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message, "Error making selection.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private Icon GetIcon(Process TargetProcess)
        {
            try
            {
                // 32-bit OS grabbing 64-bit icons isn't allowed, so we check
                if (!IsProcessOSCompatable(TargetProcess.Handle))
                    return null;

                IntPtr IconHandle = ExtractIcon(this.Handle, TargetProcess.MainModule.FileName, 0);

                if (!IconHandle.Equals(IntPtr.Zero))
                    return (Icon.FromHandle(IconHandle));
            }
            catch { }

            return null;
        }

        #endregion

        #region Events

        private void GUIProcessSelector_Load(object sender, EventArgs e)
        {

        }

        private void GUIProcessSelector_Resize(object sender, EventArgs e)
        {
            UpdateResize();
        }

        private void RefreshButton_Click(object sender, EventArgs e)
        {
            // Refresh via refresh button
            GatherProcessInfo();
        }

        private void AcceptProcessButton_Click(object sender, EventArgs e)
        {
            // Make selection via accept button
            MakeSelection();
        }

        private void ProcessListView_DoubleClick(object sender, EventArgs e)
        {
            // Make selection via double click
            MakeSelection();
        }

        private void OpenProcessToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Make selection via right click menu
            MakeSelection();
        }

        private void RefreshToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Refresh via right click menu
            GatherProcessInfo();
        }

        private void CloseProcessSelect_Click(object sender, EventArgs e)
        {
            // Close via close button
            CallBack(null);
        }

        [DllImport("shell32.dll", SetLastError = true)]
        public static extern IntPtr ExtractIcon(IntPtr hInst, string lpszExeFileName, int nIconIndex);

        #endregion

        
    }

    #region Process comparer classes

    // Class that can sort processes by time since execution
    class ProcessTimeComparer : IComparer<Process>
    {
        public static readonly ProcessTimeComparer Default = new ProcessTimeComparer();
        public ProcessTimeComparer() { }

        public int Compare(Process ProcessA, Process ProcessB)
        {
            return DateTime.Compare(ProcessB.StartTime, ProcessA.StartTime);
        }
    }

    // Class that can sort processes by ID
    class ProcessIDComparer : IComparer<Process>
    {
        public static readonly ProcessIDComparer Default = new ProcessIDComparer();
        public ProcessIDComparer() { }

        public int Compare(Process ProcessA, Process ProcessB)
        {
            if (ProcessA.Id < ProcessB.Id)
                return 1;
            else if (ProcessA.Id > ProcessB.Id)
                return -1;
            else
                return 0;
        }
    }

    #endregion
}
