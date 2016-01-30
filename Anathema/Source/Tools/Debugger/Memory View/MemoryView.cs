using Be.Windows.Forms;
using Binarysharp.MemoryManagement;
using Binarysharp.MemoryManagement.Memory;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Anathema
{
    /// <summary>
    /// Handles the displaying of results
    /// </summary>
    class MemoryView : IMemoryViewModel, IProcessObserver
    {
        private MemorySharp MemoryEditor;

        private UInt64 StartReadAddress;
        private UInt64 EndReadAddress;
        private Int32 ReadLength;

        private ConcurrentDictionary<UInt64, Byte> IndexValueMap;
        private Boolean ForceRefreshFlag;

        public MemoryView()
        {
            InitializeProcessObserver();
            ForceRefreshFlag = false;
            IndexValueMap = new ConcurrentDictionary<UInt64, Byte>();
            Begin();
        }

        public void InitializeProcessObserver()
        {
            ProcessSelector.GetInstance().Subscribe(this);
        }

        public void UpdateMemoryEditor(MemorySharp MemoryEditor)
        {
            this.MemoryEditor = MemoryEditor;

            RefreshVirtualPages();
        }

        public override void ForceRefresh()
        {
            ForceRefreshFlag = true;
        }

        public override void RefreshVirtualPages()
        {
            if (MemoryEditor == null)
                return;

            List<RemoteVirtualPage> VirtualPages = new List<RemoteVirtualPage>(MemoryEditor.Memory.VirtualPages);
            MemoryViewEventArgs Args = new MemoryViewEventArgs();
            Args.VirtualPages = VirtualPages;
            OnEventUpdateVirtualPages(Args);
        }

        public override void UpdateStartReadAddress(UInt64 StartReadAddress)
        {
            this.StartReadAddress = StartReadAddress;
            UpdateEndReadAddress();
        }

        public override void UpdateReadLength(Int32 ReadLength)
        {
            this.ReadLength = ReadLength;
            UpdateEndReadAddress();
        }

        private void UpdateEndReadAddress()
        {
            this.EndReadAddress = unchecked(StartReadAddress + (UInt64)ReadLength);
            if (this.EndReadAddress < StartReadAddress)
                this.EndReadAddress = UInt64.MaxValue;
        }

        public override void Begin()
        {
            base.Begin();
        }

        protected override void Update()
        {
            base.Update();

            for (UInt64 Index = StartReadAddress; Index <= EndReadAddress; Index++)
            {
                // Ignore attempts to read null address
                if (Index == 0)
                    continue;

                Boolean ReadSuccess;
                Byte Value = MemoryEditor.Read<Byte>(unchecked((IntPtr)StartReadAddress), out ReadSuccess, false);

                if (ReadSuccess)
                    IndexValueMap[Index] = Value;
            }
            
            OnEventReadValues(new MemoryViewEventArgs());
        }

        public override Byte GetValueAtIndex(UInt64 Index)
        {
            if (IndexValueMap.ContainsKey(Index))
                return IndexValueMap[Index];

            return 0;
        }

    } // End class

} // End namespace