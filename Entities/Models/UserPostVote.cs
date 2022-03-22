using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class UserPostVote
    {
        [Column("UserPostVoteId")]
        public Guid Id { get; set; }

        public DateTime CreationDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public string? VoteType { get; set; }

        [ForeignKey(nameof(User))]
        public Guid UserId { get; set; }
        public User? User { get; set; }

        [ForeignKey(nameof(Post))]
        public Guid PostId { get; set; }
        public Post? Post { get; set; }

    }
}
