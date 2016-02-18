using System;
using Gecko.Interop;
using Gecko.IO;
using Gecko.IO.Native;

namespace Gecko.Net
{
	public class StreamListenerTee
		:IDisposable
	{
		internal ComPtr<nsIStreamListenerTee> _streamListenerTee;
		private RequestObserver _requestObserver=new RequestObserver();

		private byte[] _capturedData;
		private NativeOutputStream _nativeOutputStream = new NativeOutputStream( 1024 * 1024 );

		public StreamListenerTee()
		{
			_streamListenerTee = Xpcom.CreateInstance2<nsIStreamListenerTee>(Contracts.StreamListenerTee);
			_requestObserver.Stopped += OnStopped;
		}

		~StreamListenerTee()
		{
			Xpcom.DisposeObject( ref _streamListenerTee );
		}

		public void Dispose()
		{
			Xpcom.DisposeObject( ref _streamListenerTee );
			GC.SuppressFinalize( this );
		}

		/// <summary>
		/// Internal function
		/// frees unmanaged stream
		/// </summary>
		private void Close()
		{
			Xpcom.DisposeObject( ref _nativeOutputStream );
		}

		internal void IntInit(nsIStreamListener streamListener)
		{
			_streamListenerTee.Instance.Init(streamListener, _nativeOutputStream, _requestObserver);
		}

		private void OnStopped(object sender, EventArgs e)
		{
			var completed = Completed;
			// transfer data from unmanaged code
			_capturedData = _nativeOutputStream.ReadData();
			if (completed != null)
			{
				completed(this, EventArgs.Empty);
			}
			// free unmanaged buffer
			Close();
		}

		public byte[] GetCapturedData()
		{
			return _capturedData;
		}
		

		public event EventHandler Completed;
	}
}