using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace Gecko
{
	/// <summary>
	/// Use the DocumentCompleted event instead of the NavigateFinished event.
	/// </summary>
	[Obsolete("Use the DocumentCompleted event instead of the NavigateFinished event.",false)]
	public class NavigateFinishedNotifier : IDisposable
	{
		GeckoWebBrowser m_browser;

		public NavigateFinishedNotifier(GeckoWebBrowser browser)
		{
			m_browser = browser;
		}		
		
		/// <summary>
		/// This method is only intended to be used by unittests
		/// The normal way to know when a document has finished loading is to listen for the DocumentCompleted event.
		/// </summary>
		public void BlockUntilNavigationFinished()
		{
			bool done = false;
			m_browser.DocumentCompleted += (sender, e) => done = true;
			m_browser.NavigationError += (sender, e) => done = true;
			m_browser.Retargeted += (sender, e) => done = true;
			while (!done)
			{
				Application.DoEvents();
				Application.RaiseIdle(new EventArgs());
			}
		}

		[Obsolete("Use the DocumentCompleted event instead")]
		public event EventHandler NavigateFinished;

		public void Dispose()
		{
			GC.SuppressFinalize(this);
		}
	}
}
