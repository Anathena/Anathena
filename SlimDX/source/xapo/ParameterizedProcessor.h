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

#include "BaseProcessor.h"

namespace SlimDX
{
	namespace XAPO
	{
		class XAPOParametersImpl;

		public ref class ParameterizedProcessor abstract : BaseProcessor, IParameterProvider
		{
		internal:
			property XAPOParametersImpl *ParamPointer
			{
				XAPOParametersImpl *get() { return reinterpret_cast<XAPOParametersImpl*>( InternalPointer ); }
			}

		public protected:
			ParameterizedProcessor( SlimDX::XAPO::RegistrationProperties properties, DataStream^ parameterBlocks, int blockSize, bool producer );

			virtual void OnSetParameters( DataStream^ parameters );

		public:
			System::IntPtr BeginProcess();
			void EndProcess();

			virtual void GetParameters( DataStream^ parameters );
			virtual void SetParameters( DataStream^ parameters );

			property bool ParametersChanged
			{
				bool get();
			};
		};

		class XAPOParametersImpl : public CXAPOParametersBase
		{
		private:
			gcroot<ParameterizedProcessor^> m_processor;
			XAPO_REGISTRATION_PROPERTIES *pProperties;

		public:
			XAPOParametersImpl( ParameterizedProcessor^ processor, XAPO_REGISTRATION_PROPERTIES *pRegProperties, BYTE *pParameterBlocks, UINT32 uParameterBlockByteSize, BOOL fProducer );
			virtual ~XAPOParametersImpl() { delete pProperties; pProperties = NULL; }

			virtual void OnSetParameters( const void *pParameters, UINT32 ParameterByteSize );

			UINT32  WINAPI CalcInputFrames( UINT32 OutputFrameCount );
			UINT32  WINAPI CalcOutputFrames( UINT32 InputFrameCount );
			HRESULT WINAPI GetRegistrationProperties( XAPO_REGISTRATION_PROPERTIES **ppRegistrationProperties );
			HRESULT WINAPI Initialize( const void *pData, UINT32 DataByteSize );
			HRESULT WINAPI IsInputFormatSupported( const WAVEFORMATEX *pOutputFormat, const WAVEFORMATEX *pRequestedInputFormat, WAVEFORMATEX **ppSupportedInputFormat );
			HRESULT WINAPI IsOutputFormatSupported( const WAVEFORMATEX *pInputFormat, const WAVEFORMATEX *pRequestedOutputFormat, WAVEFORMATEX **ppSupportedOutputFormat );
			HRESULT WINAPI LockForProcess( UINT32 InputLockedParameterCount, const XAPO_LOCKFORPROCESS_BUFFER_PARAMETERS *pInputLockedParameters, UINT32 OutputLockedParameterCount, const XAPO_LOCKFORPROCESS_BUFFER_PARAMETERS *pOutputLockedParameters );
			void	WINAPI Reset();
			void	WINAPI UnlockForProcess();

			void	WINAPI GetParameters( void *pParameters, UINT32 ParameterByteSize );
			void	WINAPI SetParameters( const void *pParameters, UINT32 ParameterByteSize );

			bool ManagedCaller;

			void WINAPI Process( UINT32 InputProcessParameterCount, const XAPO_PROCESS_BUFFER_PARAMETERS *pInputProcessParameters, UINT32 OutputProcessParameterCount, XAPO_PROCESS_BUFFER_PARAMETERS *pOutputProcessParameters, BOOL IsEnabled );
		};
	}
}