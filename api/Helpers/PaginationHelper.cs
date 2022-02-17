using System.Collections.Generic;
using System.Linq;
using Lapis.API.Dtos;
using MongoDB.Driver;

namespace Lapis.API.Helpers
{
    public static class PaginationHelper
    {
        /// <summary>
        /// Get the entities from the pagination list
        /// </summary>
        /// <param name="entities"></param>
        /// <param name="paginationParameters"></param>
        /// <typeparam name="TEntity"></typeparam>
        /// <returns>the entities in range of pagination</returns>
        public static IEnumerable<TEntity> GetPage<TEntity>(this IEnumerable<TEntity> entities, PaginationParameters paginationParameters)
        {
            return entities
                .Skip((paginationParameters.PageNumber - 1) * paginationParameters.PageSize)
                .Take(paginationParameters.PageSize);
        }

        /// <summary>
        /// Count the list of entities then out to an count parameter
        /// </summary>
        /// <param name="entities"></param>
        /// <param name="count"></param>
        /// <typeparam name="TEntity"></typeparam>
        /// <returns>the entities</returns>
        public static IEnumerable<TEntity> GetCount<TEntity>(this IEnumerable<TEntity> entities, out int count)
        {
            count = entities.Count();
            return entities;
        }

        /// <summary>
        /// Get the entities from the pagination list
        /// </summary>
        /// <param name="entities"></param>
        /// <param name="paginationParameters"></param>
        /// <typeparam name="TEntity"></typeparam>
        /// <returns>the entities in range of pagination</returns>
        public static IQueryable<TEntity> GetPage<TEntity>(this IQueryable<TEntity> entities, PaginationParameters paginationParameters)
        {
            return entities
                .Skip((paginationParameters.PageNumber - 1) * paginationParameters.PageSize)
                .Take(paginationParameters.PageSize);
        }

        /// <summary>
        /// Count the list of entities then out to an count parameter
        /// </summary>
        /// <param name="entities"></param>
        /// <param name="count"></param>
        /// <typeparam name="TEntity"></typeparam>
        /// <returns>the entities</returns>
        public static IQueryable<TEntity> GetCount<TEntity>(this IQueryable<TEntity> entities, out int count)
        {
            count = entities.Count();
            return entities;
        }

        /// <summary>
        /// Get the entities from the pagination list
        /// </summary>
        /// <param name="entities"></param>
        /// <param name="paginationParameters"></param>
        /// <typeparam name="TEntity"></typeparam>
        /// <returns>the entities in range of pagination</returns>
        public static IFindFluent<TEntity, TEntity> GetPage<TEntity>(this IFindFluent<TEntity, TEntity> entities, PaginationParameters paginationParameters)
        {
            return entities
                .Skip((paginationParameters.PageNumber - 1) * paginationParameters.PageSize)
                .Limit(paginationParameters.PageSize);
        }

        // /// <summary>
        // /// Count the list of entities then out to an count parameter
        // /// </summary>
        // /// <param name="entities"></param>
        // /// <param name="count"></param>
        // /// <typeparam name="TEntity"></typeparam>
        // /// <returns>the entities</returns>
        // public static IFindFluent<TEntity, TEntity> GetCount<TEntity>(this IFindFluent<TEntity, TEntity> entities, FilterDefinition<TEntity> filter,out int count)
        // {
        //     // _images.CountDocumentsAsync(filter);
        //     count = IMongoCollection<TEntity>;
        //     return entities;
        // }
    }
}