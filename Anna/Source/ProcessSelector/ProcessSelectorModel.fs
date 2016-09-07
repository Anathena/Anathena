namespace Anna.Source.ProcessSelector

open System
open Anna.Source.Engine.OperatingSystems

type ProcessSelectorModel() =
    member this.GetAll() =
        seq {
            yield {
                processId = 0
                processName="We are eternal"
                startTime = DateTime.Now
                isSystemProcess=false
                icon=null
            }
            yield {
                processId = 1
                processName="All this"
                startTime = DateTime.Now
                isSystemProcess=false
                icon=null
            }
            yield {
                processId = 2
                processName="Pain is"
                startTime = DateTime.Now
                isSystemProcess=false
                icon=null
            }
            yield {
                processId = 3
                processName="An Illusion"
                startTime = DateTime.Now
                isSystemProcess=false
                icon=null
            }
        }