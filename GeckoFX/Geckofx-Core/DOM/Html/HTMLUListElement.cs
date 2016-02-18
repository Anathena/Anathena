

using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace Gecko.DOM
{	
	public class GeckoUListElement : GeckoHtmlElement
	{
		nsIDOMHTMLUListElement DOMHTMLElement;
		internal GeckoUListElement(nsIDOMHTMLUListElement element) : base(element)
		{
			this.DOMHTMLElement = element;
		}
		public GeckoUListElement(object element) : base(element as nsIDOMHTMLElement)
		{
			this.DOMHTMLElement = element as nsIDOMHTMLUListElement;
		}
		public bool Compact {
			get { return DOMHTMLElement.GetCompactAttribute(); }
			set { DOMHTMLElement.SetCompactAttribute(value); }
		}

		public string Type {
			get { return nsString.Get(DOMHTMLElement.GetTypeAttribute); }
			set { DOMHTMLElement.SetTypeAttribute(new nsAString(value)); }
		}

	}
}

