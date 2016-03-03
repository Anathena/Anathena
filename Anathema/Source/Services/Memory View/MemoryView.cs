using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using Anathema.Utils.Extensions;
using Anathema.Utils.OS;
using Anathema.Services.ProcessManager;

namespace Anathema.Services.MemoryView
{
    /// <summary>
    /// Handles the displaying of results
    /// </summary>
    class MemoryView : IMemoryViewModel, IProcessObserver
    {
        private OSInterface OSInterface;
        private IEnumerable<NormalizedRegion> VirtualPages;

        private IntPtr StartReadAddress;
        private IntPtr EndReadAddress;
        private Int32 ReadLength;

        private ConcurrentDictionary<IntPtr, Byte> AddressValueMap;

        public MemoryView()
        {
            InitializeProcessObserver();
            AddressValueMap = new ConcurrentDictionary<IntPtr, Byte>();
            Begin();
        }

        public void InitializeProcessObserver()
        {
            ProcessSelector.GetInstance().Subscribe(this);
        }

        public void UpdateOSInterface(OSInterface OSInterface)
        {
            this.OSInterface = OSInterface;

            RefreshVirtualPages();
        }

        public override void RefreshVirtualPages()
        {
            if (OSInterface == null)
                return;

            VirtualPages = OSInterface.Process.GetAllVirtualPages();
            MemoryViewEventArgs Args = new MemoryViewEventArgs();
            Args.VirtualPages = VirtualPages;
            OnEventUpdateVirtualPages(Args);
        }

        public override void UpdateStartReadAddress(IntPtr StartReadAddress)
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
            if (VirtualPageIndex < 0 || VirtualPageIndex > VirtualPages.Count())
                return;

            MemoryViewEventArgs Args = new MemoryViewEventArgs();
            Args.Address = VirtualPages.ElementAt(VirtualPageIndex).BaseAddress;
            OnEventEventGoToAddress(Args);
        }

        private void UpdateEndReadAddress()
        {
            this.EndReadAddress = StartReadAddress.Add(ReadLength);
            if (this.EndReadAddress.ToUInt64() < StartReadAddress.ToUInt64())
                this.EndReadAddress = EndReadAddress.MaxValue();
        }

        public override void WriteToAddress(IntPtr Address, Byte Value)
        {
            if (OSInterface == null)
                return;

            OSInterface.Process.Write<Byte>(Address, Value);
        }

        public override void Begin()
        {
            base.Begin();
        }

        protected override void Update()
        {
            base.Update();

            if (OSInterface == null)
                return;

            for (IntPtr Address = StartReadAddress; Address.ToUInt64() <= EndReadAddress.ToUInt64(); Address.Add(1))
            {
                // Ignore attempts to read null address
                if (Address == IntPtr.Zero)
                    continue;

                Boolean ReadSuccess;
                Byte Value = OSInterface.Process.Read<Byte>(Address, out ReadSuccess);

                if (ReadSuccess)
                    AddressValueMap[Address] = Value;
            }
            
            OnEventReadValues(new MemoryViewEventArgs());
        }

        public override Byte GetValueAtAddress(IntPtr Address)
        {
            if (AddressValueMap.ContainsKey(Address))
                return AddressValueMap[Address];

            return 0;
        }

        public override void SetValueAtAddress(IntPtr Address, Byte Value)
        {
            AddressValueMap[Address] = Value;
        }

    } // End class

} // End namespace