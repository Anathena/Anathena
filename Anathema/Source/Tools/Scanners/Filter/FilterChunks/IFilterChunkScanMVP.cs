using Binarysharp.MemoryManagement;
using Binarysharp.MemoryManagement.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anathema
{
    delegate void FilterChunkScanEventHandler(Object Sender, FilterChunksEventArgs Args);
    class FilterChunksEventArgs : EventArgs
    {
        public UInt64? FilterResultSize = null;
    }

    interface IFilterChunkScanView : IScannerView
    {
        // Methods invoked by the presenter (upstream)
        void DisplayResultSize(UInt64 FilterResultSize);
    }

    abstract class IFilterChunkScanModel : IScannerModel
    {
        // Events triggered by the model (upstream)
        public event FilterChunkScanEventHandler EventUpdateMemorySize;
        protected virtual void OnEventUpdateMemorySize(FilterChunksEventArgs E)
        {
            EventUpdateMemorySize(this, E);
        }

        // Functions invoked by presenter (downstream)
        public abstract void SetChunkSize(Int32 ChunkSize);
        public abstract void SetMinChanges(Int32 MinChanges);
    }

    class FilterChunkScanPresenter : ScannerPresenter
    {
        new IFilterChunkScanView View;
        new IFilterChunkScanModel Model;

        public FilterChunkScanPresenter(IFilterChunkScanView View, IFilterChunkScanModel Model) : base(View, Model)
        {
            this.View = View;
            this.Model = Model;
            // Bind events triggered by the model
            Model.EventUpdateMemorySize += EventUpdateMemorySize;
        }

        #region Method definitions called by the view (downstream)

        public void SetChunkSize(Int32 ChunkSize)
        {
            if (ChunkSize <= 0)
                return;

            Model.SetChunkSize(ChunkSize);
        }

        public void SetMinChanges(Int32 MinChanges)
        {
            if (MinChanges <= 0)
                return;

            Model.SetMinChanges(MinChanges);
        }

        #endregion

        #region Event definitions for events triggered by the model (upstream)

        private void EventUpdateMemorySize(Object sender, FilterChunksEventArgs e)
        {
            if (e.FilterResultSize.HasValue)
                View.DisplayResultSize(e.FilterResultSize.Value);
        }

        #endregion
    }
}
