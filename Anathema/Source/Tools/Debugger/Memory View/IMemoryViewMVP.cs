using Be.Windows.Forms;
using Binarysharp.MemoryManagement;
using Binarysharp.MemoryManagement.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Anathema
{
    delegate void MemoryViewEventHandler(Object Sender, MemoryViewEventArgs Args);
    class MemoryViewEventArgs : EventArgs
    {
        public List<RemoteVirtualPage> VirtualPages;
        public UInt64 Address;
    }

    interface IMemoryViewView : IView
    {
        // Methods invoked by the presenter (upstream)
        void ReadValues();
        void UpdateVirtualPages(List<String> VirtualPages);
        void GoToAddress(UInt64 Address);
    }

    abstract class IMemoryViewModel : RepeatedTask, IModel
    {
        // Events triggered by the model (upstream)
        public event MemoryViewEventHandler EventUpdateVirtualPages;
        protected virtual void OnEventUpdateVirtualPages(MemoryViewEventArgs E)
        {
            if (EventUpdateVirtualPages != null) EventUpdateVirtualPages(this, E);
        }
        public event MemoryViewEventHandler EventGoToAddress;
        protected virtual void OnEventEventGoToAddress(MemoryViewEventArgs E)
        {
            if (EventGoToAddress != null) EventGoToAddress(this, E);
        }
        public event MemoryViewEventHandler EventReadValues;
        protected virtual void OnEventReadValues(MemoryViewEventArgs E)
        {
            if (EventReadValues != null) EventReadValues(this, E);
        }
        public event MemoryViewEventHandler EventFlushCache;
        protected virtual void OnEventFlushCache(MemoryViewEventArgs E)
        {
            if (EventFlushCache != null) EventFlushCache(this, E);
        }

        public override void Begin()
        {
            WaitTime = Settings.GetInstance().GetResultReadInterval();
            base.Begin();
        }

        protected override void Update()
        {
            WaitTime = Settings.GetInstance().GetResultReadInterval();
        }

        // Functions invoked by presenter (downstream)
        public abstract void RefreshVirtualPages();

        public abstract Byte GetValueAtIndex(UInt64 Index);
        public abstract void ForceRefresh();
        public abstract void UpdateReadLength(Int32 ReadLength);
        public abstract void UpdateStartReadAddress(UInt64 StartReadAddress);

        public abstract void QuickNavigate(Int32 VirtualPageIndex);
    }

    class MemoryViewPresenter : Presenter<IMemoryViewView, IMemoryViewModel>, IByteProvider
    {
        protected new IMemoryViewView View;
        protected new IMemoryViewModel Model;

        private ObjectCache<Byte> ByteCache;

        public MemoryViewPresenter(IMemoryViewView View, IMemoryViewModel Model) : base(View, Model)
        {
            this.View = View;
            this.Model = Model;

            ByteCache = new ObjectCache<Byte>();

            // Bind events triggered by the model
            Model.EventUpdateVirtualPages += EventUpdateVirtualPages;
            Model.EventGoToAddress += EventGoToAddress;
            Model.EventReadValues += EventReadValues;
            Model.EventFlushCache += EventFlushCache;

            Model.ForceRefresh();
        }
        
        #region ByteProvider

        public Int64 Length
        {
            get
            {
                return ByteCache.CacheSize;
            }
        }

        public event EventHandler Changed;
        public event EventHandler LengthChanged;

        public void ApplyChanges()
        {

        }

        public Byte ReadByte(Int64 Index)
        {
            Byte Item = ByteCache.Get(unchecked((UInt64)Index));

            // Try to update and return the item if it is a valid item
            ByteCache.SetItem(unchecked((UInt64)Index), Model.GetValueAtIndex(unchecked((UInt64)Index)));

            // Item == null

            return ByteCache.Get(unchecked((UInt64)Index));
        }

        public void WriteByte(Int64 Index, Byte Value)
        {

        }

        public Boolean SupportsWriteByte() { return true; }

        #region Irrelevant Features
        public void DeleteBytes(Int64 Index, Int64 Length) { throw new NotImplementedException(); }
        public void InsertBytes(Int64 Index, Byte[] BS) { throw new NotImplementedException(); }
        public Boolean SupportsDeleteBytes() { return false; }
        public Boolean SupportsInsertBytes() { return false; }
        public Boolean HasChanges() { return false; }
        #endregion

        #endregion

        #region Method definitions called by the view (downstream)

        public void UpdateStartReadAddress(UInt64 StartReadAddress)
        {
            Model.UpdateStartReadAddress(StartReadAddress);
        }

        public void UpdateReadLength(Int32 ReadLength)
        {
            Model.UpdateReadLength(ReadLength);
        }

        public void QuickNavigate(Int32 VirtualPageIndex)
        {
            Model.QuickNavigate(VirtualPageIndex);
        }

        public void RefreshVirtualPages()
        {
            Model.RefreshVirtualPages();
        }

        #endregion

        #region Event definitions for events triggered by the model (upstream)

        private void EventUpdateVirtualPages(Object Sender, MemoryViewEventArgs E)
        {
            List<String> VirtualPages = new List<String>();

            if (E.VirtualPages != null)
                E.VirtualPages.ForEach(x => VirtualPages.Add(x.BaseAddress.ToString("X")));

            View.UpdateVirtualPages(VirtualPages);
        }

        private void EventGoToAddress(Object Sender, MemoryViewEventArgs E)
        {
            View.GoToAddress(E.Address);
        }
        
        private void EventReadValues(Object Sender, MemoryViewEventArgs E)
        {
            View.ReadValues();
        }

        private void EventFlushCache(Object Sender, MemoryViewEventArgs E)
        {
            ByteCache.FlushCache();
        }

        #endregion
    }
}