namespace Anna.Source.Main

open System

type ExpenseReport =
    { Name : String
      Department : String
      ExpenseLineItems : seq<Expense>}