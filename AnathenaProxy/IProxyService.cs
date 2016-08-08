using System;
using System.Collections.Generic;
using System.ServiceModel;

namespace AnathenaProxy
{
    [ServiceContract()]
    public interface IProxyService
    {
        #region Fasm

        [OperationContract]
        Byte[] Assemble(Boolean IsProcess32Bit, String Assembly, UInt64 BaseAddress);

        #endregion

        #region CLR

        [OperationContract]
        Boolean RefreshHeap(Int32 ProcessId);

        [OperationContract]
        IEnumerable<UInt64> GetRoots();
        [OperationContract]
        Int32 GetRootType(UInt64 RootRef);
        [OperationContract]
        String GetRootName(UInt64 RootRef);

        [OperationContract]
        IEnumerable<UInt64> GetObjectChildren(UInt64 ObjectRef);
        [OperationContract]
        IEnumerable<UInt64> GetObjectFields(UInt64 ObjectRef);
        [OperationContract]
        Int32 GetObjectType(UInt64 ObjectRef);
        [OperationContract]
        String GetObjectName(UInt64 ObjectRef);

        [OperationContract]
        String GetFieldName(UInt64 FieldRef);
        [OperationContract]
        Int32 GetFieldType(UInt64 FieldRef);
        [OperationContract]
        Int32 GetFieldOffset(UInt64 FieldRef);

        #endregion

    } // End class

} // End namespace