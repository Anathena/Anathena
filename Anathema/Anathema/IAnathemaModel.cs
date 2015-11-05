using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anathema
{
    public delegate void AnathemaModelHandler<IModel>(IAnathemaModel sender, AnathemaModelEventArgs e);
    // The ModelEventArgs class which is derived from th EventArgs class to 
    // be passed on to the controller when the value is changed
    public class AnathemaModelEventArgs : EventArgs
    {
        public int newval;
        public AnathemaModelEventArgs(int v)
        {
            newval = v;
        }
    }
    // The interface which the form/view must implement so that, the event will be
    // fired when a value is changed in the model.
    public interface IAnathemaModelObserver
    {
        void valueIncremented(IAnathemaModel model, AnathemaModelEventArgs e);
    }
    // The Model interface where we can attach the function to be notified when value
    // is changed. An actual data manipulation function increment which increments the value
    // A setvalue function which sets the value when users changes the textbox
    public interface IAnathemaModel
    {
        void attach(IAnathemaModelObserver imo);
        void increment();
        void setvalue(int v);
    }
    public class AnathemaModel : IAnathemaModel
    {
        public event AnathemaModelHandler<AnathemaModel> changed;
        int value;
        // implementation of IModel interface set the initial value to 0
        public AnathemaModel()
        {
            value = 0;
        }
        // Set value function to set the value in case if the user directly changes the value
        // in the textbox and the view change event fires in the controller
        public void setvalue(int v)
        {
            value = v;
        }
        // Change the value and fire the event with the new value inside ModelEventArgs
        // This will invoke the function valueIncremented in the model and will be displayed
        // in the textbox subsequently
        public void increment()
        {
            value++;
            changed.Invoke(this, new AnathemaModelEventArgs(value));
        }
        // Attach the function which is implementing the IModelObserver so that it can be
        // notified when a value is changed
        public void attach(IAnathemaModelObserver imo)
        {
            changed += new AnathemaModelHandler<AnathemaModel>(imo.valueIncremented);
        }
    }
}
