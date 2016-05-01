namespace Gecko.DOM
{
    public class GeckoHeadElement : GeckoHtmlElement
	{
		nsIDOMHTMLHeadElement DOMHTMLElement;
		internal GeckoHeadElement(nsIDOMHTMLHeadElement element) : base(element)
		{
			this.DOMHTMLElement = element;
		}
		public GeckoHeadElement(object element) : base(element as nsIDOMHTMLElement)
		{
			this.DOMHTMLElement = element as nsIDOMHTMLHeadElement;
		}		
	}
}

