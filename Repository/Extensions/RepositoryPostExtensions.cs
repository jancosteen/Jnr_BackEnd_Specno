using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Extensions
{
    public static class RepositoryPostExtensions
    {
        public static IQueryable<Post> FilterPosts(this IQueryable<Post> posts, DateTime minDate, DateTime maxDate) =>
            posts.Where(e => (e.CreationDate >= minDate && e.CreationDate <= maxDate));

        public static IQueryable<Post> Search(this IQueryable<Post> posts, string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
                return posts;

            var lowerCaseTerm = searchTerm.Trim().ToLower();

            return posts.Where(e => e.Title.ToLower().Contains(lowerCaseTerm));
        }
    }
}
