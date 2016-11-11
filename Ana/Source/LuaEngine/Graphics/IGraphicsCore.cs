namespace Ana.Source.LuaEngine.Graphics
{
    using System;

    /// <summary>
    /// Interface to interacting with graphics in an external process
    /// </summary>
    internal interface IGraphicsCore
    {
        Guid CreateText(String text, Int32 locationX, Int32 locationY);

        Guid CreateImage(String path, Int32 locationX, Int32 locationY);

        void DestroyObject(Guid guid);

        void ShowObject(Guid guid);

        void HideObject(Guid guid);
    }
    //// End interface
}
//// End namespace