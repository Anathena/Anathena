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
#pragma once

#include "../ComObject.h"
#include "../SlimDXException.h"
#include "../multimedia/WaveFormat.h"

#include "RegistrationProperties.h"
#include "LockParameter.h"
#include "BufferParameter.h"
#include "IParameterProvider.h"

using System::Runtime::InteropServices::OutAttribute;

namespace SlimDX
{
	namespace XAPO
	{
		public interface struct IAudioProcessor : IComObject
		{
			virtual int CalculateInputFrames( int outputFrameCount ) = 0;
			virtual int CalculateOutputFrames( int inputFrameCount ) = 0;
			virtual Result Initialize( DataStream^ data ) = 0;
			virtual bool IsInputFormatSupported( SlimDX::Multimedia::WaveFormat^ outputFormat, SlimDX::Multimedia::WaveFormat^ requestedInputFormat, [Out] SlimDX::Multimedia::WaveFormat^% supportedInputFormat ) = 0;
			virtual bool IsOutputFormatSupported( SlimDX::Multimedia::WaveFormat^ inputFormat, SlimDX::Multimedia::WaveFormat^ requestedOutputFormat, [Out] SlimDX::Multimedia::WaveFormat^% supportedOutputFormat ) = 0;
			virtual Result LockForProcess( array<LockParameter>^ inputParameters, array<LockParameter>^ outputParameters ) = 0;
			virtual void Process( array<BufferParameter>^ inputParameters, array<BufferParameter>^ outputParameters, bool isEnabled ) = 0;
			virtual void Reset() = 0;
			virtual void UnlockForProcess() = 0;

			virtual property RegistrationProperties RegistrationProperties
			{
				SlimDX::XAPO::RegistrationProperties get();
			}
		};

		class XAPOShim : public IXAPO, public IXAPOParameters
		{
		private:
			LONG refCount;
			gcroot<IAudioProcessor^> m_interface;
			gcroot<IParameterProvider^> m_parameters;

		public:
			XAPOShim( IAudioProcessor^ wrappedInterface );

			IAudioProcessor^ GetProcessor() { return m_interface; }

		public:
			// IUnknown
			HRESULT WINAPI QueryInterface( const IID &iid, LPVOID *ppv );
			ULONG   WINAPI AddRef();
			ULONG   WINAPI Release();

			// IXAPO
			UINT32  WINAPI CalcInputFrames( UINT32 OutputFrameCount );
			UINT32  WINAPI CalcOutputFrames( UINT32 InputFrameCount );
			HRESULT WINAPI GetRegistrationProperties( XAPO_REGISTRATION_PROPERTIES **ppRegistrationProperties );
			HRESULT WINAPI Initialize( const void *pData, UINT32 DataByteSize );
			HRESULT WINAPI IsInputFormatSupported( const WAVEFORMATEX *pOutputFormat, const WAVEFORMATEX *pRequestedInputFormat, WAVEFORMATEX **ppSupportedInputFormat );
			HRESULT WINAPI IsOutputFormatSupported( const WAVEFORMATEX *pInputFormat, const WAVEFORMATEX *pRequestedOutputFormat, WAVEFORMATEX **ppSupportedOutputFormat );
			HRESULT WINAPI LockForProcess( UINT32 InputLockedParameterCount, const XAPO_LOCKFORPROCESS_BUFFER_PARAMETERS *pInputLockedParameters, UINT32 OutputLockedParameterCount, const XAPO_LOCKFORPROCESS_BUFFER_PARAMETERS *pOutputLockedParameters );
			void	WINAPI Process( UINT32 InputProcessParameterCount, const XAPO_PROCESS_BUFFER_PARAMETERS *pInputProcessParameters, UINT32 OutputProcessParameterCount, XAPO_PROCESS_BUFFER_PARAMETERS *pOutputProcessParameters, BOOL IsEnabled );
			void	WINAPI Reset();
			void	WINAPI UnlockForProcess();

			// IXAPOParameters
			void	WINAPI GetParameters( void *pParameters, UINT32 ParameterByteSize );
			void	WINAPI SetParameters( const void *pParameters, UINT32 ParameterByteSize );
		};
	}
}