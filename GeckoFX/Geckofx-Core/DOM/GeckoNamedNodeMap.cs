using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Gecko.Collections;
using Gecko.Interop;

namespace Gecko.DOM
{
	public class GeckoNamedNodeMap
		:IGeckoArray<GeckoNode>, IEnumerable<GeckoNode>
	{
		private ComPtr<nsIDOMMozNamedAttrMap> _map;

		public GeckoNamedNodeMap( nsIDOMMozNamedAttrMap map )
		{
			// map should be not null
			this._map = new ComPtr<nsIDOMMozNamedAttrMap>( map );
		}


		/// <summary>
		/// Gets the number of items in the map.
		/// </summary>
		public int Length
		{
			get { return ( int ) _map.Instance.GetLengthAttribute(); }
		}

		public GeckoNode this[ int index ]
		{
			get
			{
				if ( index < 0 || index >= Length )
					throw new ArgumentOutOfRangeException( "index" );

				return _map.Instance.Item( ( uint ) index ).Wrap( GeckoNode.Create );
			}
		}

		public GeckoNode this[ string name ]
		{
			get { return nsString.Pass( _map.Instance.GetNamedItem, name ).Wrap( GeckoNode.Create ); }
		}

		public GeckoNode this[ string namespaceUri, string localName ]
		{
			get { return nsString.Pass( _map.Instance.GetNamedItemNS, namespaceUri, localName ).Wrap( GeckoNode.Create ); }
		}

		public GeckoNode RemoveNamedItem( string name )
		{
			return nsString.Pass( _map.Instance.RemoveNamedItem, name )
						   .Wrap( GeckoNode.Create );
		}

		#region IEnumerable<GeckoNode> Members

		public IEnumerator<GeckoNode> GetEnumerator()
		{
			int length = Length;
			for ( int i = 0; i < length; i++ )
			{
				yield return GeckoNode.Create( _map.Instance.Item( ( uint ) i ) );
			}
		}

		#endregion

		#region IEnumerable Members

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}

		#endregion
	}

}
