

using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace Gecko.DOM
{	
	public class GeckoObjectElement : GeckoHtmlElement
	{
		nsIDOMHTMLObjectElement DOMHTMLElement;
		internal GeckoObjectElement(nsIDOMHTMLObjectElement element) : base(element)
		{
			this.DOMHTMLElement = element;
		}
		public GeckoObjectElement(object element) : base(element as nsIDOMHTMLElement)
		{
			this.DOMHTMLElement = element as nsIDOMHTMLObjectElement;
		}
		public GeckoFormElement Form {
			get { return new GeckoFormElement(DOMHTMLElement.GetFormAttribute()); }
		}

		public string Code {
			get { return nsString.Get(DOMHTMLElement.GetCodeAttribute); }
			set { DOMHTMLElement.SetCodeAttribute(new nsAString(value)); }
		}

		public string Align {
			get { return nsString.Get(DOMHTMLElement.GetAlignAttribute); }
			set { DOMHTMLElement.SetAlignAttribute(new nsAString(value)); }
		}

		public string Archive {
			get { return nsString.Get(DOMHTMLElement.GetArchiveAttribute); }
			set { DOMHTMLElement.SetArchiveAttribute(new nsAString(value)); }
		}

		public string Border {
			get { return nsString.Get(DOMHTMLElement.GetBorderAttribute); }
			set { DOMHTMLElement.SetBorderAttribute(new nsAString(value)); }
		}

		public string CodeBase {
			get { return nsString.Get(DOMHTMLElement.GetCodeBaseAttribute); }
			set { DOMHTMLElement.SetCodeBaseAttribute(new nsAString(value)); }
		}

		public string CodeType {
			get { return nsString.Get(DOMHTMLElement.GetCodeTypeAttribute); }
			set { DOMHTMLElement.SetCodeTypeAttribute(new nsAString(value)); }
		}

		public string Data {
			get { return nsString.Get(DOMHTMLElement.GetDataAttribute); }
			set { DOMHTMLElement.SetDataAttribute(new nsAString(value)); }
		}

		public bool Declare {
			get { return DOMHTMLElement.GetDeclareAttribute(); }
			set { DOMHTMLElement.SetDeclareAttribute(value); }
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

		public string Standby {
			get { return nsString.Get(DOMHTMLElement.GetStandbyAttribute); }
			set { DOMHTMLElement.SetStandbyAttribute(new nsAString(value)); }
		}

		public string Type {
			get { return nsString.Get(DOMHTMLElement.GetTypeAttribute); }
			set { DOMHTMLElement.SetTypeAttribute(new nsAString(value)); }
		}

		public string UseMap {
			get { return nsString.Get(DOMHTMLElement.GetUseMapAttribute); }
			set { DOMHTMLElement.SetUseMapAttribute(new nsAString(value)); }
		}

		public int Vspace {
			get { return DOMHTMLElement.GetVspaceAttribute(); }
			set { DOMHTMLElement.SetVspaceAttribute(value); }
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

