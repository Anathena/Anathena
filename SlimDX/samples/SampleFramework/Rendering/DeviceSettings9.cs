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

using SlimDX.Direct3D9;

namespace SlimDX.SampleFramework {
    /// <summary>
    /// Settings used to initialize a Direct3D9 device.
    /// </summary>
    public class DeviceSettings9 {
        /// <summary>
        /// Gets or sets the adapter ordinal.
        /// </summary>
        public int AdapterOrdinal {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the creation flags.
        /// </summary>
        public CreateFlags CreationFlags {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the width of the renderable area.
        /// </summary>
        public int Width {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the height of the renderable area.
        /// </summary>
        public int Height {
            get;
            set;
        }
    }
}
