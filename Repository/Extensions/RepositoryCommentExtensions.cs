using Entities.Models;
using Repository.Extensions.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Extensions
{
    public static class RepositoryCommentExtensions
    {
        public static IQueryable<Comment> FilterComments(this IQueryable<Comment> comments, DateTime minDate, DateTime maxDate) =>
            comments.Where(e => (e.CreationDate >= minDate && e.CreationDate <= maxDate));

        public static IQueryable<Comment> Search(this IQueryable<Comment> comments, string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
                return comments;

            var lowerCaseTerm = searchTerm.Trim().ToLower();

            return comments.Where(e => e.Text.ToLower().Contains(lowerCaseTerm));
        }

        /*public static IQueryable<Comment> Sort(this IQueryable<Comment> comments, string orderByQueryString)
        {
            if (string.IsNullOrWhiteSpace(orderByQueryString))
                return comments.OrderBy(e => e.Text);

            var orderQuery = OrderQueryBuilder.CreateOrderQuery<Comment>(orderByQueryString);

            if (string.IsNullOrWhiteSpace(orderQuery))
                return comments.OrderBy(e => e.Text);

            return comments.OrderBy(orderQuery);
        }*/

    }
}

