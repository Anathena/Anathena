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

#include "IResettable.h"

namespace SlimDX
{
	namespace Direct3D9
	{
		/// <summary>Implements line drawing using textured triangles.</summary>
		/// <unmanaged>ID3DXLine</unmanaged>
		public ref class Line : ComObject, IResettable
		{
			COMOBJECT(ID3DXLine, Line);

		public:
			Line( SlimDX::Direct3D9::Device^ device );

			Result Begin();
			Result End();

			Result Draw( array<Vector2>^ vertexList, Color4 color );
			Result DrawTransformed( array<Vector3>^ vertexList, Matrix matrix, Color4 color );
			
			virtual Result OnLostDevice();
			virtual Result OnResetDevice();

			property Device^ Device
			{
				SlimDX::Direct3D9::Device^ get();
			}

			property bool Antialias
			{
				bool get();
				void set( bool value );
			}

			property bool GLLines
			{
				bool get();
				void set( bool value );
			}

			property int Pattern
			{
				int get();
				void set( int value );
			}

			property float PatternScale
			{
				float get();
				void set( float value );
			}

			property float Width
			{
				float get();
				void set( float value );
			}
		};
	}
}