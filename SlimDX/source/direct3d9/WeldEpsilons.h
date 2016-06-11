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
	namespace Direct3D9
	{
		[System::Runtime::InteropServices::StructLayout(System::Runtime::InteropServices::LayoutKind::Sequential)]
		public value class WeldEpsilons : System::IEquatable<WeldEpsilons>
		{
		public:
			property float Position;
			property float BlendWeights;
			property float Normal;
			property float PointSize;
			property float Specular;
			property float Diffuse;
			property float TextureCoordinate1;
			property float TextureCoordinate2;
			property float TextureCoordinate3;
			property float TextureCoordinate4;
			property float TextureCoordinate5;
			property float TextureCoordinate6;
			property float TextureCoordinate7;
			property float TextureCoordinate8;
			property float Tangent;
			property float Binormal;
			property float TessellationFactor;

			static bool operator == ( WeldEpsilons left, WeldEpsilons right );
			static bool operator != ( WeldEpsilons left, WeldEpsilons right );

			virtual int GetHashCode() override;
			virtual bool Equals( System::Object^ obj ) override;
			virtual bool Equals( WeldEpsilons other );
			static bool Equals( WeldEpsilons% value1, WeldEpsilons% value2 );
		};
	}
}