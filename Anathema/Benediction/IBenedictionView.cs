using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anathema
{
    public delegate void BenedictionViewHandler<IView>(IView sender, BenedictionViewEventArgs e);

    // The event arguments class that will be used while firing the events
    // for this program, we have only one value which the user changed.
    public class BenedictionViewEventArgs : EventArgs
    {
        public String TableEntry;
        public BenedictionViewEventArgs(String TableEntry) { this.TableEntry = TableEntry; }
    }

    interface IBenedictionView
    {
        event BenedictionViewHandler<IBenedictionView> UpdateTargetProcess;
        void SetBenedictionController(IBenedictionController BenedictionController);
    }
}
