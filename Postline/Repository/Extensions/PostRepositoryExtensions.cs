using System;
using System.Linq;
using System.Text.RegularExpressions;
using Entities.Models;

namespace Repository.Extensions
{
    public static class PostRepositoryExtensions
    {
        public static IQueryable<Post> FilterPostsByCategory(this IQueryable<Post>
            posts, string categoryName)
        {
            if (string.IsNullOrWhiteSpace(categoryName))
                return posts;

            return posts.Where(p => p.Category.CategoryName.Equals(categoryName));
        }

        public static IQueryable<Post> FilterPostsByDate(this IQueryable<Post>
            posts, DateTime from, DateTime to)
        {
            if (from == default)
                return posts;

            return posts.Where(p => p.PostDate >= from && p.PostDate <= to);
        }


        //Todo check this function
        public static IQueryable<Post> Search(this IQueryable<Post>
            posts, string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
                return posts;


            return posts.Where(p => Regex.IsMatch(p.Title, searchTerm, RegexOptions.IgnoreCase) ||
                                    Regex.IsMatch(p.Body, searchTerm, RegexOptions.IgnoreCase));
        }
    }
}