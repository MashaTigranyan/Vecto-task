using System.Drawing;

namespace ImageProcessing
{
    public class ScalePlugin : IPlugin
    {
        private int _width;
        private int _height;

        public ScalePlugin(int width, int height)
        {
            _width = width;
            _height = height;
        }
        
        private static Image resizeImage(Image imgToResize, Size size)
        {
            return (Image)(new Bitmap(imgToResize, size));
        }

        public Image Process(Image image)
        {
            return resizeImage(image, new Size(_width, _height));
        }
        

    }
}