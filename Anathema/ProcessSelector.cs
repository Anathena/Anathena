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

namespace Anathema
{
    /*
     * In this class we fetch a process and store it in the target process passed by reference. The method of grabbing
     * processes and sorting them based on time since execution is as follows:
     * 1) Grab all available processes
     * 2) Sort them into two categories -- 'session0' (important system processes) and 'standard'.
     * It is worth noting here that we cannot access the 'time since execution' for 'session0' unless AE is running
     * as admin. Trying to access the time results in errors and creates noticable overhead with a try/catch statement,
     * thus we have to sort them into the two formerly mentioned categories in advanced, and only fetch icons
     * for those in the 'standard' category.
     * 3) Sort the 'standard' list based on time since execution, and the 'session0' list based on processID
     * 4) Merge lists into one list, placing the 'standard' before the 'session0'.
     * 5) Loop over the 'standard' portion of the list, fetching icons.
     * Here it is also worth noting that there are issues trying to access an icon of a 64-bit process from a 32-bit
     * version of AE. If we are 64-bit, we call a function that doesn't have to worry about this stuff. If we are
     * 32-bit, again try/catches again create too much overhead, so we use the function IsWow64Process to determine if
     * each process is compatable (also 32-bit), and if so THEN we can make a proper request.
     * 6) Update the target process in the static class TargetProcess
     * 
     * -------------------------------------------------------------------------------
     * 
     * Further implamentations:
     * - Icon fetching for session0 items (very view, ~3-5, have icons)
     * - Perhaps this class should be instantized with a ref to a process so that the code can be recycled with ease
     */

    public partial class ProcessSelector : Form
    {
        #region Variables & Initialization

        // private TargetProcessData TargetProcessData;
        private static List<Process> ProcessList; // Complete list of running processes
        private volatile static List<Process> GlobalSession0List; // System processes that we can't probe without admin
        private volatile static List<Process> GlobalStandardList; // Standard user processes that we can

        // Thread related variables
        private const int ThreadCount = 3;
        private volatile static int FinishedThreads;    // Threads done scanning
        private volatile static int esi;                // Shared index counter
        private volatile static Object CopySession0Lock = new Object(); // Lock for session0
        private volatile static Object CopyStandardLock = new Object(); // Lock for standard

        public delegate void SelectCallBack(Process TargetProcess);
        private SelectCallBack CallBack;

        public unsafe ProcessSelector(SelectCallBack CallBack = null)
        {
            InitializeComponent();
            this.CallBack = CallBack;
        }

        private void ProcessSelector_Load(object sender, EventArgs e)
        {
            SetToolTips();
            GatherProcessInfo();
        }

        private void SetToolTips()
        {
            GUIToolTip.SetToolTip(AcceptProcessButton, "Sets selected process as the target process.");
            GUIToolTip.SetToolTip(RefreshButton, "Clears list and searches again.");
            GUIToolTip.SetToolTip(CloseProcessButton, "Closes this window and leaves target process unchanged.");
        }

        #endregion

        #region Methods

        private unsafe void GatherProcessInfo()
        {
            // Update preparation
            ProcessListView.SuspendLayout();
            ProcessListView.BeginUpdate();
            ProcessListView.Items.Clear();

            // Grab all processes
            ProcessList = new List<Process>(Process.GetProcesses());
            GlobalSession0List = new List<Process>();
            GlobalStandardList = new List<Process>();

            esi = 0; // Reset cross-thread index
            FinishedThreads = 0;

            // Create threads to fetch process information
            for (int ecx = 0; ecx < ThreadCount; ecx++)
            {
                Thread QueryProcessThread;
                QueryProcessThread = new Thread(QueryProcess);
                QueryProcessThread.Priority = ThreadPriority.AboveNormal;
                QueryProcessThread.Start();
            }

            // Hold out here until threads are done
            while (FinishedThreads != ThreadCount) { }

            // Merge our 'session0' and 'standard' lists
            MergeLists();

            // Update the list with the found processes
            WriteProcessesToList();

            // Grab icons for the processes
            if (IsOSCompatabilityGuarenteed())
                FetchIconsOnCompatableOS(); // Running AE 64-bit on 64-bit machine, or AE 32-bit on 32-bit machine
            else // Aecial Engine is in 32-bit mode on a 64-bit machine, so theres a bit more work to do.
                FetchIconsOnIncompatableOS();


            // Clean up
            ProcessListView.ResumeLayout();
            ProcessListView.EndUpdate();

            GlobalStandardList.Clear();
            GlobalSession0List.Clear();
        }

        /// <summary>
        /// Determines if all processes are guarenteed to be compatable
        /// </summary>
        /// <returns></returns>
        public static bool IsOSCompatabilityGuarenteed()
        {
            // If Anathema is 64 bit compatability with a target process is guarenteed
            if (OSInterface.IsAnthema64Bit())
                return true;

            // If the OS is 32 bit, all processes are 32 bit and compatability is guarenteed
            if (!OSInterface.IsOS64Bit())
                return true;

            return false;
        }

        /*
        /// <summary>
        /// Determines if Anathema is able to perform certain actions on the target process, such as fetching icons
        /// </summary>
        /// <param name="ProcessHandle"></param>
        /// <returns></returns>
        public static bool IsProcessOSCompatable(IntPtr ProcessHandle)
        {
            // If the OS is 32 bit all processes are compatable
            if (!OSInterface.IsOS64Bit())
                return true;

            // If Anathema is 64 bit then it is compatible with 32 and 64 bit targets
            if (OSInterface.IsAnthema64Bit())
                return true;

            // Finally if the two are equals (ie OS is 64 bit, Anathema is 32 bit, and target is 32 bit)
            if (OSInterface.IsProcess64Bit(ProcessHandle) == OSInterface.IsAnthema64Bit())
                return true;

            // Target uses higher addressing than Anathema, thus Anathema is not compatable
            return false;
        }
        */

        /// <summary>
        /// Determines if Anathema is able to perform certain actions on the target process, such as fetching icons
        /// </summary>
        /// <param name="ProcessHandle"></param>
        /// <returns></returns>
        public static bool IsProcessOSCompatable(IntPtr ProcessHandle)
        {
            // Check if this process and the target process have the same addressing
            if (OSInterface.IsProcess64Bit(ProcessHandle) == OSInterface.IsAnthema64Bit())
                return true;

            // Target uses higher addressing than Anathema, thus Anathema is not compatable
            return false;
        }

        // Determines if a process is a session0 or not and adds it to the appropriate list
        private unsafe void QueryProcess()
        {
            // Returns these:
            List<Process> ThreadSession0List = new List<Process>(); // Important system processes
            List<Process> ThreadStandardList = new List<Process>(); // Generic processes

            int ecx; // Current index for this thread

            // PROCESS FETCHING
            while (true)
            {
                ecx = esi++;                    // Grab next process index
                if (ecx >= ProcessList.Count)   // Exit if out of range
                    break;

                // Guarenteed session0, but misses some things
                if (ProcessList[ecx].SessionId == 0) // NO possible access violation
                    ThreadSession0List.Add(ProcessList[ecx]);

                // This seems to grab only system processes. This is semi-incorrect since
                // that doesn't have to be true, but it seems to always be the case.
                else if (ProcessList[ecx].BasePriority == 13) //NO possible access violation
                    ThreadSession0List.Add(ProcessList[ecx]);
                else
                    try
                    {
                        // This boolean will throw an error if it is a Session0 process
                        if (ProcessList[ecx].PriorityBoostEnabled) // POSSIBLE access violation
                            ThreadStandardList.Add(ProcessList[ecx]);
                    }
                    catch (Win32Exception) // Session0 access denied error
                    {
                        ThreadSession0List.Add(ProcessList[ecx]); // Collect any missed session0 targets
                    }
            }

            // Copy in thread lists to global list
            lock (CopySession0Lock)
            {
                GlobalSession0List.AddRange(ThreadSession0List);
            }
            lock (CopyStandardLock)
            {
                GlobalStandardList.AddRange(ThreadStandardList);
            }

            FinishedThreads++;
        }

        private void MergeLists()
        {
            // Sort standard & session0 lists
            GlobalStandardList.Sort(ProcessTimeComparer.Default); // Sort our standard list based on execution time
            GlobalSession0List.Sort(ProcessIDComparer.Default);   // Sort session0 items by ID

            // Consolidate them into our process list
            ProcessList.Clear();
            ProcessList.AddRange(GlobalStandardList); // Start by copying all Standard items first
            ProcessList.AddRange(GlobalSession0List); // Copy in session0/system items last
        }

        private void WriteProcessesToList()
        {
            // Add all processes to list
            for (int ecx = 0; ecx < ProcessList.Count; ecx++)
            {
                AddProcessToList(ProcessList[ecx].ProcessName,
                  ProcessList[ecx].MainWindowTitle, ProcessList[ecx].Id);
            }

        }

        // Process icon fetching optimized for AE 32-bit on a 64-bit OS
        private void FetchIconsOnIncompatableOS()
        {
            ImageList ImageList = new ImageList(); // Icons to correspond to our processes
            Icon GrabbedIcon = null;    // Current icon being added
            int ImageCount = 0;

            // Try to grab icons for the main list only
            for (int ecx = 0; ecx < GlobalStandardList.Count; ecx++)
            {
                // 32-bit OS grabbing 64-bit icons isn't allowed, so we check
                if (!IsProcessOSCompatable(GlobalStandardList[ecx].Handle))
                    continue;

                // Try to grab icon
                GrabbedIcon = GetIcon(GlobalStandardList[ecx].MainModule.FileName, 0);

                if (GrabbedIcon != null)
                {
                    ImageList.Images.Add(GrabbedIcon);
                    ProcessListView.Items[ecx].ImageIndex = ImageCount++;
                }
            }

            // Apply our image list
            ProcessListView.SmallImageList = ImageList;
        }

        // Process icon fetching optimized for AE 64-bit on a 64-bit machine, and AE 32-bit on a 32-bit machine
        private void FetchIconsOnCompatableOS()
        {
            ImageList ImageList = new ImageList(); // Icons to correspond to our processes
            Icon GrabbedIcon = null;    // Current icon being added
            int ImageCount = 0;

            // Try to grab icons for the main list only
            for (int ecx = 0; ecx < GlobalStandardList.Count; ecx++)
            {
                GrabbedIcon = GetIcon(GlobalStandardList[ecx].MainModule.FileName, 0);

                if (GrabbedIcon != null)
                {
                    ImageList.Images.Add(GrabbedIcon);
                    ProcessListView.Items[ecx].ImageIndex = ImageCount++;
                }
            }

            // Apply our image list
            ProcessListView.SmallImageList = ImageList;
        }

        private void AddProcessToList(string ProcessName, string MainWindowTitle, int Id)
        {
            if (MainWindowTitle != "")
            {
                // Include title window name
                ProcessListView.Items.Add(Conversions.ToAddress(Convert.ToString(Id)) + " - " + ProcessName + " - (" + MainWindowTitle + ")");
            }
            else
            {
                // No name, just add process name
                ProcessListView.Items.Add(Conversions.ToAddress(Convert.ToString(Id)) + " - " + ProcessName);
            }
        }

        // Applies a selection made either via
        private void MakeSelection()
        {
            try
            {
                // Call the call back function, giving the target process
                if (CallBack != null)
                    CallBack(ProcessList[ProcessListView.SelectedIndices[0]]);

                this.Close();
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message, "Error gathering all process data.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private Icon GetIcon(string FileName, int IconIndex)
        {
            try
            {
                IntPtr hc = Icons.ExtractIcon(this.Handle, FileName, IconIndex);
                if (!hc.Equals(IntPtr.Zero))
                    return (Icon.FromHandle(hc));
            }
            catch { }

            return null;
        }

        #endregion

        #region Events

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
            this.Close();
        }

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

    // Useful p/invokes and structures for icon grabbing. Ditched most of them since few are efficient.
    class Icons
    {
        // P/Invokes to allow extraction of Icons from processes/files
        [DllImport("shell32.dll", SetLastError = true)]
        public static extern IntPtr ExtractIcon(IntPtr hInst, string lpszExeFileName, int nIconIndex);

        //[DllImport("user32.dll", EntryPoint = "DestroyIcon", SetLastError = true)]
        //public static extern int DestroyIcon(IntPtr hIcon);
    }
}
 