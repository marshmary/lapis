using System;
using System.IO;
using Microsoft.AspNetCore.Http;

namespace Lapis.API.Helpers
{
    public class FileExtensionCheckHelper
    {
        /// <summary>
        /// Check a file is has .png or .jpeg or .jpg or not
        /// </summary>
        /// <param name="file"></param>
        /// <returns>true / false + error_message</returns>
        public static (bool, string) CheckImageExtension(IFormFile file) => CheckFileExtension(file,
            new[] {
                ".jpeg", ".png", ".jpg", ".jfif"
            },
            new[] {
                "image/jpeg", "image/png", "image/pjpeg"
            }
        );

        /// <summary>
        /// Validate the file extension
        /// </summary>
        /// <param name="file">the file</param>
        /// <param name="fileTypes">the extension types</param>
        /// <param name="mimeTypes">the mime types</param>
        /// <returns>a tuple contains the validate status (boolean) and the message</returns>
        public static (bool, string) CheckFileExtension(IFormFile file, string[] fileTypes, string[] mimeTypes)
        {
            if (file == null || file.Length <= 0)
            {
                return (false, "No upload file");
            }

            bool success;

            // Check file extension
            var fileType = Path.GetExtension(file.FileName);
            success = false;
            foreach (var type in fileTypes)
            {
                if (fileType.Equals(type, StringComparison.OrdinalIgnoreCase))
                {
                    success = true;
                    break;
                }
            }
            if (!success)
            {
                return (false, "Not support file extension");
            }

            // Check MIME type
            Stream fs = file.OpenReadStream();
            BinaryReader br = new BinaryReader(fs);
            byte[] bytes = br.ReadBytes((Int32)fs.Length);
            var mimeType = HeyRed.Mime.MimeGuesser.GuessMimeType(bytes);
            success = false;
            foreach (var type in mimeTypes)
            {
                if (mimeType.Equals(type))
                {
                    success = true;
                    break;
                }
            }
            if (!success)
            {
                return (false, "Not support fake extension");
            }

            return (true, "Valid image extension");
        }
    }
}