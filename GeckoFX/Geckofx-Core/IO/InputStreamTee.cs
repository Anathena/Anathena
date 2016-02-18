namespace Gecko.IO
{
	public sealed class InputStreamTee
		:InputStream
	{
		private nsIInputStreamTee _inputStreamTee;

		internal InputStreamTee(nsIInputStreamTee inputStreamTee)
			:base(inputStreamTee)
		{
			_inputStreamTee = inputStreamTee;
		}


		public InputStream Source
		{
			get { return Create( _inputStreamTee.GetSourceAttribute() ); }
			set { _inputStreamTee.SetSourceAttribute( value._inputStream ); }
		}

		public OutputStream Sink
		{
			get { return OutputStream.Create( _inputStreamTee.GetSinkAttribute() ); }
			set { _inputStreamTee.SetSinkAttribute( value._outputStream ); }
		}

		public nsIEventTarget EventTarget
		{
			get { return _inputStreamTee.GetEventTargetAttribute(); }
			set { _inputStreamTee.SetEventTargetAttribute( value ); }
		}
	}
}