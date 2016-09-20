namespace Ana.Source.Utils
{
    using System;
    using System.Windows.Media.Imaging;

    /// <summary>
    /// Static class for loading images from the project content
    /// </summary>
    public static class ImageLoader
    {
        /// <summary>
        /// Loads an image from the given uri
        /// </summary>
        /// <param name="uri">The uri specifying from where to load the image</param>
        /// <returns>The bitmap image loaded from the given uri</returns>
        public static BitmapImage LoadImage(String uri)
        {
            BitmapImage bitmapImage = new BitmapImage();

            bitmapImage.BeginInit();
            bitmapImage.UriSource = new Uri(uri);
            bitmapImage.EndInit();

            return bitmapImage;
        }
    }
    //// End class
}
//// End namespace