using System.Collections.Generic;

namespace Gecko
{
	/// <summary>
	/// Represents a collection of GeckoNode's
	/// </summary>
	internal class GeckoElementEnumerable : IEnumerable<GeckoHtmlElement>
	{
		private nsIDOMXPathResult xpathResult = null;

		internal GeckoElementEnumerable(nsIDOMXPathResult xpathResult)
		{
			this.xpathResult = xpathResult;
		}

		#region IEnumerable<GeckoNode> Members

		// TODO: This current implementation only also GetEnumerator to be called once!
		// refactor so that Enumerator is inplemented in seperate class and GetEnumerator can
		// be called multiple times.
		public IEnumerator<GeckoHtmlElement> GetEnumerator()
		{
			nsIDOMNode node;
			while ((node = xpathResult.IterateNext()) != null)
				if (node is nsIDOMHTMLElement)
					yield return GeckoHtmlElement.Create((nsIDOMHTMLElement)node);
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