namespace Gecko.DOM
{
    public class GeckoMapElement : GeckoHtmlElement
	{
		nsIDOMHTMLMapElement DOMHTMLElement;
		internal GeckoMapElement(nsIDOMHTMLMapElement element) : base(element)
		{
			this.DOMHTMLElement = element;
		}
		public GeckoMapElement(object element) : base(element as nsIDOMHTMLElement)
		{
			this.DOMHTMLElement = element as nsIDOMHTMLMapElement;
		}
		public GeckoHtmlElementCollection Areas {
			get { return new GeckoHtmlElementCollection(DOMHTMLElement.GetAreasAttribute()); }
		}

		public string Name {
			get { return nsString.Get(DOMHTMLElement.GetNameAttribute); }
			set { DOMHTMLElement.SetNameAttribute(new nsAString(value)); }
		}

	}
}

