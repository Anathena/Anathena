namespace Anna.Source.ProcessSelector

open Anna.Source.Engine.OperatingSystems

type ProcessSelectorPreviewItems() =
    member x.GetAll() =
        seq{ yield {Name="Please" 
                    Department="Legal" 
                    ExpenseLineItems = 
                        [{Icon="Lunch" 
                          ProcessName="50"};
                         {Icon="Transportation" 
                          ProcessName="50"}]}
             yield {Name="Please2"
                    Department="Marketing" 
                    ExpenseLineItems = 
                        [{Icon="Document printing" 
                          ProcessName="50"};
                         {Icon="Gift" 
                          ProcessName="125"}]}    
             yield {Name="Please3" 
                    Department="Engineering"
                    ExpenseLineItems = 
                        [{Icon="Magazine subscription" 
                          ProcessName="50"};
                         {Icon="New machine" 
                          ProcessName="600"};
                         {Icon="Software" 
                          ProcessName="500"}]}
             yield {Name="Please4"
                    Department="Finance"
                    ExpenseLineItems = 
                        [{Icon="Dinner" 
                          ProcessName="100"}]}
           }