using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Gecko.Interop;

namespace Gecko.DOM.Xul
{
	public class XulElement
		:GeckoElement
	{
		protected XulElement(nsIDOMXULElement xulElement)
			:base(xulElement)
		{
			
		}


		public static XulElement CreateXulElementWrapper(nsIDOMXULElement xulElement)
		{
			return xulElement == null ? null : new XulElement( xulElement );
		}

		/// <summary>
		/// Gets the inline style of the XulElement. 
		/// </summary>
		public GeckoStyle Style
		{
			get
			{
				return GeckoStyle.Create(Xpcom.QueryInterface<nsIDOMElementCSSInlineStyle>(DomObject).GetStyleAttribute());
			}
		}

		/// <summary>
		/// Gets style of the XulElement. 
		/// </summary>
		public GeckoStyle ComputedStyle
		{
			get
			{
				nsIDOMCSSStyleDeclaration style;
				using (var element = new ComPtr<nsIDOMElement>(Xpcom.QueryInterface<nsIDOMElement>(this.DomObject)))
				{
					using (var nullString = new nsAString())
					{
						nullString.SetData(null);
						style = this.OwnerDocument.DefaultView.DomWindow.GetComputedStyle(element.Instance, nullString);
					}
				}
				return GeckoStyle.Create(style);
			}
		}
	}
}
