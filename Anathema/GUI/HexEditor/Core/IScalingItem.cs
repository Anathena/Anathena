using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Be.HexEditor.Core
{
    public interface IScalingItem
    {
        System.Drawing.Image Image16 { get; set; }
        System.Drawing.Image Image24 { get; set; }
        System.Drawing.Image Image32 { get; set; }
    }
}
