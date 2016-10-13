using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Shapes;
using Gecko;

namespace Gecko.DOM
{
	public static class GeckoElementExtensionMethods
	{
		/// <summary>
		/// UI specific implementation extension method GetBoundingClientRect()
		/// </summary>
		/// <param name="element"></param>
		/// <returns></returns>
		public static System.Windows.Rect GetBoundingClientRect( this GeckoElement element )
		{
			nsIDOMClientRect domRect = element.DOMElement.GetBoundingClientRect();
			if ( domRect == null ) return System.Windows.Rect.Empty;
			var r = new System.Windows.Rect(
				domRect.GetLeftAttribute(),
				domRect.GetTopAttribute(),
				domRect.GetWidthAttribute(),
				domRect.GetHeightAttribute() );
			return r;
		}

		/// <summary>
		/// UI specific implementation extension method GetBoundingClientRect()
		/// </summary>
		/// <param name="element"></param>
		/// <returns></returns>
		public static System.Windows.Int32Rect GetBoundingClientRectInt32( this GeckoElement element )
		{
			nsIDOMClientRect domRect = element.DOMElement.GetBoundingClientRect();
			if ( domRect == null ) return System.Windows.Int32Rect.Empty;
			var r = new System.Windows.Int32Rect(
				( int ) domRect.GetLeftAttribute(),
				( int ) domRect.GetTopAttribute(),
				( int ) domRect.GetWidthAttribute(),
				( int ) domRect.GetHeightAttribute() );
			return r;

		}
	}
}
