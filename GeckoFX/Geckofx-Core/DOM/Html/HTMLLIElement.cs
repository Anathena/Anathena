

using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace Gecko.DOM
{	
	public class GeckoLIElement : GeckoHtmlElement
	{
		nsIDOMHTMLLIElement DOMHTMLElement;
		internal GeckoLIElement(nsIDOMHTMLLIElement element) : base(element)
		{
			this.DOMHTMLElement = element;
		}
		public GeckoLIElement(object element) : base(element as nsIDOMHTMLElement)
		{
			this.DOMHTMLElement = element as nsIDOMHTMLLIElement;
		}
		public string Type {
			get { return nsString.Get(DOMHTMLElement.GetTypeAttribute); }
			set { DOMHTMLElement.SetTypeAttribute(new nsAString(value)); }
		}

		public int Value {
			get { return DOMHTMLElement.GetValueAttribute(); }
			set { DOMHTMLElement.SetValueAttribute(value); }
		}

	}
}

