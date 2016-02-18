

using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace Gecko.DOM
{	
	public class GeckoScriptElement : GeckoHtmlElement
	{
		nsIDOMHTMLScriptElement DOMHTMLElement;
		internal GeckoScriptElement(nsIDOMHTMLScriptElement element) : base(element)
		{
			this.DOMHTMLElement = element;
		}
		public GeckoScriptElement(object element) : base(element as nsIDOMHTMLElement)
		{
			this.DOMHTMLElement = element as nsIDOMHTMLScriptElement;
		}
		public string Text {
			get { return nsString.Get(DOMHTMLElement.GetTextAttribute); }
			set { DOMHTMLElement.SetTextAttribute(new nsAString(value)); }
		}

		public string HtmlFor {
			get { return nsString.Get(DOMHTMLElement.GetHtmlForAttribute); }
			set { DOMHTMLElement.SetHtmlForAttribute(new nsAString(value)); }
		}

		public string Event {
			get { return nsString.Get(DOMHTMLElement.GetEventAttribute); }
			set { DOMHTMLElement.SetEventAttribute(new nsAString(value)); }
		}

		public string Charset {
			get { return nsString.Get(DOMHTMLElement.GetCharsetAttribute); }
			set { DOMHTMLElement.SetCharsetAttribute(new nsAString(value)); }
		}

		public bool Defer {
			get { return DOMHTMLElement.GetDeferAttribute(); }
			set { DOMHTMLElement.SetDeferAttribute(value); }
		}

		public string Src {
			get { return nsString.Get(DOMHTMLElement.GetSrcAttribute); }
			set { DOMHTMLElement.SetSrcAttribute(new nsAString(value)); }
		}

		public string Type {
			get { return nsString.Get(DOMHTMLElement.GetTypeAttribute); }
			set { DOMHTMLElement.SetTypeAttribute(new nsAString(value)); }
		}

	}
}

