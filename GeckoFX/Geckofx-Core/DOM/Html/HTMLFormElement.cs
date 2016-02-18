

using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace Gecko.DOM
{	
	public class GeckoFormElement : GeckoHtmlElement
	{
		nsIDOMHTMLFormElement DOMHTMLElement;
		internal GeckoFormElement(nsIDOMHTMLFormElement element) : base(element)
		{
			this.DOMHTMLElement = element;
		}
		public GeckoFormElement(object element) : base(element as nsIDOMHTMLElement)
		{
			this.DOMHTMLElement = element as nsIDOMHTMLFormElement;
		}
		public GeckoHtmlElementCollection Elements {
			get { return new GeckoHtmlElementCollection(DOMHTMLElement.GetElementsAttribute()); }
		}

		public int Length {
			get { return DOMHTMLElement.GetLengthAttribute(); }
		}

		public string Name {
			get { return nsString.Get(DOMHTMLElement.GetNameAttribute); }
			set { DOMHTMLElement.SetNameAttribute(new nsAString(value)); }
		}

		public string AcceptCharset {
			get { return nsString.Get(DOMHTMLElement.GetAcceptCharsetAttribute); }
			set { DOMHTMLElement.SetAcceptCharsetAttribute(new nsAString(value)); }
		}

		public string Action {
			get { return nsString.Get(DOMHTMLElement.GetActionAttribute); }
			set { DOMHTMLElement.SetActionAttribute(new nsAString(value)); }
		}

		public string Enctype {
			get { return nsString.Get(DOMHTMLElement.GetEnctypeAttribute); }
			set { DOMHTMLElement.SetEnctypeAttribute(new nsAString(value)); }
		}

		public string Method {
			get { return nsString.Get(DOMHTMLElement.GetMethodAttribute); }
			set { DOMHTMLElement.SetMethodAttribute(new nsAString(value)); }
		}

		public string Target {
			get { return nsString.Get(DOMHTMLElement.GetTargetAttribute); }
			set { DOMHTMLElement.SetTargetAttribute(new nsAString(value)); }
		}

		public void submit()
		{
			DOMHTMLElement.Submit();
		}

		public void reset()
		{
			DOMHTMLElement.Reset();
		}

	}
}

