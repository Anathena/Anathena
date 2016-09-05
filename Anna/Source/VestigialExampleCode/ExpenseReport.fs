namespace Anna.Source.VestigialExampleCode

open System

type ExpenseReport =
    { Name : String
      Department : String
      ExpenseLineItems : seq<Expense>}