namespace Anna.Source.Engine.OperatingSystems.Windows.Processes
(*
    * In this class we fetch a process and store it in the target process passed by reference. The method of grabbing
    * processes and sorting them based on time since execution is as follows:
    * 1) Grab all available processes
    * 2) Sort them into two categories -- 'session0' (important system processes) and 'standard'.
    * It is worth noting here that we cannot access the 'time since execution' for 'session0' unless anathena is running
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
*)

open System
open System.Collections
open System.Diagnostics
open System.Drawing
open Anna.Source.Engine.OperatingSystems
open Anna.Source.Engine.OperatingSystems.Windows

type ProcessCollector() = 
    // Retrieves all running processes
    member this.GetProcesses() =
        Process.GetProcesses() |>
        Seq.map(fun (externalProcess) ->
        {
            processId= externalProcess.Id
            processName= externalProcess.ProcessName
            icon = this.GetIcon(externalProcess)
        })

    // Fetches the icon associated with the provided process
    member this.GetIcon(externalProcess: Process) = 
        let noIcon: Icon = null
        // TODO: Check process bitness compatability
        let isSystemProcess = (externalProcess.SessionId = 0 || externalProcess.BasePriority = 13)
        if isSystemProcess then noIcon
        else
            try
                // Accessing this field will cause an access exception for system processes. This saves
                // time because handling the exception is faster than failing to fetch the icon later
                ignore externalProcess.PriorityBoostEnabled

                let iconHandle = Native.ExtractIcon(externalProcess.Handle, externalProcess.MainModule.FileName, 0)
                if (iconHandle.Equals(IntPtr.Zero)) then noIcon else Icon.FromHandle(iconHandle)
            with
                | _ -> noIcon
(*
class ProcessSelector : IProcessSelectorModel
{
    private ProcessSelector();
    public void OnGUIOpen() { }
    public static ProcessSelector GetInstance();
    public void Subscribe(IProcessObserver Observer);
    public void Unsubscribe(IProcessObserver Observer);
    public void Notify(Process Process = null);
    public void SelectProcess(Int32 Index);
    public void RefreshProcesses(IntPtr ProcessSelectorHandle);

    // Determines if Anathena is able to perform certain actions on the target process, such as fetching icons
    public Boolean IsProcessOSCompatable(Process Process)
    {
        // Always compatable if anathena is 64 bit
        if (EngineCore.Memory.IsAnathena64Bit())
            return true;

        // Always compatable if the target is 32 bit
        if (EngineCore.Memory.IsProcess32Bit(Process))
            return true;

        // Target uses higher addressing than Anathena, thus Anathena is not compatable
        return false;
    }

    // Determines if a process is a session0 or not and adds it to the appropriate list
    private List<Process> FetchAllProcesses(out List<Process> StandardProcessList)
    {
        List<Process> UnsortedProcesses = new List<Process>(Process.GetProcesses());
        ConcurrentBag<Process> SystemProcessBag = new ConcurrentBag<Process>(); // Important system processes
        ConcurrentBag<Process> StandardProcessBag = new ConcurrentBag<Process>(); // Generic processes

        // Fetch processes in parallel
        Parallel.ForEach(UnsortedProcesses, (Process) =>
        {
            // Guarenteed session0, but misses some things
            if (Process.SessionId == 0) // NO possible access violation
                SystemProcessBag.Add(Process);

            // This seems to grab only system processes. This is semi-incorrect since
            // that doesn't have to be true, but it seems to always be the case.
            else if (Process.BasePriority == 13) // NO possible access violation
            {
                SystemProcessBag.Add(Process);
            }
            else
            {
                try
                {
                    // This boolean will throw an error if it is a Session0 process
                    if (Process.PriorityBoostEnabled) // POSSIBLE access violation
                        StandardProcessBag.Add(Process);
                }
                catch (Win32Exception) // Session0 access denied error
                {
                    SystemProcessBag.Add(Process); // Collect any missed session0 targets
                }
                catch (InvalidOperationException)
                {

                }
            }
        });

        // Convert concurrent bags to lists
        List<Process> SystemProcessList = new List<Process>(SystemProcessBag);
        StandardProcessList = new List<Process>(StandardProcessBag);

        // Sort the lists
        StandardProcessList.Sort(ProcessTimeComparer.Default);  // Sort our standard list based on execution time
        SystemProcessList.Sort(ProcessIDComparer.Default);      // Sort session0 items by ID

        // Combine the lists
        List<Process> ProcessList = new List<Process>();
        ProcessList.AddRange(StandardProcessList); // Start by copying all standard processes first
        ProcessList.AddRange(SystemProcessList);   // Copy in session0 / system processes last

        return ProcessList;
    }

    // Process icon fetching optimized for AE 32-bit on a 64-bit OS
    List<Icon> FetchIcons(IntPtr ProcessSelectorHandle, List<Process> ProcessList)
    {
        // Icons to correspond to our processes
        List<Icon> ImageList = new List<Icon>();

        // Try to grab icons for the main list only
        for (Int32 ProcessIndex = 0; ProcessIndex < ProcessList.Count; ProcessIndex++)
            ImageList.Add(GetIcon(ProcessSelectorHandle, ProcessList[ProcessIndex]));

        return ImageList;
    }

    private Icon GetIcon(IntPtr ProcessSelectorHandle, Process TargetProcess)
    {
        try
        {
            // 32-bit OS grabbing 64-bit icons isn't allowed, so we check
            if (!IsProcessOSCompatable(TargetProcess))
                return null;

            IntPtr IconHandle = ExtractIcon(ProcessSelectorHandle, TargetProcess.MainModule.FileName, 0);

            if (!IconHandle.Equals(IntPtr.Zero))
                return (Icon.FromHandle(IconHandle));
        }
        catch { }

        return null;
    }

    [DllImport("shell32.dll", SetLastError = true)]
    public static extern IntPtr ExtractIcon(IntPtr hInst, string lpszExeFileName, int nIconIndex);

} // End class
*)