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
#include "stdafx.h"

#include "../DataStream.h"
#include "IAudioProcessor.h"

using namespace System;
using namespace SlimDX::Multimedia;

namespace SlimDX
{
namespace XAPO
{
	XAPOShim::XAPOShim( IAudioProcessor^ wrappedInterface )
	{
		m_interface = wrappedInterface;
		refCount = 1;

		if( IParameterProvider::typeid->IsAssignableFrom( wrappedInterface->GetType() ) )
			m_parameters = safe_cast<IParameterProvider^>( wrappedInterface );
		else
			m_parameters = nullptr;
	}

	HRESULT XAPOShim::QueryInterface( const IID &iid, LPVOID *ppv )
	{
		if( iid == __uuidof(IXAPO) )
		{
			AddRef();
			*ppv = static_cast<IXAPO*>( this );
			return S_OK;
		}
		else if( iid == __uuidof(IUnknown) )
		{
			AddRef();
			*ppv = static_cast<IUnknown*>( static_cast<IXAPO*>( this ) );
			return S_OK;
		}
		else if( iid == __uuidof(IXAPOParameters) && safe_cast<IParameterProvider^>( m_parameters ) != nullptr )
		{
			AddRef();
			*ppv = static_cast<IXAPOParameters*>( this );
			return S_OK;
		}

		return E_NOTIMPL;
	}

	ULONG XAPOShim::AddRef()
	{
		return InterlockedIncrement( &refCount );
	}

	ULONG XAPOShim::Release()
	{
		if( InterlockedDecrement( &refCount ) == 0 )
			delete this;
		return refCount;
	}

	UINT32 WINAPI XAPOShim::CalcInputFrames( UINT32 OutputFrameCount )
	{
		try
		{
			return m_interface->CalculateInputFrames( OutputFrameCount );
		}
		catch(...)
		{
		}

		return 0;
	}

	UINT32 WINAPI XAPOShim::CalcOutputFrames( UINT32 InputFrameCount )
	{
		try
		{
			return m_interface->CalculateOutputFrames( InputFrameCount );
		}
		catch(...)
		{
		}

		return 0;
	}

	HRESULT WINAPI XAPOShim::GetRegistrationProperties( XAPO_REGISTRATION_PROPERTIES **ppRegistrationProperties )
	{
		try
		{
			XAPO_REGISTRATION_PROPERTIES *ptr = reinterpret_cast<XAPO_REGISTRATION_PROPERTIES*>( XAPOAlloc( sizeof( XAPO_REGISTRATION_PROPERTIES ) ) );
			XAPO_REGISTRATION_PROPERTIES *properties = m_interface->RegistrationProperties.ToUnmanaged();
			memcpy( ptr, properties, sizeof( XAPO_REGISTRATION_PROPERTIES ) );

			*ppRegistrationProperties = ptr;
		}
		catch( SlimDXException^ ex )
		{
			return ex->ResultCode.Code;
		}
		catch( Exception^ )
		{
			return E_FAIL;
		}

		return S_OK;
	}

	HRESULT WINAPI XAPOShim::Initialize( const void *pData, UINT32 DataByteSize )
	{
		try
		{
			DataStream^ data = gcnew DataStream( pData, DataByteSize, true, false );
			
			return m_interface->Initialize( data ).Code;
		}
		catch( SlimDXException^ ex )
		{
			return ex->ResultCode.Code;
		}
		catch( Exception^ )
		{
			return E_FAIL;
		}

		return S_OK;
	}

	HRESULT WINAPI XAPOShim::IsInputFormatSupported( const WAVEFORMATEX *pOutputFormat, const WAVEFORMATEX *pRequestedInputFormat, WAVEFORMATEX **ppSupportedInputFormat )
	{
		try
		{
			WaveFormat^ format;
			bool result = m_interface->IsInputFormatSupported( WaveFormat::FromUnmanaged( *pOutputFormat ), WaveFormat::FromUnmanaged( *pRequestedInputFormat ), format );

			auto_array<WAVEFORMATEX> native = WaveFormat::ToUnmanaged( format );
			*ppSupportedInputFormat = native.release();

			return result ? S_OK : XAPO_E_FORMAT_UNSUPPORTED;
		}
		catch( SlimDXException^ ex )
		{
			return ex->ResultCode.Code;
		}
		catch( Exception^ )
		{
			return E_FAIL;
		}

		return S_OK;
	}

	HRESULT WINAPI XAPOShim::IsOutputFormatSupported( const WAVEFORMATEX *pInputFormat, const WAVEFORMATEX *pRequestedOutputFormat, WAVEFORMATEX **ppSupportedOutputFormat )
	{
		try
		{
			WaveFormat^ format;
			bool result = m_interface->IsOutputFormatSupported( WaveFormat::FromUnmanaged( *pInputFormat ), WaveFormat::FromUnmanaged( *pRequestedOutputFormat ), format );

			auto_array<WAVEFORMATEX> native = WaveFormat::ToUnmanaged( format );
			*ppSupportedOutputFormat = native.release();

			return result ? S_OK : XAPO_E_FORMAT_UNSUPPORTED;
		}
		catch( SlimDXException^ ex )
		{
			return ex->ResultCode.Code;
		}
		catch( Exception^ )
		{
			return E_FAIL;
		}

		return S_OK;
	}

	HRESULT WINAPI XAPOShim::LockForProcess( UINT32 InputLockedParameterCount, const XAPO_LOCKFORPROCESS_BUFFER_PARAMETERS *pInputLockedParameters, UINT32 OutputLockedParameterCount, const XAPO_LOCKFORPROCESS_BUFFER_PARAMETERS *pOutputLockedParameters )
	{
		try
		{
			array<LockParameter>^ input = gcnew array<LockParameter>( InputLockedParameterCount );
			for( int i = 0; i < input->Length; i++ )
			{
				LockParameter p;
				p.Format = WaveFormat::FromUnmanaged( *pInputLockedParameters[i].pFormat );
				p.MaxFrameCount = pInputLockedParameters[i].MaxFrameCount;

				input[i] = p;
			}

			array<LockParameter>^ output = gcnew array<LockParameter>( OutputLockedParameterCount );
			for( int i = 0; i < output->Length; i++ )
			{
				LockParameter p;
				p.Format = WaveFormat::FromUnmanaged( *pOutputLockedParameters[i].pFormat );
				p.MaxFrameCount = pOutputLockedParameters[i].MaxFrameCount;

				output[i] = p;
			}

			return m_interface->LockForProcess( input, output ).Code;
		}
		catch( SlimDXException^ ex )
		{
			return ex->ResultCode.Code;
		}
		catch( Exception^ )
		{
			return E_FAIL;
		}

		return S_OK;
	}

	void WINAPI XAPOShim::Process( UINT32 InputProcessParameterCount, const XAPO_PROCESS_BUFFER_PARAMETERS *pInputProcessParameters, UINT32 OutputProcessParameterCount, XAPO_PROCESS_BUFFER_PARAMETERS *pOutputProcessParameters, BOOL IsEnabled )
	{
		try
		{
			array<BufferParameter>^ input = gcnew array<BufferParameter>( InputProcessParameterCount );
			for( int i = 0; i < input->Length; i++ )
			{
				BufferParameter p;
				p.Buffer = IntPtr( pInputProcessParameters[i].pBuffer );
				p.Flags = static_cast<BufferFlags>( pInputProcessParameters[i].BufferFlags );
				p.ValidFrameCount = pInputProcessParameters[i].ValidFrameCount;

				input[i] = p;
			}

			array<BufferParameter>^ output = gcnew array<BufferParameter>( OutputProcessParameterCount );
			for( int i = 0; i < output->Length; i++ )
			{
				BufferParameter p;
				p.Buffer = IntPtr( pOutputProcessParameters[i].pBuffer );
				p.Flags = static_cast<BufferFlags>( pOutputProcessParameters[i].BufferFlags );
				p.ValidFrameCount = pOutputProcessParameters[i].ValidFrameCount;

				output[i] = p;
			}

			m_interface->Process( input, output, IsEnabled > 0 );
		}
		catch(...)
		{
		}
	}

	void WINAPI XAPOShim::Reset()
	{
		try
		{
			m_interface->Reset();
		}
		catch(...)
		{
		}
	}

	void WINAPI XAPOShim::UnlockForProcess()
	{
		try
		{
			m_interface->UnlockForProcess();
		}
		catch(...)
		{
		}
	}

	void WINAPI XAPOShim::GetParameters( void *pParameters, UINT32 ParameterByteSize )
	{
		try
		{
			m_parameters->GetParameters( gcnew DataStream( pParameters, ParameterByteSize, true, true, false ) );
		}
		catch(...)
		{
		}
	}

	void WINAPI XAPOShim::SetParameters( const void *pParameters, UINT32 ParameterByteSize )
	{
		try
		{
			m_parameters->SetParameters( gcnew DataStream( pParameters, ParameterByteSize, true, false ) );
		}
		catch(...)
		{
		}
	}
}
}