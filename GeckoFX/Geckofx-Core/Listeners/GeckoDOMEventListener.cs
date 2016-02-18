using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gecko.Listeners
{
	[Obsolete]
    class GeckoDOMEventListener : GeckoBaseListener, nsIDOMEventListener
    {
        public GeckoDOMEventListener(nsIDOMEventListener p_broowser)
        {
            _weakBrowser = new System.WeakReference(p_broowser);
        }

        public void HandleEvent(nsIDOMEvent @event)
        {
            nsIDOMEventListener b = (nsIDOMEventListener)_browser;
            if (b != null)
                b.HandleEvent(@event);
        }
    }
}
