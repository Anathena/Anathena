using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anathema
{
    interface IBenedictionController
    {

    }

    class BenedictionController : IBenedictionController
    {
        IBenedictionView View;
        IBenedictionModel Model;

        public BenedictionController(IBenedictionView View, IBenedictionModel Model)
        {
            this.View = View;
            this.Model = Model;

            View.SetBenedictionControler(this);
            Model.attach((IBenedictionModelObserver)View);
            View.changed += new ViewHandler<IBenedictionView>(this.view_changed);
        }
        
        public void view_changed(IBenedictionView v, ViewEventArgs e)
        {
            // Model.SetValue(e.value);
        }
        
        public void DoSomething()
        {
            // Model.DoSomething();
        }
    }
}
