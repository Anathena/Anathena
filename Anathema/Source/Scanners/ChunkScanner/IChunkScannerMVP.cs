using System;

namespace Anathema.Source.Scanners.ChunkScanner
{
    interface IChunkScannerView : IScannerView
    {
        // Methods invoked by the presenter (upstream)
    }

    abstract class IChunkScannerModel : IScannerModel
    {
        // Events triggered by the model (upstream)

        // Functions invoked by presenter (downstream)
        public abstract void SetMinChanges(Int32 MinChanges);
    }

    class ChunkScannerPresenter : ScannerPresenter
    {
        private new IChunkScannerView View { get; set; }
        private new IChunkScannerModel Model { get; set; }

        public ChunkScannerPresenter(IChunkScannerView View, IChunkScannerModel Model) : base(View, Model)
        {
            this.View = View;
            this.Model = Model;

            // Bind events triggered by the model


            Model.OnGUIOpen();
        }

        #region Method definitions called by the view (downstream)

        public void SetChunkSize(Int32 ChunkSize)
        {
            if (ChunkSize <= 0)
                return;
        }

        public void SetMinChanges(Int32 MinChanges)
        {
            if (MinChanges <= 0)
                return;

            Model.SetMinChanges(MinChanges);
        }

        #endregion

        #region Event definitions for events triggered by the model (upstream)

        #endregion

    } // End class

} // End namespace