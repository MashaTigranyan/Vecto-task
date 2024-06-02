using System.Drawing;

namespace ImageProcessing
{
    public interface IPlugin
    {
         Image Process(Image image);
    }
}