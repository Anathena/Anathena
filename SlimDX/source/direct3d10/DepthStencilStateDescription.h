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

#include "DepthStencilOperationDescription.h"
#include "Enums.h"

namespace SlimDX
{
	namespace Direct3D10
	{	
		public value class DepthStencilStateDescription : System::IEquatable<DepthStencilStateDescription>
		{
		private:
			bool m_DepthEnable;
			Direct3D10::DepthWriteMask m_DepthWriteMask;
			Comparison m_DepthFunc;
			bool m_StencilEnable;
			System::Byte m_StencilReadMask;
			System::Byte m_StencilWriteMask;
			DepthStencilOperationDescription m_FrontFace;
			DepthStencilOperationDescription m_BackFace;

		internal:
			DepthStencilStateDescription( const D3D10_DEPTH_STENCIL_DESC& description );
			
			D3D10_DEPTH_STENCIL_DESC CreateNativeVersion();
			
		public:
			property bool IsDepthEnabled
			{
				bool get();
				void set( bool value );
			}

			property Direct3D10::DepthWriteMask DepthWriteMask
			{
				Direct3D10::DepthWriteMask get();
				void set( Direct3D10::DepthWriteMask value );
			}

			property Comparison DepthComparison
			{
				Comparison get();
				void set( Comparison value );
			}

			property bool IsStencilEnabled
			{
				bool get();
				void set( bool value );
			}

			property System::Byte StencilReadMask
			{
				System::Byte get();
				void set( System::Byte value );
			}

			property System::Byte StencilWriteMask
			{
				System::Byte get();
				void set( System::Byte value );
			}

			property DepthStencilOperationDescription FrontFace
			{
				DepthStencilOperationDescription get();
				void set( DepthStencilOperationDescription value );
			}

			property DepthStencilOperationDescription BackFace
			{
				DepthStencilOperationDescription get();
				void set( DepthStencilOperationDescription value );
			}

			static bool operator == ( DepthStencilStateDescription left, DepthStencilStateDescription right );
			static bool operator != ( DepthStencilStateDescription left, DepthStencilStateDescription right );

			virtual int GetHashCode() override;
			virtual bool Equals( System::Object^ obj ) override;
			virtual bool Equals( DepthStencilStateDescription other );
			static bool Equals( DepthStencilStateDescription% value1, DepthStencilStateDescription% value2 );
		};
	}
};