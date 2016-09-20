namespace Ana.Source.Utils.MVP
{
    /// <summary>
    /// Interface for GUI components
    /// </summary>
    public interface IView
    {
        // Base view interface. No functions yet, this may change.
    }

    /// <summary>
    /// Interface for backend code
    /// </summary>
    public interface IModel
    {
        void OnGUIOpen();
    }

    /// <summary>
    /// Base class for classes which connect a GUI component to back end logic.
    /// </summary>
    /// <typeparam name="TView"></typeparam>
    public class Presenter<TView, TModel> where TView : class, IView where TModel : class, IModel
    {
        protected TView view { get; private set; }
        protected TModel model { get; private set; }

        public Presenter(TView view, TModel model)
        {
            this.view = view;
            this.model = model;
        }

    } // End class

} // End namespace