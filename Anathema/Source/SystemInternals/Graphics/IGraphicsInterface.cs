using System;

namespace Anathema.Source.Graphics
{
    public interface IGraphicsInterface
    {
        Guid CreateText(String Text, Int32 LocationX, Int32 LocationY);
        Guid CreateImage(String Path, Int32 LocationX, Int32 LocationY);

        void ShowObject(Guid Guid);

        void HideObject(Guid Guid);

        void Disconnect();

    } // End interface

} // End namespace