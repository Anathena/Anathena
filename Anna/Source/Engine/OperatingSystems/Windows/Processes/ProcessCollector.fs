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
open Anna.Source.Engine.OperatingSystems.Windows.Native

// Temporary structure for constructing process list
[<NoComparison>]
type IntermediateProcess ={ isSystemProcess: Boolean; externalProcess: Process }

type ProcessCollector() = 
    // Retrieves all running processes
    member this.GetProcesses() =
        Process.GetProcesses() |>
        Seq.map(fun (externalProcess) ->
        {
            isSystemProcess = this.IsProcessSystemProcess(externalProcess)
            externalProcess = externalProcess
        }) |>
        Seq.map(fun (filteredProcess) ->
        {
            processId= filteredProcess.externalProcess.Id
            processName= filteredProcess.externalProcess.ProcessName
            startTime = 
                try if filteredProcess.isSystemProcess then DateTime.MinValue else filteredProcess.externalProcess.StartTime
                with | _ -> DateTime.MinValue
            isSystemProcess= filteredProcess.isSystemProcess
            icon = this.GetIcon(filteredProcess.externalProcess, filteredProcess.isSystemProcess)
        }) |>
        Seq.sort

    // Fetches the icon associated with the provided process
    member this.IsProcessSystemProcess(externalProcess: Process) = 
        if (externalProcess.SessionId = 0 || externalProcess.BasePriority = 13) then true
        else
            try
                // Accessing this field will cause an access exception for system processes. This saves
                // time because handling the exception is faster than failing to fetch the icon later
                ignore externalProcess.PriorityBoostEnabled

                false
            with
                | _ -> true

    // Fetches the icon associated with the provided process
    member this.GetIcon(externalProcess: Process, isSystemProcess: Boolean) = 
        let noIcon: Icon = null
        // TODO: Check process bitness compatability
        if isSystemProcess then noIcon
        else
            try
                let iconHandle = NativeMethods.ExtractIcon(externalProcess.Handle, externalProcess.MainModule.FileName, 0)
                if (iconHandle = IntPtr.Zero) then noIcon else Icon.FromHandle(iconHandle)
            with
                | _ -> noIcon