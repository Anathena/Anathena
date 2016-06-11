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

namespace SlimDX.SampleFramework {
    /// <summary>
    /// Describes the desired application configuration of a <see cref="Sample">SlimDX sample</see>. 
    /// </summary>
    public class SampleConfiguration {
        #region Public Interface

        /// <summary>
        /// Initializes a new instance of the <see cref="SampleConfiguration"/> class.
        /// </summary>
        public SampleConfiguration() {
            WindowTitle = "SlimDX Sample";
            WindowWidth = 800;
            WindowHeight = 600;
        }

        /// <summary>
        /// Gets or sets the window title.
        /// </summary>
        public string WindowTitle {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the width of the window.
        /// </summary>
        public int WindowWidth {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the height of the window.
        /// </summary>
        public int WindowHeight {
            get;
            set;
        }

        #endregion
    }
}
