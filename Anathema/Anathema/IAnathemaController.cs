using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anathema
{
    interface IAnathemaController
    {

    }

    class AnathemaController : IAnathemaController
    {
        IAnathemaView View;
        IAnathemaModel Model;

        public AnathemaController(IAnathemaView View, IAnathemaModel Model)
        {
            this.View = View;
            this.Model = Model;

            View.SetAnathemaControler(this);
            Model.attach((IAnathemaModelObserver)View);
            View.changed += new ViewHandler<IAnathemaView>(this.view_changed);
        }
        
        public void view_changed(IAnathemaView v, ViewEventArgs e)
        {
            // Model.SetValue(e.value);
        }
        
        public void DoSomething()
        {
            // Model.DoSomething();
        }
    }
}
