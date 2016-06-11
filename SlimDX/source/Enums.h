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
	// NOTE: The enumerations defined in this file are in alphabetical order. When
	//       adding new enumerations or renaming existing ones, please make sure
	//       the ordering is maintained.
	
	/// <summary>
	/// Specifies possible performance profiling options.
	/// </summary>
	[System::Flags]
	public enum class PerformanceOptions : System::Int32
	{
		/// <summary>
		/// No performance options specified.
		/// </summary>
		None = 0,

		/// <summary>
		/// Do not allow performance profiling.
		/// </summary>
		DoNotAllowProfiling = 1,
	};

	/// <summary>
	/// Specifies possible behaviors of result watches.
	/// </summary>
	[System::Flags]
	public enum class ResultWatchFlags : System::Int32
	{
		/// <summary>
		/// Always ignore the result.
		/// </summary>
		AlwaysIgnore = 0,

		/// <summary>
		/// Break whenever the result occurs.
		/// </summary>
		Break = 1,

		/// <summary>
		/// Throw an exception whenever the result occurs.
		/// </summary>
		Throw = 2,
	};
}