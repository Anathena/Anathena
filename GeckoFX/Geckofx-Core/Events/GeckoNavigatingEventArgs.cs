using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace Gecko.Events
{
	#region GeckoNavigatingEventArgs
	/// <summary>Provides data for event.</summary>
	public class GeckoNavigatingEventArgs
		: CancelEventArgs
	{
		public readonly Uri Uri;
		public readonly GeckoWindow DomWindow;
		public readonly bool DomWindowTopLevel;

		/// <summary>Creates a new instance of a <see cref="GeckoNavigatingEventArgs"/> object.</summary>
		/// <param name="value"></param>
		public GeckoNavigatingEventArgs(Uri value, GeckoWindow domWind)
		{
			Uri = value;
			DomWindow = domWind;
			DomWindowTopLevel = domWind.IsTopWindow();
		}
	}
	#endregion
}
