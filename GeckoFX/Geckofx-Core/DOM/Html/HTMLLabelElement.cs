

using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace Gecko.DOM
{	
	public class GeckoLabelElement : GeckoHtmlElement
	{
		nsIDOMHTMLLabelElement DOMHTMLElement;
		internal GeckoLabelElement(nsIDOMHTMLLabelElement element) : base(element)
		{
			this.DOMHTMLElement = element;
		}
		public GeckoLabelElement(object element) : base(element as nsIDOMHTMLElement)
		{
			this.DOMHTMLElement = element as nsIDOMHTMLLabelElement;
		}
		public GeckoFormElement Form {
			get { return new GeckoFormElement(DOMHTMLElement.GetFormAttribute()); }
		}

		public string HtmlFor {
			get { return nsString.Get(DOMHTMLElement.GetHtmlForAttribute); }
			set { DOMHTMLElement.SetHtmlForAttribute(new nsAString(value)); }
		}

	}
}

