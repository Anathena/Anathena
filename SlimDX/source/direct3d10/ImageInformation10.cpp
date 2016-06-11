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

#include <d3d10.h>
#include <vcclr.h>

#include "Direct3D10Exception.h"

#include "ImageInformation10.h"

using namespace System;

namespace SlimDX
{
	namespace Direct3D10
	{ 	
		ImageInformation::ImageInformation( const D3DX10_IMAGE_INFO& native )
		{
			m_Width = native.Width;
			m_Height = native.Height;
			m_Depth = native.Depth;
			m_ArraySize = native.ArraySize;
			m_MipLevels = native.MipLevels;
			m_MiscFlags = static_cast<ResourceOptionFlags>( native.MiscFlags );
			m_Format = static_cast<DXGI::Format>( native.Format );
			m_Dimension = static_cast<ResourceDimension>( native.ResourceDimension );
			m_FileFormat = static_cast<ImageFileFormat>( native.ImageFileFormat );
		}

		D3DX10_IMAGE_INFO ImageInformation::CreateNativeVersion()
		{
			D3DX10_IMAGE_INFO native;
			native.Width = m_Width;
			native.Height = m_Height;
			native.Depth = m_Depth;
			native.ArraySize = m_ArraySize;
			native.MipLevels = m_MipLevels;
			native.MiscFlags = static_cast<UINT>( m_MiscFlags );
			native.Format = static_cast<DXGI_FORMAT>( m_Format );
			native.ResourceDimension = static_cast<D3D10_RESOURCE_DIMENSION>( m_Dimension );
			native.ImageFileFormat = static_cast<D3DX10_IMAGE_FILE_FORMAT>( m_FileFormat );

			return native;
		}

		int ImageInformation::Width::get()
		{
			return m_Width;
		}

		void ImageInformation::Width::set( int value )
		{
			m_Width = value;
		}
		
		int ImageInformation::Height::get()
		{
			return m_Height;
		}

		void ImageInformation::Height::set( int value )
		{
			m_Height = value;
		}
		
		int ImageInformation::Depth::get()
		{
			return m_Depth;
		}

		void ImageInformation::Depth::set( int value )
		{
			m_Depth = value;
		}
		
		int ImageInformation::ArraySize::get()
		{
			return m_ArraySize;
		}

		void ImageInformation::ArraySize::set( int value )
		{
			m_ArraySize = value;
		}
		
		int ImageInformation::MipLevels::get()
		{
			return m_MipLevels;
		}

		void ImageInformation::MipLevels::set( int value )
		{
			m_MipLevels = value;
		}

		DXGI::Format ImageInformation::Format::get()
		{
			return m_Format;
		}

		void ImageInformation::Format::set( DXGI::Format value )
		{
			m_Format = value;
		}

		ResourceOptionFlags ImageInformation::OptionFlags::get()
		{
			return m_MiscFlags;
		}

		void ImageInformation::OptionFlags::set( ResourceOptionFlags value )
		{
			m_MiscFlags = value;
		}
		
		ResourceDimension ImageInformation::Dimension::get()
		{
			return m_Dimension;
		}

		void ImageInformation::Dimension::set( ResourceDimension value )
		{
			m_Dimension = value;
		}
		
		ImageFileFormat ImageInformation::FileFormat::get()
		{
			return m_FileFormat;
		}

		void ImageInformation::FileFormat::set( ImageFileFormat value )
		{
			m_FileFormat = value;
		}
		
		Nullable<ImageInformation> ImageInformation::FromFile( String^ fileName )
		{
			D3DX10_IMAGE_INFO info;
			pin_ptr<const wchar_t> pinnedName = PtrToStringChars( fileName );
			HRESULT hr = D3DX10GetImageInfoFromFile( pinnedName, 0, &info, 0 );
			if( RECORD_D3D10( hr ).IsFailure )
				return Nullable<ImageInformation>();
			
			return ImageInformation( info );
		}
		
		Nullable<ImageInformation> ImageInformation::FromMemory( array<Byte>^ memory )
		{
			D3DX10_IMAGE_INFO info;
			pin_ptr<unsigned char> pinnedMemory = &memory[0];
			HRESULT hr = D3DX10GetImageInfoFromMemory( pinnedMemory, memory->Length, 0, &info, 0 );
			if( RECORD_D3D10( hr ).IsFailure )
				return Nullable<ImageInformation>();
			
			return ImageInformation( info );
		}

		bool ImageInformation::operator == ( ImageInformation left, ImageInformation right )
		{
			return ImageInformation::Equals( left, right );
		}

		bool ImageInformation::operator != ( ImageInformation left, ImageInformation right )
		{
			return !ImageInformation::Equals( left, right );
		}

		int ImageInformation::GetHashCode()
		{
			return m_Width.GetHashCode() + m_Height.GetHashCode() + m_Depth.GetHashCode()
				+ m_ArraySize.GetHashCode() + m_MipLevels.GetHashCode() + m_MiscFlags.GetHashCode()
				+ m_Format.GetHashCode() + m_Dimension.GetHashCode() + m_FileFormat.GetHashCode();
		}

		bool ImageInformation::Equals( Object^ value )
		{
			if( value == nullptr )
				return false;

			if( value->GetType() != GetType() )
				return false;

			return Equals( safe_cast<ImageInformation>( value ) );
		}

		bool ImageInformation::Equals( ImageInformation value )
		{
			return (
				m_Width == value.m_Width && 
				m_Height == value.m_Height && 
				m_Depth == value.m_Depth && 
				m_ArraySize == value.m_ArraySize && 
				m_MipLevels == value.m_MipLevels && 
				m_MiscFlags == value.m_MiscFlags && 
				m_Format == value.m_Format &&
				m_Dimension == value.m_Dimension &&
				m_FileFormat == value.m_FileFormat
			);
		}

		bool ImageInformation::Equals( ImageInformation% value1, ImageInformation% value2 )
		{
			return (
				value1.m_Width == value2.m_Width && 
				value1.m_Height == value2.m_Height && 
				value1.m_Depth == value2.m_Depth && 
				value1.m_ArraySize == value2.m_ArraySize && 
				value1.m_MipLevels == value2.m_MipLevels && 
				value1.m_MiscFlags == value2.m_MiscFlags && 
				value1.m_Format == value2.m_Format &&
				value1.m_Dimension == value2.m_Dimension &&
				value1.m_FileFormat == value2.m_FileFormat
			);
		}
	}
}
