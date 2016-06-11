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

#include "ImageLoadInformation.h"

namespace SlimDX
{
	namespace Direct3D10
	{ 	
		ImageLoadInformation::ImageLoadInformation( const D3DX10_IMAGE_LOAD_INFO& native )
		{
			m_Width = native.Width;
			m_Height = native.Height;
			m_Depth = native.Depth;
			m_FirstMipLevel = native.FirstMipLevel;
			m_MipLevels = native.MipLevels;
			m_Usage = static_cast<ResourceUsage>( native.Usage );
			m_BindFlags = static_cast<Direct3D10::BindFlags>( native.BindFlags );
			m_CPUAccessFlags = static_cast<Direct3D10::CpuAccessFlags>( native.CpuAccessFlags );
			m_MiscFlags = static_cast<ResourceOptionFlags>( native.MiscFlags );
			m_Format = static_cast<DXGI::Format>( native.Format );
			m_Filter = static_cast<Direct3D10::FilterFlags>( native.Filter );
			m_MipFilter = static_cast<Direct3D10::FilterFlags>( native.MipFilter );
		}

		D3DX10_IMAGE_LOAD_INFO ImageLoadInformation::CreateNativeVersion()
		{
			D3DX10_IMAGE_LOAD_INFO native;
			native.Width = m_Width;
			native.Height = m_Height;
			native.Depth = m_Depth;
			native.FirstMipLevel = m_FirstMipLevel;
			native.MipLevels = m_MipLevels;
			native.Usage = static_cast<D3D10_USAGE>( m_Usage );
			native.BindFlags = static_cast<UINT>( m_BindFlags );
			native.CpuAccessFlags = static_cast<UINT>( m_CPUAccessFlags );
			native.MiscFlags = static_cast<UINT>( m_MiscFlags );
			native.Format = static_cast<DXGI_FORMAT>( m_Format );
			native.Filter = static_cast<D3DX10_FILTER_FLAG>( m_Filter );
			native.MipFilter = static_cast<D3DX10_FILTER_FLAG>( m_MipFilter );

			return native;
		}

		ImageLoadInformation ImageLoadInformation::FromDefaults()
		{
			ImageLoadInformation ret;
			ret.m_Width = D3DX10_DEFAULT;
			ret.m_Height = D3DX10_DEFAULT;
			ret.m_Depth = D3DX10_DEFAULT;
			ret.m_FirstMipLevel = D3DX10_DEFAULT;
			ret.m_MipLevels = D3DX10_DEFAULT;
			ret.m_Usage = static_cast<ResourceUsage>( D3DX10_DEFAULT );
			ret.m_BindFlags = static_cast<Direct3D10::BindFlags>( D3DX10_DEFAULT );
			ret.m_CPUAccessFlags = static_cast<Direct3D10::CpuAccessFlags>( D3DX10_DEFAULT );
			ret.m_MiscFlags = static_cast<ResourceOptionFlags>( D3DX10_DEFAULT );
			ret.m_Format = static_cast<DXGI::Format>( D3DX10_DEFAULT );
			ret.m_Filter = static_cast<Direct3D10::FilterFlags>( D3DX10_DEFAULT );
			ret.m_MipFilter = static_cast<Direct3D10::FilterFlags>( D3DX10_DEFAULT );
			return ret;
		}
	
		int ImageLoadInformation::FileDefaultValue::get()
		{
			return D3DX10_DEFAULT;
		}

		int ImageLoadInformation::Width::get()
		{
			return m_Width;
		}

		void ImageLoadInformation::Width::set( int value )
		{
			m_Width = value;
		}

		int ImageLoadInformation::Height::get()
		{
			return m_Height;
		}

		void ImageLoadInformation::Height::set( int value )
		{
			m_Height = value;
		}

		int ImageLoadInformation::Depth::get()
		{
			return m_Depth;
		}

		void ImageLoadInformation::Depth::set( int value )
		{
			m_Depth = value;
		}

		int ImageLoadInformation::FirstMipLevel::get()
		{
			return m_FirstMipLevel;
		}

		void ImageLoadInformation::FirstMipLevel::set( int value )
		{
			m_FirstMipLevel = value;
		}

		int ImageLoadInformation::MipLevels::get()
		{
			return m_MipLevels;
		}

		void ImageLoadInformation::MipLevels::set( int value )
		{
			m_MipLevels = value;
		}
		
		ResourceUsage ImageLoadInformation::Usage::get()
		{
			return m_Usage;
		}

		void ImageLoadInformation::Usage::set( ResourceUsage value )
		{
			m_Usage = value;
		}
		
		Direct3D10::BindFlags ImageLoadInformation::BindFlags::get()
		{
			return m_BindFlags;
		}

		void ImageLoadInformation::BindFlags::set( Direct3D10::BindFlags value )
		{
			m_BindFlags = value;
		}
		
		Direct3D10::CpuAccessFlags ImageLoadInformation::CpuAccessFlags::get()
		{
			return m_CPUAccessFlags;
		}

		void ImageLoadInformation::CpuAccessFlags::set( Direct3D10::CpuAccessFlags value )
		{
			m_CPUAccessFlags = value;
		}

		ResourceOptionFlags ImageLoadInformation::OptionFlags::get()
		{
			return m_MiscFlags;
		}

		void ImageLoadInformation::OptionFlags::set( ResourceOptionFlags value )
		{
			m_MiscFlags = value;
		}
		
		DXGI::Format ImageLoadInformation::Format::get()
		{
			return m_Format;
		}

		void ImageLoadInformation::Format::set( DXGI::Format value )
		{
			m_Format = value;
		}
		
		Direct3D10::FilterFlags ImageLoadInformation::FilterFlags::get()
		{
			return m_Filter;
		}

		void ImageLoadInformation::FilterFlags::set( Direct3D10::FilterFlags value )
		{
			m_Filter = value;
		}

		Direct3D10::FilterFlags ImageLoadInformation::MipFilterFlags::get()
		{
			return m_MipFilter;
		}

		void ImageLoadInformation::MipFilterFlags::set( Direct3D10::FilterFlags value )
		{
			m_MipFilter = value;
		}

		bool ImageLoadInformation::operator == ( ImageLoadInformation left, ImageLoadInformation right )
		{
			return ImageLoadInformation::Equals( left, right );
		}

		bool ImageLoadInformation::operator != ( ImageLoadInformation left, ImageLoadInformation right )
		{
			return !ImageLoadInformation::Equals( left, right );
		}

		int ImageLoadInformation::GetHashCode()
		{
			return m_Width.GetHashCode() + m_Height.GetHashCode() + m_Depth.GetHashCode()
				+ m_FirstMipLevel.GetHashCode() + m_MipLevels.GetHashCode()
				+ m_Usage.GetHashCode() + m_BindFlags.GetHashCode() + m_CPUAccessFlags.GetHashCode() + m_MiscFlags.GetHashCode()
				+ m_Format.GetHashCode() + m_Filter.GetHashCode() + m_MipFilter.GetHashCode();
		}

		bool ImageLoadInformation::Equals( Object^ value )
		{
			if( value == nullptr )
				return false;

			if( value->GetType() != GetType() )
				return false;

			return Equals( safe_cast<ImageLoadInformation>( value ) );
		}

		bool ImageLoadInformation::Equals( ImageLoadInformation value )
		{
			return (
				m_Width == value.m_Width && 
				m_Height == value.m_Height && 
				m_Depth == value.m_Depth && 
				m_FirstMipLevel == value.m_FirstMipLevel && 
				m_MipLevels == value.m_MipLevels && 
				m_Usage == value.m_Usage &&
				m_BindFlags == value.m_BindFlags &&
				m_CPUAccessFlags == value.m_CPUAccessFlags &&
				m_MiscFlags == value.m_MiscFlags && 
				m_Format == value.m_Format &&
				m_Filter == value.m_Filter &&
				m_MipFilter == value.m_MipFilter
			);
		}

		bool ImageLoadInformation::Equals( ImageLoadInformation% value1, ImageLoadInformation% value2 )
		{
			return (
				value1.m_Width == value2.m_Width && 
				value1.m_Height == value2.m_Height && 
				value1.m_Depth == value2.m_Depth && 
				value1.m_FirstMipLevel == value2.m_FirstMipLevel && 
				value1.m_MipLevels == value2.m_MipLevels && 
				value1.m_Usage == value2.m_Usage &&
				value1.m_BindFlags == value2.m_BindFlags &&
				value1.m_CPUAccessFlags == value2.m_CPUAccessFlags &&
				value1.m_MiscFlags == value2.m_MiscFlags && 
				value1.m_Format == value2.m_Format &&
				value1.m_Filter == value2.m_Filter &&
				value1.m_MipFilter == value2.m_MipFilter
			);
		}
	}
}
