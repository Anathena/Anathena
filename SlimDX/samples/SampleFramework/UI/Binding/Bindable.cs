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
    /// A wrapper for value types that enable their use in UI element bindings.
    /// </summary>
    /// <typeparam name="T">The underlying value type.</typeparam>
    public class Bindable<T> : IBindable where T : struct {
        #region Public Interface

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        public T Value {
            get {
                return value;
            }
            set {
                if (!object.Equals(this.value, value)) {
                    this.value = value;
                }
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Bindable&lt;T&gt;"/> class.
        /// </summary>
        public Bindable()
            : this(default(T)) {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Bindable&lt;T&gt;"/> class.
        /// </summary>
        /// <param name="value">The value.</param>
        public Bindable(T value) {
            this.value = value;
        }

        #endregion
        #region Implementation Detail

        T value;

        object IBindable.GetValue() {
            return Value;
        }

        #endregion
    }
}
