using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Extensions
{
    public static class UserPostVoteExtensions
    {
        public static IQueryable<UserPostVote> FilterUserPostVotes(this IQueryable<UserPostVote> userPostVotes, DateTime minDate, DateTime maxDate) =>
            userPostVotes.Where(e => (e.CreationDate >= minDate && e.CreationDate <= maxDate));

        public static IQueryable<UserPostVote> Search(this IQueryable<UserPostVote> userPostVotes, string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
                return userPostVotes;

            var lowerCaseTerm = searchTerm.Trim().ToLower();

            return userPostVotes.Where(e => e.User.UserName.ToLower().Contains(lowerCaseTerm));
        }
    }
}
