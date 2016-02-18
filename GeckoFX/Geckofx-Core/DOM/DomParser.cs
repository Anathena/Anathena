using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Gecko.Interop;

namespace Gecko.DOM
{
	public sealed class DomParser
	{
		private ComPtr<nsIDOMParser> _domParser;

		public DomParser()
		{
			_domParser = Xpcom.CreateInstance2<nsIDOMParser>(Contracts.DomParser);
		}

		/// <summary>
		/// It is working!
		/// </summary>
		/// <param name="str"></param>
		/// <param name="contentType"></param>
		/// <returns></returns>
		public GeckoDomDocument ParseFromString(string str, string contentType = "text/html")
		{
			return _domParser.Instance.ParseFromString(str, contentType).Wrap(GeckoDomDocument.CreateDomDocumentWraper);
		}

		/// <summary>
		/// throws not implemented exception
		/// </summary>
		/// <param name="buffer"></param>
		/// <param name="count"></param>
		/// <param name="contentType"></param>
		/// <returns></returns>
		public GeckoDomDocument ParseFromBuffer(byte[] buffer, int count, string contentType = "text/html")
		{
			return _domParser.Instance.ParseFromBuffer(buffer, (uint)count, contentType).Wrap(GeckoDomDocument.CreateDomDocumentWraper);
		}

		/// <summary>
		/// throws not implemented exception
		/// </summary>
		/// <param name="buffer"></param>
		/// <param name="contentType"></param>
		/// <returns></returns>
		public GeckoDomDocument ParseFromBuffer(byte[] buffer, string contentType = "text/html")
		{
			return _domParser.Instance.ParseFromBuffer(buffer, (uint)buffer.Length, contentType)
				.Wrap(GeckoDomDocument.CreateDomDocumentWraper);
		}

		// not needed in .NET
		//public GeckoDomDocument ParseFromStream(ByteArrayInputStream buffer, string contentType)
		//{
		//    return _domParser.Instance.ParseFromStream()
		//}
	}
}
