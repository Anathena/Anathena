

using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace Gecko.DOM
{	
	public class GeckoOptGroupElement : GeckoHtmlElement
	{
		nsIDOMHTMLOptGroupElement DOMHTMLElement;
		internal GeckoOptGroupElement(nsIDOMHTMLOptGroupElement element) : base(element)
		{
			this.DOMHTMLElement = element;
		}
		public GeckoOptGroupElement(object element) : base(element as nsIDOMHTMLElement)
		{
			this.DOMHTMLElement = element as nsIDOMHTMLOptGroupElement;
		}
		public bool Disabled {
			get { return DOMHTMLElement.GetDisabledAttribute(); }
			set { DOMHTMLElement.SetDisabledAttribute(value); }
		}

		public string Label {
			get { return nsString.Get(DOMHTMLElement.GetLabelAttribute); }
			set { DOMHTMLElement.SetLabelAttribute(new nsAString(value)); }
		}

	}
}

