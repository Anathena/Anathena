using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anathema
{
    delegate void MainViewHandler<IMainView>(IMainView sender, MainViewEventArgs e);

    interface IMainView
    {
        event MainViewHandler<IMainView> Changed;
    }

    public class MainViewEventArgs : EventArgs
    {
        public int value;
        public MainViewEventArgs(int v) { value = v; }
    }
}
