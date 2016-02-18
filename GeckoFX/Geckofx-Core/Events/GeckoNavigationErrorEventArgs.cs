using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace Gecko.Events
{
	#region GeckoNavigationErrorEventArgs
	/// <summary>Provides data for event.</summary>
	public class GeckoNavigationErrorEventArgs
		: EventArgs
	{
		public readonly GeckoWindow DomWindow;
		public readonly Boolean DomWindowTopLevel;
		/// <summary>
		/// Values are usually defined in <see cref="Gecko.GeckoError"/>.
		/// Typical values are:
		///   - NS_BINDING_ABORTED: navigation cancelled by user 
		///     (call GeckoWebBrowser.Stop() or cancel GeckoNavigatingEventArgs).
		///   - NS_ERROR_CONNECTION_REFUSED
		///   - NS_ERROR_PORT_ACCESS_NOT_ALLOWED
		///   - NS_ERROR_FILE_NOT_FOUND: local file not found
		///   - NS_ERROR_FILE_UNRECOGNIZED_PATH: local file path invalid for the platform, e.g. file://a on windows.
		///   - NS_ERROR_FAILURE: illformed url, e.g. chrome://global/bindings/general.xml (mising 'content').
		///   - NS_ERROR_NOT_AVAILABLE: not registered special url, e.g. chrome://yyy/content/a
		/// </summary>
		public readonly int ErrorCode;
		public readonly string Uri;

		/// <summary>Creates a new instance of a <see cref="GeckoNavigatedEventArgs"/> object.</summary>
		/// <param name="value"></param>
		/// <param name="response"></param>
		internal GeckoNavigationErrorEventArgs(string uri, GeckoWindow domWind, int errorCode)
		{
			Uri = uri;
			DomWindow = domWind;
			DomWindowTopLevel = (domWind == null) || domWind.IsTopWindow();
			ErrorCode = errorCode;
		}
	}
	#endregion
}
