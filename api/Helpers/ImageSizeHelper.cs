using System.Drawing;
using System.IO;
using Microsoft.AspNetCore.Http;
using static Lapis.API.Helpers.CommonEnum;

namespace Lapis.API.Helpers
{
    public class ImageSizeHelper
    {
        /// <summary>
        /// Try to get image Size and orientation
        /// </summary>
        /// <param name="file"></param>
        /// <returns>3 parameters return</returns>
        public static (int height, int width, string orientation) GetImageSize(IFormFile file)
        {
            int height, width = 0;
            string imageOrientation = "";

            using (var memoryStream = new MemoryStream())
            {
                file.CopyTo(memoryStream);
                var imageFromStream = Image.FromStream(memoryStream);
                System.Drawing.Bitmap img = new System.Drawing.Bitmap(imageFromStream);

                height = img.Height;
                width = img.Width;

                imageOrientation = GetImageOrientation(height, width);
            }
            return (height, width, imageOrientation);
        }

        /// <summary>
        /// Check image orientation using height and width
        /// </summary>
        /// <param name="height"></param>
        /// <param name="width"></param>
        /// <returns>Orientation in string</returns>
        private static string GetImageOrientation(int height, int width)
        {
            if (height > width)
            {
                return ImageOrientation.Vertical.ToString();
            }
            else if (height < width)
            {
                return ImageOrientation.Horizontal.ToString();
            }
            return ImageOrientation.Square.ToString();
        }
    }
}