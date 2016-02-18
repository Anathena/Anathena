

using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace Gecko.DOM
{	
	public class GeckoFrameElement : GeckoHtmlElement
	{
		nsIDOMHTMLFrameElement DOMHTMLElement;
		internal GeckoFrameElement(nsIDOMHTMLFrameElement element) : base(element)
		{
			this.DOMHTMLElement = element;
		}
		public GeckoFrameElement(object element) : base(element as nsIDOMHTMLElement)
		{
			this.DOMHTMLElement = element as nsIDOMHTMLFrameElement;
		}
		public string FrameBorder {
			get { return nsString.Get(DOMHTMLElement.GetFrameBorderAttribute); }
			set { DOMHTMLElement.SetFrameBorderAttribute(new nsAString(value)); }
		}

		public string LongDesc {
			get { return nsString.Get(DOMHTMLElement.GetLongDescAttribute); }
			set { DOMHTMLElement.SetLongDescAttribute(new nsAString(value)); }
		}

		public string MarginHeight {
			get { return nsString.Get(DOMHTMLElement.GetMarginHeightAttribute); }
			set { DOMHTMLElement.SetMarginHeightAttribute(new nsAString(value)); }
		}

		public string MarginWidth {
			get { return nsString.Get(DOMHTMLElement.GetMarginWidthAttribute); }
			set { DOMHTMLElement.SetMarginWidthAttribute(new nsAString(value)); }
		}

		public string Name {
			get { return nsString.Get(DOMHTMLElement.GetNameAttribute); }
			set { DOMHTMLElement.SetNameAttribute(new nsAString(value)); }
		}

		public bool NoResize {
			get { return DOMHTMLElement.GetNoResizeAttribute(); }
			set { DOMHTMLElement.SetNoResizeAttribute(value); }
		}

		public string Scrolling {
			get { return nsString.Get(DOMHTMLElement.GetScrollingAttribute); }
			set { DOMHTMLElement.SetScrollingAttribute(new nsAString(value)); }
		}

		public string Src {
			get { return nsString.Get(DOMHTMLElement.GetSrcAttribute); }
			set { DOMHTMLElement.SetSrcAttribute(new nsAString(value)); }
		}

		public GeckoDocument ContentDocument {
			get {
				var doc = DOMHTMLElement.GetContentDocumentAttribute() as nsIDOMHTMLDocument;
				return (doc == null) ? null : new GeckoDocument(doc);
			}
		}

	}
}

