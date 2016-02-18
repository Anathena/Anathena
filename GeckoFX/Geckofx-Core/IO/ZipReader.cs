using System;
using System.Collections.Generic;
using System.Threading;
using Gecko.Collections;
using Gecko.Interop;

namespace Gecko.IO
{
	public sealed class ZipReader
		:IDisposable
	{
		private ComPtr<nsIZipReader> _zipReader;

		public ZipReader()
		{
			_zipReader = Xpcom.CreateInstance2<nsIZipReader>(Contracts.ZipReader);
		}

		~ZipReader()
		{
			Close();
		}

		public void Dispose()
		{
			Close();
			GC.SuppressFinalize( this );
		}

		public void Open(string fileName)
		{
			_zipReader.Instance.Open(( nsIFile ) Xpcom.NewNativeLocalFile(fileName));
		}

		public IEnumerable<string> FindEntries(nsAUTF8String pattern)
		{
			return new SimpleGeckoEnumerableCollection<string>(
				() => new Utf8StringEnumerator(_zipReader.Instance.FindEntries(pattern)));
		}

		public void Close()
		{
			Xpcom.DisposeObject( ref _zipReader );
		}

		public bool HasEntry(string entry)
		{
			return nsString.Pass<bool>(_zipReader.Instance.HasEntry, entry);
		}

		public bool Test(nsAUTF8String entry)
		{
			bool ret;
			try
			{
				_zipReader.Instance.Test(entry);
				ret = true;
			}
			catch ( Exception )
			{
				ret = false;
			}
			return ret;
		}
		
	}
}