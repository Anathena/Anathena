using System;
using Gecko.Interop;
using System.Runtime.InteropServices;

namespace Gecko
{
	sealed class ChromeContext: IDisposable
	{
		private bool _isInitialized;
		private ComPtr<nsIWebNavigation> webNav;
		private ComPtr<nsIDOMXULElement> command;

		public ChromeContext()
		{			
			using (var appShallSvc = Xpcom.GetService2<nsIAppShellService>(Contracts.AppShellService))
			{
				webNav = appShallSvc.Instance.CreateWindowlessBrowser(true).AsComPtr();
				webNav.Instance.LoadURI("chrome://global/content/alerts/alert.xul", 0, null, null, null);
			}
		}

		#region IDisposable implementation

		#if DEBUG
		/// <summary/>
		~ChromeContext()
		{
			Dispose(false);
		}
		#endif

		/// <summary/>
		public bool IsDisposed { get; private set; }

		/// <summary/>
		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		/// <summary/>
		private void Dispose(bool fDisposing)
		{
			System.Diagnostics.Debug.WriteLineIf(!fDisposing, "****** Missing Dispose() call for " + GetType() + ". *******");
			if (fDisposing && !IsDisposed)
			{
				// dispose managed and unmanaged objects
				if (webNav != null)
					webNav.Dispose();
				if (command != null)
					command.Dispose();
				_isInitialized = false;
			}
			webNav = null;
			command = null;
			IsDisposed = true;
		}

		#endregion

		private void Init()
		{
			if (!_isInitialized)
			{
				_isInitialized = true;
				GeckoDomDocument doc = webNav.Instance.GetDocumentAttribute().Wrap(GeckoDocument.CreateDomDocumentWraper);
				GeckoElement rootElement = doc.DocumentElement;
				while (rootElement.FirstChild != null)
					rootElement.RemoveChild(rootElement.FirstChild);


				// Use of the canvas technique was inspired by: the abduction! firefox plugin by Rowan Lewis
				// https://addons.mozilla.org/en-US/firefox/addon/abduction/

				uint flags = (uint)(nsIDOMCanvasRenderingContext2DConsts.DRAWWINDOW_DO_NOT_FLUSH
							//| nsIDOMCanvasRenderingContext2DConsts.DRAWWINDOW_DRAW_VIEW
							| nsIDOMCanvasRenderingContext2DConsts.DRAWWINDOW_ASYNC_DECODE_IMAGES
							| nsIDOMCanvasRenderingContext2DConsts.DRAWWINDOW_USE_WIDGET_LAYERS);

				string func = @"
function drawWindow(window, x, y, w, h, canvas, ctx)
{
	try {
		canvas = window.document.createElement('canvas');
		canvas.width = w;
		canvas.height = h;
		ctx = canvas.getContext('2d');
		ctx.drawWindow(window, x, y, w, h, 'rgb(255,255,255)', " + flags.ToString() + @");
		return canvas.toDataURL('image/png');
	} catch(e) {
		return e + ''
	}
}
";

				var button = doc.CreateElement("button");
				button.SetAttribute("oncommand", func + @"this.setUserData('drawResult', drawWindow(this.getUserData('window'), this.getUserData('x'), this.getUserData('y'), this.getUserData('w'), this.getUserData('h')), null)");
				rootElement.AppendChild(button);

				command = Xpcom.QueryInterface<nsIDOMXULElement>(button.DOMElement).AsComPtr();
			}
		}

		internal byte[] DrawWindow(nsIDOMWindow window, uint x, uint y, uint w, uint h)
		{
			Xpcom.AssertCorrectThread();
			Init();

			SetValue("x", x);
			SetValue("y", y);
			SetValue("w", w);
			SetValue("h", h);

			using (var data = Xpcom.CreateInstance2<nsIWritableVariant>("@mozilla.org/variant;1"))
			{
				data.Instance.SetAsISupports((nsISupports)window);
				using (var key = new nsAString("window"))
				{
					object comObj = command.Instance.SetUserData(key, data.Instance, null);
					Xpcom.FreeComObject(ref comObj);
					command.Instance.DoCommand();
					comObj = command.Instance.SetUserData(key, null, null);
					Xpcom.FreeComObject(ref comObj);
				}
			}
			string base64Image = null;
			using (var key = new nsAString("drawResult"))
			{
				using (var drawResult = command.Instance.SetUserData(key, null, null).AsComPtr())
				{
					if (drawResult != null)
						base64Image = drawResult.Instance.GetAsWString();
				}
			}

			if (base64Image == null)
				throw new InvalidOperationException();
			if(!base64Image.StartsWith("data:image/png;base64,"))
				throw new InvalidOperationException(base64Image);

			byte[] bytes = Convert.FromBase64String(base64Image.Substring("data:image/png;base64,".Length));
			return bytes;

		}

		private void SetValue(string name, uint value)
		{
			if (name == null)
				throw new ArgumentNullException("name");

			using (var data = Xpcom.CreateInstance2<nsIWritableVariant>("@mozilla.org/variant;1"))
			{
				data.Instance.SetAsUint32(value);
				using (var key = new nsAString(name))
				{
					object comObj = command.Instance.SetUserData(key, data.Instance, null);
					Xpcom.FreeComObject(ref comObj);
				}
			}
		}
		private void SetValue(string name, nsISupports value)
		{
			if (name == null)
				throw new ArgumentNullException("name");

			using (var data = Xpcom.CreateInstance2<nsIWritableVariant>("@mozilla.org/variant;1"))
			{
				data.Instance.SetAsISupports(value);
				using (var key = new nsAString(name))
				{
					object comObj = command.Instance.SetUserData(key, data.Instance, null);
					Xpcom.FreeComObject(ref comObj);
				}
			}
		}
		private void ClearValue(string name)
		{
			if (name == null)
				throw new ArgumentNullException("name");

			using (var key = new nsAString(name))
			{
				object comObj = command.Instance.SetUserData(key, null, null);
				Xpcom.FreeComObject(ref comObj);
			}
		}
	}
}
