using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Anathema
{
    public static class ControlThreadingHelper
    {
        public static void InvokeControlAction<t>(t control, Action action) where t : Control
        {
            if (control.InvokeRequired)
                control.Invoke(new Action<t, Action>(InvokeControlAction), new object[] { control, action });
            else
                action();
        }
    }
}
