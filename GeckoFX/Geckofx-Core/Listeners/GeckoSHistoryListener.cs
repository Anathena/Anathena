using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gecko.Listeners
{
	[Obsolete]
    class GeckoSHistoryListener : GeckoBaseListener, nsISHistoryListener
    {
        public GeckoSHistoryListener(nsISHistoryListener p_broowser)
        {
            _weakBrowser = new System.WeakReference(p_broowser);
        }

        public void OnHistoryNewEntry(nsIURI aNewURI)
        {
            nsISHistoryListener b = (nsISHistoryListener)_browser;
            if (b != null)
                b.OnHistoryNewEntry(aNewURI);
        }

        public bool OnHistoryGoBack(nsIURI aBackURI)
        {
            nsISHistoryListener b = (nsISHistoryListener)_browser;
            if (b != null)
                return b.OnHistoryGoBack(aBackURI);
            else
                return false;
        }

        public bool OnHistoryGoForward(nsIURI aForwardURI)
        {
            nsISHistoryListener b = (nsISHistoryListener)_browser;
            if (b != null)
                return b.OnHistoryGoForward(aForwardURI);
            else
                return false;
        }

        public bool OnHistoryReload(nsIURI aReloadURI, uint aReloadFlags)
        {
            nsISHistoryListener b = (nsISHistoryListener)_browser;
            if (b != null)
                return b.OnHistoryReload(aReloadURI, aReloadFlags);
            else
                return false;
        }

        public bool OnHistoryGotoIndex(int aIndex, nsIURI aGotoURI)
        {
            nsISHistoryListener b = (nsISHistoryListener)_browser;
            if (b != null)
                return b.OnHistoryGotoIndex(aIndex, aGotoURI);
            else
                return false;
        }

        public bool OnHistoryPurge(int aNumEntries)
        {
            nsISHistoryListener b = (nsISHistoryListener)_browser;
            if (b != null)
                return b.OnHistoryPurge(aNumEntries);
            else
                return false;
        }

	    public void OnHistoryReplaceEntry(int aIndex)
	    {
	        throw new NotImplementedException();
	    }
    }
}
