

using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace Gecko.DOM
{	
	public class GeckoSelectElement : GeckoHtmlElement
	{
		nsIDOMHTMLSelectElement DOMHTMLElement;
		internal GeckoSelectElement(nsIDOMHTMLSelectElement element) : base(element)
		{
			this.DOMHTMLElement = element;
		}
		public GeckoSelectElement(object element) : base(element as nsIDOMHTMLElement)
		{
			this.DOMHTMLElement = element as nsIDOMHTMLSelectElement;
		}
		public string Type {
			get { return nsString.Get(DOMHTMLElement.GetTypeAttribute); }
		}

		public int SelectedIndex {
			get { return DOMHTMLElement.GetSelectedIndexAttribute(); }
			set { DOMHTMLElement.SetSelectedIndexAttribute(value); }
		}

		public string Value {
			get { return nsString.Get(DOMHTMLElement.GetValueAttribute); }
			set { DOMHTMLElement.SetValueAttribute(new nsAString(value)); }
		}

		public uint Length {
			get { return DOMHTMLElement.GetLengthAttribute(); }
			set { DOMHTMLElement.SetLengthAttribute(value); }
		}

		public GeckoFormElement Form {
			get {
			  var formAttr = DOMHTMLElement.GetFormAttribute();
			  return formAttr == null ? null : new GeckoFormElement(formAttr);
			}
		}

		public GeckoOptionsCollection Options {
						get {
			  var optionsAttr = DOMHTMLElement.GetOptionsAttribute();
			  return optionsAttr == null ? null : new GeckoOptionsCollection(optionsAttr);
			}
		}

		public bool Disabled {
			get { return DOMHTMLElement.GetDisabledAttribute(); }
			set { DOMHTMLElement.SetDisabledAttribute(value); }
		}

		public bool Multiple {
			get { return DOMHTMLElement.GetMultipleAttribute(); }
			set { DOMHTMLElement.SetMultipleAttribute(value); }
		}

		public string Name {
			get { return nsString.Get(DOMHTMLElement.GetNameAttribute); }
			set { DOMHTMLElement.SetNameAttribute(new nsAString(value)); }
		}

		public uint Size {
			get { return DOMHTMLElement.GetSizeAttribute(); }
			set { DOMHTMLElement.SetSizeAttribute(value); }
		}

        public void add(GeckoHtmlElement element, GeckoHtmlElement before)
		{
            DOMHTMLElement.Add(element.DomObject as nsIDOMHTMLElement, before.DomObject as nsIVariant);
		}

		public void remove(int index)
		{
			DOMHTMLElement.Remove(index);
		}
	}
}
