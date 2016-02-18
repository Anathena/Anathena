//License MPL, the GPL or the LGPL.

using System;
using System.Runtime.InteropServices;

namespace Gecko
{
	/// <summary>
	/// Provides data froma a message event.
	/// doxygen.db48x.net/mozilla.../interfacensIDOMMessageEvent.html
	/// </summary>
	public class DomMessageEventArgs
		: DomEventArgs
	{
		private nsIDOMMessageEvent _messageEvent;

		private DomMessageEventArgs(nsIDOMMessageEvent ev)
			: base(ev)
		{
			_messageEvent = ev;
		}

		public static DomMessageEventArgs Create(nsIDOMMessageEvent ev)
		{
			return new DomMessageEventArgs( ev );
		}
		
		/// <summary>
		/// The Message payload of the event
		/// </summary>
		public string Message
		{
			get
			{
				using (AutoJSContext context = new AutoJSContext(_messageEvent.GetTargetAttribute().GetJSContextForEventHandlers()))
				{
				    
                    var val = _messageEvent.GetDataAttribute(context.ContextPointer);
					//TODO if(!val.IsString)
					//throw new NotImplementedException("GeckoFx currently only supports messages which are strings.");

					try
					{
						return val.ToString();
					}
					catch (AccessViolationException e)
					{
						//NB: errors here can be caused by not getting an actual string back. E.g., is it a number, a bool, etc.? Those aren't supported (but you could easily add them).
						throw e;
					}
				}
			}
		}		
	}
}
