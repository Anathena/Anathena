using Anathema.Source.Utils.Extensions;
using Anathema.User.UserSettings;
using Anathema.Utils;
using Anathema.Utils.Cache;
using Anathema.Utils.Extensions;
using Anathema.Utils.MVP;
using Anathema.Utils.OS;
using Be.Windows.Forms;
using System;
using System.Collections.Generic;

namespace Anathema.Services.MemoryView
{
    delegate void MemoryViewEventHandler(Object Sender, MemoryViewEventArgs Args);
    class MemoryViewEventArgs : EventArgs
    {
        public IEnumerable<NormalizedRegion> VirtualPages;
        public IntPtr Address;
    }

    interface IMemoryViewView : IView
    {
        // Methods invoked by the presenter (upstream)
        void ReadValues();
        void UpdateVirtualPages(IEnumerable<String> VirtualPages);
        void GoToAddress(IntPtr Address);
    }

    abstract class IMemoryViewModel : RepeatedTask, IModel
    {
        // Events triggered by the model (upstream)
        public event MemoryViewEventHandler EventUpdateVirtualPages;
        protected virtual void OnEventUpdateVirtualPages(MemoryViewEventArgs E)
        {
            EventUpdateVirtualPages?.Invoke(this, E);
        }
        public event MemoryViewEventHandler EventGoToAddress;
        protected virtual void OnEventEventGoToAddress(MemoryViewEventArgs E)
        {
            EventGoToAddress?.Invoke(this, E);
        }
        public event MemoryViewEventHandler EventReadValues;
        protected virtual void OnEventReadValues(MemoryViewEventArgs E)
        {
            EventReadValues?.Invoke(this, E);
        }
        public event MemoryViewEventHandler EventFlushCache;
        protected virtual void OnEventFlushCache(MemoryViewEventArgs E)
        {
            EventFlushCache?.Invoke(this, E);
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

        public abstract Byte GetValueAtAddress(IntPtr Address);
        public abstract void SetValueAtAddress(IntPtr Address, Byte Value);
        public abstract void UpdateReadLength(Int32 ReadLength);
        public abstract void UpdateStartReadAddress(IntPtr StartReadAddress);

        public abstract void QuickNavigate(Int32 VirtualPageIndex);

        public abstract void WriteToAddress(IntPtr Address, Byte Value);
    }

    class MemoryViewPresenter : Presenter<IMemoryViewView, IMemoryViewModel>, IByteProvider
    {
        protected new IMemoryViewView View;
        protected new IMemoryViewModel Model;

        private const Int32 ViewRange = UInt16.MaxValue;

        private ObjectCache<Byte> ByteCache;
        private IntPtr BaseAddress;

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
        }

        #region ByteProvider

        public Int64 Length { get { return ViewRange; } }

        public Byte ReadByte(Int64 Index)
        {
            IntPtr EffectiveAddress = BaseAddress.Add(Index);
            Byte Item = ByteCache.Get(EffectiveAddress.ToUInt64());

            // Try to update and return the item if it is a valid item
            if (ByteCache.TryUpdateItem(EffectiveAddress.ToUInt64(), Model.GetValueAtAddress(EffectiveAddress)))
                return ByteCache.Get(EffectiveAddress.ToUInt64());

            Item = 0; // null;
            ByteCache.Add(EffectiveAddress.ToUInt64(), Item);

            return Item;
        }

        public void WriteByte(Int64 Index, Byte Value)
        {
            IntPtr EffectiveAddress = BaseAddress.Add(Index);
            Model.WriteToAddress(EffectiveAddress, Value);
            ByteCache.TryUpdateItem(EffectiveAddress.ToUInt64(), Value);
            Model.SetValueAtAddress(EffectiveAddress, Value);
        }

        public void ApplyChanges() { }
        public Boolean SupportsWriteByte() { return true; }

        #region Irrelevant Features
        public event EventHandler Changed, LengthChanged;
        public void DeleteBytes(Int64 Index, Int64 Length) { throw new NotImplementedException(); }
        public void InsertBytes(Int64 Index, Byte[] BS) { throw new NotImplementedException(); }
        public Boolean SupportsDeleteBytes() { return false; }
        public Boolean SupportsInsertBytes() { return false; }
        public Boolean HasChanges() { return false; }
        #endregion

        #endregion

        #region Method definitions called by the view (downstream)

        public void UpdateStartReadAddress(IntPtr StartReadAddress)
        {
            Model.UpdateStartReadAddress(StartReadAddress);
        }

        public void UpdateReadLength(Int32 ReadLength)
        {
            Model.UpdateReadLength(ReadLength);
        }

        public void UpdateBaseAddress(IntPtr BaseAddress)
        {
            this.BaseAddress = BaseAddress;
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

    } // End class

} // End namespace