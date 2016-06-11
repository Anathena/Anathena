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

#include "Enums.h"
#include "ModeDescription.h"
#include "SampleDescription.h"

namespace SlimDX
{
	namespace DXGI
	{		
		/// <summary>
		/// Describes properties of a swap chain.
		/// </summary>
		/// <unmanaged>DXGI_SWAP_CHAIN_DESC</unmanaged>
		public value class SwapChainDescription : System::IEquatable<SwapChainDescription>
		{
		private:
			ModeDescription m_BufferDesc;
			SampleDescription m_SampleDesc;
			Usage m_BufferUsage;
			int m_BufferCount;
			System::IntPtr m_OutputWindow;
			bool m_Windowed;
			SwapEffect m_SwapEffect;
			SwapChainFlags m_Flags;
		
		internal:
			SwapChainDescription( const DXGI_SWAP_CHAIN_DESC& native );
			
			DXGI_SWAP_CHAIN_DESC CreateNativeVersion();

		public:
			/// <summary>
			/// Gets or sets the swap chain's display mode properties.
			/// </summary>
			property ModeDescription ModeDescription
			{
				DXGI::ModeDescription get();
				void set( DXGI::ModeDescription value );
			}

			/// <summary>
			/// Gets or sets the swap chain's multisampling properties.
			/// </summary>
			property SampleDescription SampleDescription
			{
				DXGI::SampleDescription get();
				void set( DXGI::SampleDescription value );
			}
			
			/// <summary>
			/// Gets or sets swap chain's surface usage.
			/// </summary>
			property Usage Usage
			{
				DXGI::Usage get();
				void set( DXGI::Usage value );
			}

			/// <summary>
			/// Gets or sets the number of buffers in the swap chain.
			/// </summary>
			property int BufferCount
			{
				int get();
				void set( int value );
			}
			
			/// <summary>
			/// Gets or sets the swap chain's output handle.
			/// </summary>
			property System::IntPtr OutputHandle
			{
				System::IntPtr get();
				void set( System::IntPtr value );
			}

			/// <summary>
			/// Gets or sets a value indicating whether or not the swap chain is in windowed mode.
			/// </summary>
			property bool IsWindowed
			{
				bool get();
				void set( bool value );
			}

			/// <summary>
			/// Gets or sets swapping effect behavior.
			/// </summary>
			property DXGI::SwapEffect SwapEffect
			{
				DXGI::SwapEffect get();
				void set( DXGI::SwapEffect value );
			}

			/// <summary>
			/// Gets or sets swap chain options.
			/// </summary>
			property SwapChainFlags Flags
			{
				SwapChainFlags get();
				void set( SwapChainFlags value );
			}

			static bool operator == ( SwapChainDescription left, SwapChainDescription right );
			static bool operator != ( SwapChainDescription left, SwapChainDescription right );

			virtual int GetHashCode() override;
			virtual bool Equals( System::Object^ obj ) override;
			virtual bool Equals( SwapChainDescription other );
			static bool Equals( SwapChainDescription% value1, SwapChainDescription% value2 );
		};
	}
};