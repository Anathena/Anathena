using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anathema
{
    interface IMainController
    {

    }

    class MainController : IMainController
    {
        IMainView View;
        IMainModel Model;

        public MainController(IMainView View, IMainModel Model)
        {
            this.View = View;
            this.Model = Model;
            
            Model.attach((IMainModelObserver)View);
            View.Changed += new MainViewHandler<IMainView>(this.view_changed);
        }
        
        public void view_changed(IMainView v, MainViewEventArgs e)
        {
            // Model.SetValue(e.value);
        }
        
        public void DoSomething()
        {
            // Model.DoSomething();
        }
    }
}
