using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Reflection;
using System.Text;
using Entities.Models;

namespace Repository.Extensions
{
    public static class PostRepositoryExtensions
    {
        #region Fitler by Category

        public static IQueryable<Post> FilterPostsByCategory(this IQueryable<Post>
            posts, string categoryName)
        {
            if (string.IsNullOrWhiteSpace(categoryName))
                return posts;

            return posts.Where(p => p.Category.CategoryName.Equals(categoryName));
        }

        #endregion

        #region Filter by Date

        public static IQueryable<Post> FilterPostsByDate(this IQueryable<Post>
            posts, DateTime from, DateTime to)
        {
            if (from == default)
                return posts;

            return posts.Where(p => p.PostDate >= from && p.PostDate <= to);
        }

        #endregion

        #region Search

        public static IQueryable<Post> Search(this IQueryable<Post>
            posts, string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
                return posts;


            return posts.Where(p => p.Title.ToLower().Contains(searchTerm) ||
                                    p.Body.Contains(searchTerm));
        }
        

        #endregion
        
        public static IQueryable<Post> Sort(this IQueryable<Post> posts, string
            orderByQueryString)
        {
            if (string.IsNullOrWhiteSpace(orderByQueryString))
                return posts.OrderBy(p => p.PostDate);

            var orderParams = orderByQueryString.Trim().Split(',');
            var propertyInfos = typeof(Post).GetProperties(BindingFlags.Public |
                                                           BindingFlags.Instance);
            var orderQueryBuilder = new StringBuilder();

            foreach (var param in orderParams)
            {
                if (string.IsNullOrWhiteSpace(param))
                    continue;

                var propertyFromQueryName = param.Split(" ")[0];
                var objectProperty = propertyInfos.FirstOrDefault(pi =>
                    pi.Name.Equals(propertyFromQueryName, StringComparison.InvariantCultureIgnoreCase));

                if (objectProperty == null)
                    continue;

                var direction = param.EndsWith(" desc") ? "descending" : "ascending";

                orderQueryBuilder.Append($"{objectProperty.Name.ToString()} {direction},");
            }

            var orderQuery = orderQueryBuilder.ToString().TrimEnd(',', ' ');

            if (string.IsNullOrWhiteSpace(orderQuery))
                return posts.OrderBy(p => p.PostDate);

            return posts.OrderBy(orderQuery);
        }
    }
}