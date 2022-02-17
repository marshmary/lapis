using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Lapis.API.Helpers
{
    public interface IImgBBHelper
    {
        Task<(string thumbnail, string medium, string high)> Upload(Stream stream, string fileName, long fileLength, string fileType);
    }

    public class ImgBBHelper : IImgBBHelper
    {
        private readonly string apiUrl;

        public ImgBBHelper(string key)
        {
            this.apiUrl = $"https://api.imgbb.com/1/upload?key={key}";
        }

        /// <summary>
        /// Upload image to ImgBB then get return thumbnail, medium and high url
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="fileName"></param>
        /// <param name="fileLength"></param>
        /// <param name="fileType"></param>
        /// <returns>thumbnail, medium and high</returns>
        public async Task<(string thumbnail, string medium, string high)> Upload(Stream stream, string fileName, long fileLength, string fileType)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(this.apiUrl);

                using (var content = new MultipartFormDataContent())
                {
                    // Fetch data
                    content.Add(new StreamContent(stream)
                    {
                        Headers =
                    {
                        ContentLength = fileLength,
                        ContentType = new MediaTypeHeaderValue(fileType)
                    }
                    }, "image", fileName);

                    var response = await client.PostAsync(this.apiUrl, content);
                    response.EnsureSuccessStatusCode();

                    // Convert from JSON to object
                    string responseBody = await response.Content.ReadAsStringAsync();
                    dynamic dec = JsonConvert.DeserializeObject(responseBody);

                    // Get return data
                    string url = dec.data.url;
                    string thumbnail = dec.data.thumb.url;
                    string medium = dec.data.medium.url;
                    return (thumbnail, medium, url);
                }
            }
        }
    }
}