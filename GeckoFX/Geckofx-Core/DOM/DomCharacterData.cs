using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gecko.DOM
{
	public class DomCharacterData
		:GeckoNode
	{
		private nsIDOMCharacterData _domCharacterData;

		internal DomCharacterData(nsIDOMCharacterData domCharacterData)
			:base(domCharacterData)
		{
			_domCharacterData = domCharacterData;
		}


		public string Data
		{
			get { return nsString.Get(_domCharacterData.GetDataAttribute); }
			set { nsString.Set(_domCharacterData.SetDataAttribute, value); }
		}

		public uint Length
		{
			get { return _domCharacterData.GetLengthAttribute(); }
		}


		public string SubstringData(uint offset, uint count)
		{
			return nsString.Get( _domCharacterData.SubstringData, offset, count );
			//using (nsAString retval = new nsAString())
			//{
			//    _domCharacterData.SubstringData(offset, count, retval);
			//    return retval.ToString();
			//}
		}

		public void AppendData(string arg)
		{
			nsString.Set(_domCharacterData.AppendData, arg);
		}

		public void InsertData(uint offset, string arg)
		{
			nsString.Set(_domCharacterData.InsertData, offset, arg);
			//using (nsAString str = new nsAString(arg))
			//{
			//    DomComment.InsertData(offset, str);
			//}
		}

		public void DeleteData(uint offset, uint count)
		{
			_domCharacterData.DeleteData(offset, count);
		}

		public void ReplaceData(uint offset, uint count, string arg)
		{
			nsString.Set(_domCharacterData.ReplaceData, offset, count, arg);
			//using (nsAString str = new nsAString(arg))
			//{
			//    DomComment.ReplaceData(offset, count, str);
			//}
		}

	}
}
