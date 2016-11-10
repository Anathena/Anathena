namespace Ana.Source.Engine.Hook.Graphics.DirectX.Interface
{
    using Elements;
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.IO;

    /// <summary>
    /// Injected object in an external process that provides access to its DirectX library
    /// </summary>
    [Serializable]
    internal class DirextXGraphicsInterface : MarshalByRefObject, IGraphicsInterface
    {
        public DirextXGraphicsInterface(String projectDirectory)
        {
            this.ProjectDirectory = projectDirectory;
            this.TextElements = new Dictionary<Guid, TextElement>();
            this.ImageElements = new Dictionary<Guid, ImageElement>();
            this.Font = new Font("Tahoma", 16, FontStyle.Bold);
        }

        private Font Font { get; set; }

        private String ProjectDirectory { get; set; }

        private Dictionary<Guid, TextElement> TextElements { get; set; }

        private Dictionary<Guid, ImageElement> ImageElements { get; set; }

        public IEnumerable<TextElement> GetTextElements()
        {
            return this.TextElements.Values;
        }

        public IEnumerable<ImageElement> GetImageElements()
        {
            return this.ImageElements.Values;
        }

        public Guid CreateText(String text, Int32 locationX, Int32 locationY)
        {
            Guid guid = Guid.NewGuid();
            TextElement textElement = new TextElement(Font);

            textElement.Text = text;
            textElement.Color = Color.Red;
            textElement.Location = new Point(locationX, locationY);

            this.TextElements.Add(guid, textElement);

            return guid;
        }

        public Guid CreateImage(String fileName, Int32 locationX, Int32 locationY)
        {
            Guid guid = Guid.NewGuid();
            ImageElement imageElement = new ImageElement(Path.Combine(this.ProjectDirectory, fileName));

            imageElement.Location = new Point(locationX, locationY);

            this.ImageElements.Add(guid, imageElement);

            return guid;
        }

        public void DestroyObject(Guid guid)
        {
            if (this.TextElements.ContainsKey(guid))
            {
                this.TextElements.Remove(guid);
            }
            else if (this.ImageElements.ContainsKey(guid))
            {
                this.ImageElements.Remove(guid);
            }
        }

        public void ShowObject(Guid guid)
        {
            //// this.GraphicObjects[guid].Visible = true;
        }

        public void HideObject(Guid guid)
        {
            //// this.GraphicObjects[guid].Visible = false;
        }
    }
    //// End class
}
//// End namespace