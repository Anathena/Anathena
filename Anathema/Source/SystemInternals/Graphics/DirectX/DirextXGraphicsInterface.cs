using System;

namespace Anathema.Source.SystemInternals.Graphics.DirectX.Interface
{
    [Serializable]
    public class DirextXGraphicsInterface : MarshalByRefObject, IGraphicsInterface
    {
        /// <summary>
        /// The client process Id
        /// </summary>
        public Int32 ProcessId { get; set; }

        public String ProjectDirectory { get; set; }

        public DirextXGraphicsInterface() { }

        /// <summary>
        /// Empty method to ensure we can call the client without crashing
        /// </summary>
        public void Ping() { }

        Guid IGraphicsInterface.CreateText(String Text, Int32 LocationX, Int32 LocationY)
        {
            return new Guid();
        }

        Guid IGraphicsInterface.CreateImage(String Path, Int32 LocationX, Int32 LocationY)
        {
            return new Guid();
        }

        public void ShowObject(Guid Guid)
        {
            throw new NotImplementedException();
        }

        public void HideObject(Guid Guid)
        {
            throw new NotImplementedException();
        }

    } // End class

} // End namespace