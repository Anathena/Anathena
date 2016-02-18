using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Gecko.Interop;

namespace Gecko.DOM.Svg
{
	public class DomSvgLength
	{
		private ComPtr<nsIDOMSVGLength> _domSvgLength;

		private DomSvgLength(nsIDOMSVGLength domSvgLength)
		{
			_domSvgLength = new ComPtr<nsIDOMSVGLength>( domSvgLength );
		}

		public static DomSvgLength Create( nsIDOMSVGLength domSvgLength )
		{
			return new DomSvgLength( domSvgLength );
		}

		public SvgLengthType UnitType
		{
			get { return ( SvgLengthType ) _domSvgLength.Instance.GetUnitTypeAttribute(); }
		}

		public float Value
		{
			get { return _domSvgLength.Instance.GetValueAttribute(); }
			set { _domSvgLength.Instance.SetValueAttribute( value ); }
		}

		/// <summary>
		/// raises DOMException on setting
		/// </summary>
		public float ValueInSpecifiedUnits
		{
			get { return _domSvgLength.Instance.GetValueInSpecifiedUnitsAttribute(); }
			set { _domSvgLength.Instance.SetValueInSpecifiedUnitsAttribute( value ); }
		}

		/// <summary>
		/// raises DOMException on setting
		/// </summary
		public string ValueAsString
		{
			get { return nsString.Get( _domSvgLength.Instance.GetValueAsStringAttribute ); }
			set { nsString.Set( _domSvgLength.Instance.SetValueAsStringAttribute, value ); }
		}

		public void NewValueSpecifiedUnits( SvgLengthType unitType, float valueInSpecifiedUnits )
		{
			_domSvgLength.Instance.NewValueSpecifiedUnits( ( ushort ) unitType, valueInSpecifiedUnits );
		}

		public void ConvertToSpecifiedUnits( SvgLengthType unitType )
		{
			_domSvgLength.Instance.ConvertToSpecifiedUnits( ( ushort ) unitType );
		}
	}

	public enum SvgLengthType
	{
		Unknown = (ushort)nsIDOMSVGLengthConsts.SVG_LENGTHTYPE_UNKNOWN,
		Number = (ushort)nsIDOMSVGLengthConsts.SVG_LENGTHTYPE_NUMBER,
		Percentage = (ushort)nsIDOMSVGLengthConsts.SVG_LENGTHTYPE_PERCENTAGE,
		Ems = (ushort)nsIDOMSVGLengthConsts.SVG_LENGTHTYPE_EMS,
		Exs = (ushort)nsIDOMSVGLengthConsts.SVG_LENGTHTYPE_EXS,
		Px = (ushort)nsIDOMSVGLengthConsts.SVG_LENGTHTYPE_PX,
		Cm = (ushort)nsIDOMSVGLengthConsts.SVG_LENGTHTYPE_CM,
		Mm = (ushort)nsIDOMSVGLengthConsts.SVG_LENGTHTYPE_MM,
		In = (ushort)nsIDOMSVGLengthConsts.SVG_LENGTHTYPE_IN,
		Pt = (ushort)nsIDOMSVGLengthConsts.SVG_LENGTHTYPE_PT,
		Pc = (ushort)nsIDOMSVGLengthConsts.SVG_LENGTHTYPE_PC,
	}
}
