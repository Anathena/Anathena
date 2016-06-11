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

using System.Diagnostics;

using SlimDX;

namespace SlimDX.SampleFramework {
	/// <summary>
	/// A mechanism for tracking elapsed time.
	/// </summary>
	public class Clock {
		#region Public Interface

		/// <summary>
		/// Initializes a new instance of the <see cref="Clock"/> class.
		/// </summary>
		public Clock() {
			frequency = Stopwatch.Frequency;
		}

		public void Start() {
			count = Stopwatch.GetTimestamp();
			isRunning = true;
		}

		/// <summary>
		/// Updates the clock.
		/// </summary>
		/// <returns>The time, in seconds, that elapsed since the previous update.</returns>
		public float Update() {
			float result = 0.0f;
			if( isRunning ) {
				long last = count;
				count = Stopwatch.GetTimestamp();
				result = (float)( count - last ) / frequency;
			}

			return result;
		}

		#endregion
		#region Implementation Detail

		private bool isRunning;
		private readonly long frequency;
		private long count;

		#endregion
	}
}
