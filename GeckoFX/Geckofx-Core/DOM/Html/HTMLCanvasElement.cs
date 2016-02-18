

using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace Gecko.DOM
{	
	public class GeckoCanvasElement : GeckoHtmlElement
	{
		nsIDOMHTMLCanvasElement DOMHTMLElement;
		internal GeckoCanvasElement(nsIDOMHTMLCanvasElement element) : base(element)
		{
			this.DOMHTMLElement = element;
		}
		public GeckoCanvasElement(object element) : base(element as nsIDOMHTMLElement)
		{
			this.DOMHTMLElement = element as nsIDOMHTMLCanvasElement;
		}
		public uint Width {
			get { return DOMHTMLElement.GetWidthAttribute(); }
			set { DOMHTMLElement.SetWidthAttribute(value); }
		}

		public uint Height {
			get { return DOMHTMLElement.GetHeightAttribute(); }
			set { DOMHTMLElement.SetHeightAttribute(value); }
		}

		public string ToDataURL(string type)
		{
			using (var context = new AutoJSContext(GlobalJSContextHolder.BackstageJSContext))
			using (nsAString retval = new nsAString(), param = new nsAString(type))
			{
				JsVal js = default(JsVal);
				DOMHTMLElement.ToDataURL(param, ref js, context.ContextPointer, retval);
				return retval.ToString();
			}
		}
	}
}

