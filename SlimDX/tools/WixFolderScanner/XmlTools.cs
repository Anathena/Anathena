using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace WixFolderScanner
{
	public static class XmlTools
	{
		public static Stack<string> TagStack = new Stack<string>();

		public static int CurrentIndent
		{
			get { return TagStack.Count; }
		}

		public static string OpenTag(string tag)
		{
			return OpenTag(tag, string.Empty);
		}

		public static string OpenTag(string tag, string attribs)
		{
			Debug.Assert(!string.IsNullOrEmpty(tag));

			string xml = new string('\t', CurrentIndent);
			TagStack.Push(tag);

			if(attribs != string.Empty)
				attribs = " " + attribs;
			xml += string.Format("<{0}{1}>{2}", tag, attribs, Environment.NewLine);

			return xml;
		}

		public static string CloseTag()
		{
			string tag = TagStack.Pop();
			Debug.Assert(!string.IsNullOrEmpty(tag));

			string xml = new string('\t', CurrentIndent);
			xml += string.Format("</{0}>{1}", tag, Environment.NewLine);
			return xml;
		}

		public static string SelfClosingTag(string tag, string attribs)
		{
			Debug.Assert(!string.IsNullOrEmpty(tag));

			string xml = new string('\t', CurrentIndent + 1);
			if(attribs != string.Empty)
				attribs = " " + attribs;
			xml += string.Format("<{0}{1} />{2}", tag, attribs, Environment.NewLine);

			return xml;
		}

		public static string WriteValue<T>(string name, T value)
		{
			string valueString = value != null ? value.ToString() : string.Empty;
			string text = string.Format("<{0}>{1}</{0}>{2}", name, valueString, Environment.NewLine);
			string indentString = new string('\t', CurrentIndent);
			return indentString + text;
		}
	}
}
