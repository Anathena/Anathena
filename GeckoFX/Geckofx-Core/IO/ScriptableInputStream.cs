using System.Text;

namespace Gecko.IO
{
	public sealed class ScriptableInputStream
	{
		private nsIScriptableInputStream _scriptableInputStream;

		public ScriptableInputStream()
		{
			_scriptableInputStream = Xpcom.CreateInstance<nsIScriptableInputStream>(Contracts.ScriptableInputStream);
		}

		public ScriptableInputStream(InputStream stream)
			:this()
		{
			Init( stream );
		}

		public int Available
		{
			get { return (int) _scriptableInputStream.Available(); }
		}

		public void Close()
		{
			_scriptableInputStream.Close();
		}

		public void Init(InputStream stream)
		{
			_scriptableInputStream.Init( stream._inputStream );
		}

		public string Read(int count)
		{
			return _scriptableInputStream.Read( (uint)count );
		}

		public string ReadBytes(int count)
		{
			return nsString.Get( _scriptableInputStream.ReadBytes, ( uint ) count );
		}
		
		

	}
}