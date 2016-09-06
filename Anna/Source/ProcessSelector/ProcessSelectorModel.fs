namespace Anna.Source.ProcessSelector

open Anna.Source.Engine.OperatingSystems

type ProcessSelectorModel() =
    member x.GetAll() =
        seq{ yield {Icon="{}" 
                    ProcessName="We are eternal"}
             yield {Icon="{}"
                    ProcessName="All this"}    
             yield {Icon="{}" 
                    ProcessName="Pain is"}
             yield {Icon="{}"
                    ProcessName="An Illusion"}
           }