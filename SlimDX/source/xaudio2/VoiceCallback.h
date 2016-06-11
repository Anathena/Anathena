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

namespace SlimDX
{
	namespace XAudio2
	{
		ref class SourceVoice;

		class VoiceCallbackShim : public IXAudio2VoiceCallback
		{
		private:
			gcroot<SourceVoice^> m_WrappedInterface;

		public:
			VoiceCallbackShim( SourceVoice^ wrappedInterface );

			void WINAPI OnBufferEnd( void *context );
			void WINAPI OnBufferStart( void *context );
			void WINAPI OnLoopEnd( void *context );
			void WINAPI OnStreamEnd();
			void WINAPI OnVoiceError( void *context, HRESULT error );
			void WINAPI OnVoiceProcessingPassStart( UINT32 bytesRequired );
			void WINAPI OnVoiceProcessingPassEnd();
		};
	}
}