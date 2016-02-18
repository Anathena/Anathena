

using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace Gecko.DOM
{	
	public class GeckoImageElement : GeckoHtmlElement
	{
		nsIDOMHTMLImageElement DOMHTMLElement;
		internal GeckoImageElement(nsIDOMHTMLImageElement element) : base(element)
		{
			this.DOMHTMLElement = element;
		}
		public GeckoImageElement(object element) : base(element as nsIDOMHTMLElement)
		{
			this.DOMHTMLElement = element as nsIDOMHTMLImageElement;
		}
		public string Name {
			get { return nsString.Get(DOMHTMLElement.GetNameAttribute); }
			set { DOMHTMLElement.SetNameAttribute(new nsAString(value)); }
		}

		public string Align {
			get { return nsString.Get(DOMHTMLElement.GetAlignAttribute); }
			set { DOMHTMLElement.SetAlignAttribute(new nsAString(value)); }
		}

		public string Alt {
			get { return nsString.Get(DOMHTMLElement.GetAltAttribute); }
			set { DOMHTMLElement.SetAltAttribute(new nsAString(value)); }
		}

		public string Border {
			get { return nsString.Get(DOMHTMLElement.GetBorderAttribute); }
			set { DOMHTMLElement.SetBorderAttribute(new nsAString(value)); }
		}

		public uint Height {
			get { return DOMHTMLElement.GetHeightAttribute(); }
			set { DOMHTMLElement.SetHeightAttribute(value); }
		}

		public int Hspace {
			get { return DOMHTMLElement.GetHspaceAttribute(); }
			set { DOMHTMLElement.SetHspaceAttribute(value); }
		}

		public bool IsMap {
			get { return DOMHTMLElement.GetIsMapAttribute(); }
			set { DOMHTMLElement.SetIsMapAttribute(value); }
		}

		public string LongDesc {
			get { return nsString.Get(DOMHTMLElement.GetLongDescAttribute); }
			set { DOMHTMLElement.SetLongDescAttribute(new nsAString(value)); }
		}

		public string Src {
			get { return nsString.Get(DOMHTMLElement.GetSrcAttribute); }
			set { DOMHTMLElement.SetSrcAttribute(new nsAString(value)); }
		}

		public string UseMap {
			get { return nsString.Get(DOMHTMLElement.GetUseMapAttribute); }
			set { DOMHTMLElement.SetUseMapAttribute(new nsAString(value)); }
		}

		public int Vspace {
			get { return DOMHTMLElement.GetVspaceAttribute(); }
			set { DOMHTMLElement.SetVspaceAttribute(value); }
		}

		public uint Width {
			get { return DOMHTMLElement.GetWidthAttribute(); }
			set { DOMHTMLElement.SetWidthAttribute(value); }
		}

	}
}

