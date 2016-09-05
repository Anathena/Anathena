namespace Anna.Source.VestigialExampleCode

type ExpenseReportRepository() =
    member x.GetAll() =
        seq{ yield {Name="Mike" 
                    Department="Legal" 
                    ExpenseLineItems = 
                        [{ExpenseType="Lunch" 
                          ExpenseAmount="50"};
                         {ExpenseType="Transportation" 
                          ExpenseAmount="50"}]}
             yield {Name="Lisa"
                    Department="Marketing" 
                    ExpenseLineItems = 
                        [{ExpenseType="Document printing" 
                          ExpenseAmount="50"};
                         {ExpenseType="Gift" 
                          ExpenseAmount="125"}]}    
             yield {Name="John" 
                    Department="Engineering"
                    ExpenseLineItems = 
                        [{ExpenseType="Magazine subscription" 
                          ExpenseAmount="50"};
                         {ExpenseType="New machine" 
                          ExpenseAmount="600"};
                         {ExpenseType="Software" 
                          ExpenseAmount="500"}]}
             yield {Name="Mary"
                    Department="Finance"
                    ExpenseLineItems = 
                        [{ExpenseType="Dinner" 
                          ExpenseAmount="100"}]}
           }