using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace Lapis.API.Dtos
{
    #region ImageCreateDto

    public class ImageCreateDto
    {
        [Required]
        public IFormFile Image { get; set; }
        [Required]
        public HashSet<string> Tags { get; set; }
        [Required]
        public ImageCreateColor Colors { get; set; }
        [Required]
        public ImageCreateCredit Credit { get; set; }
    }

    public class ImageCreateColor
    {
        [Required]
        public string Primary { get; set; }
        [Required]
        public string Secondary { get; set; }
        [Required]
        public string Tertiary { get; set; }
    }

    public class ImageCreateCredit
    {
        [Required]
        public string Author { get; set; }
        [Required]
        public string SourceUrl { get; set; }
    }

    #endregion

    #region ImageReadDto

    public class ImageReadDto
    {
        public string Id { get; set; }
        public string Thumbnail { get; set; }
        public string Medium { get; set; }
        public string Hight { get; set; }
        public HashSet<string> Tags { get; set; }
        public string Orientation { get; set; }
        public ImageReadSize Size { get; set; }
        public ImageReadCredit Credit { get; set; }
        public DateTime Created { get; set; }
    }

    public class ImageReadSize
    {
        public int Height { get; set; }
        public int Width { get; set; }
    }

    public class ImageReadCredit
    {
        public string Author { get; set; }
        public string SourceUrl { get; set; }
    }

    #endregion

    #region ImageUpdateDto

    public class ImageUpdateDto
    {
        [Required]
        public HashSet<string> Tags { get; set; }
    }

    #endregion
}