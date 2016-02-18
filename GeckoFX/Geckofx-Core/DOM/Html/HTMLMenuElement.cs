

using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace Gecko.DOM
{	
	public class GeckoMenuElement : GeckoHtmlElement
	{
		nsIDOMHTMLMenuElement DOMHTMLElement;
		internal GeckoMenuElement(nsIDOMHTMLMenuElement element) : base(element)
		{
			this.DOMHTMLElement = element;
		}
		public GeckoMenuElement(object element) : base(element as nsIDOMHTMLElement)
		{
			this.DOMHTMLElement = element as nsIDOMHTMLMenuElement;
		}
		public bool Compact {
			get { return DOMHTMLElement.GetCompactAttribute(); }
			set { DOMHTMLElement.SetCompactAttribute(value); }
		}

	}
}

