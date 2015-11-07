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
        event EventHandler Initialize;
        event EventHandler Load;
    }
}
