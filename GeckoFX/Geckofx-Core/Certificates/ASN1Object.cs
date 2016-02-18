namespace Gecko.Certificates
{
	public sealed class ASN1Object
	{
		internal nsIASN1Object _object;

		internal ASN1Object( nsIASN1Object o )
		{
			_object = o;
		}


		public string DisplayName
		{
			get { return nsString.Get( _object.GetDisplayNameAttribute ); }
			set { nsString.Set( _object.SetDisplayNameAttribute, value ); }
		}

		public string DisplayValue
		{
			get { return nsString.Get( _object.GetDisplayValueAttribute ); }
			set { nsString.Set( _object.SetDisplayValueAttribute, value ); }
		}

		public uint Tag
		{
			get { return _object.GetTagAttribute(); }
			set { _object.SetTagAttribute( value ); }
		}

		public uint Type
		{
			get { return _object.GetTypeAttribute(); }
			set { _object.SetTypeAttribute( value ); }
		}
	}
}