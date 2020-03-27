using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using FreeImageAPI;
using SkiaSharp;


namespace TodoApi.Helpers
{
    public static class ImageHelper
    {
        public static string ResizeImage(string imagePath, string outputPath, bool makeThumbnail)
        {
            const int quality = 75;

            int size;
            if (makeThumbnail) size = 512;
            else size = 1024;

            /*using (var image = new MagickImage(imagePath))
            {
                image.Resize(size, size);
                image.Strip();
                image.Quality = quality;
                image.Write(outputPath);
            }
            */



            /* freeimage
             using (var original = FreeImageBitmap.FromFile(imagePath))
            {
                int width, height;
                if (original.Width > original.Height)
                {
                    width = size;
                    height = original.Height * size / original.Width;
                }
                else
                {
                    width = original.Width * size / original.Height;
                    height = size;
                }
                var resized = new FreeImageBitmap(original, width, height);
                // JPEG_QUALITYGOOD is 75 JPEG.
                // JPEG_BASELINE strips metadata (EXIF, etc.)
                resized.Save(outputPath, FREE_IMAGE_FORMAT.FIF_JPEG,
                    FREE_IMAGE_SAVE_FLAGS.JPEG_QUALITYGOOD |
                    FREE_IMAGE_SAVE_FLAGS.JPEG_BASELINE);
            }*/

            using (var input = File.OpenRead(imagePath))
            {
                using (var inputStream = new SKManagedStream(input))
                {
                    using (var original = SKBitmap.Decode(inputStream))
                    {
                        int width, height;
                        if (original.Width > original.Height)
                        {
                            width = size;
                            height = original.Height * size / original.Width;
                        }
                        else
                        {
                            width = original.Width * size / original.Height;
                            height = size;
                        }

                        using (var resized = original
                            .Resize(new SKImageInfo(width, height), SKBitmapResizeMethod.Lanczos3))
                        {
                            if (resized == null) return "";

                            using (var image = SKImage.FromBitmap(resized))
                            {
                                using (var output = File.OpenWrite(outputPath))
                                {
                                    image.Encode(SKEncodedImageFormat.Jpeg, quality)
                                        .SaveTo(output);
                                    image.Encode(SKEncodedImageFormat.Jpeg, quality)
                                        .SaveTo(output);
                                }
                            }
                        }
                    }
                }
            }

            return outputPath;
        }
    }
}
