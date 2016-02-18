using System;
using Gecko.Interop;

namespace Gecko
{
	/// <summary>
	/// Provides data about a DOM event.
	/// </summary>
	public class DomEventArgs
		: EventArgs
	{
		private nsIDOMEvent _event;
		bool _Handled;

		protected DomEventArgs(nsIDOMEvent ev)
		{
			_event = ev;
		}

		public static DomEventArgs Create(nsIDOMEvent ev)
		{
			var type = nsString.Get(ev.GetTypeAttribute).ToLowerInvariant();
			// search by name and create child class wrappers
			switch (type)
			{
				case "keydown":
				case "keyup":
				case "keypress":
					return DomKeyEventArgs.Create((nsIDOMKeyEvent)ev);
				case "mousedown":
				case "mouseup":
				case "mousemove":
				case "mouseover":
				case "mouseout":
				case "contextmenu":
					return DomMouseEventArgs.Create((nsIDOMMouseEvent)ev);
				case "dommousescroll":
					return DomMouseScrollEventArgs.Create((nsIDOMMouseScrollEvent)ev);
				case "dragstart":
				case "dragenter":
				case "dragover":
				case "dragleave":
				case "drag":
				case "drop":
				case "dragend":
					return DomDragEventArgs.Create((nsIDOMDragEvent)ev);
				case "hashchange":
					return DomHashChangeEventArgs.Create( ( nsIDOMHashChangeEvent ) ev );
			}
			
			if (ev is nsIDOMUIEvent)
			{
				return DomUIEventArgs.Create((nsIDOMUIEvent)ev);
			}
			if (ev is nsIDOMMessageEvent)
			{
				return DomMessageEventArgs.Create((nsIDOMMessageEvent)ev);
			}
			
			return new DomEventArgs(ev);
		}

		public nsIDOMEvent DomEvent
		{
			get { return _event; }
		}

		public string Type
		{
			get { return nsString.Get( _event.GetTypeAttribute ); }
		}


		public bool Bubbles
		{
			get { return _event.GetBubblesAttribute(); }
		}

		public bool Cancelable
		{
			get { return _event.GetCancelableAttribute(); }
		}

		public bool GetDefaultPrevented
		{
			get { return _event.GetDefaultPreventedAttribute(); }
		}

		public long Timestamp
		{
			get { return _event.GetTimeStampAttribute(); }
		}

		public EventPhase EventPhase
		{
			get { return (EventPhase)_event.GetEventPhaseAttribute(); }
		}

		public DOM.DomEventTarget CurrentTarget
		{
			get { return _event.GetCurrentTargetAttribute().Wrap( DOM.DomEventTarget.Create ); }
		}

		/// <summary>
		/// Gets the final destination of the event.
		/// </summary>
		public DOM.DomEventTarget Target
		{
			get { return _event.GetTargetAttribute().Wrap( DOM.DomEventTarget.Create ); }
		}


		/// <summary>
		/// If an event is cancelable, the preventDefault method is used to
		/// signify that the event is to be canceled, meaning any default action
		/// normally taken by the implementation as a result of the event will
		/// not occur. If, during any stage of event flow, the preventDefault
		/// method is called the event is canceled. Any default action associated
		/// with the event will not occur. Calling this method for a
		/// non-cancelable event has no effect. Once preventDefault has been
		/// called it will remain in effect throughout the remainder of the
		/// event's propagation. This method may be used during any stage of
		/// event flow.
		/// </summary>
		public void PreventDefault()
		{
			_event.PreventDefault();
		}

		/// <summary>
		/// The stopPropagation method is used prevent further propagation of an
		/// event during event flow. If this method is called by any
		/// EventListener the event will cease propagating through the tree. The
		/// event will complete dispatch to all listeners on the current
		/// EventTarget before event flow stops. This method may be used during
		/// any stage of event flow.
		/// </summary>
		public void StopPropagation()
		{
			_event.StopPropagation();
		}	

		public void StopImmediatePropagation()
		{
			_event.StopImmediatePropagation();
		}

		/// <summary>
		/// Gets or sets whether the event was handled.  Setting this property prevents the target DOM object
		/// from receiving the event (if Cancelable is true).
		/// </summary>
		public bool Handled
		{
			get { return _Handled; }
			set { _Handled = value; }
		}
		

	}

	public enum EventPhase
		:ushort
	{
		None=(ushort)nsIDOMEventConsts.NONE,
		CapturingPhase=(ushort)nsIDOMEventConsts.CAPTURING_PHASE,
		AtTarget=(ushort)nsIDOMEventConsts.AT_TARGET,
		BubblingPhase=(ushort)nsIDOMEventConsts.BUBBLING_PHASE
	}
}