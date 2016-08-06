using Microsoft.Diagnostics.Runtime;
using System.Diagnostics;

namespace AnathenaProxy
{
    public interface IClrServiceInterface
    {
        ClrHeap GetProcessClrHeap(Process TargetProcess);

    } // End interface

} // End namespace