using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Linq;
using ImageProcessing;

namespace ImageProcessing
{
    public class ImageProcessor
    {
        public IList<IPlugin> plugins = new Collection<IPlugin>();
        private Image _image;
        
        public ImageProcessor(string path)
        {
            _image = Image.FromFile(path);
        }

        public void AddPlugin(IPlugin plugin)
        {
            plugins.Add(plugin);
        }

        public Image Build()
        {
            Image processedImage = _image;
            
            foreach (var plugin in plugins)
            {
                processedImage = plugin.Process(processedImage);
            }

            return processedImage;
        }
    }
}