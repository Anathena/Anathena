

using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace Gecko.DOM
{	
	public class GeckoBRElement : GeckoHtmlElement
	{
		private nsIDOMHTMLBRElement _domHtmlBrElement;

		internal GeckoBRElement(nsIDOMHTMLBRElement element) : base(element)
		{
			this._domHtmlBrElement = element;
		}

		public string Clear
		{
			get { return nsString.Get( _domHtmlBrElement.GetClearAttribute ); }
			set { nsString.Set( _domHtmlBrElement.SetClearAttribute, value ); }
		}

		public override string OuterHtml
		{
			get { return "<br>"; }
		}

	}
}

