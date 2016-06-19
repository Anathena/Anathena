using System;

namespace Anathema.Source.SystemInternals.LuaWrapper.Graphics
{
    interface IGraphicsCore
    {
        Guid CreateText(String Text, Int32 LocationX, Int32 LocationY);
        Guid CreateImage(String Path, Int32 LocationX, Int32 LocationY);
        void DestroyObject(Guid Guid);
        void ShowObject(Guid Guid);
        void HideObject(Guid Guid);

    } // End interface

} // End namespace