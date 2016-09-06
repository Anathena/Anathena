namespace Anna.Source.Engine.OperatingSystems

open System
open System.Drawing

[<CustomEquality; CustomComparison>]
type NormalizedProcess =
    {
        processId: Int32
        processName: String
        icon: Icon
    }

    override this.Equals(otherObject) =
           match otherObject with
             | :? NormalizedProcess as otherProcess -> (this.processId = otherProcess.processId)
             | _ -> false

    override this.GetHashCode() = hash this.processId

    interface System.IComparable with
        member this.CompareTo otherObject =
            match otherObject with
                | :? NormalizedProcess as otherProcess -> this.processId .CompareTo(otherProcess.processId)
                | _ -> invalidArg "otherProcess" "Objects do not have the same type" 
(*

// Class that can sort processes by time since execution
class ProcessTimeComparer : IComparer<Process>
{
    public static readonly ProcessTimeComparer Default = new ProcessTimeComparer();
    public ProcessTimeComparer() { }

    public int Compare(Process ProcessA, Process ProcessB)
    {
        try
        {
            return DateTime.Compare(ProcessB.StartTime, ProcessA.StartTime);
        }
        catch (InvalidOperationException)
        {
            return 0;
        }
    }
} // End class

class ProcessIDComparer : IComparer<Process>
{
    public static readonly ProcessIDComparer Default = new ProcessIDComparer();
    public ProcessIDComparer() { }

    public int Compare(Process ProcessA, Process ProcessB)
    {
        if (ProcessA.Id < ProcessB.Id)
            return 1;
        else if (ProcessA.Id > ProcessB.Id)
            return -1;
        else
            return 0;
    }
} // End class
*)