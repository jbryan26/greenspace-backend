using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ImageMagick;

namespace TodoApi.Helpers
{
    public static class ImageHelper
    {
        public static string ResizeImage(FileInfo imagePath, string outputPath, bool makeThumbnail)
        {
            const int quality = 75;

            int size;
            if (makeThumbnail) size = 80;
            else size = 800;

            using (var image = new MagickImage(imagePath))
            {
                image.Resize(size, size);
                image.Strip();
                image.Quality = quality;
                image.Write(outputPath);
            }

            return outputPath;
        }
    }
}
