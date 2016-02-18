

using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace Gecko.DOM
{
	public class GeckoIFrameElement : GeckoHtmlElement
	{
		nsIDOMHTMLIFrameElement DOMHTMLElement;
		internal GeckoIFrameElement(nsIDOMHTMLIFrameElement element) : base(element)
		{
			this.DOMHTMLElement = element;
		}
		public GeckoIFrameElement(object element) : base(element as nsIDOMHTMLElement)
		{
			this.DOMHTMLElement = element as nsIDOMHTMLIFrameElement;
		}
		public string Align {
			get { return nsString.Get(DOMHTMLElement.GetAlignAttribute); }
			set { DOMHTMLElement.SetAlignAttribute(new nsAString(value)); }
		}

		public string FrameBorder {
			get { return nsString.Get(DOMHTMLElement.GetFrameBorderAttribute); }
			set { DOMHTMLElement.SetFrameBorderAttribute(new nsAString(value)); }
		}

		public string Height {
			get { return nsString.Get(DOMHTMLElement.GetHeightAttribute); }
			set { DOMHTMLElement.SetHeightAttribute(new nsAString(value)); }
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

		public string Scrolling {
			get { return nsString.Get(DOMHTMLElement.GetScrollingAttribute); }
			set { DOMHTMLElement.SetScrollingAttribute(new nsAString(value)); }
		}

		public string Src {
			get { return nsString.Get(DOMHTMLElement.GetSrcAttribute); }
			set { DOMHTMLElement.SetSrcAttribute(new nsAString(value)); }
		}

		public string Width {
			get { return nsString.Get(DOMHTMLElement.GetWidthAttribute); }
			set { DOMHTMLElement.SetWidthAttribute(new nsAString(value)); }
		}

		public GeckoDocument ContentDocument {
			get {
				var doc = DOMHTMLElement.GetContentDocumentAttribute() as nsIDOMHTMLDocument;
				return (doc == null) ? null : new GeckoDocument(doc);
			}
		}

	}
}

