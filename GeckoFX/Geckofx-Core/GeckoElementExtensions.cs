using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gecko
{
	public static class GeckoElementExtensions
	{
		/// <summary>
		/// Gets the value of the data-xxx attributes
		/// </summary>
		/// <param name="dataAttributeName">Microdata label in data-label attribute</param>
		/// <returns></returns>
		public static string GetData(this GeckoHtmlElement node, string dataAttributeName)
		{
			if (string.IsNullOrEmpty(dataAttributeName))
				throw new ArgumentException("attributeName");

			return node.GetAttribute( string.Concat( "data-", dataAttributeName ) );
		}


		/// <summary>
		/// Performs recursive search in all <paramref name="element"/> ChildNodes elements
		/// Returns first match
		/// </summary>
		/// <param name="element"></param>
		/// <param name="predicate"></param>
		/// <returns></returns>
		public static T FindFirstChildInTree<T>(this GeckoNode element, Predicate<T> predicate)
			where T:GeckoNode
		{
			for (int i = 0; i < element.ChildNodes.Length; i++)
			{
				var node = element.ChildNodes[i];
				if ( !( node is T ) )
				{
					continue;
				}
				var childElement = (T)node;
				if (predicate(childElement))
				{
					return childElement;
				}
				if (childElement.HasChildNodes)
				{
					var ret = FindFirstChildInTree(childElement, predicate);
					if (ret != null)
					{
						return ret;
					}
				}
			}
			return null;
		}
	}
}
