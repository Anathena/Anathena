using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace Gecko.Listeners
{
	[Obsolete]
    class GeckoWebProgressListener : GeckoBaseListener, nsIWebProgressListener
    {
        public GeckoWebProgressListener(nsIWebProgressListener p_broowser)
        {
            _weakBrowser = new System.WeakReference(p_broowser);
        }

        public void OnStateChange(nsIWebProgress aWebProgress, nsIRequest aRequest, uint aStateFlags, int aStatus)
        {
            nsIWebProgressListener b = (nsIWebProgressListener)_browser;
            if (b != null)
                b.OnStateChange(aWebProgress, aRequest, aStateFlags, aStatus);
        }

        public void OnProgressChange(nsIWebProgress aWebProgress, nsIRequest aRequest, int aCurSelfProgress, int aMaxSelfProgress, int aCurTotalProgress, int aMaxTotalProgress)
        {
            nsIWebProgressListener b = (nsIWebProgressListener)_browser;
            if (b != null)
                b.OnProgressChange(aWebProgress, aRequest, aCurSelfProgress, aMaxSelfProgress, aCurTotalProgress, aMaxTotalProgress);
        }

        public void OnLocationChange(nsIWebProgress aWebProgress, nsIRequest aRequest, nsIURI aLocation, uint flags)
        {
            nsIWebProgressListener b = (nsIWebProgressListener)_browser;
            if (b != null)
                b.OnLocationChange(aWebProgress, aRequest, aLocation, flags);
        }

        public void OnStatusChange(nsIWebProgress aWebProgress, nsIRequest aRequest, int aStatus, string aMessage)
        {
            nsIWebProgressListener b = (nsIWebProgressListener)_browser;
            if (b != null)
                b.OnStatusChange(aWebProgress, aRequest, aStatus, aMessage);
        }

        public void OnSecurityChange(nsIWebProgress aWebProgress, nsIRequest aRequest, uint aState)
        {
            nsIWebProgressListener b = (nsIWebProgressListener)_browser;
            if (b != null)
                b.OnSecurityChange(aWebProgress, aRequest, aState);
        }

        
    }
}
