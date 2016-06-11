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
	value class Vector3;
	value class Matrix;

	namespace Direct3D9
	{
		/// <summary>Applications use the methods of the MatrixStack class to manipulate a matrix stack.</summary>
		/// <unmanaged>ID3DXMatrixStack</unmanaged>
		public ref class MatrixStack : ComObject
		{
			COMOBJECT(ID3DXMatrixStack, MatrixStack);

		public:
			MatrixStack();

			Result Push();
			Result Pop();

			Result LoadIdentity();
			Result LoadMatrix( Matrix matrix );
			Result MultiplyMatrix( Matrix matrix );
			Result MultiplyMatrixLocal( Matrix matrix );

			Result RotateAxis( Vector3 axis, float angle );
			Result RotateAxisLocal( Vector3 axis, float angle );
			Result RotateYawPitchRoll( float yaw, float pitch, float roll );
			Result RotateYawPitchRollLocal( float yaw, float pitch, float roll );

			Result Scale( float x, float y, float z );
			Result ScaleLocal( float x, float y, float z );

			Result Translate( float x, float y, float z );
			Result TranslateLocal( float x, float y, float z );

			property Matrix Top
			{
				Matrix get();
			}
		};
	}
}
