using System;
using Gecko.Events;
using Gecko.Interop;

namespace Gecko
{
	/// <summary>
	/// nsIWebProgressListener2 callback listener
	/// is needed to share code between 2 different GUI implementations of GeckoWebBrowser
	/// </summary>
	public sealed class WebProgressListener
		: nsIWebProgressListener, nsIWebProgressListener2,
		nsISupportsWeakReference
	{
		private bool _isListening = true;

		//callbacks
		private Action<nsIWebProgress, nsIRequest, uint, int> _onStateChangeCallback;
		//private Action<nsIWebProgress, nsIRequest, int, int, int, int> _onProgressChangeCallback;

		private Action<string> _onStatusChangeCallback;
		private Action<Events.GeckoNavigatingEventArgs> _onNavigatingCallback;

		public WebProgressListener()
		{
			
		}


		public bool IsListening
		{
			get { return _isListening; }
			set { _isListening = value; }
		}

		internal Action<nsIWebProgress, nsIRequest, uint, int> OnStateChangeCallback
		{
			get { return _onStateChangeCallback; }
			set { _onStateChangeCallback = value; }
		}

		internal Action<GeckoNavigatingEventArgs> OnNavigatingCallback
		{
			get { return _onNavigatingCallback; }
			set { _onNavigatingCallback = value; }
		}

		public Action<string> OnStatusChangeCallback
		{
			get { return _onStatusChangeCallback; }
			set { _onStatusChangeCallback = value; }
		}

		#region OnStateChange
		void nsIWebProgressListener.OnStateChange( nsIWebProgress aWebProgress, nsIRequest aRequest, uint aStateFlags, int aStatus )
		{
			OnStateChange(aWebProgress, aRequest, aStateFlags, aStatus);
		}

		void nsIWebProgressListener2.OnStateChange(nsIWebProgress aWebProgress, nsIRequest aRequest, uint aStateFlags, int aStatus)
		{
			OnStateChange( aWebProgress, aRequest, aStateFlags, aStatus );
		}

		private void OnStateChange( nsIWebProgress aWebProgress, nsIRequest aRequest, uint aStateFlags, int aStatus )
		{
			if ( !_isListening ) return;
			var evnt = _onStateChangeCallback;
			if ( evnt != null ) evnt( aWebProgress, aRequest, aStateFlags, aStatus );
		}
		#endregion

		#region OnProgressChange
		void nsIWebProgressListener.OnProgressChange(
			nsIWebProgress aWebProgress, nsIRequest aRequest, int aCurSelfProgress, int aMaxSelfProgress, int aCurTotalProgress,
			int aMaxTotalProgress )
		{
			OnProgressChange(aWebProgress, aRequest, aCurSelfProgress, aMaxSelfProgress, aCurTotalProgress, aMaxTotalProgress);
		}

		void nsIWebProgressListener2.OnProgressChange(
			nsIWebProgress aWebProgress, nsIRequest aRequest, int aCurSelfProgress, int aMaxSelfProgress, int aCurTotalProgress,
			int aMaxTotalProgress)
		{
			OnProgressChange( aWebProgress,aRequest,aCurSelfProgress,aMaxSelfProgress,aCurTotalProgress,aMaxTotalProgress );
		}

		private void OnProgressChange(
			nsIWebProgress aWebProgress, nsIRequest aRequest, int aCurSelfProgress, int aMaxSelfProgress, int aCurTotalProgress,
			int aMaxTotalProgress )
		{
			if (!_isListening) return;
		//	var evnt = _onProgressChangeCallback;
		//	if ( evnt != null )
		//		evnt( aWebProgress, aRequest, aCurSelfProgress, aMaxSelfProgress, aCurTotalProgress, aMaxTotalProgress );
		}
		#endregion

		#region OnLocationChange
		void nsIWebProgressListener.OnLocationChange( nsIWebProgress aWebProgress, nsIRequest aRequest, nsIURI aLocation, uint aFlags )
		{
			OnLocationChange( aWebProgress, aRequest, aLocation, aFlags );
		}

		void nsIWebProgressListener2.OnLocationChange(nsIWebProgress aWebProgress, nsIRequest aRequest, nsIURI aLocation, uint aFlags)
		{
			OnLocationChange(aWebProgress, aRequest, aLocation, aFlags);
		}

		private void OnLocationChange( nsIWebProgress aWebProgress, nsIRequest aRequest, nsIURI aLocation, uint aFlags )
		{
			if (!_isListening) return;
		}
		#endregion

		#region OnStatusChange
		void nsIWebProgressListener.OnStatusChange( nsIWebProgress aWebProgress, nsIRequest aRequest, int aStatus, string aMessage )
		{
			OnStatusChange( aWebProgress, aRequest, aStatus, aMessage );
		}

		void nsIWebProgressListener2.OnStatusChange(nsIWebProgress aWebProgress, nsIRequest aRequest, int aStatus, string aMessage)
		{
			OnStatusChange(aWebProgress, aRequest, aStatus, aMessage);
		}

		private void OnStatusChange( nsIWebProgress aWebProgress, nsIRequest aRequest, int aStatus, string aMessage )
		{
			if (!_isListening) return;
			var evnt = _onStatusChangeCallback;
			if ( evnt != null ) evnt( aMessage );
		}
		#endregion

		#region OnSecurityChange
		void nsIWebProgressListener.OnSecurityChange( nsIWebProgress aWebProgress, nsIRequest aRequest, uint aState )
		{
			OnSecurityChange( aWebProgress, aRequest, aState );
		}

		void nsIWebProgressListener2.OnSecurityChange(nsIWebProgress aWebProgress, nsIRequest aRequest, uint aState)
		{
			OnSecurityChange(aWebProgress, aRequest, aState);
		}

		private void OnSecurityChange( nsIWebProgress aWebProgress, nsIRequest aRequest, uint aState )
		{
			if (!_isListening) return;

		}
		#endregion


		void nsIWebProgressListener2.OnProgressChange64(
			nsIWebProgress aWebProgress, nsIRequest aRequest, long aCurSelfProgress, long aMaxSelfProgress, long aCurTotalProgress,
			long aMaxTotalProgress)
		{
			
		}



		bool nsIWebProgressListener2.OnRefreshAttempted(nsIWebProgress aWebProgress, nsIURI aRefreshURI, int aMillis, bool aSameURI)
		{
			if ( !_isListening ) return true;

			var evnt = _onNavigatingCallback;
			if ( evnt == null ) return true;

			Uri destUri = new Uri(nsString.Get(aRefreshURI.GetSpecAttribute));
			bool cancel = false;
			using (var domWindow = aWebProgress.GetDOMWindowAttribute().Wrap(x=>new GeckoWindow(x)))
			{
			    GeckoNavigatingEventArgs ea = new GeckoNavigatingEventArgs(destUri, domWindow);
				evnt( ea );
			    cancel = ea.Cancel;
			}
			return !cancel;
		}

		public nsIWeakReference GetWeakReference()
		{
			return new nsWeakReference(this);
		}
	}
}