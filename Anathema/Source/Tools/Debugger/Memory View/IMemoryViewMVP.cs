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
        public UInt64 ElementCount = 0;
        public UInt64 MemorySize = 0;

        public List<RemoteVirtualPage> VirtualPages;
    }

    interface IMemoryViewView : IView
    {
        // Methods invoked by the presenter (upstream)
        void ReadValues();
        void UpdateVirtualPages(List<String> VirtualPages);
        void UpdateItemCount(Int32 ItemCount);
    }

    abstract class IMemoryViewModel : RepeatedTask, IModel
    {
        // Events triggered by the model (upstream)
        public event MemoryViewEventHandler EventUpdateVirtualPages;
        protected virtual void OnEventUpdateVirtualPages(MemoryViewEventArgs E)
        {
            EventUpdateVirtualPages(this, E);
        }
        public event MemoryViewEventHandler EventReadValues;
        protected virtual void OnEventReadValues(MemoryViewEventArgs E)
        {
            EventReadValues(this, E);
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

        public abstract void AddSelectionToTable(Int32 Index);

        public abstract IntPtr GetAddressAtIndex(Int32 Index);
        public abstract String GetValueAtIndex(Int32 Index);
        public abstract String GetLabelAtIndex(Int32 Index);

        public abstract void ForceRefresh();

        public abstract void UpdateReadBounds(Int32 StartReadIndex, Int32 EndReadIndex);
    }

    class MemoryViewPresenter : Presenter<IMemoryViewView, IMemoryViewModel>
    {
        protected new IMemoryViewView View;
        protected new IMemoryViewModel Model;

        private ObjectCache<String> ByteCache;
        
        public MemoryViewPresenter(IMemoryViewView View, IMemoryViewModel Model) : base(View, Model)
        {
            this.View = View;
            this.Model = Model;

            ByteCache = new ObjectCache<String>();

            // Bind events triggered by the model
            Model.EventUpdateVirtualPages += EventUpdateVirtualPages;
            Model.EventReadValues += EventReadValues;
            Model.EventFlushCache += EventFlushCache;

            Model.ForceRefresh();
        }

        #region Method definitions called by the view (downstream)
        
        public void UpdateReadBounds(Int32 StartReadIndex, Int32 EndReadIndex)
        {
            Model.UpdateReadBounds(StartReadIndex, EndReadIndex);
        }

        public void RefreshVirtualPages()
        {
            Model.RefreshVirtualPages();
        }

        public String GetItemAt(Int32 Index)
        {
            String Item = ByteCache.Get(Index);

            // Try to update and return the item if it is a valid item
            if (Item != null && ByteCache.TryUpdateItem(Index, Model.GetValueAtIndex(Index)))
                return Item;

            // Add the properties to the cache and get the list view item back
            Item = "--";

            return Item;
        }

        public void AddSelectionToTable(Int32 Index)
        {
            Model.AddSelectionToTable(Index);
        }

        #endregion

        #region Event definitions for events triggered by the model (upstream)

        private void EventUpdateVirtualPages(Object Sender, MemoryViewEventArgs E)
        {
            List<String> VirtualPages = new List<String>();
            E.VirtualPages.ForEach(x => VirtualPages.Add(x.BaseAddress.ToString("X")));

            View.UpdateVirtualPages(VirtualPages);
        }

        private void EventReadValues(Object Sender, MemoryViewEventArgs E)
        {
            View.ReadValues();
        }

        private void EventFlushCache(Object Sender, MemoryViewEventArgs E)
        {
            ByteCache.FlushCache();
            View.UpdateItemCount((Int32)Math.Min(E.ElementCount, Int32.MaxValue));
        }

        #endregion
    }
}