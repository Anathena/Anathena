

using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace Gecko.DOM
{	
	public class GeckoTableCellElement : GeckoHtmlElement
	{
		nsIDOMHTMLTableCellElement DOMHTMLElement;
		internal GeckoTableCellElement(nsIDOMHTMLTableCellElement element) : base(element)
		{
			this.DOMHTMLElement = element;
		}
		public GeckoTableCellElement(object element) : base(element as nsIDOMHTMLElement)
		{
			this.DOMHTMLElement = element as nsIDOMHTMLTableCellElement;
		}
		public int CellIndex {
			get { return DOMHTMLElement.GetCellIndexAttribute(); }
		}

		public string Abbr {
			get { return nsString.Get(DOMHTMLElement.GetAbbrAttribute); }
			set { DOMHTMLElement.SetAbbrAttribute(new nsAString(value)); }
		}

		public string Align {
			get { return nsString.Get(DOMHTMLElement.GetAlignAttribute); }
			set { DOMHTMLElement.SetAlignAttribute(new nsAString(value)); }
		}

		public string Axis {
			get { return nsString.Get(DOMHTMLElement.GetAxisAttribute); }
			set { DOMHTMLElement.SetAxisAttribute(new nsAString(value)); }
		}

		public string BgColor {
			get { return nsString.Get(DOMHTMLElement.GetBgColorAttribute); }
			set { DOMHTMLElement.SetBgColorAttribute(new nsAString(value)); }
		}

		public string Ch {
			get { return nsString.Get(DOMHTMLElement.GetChAttribute); }
			set { DOMHTMLElement.SetChAttribute(new nsAString(value)); }
		}

		public string ChOff {
			get { return nsString.Get(DOMHTMLElement.GetChOffAttribute); }
			set { DOMHTMLElement.SetChOffAttribute(new nsAString(value)); }
		}

		public int ColSpan {
			get { return DOMHTMLElement.GetColSpanAttribute(); }
			set { DOMHTMLElement.SetColSpanAttribute(value); }
		}

		public string Headers {
			get { return nsString.Get(DOMHTMLElement.GetHeadersAttribute); }
			set { DOMHTMLElement.SetHeadersAttribute(new nsAString(value)); }
		}

		public string Height {
			get { return nsString.Get(DOMHTMLElement.GetHeightAttribute); }
			set { DOMHTMLElement.SetHeightAttribute(new nsAString(value)); }
		}

		public bool NoWrap {
			get { return DOMHTMLElement.GetNoWrapAttribute(); }
			set { DOMHTMLElement.SetNoWrapAttribute(value); }
		}

		public int RowSpan {
			get { return DOMHTMLElement.GetRowSpanAttribute(); }
			set { DOMHTMLElement.SetRowSpanAttribute(value); }
		}

		public string Scope {
			get { return nsString.Get(DOMHTMLElement.GetScopeAttribute); }
			set { DOMHTMLElement.SetScopeAttribute(new nsAString(value)); }
		}

		public string VAlign {
			get { return nsString.Get(DOMHTMLElement.GetVAlignAttribute); }
			set { DOMHTMLElement.SetVAlignAttribute(new nsAString(value)); }
		}

		public string Width {
			get { return nsString.Get(DOMHTMLElement.GetWidthAttribute); }
			set { DOMHTMLElement.SetWidthAttribute(new nsAString(value)); }
		}

	}
}
