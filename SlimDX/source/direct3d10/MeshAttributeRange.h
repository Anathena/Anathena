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

#include <d3dx10.h>

namespace SlimDX
{
	namespace Direct3D10
	{
		public value class MeshAttributeRange : System::IEquatable<MeshAttributeRange>
		{
		private:
			int m_AttribId;
			int m_FaceStart;
			int m_FaceCount;
			int m_VertexStart;
			int m_VertexCount;
		
		internal:
			MeshAttributeRange( const D3DX10_ATTRIBUTE_RANGE &native );

			D3DX10_ATTRIBUTE_RANGE CreateNativeVersion();

		public:
			property int Id
			{
				int get();
				void set( int value );
			}

			property int FaceStart
			{
				int get();
				void set( int value );
			}

			property int FaceCount
			{
				int get();
				void set( int value );
			}

			property int VertexStart
			{
				int get();
				void set( int value );
			}

			property int VertexCount
			{
				int get();
				void set( int value );
			}

			static bool operator == ( MeshAttributeRange left, MeshAttributeRange right );
			static bool operator != ( MeshAttributeRange left, MeshAttributeRange right );

			virtual int GetHashCode() override;
			virtual bool Equals( System::Object^ obj ) override;
			virtual bool Equals( MeshAttributeRange other );
			static bool Equals( MeshAttributeRange% value1, MeshAttributeRange% value2 );
		};
	}
};