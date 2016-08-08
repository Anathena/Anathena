namespace Anathena.Source.Scanners.FiniteStateScanner
{
    interface IFiniteStateScannerView : IScannerView
    {
        // Methods invoked by the presenter (upstream)

    }

    abstract class IFiniteStateScannerModel : IScannerModel
    {
        // Events triggered by the model (upstream)

        // Functions invoked by presenter (downstream)

    }

    class FiniteStateScannerPresenter : ScannerPresenter
    {
        private new IFiniteStateScannerView View { get; set; }
        private new IFiniteStateScannerModel Model { get; set; }

        public FiniteStateScannerPresenter(IFiniteStateScannerView View, IFiniteStateScannerModel Model) : base(View, Model)
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