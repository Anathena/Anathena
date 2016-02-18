using System;
using System.Collections;
using System.Collections.Generic;
using Gecko.Interop;

namespace Gecko.Net
{
	public sealed class DnsRecord
	{
		private ComPtr<nsIDNSRecord> _record;

		internal DnsRecord( nsIDNSRecord record )
		{
			_record = new ComPtr<nsIDNSRecord>( record );
		}

		public string CanonicalName
		{
			get { return nsString.Get( _record.Instance.GetCanonicalNameAttribute ); }
		}

		public string[] Records
		{
			get
			{
				List<string> ret = new List<string>();
				while ( _record.Instance.HasMore() )
				{
					var curRecord = nsString.Get( _record.Instance.GetNextAddrAsString );
					ret.Add( curRecord );
				}
				_record.Instance.Rewind();
				return ret.ToArray();
			}
		}
	}

}