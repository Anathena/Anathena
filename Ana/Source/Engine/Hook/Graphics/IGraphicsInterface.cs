namespace Ana.Source.Engine.Hook.Graphics
{
    using System;

    internal interface IGraphicsInterface
    {
        Guid CreateText(String text, Int32 locationX, Int32 locationY);

        Guid CreateImage(String fileName, Int32 locationX, Int32 locationY);

        void DestroyObject(Guid guid);

        void ShowObject(Guid guid);

        void HideObject(Guid guid);
    }
    //// End interface
}
//// End namespace