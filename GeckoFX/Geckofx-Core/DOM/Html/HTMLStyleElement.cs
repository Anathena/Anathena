

using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace Gecko.DOM
{	
	public class GeckoStyleElement : GeckoHtmlElement
	{
		nsIDOMHTMLStyleElement DOMHTMLElement;
		internal GeckoStyleElement(nsIDOMHTMLStyleElement element) : base(element)
		{
			this.DOMHTMLElement = element;
		}
		public GeckoStyleElement(object element) : base(element as nsIDOMHTMLElement)
		{
			this.DOMHTMLElement = element as nsIDOMHTMLStyleElement;
		}
		public bool Disabled {
			get { return DOMHTMLElement.GetDisabledAttribute(); }
			set { DOMHTMLElement.SetDisabledAttribute(value); }
		}

		public string Media {
			get { return nsString.Get(DOMHTMLElement.GetMediaAttribute); }
			set { DOMHTMLElement.SetMediaAttribute(new nsAString(value)); }
		}

		public string Type {
			get { return nsString.Get(DOMHTMLElement.GetTypeAttribute); }
			set { DOMHTMLElement.SetTypeAttribute(new nsAString(value)); }
		}

	}
}

