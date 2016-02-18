using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace Gecko.DOM
{	
	public class GeckoInputElement : GeckoHtmlElement
	{
		nsIDOMHTMLInputElement DOMHTMLElement;
		internal GeckoInputElement(nsIDOMHTMLInputElement element) : base(element)
		{
			this.DOMHTMLElement = element;
		}
		public GeckoInputElement(object element) : base(element as nsIDOMHTMLElement)
		{
			this.DOMHTMLElement = element as nsIDOMHTMLInputElement;
		}
		public string DefaultValue {
			get { return nsString.Get(DOMHTMLElement.GetDefaultValueAttribute); }
			set { DOMHTMLElement.SetDefaultValueAttribute(new nsAString(value)); }
		}

		public bool DefaultChecked {
			get { return DOMHTMLElement.GetDefaultCheckedAttribute(); }
			set { DOMHTMLElement.SetDefaultCheckedAttribute(value); }
		}

		public GeckoFormElement Form {
			get { return new GeckoFormElement(DOMHTMLElement.GetFormAttribute()); }
		}

		public string Accept {
			get { return nsString.Get(DOMHTMLElement.GetAcceptAttribute); }
			set { DOMHTMLElement.SetAcceptAttribute(new nsAString(value)); }
		}

		public string Align {
			get { return nsString.Get(DOMHTMLElement.GetAlignAttribute); }
			set { DOMHTMLElement.SetAlignAttribute(new nsAString(value)); }
		}

		public string Alt {
			get { return nsString.Get(DOMHTMLElement.GetAltAttribute); }
			set { DOMHTMLElement.SetAltAttribute(new nsAString(value)); }
		}

		public bool Checked {
			get { return DOMHTMLElement.GetCheckedAttribute(); }
			set { DOMHTMLElement.SetCheckedAttribute(value); }
		}

		public bool Disabled {
			get { return DOMHTMLElement.GetDisabledAttribute(); }
			set { DOMHTMLElement.SetDisabledAttribute(value); }
		}

		public int MaxLength {
			get { return DOMHTMLElement.GetMaxLengthAttribute(); }
			set { DOMHTMLElement.SetMaxLengthAttribute(value); }
		}

		public string Name {
			get { return nsString.Get(DOMHTMLElement.GetNameAttribute); }
			set { DOMHTMLElement.SetNameAttribute(new nsAString(value)); }
		}

		public bool ReadOnly {
			get { return DOMHTMLElement.GetReadOnlyAttribute(); }
			set { DOMHTMLElement.SetReadOnlyAttribute(value); }
		}

		public uint Size {
			get { return DOMHTMLElement.GetSizeAttribute(); }
			set { DOMHTMLElement.SetSizeAttribute(value); }
		}

		public string Src {
			get { return nsString.Get(DOMHTMLElement.GetSrcAttribute); }
			set { DOMHTMLElement.SetSrcAttribute(new nsAString(value)); }
		}

		public string Type {
			get { return nsString.Get(DOMHTMLElement.GetTypeAttribute); }
			set { DOMHTMLElement.SetTypeAttribute(new nsAString(value)); }
		}

		public string UseMap {
			get { return nsString.Get(DOMHTMLElement.GetUseMapAttribute); }
			set { DOMHTMLElement.SetUseMapAttribute(new nsAString(value)); }
		}

		public string Value {
			get { return nsString.Get(DOMHTMLElement.GetValueAttribute); }
			set { DOMHTMLElement.SetValueAttribute(new nsAString(value)); }
		}

		public int SelectionStart
		{
			get	{ return DOMHTMLElement.GetSelectionStartAttribute(); }
			set	{ DOMHTMLElement.SetSelectionStartAttribute(value);	}
		}

		public int SelectionEnd
		{
			get	{ return DOMHTMLElement.GetSelectionEndAttribute();	}
			set	{ DOMHTMLElement.SetSelectionEndAttribute(value); }
		}	

		public void select()
		{
			DOMHTMLElement.Select();
		}
	}
}

