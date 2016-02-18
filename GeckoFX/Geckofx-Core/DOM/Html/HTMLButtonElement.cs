using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace Gecko.DOM
{	
	public class GeckoButtonElement : GeckoHtmlElement
	{
		nsIDOMHTMLButtonElement DOMHTMLElement;
		internal GeckoButtonElement(nsIDOMHTMLButtonElement element) : base(element)
		{
			this.DOMHTMLElement = element;
		}
		public GeckoButtonElement(object element) : base(element as nsIDOMHTMLElement)
		{
			this.DOMHTMLElement = element as nsIDOMHTMLButtonElement;
		}
		public GeckoFormElement Form {
			get { return new GeckoFormElement(DOMHTMLElement.GetFormAttribute()); }
		}

		public bool Disabled {
			get { return DOMHTMLElement.GetDisabledAttribute(); }
			set { DOMHTMLElement.SetDisabledAttribute(value); }
		}

		public string Name {
			get { return nsString.Get(DOMHTMLElement.GetNameAttribute); }
			set { DOMHTMLElement.SetNameAttribute(new nsAString(value)); }
		}

		public string Type {
			get { return nsString.Get(DOMHTMLElement.GetTypeAttribute); }
		}

		public string Value {
			get { return nsString.Get(DOMHTMLElement.GetValueAttribute); }
			set { DOMHTMLElement.SetValueAttribute(new nsAString(value)); }
		}

	}
}

