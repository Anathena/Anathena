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

#include "IAudioProcessor.h"

extern const IID IID_CXAPOBase;

namespace SlimDX
{
	namespace XAPO
	{
		class XAPOBaseImpl;

		public ref class BaseProcessor abstract : ComObject, IAudioProcessor
		{
			COMOBJECT_BASE(CXAPOBase);

		internal:
			BaseProcessor() { }

			property XAPOBaseImpl *ImplPointer
			{
				XAPOBaseImpl *get() { return reinterpret_cast<XAPOBaseImpl*>( InternalPointer ); }
			}

		protected:
			BaseProcessor( RegistrationProperties properties );

			property bool IsLocked
			{
				bool get();
			}

			void ProcessThru( DataStream^ inputBuffer, array<float>^ outputBuffer, int frameCount, int inputChannelCount, int outputChannelCount, bool mixWithDestination );
			Result ValidateFormatDefault( SlimDX::Multimedia::WaveFormat^ format );
			Result ValidateFormatPair( SlimDX::Multimedia::WaveFormat^ supportedFormat, SlimDX::Multimedia::WaveFormat^ requestedFormat );

		public:
			virtual int CalculateInputFrames( int outputFrameCount );
			virtual int CalculateOutputFrames( int inputFrameCount );
			virtual Result Initialize( DataStream^ data );
			virtual bool IsInputFormatSupported( SlimDX::Multimedia::WaveFormat^ outputFormat, SlimDX::Multimedia::WaveFormat^ requestedInputFormat, [Out] SlimDX::Multimedia::WaveFormat^% supportedInputFormat );
			virtual bool IsOutputFormatSupported( SlimDX::Multimedia::WaveFormat^ inputFormat, SlimDX::Multimedia::WaveFormat^ requestedOutputFormat, [Out] SlimDX::Multimedia::WaveFormat^% supportedOutputFormat );
			virtual Result LockForProcess( array<LockParameter>^ inputParameters, array<LockParameter>^ outputParameters );
			virtual void Process( array<BufferParameter>^ inputParameters, array<BufferParameter>^ outputParameters, bool isEnabled ) abstract;
			virtual void Reset();
			virtual void UnlockForProcess();

			virtual property RegistrationProperties RegistrationProperties
			{
				SlimDX::XAPO::RegistrationProperties get();
			}
		};

		class XAPOBaseImpl : public CXAPOBase
		{
		private:
			gcroot<BaseProcessor^> m_processor;
			XAPO_REGISTRATION_PROPERTIES *pProperties;

		public:
			XAPOBaseImpl( BaseProcessor^ processor, XAPO_REGISTRATION_PROPERTIES *pRegProperties );
			virtual ~XAPOBaseImpl() { delete pProperties; pProperties = NULL; }

			const XAPO_REGISTRATION_PROPERTIES* WINAPI GetRegistrationPropertiesInternal() { return CXAPOBase::GetRegistrationPropertiesInternal(); }
			BOOL WINAPI IsLocked() { return CXAPOBase::IsLocked(); }
			void ProcessThru( void *pInputBuffer, FLOAT32 *pOutputBuffer, UINT32 FrameCount, WORD InputChannelCount, WORD OutputChannelCount, BOOL MixWithDestination ) { CXAPOBase::ProcessThru( pInputBuffer, pOutputBuffer, FrameCount, InputChannelCount, OutputChannelCount, MixWithDestination ); }
			HRESULT ValidateFormatDefault( WAVEFORMATEX *pFormat, BOOL fOverwrite ) { return CXAPOBase::ValidateFormatDefault( pFormat, fOverwrite ); }
			HRESULT ValidateFormatPair( const WAVEFORMATEX *pSupportedFormat, WAVEFORMATEX *pRequestedFormat, BOOL fOverwrite ) { return CXAPOBase::ValidateFormatPair( pSupportedFormat, pRequestedFormat, fOverwrite ); }

			UINT32  WINAPI CalcInputFrames( UINT32 OutputFrameCount );
			UINT32  WINAPI CalcOutputFrames( UINT32 InputFrameCount );
			HRESULT WINAPI GetRegistrationProperties( XAPO_REGISTRATION_PROPERTIES **ppRegistrationProperties );
			HRESULT WINAPI Initialize( const void *pData, UINT32 DataByteSize );
			HRESULT WINAPI IsInputFormatSupported( const WAVEFORMATEX *pOutputFormat, const WAVEFORMATEX *pRequestedInputFormat, WAVEFORMATEX **ppSupportedInputFormat );
			HRESULT WINAPI IsOutputFormatSupported( const WAVEFORMATEX *pInputFormat, const WAVEFORMATEX *pRequestedOutputFormat, WAVEFORMATEX **ppSupportedOutputFormat );
			HRESULT WINAPI LockForProcess( UINT32 InputLockedParameterCount, const XAPO_LOCKFORPROCESS_BUFFER_PARAMETERS *pInputLockedParameters, UINT32 OutputLockedParameterCount, const XAPO_LOCKFORPROCESS_BUFFER_PARAMETERS *pOutputLockedParameters );
			void	WINAPI Reset();
			void	WINAPI UnlockForProcess();
			void	WINAPI Process( UINT32 InputProcessParameterCount, const XAPO_PROCESS_BUFFER_PARAMETERS *pInputProcessParameters, UINT32 OutputProcessParameterCount, XAPO_PROCESS_BUFFER_PARAMETERS *pOutputProcessParameters, BOOL IsEnabled );
		
			bool ManagedCaller;
		};
	}
}