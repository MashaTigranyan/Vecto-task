using System.Drawing;
using System.Drawing.Imaging;

namespace ImageProcessing
{
    public class BlurPlugin : IPlugin
    {
        private Rectangle _rectangle;
        private int _blurSize;
        
        public BlurPlugin(Rectangle rectangle, int blurSize)
        {
            _rectangle = rectangle;
            _blurSize = blurSize;
        }
        

        public Image Process(Image image)
        {
            unsafe
            {
                Bitmap blurred = new Bitmap(image.Width, image.Height);
                
                using (Graphics graphics = Graphics.FromImage(blurred))
                    graphics.DrawImage(image, new Rectangle(0, 0, image.Width, image.Height),
                        new Rectangle(0, 0, image.Width, image.Height), GraphicsUnit.Pixel);
                
                BitmapData blurredData = blurred.LockBits(new Rectangle(0, 0, image.Width, image.Height), ImageLockMode.ReadWrite, blurred.PixelFormat);
                
                int bitsPerPixel = Image.GetPixelFormatSize(blurred.PixelFormat);
                byte* scan0 = (byte*)blurredData.Scan0.ToPointer();
                
                for (int xx = _rectangle.X; xx < _rectangle.X + _rectangle.Width; xx++)
                {
                    for (int yy = _rectangle.Y; yy < _rectangle.Y + _rectangle.Height; yy++)
                    {
                        int avgR = 0, avgG = 0, avgB = 0;
                        int blurPixelCount = 0;
                        
                        for (int x = xx; (x < xx + _blurSize && x < image.Width); x++)
                        {
                            for (int y = yy; (y < yy + _blurSize && y < image.Height); y++)
                            {
                                byte* data = scan0 + y * blurredData.Stride + x * bitsPerPixel / 8;

                                avgB += data[0]; // Blue
                                avgG += data[1]; // Green
                                avgR += data[2]; // Red

                                blurPixelCount++;
                            }
                        }

                        avgR = avgR / blurPixelCount;
                        avgG = avgG / blurPixelCount;
                        avgB = avgB / blurPixelCount;
                        
                        for (int x = xx; x < xx + _blurSize && x < image.Width && x < _rectangle.Width; x++)
                        {
                            for (int y = yy; y < yy + _blurSize && y < image.Height && y < _rectangle.Height; y++)
                            {
                                byte* data = scan0 + y * blurredData.Stride + x * bitsPerPixel / 8;
                                
                                data[0] = (byte)avgB;
                                data[1] = (byte)avgG;
                                data[2] = (byte)avgR;
                            }
                        }
                    }
                }
                
                blurred.UnlockBits(blurredData);

                return (Image)blurred;
            }
        }
    }
}