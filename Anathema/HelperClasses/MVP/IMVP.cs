using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anathema
{
    /// <summary>
    /// Interface for GUI components
    /// </summary>
    public interface IView
    {
        // Nothing here yet, may not be needed.
    }

    /// <summary>
    /// Interface for backend code
    /// </summary>
    public interface IModel
    {
        // Nothing here yet, may not be needed.
    }

    /// <summary>
    /// Base class for classes which connect a GUI component to back end logic.
    /// </summary>
    /// <typeparam name="TView"></typeparam>
    public class Presenter<TView, TModel> where TView : class, IView where TModel : class, IModel
    {
        public TView View { get; private set; }
        public TModel Model { get; private set; }

        public Presenter(TView View, TModel Model)
        {
            if (View == null)
                throw new ArgumentNullException("View");

            if (Model == null)
                throw new ArgumentNullException("Model");

            this.View = View;
            this.Model = Model;
        }
    }
}
