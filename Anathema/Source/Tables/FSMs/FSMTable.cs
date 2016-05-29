using Anathema.Source.OS;
using Anathema.Source.Scanners.FiniteStateScanner;
using System;
using System.Collections.Generic;

namespace Anathema.Source.Tables.FSMs
{
    /// <summary>
    /// Handles the displaying of results
    /// </summary>
    class FSMTable : IFSMTableModel
    {
        // Singleton instance of fsm table
        private static Lazy<FSMTable> FSMTableInstance = new Lazy<FSMTable>(() => { return new FSMTable(); });

        private List<FiniteStateMachine> FiniteStateMachines;

        private FSMTable()
        {
            FiniteStateMachines = new List<FiniteStateMachine>();
        }

        public void OnGUIOpen() { }

        public static FSMTable GetInstance()
        {
            return FSMTableInstance.Value;
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