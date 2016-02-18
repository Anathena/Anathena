

using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace Gecko.DOM
{	
	public class GeckoAppletElement : GeckoHtmlElement
	{
		nsIDOMHTMLAppletElement DOMHTMLElement;
		internal GeckoAppletElement(nsIDOMHTMLAppletElement element) : base(element)
		{
			this.DOMHTMLElement = element;
		}
		public GeckoAppletElement(object element) : base(element as nsIDOMHTMLElement)
		{
			this.DOMHTMLElement = element as nsIDOMHTMLAppletElement;
		}
		public string Align {
			get { return nsString.Get(DOMHTMLElement.GetAlignAttribute); }
			set { DOMHTMLElement.SetAlignAttribute(new nsAString(value)); }
		}

		public string Alt {
			get { return nsString.Get(DOMHTMLElement.GetAltAttribute); }
			set { DOMHTMLElement.SetAltAttribute(new nsAString(value)); }
		}

		public string Archive {
			get { return nsString.Get(DOMHTMLElement.GetArchiveAttribute); }
			set { DOMHTMLElement.SetArchiveAttribute(new nsAString(value)); }
		}

		public string Code {
			get { return nsString.Get(DOMHTMLElement.GetCodeAttribute); }
			set { DOMHTMLElement.SetCodeAttribute(new nsAString(value)); }
		}

		public string CodeBase {
			get { return nsString.Get(DOMHTMLElement.GetCodeBaseAttribute); }
			set { DOMHTMLElement.SetCodeBaseAttribute(new nsAString(value)); }
		}

		public string Height {
			get { return nsString.Get(DOMHTMLElement.GetHeightAttribute); }
			set { DOMHTMLElement.SetHeightAttribute(new nsAString(value)); }
		}

		public int Hspace {
			get { return DOMHTMLElement.GetHspaceAttribute(); }
			set { DOMHTMLElement.SetHspaceAttribute(value); }
		}

		public string Name {
			get { return nsString.Get(DOMHTMLElement.GetNameAttribute); }
			set { DOMHTMLElement.SetNameAttribute(new nsAString(value)); }
		}

		public string Object {
			get { return nsString.Get(DOMHTMLElement.GetObjectAttribute); }
			set { DOMHTMLElement.SetObjectAttribute(new nsAString(value)); }
		}

		public int Vspace {
			get { return DOMHTMLElement.GetVspaceAttribute(); }
			set { DOMHTMLElement.SetVspaceAttribute(value); }
		}

		public string Width {
			get { return nsString.Get(DOMHTMLElement.GetWidthAttribute); }
			set { DOMHTMLElement.SetWidthAttribute(new nsAString(value)); }
		}

	}
}

