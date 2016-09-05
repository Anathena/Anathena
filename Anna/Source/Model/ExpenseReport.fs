namespace Anna.Source.Model

type ExpenseReport =
    { Name : string
      Department : string
      ExpenseLineItems : seq<Expense>}

