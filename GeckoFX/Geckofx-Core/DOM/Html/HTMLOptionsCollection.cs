

using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace Gecko.DOM
{	
	public class GeckoOptionsCollection
	{
		nsIDOMHTMLOptionsCollection DOMHTMLElement;
		internal GeckoOptionsCollection(nsIDOMHTMLOptionsCollection element)
		{
			this.DOMHTMLElement = element;
		}
		public uint Length {
			get { return DOMHTMLElement.GetLengthAttribute(); }
			set { DOMHTMLElement.SetLengthAttribute(value); }
		}

		public GeckoOptionElement item(uint index)
		{
			return new GeckoOptionElement(DOMHTMLElement.Item(index));
		}

		public GeckoOptionElement namedItem(string name)
		{
			return new GeckoOptionElement(DOMHTMLElement.NamedItem(new nsAString(name)));
		}

	}
}

