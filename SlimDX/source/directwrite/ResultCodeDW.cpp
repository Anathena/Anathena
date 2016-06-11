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
#include "stdafx.h"

#include "DirectWriteException.h"
#include "ResultCodeDW.h"

namespace SlimDX
{
namespace DirectWrite
{
	ResultCode::ResultCode()
	{
	}

	Result ResultCode::InvalidFileFormat::get()
	{
		return Result( DWRITE_E_FILEFORMAT );
	}

	Result ResultCode::UnexpectedError::get()
	{
		return Result( DWRITE_E_UNEXPECTED );
	}

	Result ResultCode::NonexistentFont::get()
	{
		return Result( DWRITE_E_NOFONT );
	}

	Result ResultCode::FileNotFound::get()
	{
		return Result( DWRITE_E_FILENOTFOUND );
	}

	Result ResultCode::InvalidFileAccess::get()
	{
		return Result( DWRITE_E_FILEACCESS );
	}

	Result ResultCode::FontCollectionObsolete::get()
	{
		return Result( DWRITE_E_FONTCOLLECTIONOBSOLETE );
	}

	Result ResultCode::AlreadyRegistered::get()
	{
		return Result( DWRITE_E_ALREADYREGISTERED );
	}

	Result ResultCode::Failure::get()
	{
		return Result( E_FAIL );
	}

	Result ResultCode::Success::get()
	{
		return Result( S_OK );
	}
}
}