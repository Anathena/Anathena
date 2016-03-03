namespace Anathema.Scanners.FiniteStateScanner
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
        new IFiniteStateScannerView View;
        new IFiniteStateScannerModel Model;

        public FiniteStateScannerPresenter(IFiniteStateScannerView View, IFiniteStateScannerModel Model) : base(View, Model)
        {
            this.View = View;
            this.Model = Model;

            // Bind events triggered by the model

        }

        #region Method definitions called by the view (downstream)

        #endregion

        #region Event definitions for events triggered by the model (upstream)

        #endregion
    }
}
