using System;

namespace Ana.Source.LuaEngine.Graphics
{
    internal class LuaGraphicsCore : IGraphicsCore
    {
        public Guid CreateText(String Text, Int32 LocationX, Int32 LocationY) { throw new NotImplementedException(); }
        public Guid CreateImage(String Path, Int32 LocationX, Int32 LocationY) { throw new NotImplementedException(); }
        public void DestroyObject(Guid Guid) { throw new NotImplementedException(); }
        public void ShowObject(Guid Guid) { throw new NotImplementedException(); }
        public void HideObject(Guid Guid) { throw new NotImplementedException(); }

    } // End interface

} // End namespace