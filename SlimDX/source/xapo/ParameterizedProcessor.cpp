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

#include "../ComObject.h"
#include "../DataStream.h"
#include "../stack_array.h"

const IID IID_CXAPOBase = __uuidof(IXAPO);
const IID IID_CXAPOParametersBase = __uuidof(IXAPOParameters);

#include "ParameterizedProcessor.h"

using namespace System;
using namespace SlimDX::Multimedia;

namespace SlimDX
{
namespace XAPO
{
	ParameterizedProcessor::ParameterizedProcessor( SlimDX::XAPO::RegistrationProperties properties, DataStream^ parameterBlocks, int blockSize, bool producer )
	{
		XAPO_REGISTRATION_PROPERTIES *props = properties.ToUnmanaged();

		Construct( static_cast<IXAPOParameters*>( new XAPOParametersImpl( this, props, reinterpret_cast<BYTE*>( parameterBlocks->PositionPointer ), blockSize, producer ) ) );
	}

	IntPtr ParameterizedProcessor::BeginProcess()
	{
		return IntPtr( ParamPointer->BeginProcess() );
	}

	void ParameterizedProcessor::EndProcess()
	{
		ParamPointer->EndProcess();
	}

	void ParameterizedProcessor::OnSetParameters( DataStream^ parameters )
	{
		SLIMDX_UNREFERENCED_PARAMETER( parameters );
	}

	void ParameterizedProcessor::GetParameters( DataStream^ parameters )
	{
		ParamPointer->ManagedCaller = true;
		ParamPointer->GetParameters( parameters->PositionPointer, static_cast<UINT32>( parameters->RemainingLength ) );
	}

	void ParameterizedProcessor::SetParameters( DataStream^ parameters )
	{
		ParamPointer->ManagedCaller = true;
		ParamPointer->SetParameters( parameters->PositionPointer, static_cast<UINT32>( parameters->RemainingLength ) );
	}

	bool ParameterizedProcessor::ParametersChanged::get()
	{
		return ParamPointer->ParametersChanged() > 0;
	}

	XAPOParametersImpl::XAPOParametersImpl( ParameterizedProcessor^ processor, XAPO_REGISTRATION_PROPERTIES *pRegProperties, BYTE *pParameterBlocks, UINT32 uParameterBlockByteSize, BOOL fProducer )
		: CXAPOParametersBase( pRegProperties, pParameterBlocks, uParameterBlockByteSize, fProducer )
	{
		pProperties = pRegProperties;
		m_processor = processor;
		ManagedCaller = false;
	}

	void XAPOParametersImpl::OnSetParameters( const void *pParameters, UINT32 ParameterByteSize )
	{
		m_processor->OnSetParameters( gcnew DataStream( pParameters, ParameterByteSize, true, false ) );
	}

	UINT32 WINAPI XAPOParametersImpl::CalcInputFrames( UINT32 OutputFrameCount )
	{
		if (ManagedCaller)
		{
			ManagedCaller = false;
			return CXAPOParametersBase::CalcInputFrames(OutputFrameCount);
		}
		else
		{
			try
			{
				return m_processor->CalculateInputFrames( OutputFrameCount );
			}
			catch(...)
			{
			}

			return 0;
		}
	}

	UINT32 WINAPI XAPOParametersImpl::CalcOutputFrames( UINT32 InputFrameCount )
	{
		if (ManagedCaller)
		{
			ManagedCaller = false;
			return CXAPOParametersBase::CalcOutputFrames(InputFrameCount);
		}
		else
		{
			try
			{
				return m_processor->CalculateOutputFrames( InputFrameCount );
			}
			catch(...)
			{
			}

			return 0;
		}
	}

	HRESULT WINAPI XAPOParametersImpl::GetRegistrationProperties( XAPO_REGISTRATION_PROPERTIES **ppRegistrationProperties )
	{
		if (ManagedCaller)
		{
			ManagedCaller = false;
			return CXAPOParametersBase::GetRegistrationProperties(ppRegistrationProperties);
		}
		else
		{
			try
			{
				XAPO_REGISTRATION_PROPERTIES *ptr = reinterpret_cast<XAPO_REGISTRATION_PROPERTIES*>( XAPOAlloc( sizeof( XAPO_REGISTRATION_PROPERTIES ) ) );
				XAPO_REGISTRATION_PROPERTIES *properties = m_processor->RegistrationProperties.ToUnmanaged();
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
	}

	HRESULT WINAPI XAPOParametersImpl::Initialize( const void *pData, UINT32 DataByteSize )
	{
		if (ManagedCaller)
		{
			ManagedCaller = false;
			return CXAPOParametersBase::Initialize(pData, DataByteSize);
		}
		else
		{
			try
			{
				DataStream^ data = gcnew DataStream( pData, DataByteSize, true, false );
				
				return m_processor->Initialize( data ).Code;
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
	}

	HRESULT WINAPI XAPOParametersImpl::IsInputFormatSupported( const WAVEFORMATEX *pOutputFormat, const WAVEFORMATEX *pRequestedInputFormat, WAVEFORMATEX **ppSupportedInputFormat )
	{
		if (ManagedCaller)
		{
			ManagedCaller = false;
			return CXAPOParametersBase::IsInputFormatSupported(pOutputFormat, pRequestedInputFormat, ppSupportedInputFormat);
		}
		else
		{
			try
			{
				WaveFormat^ format;
				bool result = m_processor->IsInputFormatSupported( WaveFormat::FromUnmanaged( *pOutputFormat ), WaveFormat::FromUnmanaged( *pRequestedInputFormat ), format );

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
	}

	HRESULT WINAPI XAPOParametersImpl::IsOutputFormatSupported( const WAVEFORMATEX *pInputFormat, const WAVEFORMATEX *pRequestedOutputFormat, WAVEFORMATEX **ppSupportedOutputFormat )
	{
		if (ManagedCaller)
		{
			ManagedCaller = false;
			return CXAPOParametersBase::IsOutputFormatSupported(pInputFormat, pRequestedOutputFormat, ppSupportedOutputFormat);
		}
		else
		{
			try
			{
				WaveFormat^ format;
				bool result = m_processor->IsOutputFormatSupported( WaveFormat::FromUnmanaged( *pInputFormat ), WaveFormat::FromUnmanaged( *pRequestedOutputFormat ), format );

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
	}

	HRESULT WINAPI XAPOParametersImpl::LockForProcess( UINT32 InputLockedParameterCount, const XAPO_LOCKFORPROCESS_BUFFER_PARAMETERS *pInputLockedParameters, UINT32 OutputLockedParameterCount, const XAPO_LOCKFORPROCESS_BUFFER_PARAMETERS *pOutputLockedParameters )
	{
		if (ManagedCaller)
		{
			ManagedCaller = false;
			return CXAPOParametersBase::LockForProcess(InputLockedParameterCount, pInputLockedParameters, OutputLockedParameterCount, pOutputLockedParameters);
		}
		else
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

				return m_processor->LockForProcess( input, output ).Code;
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
	}

	void WINAPI XAPOParametersImpl::Process( UINT32 InputProcessParameterCount, const XAPO_PROCESS_BUFFER_PARAMETERS *pInputProcessParameters, UINT32 OutputProcessParameterCount, XAPO_PROCESS_BUFFER_PARAMETERS *pOutputProcessParameters, BOOL IsEnabled )
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

			m_processor->Process( input, output, IsEnabled > 0 );
		}
		catch(...)
		{
		}
	}

	void WINAPI XAPOParametersImpl::Reset()
	{
		if (ManagedCaller)
		{
			ManagedCaller = false;
			CXAPOParametersBase::Reset();
		}
		else
		{
			try
			{
				m_processor->Reset();
			}
			catch(...)
			{
			}
		}
	}

	void WINAPI XAPOParametersImpl::UnlockForProcess()
	{
		if (ManagedCaller)
		{
			ManagedCaller = false;
			CXAPOParametersBase::UnlockForProcess();
		}
		else
		{
			try
			{
				m_processor->UnlockForProcess();
			}
			catch(...)
			{
			}
		}
	}

	void WINAPI XAPOParametersImpl::GetParameters( void *pParameters, UINT32 ParameterByteSize )
	{
		if (ManagedCaller)
		{
			ManagedCaller = false;
			CXAPOParametersBase::GetParameters(pParameters, ParameterByteSize);
		}
		else
		{
			try
			{
				m_processor->GetParameters( gcnew DataStream( pParameters, ParameterByteSize, true, true, false ) );
			}
			catch(...)
			{
			}
		}
	}

	void WINAPI XAPOParametersImpl::SetParameters( const void *pParameters, UINT32 ParameterByteSize )
	{
		if (ManagedCaller)
		{
			ManagedCaller = false;
			CXAPOParametersBase::SetParameters(pParameters, ParameterByteSize);
		}
		else
		{
			try
			{
				m_processor->SetParameters( gcnew DataStream( pParameters, ParameterByteSize, true, false ) );
			}
			catch(...)
			{
			}
		}
	}
}
}