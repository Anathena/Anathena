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

#include "../math/Quaternion.h"
#include "../math/Matrix.h"
#include "../math/Vector4.h"

#include "Enums.h"

namespace SlimDX
{
	namespace Direct3D9
	{
		///<summary>Describes a callback key for use in key frame animation.</summary>
		///<unmanaged>D3DXKEY_CALLBACK</unmanaged>
		public value class CallbackKey : System::IEquatable<CallbackKey>
		{
		internal:
			CallbackKey( const D3DXKEY_CALLBACK &key );

		public:
			property float Time;
			property System::IntPtr Data;

			static bool operator == ( CallbackKey left, CallbackKey right );
			static bool operator != ( CallbackKey left, CallbackKey right );

			virtual int GetHashCode() override;
			virtual bool Equals( System::Object^ obj ) override;
			virtual bool Equals( CallbackKey other );
			static bool Equals( CallbackKey% value1, CallbackKey% value2 );
		};

		///<summary>Describes a quaternion rotation key for use in key frame animation.</summary>
		///<unmanaged>D3DXKEY_QUATERNION</unmanaged>
		[System::Runtime::InteropServices::StructLayout(System::Runtime::InteropServices::LayoutKind::Sequential)]
		public value class RotationKey : System::IEquatable<RotationKey>
		{
		internal:
			RotationKey( const D3DXKEY_QUATERNION &key );

		public:
			property float Time;
			property Quaternion Value;

			static bool operator == ( RotationKey left, RotationKey right );
			static bool operator != ( RotationKey left, RotationKey right );

			virtual int GetHashCode() override;
			virtual bool Equals( System::Object^ obj ) override;
			virtual bool Equals( RotationKey other );
			static bool Equals( RotationKey% value1, RotationKey% value2 );
		};

		///<summary>Describes a vector scale key for use in key frame animation.</summary>
		///<unmanaged>D3DXKEY_VECTOR3</unmanaged>
		[System::Runtime::InteropServices::StructLayout(System::Runtime::InteropServices::LayoutKind::Sequential)]
		public value class ScaleKey : System::IEquatable<ScaleKey>
		{
		internal:
			ScaleKey( const D3DXKEY_VECTOR3 &key );

		public:
			property float Time;
			property Vector3 Value;

			static bool operator == ( ScaleKey left, ScaleKey right );
			static bool operator != ( ScaleKey left, ScaleKey right );

			virtual int GetHashCode() override;
			virtual bool Equals( System::Object^ obj ) override;
			virtual bool Equals( ScaleKey other );
			static bool Equals( ScaleKey% value1, ScaleKey% value2 );
		};

		///<summary>Describes a vector translation key for use in key frame animation.</summary>
		///<unmanaged>D3DXKEY_VECTOR3</unmanaged>
		[System::Runtime::InteropServices::StructLayout(System::Runtime::InteropServices::LayoutKind::Sequential)]
		public value class TranslationKey : System::IEquatable<TranslationKey>
		{
		internal:
			TranslationKey( const D3DXKEY_VECTOR3 &key );

		public:
			property float Time;
			property Vector3 Value;

			static bool operator == ( TranslationKey left, TranslationKey right );
			static bool operator != ( TranslationKey left, TranslationKey right );

			virtual int GetHashCode() override;
			virtual bool Equals( System::Object^ obj ) override;
			virtual bool Equals( TranslationKey other );
			static bool Equals( TranslationKey% value1, TranslationKey% value2 );
		};
	}
}