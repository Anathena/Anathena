namespace Ana.Source.Utils
{
    using System;
    using System.Windows.Media.Imaging;

    public static class ImageLoader
    {
        public static BitmapImage LoadImage(String contentPath)
        {
            BitmapImage BitmapImage = new BitmapImage();
            BitmapImage.BeginInit();
            BitmapImage.UriSource = new Uri(contentPath);
            BitmapImage.EndInit();
            return BitmapImage;
        }
    }
    //// End class
}
//// End namespace