

using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace Gecko.DOM
{	
	public class GeckoDivElement : GeckoHtmlElement
	{
		nsIDOMHTMLDivElement DOMHTMLElement;
		internal GeckoDivElement(nsIDOMHTMLDivElement element) : base(element)
		{
			this.DOMHTMLElement = element;
		}
		public GeckoDivElement(object element) : base(element as nsIDOMHTMLElement)
		{
			this.DOMHTMLElement = element as nsIDOMHTMLDivElement;
		}
		public string Align {
			get { return nsString.Get(DOMHTMLElement.GetAlignAttribute); }
			set { DOMHTMLElement.SetAlignAttribute(new nsAString(value)); }
		}

	}
}

