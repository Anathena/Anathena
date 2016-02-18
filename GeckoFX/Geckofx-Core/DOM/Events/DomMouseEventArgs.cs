using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Gecko.DOM;

namespace Gecko
{
	/// <summary>
	/// Provides data about a DOM mouse event.
	/// </summary>
	public class DomMouseEventArgs
		: DomUIEventArgs
	{
		nsIDOMMouseEvent _domMouseEvent;

		protected DomMouseEventArgs(nsIDOMMouseEvent ev)
			: base(ev)
		{
			_domMouseEvent = ev;
		}

		public static DomMouseEventArgs Create(nsIDOMMouseEvent ev)
		{
			if (ev is nsIDOMMouseScrollEvent)
			{
				return DomMouseScrollEventArgs.Create( (nsIDOMMouseScrollEvent)ev );
			}
			if (ev is nsIDOMDragEvent)
			{
				return DomDragEventArgs.Create((nsIDOMDragEvent)ev);
			}
			return new DomMouseEventArgs( ev );
		}

		public void InitMouseEvent(string type, bool canBubble, bool cancelable, GeckoWindow view, int detail,
								   int screenX, int screenY, int clientX, int clientY,
								   bool ctrlKey, bool altKey, bool shiftKey, bool metaKey,
			ushort button, DomEventTarget target)
		{
			using (var typeArg = new nsAString(type))
			{
				_domMouseEvent.InitMouseEvent(typeArg, canBubble, cancelable, view.DomWindow, detail, screenX, screenY,
											   clientX, clientY, ctrlKey, altKey, shiftKey, metaKey, button, target.NativeObject);
			}
		}

		/// <summary>
		/// The X coordinate of the mouse pointer in local (DOM content) coordinates.
		/// </summary>
		public int ClientX
		{
			get { return _domMouseEvent.GetClientXAttribute(); }
		}
		
		/// <summary>
		/// The Y coordinate of the mouse pointer in local (DOM content) coordinates.
		/// </summary>
		public int ClientY
		{
			get { return _domMouseEvent.GetClientYAttribute(); }
		}
		
		/// <summary>
		/// The X coordinate of the mouse pointer in global (screen) coordinates.
		/// </summary>
		public int ScreenX
		{
			get { return _domMouseEvent.GetScreenXAttribute(); }
		}
		
		/// <summary>
		/// The Y coordinate of the mouse pointer in global (screen) coordinates.
		/// </summary>
		public int ScreenY
		{
			get { return _domMouseEvent.GetScreenYAttribute(); }
		}
		
		/// <summary>
		/// The button number that was pressed when the mouse event was fired.
		/// </summary>
		public GeckoMouseButton Button
		{
			get { return (GeckoMouseButton)_domMouseEvent.GetButtonAttribute(); }
		}
		
		/// <summary>
		/// true if the alt key was down when the mouse event was fired.
		/// </summary>
		public bool AltKey
		{
			get { return _domMouseEvent.GetAltKeyAttribute(); }
		}

		
		
		/// <summary>
		/// true if the control key was down when the mouse event was fired.
		/// </summary>
		public bool CtrlKey
		{
			get { return _domMouseEvent.GetCtrlKeyAttribute(); }
		}
		
		/// <summary>
		/// true if the shift key was down when the mouse event was fired.
		/// </summary>
		public bool ShiftKey
		{
			get { return _domMouseEvent.GetShiftKeyAttribute(); }
		}
	}
}