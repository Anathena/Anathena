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

// This machinery is required for this assembly to have friend
// access to SlimDX itself.
#ifdef X64
#ifdef PUBLIC
#using "../../build/x64/Public/SlimDX.dll" as_friend
#else
#ifdef NDEBUG
#using "../../build/x64/Release/SlimDX.dll" as_friend
#else
#using "../../build/x64/Debug/SlimDX.dll" as_friend
#endif
#endif
#else
#ifdef PUBLIC
#using "../../build/x86/Public/SlimDX.dll" as_friend
#else
#ifdef NDEBUG
#using "../../build/x86/Release/SlimDX.dll" as_friend
#else
#using "../../build/x86/Debug/SlimDX.dll" as_friend
#endif
#endif
#endif

#pragma warning(push)
#pragma warning(disable:4793) // 'compiled as native'
#include <gtest/gtest.h>
#include <gmock/gmock.h>
#pragma warning(pop)

#include <dxgi.h>
#include <d3d10.h>

#pragma warning(push)
#pragma warning(disable:4793) // 'compiled as native'
#include <strsafe.h>
#pragma warning(pop)