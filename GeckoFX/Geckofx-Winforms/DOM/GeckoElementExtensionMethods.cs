using System.Drawing;
using System.Runtime.InteropServices;

namespace Gecko.DOM
{
    public static class GeckoElementExtensionMethods
	{
		/// <summary>
		/// UI specific implementation extension method GetBoundingClientRect()
		/// </summary>
		/// <param name="element"></param>
		/// <returns></returns>
		public static System.Drawing.Rectangle GetBoundingClientRect( this GeckoElement element )
		{
			nsIDOMClientRect domRect = element.DOMElement.GetBoundingClientRect();
			if ( domRect == null ) return Rectangle.Empty;
			var r = new Rectangle(
				( int ) domRect.GetLeftAttribute(),
				( int ) domRect.GetTopAttribute(),
				( int ) domRect.GetWidthAttribute(),
				( int ) domRect.GetHeightAttribute() );
			Marshal.ReleaseComObject( domRect );
			return r;

		}
	}
}
