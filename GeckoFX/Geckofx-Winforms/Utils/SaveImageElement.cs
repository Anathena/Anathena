using System;
using System.Windows.Forms;
using Gecko.DOM;

namespace Gecko.Utils
{
    /// <summary>
    ///  Utility to save ImageElement To Png and To Clipboard
    /// </summary> 
    public class SaveImageElement
    {
		private static string CopyImageElementToDataImageString(GeckoHtmlElement element,
			string imageFormat, float xOffset, float yOffset, float width, float height)
        {
            if (width == 0)
                throw new ArgumentException("width");

            if (height == 0)
                throw new ArgumentException("height");

			string data;
			using (var context = new AutoJSContext())
			{
				context.EvaluateScript(string.Format(@"(function(element, canvas, ctx)
						{{
							canvas = element.ownerDocument.createElement('canvas');
							canvas.width = {2};
							canvas.height = {3};
							ctx = canvas.getContext('2d');
							ctx.drawImage(element, -{0}, -{1});
							return canvas.toDataURL('{4}');
						}}
						)(this)", xOffset, yOffset, width, height, imageFormat), (nsISupports)(nsIDOMElement)element.DomObject, out data);
			}
			return data;
        }

        /// <summary>
        /// Need the GeckoWebBrowser , ImageElement , X-OffSet , Y-Offset (put 0.0F if you want offset), Width , height) 
        /// Return the png image in data bytes[] 
        /// </summary>
		public static byte[] ConvertGeckoImageElementToPng(IGeckoWebBrowser browser, GeckoHtmlElement element, float xOffset,
                                                           float yOffset, float width, float height)
        {

			string data = CopyImageElementToDataImageString(element, "image/png", xOffset, yOffset, width, height);
			if (data == null || !data.StartsWith("data:image/png;base64,"))
				throw new InvalidOperationException();

            byte[] bytes = Convert.FromBase64String(data.Substring("data:image/png;base64,".Length));
            return bytes;
        }

        /// <summary>
        /// Need the GeckoWebBrowser , ImageElement , X-OffSet , Y-Offset (put 0.0F if you want offset), Width , height)
        /// Return true if the image was copied in the ClipBoard
        /// </summary>
		public bool CopyGeckoImageElementToPng(IGeckoWebBrowser browser, GeckoHtmlElement element, float xOffset,
                                                      float yOffset, float width, float height)
        {
			string data = CopyImageElementToDataImageString(element, "image/png", xOffset, yOffset, width, height);
			if (data == null || !data.StartsWith("data:image/png;base64,"))	return false;

            byte[] bytes = Convert.FromBase64String(data.Substring("data:image/png;base64,".Length));
            Clipboard.SetImage(System.Drawing.Image.FromStream(new System.IO.MemoryStream(bytes)));
            return Clipboard.ContainsImage();
        }
    }
}
