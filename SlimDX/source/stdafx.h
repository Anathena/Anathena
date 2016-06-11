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

#define XAUDIO2_HELPER_FUNCTIONS

#include <InitGuid.h>		// include these two headers in this order or die a horrible firey GUID related death
#include <CGuid.h>

#include <windows.h>
#include <vcclr.h>
#include <unknwn.h>

#include <d3d9.h>
#include <d3dx9.h>
#include <dxgi.h>
#include <d3d10_1.h>
#include <d3d10.h>
#include <d3dx10.h>
#include <d3d11.h>
#include <d3d11shader.h>
#include <d3dx11effect.h>
#include <d3dx11.h>
#include <d3dcsx.h>
#include <d2d1.h>
#include <d2d1helper.h>
#include <dwrite.h>
#include <dsound.h>
#include <dinput.h>
#include <xinput.h>
#include <xaudio2.h>
#include <x3daudio.h>
#include <audiodefs.h>
#include <xapo.h>
#include <xapobase.h>
#include <xact3.h>

#include <d3dcompiler.h>

#include <memory>
#include <stdexcept>
#include <cmath>