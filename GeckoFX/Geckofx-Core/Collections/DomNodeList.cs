using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Gecko.Interop;

namespace Gecko.Collections
{
	/// <summary>
	/// Generic wrapper for nsIDOMNodeList
	/// </summary>
	/// <typeparam name="TWrapper"></typeparam>
	/// <typeparam name="TGeckoNode"></typeparam>
	internal sealed class DomNodeList<TWrapper, TGeckoNode>
		: IGeckoArray<TWrapper>
		where TWrapper : GeckoNode
		where TGeckoNode : class, nsIDOMNode
	{
		private nsIDOMNodeList _list;
		private Func<TGeckoNode, TWrapper> _translator;

		internal DomNodeList(nsIDOMNodeList list, Func<TGeckoNode, TWrapper> translator)
		{
			_list = list;
			_translator = translator;
		}

		public int Length
		{
			get { return (int)_list.GetLengthAttribute(); }
		}

		public TWrapper this[int index]
		{
			get
			{
				var item = _list.Item((uint)index);
				if (item is TGeckoNode)
				{
					return ((TGeckoNode)item).Wrap(_translator);
				}
				return null;
			}
		}


		public IEnumerator<TWrapper> GetEnumerator()
		{
			return new GeckoNodeEnumerator<TWrapper, TGeckoNode>(_list, _translator);
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}
	}


	internal sealed class DomHtmlCollection<TWrapper, TGeckoNode>
		: IDomHtmlCollection<TWrapper>
		where TWrapper : GeckoNode
		where TGeckoNode : class, nsIDOMNode
	{
		private nsIDOMHTMLCollection _collection;
		private Func<TGeckoNode, TWrapper> _translator;

		public DomHtmlCollection(nsIDOMHTMLCollection collection, Func<TGeckoNode, TWrapper> translator)
		{
			_collection = collection;
			_translator = translator;
		}

		public int Length
		{
			get { return (int)_collection.GetLengthAttribute(); }
		}

		public TWrapper this[int index]
		{
			get
			{
				var item = _collection.Item((uint)index);
				if (item is TGeckoNode)
				{
					return ((TGeckoNode)item).Wrap(_translator);
				}
				return null;
			}
		}

		public TWrapper this[string name]
		{
			get
			{
				var item = nsString.Pass(_collection.NamedItem, name);
				if (item is TGeckoNode)
				{
					return ((TGeckoNode)item).Wrap(_translator);
				}
				return null;
			}
		}


		public IEnumerator<TWrapper> GetEnumerator()
		{
			return new GeckoNodeEnumerator<TWrapper, TGeckoNode>(_collection, _translator);
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}
	}


	public interface IDomHtmlCollection<TGeckoNode>
		: IGeckoArray<TGeckoNode>
	{
		TGeckoNode this[string name] { get; }
	}

}
