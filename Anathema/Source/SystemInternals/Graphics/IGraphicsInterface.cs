using System;
using System.Diagnostics;

namespace Anathema.Source.Graphics
{
    public interface IGraphicsInterface
    {
        void Inject(Process Target);
        void Uninject();

        void DoRequest();

        void DrawLine(Int32 StartX, Int32 StartY, Int32 EndX, Int32 EndY);

    } // End interface

} // End namespace