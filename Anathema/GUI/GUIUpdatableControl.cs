using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Anathema
{
    /// <summary>
    /// Interface to allow custom user controls to be updated / refreshed on a single timer.
    /// </summary>
    public interface GUIUpdateableControl
    {
        void UpdateGUI();
    }
}
