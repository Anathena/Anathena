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
        private MemoryEditor MemoryEditor;
        private List<RemoteVirtualPage> VirtualPages;

        private UInt64 StartReadAddress;
        private UInt64 EndReadAddress;
        private Int32 ReadLength;

        private ConcurrentDictionary<UInt64, Byte> AddressValueMap;

        public MemoryView()
        {
            InitializeProcessObserver();
            AddressValueMap = new ConcurrentDictionary<UInt64, Byte>();
            Begin();
        }

        public void InitializeProcessObserver()
        {
            ProcessSelector.GetInstance().Subscribe(this);
        }

        public void UpdateMemoryEditor(MemoryEditor MemoryEditor)
        {
            this.MemoryEditor = MemoryEditor;

            RefreshVirtualPages();
        }

        public override void RefreshVirtualPages()
        {
            if (MemoryEditor == null)
                return;

            VirtualPages = new List<RemoteVirtualPage>(MemoryEditor.Memory.AllVirtualPages);
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

        public override void QuickNavigate(Int32 VirtualPageIndex)
        {
            if (VirtualPageIndex < 0 || VirtualPageIndex > VirtualPages.Count)
                return;

            MemoryViewEventArgs Args = new MemoryViewEventArgs();
            Args.Address = unchecked((UInt64)VirtualPages[VirtualPageIndex].BaseAddress);
            OnEventEventGoToAddress(Args);
        }

        private void UpdateEndReadAddress()
        {
            this.EndReadAddress = unchecked(StartReadAddress + (UInt64)ReadLength);
            if (this.EndReadAddress < StartReadAddress)
                this.EndReadAddress = UInt64.MaxValue;
        }

        public override void WriteToAddress(UInt64 Address, Byte Value)
        {
            if (MemoryEditor == null)
                return;

            MemoryEditor.Write<Byte>(unchecked((IntPtr)Address), Value);
        }

        public override void Begin()
        {
            base.Begin();
        }

        protected override void Update()
        {
            base.Update();

            if (MemoryEditor == null)
                return;

            for (UInt64 Address = StartReadAddress; Address <= EndReadAddress; Address++)
            {
                // Ignore attempts to read null address
                if (Address == 0)
                    continue;

                Boolean ReadSuccess;
                Byte Value = MemoryEditor.Read<Byte>(unchecked((IntPtr)Address), out ReadSuccess);

                if (ReadSuccess)
                    AddressValueMap[Address] = Value;
            }
            
            OnEventReadValues(new MemoryViewEventArgs());
        }

        public override Byte GetValueAtAddress(UInt64 Address)
        {
            if (AddressValueMap.ContainsKey(Address))
                return AddressValueMap[Address];

            return 0;
        }

        public override void SetValueAtAddress(UInt64 Address, Byte Value)
        {
            AddressValueMap[Address] = Value;
        }

    } // End class

} // End namespace