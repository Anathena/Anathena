using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Threading;

namespace Gecko.Interop
{
	sealed class COMGC : nsITimerCallback, IDisposable
	{
		private sealed class GCData
		{
			object obj;
			bool final;

			public GCData(object obj, bool final)
			{
				this.obj = obj;
				this.final = final;
			}

			public bool CanFree
			{
				get { return obj != null; }
			}

			public void Free()
			{
				var localObj = Interlocked.Exchange(ref this.obj, null);
				if (final)
					Marshal.FinalReleaseComObject(localObj);
				else
					Marshal.ReleaseComObject(localObj);
			}
		}

		
		private nsITimer _timer;
		private object _syncRoot = new object();
		private Queue<GCData> _queue = new Queue<GCData>();


		public COMGC()
		{
			_timer = Xpcom.CreateInstance<nsITimer>("@mozilla.org/timer;1");
			_timer.InitWithCallback(this, 5000, (uint)nsITimerConsts.TYPE_REPEATING_SLACK);
		}

		public void SetDelay(uint delay)
		{
			if (_timer == null)
				throw new ObjectDisposedException(this.GetType().Name);

			_timer.SetDelayAttribute(delay);
		}

		public void Free<T>(ref T obj)
			where T: class
		{
			Free(ref obj, false);
		}

		public void FinalFree<T>(ref T obj)
			where T : class
		{
			Free(ref obj, true);
		}

		private void Free<T>(ref T obj, bool finalize)
			where T : class
		{
			if (_timer != null)
			{

				var data = new GCData(Interlocked.Exchange(ref obj, null), finalize);
				if (data.CanFree)
				{
					Monitor.Enter(_syncRoot);
					try
					{
						_queue.Enqueue(data);
					}
					finally
					{
						Monitor.Exit(_syncRoot);
					}
				}
			}
		}

		public void Notify(nsITimer timer)
		{
			if (Monitor.TryEnter(_syncRoot))
			{
				try
				{
					for (int i = _queue.Count; i > 0; i--)
					{
						_queue.Dequeue().Free();						
					}
				}
				finally
				{
					Monitor.Exit(_syncRoot);
				}
			}
		}

		public void Dispose()
		{
			if (_timer != null)
			{
				_timer.Cancel();

				var localObj = Interlocked.Exchange(ref _timer, null);
				if (localObj != null)
					Marshal.ReleaseComObject(localObj);

				_timer = null;
			}
			GC.SuppressFinalize(this);
		}

	}
}
