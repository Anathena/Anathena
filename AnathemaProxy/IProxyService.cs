using Microsoft.Diagnostics.Runtime;
using System;
using System.Diagnostics;
using System.ServiceModel;

namespace AnathenaProxy
{
    [ServiceContract()]
    public interface IProxyService
    {
        [OperationContract]
        Byte[] Assemble(Boolean IsProcess32Bit, String Assembly, UInt64 BaseAddress);

        [OperationContract]
        [ServiceKnownType(typeof(ClrHeap))]
        ClrHeap GetProcessClrHeap(Process TargetProcess);

    } // End class

} // End namespace