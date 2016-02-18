using System;
using Gecko.Interop;

namespace Gecko.DOM
{
	public class WindowUtils
	{
		private ComPtr<nsIDOMWindowUtils> _windowUtils;

		internal WindowUtils(nsIDOMWindowUtils windowUtils)
		{
			_windowUtils = new ComPtr<nsIDOMWindowUtils>( windowUtils );
		}

		public static WindowUtils Create( nsIDOMWindowUtils windowUtils )
		{
			return new WindowUtils( windowUtils );
		}

		/// <summary>
		/// Image animation mode of the window. When this attribute's value
		/// is changed, the implementation should set all images in the window
		/// to the given value. That is, when set to kDontAnimMode, all images
		/// will stop animating. The attribute's value must be one of the
		/// animationMode values from imgIContainer.
		/// @note Images may individually override the window's setting after
		/// the window's mode is set. Therefore images given different modes
		/// since the last setting of the window's mode may behave
		/// out of line with the window's overall mode.
		/// @note The attribute's value is the window's overall mode. It may
		/// for example continue to report kDontAnimMode after all images
		/// have subsequently been individually animated.
		/// @note Only images immediately in this window are affected;
		/// this is not recursive to subwindows.
		/// @see imgIContainer
		/// </summary>		
		public ushort ImageAnimationMode
		{
			get { return _windowUtils.Instance.GetImageAnimationModeAttribute(); }
			set { _windowUtils.Instance.SetImageAnimationModeAttribute( value ); }
		}


		/// <summary>
        /// Whether the charset of the window's current document has been forced by
        /// the user.
        /// Cannot be accessed from unprivileged context (not content-accessible)
        /// </summary>		
		public bool DocCharsetIsForced
		{
			get { return _windowUtils.Instance.GetDocCharsetIsForcedAttribute(); }
		}

		/// <summary>
        /// Get current cursor type from this window
        /// @return the current value of nsCursor
        /// </summary>

		public short GetCursorType()
		{
			return _windowUtils.Instance.GetCursorType();
		}

	
		/// <summary>
        /// Function to get metadata associated with the window's current document
        /// @param aName the name of the metadata.  This should be all lowercase.
        /// @return the value of the metadata, or the empty string if it's not set
        ///
        /// Will throw a DOM security error if called without UniversalXPConnect
        /// privileges.
        /// </summary>		
		public string GetDocumentMetadata(string name)
		{
			return nsString.Get( _windowUtils.Instance.GetDocumentMetadata, name );
			//using (nsAString tempName = new nsAString(name), ret = new nsAString())
			//{
			//	_windowUtils.Instance.GetDocumentMetadata( tempName, ret );
			//	return ret.ToString();
			//}
		}

		/// <summary>
        /// Force an immediate redraw of this window.  The parameter specifies
        /// the number of times to redraw, and the return value is the length,
        /// in milliseconds, that the redraws took.  If aCount is not specified
        /// or is 0, it is taken to be 1.
        /// </summary>		
		public uint Redraw(uint aCount)
		{
			return _windowUtils.Instance.Redraw(aCount);
		}

		/// <summary>
        /// Set the CSS viewport to be |widthPx| x |heightPx| in units of CSS
        /// pixels, regardless of the size of the enclosing widget/view.
        /// This will trigger reflow.
        ///
        /// The caller of this method must have UniversalXPConnect
        /// privileges.
        /// </summary>		
		public void SetCSSViewport(float aWidthPx, float aHeightPx)
		{
			_windowUtils.Instance.SetCSSViewport( aWidthPx, aHeightPx );
		}

		/// <summary>
        /// For any scrollable element, this allows you to override the
        /// visible region and draw more than what is visible, which is
        /// useful for asynchronous drawing. The "displayport" will be
        /// <xPx, yPx, widthPx, heightPx> in units of CSS pixels,
        /// regardless of the size of the enclosing container.  This
        /// will *not* trigger reflow.
        ///
        /// For the root scroll area, pass in the root document element.
        /// For scrollable elements, pass in the container element (for
        /// instance, the element with overflow: scroll).
        ///
        /// <x, y> is relative to the top-left of what would normally be
        /// the visible area of the element. This means that the pixels
        /// rendered to the displayport take scrolling into account,
        /// for example.
        ///
        /// It's legal to set a displayport that extends beyond the overflow
        /// area in any direction (left/right/top/bottom).
        ///
        /// It's also legal to set a displayport that extends beyond the
        /// area's bounds.  No pixels are rendered outside the area bounds.
        ///
        /// The caller of this method must have UniversalXPConnect
        /// privileges.
        /// </summary>		
		public void SetDisplayPortForElement(float aXPx, float aYPx, float aWidthPx, float aHeightPx, GeckoElement aElement, uint aPriority)
		{
			_windowUtils.Instance.SetDisplayPortForElement( aXPx, aYPx, aWidthPx, aHeightPx, ( nsIDOMElement ) aElement.DomObject, aPriority);
		}


		/// <summary>
        /// Get/set the resolution at which rescalable web content is drawn.
        /// Currently this is only (some) thebes content.
        ///
        /// Setting a new resolution does *not* trigger reflow.  This API is
        /// entirely separate from textZoom and fullZoom; a resolution scale
        /// can be applied together with both textZoom and fullZoom.
        ///
        /// The effect of is API for gfx code to allocate more or fewer
        /// pixels for rescalable content by a factor of |resolution| in
        /// either or both dimensions.  setResolution() together with
        /// setDisplayport() can be used to implement a non-reflowing
        /// scale-zoom in concert with another entity that can draw with a
        /// scale.  For example, to scale a content |window| inside a
        /// <browser> by a factor of 2.0
        ///
        /// window.setDisplayport(x, y, oldW / 2.0, oldH / 2.0);
        /// window.setResolution(2.0, 2.0);
        /// // elsewhere
        /// browser.setViewportScale(2.0, 2.0);
        ///
        /// The caller of this method must have UniversalXPConnect
        /// privileges.
        /// </summary>		
		public void SetResolution(float aXResolution, float aYResolution)
		{
			_windowUtils.Instance.SetResolution( aXResolution, aYResolution );
		}
		
		/// <summary>
        ///Synthesize a mouse event. The event types supported are:
        /// mousedown, mouseup, mousemove, mouseover, mouseout, contextmenu
        ///
        /// Events are sent in coordinates offset by aX and aY from the window.
        ///
        /// Note that additional events may be fired as a result of this call. For
        /// instance, typically a click event will be fired as a result of a
        /// mousedown and mouseup in sequence.
        ///
        /// Normally at this level of events, the mouseover and mouseout events are
        /// only fired when the window is entered or exited. For inter-element
        /// mouseover and mouseout events, a movemove event fired on the new element
        /// should be sufficient to generate the correct over and out events as well.
        ///
        /// Cannot be accessed from unprivileged context (not content-accessible)
        /// Will throw a DOM security error if called without UniversalXPConnect
        /// privileges.
        ///
        /// The event is dispatched via the toplevel window, so it could go to any
        /// window under the toplevel window, in some cases it could never reach this
        /// window at all.
        ///
        /// @param aType event type
        /// @param aX x offset in CSS pixels
        /// @param aY y offset in CSS pixels
        /// @param aButton button to synthesize
        /// @param aClickCount number of clicks that have been performed
        /// @param aModifiers modifiers pressed, using constants defined in nsIDOMNSEvent
        /// @param aIgnoreRootScrollFrame whether the event should ignore viewport bounds
        /// during dispatch
        /// </summary>		
		public void SendMouseEvent(string aType, float aX, float aY, GeckoMouseButton aButton, int aClickCount, int aModifiers, bool aIgnoreRootScrollFrame, float aPressure, ushort aInputSourceArg)
		{
			using (nsAString type = new nsAString(aType))
			{
				_windowUtils.Instance.SendMouseEvent( type, aX, aY, (int)aButton, aClickCount, aModifiers, aIgnoreRootScrollFrame, aPressure, aInputSourceArg, false, 10);
			}
		}
		
		
		/// <summary>
        ///The same as sendMouseEvent but ensures that the event is dispatched to
        /// this DOM window or one of its children.
        /// </summary>		
		public void SendMouseEventToWindow(string aType, float aX, float aY, GeckoMouseButton aButton, int aClickCount, int aModifiers, bool aIgnoreRootScrollFrame, float aPressure, ushort aInputSourceArg)
		{
			using (nsAString type = new nsAString(aType))
			{
				_windowUtils.Instance.SendMouseEventToWindow( type, aX, aY, (int)aButton, aClickCount, aModifiers, aIgnoreRootScrollFrame, aPressure, aInputSourceArg, false, 10 );
			}
		}		

		/// <summary>
        /// Synthesize a key event to the window. The event types supported are:
        /// keydown, keyup, keypress
        ///
        /// Key events generally end up being sent to the focused node.
        ///
        /// Cannot be accessed from unprivileged context (not content-accessible)
        /// Will throw a DOM security error if called without UniversalXPConnect
        /// privileges.
        ///
        /// @param aType event type
        /// @param aKeyCode key code
        /// @param aCharCode character code
        /// @param aModifiers modifiers pressed, using constants defined in nsIDOMNSEvent
        /// @param aPreventDefault if true, preventDefault() the event before dispatch
        ///
        /// @return false if the event had preventDefault() called on it,
        /// true otherwise.  In other words, true if and only if the
        /// default action was taken.
        /// </summary>
		/// 	
		public bool SendKeyEvent(string aType, int aKeyCode, int aCharCode, int aModifiers, bool aPreventDefault)
		{
			using (nsAString type = new nsAString(aType))
			{
				return _windowUtils.Instance.SendKeyEvent( type, aKeyCode, aCharCode, aModifiers, aPreventDefault ? 1U : 0U );
			}
		}	
		
		/// <summary>
        /// See nsIWidget::SynthesizeNativeKeyEvent
        ///
        /// Cannot be accessed from unprivileged context (not content-accessible)
        /// Will throw a DOM security error if called without UniversalXPConnect
        /// privileges.
        /// </summary>		
		public void SendNativeKeyEvent(int aNativeKeyboardLayout, int aNativeKeyCode, int aModifierFlags, string aCharacters, string aUnmodifiedCharacters)
		{
			using (nsAString characters = new nsAString(aCharacters), unmodifiedCharaters = new nsAString(aUnmodifiedCharacters))
			{
				_windowUtils.Instance.SendNativeKeyEvent( aNativeKeyboardLayout, aNativeKeyCode, aModifierFlags, characters, unmodifiedCharaters );
			}
		}

		/// <summary>
        /// See nsIWidget::SynthesizeNativeMouseEvent
        ///
        /// Will be called on the widget that contains aElement.
        /// Cannot be accessed from unprivileged context (not content-accessible)
        /// Will throw a DOM security error if called without UniversalXPConnect
        /// privileges.
        /// </summary>
		public void SendNativeMouseEvent(int aScreenX, int aScreenY, int aNativeMessage, int aModifierFlags, GeckoElement aElement)
		{
			_windowUtils.Instance.SendNativeMouseEvent( aScreenX, aScreenY, aNativeMessage, aModifierFlags, ( nsIDOMElement ) aElement.DomObject );
		}

        /// <summary>
        /// Synthesize a wheel event for a window. The event type is always "wheel".
        /// 
        /// Events are sent in coordinates offset by aX and aY from the window.
        /// 
        /// Cannot be accessed from unprivileged context (not content-accessible)
        /// Will throw a DOM security error if called without UniversalXPConnect
        /// privileges.
        /// </summary>
        public void SendWheelEvent(float aX, float aY, double aDeltaX, double aDeltaY, double aDeltaZ, uint aDeltaMode, int aModifiers, int aLineOrPageDeltaX, int aLineOrPageDeltaY, uint aOptions)
        {
            _windowUtils.Instance.SendWheelEvent(aX, aY, aDeltaX, aDeltaY, aDeltaZ, aDeltaMode, aModifiers, aLineOrPageDeltaX, aLineOrPageDeltaY, aOptions);
        }

		/// <summary>
        /// See nsIWidget::ActivateNativeMenuItemAt
        ///
        /// Cannot be accessed from unprivileged context (not content-accessible)
        /// Will throw a DOM security error if called without UniversalXPConnect
        /// privileges.
        /// </summary>		
		public void ActivateNativeMenuItemAt(string indexString)
		{
			nsString.Set( _windowUtils.Instance.ActivateNativeMenuItemAt, indexString );
		}
		
		/// <summary>
        /// See nsIWidget::ForceUpdateNativeMenuAt
        ///
        /// Cannot be accessed from unprivileged context (not content-accessible)
        /// Will throw a DOM security error if called without UniversalXPConnect
        /// privileges.
        /// </summary>
		public void ForceUpdateNativeMenuAt(string indexString)
		{
			nsString.Set( _windowUtils.Instance.ForceUpdateNativeMenuAt, indexString );
		}


		/// <summary>
        /// Focus the element aElement. The element should be in the same document
        /// that the window is displaying. Pass null to blur the element, if any,
        /// that currently has focus, and focus the document.
        ///
        /// Cannot be accessed from unprivileged context (not content-accessible)
        /// Will throw a DOM security error if called without UniversalXPConnect
        /// privileges.
        ///
        /// @param aElement the element to focus
        ///
        /// Do not use this method. Just use element.focus if available or
        /// nsIFocusManager::SetFocus instead.
        ///
        /// </summary>		
		public void Focus(GeckoElement aElement)
		{
			_windowUtils.Instance.Focus( aElement.DOMElement );
		}
		
		/// <summary>
        /// Force a garbage collection followed by a cycle collection.
        ///
        /// Will throw a DOM security error if called without UniversalXPConnect
        /// privileges in non-debug builds. Available to all callers in debug builds.
        ///
        /// @param aListener listener that receives information about the CC graph
        /// (see @mozilla.org/cycle-collector-logger;1 for a logger
        /// component)
        /// @param aExtraForgetSkippableCalls indicates how many times
        /// nsCycleCollector_forgetSkippable will
        /// be called before running cycle collection.
        /// -1 prevents the default
        /// nsCycleCollector_forgetSkippable call
        /// which happens after garbage collection.
        /// </summary>		
		public void GarbageCollect(nsICycleCollectorListener aListener, int aExtraForgetSkippableCalls)
		{
			_windowUtils.Instance.GarbageCollect(aListener, aExtraForgetSkippableCalls);
		}
		
		/// <summary>
        /// Force a cycle collection without garbage collection.
        ///
        /// Will throw a DOM security error if called without UniversalXPConnect
        /// privileges in non-debug builds. Available to all callers in debug builds.
        ///
        /// @param aListener listener that receives information about the CC graph
        /// (see @mozilla.org/cycle-collector-logger;1 for a logger
        /// component)
        /// @param aExtraForgetSkippableCalls indicates how many times
        /// nsCycleCollector_forgetSkippable will
        /// be called before running cycle collection.
        /// -1 prevents the default
        /// nsCycleCollector_forgetSkippable call
        /// which happens after garbage collection.
        /// </summary>		
		public void CycleCollect(nsICycleCollectorListener aListener, int aExtraForgetSkippableCalls)
		{
			_windowUtils.Instance.GarbageCollect( aListener, aExtraForgetSkippableCalls );
		}
		
		/// <summary>
        ///Synthesize a simple gesture event for a window. The event types
        /// supported are: MozSwipeGesture, MozMagnifyGestureStart,
        /// MozMagnifyGestureUpdate, MozMagnifyGesture, MozRotateGestureStart,
        /// MozRotateGestureUpdate, MozRotateGesture, MozPressTapGesture, and
        /// MozTapGesture.
        ///
        /// Cannot be accessed from unprivileged context (not
        /// content-accessible) Will throw a DOM security error if called
        /// without UniversalXPConnect privileges.
        ///
        /// @param aType event type
        /// @param aX x offset in CSS pixels
        /// @param aY y offset in CSS pixels
        /// @param aDirection direction, using constants defined in nsIDOMSimpleGestureEvent
        /// @param aDelta  amount of magnification or rotation for magnify and rotation events
        /// @param aModifiers modifiers pressed, using constants defined in nsIDOMNSEvent
        /// </summary>		
		public void SendSimpleGestureEvent(string aType, float aX, float aY, uint aDirection, double aDelta, int aModifiers)
		{
			throw new NotImplementedException();
		}
		
		/// <summary>
        /// Retrieve the element at point aX, aY in the window's document.
        ///
        /// @param aIgnoreRootScrollFrame whether or not to ignore the root scroll
        /// frame when retrieving the element. If false, this method returns
        /// null for coordinates outside of the viewport.
        /// @param aFlushLayout flushes layout if true. Otherwise, no flush occurs.
        /// </summary>		
		public GeckoElement ElementFromPoint(float aX, float aY, bool aIgnoreRootScrollFrame, bool aFlushLayout)
		{
			nsIDOMElement element = _windowUtils.Instance.ElementFromPoint( aX, aY, aIgnoreRootScrollFrame, aFlushLayout );
			if (element == null)
				return null;

			return GeckoElement.CreateDomElementWrapper(element);
		}
		
		/// <summary>
        /// Retrieve all nodes that intersect a rect in the window's document.
        ///
        /// @param aX x reference for the rectangle in CSS pixels
        /// @param aY y reference for the rectangle in CSS pixels
        /// @param aTopSize How much to expand up the rectangle
        /// @param aRightSize How much to expand right the rectangle
        /// @param aBottomSize How much to expand down the rectangle
        /// @param aLeftSize How much to expand left the rectangle
        /// @param aIgnoreRootScrollFrame whether or not to ignore the root scroll
        /// frame when retrieving the element. If false, this method returns
        /// null for coordinates outside of the viewport.
        /// @param aFlushLayout flushes layout if true. Otherwise, no flush occurs.
        /// </summary>		
		public GeckoNodeCollection NodesFromRect(float aX, float aY, float aTopSize, float aRightSize, float aBottomSize, float aLeftSize, bool aIgnoreRootScrollFrame, bool aFlushLayout)
		{
			throw new NotImplementedException();
		}
		
		/// <summary>
        /// Compare the two canvases, returning the number of differing pixels and
        /// the maximum difference in a channel.  This will throw an error if
        /// the dimensions of the two canvases are different.
        ///
        /// This method requires UniversalXPConnect privileges.
        /// </summary>		
		public uint CompareCanvases(GeckoCanvasElement aCanvas1, GeckoCanvasElement aCanvas2, ref uint aMaxDifference)
		{
			throw new NotImplementedException();
		}
		
		/// <summary>
        /// Returns true if a MozAfterPaint event has been queued but not yet
        /// fired.
        /// </summary>				
		public bool GetIsMozAfterPaintPendingAttribute()
		{
			throw new NotImplementedException();
		}
		
		/// <summary>
        /// Suppresses/unsuppresses user initiated event handling in window's document
        /// and subdocuments.
        ///
        /// @throw NS_ERROR_DOM_SECURITY_ERR if called without UniversalXPConnect
        /// privileges and NS_ERROR_FAILURE if window doesn't have a document.
        /// </summary>		
		public void SuppressEventHandling(bool aSuppress)
		{
			throw new NotImplementedException();
		}
				
		public void ClearMozAfterPaintEvents()
		{
			throw new NotImplementedException();
		}
		
		/// <summary>
        /// Disable or enable non synthetic test mouse events on *all* windows.
        ///
        /// Cannot be accessed from unprivileged context (not content-accessible).
        /// Will throw a DOM security error if called without UniversalXPConnect
        /// privileges.
        ///
        /// @param aDisable  If true, disable all non synthetic test mouse events
        /// on all windows.  Otherwise, enable them.
        /// </summary>
		public void DisableNonTestMouseEvents(bool aDisable)
		{
			throw new NotImplementedException();
		}
		
		/// <summary>
        /// Returns the scroll position of the window's currently loaded document.
        ///
        /// @param aFlushLayout flushes layout if true. Otherwise, no flush occurs.
        /// @see nsIDOMWindow::scrollX/Y
        /// </summary>		
		public void GetScrollXY(bool aFlushLayout, ref int aScrollX, ref int aScrollY)
		{
			throw new NotImplementedException();
		}
		
		/// <summary>
        /// Get IME open state. TRUE means 'Open', otherwise, 'Close'.
        /// This property works only when IMEEnabled is IME_STATUS_ENABLED.
        /// </summary>		
		public bool IMEIsOpen
		{
			get { return _windowUtils.Instance.GetIMEIsOpenAttribute(); }
		}
		
		/// <summary>
        /// Get IME status, see above IME_STATUS_* definitions.
        /// </summary>		
		public uint IMEStatus
		{
			get { return _windowUtils.Instance.GetIMEStatusAttribute(); }
		}
		
		/// <summary>
        /// Get the number of screen pixels per CSS pixel.
        /// </summary>		
		public float ScreenPixelsPerCSSPixel
		{
			get { return _windowUtils.Instance.GetScreenPixelsPerCSSPixelAttribute(); }
		}
		
		/// <summary>
        /// Dispatches aEvent via the nsIPresShell object of the window's document.
        /// The event is dispatched to aTarget, which should be an object
        /// which implements nsIContent interface (#element, #text, etc).
        ///
        /// Cannot be accessed from unprivileged context (not
        /// content-accessible) Will throw a DOM security error if called
        /// without UniversalXPConnect privileges.
        ///
        /// @note Event handlers won't get aEvent as parameter, but a similar event.
        /// Also, aEvent should not be reused.
        /// </summary>		
		public bool DispatchDOMEventViaPresShell(GeckoNode aTarget, GeckoNode aEvent, bool aTrusted)
		{
			throw new NotImplementedException();
		}
		
		/// <summary>
        /// Returns the real classname (possibly of the mostly-transparent security
        /// wrapper) of aObj.
        /// </summary>		
		public string GetClassName(Gecko.JsVal aObject, System.IntPtr jsContext)
		{
			throw new NotImplementedException();
		}
		
		/// <summary>
        /// Generate a content command event.
        ///
        /// Cannot be accessed from unprivileged context (not content-accessible)
        /// Will throw a DOM security error if called without UniversalXPConnect
        /// privileges.
        ///
        /// @param aType Type of command content event to send.  Can be one of "cut",
        /// "copy", "paste", "delete", "undo", "redo", or "pasteTransferable".
        /// @param aTransferable an instance of nsITransferable when aType is
        /// "pasteTransferable"
        /// </summary>		
		public void SendContentCommandEvent(string aType, nsITransferable aTransferable)
		{
			throw new NotImplementedException();
		}
		
		/// <summary>
        /// Synthesize a composition event to the window.
        ///
        /// Cannot be accessed from unprivileged context (not content-accessible)
        /// Will throw a DOM security error if called without UniversalXPConnect
        /// privileges.
        ///
        /// @param aType     The event type: "compositionstart", "compositionend" or
        /// "compositionupdate".
        /// @param aData     The data property value.  Note that this isn't applied
        /// for compositionstart event because its value is the
        /// selected text which is automatically computed.
        /// @param aLocale   The locale property value.
        /// </summary>		
		public void SendCompositionEvent(string aType, string aData, string aLocale)
		{
			throw new NotImplementedException();
		}
		
		/// <summary>
        /// NOTE: These values must be same to NS_TEXTRANGE_* in nsGUIEvent.h
        /// </summary>		
		public void SendTextEvent(string aCompositionString, int aFirstClauseLength, uint aFirstClauseAttr, int aSecondClauseLength, uint aSecondClauseAttr, int aThirdClauseLength, uint aThirdClauseAttr, int aCaretStart, int aCaretLength)
		{
			throw new NotImplementedException();
		}
		
		/// <summary>
        /// Synthesize a query content event.
        ///
        /// @param aType  On of the following const values.  And see also each comment
        /// for the other parameters and the result.
        /// </summary>				
		public nsIQueryContentEventResult SendQueryContentEvent(uint aType, uint aOffset, uint aLength, int aX, int aY)
		{
			throw new NotImplementedException();
		}
		
		/// <summary>
        /// Synthesize a selection set event to the window.
        ///
        /// This sets the selection as the specified information.
        ///
        /// @param aOffset  The caret offset of the selection start.
        /// @param aLength  The length of the selection.  If this is too long, the
        /// extra length is ignored.
        /// @param aReverse If true, the selection set from |aOffset + aLength| to
        /// |aOffset|.  Otherwise, set from |aOffset| to
        /// |aOffset + aLength|.
        /// @return True, if succeeded.  Otherwise, false.
        /// </summary>		
		public bool SendSelectionSetEvent(uint aOffset, uint aLength, bool aReverse)
		{
			throw new NotImplementedException();
		}
		
		/// <summary>
        /// Perform the equivalent of:
        /// window.getComputedStyle(aElement, aPseudoElement).
        /// getPropertyValue(aPropertyName)
        /// except that, when the link whose presence in history is allowed to
        /// influence aElement's style is visited, get the value the property
        /// would have if allowed all properties to change as a result of
        /// :visited selectors (except for cases where getComputedStyle uses
        /// data from the frame).
        ///
        /// This is easier to implement than adding our property restrictions
        /// to this API, and is sufficient for the present testing
        /// requirements (which are essentially testing 'color').
        /// </summary>		
		public string GetVisitedDependentComputedStyle(GeckoHtmlElement aElement, string aPseudoElement, string aPropertyName)
		{
			throw new NotImplementedException();
		}
		
		/// <summary>
        /// Returns the parent of obj.
        ///
        /// @param obj The JavaScript object whose parent is to be gotten.
        /// @return the parent.
        /// </summary>		
		public Gecko.JsVal GetParent(Gecko.JsVal obj, System.IntPtr jsContext)
		{
			throw new NotImplementedException();
		}
		
		/// <summary>
        /// Get the id of the outer window of this window.  This will never throw.
        /// </summary>		
		public ulong GetOuterWindowIDAttribute()
		{
			return _windowUtils.Instance.GetOuterWindowIDAttribute();
		}
		
		/// <summary>
        /// Get the id of the current inner window of this window.  If there
        /// is no current inner window, throws NS_ERROR_NOT_AVAILABLE.
        /// </summary>		
		public ulong GetCurrentInnerWindowIDAttribute()
		{
			return _windowUtils.Instance.GetCurrentInnerWindowIDAttribute();
		}
		
		/// <summary>
        /// Put the window into a state where scripts are frozen and events
        /// suppressed, for use when the window has launched a modal prompt.
        /// </summary>		
		public void EnterModalState()
		{
			_windowUtils.Instance.EnterModalState();
		}
		
		/// <summary>
        /// Resume normal window state, where scripts can run and events are
        /// delivered.
        /// </summary>		
		public void LeaveModalState()
		{
			_windowUtils.Instance.LeaveModalState();
		}
				
		/// <summary>
        /// Is the window is in a modal state? [See enterModalState()]
        /// </summary>		
		public bool IsInModalState()
		{
			return _windowUtils.Instance.IsInModalState();
		}
		
		/// <summary>
        /// Suspend/resume timeouts on this window and its descendant windows.
        /// </summary>		
		public void SuspendTimeouts()
		{
			_windowUtils.Instance.SuspendTimeouts();
		}
				
		public void ResumeTimeouts()
		{
			_windowUtils.Instance.ResumeTimeouts();
		}				
		
		/// <summary>
        /// What type of layer manager the widget associated with this window is
        /// using. "Basic" is unaccelerated; other types are accelerated. Throws an
        /// error if there is no widget associated with this window.
        /// </summary>		
		public void GetLayerManagerTypeAttribute(string aLayerManagerType)
		{
			throw new NotImplementedException();
		}
				
		public void StartFrameTimeRecording()
		{
			throw new NotImplementedException();
		}
				
		public void StopFrameTimeRecording(ref uint frameCount,  float[] frameTime)
		{
			throw new NotImplementedException();
		}
		
		/// <summary>
        /// The DPI of the display
        /// </summary>		
		public float DisplayDPI
		{
			get { return _windowUtils.Instance.GetDisplayDPIAttribute(); }
		}
		
		/// <summary>
        /// Return the outer window with the given ID, if any.  Can return null.
        /// </summary>		
		public GeckoWindow GetOuterWindowWithId(ulong aOuterWindowID)
		{
			return _windowUtils.Instance.GetOuterWindowWithId( aOuterWindowID ).Wrap( x => new GeckoWindow( x ) );
		}
				
		public void RenderDocument(nsConstRect aRect, uint aFlags, nscolor aBackgroundColor, gfxContext aThebesContext)
		{
			throw new NotImplementedException();
		}
		
		/// <summary>
        /// advanceTimeAndRefresh allows the caller to take over the refresh
        /// driver timing for a window.  A call to advanceTimeAndRefresh does
        /// three things:
        /// (1) It marks the refresh driver for this presentation so that it
        /// no longer refreshes on its own, but is instead driven entirely
        /// by the caller (except for the refresh that happens when a
        /// document comes out of the bfcache).
        /// (2) It advances the refresh driver's current refresh time by the
        /// argument given.  Negative advances are permitted.
        /// (3) It does a refresh (i.e., notifies refresh observers) at that
        /// new time.
        ///
        /// Note that this affects other connected docshells of the same type
        /// in the same docshell tree, such as parent frames.
        ///
        /// When callers have completed their use of advanceTimeAndRefresh,
        /// they must call restoreNormalRefresh.
        /// </summary>		
		public void AdvanceTimeAndRefresh(long aMilliseconds)
		{
			throw new NotImplementedException();
		}
		
		/// <summary>
        /// Undoes the effects of advanceTimeAndRefresh.
        /// </summary>		
		public void RestoreNormalRefresh()
		{
			throw new NotImplementedException();
		}
		
		/// <summary>
        /// Method for testing nsStyleAnimation::ComputeDistance.
        ///
        /// Returns the distance between the two values as reported by
        /// nsStyleAnimation::ComputeDistance for the given element and
        /// property.
        /// </summary>		
		public double ComputeAnimationDistance(GeckoHtmlElement element, string property, string value1, string value2)
		{
			throw new NotImplementedException();
		}
		
		/// <summary>
        /// Wrap an nsIFile in an nsIDOMFile
        /// </summary>		
		public nsIDOMFile WrapDOMFile(nsIFile aFile)
		{
			throw new NotImplementedException();
		}
		
		/// <summary>
        /// Get the type of the currently focused html input, if any.
        /// </summary>		
		public string FocusedInputType
		{
			get { return _windowUtils.Instance.GetFocusedInputTypeAttribute(); }
		}

		
		/// <summary>
        /// Given a view ID from the compositor process, retrieve the element
        /// associated with a view. For scrollpanes for documents, the root
        /// element of the document is returned.
        /// </summary>		
		public GeckoHtmlElement FindElementWithViewId(System.IntPtr aId)
		{
			throw new NotImplementedException();
		}
		
		/// <summary>
        /// Checks the layer tree for this window and returns true
        /// if all layers have transforms that are translations by integers,
        /// no leaf layers overlap, and the union of the leaf layers is exactly
        /// the bounds of the window. Always returns true in non-DEBUG builds.
        /// </summary>		
		public bool LeafLayersPartitionWindow()
		{
			throw new NotImplementedException();
		}
		
		/// <summary>
        /// true if the (current inner) window may have event listeners for touch events.
        /// </summary>		
		public bool MayHaveTouchEventListeners
		{
			get{ throw new NotImplementedException(); }
		}
		
		/// <summary>
        /// Check if any ThebesLayer painting has been done for this element,
        /// clears the painted flags if they have.
        /// </summary>		
		public bool CheckAndClearPaintedState(GeckoHtmlElement aElement)
		{
			throw new NotImplementedException();
		}
		
		/// <summary>
        /// Get internal id of the stored blob.
        /// </summary>		
		public int GetFileId(nsIDOMBlob aBlob)
		{
			throw new NotImplementedException();
		}
		
		/// <summary>
        /// Get file ref count info for given database and file id.
        ///
        /// </summary>		
		
		public bool GetFileReferences(string aDatabaseName, long aId, ref int aRefCnt, ref int aDBRefCnt, ref int aSliceRefCnt)
		{
			throw new NotImplementedException();
		}
		
		/// <summary>
        /// Begin opcode-level profiling of all JavaScript execution in the window's
        /// runtime.
        /// </summary>		
		public void StartPCCountProfiling(System.IntPtr jsContext)
		{
			throw new NotImplementedException();
		}
		
		/// <summary>
        /// Stop opcode-level profiling of JavaScript execution in the runtime, and
        /// collect all counts for use by getPCCount methods.
        /// </summary>		
		public void StopPCCountProfiling(System.IntPtr jsContext)
		{
			throw new NotImplementedException();
		}
		
		/// <summary>
        /// Purge collected PC counters.
        /// </summary>		
		public void PurgePCCounts(System.IntPtr jsContext)
		{
			throw new NotImplementedException();
		}
		
		/// <summary>
        /// Get the number of scripts with opcode-level profiling information.
        /// </summary>		
		public int GetPCCountScriptCount(System.IntPtr jsContext)
		{
			throw new NotImplementedException();
		}
		
		/// <summary>
        /// Get a JSON string for a short summary of a script and the PC counts
        /// accumulated for it.
        /// </summary>		
		public string GetPCCountScriptSummary(int script, System.IntPtr jsContext)
		{
			throw new NotImplementedException();
		}
		
		/// <summary>
        /// Get a JSON string with full information about a profiled script,
        /// including the decompilation of the script and placement of decompiled
        /// operations within it, and PC counts for each operation.
        /// </summary>		
		public string GetPCCountScriptContents(int script, System.IntPtr jsContext)
		{
			throw new NotImplementedException();
		}
		
		/// <summary>
        /// Returns true if painting is suppressed for this window and false
        /// otherwise.
        /// </summary>		
		bool GetPaintingSuppressedAttribute()
		{
			throw new NotImplementedException();
		}
	}
}


