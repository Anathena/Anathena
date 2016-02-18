namespace Gecko
{
	/// <summary>
	/// base nsISupportsPrimitive container
	/// </summary>
	public class nsSupportsPrimitive
	{
		private nsISupportsPrimitive _primitive;

		internal nsSupportsPrimitive(nsISupportsPrimitive primitive)
		{
			_primitive = primitive;
		}

		public ushort Type
		{
			get { return _primitive.GetTypeAttribute(); }
		}
	}
	/// <summary>
	/// nsISupportsCString container (may be it can be merged with another strings?)
	/// </summary>
	public sealed class nsCString
		: nsSupportsPrimitive
	{
		private nsISupportsCString _nsISupportsCString;

		public nsCString(nsISupportsCString nsISupportsCString)
			:base(nsISupportsCString)
		{
			_nsISupportsCString = nsISupportsCString;
		}

		public string Data
		{
			get { return nsString.Get(_nsISupportsCString.GetDataAttribute); }
			set { nsString.Set(_nsISupportsCString.SetDataAttribute, value); }
		}

		public override string ToString()
		{
			return Data;
		}
	}


}