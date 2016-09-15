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
        private new IFiniteStateScannerView view { get; set; }
        private new IFiniteStateScannerModel model { get; set; }

        public FiniteStateScannerPresenter(IFiniteStateScannerView view, IFiniteStateScannerModel model) : base(view, model)
        {
            this.view = view;
            this.model = model;

            // Bind events triggered by the model

            model.OnGUIOpen();
        }

        #region Method definitions called by the view (downstream)

        #endregion

        #region Event definitions for events triggered by the model (upstream)

        #endregion

    } // End class

} // End namespace