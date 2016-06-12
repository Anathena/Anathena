using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Capture.Interface
{
    [Serializable]
    public class DisplayTextEventArgs: MarshalByRefObject
    {
        public string Text { get; set; }
        public TimeSpan Duration { get; set; }

        public DisplayTextEventArgs(string text, TimeSpan duration)
        {
            Text = text;
            Duration = duration;
        }

        public override string ToString()
        {
            return String.Format("{0}", Text);
        }
    }
}
