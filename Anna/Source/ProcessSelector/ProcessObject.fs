namespace Anna.Source.ProcessSelector

open System
open Anna.Source.Engine.OperatingSystems

type ExpenseReport =
    { Name : String
      Department : String
      ExpenseLineItems : seq<NormalizedProcess>}