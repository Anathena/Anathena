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

namespace SlimDX
{
	namespace Direct3D10
	{	
		public value class RasterizerStateDescription : System::IEquatable<RasterizerStateDescription>
		{
		private:
			Direct3D10::FillMode m_FillMode;
			Direct3D10::CullMode m_CullMode;
			bool m_FrontCounterClockwise;
			int m_DepthBias;
			float m_DepthBiasClamp;
			float m_SlopeScaledDepthBias;
			bool m_DepthClipEnable;
			bool m_ScissorEnable;
			bool m_MultisampleEnable;
			bool m_AntialiasedLineEnable;

		internal:
			RasterizerStateDescription( const D3D10_RASTERIZER_DESC& description );
			
			D3D10_RASTERIZER_DESC CreateNativeVersion();
			
		public:
			property Direct3D10::FillMode FillMode
			{
				Direct3D10::FillMode get();
				void set( Direct3D10::FillMode value );
			}

			property Direct3D10::CullMode CullMode
			{
				Direct3D10::CullMode get();
				void set( Direct3D10::CullMode value );
			}

			property bool IsFrontCounterclockwise
			{
				bool get();
				void set( bool value );
			}

			property int DepthBias
			{
				int get();
				void set( int value );
			}

			property float DepthBiasClamp
			{
				float get();
				void set( float value );
			}

			property float SlopeScaledDepthBias
			{
				float get();
				void set( float value );
			}

			property bool IsDepthClipEnabled
			{
				bool get();
				void set( bool value );
			}

			property bool IsScissorEnabled
			{
				bool get();
				void set( bool value );
			}

			property bool IsMultisampleEnabled
			{
				bool get();
				void set( bool value );
			}

			property bool IsAntialiasedLineEnabled
			{
				bool get();
				void set( bool value );
			}

			static bool operator == ( RasterizerStateDescription left, RasterizerStateDescription right );
			static bool operator != ( RasterizerStateDescription left, RasterizerStateDescription right );

			virtual int GetHashCode() override;
			virtual bool Equals( System::Object^ obj ) override;
			virtual bool Equals( RasterizerStateDescription other );
			static bool Equals( RasterizerStateDescription% value1, RasterizerStateDescription% value2 );
		};
	}
};