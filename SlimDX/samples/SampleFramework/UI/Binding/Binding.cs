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

using System.Reflection;

namespace SlimDX.SampleFramework {
    class Binding {
        #region Public Interface

        public Binding(string targetName, object source) {
            this.targetName = targetName;
            this.source = source;
        }

        public void Update(object target) {
            PropertyInfo property = target.GetType().GetProperty(targetName);
            if (property == null) {
                return;
            }

            object value = source;
            IBindable bindable = source as IBindable;
            if (bindable != null) {
                value = bindable.GetValue();
            }

            if (property.PropertyType.IsAssignableFrom(value.GetType())) {
                property.SetValue(target, value, null);
            } else if (property.PropertyType == typeof(string)) {
                property.SetValue(target, value.ToString(), null);
            }
        }

        #endregion
        #region Implementation Detail

        string targetName;
        object source;

        #endregion
    }
}
