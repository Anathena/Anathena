using System;
using System.Collections.Generic;

namespace Anathema.Source.SystemInternals.Graphics.DirectX.Hook.Common
{
    public class Overlay : IOverlay
    {
        public virtual bool Hidden { get; set; }

        private List<IOverlayElement> _Elements = new List<IOverlayElement>();
        public virtual List<IOverlayElement> Elements
        {
            get { return _Elements; }
            set { _Elements = value; }
        }

        public virtual void Frame()
        {
            foreach (IOverlayElement element in Elements)
                element.Frame();
        }

        public virtual Object Clone()
        {
            return MemberwiseClone();
        }

    } // End class

} // End namespace