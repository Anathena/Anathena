

using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace Gecko.DOM
{	
	public class GeckoParagraphElement : GeckoHtmlElement
	{
		nsIDOMHTMLParagraphElement DOMHTMLElement;
		internal GeckoParagraphElement(nsIDOMHTMLParagraphElement element) : base(element)
		{
			this.DOMHTMLElement = element;
		}
		public GeckoParagraphElement(object element) : base(element as nsIDOMHTMLElement)
		{
			this.DOMHTMLElement = element as nsIDOMHTMLParagraphElement;
		}
		public string Align {
			get { return nsString.Get(DOMHTMLElement.GetAlignAttribute); }
			set { DOMHTMLElement.SetAlignAttribute(new nsAString(value)); }
		}

	}
}

