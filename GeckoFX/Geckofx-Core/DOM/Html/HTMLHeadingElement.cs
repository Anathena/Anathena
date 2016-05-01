namespace Gecko.DOM
{
    public class GeckoHeadingElement : GeckoHtmlElement
	{
		nsIDOMHTMLHeadingElement DOMHTMLElement;
		internal GeckoHeadingElement(nsIDOMHTMLHeadingElement element) : base(element)
		{
			this.DOMHTMLElement = element;
		}
		public GeckoHeadingElement(object element) : base(element as nsIDOMHTMLElement)
		{
			this.DOMHTMLElement = element as nsIDOMHTMLHeadingElement;
		}
		public string Align {
			get { return nsString.Get(DOMHTMLElement.GetAlignAttribute); }
			set { DOMHTMLElement.SetAlignAttribute(new nsAString(value)); }
		}

	}
}

