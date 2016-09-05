namespace Anna.Source.MVVM.Model

open System

type ExpenseReport =
    { Name : String
      Department : String
      ExpenseLineItems : seq<Expense>}