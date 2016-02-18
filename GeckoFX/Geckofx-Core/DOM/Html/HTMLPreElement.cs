

using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace Gecko.DOM
{	
	public class GeckoPreElement : GeckoHtmlElement
	{
		nsIDOMHTMLPreElement DOMHTMLElement;
		internal GeckoPreElement(nsIDOMHTMLPreElement element) : base(element)
		{
			this.DOMHTMLElement = element;
		}
		public GeckoPreElement(object element) : base(element as nsIDOMHTMLElement)
		{
			this.DOMHTMLElement = element as nsIDOMHTMLPreElement;
		}
		public int Width {
			get { return DOMHTMLElement.GetWidthAttribute(); }
			set { DOMHTMLElement.SetWidthAttribute(value); }
		}

	}
}

