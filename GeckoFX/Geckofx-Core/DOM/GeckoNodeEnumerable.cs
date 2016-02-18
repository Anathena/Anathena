using System.Collections.Generic;

namespace Gecko
{
	/// <summary>
	/// Represents a collection of GeckoNode's
	/// </summary>
	internal class GeckoNodeEnumerable : IEnumerable<GeckoNode>
	{
		private nsIDOMXPathResult xpathResult = null;

		internal GeckoNodeEnumerable(nsIDOMXPathResult xpathResult)
		{
			this.xpathResult = xpathResult;
		}

		#region IEnumerable<GeckoNode> Members

		public IEnumerator<GeckoNode> GetEnumerator()
		{
			nsIDOMNode node;
			while ((node = xpathResult.IterateNext()) != null)
				yield return GeckoNode.Create(node);
		}

		#endregion

		#region IEnumerable Members

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
		{
			nsIDOMNode node;
			while ((node = xpathResult.IterateNext()) != null)
				yield return GeckoNode.Create(node);
		}

		#endregion
	}
}