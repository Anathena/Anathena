using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace Gecko.DOM
{	
	public class GeckoDirectoryElement : GeckoHtmlElement
	{
		nsIDOMHTMLDirectoryElement DOMHTMLElement;
		internal GeckoDirectoryElement(nsIDOMHTMLDirectoryElement element) : base(element)
		{
			this.DOMHTMLElement = element;
		}
		public GeckoDirectoryElement(object element) : base(element as nsIDOMHTMLElement)
		{
			this.DOMHTMLElement = element as nsIDOMHTMLDirectoryElement;
		}
	}
}

