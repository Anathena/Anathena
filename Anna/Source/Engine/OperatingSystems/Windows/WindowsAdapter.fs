namespace Anna.Source.Engine.OperatingSystems.Windows

open System.Diagnostics

open Anna.Source.Engine.OperatingSystems

type WindowsAdapter(externalProcess: Process) =
    interface IOperatingSystem2 with 
        member this.IsOS32Bit =
            true