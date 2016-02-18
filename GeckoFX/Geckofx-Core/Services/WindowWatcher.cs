using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Gecko.Interop;

namespace Gecko.Services
{
	public static class WindowWatcher
	{
		private static ComPtr<nsIWindowWatcher> _watcher;
		private static bool _windowCreatorLocked;

		static WindowWatcher()
		{
			_watcher = Xpcom.GetService2<nsIWindowWatcher>(Contracts.WindowWatcher);
		}

		public static void Shutdown()
		{
			if (_watcher != null)
				_watcher.Dispose();
			_watcher = null;
		}

		/// <summary>
		/// After calling this method changing of WindowCreator is not allowed
		/// </summary>
		public static void LockWindowCreator()
		{
			_windowCreatorLocked = true;
		}

		public static nsIWindowCreator2 WindowCreator
		{
			set
			{
				if (_windowCreatorLocked) return;
				_watcher.Instance.SetWindowCreator(value);

			}
		}

		public static GeckoWindow ActiveWindow
		{
			get { return _watcher.Instance.GetActiveWindowAttribute().Wrap(x=>new GeckoWindow(x)); }
			set
			{
				if (value != null)
					_watcher.Instance.SetActiveWindowAttribute( value.DomWindow );
			}
		}

		public static IEnumerator<GeckoWindow> GetWindowEnumerator()
		{
			return new Collections.GeckoEnumerator<GeckoWindow, nsIDOMWindow>(
				_watcher.Instance.GetWindowEnumerator(),
				x => new GeckoWindow( x ) );
		}


	}
}
