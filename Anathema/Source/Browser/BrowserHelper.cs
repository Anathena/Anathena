using Gecko;
using System;
using System.IO;
using System.Windows.Forms;

namespace Anathema
{
    class BrowserHelper
    {
        private static BrowserHelper _BrowserHelper;
        private static Boolean RunOnce;
        private static Object InitializeLock = new Object();

        private BrowserHelper()
        {
            RunOnce = true;
        }

        public static BrowserHelper GetInstance()
        {
            if (_BrowserHelper == null)
                _BrowserHelper = new BrowserHelper();
            return _BrowserHelper;
        }

        public void InitializeXpcom()
        {
            lock (InitializeLock)
            {
                if (!RunOnce)
                    return;

                if (OSInterface.IsAnathema32Bit())
                    Xpcom.Initialize(Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "xulrunner-32"));

                if (OSInterface.IsAnathema64Bit())
                    Xpcom.Initialize(Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "xulrunner-64"));

                RunOnce = false;
            }
        }

    } // End class

} // End namespace