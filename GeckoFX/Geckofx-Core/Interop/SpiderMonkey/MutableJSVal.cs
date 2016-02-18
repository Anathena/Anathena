using System.Runtime.InteropServices;

namespace Gecko
{
    [StructLayout(LayoutKind.Sequential)]
    public struct MutableJSVal
    {
        private JsVal _val;

        public MutableJSVal(JsVal val)
        {
            _val = val;
        }

        public JsVal Val
        {
            get { return _val; }
        }
    }
}