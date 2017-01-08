namespace Ana.Source.ScriptEngine.Graphics
{
    using System;

    /// <summary>
    /// Interface to provide access to manipulating graphics in an external process.
    /// </summary>
    public interface IGraphicsCore
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