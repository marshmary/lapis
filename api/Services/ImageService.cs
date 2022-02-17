using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Lapis.API.Helpers;
using Lapis.API.Dtos;
using Lapis.API.Models;
using MongoDB.Driver;
using Microsoft.EntityFrameworkCore;

namespace Lapis.API.Services
{
    public interface IImageService
    {
        Task<Image> Create(Image image);
        Task<(long, IEnumerable<Image>)> GetAll(PaginationParameters paginationParameters);
        Task<Image> GetById(string id);
        Task Update(string id, Image image);
        Task<bool> Delete(string id);
    }

    public class ImageService : IImageService
    {
        private readonly IMongoCollection<Image> _images;
        private string IMAGES_COLLECTION = "Images";

        public ImageService(IDatabaseSettings settings)
        {
            var client = new MongoClient(settings.GetConnectionString());
            var database = client.GetDatabase(settings.DatabaseName);
            _images = database.GetCollection<Image>(IMAGES_COLLECTION);
        }

        /// <summary>
        /// Get new image details from user then upload to MongoDB
        /// </summary>
        /// <param name="image">Image detail</param>
        /// <returns>image with id</returns>
        public async Task<Image> Create(Image image)
        {
            if (image == null)
            {
                throw new ArgumentNullException(nameof(image));
            }
            await _images.InsertOneAsync(image);
            return image;
        }

        /// <summary>
        /// Get image with a pagination filter
        /// </summary>
        /// <param name="paginationParameters"></param>
        /// <returns></returns>
        public async Task<(long, IEnumerable<Image>)> GetAll(PaginationParameters paginationParameters)
        {
            // Build filters for condition in pagination
            var builder = Builders<Image>.Filter;

            var filter = builder.Eq(img => img.IsDeleted, false);

            // Orientaton filter
            if (paginationParameters.Orientation == null || paginationParameters.Orientation != "")
            {
                filter = filter & builder.Eq(img => img.Orientation, paginationParameters.Orientation);
            }

            // Tag filter
            if (paginationParameters.Tags.Count != 0)
            {
                filter = filter & builder.All(img => img.Tags, paginationParameters.Tags);
            }

            // Color filter
            if (paginationParameters.PrimaryColor == null || paginationParameters.PrimaryColor != "")
            {
                filter = filter & builder.Eq(img => img.Colors.Primary, paginationParameters.PrimaryColor);
            }

            if (paginationParameters.SecondaryColor == null || paginationParameters.SecondaryColor != "")
            {
                filter = filter & builder.Eq(img => img.Colors.Secondary, paginationParameters.SecondaryColor);
            }

            if (paginationParameters.TertiaryColor == null || paginationParameters.TertiaryColor != "")
            {
                filter = filter & builder.Eq(img => img.Colors.Tertiary, paginationParameters.TertiaryColor);
            }

            // Get number of record match with conditions
            var totalRecord = await _images.CountDocumentsAsync(filter);

            // Get list of objects match with conditions
            var images = await _images.Find(filter).SortByDescending(img => img.Created)
                .GetPage(paginationParameters)
                .ToListAsync();

            return (totalRecord, images);
        }

        /// <summary>
        /// Get an image by id
        /// </summary>
        /// <param name="id">id of image</param>
        /// <returns>image</returns>
        public async Task<Image> GetById(string id)
        {
            return await _images.Find<Image>(s => s.Id == id && s.IsDeleted == false).FirstOrDefaultAsync();
        }

        /// <summary>
        /// Update image details from input user
        /// </summary>
        /// <param name="id">id of image</param>
        /// <param name="image">image details</param>
        /// <returns>No return</returns>
        public async Task Update(string id, Image image)
        {
            image.Id = id;
            var rs = await _images.ReplaceOneAsync(s => s.Id == id, image);
        }

        /// <summary>
        /// Set IsDelete of image to true
        /// </summary>
        /// <param name="id">id of image</param>
        /// <returns>true or false</returns>
        public async Task<bool> Delete(string id)
        {
            var deleteImage = await _images.Find<Image>(s => s.Id == id && s.IsDeleted == true).FirstOrDefaultAsync();

            if (deleteImage == null)
            {
                return false;
            }

            deleteImage.IsDeleted = true;
            deleteImage.Deleted = DateTime.Now;
            var rs = await _images.ReplaceOneAsync(s => s.Id == id, deleteImage);

            return true;
        }
    }
}