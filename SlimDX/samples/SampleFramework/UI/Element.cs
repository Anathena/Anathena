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

using System.Collections.Generic;
namespace SlimDX.SampleFramework {
    /// <summary>
    /// Provides basic logical UI component functionality.
    /// </summary>
    public class Element {
        #region Public Interface

        /// <summary>
        /// Gets or sets the element's label.
        /// </summary>
        public string Label {
            get;
            set;
        }

        public void SetBinding(string targetName, object source) {
            bindings[targetName] = new Binding(targetName, source);
        }

        public void Update() {
            foreach (Binding binding in bindings.Values) {
                binding.Update(this);
            }
        }

        #endregion
        #region Implementation Detail

        Dictionary<string, Binding> bindings = new Dictionary<string, Binding>();

        #endregion
    }
}
