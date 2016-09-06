namespace Anna.Source.ProcessSelector

open Anna.Source.Engine.OperatingSystems

type ProcessSelectorModel() =
    member this.GetAll() =
        seq {
            yield {
                processId = 0
                processName="We are eternal"
                icon=null
            }
            yield {
                processId = 0
                processName="All this"
                icon=null
            }
            yield {
                processId = 0
                processName="Pain is"
                icon=null
            }
            yield {
                processId = 0
                processName="An Illusion"
                icon=null
            }
        }