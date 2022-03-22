using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class UserCommentVote
    {
        [Column("UserCommentVoteId")]
        public Guid Id { get; set; }

        public DateTime CreationDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public string? VoteType { get; set; }

        [ForeignKey(nameof(User))]
        public string UserId { get; set; }
        public User? User { get; set; }

        [ForeignKey(nameof(Comment))]
        public Guid CommentId { get; set; }
        public Comment? Comment { get; set; }
    }
}
