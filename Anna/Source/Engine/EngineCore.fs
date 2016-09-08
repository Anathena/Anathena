namespace Anna.Source.Engine

open System.Diagnostics

type EngineCore(externalProcess: Process) = 
    member this.getProcesses() =
        0