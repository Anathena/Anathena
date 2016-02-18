using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gecko.Events
{
	public class GeckoDocumentCompletedEventArgs : EventArgs
	{
		public Uri Uri { get; private set; }
		public GeckoWindow Window { get; private set; }
		public bool IsTopLevel { get; private set; }

		public GeckoDocumentCompletedEventArgs(Uri uri, GeckoWindow window)
		{
			this.Uri = uri;
			this.Window = window;
			this.IsTopLevel = window.IsTopWindow();
		}
	}
}