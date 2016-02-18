using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Diagnostics;
using Gecko.DOM;

namespace Gecko
{
	/// <summary>
	/// Represents a DOM Comment
	/// </summary>
	public class GeckoComment : DOM.DomCharacterData
	{
		nsIDOMComment DomComment;

		internal GeckoComment(nsIDOMComment comment)
			: base(comment)
		{
			DomComment = comment;
		}

		internal static GeckoComment CreateCommentWrapper(nsIDOMComment comment)
		{
			return (comment == null) ? null : new GeckoComment(comment);
		}
	}
}