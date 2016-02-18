using System;
using System.Collections.Generic;
using Gecko.Collections;

namespace Gecko
{
	/// <summary>
	/// Represents a collection of <see cref="GeckoHtmlElement"/> objects.
	/// </summary>
	public class GeckoElementCollection
		: IGeckoArray<GeckoHtmlElement>, IEnumerable<GeckoHtmlElement>
	{
		nsIDOMNodeList List;		

		internal GeckoElementCollection(nsIDOMNodeList list)
		{
			this.List = list;
		}		

		public virtual int Length
		{
			get { return (List == null) ? 0 : (int)List.GetLengthAttribute(); }
		}
		
		public virtual GeckoHtmlElement this[int index]
		{
			get
			{
				if (index < 0 || index >= Length)
					throw new ArgumentOutOfRangeException("index");

				return GeckoHtmlElement.Create((nsIDOMHTMLElement)List.Item((uint)index));
			}
		}
		
		#region IEnumerable<GeckoElement> Members

		public virtual IEnumerator<GeckoHtmlElement> GetEnumerator()
		{
			int length = Length;
			for (int i = 0; i < length; i++)
			{
				yield return GeckoHtmlElement.Create((nsIDOMHTMLElement)List.Item((uint)i));
			}
		}
		
		#endregion
		
		#region IEnumerable Members
		
		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
		{
			foreach (GeckoHtmlElement element in this)
				yield return element;
		}

		#endregion
	}
}