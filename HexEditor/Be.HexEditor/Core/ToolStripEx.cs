using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Be.HexEditor.Core
{
    public class ToolStripEx : ToolStrip
    {
        FormEx _form;
        float _dpiOld = 96F;

        public ToolStripEx()
        {
        }

        protected override void OnParentChanged(EventArgs e)
        {
            base.OnParentChanged(e);

            EnableFormEvents();
        }

        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);
            EnableFormEvents();
        }

        protected override void OnHandleDestroyed(EventArgs e)
        {
            base.OnHandleDestroyed(e);
            EnableFormEvents();
        }

        void EnableFormEvents()
        {
            _form = CoreUtil.GetParent<FormEx>(this);

            if(_form != null)
            {
                CoreUtil.AdjustImages(this, ref _dpiOld, _form.DpiNew);
            }
        }

        protected override void ScaleControl(System.Drawing.SizeF factor, BoundsSpecified specified)
        {
            base.ScaleControl(factor, specified);
            CoreUtil.AdjustImages(this, ref _dpiOld, _form.DpiNew);
        }
    }
}
