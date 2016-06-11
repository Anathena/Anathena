#include "stdafx.h"
/*
* Copyright (c) 2007-2012 SlimDX Group
* 
* Permission is hereby granted, free of charge, to any person obtaining a copy
* of this software and associated documentation files (the "Software"), to deal
* in the Software without restriction, including without limitation the rights
* to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
* copies of the Software, and to permit persons to whom the Software is
* furnished to do so, subject to the following conditions:
* 
* The above copyright notice and this permission notice shall be included in
* all copies or substantial portions of the Software.
* 
* THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
* IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
* FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
* AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
* LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
* OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
* THE SOFTWARE.
*/

#include <d3dx9.h>

#include "../SlimDXException.h"

#include "../direct3d9/Enums.h"
#include "../direct3d9/PaletteEntry.h"
#include "../direct3d9/CubeTexture.h"

#include "Color3.h"
#include "Color4.h"
#include "Matrix.h"
#include "Vector3.h"
#include "SHVector.h"

using namespace System;
using namespace System::Text;
using namespace System::Globalization;

namespace SlimDX
{
	SHVector::SHVector( int order )
	{
		coefficients = gcnew array<float>( order * order );
		this->order = order;
	}

	SHVector::SHVector( int order, array<float>^ coefficients )
	{
		this->coefficients = coefficients;
		this->order = order;

		if( order * order != coefficients->Length )
			throw gcnew InvalidOperationException( "The number of coefficients must be equal to the order squared." );
	}

	SHVector^ SHVector::Add( SHVector^ left, SHVector^ right )
	{
		if( left->Order != right->Order )
			throw gcnew InvalidOperationException( "The order of each vector must be identical." );

		array<float>^ output = gcnew array<float>( left->Coefficients->Length );
		for( int i = 0; i < left->Coefficients->Length; i++ )
			output[i] = left[i] + right[i];

		return gcnew SHVector( left->Order, output );
	}

	SHVector^ SHVector::Rotate( SHVector^ vector, Matrix rotationMatrix )
	{
		array<float>^ output = gcnew array<float>( vector->Coefficients->Length );

		pin_ptr<float> pinnedOutput = &output[0];
		pin_ptr<float> pinnedInput = &vector->Coefficients[0];

		D3DXSHRotate( pinnedOutput, vector->Order, reinterpret_cast<D3DXMATRIX*>( &rotationMatrix ), pinnedInput );

		return gcnew SHVector( vector->Order, output );
	}

	SHVector^ SHVector::RotateZ( SHVector^ vector, float angle )
	{
		array<float>^ output = gcnew array<float>( vector->Coefficients->Length );

		pin_ptr<float> pinnedOutput = &output[0];
		pin_ptr<float> pinnedInput = &vector->Coefficients[0];

		D3DXSHRotateZ( pinnedOutput, vector->Order, angle, pinnedInput );

		return gcnew SHVector( vector->Order, output );
	}

	SHVector^ SHVector::Scale( SHVector^ vector, float scale )
	{
		array<float>^ output = gcnew array<float>( vector->Coefficients->Length );

		pin_ptr<float> pinnedOutput = &output[0];
		pin_ptr<float> pinnedInput = &vector->Coefficients[0];

		D3DXSHScale( pinnedOutput, vector->Order, pinnedInput, scale );

		return gcnew SHVector( vector->Order, output );
	}

	float SHVector::Dot( SHVector^ left, SHVector^ right )
	{
		if( left->Order != right->Order )
			throw gcnew InvalidOperationException( "The order of each vector must be identical." );

		pin_ptr<float> pinnedLeft = &left->Coefficients[0];
		pin_ptr<float> pinnedRight = &right->Coefficients[0];

		return D3DXSHDot( left->Order, pinnedLeft, pinnedRight );
	}

	SHVector^ SHVector::EvaluateDirection( int order, Vector3 direction )
	{
		array<float>^ output = gcnew array<float>( order * order );
		pin_ptr<float> pinnedOutput = &output[0];

		D3DXSHEvalDirection( pinnedOutput, order, reinterpret_cast<D3DXVECTOR3*>( &direction ) );

		return gcnew SHVector( order, output );
	}

	Result SHVector::EvaluateConeLight( int order, Vector3 direction, float radius, Color3 color, [Out] SHVector^% red, [Out] SHVector^% green, [Out] SHVector^% blue )
	{
		array<float>^ redOutput = gcnew array<float>( order * order );
		array<float>^ greenOutput = gcnew array<float>( order * order );
		array<float>^ blueOutput = gcnew array<float>( order * order );

		pin_ptr<float> pinnedRed = &redOutput[0];
		pin_ptr<float> pinnedGreen = &greenOutput[0];
		pin_ptr<float> pinnedBlue = &blueOutput[0];

		HRESULT hr = D3DXSHEvalConeLight( order, reinterpret_cast<D3DXVECTOR3*>( &direction ), radius, color.Red, color.Green, color.Blue, pinnedRed, pinnedGreen, pinnedBlue );
		if( RECORD_SDX( hr ).IsFailure )
		{
			red = nullptr;
			green = nullptr;
			blue = nullptr;
			return Result::Last;
		}

		red = gcnew SHVector( order, redOutput );
		green = gcnew SHVector( order, greenOutput );
		blue = gcnew SHVector( order, blueOutput );

		return Result::Last;
	}

	Result SHVector::EvaluateDirectionalLight( int order, Vector3 direction, Color3 color, [Out] SHVector^% red, [Out] SHVector^% green, [Out] SHVector^% blue )
	{
		array<float>^ redOutput = gcnew array<float>( order * order );
		array<float>^ greenOutput = gcnew array<float>( order * order );
		array<float>^ blueOutput = gcnew array<float>( order * order );

		pin_ptr<float> pinnedRed = &redOutput[0];
		pin_ptr<float> pinnedGreen = &greenOutput[0];
		pin_ptr<float> pinnedBlue = &blueOutput[0];

		HRESULT hr = D3DXSHEvalDirectionalLight( order, reinterpret_cast<D3DXVECTOR3*>( &direction ), color.Red, color.Green, color.Blue, pinnedRed, pinnedGreen, pinnedBlue );
		if( RECORD_SDX( hr ).IsFailure )
		{
			red = nullptr;
			green = nullptr;
			blue = nullptr;
			return Result::Last;
		}

		red = gcnew SHVector( order, redOutput );
		green = gcnew SHVector( order, greenOutput );
		blue = gcnew SHVector( order, blueOutput );

		return Result::Last;
	}

	Result SHVector::EvaluateSphericalLight( int order, Vector3 direction, float radius, Color3 color, [Out] SHVector^% red, [Out] SHVector^% green, [Out] SHVector^% blue )
	{
		array<float>^ redOutput = gcnew array<float>( order * order );
		array<float>^ greenOutput = gcnew array<float>( order * order );
		array<float>^ blueOutput = gcnew array<float>( order * order );

		pin_ptr<float> pinnedRed = &redOutput[0];
		pin_ptr<float> pinnedGreen = &greenOutput[0];
		pin_ptr<float> pinnedBlue = &blueOutput[0];

		HRESULT hr = D3DXSHEvalSphericalLight( order, reinterpret_cast<D3DXVECTOR3*>( &direction ), radius, color.Red, color.Green, color.Blue, pinnedRed, pinnedGreen, pinnedBlue );
		if( RECORD_SDX( hr ).IsFailure )
		{
			red = nullptr;
			green = nullptr;
			blue = nullptr;
			return Result::Last;
		}

		red = gcnew SHVector( order, redOutput );
		green = gcnew SHVector( order, greenOutput );
		blue = gcnew SHVector( order, blueOutput );

		return Result::Last;
	}

	Result SHVector::EvaluateHemisphereLight( int order, Vector3 direction, Color4 top, Color4 bottom, [Out] SHVector^% red, [Out] SHVector^% green, [Out] SHVector^% blue )
	{
		array<float>^ redOutput = gcnew array<float>( order * order );
		array<float>^ greenOutput = gcnew array<float>( order * order );
		array<float>^ blueOutput = gcnew array<float>( order * order );

		pin_ptr<float> pinnedRed = &redOutput[0];
		pin_ptr<float> pinnedGreen = &greenOutput[0];
		pin_ptr<float> pinnedBlue = &blueOutput[0];

		HRESULT hr = D3DXSHEvalHemisphereLight( order, reinterpret_cast<D3DXVECTOR3*>( &direction ), top.ToUnmanaged(), 
			bottom.ToUnmanaged(), pinnedRed, pinnedGreen, pinnedBlue );
		if( RECORD_SDX( hr ).IsFailure )
		{
			red = nullptr;
			green = nullptr;
			blue = nullptr;
			return Result::Last;
		}

		red = gcnew SHVector( order, redOutput );
		green = gcnew SHVector( order, greenOutput );
		blue = gcnew SHVector( order, blueOutput );

		return Result::Last;
	}

	Result SHVector::ProjectCubeMap( int order, SlimDX::Direct3D9::CubeTexture^ cubeMap, [Out] SHVector^% red, [Out] SHVector^% green, [Out] SHVector^% blue )
	{
		array<float>^ redOutput = gcnew array<float>( order * order );
		array<float>^ greenOutput = gcnew array<float>( order * order );
		array<float>^ blueOutput = gcnew array<float>( order * order );

		pin_ptr<float> pinnedRed = &redOutput[0];
		pin_ptr<float> pinnedGreen = &greenOutput[0];
		pin_ptr<float> pinnedBlue = &blueOutput[0];

		HRESULT hr = D3DXSHProjectCubeMap( order, cubeMap->InternalPointer, pinnedRed, pinnedGreen, pinnedBlue );
		if( RECORD_SDX( hr ).IsFailure )
		{
			red = nullptr;
			green = nullptr;
			blue = nullptr;
			return Result::Last;
		}

		red = gcnew SHVector( order, redOutput );
		green = gcnew SHVector( order, greenOutput );
		blue = gcnew SHVector( order, blueOutput );

		return Result::Last;
	}

	SHVector^ SHVector::operator + ( SHVector^ left, SHVector^ right )
	{
		return Add( left, right );
	}

	SHVector^ SHVector::operator * ( SHVector^ value, float scale )
	{
		return Scale( value, scale );
	}
	
	SHVector^ SHVector::operator * ( float scale, SHVector^ vec )
	{
		return Scale( vec, scale );
	}

	bool SHVector::operator == ( SHVector^ left, SHVector^ right )
	{
		if( ReferenceEquals( left, nullptr ) )
			return ReferenceEquals( right, nullptr );

		return SHVector::Equals( left, right );
	}
	
	bool SHVector::operator != ( SHVector^ left, SHVector^ right )
	{
		return !( left == right );
	}
	
	String^ SHVector::ToString()
	{
		StringBuilder^ output = gcnew StringBuilder();
		output->AppendFormat( "Order:{0}", order );
		for each( float f in coefficients )
			output->AppendFormat( " {1}", f );

		return output->ToString();
	}

	int SHVector::GetHashCode()
	{
		int value = 0;
		for each( float f in coefficients )
			value += f.GetHashCode();

		return value;
	}

	bool SHVector::Equals( Object^ value )
	{
		if( value == nullptr )
			return false;

		if( value->GetType() != GetType() )
			return false;

		return Equals( safe_cast<SHVector^>( value ) );
	}

	bool SHVector::Equals( SHVector^ value )
	{
		if( value == nullptr )
			return false;

		if( ReferenceEquals( this, value ) )
			return true;

		if( value->Order != order )
			return false;

		for( int i = 0; i < coefficients->Length; i++ )
			if( value[i] != coefficients[i] )
				return false;

		return true;
	}

	bool SHVector::Equals( SHVector^ value1, SHVector^ value2 )
	{
		return value1->Equals( value2 );
	}
}