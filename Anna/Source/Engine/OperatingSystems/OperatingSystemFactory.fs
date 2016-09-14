namespace Anna.Source.Engine.OperatingSystems

open System.Diagnostics

open Anna.Source.Engine.OperatingSystems.Windows

type OperatingSystemFactory2() = 
    member this.GetOperatingSystem(externalProcess: Process) =
        new WindowsAdapter(externalProcess);