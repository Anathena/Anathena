using Gecko.DOM;

namespace Gecko
{
	public class DomMouseScrollEventArgs
		: DomMouseEventArgs
	{
		private nsIDOMMouseScrollEvent _mouseScrollEvent;

		private DomMouseScrollEventArgs(nsIDOMMouseScrollEvent ev)
			: base(ev)
		{
			_mouseScrollEvent = ev;
		}

		public static DomMouseScrollEventArgs Create(nsIDOMMouseScrollEvent ev)
		{
			return new DomMouseScrollEventArgs( ev );
		}

		public void InitMouseScrollEvent(string type,bool canBubble,bool cancelable,GeckoWindow view,int detail,
		                                 int screenX, int screenY, int clientX, int clientY,
		                                 bool ctrlKey, bool altKey, bool shiftKey, bool metaKey,
		                                 ushort button, DomEventTarget target,int axis
			)
		{
			using (var typeArg = new nsAString(type))
			{
				_mouseScrollEvent.InitMouseScrollEvent( typeArg, canBubble, cancelable, view.DomWindow, detail, screenX, screenY,
				                                        clientX, clientY, ctrlKey, altKey, shiftKey, metaKey, button,
				                                        target.NativeObject, axis );
			}
		}

		/// <summary>
		/// Indicates which mouse wheel axis changed; this will be either HORIZONTAL_AXIS (1) or VERTICAL_AXIS (2).
		/// </summary>
		public int Axis
		{
			get
			{
				return _mouseScrollEvent.GetAxisAttribute();
			}
		}
	}
}