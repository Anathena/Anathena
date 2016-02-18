using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Gecko.Interop;

namespace Gecko.DOM
{
	public class XPathResult
	{
		private ComPtr<nsIDOMXPathResult> xpathResult = null;

		internal XPathResult( nsIDOMXPathResult xpathResult )
		{
			this.xpathResult = new ComPtr<nsIDOMXPathResult>( xpathResult );
		}

		public XPathResultType GetResultType()
		{
			return (XPathResultType) xpathResult.Instance.GetResultTypeAttribute();
		}

		public double GetNumberValue()
		{
			return xpathResult.Instance.GetNumberValueAttribute();
		}

		public string GetStringValue()
		{
			return nsString.Get( xpathResult.Instance.GetStringValueAttribute );
		}

		public bool GetBooleanValue()
		{
			return xpathResult.Instance.GetBooleanValueAttribute();
		}

		public GeckoNode GetSingleNodeValue()
		{
			return xpathResult.Instance.GetSingleNodeValueAttribute().Wrap( GeckoNode.Create );
		}

		public IEnumerable<GeckoNode> GetNodes()
		{
			return new GeckoNodeEnumerable( xpathResult.Instance );
		}

	}

	public enum XPathResultType
		: ushort
	{
		Any = nsIDOMXPathResultConsts.ANY_TYPE,
		Number = nsIDOMXPathResultConsts.NUMBER_TYPE,
		String = nsIDOMXPathResultConsts.STRING_TYPE,
		Boolean = nsIDOMXPathResultConsts.BOOLEAN_TYPE,
		UnorderedNodeIterator = nsIDOMXPathResultConsts.UNORDERED_NODE_ITERATOR_TYPE,
		OrderedNodeIterator = nsIDOMXPathResultConsts.ORDERED_NODE_ITERATOR_TYPE,
		UnorderedNodeSnapshot = nsIDOMXPathResultConsts.UNORDERED_NODE_SNAPSHOT_TYPE,
		OrderedNodeSnapshot = nsIDOMXPathResultConsts.ORDERED_NODE_SNAPSHOT_TYPE,
		AnyUnorderedNode = nsIDOMXPathResultConsts.ANY_UNORDERED_NODE_TYPE,
		FirstOrderedNode = nsIDOMXPathResultConsts.FIRST_ORDERED_NODE_TYPE,
	}
}
