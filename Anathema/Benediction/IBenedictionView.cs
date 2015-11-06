using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anathema
{
    public delegate void ViewHandler<IView>(IView sender, ViewEventArgs e);

    public class ViewEventArgs : EventArgs
    {
        public int value;
        public ViewEventArgs(int v) { value = v; }
    }

    interface IBenedictionView
    {
        event ViewHandler<IBenedictionView> changed;
        void SetBenedictionControler(IBenedictionController cont);
    }
}
