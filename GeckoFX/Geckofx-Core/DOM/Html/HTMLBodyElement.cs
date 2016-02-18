

using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace Gecko.DOM
{
	public class GeckoBodyElement
		: GeckoHtmlElement
	{
		private nsIDOMHTMLBodyElement _domHtmlBodyElement;

		internal GeckoBodyElement( nsIDOMHTMLBodyElement element )
			: base( element )
		{
			_domHtmlBodyElement = element;
		}

		public string ALink
		{
			get { return nsString.Get( _domHtmlBodyElement.GetALinkAttribute ); }
			set { nsString.Set( _domHtmlBodyElement.SetALinkAttribute, value ); }
		}

		public string Background
		{
			get { return nsString.Get( _domHtmlBodyElement.GetBackgroundAttribute ); }
			set { nsString.Set( _domHtmlBodyElement.SetBackgroundAttribute, value ); }
		}

		public string BgColor
		{
			get { return nsString.Get( _domHtmlBodyElement.GetBgColorAttribute ); }
			set { nsString.Set( _domHtmlBodyElement.SetBgColorAttribute, value ); }
		}

		public string Link
		{
			get { return nsString.Get( _domHtmlBodyElement.GetLinkAttribute ); }
			set { nsString.Set( _domHtmlBodyElement.SetLinkAttribute, value ); }
		}

		public string Text
		{
			get { return nsString.Get( _domHtmlBodyElement.GetTextAttribute ); }
			set { nsString.Set( _domHtmlBodyElement.SetTextAttribute, value ); }
		}

		public string VLink
		{
			get { return nsString.Get( _domHtmlBodyElement.GetVLinkAttribute ); }
			set { nsString.Set( _domHtmlBodyElement.SetVLinkAttribute, value ); }
		}

	}
}

