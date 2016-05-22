namespace Anathema.Scanners.TreeScanner
{
    interface ITreeScannerView : IScannerView
    {
        // Methods invoked by the presenter (upstream)

    }

    abstract class ITreeScannerModel : IScannerModel
    {
        // Events triggered by the model (upstream)

        // Functions invoked by presenter (downstream)
    }

    class TreeScannerPresenter : ScannerPresenter
    {
        private new ITreeScannerView View { get; set; }
        private new ITreeScannerModel Model { get; set; }

        public TreeScannerPresenter(ITreeScannerView View, ITreeScannerModel Model) : base(View, Model)
        {
            this.View = View;
            this.Model = Model;

            // Bind events triggered by the model


            Model.OnGUIOpen();
        }

        #region Method definitions called by the view (downstream)

        #endregion

        #region Event definitions for events triggered by the model (upstream)

        #endregion

    } // End class

} // End namespace