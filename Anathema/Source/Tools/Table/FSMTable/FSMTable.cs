using Anathema.MemoryManagement;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;

namespace Anathema
{
    /// <summary>
    /// Handles the displaying of results
    /// </summary>
    class FSMTable : IFSMTableModel
    {
        private static FSMTable FSMTableInstance;

        private List<FiniteStateMachine> FiniteStateMachines;

        public event ScriptTableEventHandler EventClearScriptCacheItem;
        public event ScriptTableEventHandler EventClearScriptCache;
        public event FSMTableEventHandler EventClearFSMCacheItem;
        public event FSMTableEventHandler EventClearFSMCache;

        private FSMTable()
        {
            FiniteStateMachines = new List<FiniteStateMachine>();
        }

        public static FSMTable GetInstance()
        {
            if (FSMTableInstance == null)
                FSMTableInstance = new FSMTable();
            return FSMTableInstance;
        }

        public bool SaveTable(String Path)
        {
            throw new NotImplementedException();
        }

        public bool LoadTable(String Path)
        {
            throw new NotImplementedException();
        }

        public void OpenFSM(Int32 Index)
        {
            throw new NotImplementedException();
        }

        public FiniteStateMachine GetFSMItemAt(Int32 Index)
        {
            throw new NotImplementedException();
        }

        public void InitializeProcessObserver()
        {
            throw new NotImplementedException();
        }

        public void UpdateOSInterface(OSInterface OSInterface)
        {
            throw new NotImplementedException();
        }

    } // End class

} // End namespace