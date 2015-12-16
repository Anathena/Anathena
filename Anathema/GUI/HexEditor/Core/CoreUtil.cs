using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Be.HexEditor.Core
{
    public static class CoreUtil
    {
        public static void ScaleFont(Control control, float factor)
        {
            control.Font = new Font(control.Font.FontFamily,
                                 control.Font.Size * factor,
                                 control.Font.Style);
        }

        public static void AdjustImages(ToolStrip toolStrip, ref float dpiOld, float dpiNew)
        {
            if (Util.DesignMode) return;

            //var newDpi = _form.DpiNew;
            var factor = dpiNew / dpiOld;

            //MessageBox.Show(factor.ToString());

            if ((dpiNew == 0) || (dpiOld == dpiNew)) return; // Abort.

            //float factor = _form.DpiNew / _dpiOld;

            dpiOld = dpiNew;

            //MessageBox.Show(factor.ToString());

            toolStrip.ImageScalingSize = new System.Drawing.Size((int)(toolStrip.ImageScalingSize.Width * factor), (int)(toolStrip.ImageScalingSize.Height * factor));
            //MessageBox.Show(toolStrip.ImageScalingSize.Width.ToString());

            var width = toolStrip.ImageScalingSize.Width;

            foreach (ToolStripItem item in toolStrip.Items)
            {
                var scalingItem = item as IScalingItem;
                if (scalingItem == null)
                    continue;

                if (width < 17 && scalingItem.Image16 != null)
                    item.Image = scalingItem.Image16;
                else if (width < 25 && scalingItem.Image24 != null)
                    item.Image = scalingItem.Image24;
                else if (width < 33 && scalingItem.Image32 != null)
                    item.Image = scalingItem.Image32;
            }
        }

        public static T GetParent<T>(Control c) where T : Control
        {
            if (c == null)
                return default(T);

            var parent = c.Parent;

            var found = parent as T;
            if (found != null)
                return found;

            return GetParent<T>(parent);
        }
    }
}
