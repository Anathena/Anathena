

using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace Gecko.DOM
{	
	public class GeckoOptionElement : GeckoHtmlElement
	{
		nsIDOMHTMLOptionElement DOMHTMLElement;
		internal GeckoOptionElement(nsIDOMHTMLOptionElement element) : base(element)
		{
			this.DOMHTMLElement = element;
		}
		public GeckoOptionElement(object element) : base(element as nsIDOMHTMLElement)
		{
			this.DOMHTMLElement = element as nsIDOMHTMLOptionElement;
		}
		public GeckoFormElement Form {
			get { return new GeckoFormElement(DOMHTMLElement.GetFormAttribute()); }
		}

		public bool DefaultSelected {
			get { return DOMHTMLElement.GetDefaultSelectedAttribute(); }
			set { DOMHTMLElement.SetDefaultSelectedAttribute(value); }
		}

		public string Text {
			get { return nsString.Get(DOMHTMLElement.GetTextAttribute); }
		}

		public int Index {
			get { return DOMHTMLElement.GetIndexAttribute(); }
		}

		public bool Disabled {
			get { return DOMHTMLElement.GetDisabledAttribute(); }
			set { DOMHTMLElement.SetDisabledAttribute(value); }
		}

		public string Label {
			get { return nsString.Get(DOMHTMLElement.GetLabelAttribute); }
			set { DOMHTMLElement.SetLabelAttribute(new nsAString(value)); }
		}

		public bool Selected {
			get { return DOMHTMLElement.GetSelectedAttribute(); }
			set { DOMHTMLElement.SetSelectedAttribute(value); }
		}

		public string Value {
			get { return nsString.Get(DOMHTMLElement.GetValueAttribute); }
			set { DOMHTMLElement.SetValueAttribute(new nsAString(value)); }
		}

	}
}
