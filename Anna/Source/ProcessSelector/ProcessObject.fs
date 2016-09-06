namespace Anna.Source.ProcessSelector

open System
open Anna.Source.Engine.OperatingSystems

type ProcessObject =
    { Name : String
      Department : String
      ProcessObjectLineItems : seq<NormalizedProcess>}