

using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace Gecko.DOM
{	
	public class GeckoLinkElement : GeckoHtmlElement
	{
		nsIDOMHTMLLinkElement DOMHTMLElement;
		internal GeckoLinkElement(nsIDOMHTMLLinkElement element) : base(element)
		{
			this.DOMHTMLElement = element;
		}
		public GeckoLinkElement(object element) : base(element as nsIDOMHTMLElement)
		{
			this.DOMHTMLElement = element as nsIDOMHTMLLinkElement;
		}
		public bool Disabled {
			get { return DOMHTMLElement.GetDisabledAttribute(); }
			set { DOMHTMLElement.SetDisabledAttribute(value); }
		}

		public string Charset {
			get { return nsString.Get(DOMHTMLElement.GetCharsetAttribute); }
			set { DOMHTMLElement.SetCharsetAttribute(new nsAString(value)); }
		}

		public string Href {
			get { return nsString.Get(DOMHTMLElement.GetHrefAttribute); }
			set { DOMHTMLElement.SetHrefAttribute(new nsAString(value)); }
		}

		public string Hreflang {
			get { return nsString.Get(DOMHTMLElement.GetHreflangAttribute); }
			set { DOMHTMLElement.SetHreflangAttribute(new nsAString(value)); }
		}

		public string Media {
			get { return nsString.Get(DOMHTMLElement.GetMediaAttribute); }
			set { DOMHTMLElement.SetMediaAttribute(new nsAString(value)); }
		}

		public string Rel {
			get { return nsString.Get(DOMHTMLElement.GetRelAttribute); }
			set { DOMHTMLElement.SetRelAttribute(new nsAString(value)); }
		}

		public string Rev {
			get { return nsString.Get(DOMHTMLElement.GetRevAttribute); }
			set { DOMHTMLElement.SetRevAttribute(new nsAString(value)); }
		}

		public string Target {
			get { return nsString.Get(DOMHTMLElement.GetTargetAttribute); }
			set { DOMHTMLElement.SetTargetAttribute(new nsAString(value)); }
		}

		public string Type {
			get { return nsString.Get(DOMHTMLElement.GetTypeAttribute); }
			set { DOMHTMLElement.SetTypeAttribute(new nsAString(value)); }
		}

	}
}

