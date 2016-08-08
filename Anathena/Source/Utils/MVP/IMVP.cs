namespace Anathena.Source.Utils.MVP
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
        protected TView View { get; private set; }
        protected TModel Model { get; private set; }

        public Presenter(TView View, TModel Model)
        {
            this.View = View;
            this.Model = Model;
        }

    } // End class

} // End namespace