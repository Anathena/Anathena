using Ana.Source.Engine.OperatingSystems;
using Ana.Source.UserSettings;
using Ana.Source.Utils;
using Ana.Source.Utils.DataStructures;
using Ana.Source.Utils.Extensions;
using Ana.Source.Utils.MVP;
using Be.Windows.Forms;
using System;
using System.Collections.Generic;

namespace Ana.Source.MemoryView
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
        public void OnGUIOpen() { }

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
            UpdateInterval = Settings.GetInstance().GetResultReadInterval();
            base.Begin();
        }

        protected override void Update()
        {
            UpdateInterval = Settings.GetInstance().GetResultReadInterval();
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
        private new IMemoryViewView view { get; set; }
        private new IMemoryViewModel model { get; set; }

        private const Int32 ViewRange = UInt16.MaxValue;

        private ObjectCache<Byte> ByteCache;
        private IntPtr BaseAddress;

        public MemoryViewPresenter(IMemoryViewView view, IMemoryViewModel model) : base(view, model)
        {
            this.view = view;
            this.model = model;

            ByteCache = new ObjectCache<Byte>();

            // Bind events triggered by the model
            model.EventUpdateVirtualPages += EventUpdateVirtualPages;
            model.EventGoToAddress += EventGoToAddress;
            model.EventReadValues += EventReadValues;
            model.EventFlushCache += EventFlushCache;

            model.OnGUIOpen();
        }

        #region ByteProvider

        public Int64 Length { get { return ViewRange; } }

        public Byte ReadByte(Int64 Index)
        {
            IntPtr EffectiveAddress = BaseAddress.Add(Index);
            Byte Item = ByteCache.Get(EffectiveAddress.ToUInt64());

            // Try to update and return the item if it is a valid item
            if (ByteCache.TryUpdateItem(EffectiveAddress.ToUInt64(), model.GetValueAtAddress(EffectiveAddress)))
                return ByteCache.Get(EffectiveAddress.ToUInt64());

            Item = 0; // null;
            ByteCache.Add(EffectiveAddress.ToUInt64(), Item);

            return Item;
        }

        public void WriteByte(Int64 Index, Byte Value)
        {
            IntPtr EffectiveAddress = BaseAddress.Add(Index);
            model.WriteToAddress(EffectiveAddress, Value);
            ByteCache.TryUpdateItem(EffectiveAddress.ToUInt64(), Value);
            model.SetValueAtAddress(EffectiveAddress, Value);
        }

        public void ApplyChanges() { }
        public Boolean SupportsWriteByte() { return true; }

        #region Irrelevant Features
        public event EventHandler Changed, LengthChanged = null;
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
            model.UpdateStartReadAddress(StartReadAddress);
        }

        public void UpdateReadLength(Int32 ReadLength)
        {
            model.UpdateReadLength(ReadLength);
        }

        public void UpdateBaseAddress(IntPtr BaseAddress)
        {
            this.BaseAddress = BaseAddress;
        }

        public void QuickNavigate(Int32 VirtualPageIndex)
        {
            model.QuickNavigate(VirtualPageIndex);
        }

        public void RefreshVirtualPages()
        {
            model.RefreshVirtualPages();
        }

        #endregion

        #region Event definitions for events triggered by the model (upstream)

        private void EventUpdateVirtualPages(Object Sender, MemoryViewEventArgs E)
        {
            List<String> VirtualPages = new List<String>();

            if (E.VirtualPages != null)
                E.VirtualPages.ForEach(x => VirtualPages.Add(x.BaseAddress.ToString("X")));

            view.UpdateVirtualPages(VirtualPages);
        }

        private void EventGoToAddress(Object Sender, MemoryViewEventArgs E)
        {
            view.GoToAddress(E.Address);
        }

        private void EventReadValues(Object Sender, MemoryViewEventArgs E)
        {
            view.ReadValues();
        }

        private void EventFlushCache(Object Sender, MemoryViewEventArgs E)
        {
            ByteCache.FlushCache();
        }

        #endregion

    } // End class

} // End namespace