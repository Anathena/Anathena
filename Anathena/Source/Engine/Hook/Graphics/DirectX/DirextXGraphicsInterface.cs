using Anathena.Source.Engine.Hook.Graphics.DirectX.Interface.Elements;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;

namespace Anathena.Source.Engine.Hook.Graphics.DirectX.Interface
{
    [Serializable]
    public class DirextXGraphicsInterface : MarshalByRefObject, IGraphicsInterface
    {
        private String ProjectDirectory { get; set; }

        private Dictionary<Guid, TextElement> TextElements;
        private Dictionary<Guid, ImageElement> ImageElements;

        private Font Font;

        public DirextXGraphicsInterface(String ProjectDirectory)
        {
            this.ProjectDirectory = ProjectDirectory;

            TextElements = new Dictionary<Guid, TextElement>();
            ImageElements = new Dictionary<Guid, ImageElement>();

            Font = new Font("Tahoma", 16, FontStyle.Bold);
        }

        public IEnumerable<TextElement> GetTextElements()
        {
            return TextElements.Values;
        }

        public IEnumerable<ImageElement> GetImageElements()
        {
            return ImageElements.Values;
        }

        public Guid CreateText(String Text, Int32 LocationX, Int32 LocationY)
        {
            Guid Guid = Guid.NewGuid();
            TextElement TextElement = new TextElement(Font);

            TextElement.Text = Text;
            TextElement.Color = Color.Red;
            TextElement.Location = new Point(LocationX, LocationY);

            TextElements.Add(Guid, TextElement);

            return Guid;
        }

        public Guid CreateImage(String FileName, Int32 LocationX, Int32 LocationY)
        {
            Guid Guid = Guid.NewGuid();
            ImageElement ImageElement = new ImageElement(Path.Combine(ProjectDirectory, FileName));

            ImageElement.Location = new Point(LocationX, LocationY);

            ImageElements.Add(Guid, ImageElement);

            return Guid;
        }

        public void DestroyObject(Guid Guid)
        {
            if (TextElements.ContainsKey(Guid))
                TextElements.Remove(Guid);

            if (ImageElements.ContainsKey(Guid))
                ImageElements.Remove(Guid);
        }

        public void ShowObject(Guid Guid)
        {
            // GraphicObjects[Guid].Visible = true;
        }

        public void HideObject(Guid Guid)
        {
            // GraphicObjects[Guid].Visible = false;
        }

    } // End class

} // End namespace