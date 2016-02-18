using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Gecko.Events;
using Gecko.Interop;
using Gecko.Net;
using System.Collections.Generic;

//EventHandlerList http://msdn.microsoft.com/en-us/library/system.componentmodel.eventhandlerlist.aspx
//Generic EventHandler http://msdn.microsoft.com/en-us/library/db0etb8x.aspx
//C# 3.0 in a nutshell http://books.google.com/books?id=_Y0rWd-Q2xkC&pg=PA113&lpg=PA113&dq=readonly+field+eventargs&source=bl&ots=R8CCSaWH-j&sig=s6ImztjXLnI2rrL-usLfoGmVR8g&hl=en&sa=X&ei=EctAT5baOM3Zsga9ocDYBA&ved=0CFkQ6AEwBw

// I have placed to readonly fields only basic .NET Framework objects - value types,strings, Uri's
namespace Gecko
{
	partial class GeckoWebBrowser
	{
		#region Event Keys
		// Navigation
		private static readonly object NavigatingEvent = new object();
		private static readonly object NavigatedEvent = new object();
		private static readonly object NavigationErrorEvent = new object();
		private static readonly object FrameNavigatingEvent = new object();
		private static readonly object DocumentCompletedEvent = new object();
		private static readonly object RedirectingEvent = new object();
		private static readonly object RetargetedEvent = new object();
		private static readonly object CanGoBackChangedEvent = new object();
		private static readonly object CanGoForwardChangedEvent = new object();
		// ProgressChanged
		private static readonly object RequestProgressChangedEvent = new object();
		private static readonly object ProgressChangedEvent = new object();
		// History
		private static readonly object HistoryNewEntryEvent = new object();
		private static readonly object HistoryGoBackEvent = new object();
		private static readonly object HistoryGoForwardEvent = new object();
		private static readonly object HistoryReloadEvent = new object();
		private static readonly object HistoryGotoIndexEvent = new object();
		private static readonly object HistoryPurgeEvent = new object();
		// Windows
		private static readonly object CreateWindowEvent = new object();
		private static readonly object CreateWindow2Event = new object();
		private static readonly object WindowSetBoundsEvent = new object();
		private static readonly object WindowClosedEvent = new object();
		// StatusTextChanged
		private static readonly object StatusTextChangedEvent = new object();
		// DocumentTitleChanged
		private static readonly object DocumentTitleChangedEvent = new object();
		// ShowContextMenu
		private static readonly object ShowContextMenuEvent = new object();
		// ObserveHttpModifyRequest
		private static readonly object ObserveHttpModifyRequestEvent = new object();
		// Security
		private static readonly object NSSErrorEvent = new object();		
		// Dom
		private static readonly object DomKeyDownEvent = new object();
		private static readonly object DomKeyUpEvent = new object();
		private static readonly object DomKeyPressEvent = new object();
		private static readonly object DomMouseDownEvent = new object();
		private static readonly object DomMouseUpEvent = new object();
		private static readonly object DomMouseOverEvent = new object();
		private static readonly object DomMouseOutEvent = new object();
		private static readonly object DomMouseMoveEvent = new object();
		private static readonly object DomContextMenuEvent = new object();
		private static readonly object DomMouseScrollEvent = new object();
		private static readonly object DomSubmitEvent = new object();
		private static readonly object DomCompositionStartEvent = new object();
		private static readonly object DomCompositionEndEvent = new object();
		private static readonly object DomFocusEvent = new object();
		private static readonly object DomBlurEvent = new object();
		private static readonly object LoadEvent = new object();
		private static readonly object DOMContentLoadedEvent = new object();
		private static readonly object ReadyStateChangeEvent = new object();
		private static readonly object HashChangeEvent = new object();
		private static readonly object DomContentChangedEvent = new object();
		private static readonly object DomClickEvent = new object();
		private static readonly object DomDoubleClickEvent = new object();
		private static readonly object DomDragStartEvent = new object();
		private static readonly object DomDragEnterEvent = new object();
		private static readonly object DomDragOverEvent = new object();
		private static readonly object DomDragLeaveEvent = new object();
		private static readonly object DomDragEvent = new object();
		private static readonly object DomDropEvent = new object();
		private static readonly object DomDragEndEvent = new object();
		private static readonly object FullscreenChangeEvent = new object();
		#endregion

		#region Navigation events

		#region public event EventHandler<GeckoNavigatingEventArgs> Navigating

		/// <summary>
		/// Occurs before the browser navigates to a new page.
		/// </summary>
		[Category( "Navigation" )]
		[Description( "Occurs before the browser navigates to a new page." )]
		public event EventHandler<GeckoNavigatingEventArgs> Navigating
		{
			add { Events.AddHandler( NavigatingEvent, value ); }
			remove { Events.RemoveHandler( NavigatingEvent, value ); }
		}

		/// <summary>Raises the <see cref="Navigating"/> event.</summary>
		/// <param name="e">The data for the event.</param>
		protected virtual void OnNavigating( GeckoNavigatingEventArgs e )
		{
			var evnt = ( ( EventHandler<GeckoNavigatingEventArgs> ) Events[ NavigatingEvent ] );
			if ( evnt != null ) evnt( this, e );
		}

		#endregion

		#region public event EventHandler<GeckoRetargetedEventArgs> Retargeted

		/// <summary>
		/// Occurs after the navigation is retargeted.
		/// E.g. the content-type can't be handled by browser or plugins, or the content is supposed to be downloaded.
		/// </summary>
		[Category("Navigation")]
		[Description("Occurs after the navigation is retargeted.")]
		public event EventHandler<GeckoRetargetedEventArgs> Retargeted
		{
			add { Events.AddHandler(RetargetedEvent, value); }
			remove { Events.RemoveHandler(RetargetedEvent, value); }
		}

		/// <summary>Raises the <see cref="Retargeted"/> event.</summary>
		/// <param name="e">The data for the event.</param>
		protected virtual void OnRetargeted(GeckoRetargetedEventArgs e)
		{
			var evnt = ((EventHandler<GeckoRetargetedEventArgs>)Events[RetargetedEvent]);
			if (evnt != null) evnt(this, e);
		}

		#endregion

		#region public event EventHandler<GeckoNavigatedEventArgs> Navigated

		/// <summary>
		/// Occurs after the browser has navigated to a new page.
		/// </summary>
		[Category( "Navigation" )]
		[Description( "Occurs after the browser has navigated to a new page." )]
		public event EventHandler<GeckoNavigatedEventArgs> Navigated
		{
			add { Events.AddHandler( NavigatedEvent, value ); }
			remove { Events.RemoveHandler( NavigatedEvent, value ); }
		}

		/// <summary>Raises the <see cref="Navigated"/> event.</summary>
		/// <param name="e">The data for the event.</param>
		protected virtual void OnNavigated( GeckoNavigatedEventArgs e )
		{
			var evnt = ( EventHandler<GeckoNavigatedEventArgs> ) Events[ NavigatedEvent ];
			if ( evnt != null ) evnt( this, e );
		}

		#endregion

		#region public event EventHandler<GeckoNavigationErrorEventArgs> NavigationError

		/// <summary>
		/// Occurs when navigation to a new page has failed or has been aborted by user.
		/// </summary>
		[Category( "Navigation" )]
		[Description( "Occurs when navigation to a new page has failed or has been aborted by user." )]
		public event EventHandler<GeckoNavigationErrorEventArgs> NavigationError
		{
			add { Events.AddHandler( NavigationErrorEvent, value ); }
			remove { Events.RemoveHandler( NavigationErrorEvent, value ); }
		}

		/// <summary>Raises the <see cref="NavigationError"/> event.</summary>
		/// <param name="e">The data for the event.</param>
		protected virtual void OnNavigationError( GeckoNavigationErrorEventArgs e )
		{
			var evnt = ( ( EventHandler<GeckoNavigationErrorEventArgs> ) Events[ NavigationErrorEvent ] );
			if ( evnt != null ) evnt( this, e );
		}

		#endregion

		#region public event EventHandler<GeckoRedirectingEventArgs> Redirecting

		/// <summary>
		/// Occurs before the browser redirects to a new page.
		/// </summary>
		[Category("Navigation")]
		[Description("Occurs before the browser redirects to a new page.")]
		public event EventHandler<GeckoRedirectingEventArgs> Redirecting
		{
			add
			{
				Events.AddHandler(RedirectingEvent, value);
			}
			remove
			{
				Events.RemoveHandler(RedirectingEvent, value);
			}
		}

		/// <summary>Raises the <see cref="Redirecting"/> event.</summary>
		/// <param name="e">The data for the event.</param>
		protected virtual void OnRedirecting(GeckoRedirectingEventArgs e)
		{
			var evnt = ((EventHandler<GeckoRedirectingEventArgs>)Events[RedirectingEvent]);
			if (evnt != null)
				evnt(this, e);
		}

		#endregion

		#region public event EventHandler<GeckoNavigatingEventArgs> FrameNavigating

		/// <summary>
		/// Occurs before the browser navigates to a new frame.
		/// </summary>
		[Category("Navigation")]
		[Description("Occurs before the browser navigates to a new frame.")]
		public event EventHandler<GeckoNavigatingEventArgs> FrameNavigating
		{
			add { Events.AddHandler(FrameNavigatingEvent, value); }
			remove { Events.RemoveHandler(FrameNavigatingEvent, value); }
		}

		/// <summary>Raises the <see cref="FrameNavigating"/> event.</summary>
		/// <param name="e">The data for the event.</param>
		protected virtual void OnFrameNavigating(GeckoNavigatingEventArgs e)
		{
			var evnt = ((EventHandler<GeckoNavigatingEventArgs>)Events[FrameNavigatingEvent]);
			if (evnt != null) evnt(this, e);
		}

		#endregion


		#region public event EventHandler DocumentCompleted<GeckoDocumentCompletedEventArgs>

		/// <summary>
		/// Occurs after the browser has finished parsing a new page and updated the <see cref="Document"/> property.
		/// </summary>
		[Category( "Navigation" )]
		[Description( "Occurs after the browser has finished parsing a new page and updated the Document property." )]
		public event EventHandler<GeckoDocumentCompletedEventArgs> DocumentCompleted
		{
			add { Events.AddHandler( DocumentCompletedEvent, value ); }
			remove { Events.RemoveHandler( DocumentCompletedEvent, value ); }
		}

		/// <summary>Raises the <see cref="DocumentCompleted"/> event.</summary>
		/// <param name="e">The data for the event.</param>
		protected virtual void OnDocumentCompleted(GeckoDocumentCompletedEventArgs e)
		{
			var evnt = (EventHandler<GeckoDocumentCompletedEventArgs>)Events[DocumentCompletedEvent];
			if ( evnt != null ) evnt( this, e );
		}

		#endregion

		#region public event EventHandler CanGoBackChanged

		/// <summary>
		/// Occurs when the value of the <see cref="CanGoBack"/> property is changed.
		/// </summary>
		[Category( "Property Changed" )]
		[Description( "Occurs when the value of the CanGoBack property is changed." )]
		public event EventHandler CanGoBackChanged
		{
			add { Events.AddHandler( CanGoBackChangedEvent, value ); }
			remove { Events.RemoveHandler( CanGoBackChangedEvent, value ); }
		}

		/// <summary>Raises the <see cref="CanGoBackChanged"/> event.</summary>
		/// <param name="e">The data for the event.</param>
		protected virtual void OnCanGoBackChanged( EventArgs e )
		{
			var evnt = ( EventHandler ) Events[ CanGoBackChangedEvent ];
			if ( evnt != null ) evnt( this, e );
		}

		#endregion

		#region public event EventHandler CanGoForwardChanged

		/// <summary>
		/// Occurs when the value of the <see cref="CanGoForward"/> property is changed.
		/// </summary>
		[Category( "Property Changed" )]
		[Description( "Occurs when the value of the CanGoForward property is changed." )]
		public event EventHandler CanGoForwardChanged
		{
			add { Events.AddHandler( CanGoForwardChangedEvent, value ); }
			remove { Events.RemoveHandler( CanGoForwardChangedEvent, value ); }
		}

		/// <summary>Raises the <see cref="CanGoForwardChanged"/> event.</summary>
		/// <param name="e">The data for the event.</param>
		protected virtual void OnCanGoForwardChanged( EventArgs e )
		{
			var evnt = ( EventHandler ) Events[ CanGoForwardChangedEvent ];
			if ( evnt != null ) evnt( this, e );
		}

		#endregion

		#endregion

		#region History events

		#region public event EventHandler<GeckoHistoryEventArgs> HistoryNewEntry

		[Category( "History" )]
		public event EventHandler<GeckoHistoryEventArgs> HistoryNewEntry
		{
			add { Events.AddHandler( HistoryNewEntryEvent, value ); }
			remove { Events.RemoveHandler( HistoryNewEntryEvent, value ); }
		}

		/// <summary>Raises the <see cref="HistoryNewEntry"/> event.</summary>
		/// <param name="e">The data for the event.</param>
		protected virtual void OnHistoryNewEntry( GeckoHistoryEventArgs e )
		{
			var evnt = ( EventHandler<GeckoHistoryEventArgs> ) Events[ HistoryNewEntryEvent ];
			if ( evnt != null ) evnt( this, e );
		}

		#endregion

		#region public event EventHandler<GeckoHistoryEventArgs> HistoryGoBack

		[Category( "History" )]
		public event EventHandler<GeckoHistoryEventArgs> HistoryGoBack
		{
			add { Events.AddHandler( HistoryGoBackEvent, value ); }
			remove { Events.RemoveHandler( HistoryGoBackEvent, value ); }
		}

		/// <summary>Raises the <see cref="HistoryGoBack"/> event.</summary>
		/// <param name="e">The data for the event.</param>
		protected virtual void OnHistoryGoBack( GeckoHistoryEventArgs e )
		{
			var evnt = ( EventHandler<GeckoHistoryEventArgs> ) Events[ HistoryGoBackEvent ];
			if ( evnt != null ) evnt( this, e );
		}

		#endregion

		#region public event EventHandler<GeckoHistoryEventArgs> HistoryGoForward

		[Category( "History" )]
		public event EventHandler<GeckoHistoryEventArgs> HistoryGoForward
		{
			add { Events.AddHandler( HistoryGoForwardEvent, value ); }
			remove { Events.RemoveHandler( HistoryGoForwardEvent, value ); }
		}

		/// <summary>Raises the <see cref="HistoryGoForward"/> event.</summary>
		/// <param name="e">The data for the event.</param>
		protected virtual void OnHistoryGoForward( GeckoHistoryEventArgs e )
		{
			var evnt = ( EventHandler<GeckoHistoryEventArgs> ) Events[ HistoryGoForwardEvent ];
			if ( evnt != null ) evnt( this, e );
		}

		#endregion

		#region public event EventHandler<GeckoHistoryEventArgs> HistoryReload

		[Category( "History" )]
		public event EventHandler<GeckoHistoryEventArgs> HistoryReload
		{
			add { Events.AddHandler( HistoryReloadEvent, value ); }
			remove { Events.RemoveHandler( HistoryReloadEvent, value ); }
		}

		/// <summary>Raises the <see cref="HistoryReload"/> event.</summary>
		/// <param name="e">The data for the event.</param>
		protected virtual void OnHistoryReload( GeckoHistoryEventArgs e )
		{
			var evnt = ( EventHandler<GeckoHistoryEventArgs> ) Events[ HistoryReloadEvent ];
			if ( evnt != null ) evnt( this, e );
		}

		#endregion

		#region public event GeckoHistoryGotoIndexEventHandler HistoryGotoIndex

		[Category( "History" )]
		public event EventHandler<GeckoHistoryGotoIndexEventArgs> HistoryGotoIndex
		{
			add { Events.AddHandler( HistoryGotoIndexEvent, value ); }
			remove { Events.RemoveHandler( HistoryGotoIndexEvent, value ); }
		}

		/// <summary>Raises the <see cref="HistoryGotoIndex"/> event.</summary>
		/// <param name="e">The data for the event.</param>
		protected virtual void OnHistoryGotoIndex( GeckoHistoryGotoIndexEventArgs e )
		{
			var evnt = ( EventHandler<GeckoHistoryGotoIndexEventArgs> ) Events[ HistoryGotoIndexEvent ];
			if ( evnt != null ) evnt( this, e );
		}

		#endregion

		#region public event GeckoHistoryPurgeEventHandler HistoryPurge

		[Category( "History" )]
		public event EventHandler<GeckoHistoryPurgeEventArgs> HistoryPurge
		{
			add { Events.AddHandler( HistoryPurgeEvent, value ); }
			remove { Events.RemoveHandler( HistoryPurgeEvent, value ); }
		}

		/// <summary>Raises the <see cref="HistoryPurge"/> event.</summary>
		/// <param name="e">The data for the event.</param>
		protected virtual void OnHistoryPurge( GeckoHistoryPurgeEventArgs e )
		{
			var evnt = ( EventHandler<GeckoHistoryPurgeEventArgs> ) Events[ HistoryPurgeEvent ];
			if ( evnt != null ) evnt( this, e );
		}

		#endregion

		#endregion

		#region public event GeckoRequestProgressEventHandler ProgressChanged

		/// <summary>
		/// Occurs when the control has updated progress information.
		/// </summary>
		[Category("Navigation")]
		[Description("Occurs when the Request has updated progress information.")]
		public event EventHandler<GeckoRequestProgressEventArgs> RequestProgressChanged
		{
			add
			{
				Events.AddHandler(RequestProgressChangedEvent, value);
			}
			remove
			{
				Events.RemoveHandler(RequestProgressChangedEvent, value);
			}
		}

		/// <summary>Raises the <see cref="ProgressChanged"/> event.</summary>
		/// <param name="e">The data for the event.</param>
		protected virtual void OnRequestProgressChanged(GeckoRequestProgressEventArgs e)
		{
			var evnt = (EventHandler<GeckoRequestProgressEventArgs>)Events[RequestProgressChangedEvent];
			if (evnt != null)
				evnt(this, e);
		}

		#endregion

		#region public event GeckoRequestProgressEventHandler ProgressChanged

		/// <summary>
		/// Occurs when the control has updated progress information.
		/// </summary>
		[Category( "Navigation" )]
		[Description( "Occurs when the control has updated progress information." )]
		public event EventHandler<GeckoProgressEventArgs> ProgressChanged
		{
			add { Events.AddHandler( ProgressChangedEvent, value ); }
			remove { Events.RemoveHandler( ProgressChangedEvent, value ); }
		}


		/// <summary>Raises the <see cref="ProgressChanged"/> event.</summary>
		/// <param name="e">The data for the event.</param>
		protected virtual void OnProgressChanged( GeckoProgressEventArgs e )
		{
			var evnt = ( EventHandler<GeckoProgressEventArgs> ) Events[ ProgressChangedEvent ];
			if ( evnt != null ) evnt( this, e );
		}

		#endregion

		#region Window events

		#region public event GeckoCreateWindowEventHandler CreateWindow

		public event EventHandler<GeckoCreateWindowEventArgs> CreateWindow
		{
			add { Events.AddHandler( CreateWindowEvent, value ); }
			remove { Events.RemoveHandler( CreateWindowEvent, value ); }
		}

		/// <summary>Raises the <see cref="CreateWindow"/> event.</summary>
		/// <param name="e">The data for the event.</param>
		protected virtual void OnCreateWindow( GeckoCreateWindowEventArgs e )
		{
			var evnt = ( EventHandler<GeckoCreateWindowEventArgs> ) Events[ CreateWindowEvent ];
			if ( evnt != null ) evnt( this, e );
		}

		#endregion

		#region public event GeckoCreateWindow2EventHandler CreateWindow2

		[Obsolete("Merged to CreateWindow event, just use it")]
		public event EventHandler<GeckoCreateWindow2EventArgs> CreateWindow2
		{
			add { Events.AddHandler( CreateWindow2Event, value ); }
			remove { Events.RemoveHandler( CreateWindow2Event, value ); }
		}

		/// <summary>Raises the <see cref="CreateWindow"/> event.</summary>
		/// <param name="e">The data for the event.</param>
		protected virtual void OnCreateWindow2( GeckoCreateWindow2EventArgs e )
		{
			var evnt = ( EventHandler<GeckoCreateWindow2EventArgs> ) Events[ CreateWindow2Event ];
			if ( evnt != null ) evnt( this, e );
		}

		#endregion

		#region public event GeckoWindowSetBoundsEventHandler WindowSetBounds



		public event EventHandler<GeckoWindowSetBoundsEventArgs> WindowSetBounds
		{
			add { Events.AddHandler( WindowSetBoundsEvent, value ); }
			remove { Events.RemoveHandler( WindowSetBoundsEvent, value ); }
		}

		/// <summary>Raises the <see cref="WindowSetBounds"/> event.</summary>
		/// <param name="e">The data for the event.</param>
		protected virtual void OnWindowSetBounds( GeckoWindowSetBoundsEventArgs e )
		{
			var evnt = (EventHandler<GeckoWindowSetBoundsEventArgs>)Events[WindowSetBoundsEvent];
			if ( evnt != null ) evnt( this, e );
		}

		#endregion


		#region public event EventHandler WindowClosed
		public event EventHandler WindowClosed
		{
			add { Events.AddHandler(WindowClosedEvent, value); }
			remove { Events.RemoveHandler(WindowClosedEvent, value); }
		}
		

		/// <summary>Raises the <see cref="WindowClosed"/> event.</summary>
		/// <param name="e">The data for the event.</param>
		protected virtual void OnWindowClosed(EventArgs e)
		{
			var evnt = ( EventHandler ) Events[ WindowClosedEvent ];
			if (evnt != null) evnt(this, e);
		}
		#endregion
		#endregion

		#region public event EventHandler StatusTextChanged

		/// <summary>
		/// Occurs when the value of the <see cref="StatusText"/> property is changed.
		/// </summary>
		[Category( "Property Changed" )]
		[Description( "Occurs when the value of the StatusText property is changed." )]
		public event EventHandler StatusTextChanged
		{
			add { Events.AddHandler( StatusTextChangedEvent, value ); }
			remove { Events.RemoveHandler( StatusTextChangedEvent, value ); }
		}

		/// <summary>Raises the <see cref="StatusTextChanged"/> event.</summary>
		/// <param name="e">The data for the event.</param>
		protected virtual void OnStatusTextChanged( EventArgs e )
		{
			var evnt = ( EventHandler ) Events[ StatusTextChangedEvent ];
			if ( evnt != null ) evnt( this, e );
		}

		#endregion

		#region public event EventHandler DocumentTitleChanged

		/// <summary>
		/// Occurs when the value of the <see cref="DocumentTitle"/> property is changed.
		/// </summary>
		[Category( "Property Changed" )]
		[Description( "Occurs when the value of the DocumentTitle property is changed." )]
		public event EventHandler DocumentTitleChanged
		{
			add { Events.AddHandler( DocumentTitleChangedEvent, value ); }
			remove { Events.RemoveHandler( DocumentTitleChangedEvent, value ); }
		}

		/// <summary>Raises the <see cref="DocumentTitleChanged"/> event.</summary>
		/// <param name="e">The data for the event.</param>
		protected virtual void OnDocumentTitleChanged( EventArgs e )
		{
			var evnt = ( EventHandler ) Events[ DocumentTitleChangedEvent ];
			if ( evnt != null ) evnt( this, e );
		}

		#endregion

		#region public event GeckoContextMenuEventHandler ShowContextMenu

		public event EventHandler<GeckoContextMenuEventArgs> ShowContextMenu
		{
			add { Events.AddHandler( ShowContextMenuEvent, value ); }
			remove { Events.RemoveHandler( ShowContextMenuEvent, value ); }
		}

		/// <summary>Raises the <see cref="ShowContextMenu"/> event.</summary>
		/// <param name="e">The data for the event.</param>
		protected virtual void OnShowContextMenu( GeckoContextMenuEventArgs e )
		{
			var evnt = ( EventHandler<GeckoContextMenuEventArgs> ) Events[ ShowContextMenuEvent ];
			if ( evnt != null ) evnt( this, e );
		}

		#endregion

		#region public event GeckoObserveHttpModifyRequestEventHandler Navigated

		/// <summary>
		/// Occurs after the browser has send a http request to the web
		/// </summary>
		[Category( "Observe" )]
		[Description( "Occurs after the browser has navigated to a new page." )]
		public event EventHandler<GeckoObserveHttpModifyRequestEventArgs> ObserveHttpModifyRequest
		{
			add { Events.AddHandler( ObserveHttpModifyRequestEvent, value ); }
			remove { Events.RemoveHandler( ObserveHttpModifyRequestEvent, value ); }
		}

		/// <summary>Raises the <see cref="ObserveHttpModify"/> event.</summary>
		/// <param name="e">The data for the event.</param>
		protected virtual void OnObserveHttpModifyRequest(GeckoObserveHttpModifyRequestEventArgs e)
		{
			var evnt = ( EventHandler<GeckoObserveHttpModifyRequestEventArgs> ) Events[ ObserveHttpModifyRequestEvent ];
			if ( evnt != null ) evnt( this, e );
		}

		#endregion

		#region Dom EventHandlers

		#region Dom keyboard events
		#region public event GeckoDomKeyEventHandler DomKeyDown
		[Category("DOM Events")]
		public event EventHandler<DomKeyEventArgs> DomKeyDown
		{
			add { Events.AddHandler(DomKeyDownEvent, value); }
			remove { Events.RemoveHandler(DomKeyDownEvent, value); }
		}

		/// <summary>Raises the <see cref="DomKeyDown"/> event.</summary>
		/// <param name="e">The data for the event.</param>
		protected virtual void OnDomKeyDown(DomKeyEventArgs e)
		{
			var evnt = (EventHandler<DomKeyEventArgs>)Events[DomKeyDownEvent];
			if (evnt != null) evnt(this, e);
		}
		#endregion

		#region public event GeckoDomKeyEventHandler DomKeyUp
		[Category("DOM Events")]
		public event EventHandler<DomKeyEventArgs> DomKeyUp
		{
			add { Events.AddHandler(DomKeyUpEvent, value); }
			remove { Events.RemoveHandler(DomKeyUpEvent, value); }
		}

		/// <summary>Raises the <see cref="DomKeyUp"/> event.</summary>
		/// <param name="e">The data for the event.</param>
		protected virtual void OnDomKeyUp(DomKeyEventArgs e)
		{
			var evnt = (EventHandler<DomKeyEventArgs>)Events[DomKeyUpEvent];
			if (evnt != null) evnt(this, e);
		}
		#endregion

		#region public event GeckoDomKeyEventHandler DomKeyPress
		[Category("DOM Events")]
		public event EventHandler<DomKeyEventArgs> DomKeyPress
		{
			add { Events.AddHandler(DomKeyPressEvent, value); }
			remove { Events.RemoveHandler(DomKeyPressEvent, value); }
		}

		/// <summary>Raises the <see cref="DomKeyPress"/> event.</summary>
		/// <param name="e">The data for the event.</param>
		protected virtual void OnDomKeyPress(DomKeyEventArgs e)
		{
			var evnt = (EventHandler<DomKeyEventArgs>)Events[DomKeyPressEvent];
			if (evnt != null) evnt(this, e);
		}
		#endregion
		#endregion

		#region Dom mouse events
		#region public event GeckoDomMouseEventHandler DomMouseDown
		[Category("DOM Events")]
		public event EventHandler<DomMouseEventArgs> DomMouseDown
		{
			add { Events.AddHandler(DomMouseDownEvent, value); }
			remove { Events.RemoveHandler(DomMouseDownEvent, value); }
		}
		
		/// <summary>Raises the <see cref="DomMouseDown"/> event.</summary>
		/// <param name="e">The data for the event.</param>
		protected virtual void OnDomMouseDown(DomMouseEventArgs e)
		{
			var evnt = (EventHandler<DomMouseEventArgs>)Events[DomMouseDownEvent];
			if (evnt != null) evnt(this, e);
		}
		#endregion

		#region public event GeckoDomMouseEventHandler DomMouseUp
		[Category("DOM Events")]
		public event EventHandler<DomMouseEventArgs> DomMouseUp
		{
			add { Events.AddHandler(DomMouseUpEvent, value); }
			remove { Events.RemoveHandler(DomMouseUpEvent, value); }
		}

		/// <summary>Raises the <see cref="DomMouseUp"/> event.</summary>
		/// <param name="e">The data for the event.</param>
		protected virtual void OnDomMouseUp(DomMouseEventArgs e)
		{
			var evnt = (EventHandler<DomMouseEventArgs>)Events[DomMouseUpEvent];
			if (evnt != null) evnt(this, e);
		}
		#endregion

		#region public event GeckoDomMouseEventHandler DomMouseOver
		[Category("DOM Events")]
		public event EventHandler<DomMouseEventArgs> DomMouseOver
		{
			add { Events.AddHandler(DomMouseOverEvent, value); }
			remove { Events.RemoveHandler(DomMouseOverEvent, value); }
		}
		

		/// <summary>Raises the <see cref="DomMouseOver"/> event.</summary>
		/// <param name="e">The data for the event.</param>
		protected virtual void OnDomMouseOver(DomMouseEventArgs e)
		{
			var evnt = (EventHandler<DomMouseEventArgs>)this.Events[DomMouseOverEvent];
			if (evnt != null) evnt(this, e);
		}
		#endregion

		#region public event GeckoDomMouseEventHandler DomMouseOut
		[Category("DOM Events")]
		public event EventHandler<DomMouseEventArgs> DomMouseOut
		{
			add { Events.AddHandler(DomMouseOutEvent, value); }
			remove { Events.RemoveHandler(DomMouseOutEvent, value); }
		}
		
		/// <summary>Raises the <see cref="DomMouseOut"/> event.</summary>
		/// <param name="e">The data for the event.</param>
		protected virtual void OnDomMouseOut(DomMouseEventArgs e)
		{
			var evnt = (EventHandler<DomMouseEventArgs>)Events[DomMouseOutEvent];
			if (evnt != null) evnt( this, e );
		}
		#endregion

		#region public event GeckoDomMouseEventHandler DomMouseMove
		[Category("DOM Events")]
		public event EventHandler<DomMouseEventArgs> DomMouseMove
		{
			add { Events.AddHandler(DomMouseMoveEvent, value); }
			remove { Events.RemoveHandler(DomMouseMoveEvent, value); }
		}
		
		/// <summary>Raises the <see cref="DomMouseMove"/> event.</summary>
		/// <param name="e">The data for the event.</param>
		protected virtual void OnDomMouseMove(DomMouseEventArgs e)
		{
			var evnt = (EventHandler<DomMouseEventArgs>)Events[DomMouseMoveEvent];
			if (evnt != null) evnt(this, e);
		}
		#endregion

		#region public event GeckoDomMouseEventHandler DomContextMenu
		[Category("DOM Events")]
		public event EventHandler<DomMouseEventArgs> DomContextMenu
		{
			add { Events.AddHandler(DomContextMenuEvent, value); }
			remove { Events.RemoveHandler(DomContextMenuEvent, value); }
		}
		
		/// <summary>Raises the <see cref="DomContextMenu"/> event.</summary>
		/// <param name="e">The data for the event.</param>
		protected virtual void OnDomContextMenu(DomMouseEventArgs e)
		{
			var evnt = (EventHandler<DomMouseEventArgs>)Events[DomContextMenuEvent];
			if (evnt != null)evnt(this, e);
		}
		#endregion

		#region public event GeckoDomMouseEventHandler DOMMouseScroll
		[Category("DOM Events")]
		public event EventHandler<DomMouseEventArgs> DomMouseScroll
		{
			add { Events.AddHandler(DomMouseScrollEvent, value); }
			remove { Events.RemoveHandler(DomMouseScrollEvent, value); }
		}

		/// <summary>Raises the <see cref="DomMouseScroll"/> event.</summary>
		/// <param name="e">The data for the event.</param>
		protected virtual void OnDomMouseScroll(DomMouseEventArgs e)
		{
			var evnt = (EventHandler<DomMouseEventArgs>)Events[DomMouseScrollEvent];
			if (evnt != null) evnt(this, e);
		}
		#endregion
		#endregion

		#region public event GeckoDomEventHandler DomSubmit
		[Category("DOM Events")]
		public event EventHandler<DomEventArgs> DomSubmit
		{
			add { Events.AddHandler(DomSubmitEvent, value); }
			remove { Events.RemoveHandler(DomSubmitEvent, value); }
		}
		
		/// <summary>Raises the <see cref="DomSubmit"/> event.</summary>
		/// <param name="e">The data for the event.</param>
		protected virtual void OnDomSubmit(DomEventArgs e)
		{
			var evnt = (EventHandler<DomEventArgs>)Events[DomSubmitEvent];
			if (evnt != null) evnt(this, e);
		}
		#endregion

		#region public event GeckoDomEventHandler DomCompositionStart
		[Category("DOM Events")]
		public event EventHandler<DomEventArgs> DomCompositionStart
		{
			add { Events.AddHandler(DomCompositionStartEvent, value); }
			remove { Events.RemoveHandler(DomCompositionStartEvent, value); }
		}
		
		/// <summary>Raises the <see cref="DomCompositionStart"/> event.</summary>
		/// <param name="e">The data for the event.</param>
		protected virtual void OnDomCompositionStart(DomEventArgs e)
		{
			var evnt = (EventHandler<DomEventArgs>)Events[DomCompositionStartEvent];
			if (evnt != null) evnt(this, e);
		}
		#endregion

		#region public event GeckoDomEventHandler DomCompositionEnd
		[Category("DOM Events")]
		public event EventHandler<DomEventArgs> DomCompositionEnd
		{
			add { Events.AddHandler(DomCompositionEndEvent, value); }
			remove { Events.RemoveHandler(DomCompositionEndEvent, value); }
		}
		
		/// <summary>Raises the <see cref="DomCompositionEnd"/> event.</summary>
		/// <param name="e">The data for the event.</param>
		protected virtual void OnDomCompositionEnd(DomEventArgs e)
		{
			var evnt = (EventHandler<DomEventArgs>)Events[DomCompositionEndEvent];
			if (evnt != null) evnt(this, e);
		}
		#endregion

		#region public event GeckoDomEventHandler DomFocus
		[Category("DOM Events")]
		public event EventHandler<DomEventArgs> DomFocus
		{
			add { Events.AddHandler(DomFocusEvent, value); }
			remove { Events.RemoveHandler(DomFocusEvent, value); }
		}
		
		/// <summary>Raises the <see cref="DomFocus"/> event.</summary>
		/// <param name="e">The data for the event.</param>
		protected virtual void OnDomFocus(DomEventArgs e)
		{
			var evnt = (EventHandler<DomEventArgs>)Events[DomFocusEvent];
			if (evnt != null) evnt(this, e);
		}
		#endregion

		#region public event GeckoDomEventHandler DomBlur
		[Category("DOM Events")]
		public event EventHandler<DomEventArgs> DomBlur
		{
			add { Events.AddHandler(DomBlurEvent, value); }
			remove { Events.RemoveHandler(DomBlurEvent, value); }
		}

		/// <summary>Raises the <see cref="DomBlur"/> event.</summary>
		/// <param name="e">The data for the event.</param>
		protected virtual void OnDomBlur(DomEventArgs e)
		{
			var evnt = (EventHandler<DomEventArgs>)Events[DomBlurEvent];
			if (evnt != null) evnt(this, e);
		}
		#endregion

		#region public event GeckoDomEventHandler Load
		[Category("DOM Events")]
		public event EventHandler<DomEventArgs> Load
		{
			add { Events.AddHandler(LoadEvent, value); }
			remove { Events.RemoveHandler(LoadEvent, value); }
		}
		
		/// <summary>Raises the <see cref="LoadEvent"/> event.</summary>
		/// <param name="e">The data for the event.</param>
		protected virtual void OnLoad(DomEventArgs e)
		{
			var evnt = (EventHandler<DomEventArgs>)Events[LoadEvent];
			if (evnt != null) evnt(this, e);
		}
		#endregion

		#region public event GeckoDomEventHandler DOMContentLoaded
		[Category("DOM Events")]
		public event EventHandler<DomEventArgs> DOMContentLoaded {
			add {
				Events.AddHandler(DOMContentLoadedEvent, value);
			}
			remove {
				Events.RemoveHandler(DOMContentLoadedEvent, value);
			}
		}

		/// <summary>Raises the <see cref="DOMContentLoadedEvent"/> event.</summary>
		/// <param name="e">The data for the event.</param>
		protected virtual void OnDOMContentLoaded(DomEventArgs e) {
			var evnt = (EventHandler<DomEventArgs>)Events[DOMContentLoadedEvent];
			if (evnt != null)
				evnt(this, e);
		}
		#endregion

		#region public event GeckoDomEventHandler ReadyStateChange
		[Category("DOM Events")]
		public event EventHandler<DomEventArgs> ReadyStateChange {
			add {
				Events.AddHandler(ReadyStateChangeEvent, value);
			}
			remove {
				Events.RemoveHandler(ReadyStateChangeEvent, value);
			}
		}

		/// <summary>Raises the <see cref="ReadyStateChangeEvent"/> event.</summary>
		/// <param name="e">The data for the event.</param>
		protected virtual void OnReadyStateChange(DomEventArgs e) {
			var evnt = (EventHandler<DomEventArgs>)Events[ReadyStateChangeEvent];
			if (evnt != null)
				evnt(this, e);
		}
		#endregion

        #region public event GeckoDomEventHandler HashChange
        [Category("DOM Events")]
		public event EventHandler<DomHashChangeEventArgs> HashChange
        {
            add { Events.AddHandler(HashChangeEvent, value); }
            remove { Events.RemoveHandler(HashChangeEvent, value); }
        }

        /// <summary>Raises the <see cref="HashChangeEvent"/> event.</summary>
        /// <param name="e">The data for the event.</param>
		protected virtual void OnHashChange(DomHashChangeEventArgs e)
        {
            var evnt = (EventHandler<DomHashChangeEventArgs>)Events[HashChangeEvent];
            if (evnt != null) evnt(this, e);
        }
        #endregion

		#region drag events

		// DragStart

		public event EventHandler<DomDragEventArgs> DomDragStart
		{
			add { Events.AddHandler(DomDragStartEvent, value); }
			remove { Events.RemoveHandler(DomDragStartEvent, value); }
		}

		/// <summary>Raises the <see cref="DomDragStart"/> event.</summary>
		/// <param name="e">The data for the event.</param>
		protected virtual void OnDomDragStart(DomDragEventArgs e)
		{
			var evnt = (EventHandler<DomDragEventArgs>)Events[DomDragStartEvent];
			if (evnt != null) evnt(this, e);
		}

		// DragEnter

		public event EventHandler<DomDragEventArgs> DomDragEnter
		{
			add { Events.AddHandler(DomDragEnterEvent, value); }
			remove { Events.RemoveHandler(DomDragEnterEvent, value); }
		}

		/// <summary>Raises the <see cref="DomDragEnter"/> event.</summary>
		/// <param name="e">The data for the event.</param>
		protected virtual void OnDomDragEnter(DomDragEventArgs e)
		{
			var evnt = (EventHandler<DomDragEventArgs>)Events[DomDragEnterEvent];
			if (evnt != null) evnt(this, e);
		}

		// DragOver

		public event EventHandler<DomDragEventArgs> DomDragOver
		{
			add { Events.AddHandler(DomDragOverEvent, value); }
			remove { Events.RemoveHandler(DomDragOverEvent, value); }
		}

		/// <summary>Raises the <see cref="DomDragOver"/> event.</summary>
		/// <param name="e">The data for the event.</param>
		protected virtual void OnDomDragOver(DomDragEventArgs e)
		{
			var evnt = (EventHandler<DomDragEventArgs>)Events[DomDragOverEvent];
			if (evnt != null) evnt(this, e);
		}

		// DragLeave

		public event EventHandler<DomDragEventArgs> DomDragLeave
		{
			add { Events.AddHandler(DomDragLeaveEvent, value); }
			remove { Events.RemoveHandler(DomDragLeaveEvent, value); }
		}

		/// <summary>Raises the <see cref="DomDragLeave"/> event.</summary>
		/// <param name="e">The data for the event.</param>
		protected virtual void OnDomDragLeave(DomDragEventArgs e)
		{
			var evnt = (EventHandler<DomDragEventArgs>)Events[DomDragLeaveEvent];
			if (evnt != null) evnt(this, e);
		}

		// Drag

		public event EventHandler<DomDragEventArgs> DomDrag
		{
			add { Events.AddHandler(DomDragEvent, value); }
			remove { Events.RemoveHandler(DomDragEvent, value); }
		}

		/// <summary>Raises the <see cref="DomDrag"/> event.</summary>
		/// <param name="e">The data for the event.</param>
		protected virtual void OnDomDrag(DomDragEventArgs e)
		{
			var evnt = (EventHandler<DomDragEventArgs>)Events[DomDragEvent];
			if (evnt != null) evnt(this, e);
		}

		// Drop

		public event EventHandler<DomDragEventArgs> DomDrop
		{
			add { Events.AddHandler(DomDropEvent, value); }
			remove { Events.RemoveHandler(DomDropEvent, value); }
		}

		/// <summary>Raises the <see cref="DomDrop"/> event.</summary>
		/// <param name="e">The data for the event.</param>
		protected virtual void OnDomDrop(DomDragEventArgs e)
		{
			var evnt = (EventHandler<DomDragEventArgs>)Events[DomDropEvent];
			if (evnt != null) evnt(this, e);
		}

		// DragEnd

		public event EventHandler<DomDragEventArgs> DomDragEnd
		{
			add { Events.AddHandler(DomDragEndEvent, value); }
			remove { Events.RemoveHandler(DomDragEndEvent, value); }
		}

		/// <summary>Raises the <see cref="DomDragEnd"/> event.</summary>
		/// <param name="e">The data for the event.</param>
		protected virtual void OnDomDragEnd(DomDragEventArgs e)
		{
			var evnt = (EventHandler<DomDragEventArgs>)Events[DomDragEndEvent];
			if (evnt != null) evnt(this, e);
		}

		#endregion

		#region public event GeckoDomEventHandler DomContentChanged
		[Category("DOM Events")]
		public event EventHandler<DomEventArgs> DomContentChanged
		{
			add { Events.AddHandler(DomContentChangedEvent, value); }
			remove { Events.RemoveHandler(DomContentChangedEvent, value); }
		}

		/// <summary>Raises the <see cref="DomContentChanged"/> event.</summary>
		/// <param name="e">The data for the event.</param>
		protected virtual void OnDomContentChanged(DomEventArgs e)
		{
			var evnt = (EventHandler<DomEventArgs>)Events[DomContentChangedEvent];
			if (evnt != null) evnt(this, e);
		}
		#endregion

		#region public event GeckoDomEventHandler DomClick
		[Category("DOM Events")]
		public event EventHandler<DomMouseEventArgs> DomClick
		{
			add { Events.AddHandler(DomClickEvent, value); }
			remove { Events.RemoveHandler(DomClickEvent, value); }
		}
		
		/// <summary>Raises the <see cref="DomClick"/> event.</summary>
		/// <param name="e">The data for the event.</param>
		protected virtual void OnDomClick(DomMouseEventArgs e)
		{
			var evnt = (EventHandler<DomMouseEventArgs>)Events[DomClickEvent];
			if (evnt != null) evnt(this, e);
		}
		#endregion

		#region public event GeckoDomEventHandler DomDoubleClick

		[Category("DOM Events")]
		public event EventHandler<DomMouseEventArgs> DomDoubleClick
		{
			add { Events.AddHandler(DomDoubleClickEvent, value); }
			remove { Events.RemoveHandler(DomDoubleClickEvent, value); }
		}
		
		/// <summary>Raises the <see cref="DomDoubleClick"/> event.</summary>
		/// <param name="e">The data for the event.</param>
		protected virtual void OnDomDoubleClick(DomMouseEventArgs e)
		{
			var evnt = (EventHandler<DomMouseEventArgs>)Events[DomDoubleClickEvent];
			if (evnt != null) evnt(this, e);
		}

		#endregion public event GeckoDomEventHandler DomDoubleClick

		#region public event GeckoDomEventHandler FullscreenChange

		[Category("DOM Events")]
		public event EventHandler<DomEventArgs> FullscreenChange
		{
			add { Events.AddHandler(FullscreenChangeEvent, value); }
			remove { Events.RemoveHandler(FullscreenChangeEvent, value); }
		}

		/// <summary>Raises the <see cref="FullscreenChange"/> event.</summary>
		/// <param name="e">The data for the event.</param>
		protected virtual void OnFullscreenChange(DomEventArgs e)
		{
			var evnt = (EventHandler<DomEventArgs>)Events[FullscreenChangeEvent];
			if (evnt != null) evnt(this, e);
		}

		#endregion public event GeckoDomEventHandler FullscreenChange

		#endregion

		#region event JavascriptErrorEventHandler JavascriptError

		internal class JSErrorHandler : jsdIErrorHook
		{
			GeckoWebBrowser m_browser;

			internal JSErrorHandler(GeckoWebBrowser browser)
			{
				m_browser = browser;
			}

			public bool OnError(nsAUTF8StringBase message, nsAUTF8StringBase fileName, uint line, uint pos, uint flags, uint errnum, jsdIValue exc)
			{
				if (m_browser.IsDisposed) return true;
				var eventArgs = new JavascriptErrorEventArgs(message.ToString(), fileName.ToString(), line, pos, flags, errnum);
				m_browser.OnJavascriptError(eventArgs);
				return true;
			}
		}

		public void EnableJavascriptDebugger()
		{
			if (m_javascriptDebuggingEnabled)
				return;

			//using (var a = new AutoJSContext(JSContext))
			{
				using (var jsd = Xpcom.GetService2<jsdIDebuggerService>(Contracts.DebuggerService))
				{
					jsd.Instance.SetErrorHookAttribute( new JSErrorHandler( this ) );
					using (var runtime = Xpcom.GetService2<nsIJSRuntimeService>(Contracts.RuntimeService))
					{
						jsd.Instance.ActivateDebugger( runtime.Instance.GetRuntimeAttribute() );
					}
				}
			}
			m_javascriptDebuggingEnabled = true;
		}

		public delegate void JavascriptErrorEventHandler(object sender, JavascriptErrorEventArgs e);

		private JavascriptErrorEventHandler _JavascriptError;

		public event JavascriptErrorEventHandler JavascriptError
		{
			add
			{
				EnableJavascriptDebugger();
				_JavascriptError += value;
			}
			remove { _JavascriptError -= value; }
		}

		protected virtual void OnJavascriptError(JavascriptErrorEventArgs e)
		{
			if (_JavascriptError != null)
				_JavascriptError(this, e);
		}

		#endregion

		#region event EventHandler<ConsoleMessageEventArgs> ConsoleMessage

		public sealed class ConsoleListener
			: nsIConsoleListener
		{
			GeckoWebBrowser m_browser;

			public ConsoleListener(GeckoWebBrowser browser)
			{
				m_browser = browser;
			}

			public void Observe(nsIConsoleMessage aMessage)
			{
				if (m_browser.IsDisposed) return;
				var e = new ConsoleMessageEventArgs(aMessage.GetMessageAttribute());
				m_browser.OnConsoleMessage(e);
			}
		}

		public void EnableConsoleMessageNotfication()
		{
			using (var consoleService = Xpcom.GetService2<nsIConsoleService>(Contracts.ConsoleService)) 
			{
				consoleService.Instance.RegisterListener(new ConsoleListener(this));
			}			
		}

		private EventHandler<ConsoleMessageEventArgs> _ConsoleMessage;

		public event EventHandler<ConsoleMessageEventArgs> ConsoleMessage
		{
			add
			{
				EnableConsoleMessageNotfication();
				_ConsoleMessage += value;
			}
			remove { _ConsoleMessage -= value; }
		}


		protected virtual void OnConsoleMessage(ConsoleMessageEventArgs e)
		{
			if (_ConsoleMessage != null)
				_ConsoleMessage(this, e);
		}

		#endregion

		#region public event EventHandler<GeckoNSSErrorEventArgs> NSSError

		/// <summary>
		/// Occurs when the control has updated progress information.
		/// </summary>
		[Category("Security")]
		public event EventHandler<GeckoNSSErrorEventArgs> NSSError
		{
			add { Events.AddHandler(NSSErrorEvent, value); }
			remove { Events.RemoveHandler(NSSErrorEvent, value); }
		}


		/// <summary>Raises the <see cref="NSSError"/> event.</summary>
		/// <param name="e">The data for the event.</param>
		protected virtual void OnNSSError(GeckoNSSErrorEventArgs e)
		{
			var evnt = (EventHandler<GeckoNSSErrorEventArgs>)Events[NSSErrorEvent];
			if (evnt != null) evnt(this, e);
		}

		#endregion
	}

	#region EventArgs Classes

	#region GeckoHistoryEventArgs
	/// <summary>Provides data for event.</summary>
	public class GeckoHistoryEventArgs
		: CancelEventArgs
	{
		/// <summary>
		/// Gets the URL of the history entry.
		/// </summary>
		public readonly Uri Url;

		/// <summary>Creates a new instance of a <see cref="GeckoHistoryEventArgs"/> object.</summary>
		/// <param name="url"></param>
		public GeckoHistoryEventArgs(Uri url)
		{
			Url = url;
		}
	}
	#endregion

	#region GeckoHistoryGotoIndexEventArgs

	/// <summary>Provides data for event.</summary>
	public class GeckoHistoryGotoIndexEventArgs
		: GeckoHistoryEventArgs
	{
		/// <summary>
		/// Gets the index in history of the document to be loaded.
		/// </summary>
		public readonly int Index;

		/// <summary>Creates a new instance of a <see cref="GeckoHistoryGotoIndexEventArgs"/> object.</summary>
		/// <param name="url"></param>
		/// <param name="index"></param>
		public GeckoHistoryGotoIndexEventArgs(Uri url, int index)
			: base(url)
		{
			Index = index;
		}
	}
	#endregion

	#region GeckoHistoryPurgeEventArgs
	/// <summary>Provides data for event.</summary>
	public class GeckoHistoryPurgeEventArgs
		: CancelEventArgs
	{
		/// <summary>
		/// Gets the number of entries to be purged from the history.
		/// </summary>
		public readonly int Count;

		/// <summary>Creates a new instance of a <see cref="GeckoHistoryPurgeEventArgs"/> object.</summary>
		/// <param name="count"></param>
		public GeckoHistoryPurgeEventArgs(int count)
		{
			Count = count;
		}

	}
	#endregion

	#region GeckoRequestProgressEventArgs
	/// <summary>Provides data for event.</summary>
	public class GeckoRequestProgressEventArgs
		: EventArgs
	{
		private nsIRequest _request;
		private GeckoResponse _reqWrapper;

		public readonly long CurrentProgress;
		public readonly long MaximumProgress;
		/// <summary>Creates a new instance of a <see cref="GeckoRequestProgressEventArgs"/> object.</summary>
		public GeckoRequestProgressEventArgs(long current, long max, nsIRequest req)
		{
			CurrentProgress = current;
			MaximumProgress = max;
			_request = req;
		}

		public GeckoResponse Request
		{
			get
			{
				return _reqWrapper ?? (_reqWrapper = new GeckoResponse(_request));
			}
		}
	}
	#endregion

	#region GeckoProgressEventArgs
	/// <summary>Provides data for  event.</summary>
	public class GeckoProgressEventArgs
		: EventArgs
	{
		public readonly long CurrentProgress;
		public readonly long MaximumProgress;
		/// <summary>Creates a new instance of a <see cref="GeckoProgressEventArgs"/> object.</summary>
		public GeckoProgressEventArgs(long current, long max)
		{
			CurrentProgress = current;
			MaximumProgress = max;
		}
	}
	#endregion

	#region GeckoNavigatedEventArgs
	/// <summary>Provides data for event.</summary>
	public class GeckoNavigatedEventArgs
		: EventArgs
	{
		// Wrapper is not often needed, so store only nsIRequest
		private nsIRequest _response;
		private GeckoResponse _wrapper;

		public readonly GeckoWindow DomWindow;
		public readonly Boolean DomWindowTopLevel;

		public readonly Uri Uri;

		public readonly Boolean IsSameDocument;
		public readonly Boolean IsErrorPage;

		/// <summary>Creates a new instance of a <see cref="GeckoNavigatedEventArgs"/> object.</summary>
		/// <param name="value"></param>
		/// <param name="response"></param>
		internal GeckoNavigatedEventArgs(Uri value, nsIRequest response, GeckoWindow domWind, bool _sameDocument, bool _errorPage)
		{
			Uri = value;
			_response = response;
			DomWindow = domWind;

			using (var topWindow = DomWindow.Top)
			{
				DomWindowTopLevel = ((domWind == null) ? true : DomWindow.DomWindow.Equals(topWindow.DomWindow));
			}

			IsSameDocument = _sameDocument;
			IsErrorPage = _errorPage;
		}

		public GeckoResponse Response
		{
			get { return _wrapper ?? ( _wrapper = new GeckoResponse( _response ) ); }
		}
	}
	#endregion

	#region GeckoRedirectingEventArgs
	/// <summary>Provides data for event.</summary>
	public class GeckoRedirectingEventArgs
		: CancelEventArgs
	{
		public readonly Uri Uri;
		public readonly GeckoWindow DomWindow;
		public readonly Boolean DomWindowTopLevel;

		/// <summary>Creates a new instance of a <see cref="GeckoRedirectingEventArgs"/> object.</summary>
		/// <param name="value"></param>
		public GeckoRedirectingEventArgs(Uri value, GeckoWindow domWind)
		{
			Uri = value;
			DomWindow = domWind;
			DomWindowTopLevel = ((domWind == null) ? true : DomWindow.DomWindow.Equals(DomWindow.Top.DomWindow));
		}
	}
	#endregion

	#region GeckoRetargetedEventArgs
	/// <summary>Provides data for event.</summary>
	public class GeckoRetargetedEventArgs
		: EventArgs
	{
		public readonly Uri Uri;
		public readonly GeckoWindow DomWindow;
		public readonly bool DomWindowTopLevel;
		public readonly Request Request;

		/// <summary>Creates a new instance of a <see cref="GeckoRetargetedEventArgs"/> object.</summary>
		/// <param name="uri"></param>
		public GeckoRetargetedEventArgs(Uri uri, GeckoWindow domWind, Request req)
		{
			Uri = uri;
			DomWindow = domWind;
			DomWindowTopLevel = domWind.IsTopWindow();
			Request = req;
		}
	}
	#endregion

	#region GeckoCreateWindowEventArgs

	/// <summary>Provides data for event.</summary>
	public class GeckoCreateWindowEventArgs
		: CancelEventArgs
	{
		private GeckoWebBrowser _webBrowser;

		public readonly GeckoWindowFlags Flags;
		public readonly String Uri;

		public int InitialWidth = (int)nsIAppShellServiceConsts.SIZE_TO_CONTENT;
		public int InitialHeight = (int)nsIAppShellServiceConsts.SIZE_TO_CONTENT;
		
		/// <summary>Creates a new instance of a <see cref="GeckoCreateWindowEventArgs"/> object.</summary>
		/// <param name="flags"></param>
		/// <param name="uri"></param>
		public GeckoCreateWindowEventArgs(GeckoWindowFlags flags, String uri)
			:base(false)
		{
			Flags = flags;
			Uri = uri;
		}

		/// <summary>
		/// Gets or sets the <see cref="GeckoWebBrowser"/> used in the new window.
		/// </summary>
		public GeckoWebBrowser WebBrowser
		{
			get { return _webBrowser; }
			set { _webBrowser = value; }
		}

	}
	#endregion

	#region GeckoCreateWindow2EventArgs

	// TODO Merged into GeckoCreateWindowEventArgs, remove GeckoCreateWindow2EventArgs in future
	/// <summary>Provides data for event.</summary>
	public class GeckoCreateWindow2EventArgs
		: GeckoCreateWindowEventArgs
	{
		/// <summary>Creates a new instance of a <see cref="GeckoCreateWindowEventArgs"/> object.</summary>
		/// <param name="flags"></param>
		/// <param name="uri"></param>
		public GeckoCreateWindow2EventArgs(GeckoWindowFlags flags, String uri)
			:base(flags, uri)
		{
		}
	}
	#endregion

	#region GeckoWindowSetBoundsEventArgs

	/// <summary>Provides data for event.</summary>
	public class GeckoWindowSetBoundsEventArgs
		: EventArgs
	{
		public readonly Rectangle Bounds;
		public readonly BoundsSpecified BoundsSpecified;

		/// <summary>Creates a new instance of a <see cref="GeckoWindowSetBoundsEventArgs"/> object.</summary>
		/// <param name="bounds"></param>
		/// <param name="specified"></param>
		public GeckoWindowSetBoundsEventArgs(Rectangle bounds, BoundsSpecified specified)
		{
			Bounds = bounds;
			BoundsSpecified = specified;
		}
	}
	#endregion

	#region GeckoContextMenuEventArgs
	/// <summary>Provides data for event.</summary>
	public class GeckoContextMenuEventArgs
		: EventArgs
	{
		/// <summary>
		/// Gets the location where the context menu will be displayed.
		/// </summary>
		public readonly Point Location;
		public readonly Uri BackgroundImageSrc;
		public readonly Uri ImageSrc;
		public readonly string AssociatedLink;

		private GeckoNode _targetNode;

		/// <summary>Creates a new instance of a <see cref="GeckoContextMenuEventArgs"/> object.</summary>
		public GeckoContextMenuEventArgs(Point location, ContextMenu contextMenu, string associatedLink, Uri backgroundImageSrc, Uri imageSrc, GeckoNode targetNode)
		{
			Location = location;
			_contextMenu = contextMenu;
			AssociatedLink = associatedLink;
			BackgroundImageSrc = backgroundImageSrc;
			ImageSrc = imageSrc;
			_targetNode = targetNode;
		}

		

		/// <summary>
		/// Gets or sets the context menu to be displayed.  Set this property to null to disable
		/// the context menu.
		/// </summary>
		public ContextMenu ContextMenu
		{
			get { return _contextMenu; }
			set { _contextMenu = value; }
		}
		ContextMenu _contextMenu;

		

		public GeckoNode TargetNode
		{
			get { return _targetNode; }
		}
		
	}
	#endregion

	#region GeckoObserveHttpModifyRequestEventArgs
	/// <summary>Provides data for event.</summary>
	public class GeckoObserveHttpModifyRequestEventArgs
		: CancelEventArgs {
		public readonly Uri Uri;
		public readonly Uri Referrer;
		public readonly string RequestMethod;
		public readonly Byte[] RequestBody;
		public readonly List<KeyValuePair<string, string>> RequestHeaders;
		public readonly HttpChannel Channel;
		public readonly bool? ReqBodyContainsHeaders;

		/// <summary>Creates a new instance of a <see cref="GeckoObserveHttpModifyRequestEventArgs"/> object.</summary>
		/// <param name="uri">Uri</param>
		/// <param name="refVal">Referrer</param>
		/// <param name="reqMethod">Request Method</param>
		/// <param name="reqBody">Request Body</param>
		/// <param name="reqHeaders">Request Headers</param>
		/// <param name="httpChan">Reference to Http Channel</param>
		/// <param name="bodyContainsHeaders">Does ReqBody contain the headers</param>
		public GeckoObserveHttpModifyRequestEventArgs(Uri uri, Uri refVal, String reqMethod, Byte[] reqBody, List<KeyValuePair<string, string>> reqHeaders, HttpChannel httpChan, bool? bodyContainsHeaders)
			: base(false) {
			Uri = uri;
			Referrer = refVal;
			RequestMethod = reqMethod;
			RequestBody = reqBody;
			RequestHeaders = reqHeaders;
			Channel = httpChan;
			ReqBodyContainsHeaders = bodyContainsHeaders;
		}
	}
	#endregion

	#endregion
}
