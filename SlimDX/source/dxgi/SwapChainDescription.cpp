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

#include <dxgi.h>

#include "SwapChainDescription.h"

using namespace System;

namespace SlimDX
{
namespace DXGI
{ 
	SwapChainDescription::SwapChainDescription( const DXGI_SWAP_CHAIN_DESC& native )
	{
		m_BufferDesc = DXGI::ModeDescription( native.BufferDesc );
		m_SampleDesc = DXGI::SampleDescription( native.SampleDesc );
		m_BufferUsage = static_cast<DXGI::Usage>( native.BufferUsage );
		m_BufferCount = native.BufferCount;
		m_OutputWindow = IntPtr( native.OutputWindow );
		m_Windowed = native.Windowed ? true : false;
		m_SwapEffect = static_cast<DXGI::SwapEffect>( native.SwapEffect );
		m_Flags = static_cast<SwapChainFlags>( native.Flags );
	}
	
	DXGI_SWAP_CHAIN_DESC SwapChainDescription::CreateNativeVersion()
	{
		DXGI_SWAP_CHAIN_DESC native;
		
		native.BufferDesc = m_BufferDesc.CreateNativeVersion();
		native.SampleDesc = m_SampleDesc.CreateNativeVersion();
		native.BufferUsage = static_cast<DXGI_USAGE>( m_BufferUsage );
		native.BufferCount = m_BufferCount;
		native.OutputWindow = static_cast<HWND>( m_OutputWindow.ToPointer() );
		native.Windowed = m_Windowed;
		native.SwapEffect = static_cast<DXGI_SWAP_EFFECT>( m_SwapEffect );
		native.Flags = static_cast<UINT>( m_Flags );
		
		return native;
	}
	
	DXGI::ModeDescription SwapChainDescription::ModeDescription::get()
	{
		return m_BufferDesc;
	}

	void SwapChainDescription::ModeDescription::set( DXGI::ModeDescription value )
	{
		m_BufferDesc = value;
	}

	DXGI::SampleDescription SwapChainDescription::SampleDescription::get()
	{
		return m_SampleDesc;
	}

	void SwapChainDescription::SampleDescription::set( DXGI::SampleDescription value )
	{
		m_SampleDesc = value;
	}
	
	DXGI::Usage SwapChainDescription::Usage::get()
	{
		return m_BufferUsage;
	}
	
	void SwapChainDescription::Usage::set( DXGI::Usage value )
	{
		m_BufferUsage = value;
	}
	
	int SwapChainDescription::BufferCount::get()
	{
		return m_BufferCount;
	}
	
	void SwapChainDescription::BufferCount::set( int value )
	{
		m_BufferCount = value;
	}
	
	IntPtr SwapChainDescription::OutputHandle::get()
	{
		return m_OutputWindow;
	}
	
	void SwapChainDescription::OutputHandle::set( IntPtr value )
	{
		m_OutputWindow = value;
	}
	
	bool SwapChainDescription::IsWindowed::get()
	{
		return m_Windowed;
	}
	
	void SwapChainDescription::IsWindowed::set( bool value )
	{
		m_Windowed = value;
	}
	
	DXGI::SwapEffect SwapChainDescription::SwapEffect::get()
	{
		return m_SwapEffect;
	}
	
	void SwapChainDescription::SwapEffect::set( DXGI::SwapEffect value )
	{
		m_SwapEffect = value;
	}
	
	SwapChainFlags SwapChainDescription::Flags::get()
	{
		return m_Flags;
	}
	
	void SwapChainDescription::Flags::set( SwapChainFlags value )
	{
		m_Flags = value;
	}

	bool SwapChainDescription::operator == ( SwapChainDescription left, SwapChainDescription right )
	{
		return SwapChainDescription::Equals( left, right );
	}

	bool SwapChainDescription::operator != ( SwapChainDescription left, SwapChainDescription right )
	{
		return !SwapChainDescription::Equals( left, right );
	}

	int SwapChainDescription::GetHashCode()
	{
		return m_BufferDesc.GetHashCode() + m_SampleDesc.GetHashCode() + m_BufferUsage.GetHashCode()
			 + m_BufferCount.GetHashCode() + m_OutputWindow.GetHashCode() + m_Windowed.GetHashCode()
			 + m_SwapEffect.GetHashCode() + m_Flags.GetHashCode();
	}

	bool SwapChainDescription::Equals( Object^ value )
	{
		if( value == nullptr )
			return false;

		if( value->GetType() != GetType() )
			return false;

		return Equals( safe_cast<SwapChainDescription>( value ) );
	}

	bool SwapChainDescription::Equals( SwapChainDescription value )
	{
		return ( m_BufferDesc == value.m_BufferDesc && m_SampleDesc == value.m_SampleDesc && m_BufferUsage == value.m_BufferUsage
			 && m_BufferCount == value.m_BufferCount && m_OutputWindow == value.m_OutputWindow && m_Windowed == value.m_Windowed
			 && m_SwapEffect == value.m_SwapEffect && m_Flags == value.m_Flags );
	}

	bool SwapChainDescription::Equals( SwapChainDescription% value1, SwapChainDescription% value2 )
	{
		return ( value1.m_BufferDesc == value2.m_BufferDesc && value1.m_SampleDesc == value2.m_SampleDesc && value1.m_BufferUsage == value2.m_BufferUsage
			 && value1.m_BufferCount == value2.m_BufferCount && value1.m_OutputWindow == value2.m_OutputWindow && value1.m_Windowed == value2.m_Windowed
			 && value1.m_SwapEffect == value2.m_SwapEffect && value1.m_Flags == value2.m_Flags );
	}
}
}
