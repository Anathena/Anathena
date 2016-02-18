

using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace Gecko.DOM
{	
	public class GeckoHeadElement : GeckoHtmlElement
	{
		nsIDOMHTMLHeadElement DOMHTMLElement;
		internal GeckoHeadElement(nsIDOMHTMLHeadElement element) : base(element)
		{
			this.DOMHTMLElement = element;
		}
		public GeckoHeadElement(object element) : base(element as nsIDOMHTMLElement)
		{
			this.DOMHTMLElement = element as nsIDOMHTMLHeadElement;
		}		
	}
}

