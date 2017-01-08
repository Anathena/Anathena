namespace AnathenaProxy
{
    using System;
    using System.Collections.Generic;
    using System.ServiceModel;

    [ServiceContract()]
    public interface IProxyService
    {
        #region Fasm

        [OperationContract]
        Byte[] Assemble(Boolean isProcess32Bit, String assembly, UInt64 baseAddress, out String logs);

        #endregion

        #region CLR

        [OperationContract]
        Boolean RefreshHeap(Int32 ProcessId);

        [OperationContract]
        IEnumerable<UInt64> GetRoots();
        [OperationContract]
        Int32 GetRootType(UInt64 rootRef);
        [OperationContract]
        String GetRootName(UInt64 rootRef);

        [OperationContract]
        IEnumerable<UInt64> GetObjectChildren(UInt64 objectRef);
        [OperationContract]
        IEnumerable<UInt64> GetObjectFields(UInt64 objectRef);
        [OperationContract]
        Int32 GetObjectType(UInt64 objectRef);
        [OperationContract]
        String GetObjectName(UInt64 objectRef);

        [OperationContract]
        String GetFieldName(UInt64 fieldRef);
        [OperationContract]
        Int32 GetFieldType(UInt64 fieldRef);
        [OperationContract]
        Int32 GetFieldOffset(UInt64 fieldRef);

        #endregion
    }
    //// End class
}
//// End namespace