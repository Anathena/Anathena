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

//For some dumbass reason, VC++ EE does not include these headers.
//SlimDX does not need them right now; if it does, just write a 
//new marshal_as based on the header version.
//#include <msclr/marshal.h>
//#include <msclr/marshal_windows.h>
#include <windows.h>

#define SLIMDX_UNREFERENCED_PARAMETER(P) (P)

#ifdef NDEBUG
#	define SLIMDX_DEBUG_UNREFERENCED_PARAMETER(P)
#else
#	define SLIMDX_DEBUG_UNREFERENCED_PARAMETER(P) (P)
#endif

namespace msclr
{
	namespace interop
	{
		template<typename ToType, typename FromType>
		inline ToType marshal_as(const FromType& from);

		template<>
		inline System::Drawing::Rectangle marshal_as<System::Drawing::Rectangle, RECT>( const RECT &from )
		{
			return System::Drawing::Rectangle::FromLTRB( from.left, from.top, from.right, from.bottom );
		}

		template<class _To_Type>
		inline _To_Type marshal_as(System::Drawing::Rectangle _from_object);

		template<>
		inline RECT marshal_as<RECT>( System::Drawing::Rectangle from )
		{
			RECT to;
			to.left = from.Left;
			to.right = from.Right;
			to.top = from.Top;
			to.bottom = from.Bottom;

			return to;
		}

		template<>
		inline System::Drawing::Point marshal_as<System::Drawing::Point, POINT>( const POINT &from )
		{
			return System::Drawing::Point( from.x, from.y );
		}

		template<class _To_Type>
		inline _To_Type marshal_as(System::Drawing::Point _from_object);

		template<>
		inline POINT marshal_as<POINT>(System::Drawing::Point from)
		{
			POINT p;
			p.x = from.X;
			p.y = from.Y;

			return p;
		}
	}
}