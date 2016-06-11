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
	namespace DirectWrite
	{
		/// <summary>Defines result codes that are returned by DirectWrite functions.</summary>
		/// <unmanaged href="ms679692">HRESULT</unmanaged>
		public ref class ResultCode sealed
		{
		private:
			ResultCode();
			
		public:
			/// <summary>
			/// Indicates an error in an input file such as a font file.
			/// </summary>
			/// <remarks>This is a failed result.</remarks>
			/// <unmanaged>DWRITE_E_FILEFORMAT</unmanaged>
			property static Result InvalidFileFormat
			{
				Result get();
			};

			/// <summary>
			/// Indicates an error originating in DirectWrite code, which is not expected to occur but is safe to recover from.
			/// </summary>
			/// <remarks>This is a failed result.</remarks>
			/// <unmanaged>DWRITE_E_UNEXPECTED</unmanaged>
			property static Result UnexpectedError
			{
				Result get();
			};

			/// <summary>
			/// Indicates the specified font does not exist.
			/// </summary>
			/// <remarks>This is a failed result.</remarks>
			/// <unmanaged>DWRITE_E_NOFONT</unmanaged>
			property static Result NonexistentFont
			{
				Result get();
			};

			/// <summary>
			/// Represents a generic failure result.
			/// </summary>
			/// <remarks>This is a failed result.</remarks>
			/// <unmanaged href="aa378137">E_FAIL</unmanaged>
			property static Result Failure
			{
				Result get();
			};

			/// <summary>
			/// A font file could not be opened because the file, directory, network location, drive, or other storage
			/// location does not exist or is unavailable.
			/// </summary>
			/// <remarks>This is a failed result.</remarks>
			/// <unmanaged>DWRITE_E_FILENOTFOUND</unmanaged>
			property static Result FileNotFound
			{
				Result get();
			};

			/// <summary>
			/// A font file exists but could not be opened due to access denied, sharing violation, or similar error.
			/// </summary>
			/// <remarks>This is a failed result.</remarks>
			/// <unmanaged>DWRITE_E_FILEACCESS</unmanaged>
			property static Result InvalidFileAccess
			{
				Result get();
			};

			/// <summary>
			/// A font collection is obsolete due to changes in the system.
			/// </summary>
			/// <remarks>This is a failed result.</remarks>
			/// <unmanaged>DWRITE_E_FONTCOLLECTIONOBSOLETE</unmanaged>
			property static Result FontCollectionObsolete
			{
				Result get();
			};

			/// <summary>
			/// The given interface is already registered.
			/// </summary>
			/// <remarks>This is a failed result.</remarks>
			/// <unmanaged>DWRITE_E_ALREADYREGISTERED</unmanaged>
			property static Result AlreadyRegistered
			{
				Result get();
			};

			/// <summary>
			/// Represents the result of a successful operation.
			/// </summary>
			/// <remarks>This is a successful result.</remarks>
			/// <unmanaged href="aa378137">S_OK</unmanaged>
			property static Result Success
			{
				Result get();
			};
		};
	}
}