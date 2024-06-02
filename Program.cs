using System.Drawing;
using System.Drawing.Imaging;

namespace ImageProcessing
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            var imageProcessor = new ImageProcessor("C:\\Users\\masht\\RiderProjects\\ImageProcessing\\App\\free-images.jpg");

            /* Blur */
            var blurPlugin = new BlurPlugin(new Rectangle(0, 0, 500, 500), 10);
            
            /* Scale */
            var scalePlugin = new ScalePlugin(100, 100);
            
            imageProcessor.AddPlugin(blurPlugin);
            imageProcessor.AddPlugin(scalePlugin);  
           

            var img = imageProcessor.Build();
            
            img.Save("new_file.jpeg", ImageFormat.Jpeg);
        }
    }
}