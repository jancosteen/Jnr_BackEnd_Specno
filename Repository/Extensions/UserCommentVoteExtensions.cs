using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Extensions
{
    public static class UserCommentVoteExtensions
    {
        public static IQueryable<UserCommentVote> FilterUserCommentVotes(this IQueryable<UserCommentVote> userCommentVotes, DateTime minDate, DateTime maxDate) =>
            userCommentVotes.Where(e => (e.CreationDate >= minDate && e.CreationDate <= maxDate));

        public static IQueryable<UserCommentVote> Search(this IQueryable<UserCommentVote> userCommentVotes, string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
                return userCommentVotes;

            var lowerCaseTerm = searchTerm.Trim().ToLower();

            return userCommentVotes.Where(e => e.User.UserName.ToLower().Contains(lowerCaseTerm));
        }
    }
}

