using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class User_Post_Vote
    {
        [Column("UserPostVoteId")]
        public Guid Id { get; set; }

        public DateTime CreationDateTime { get; set; }
        public DateTime? UpdateDateTime { get; set; }
        public string? VoteType { get; set; }

        [ForeignKey(nameof(User))]
        public Guid UserId { get; set; }
        public User? User { get; set; }

        [ForeignKey(nameof(Post))]
        public Guid PostId { get; set; }
        public User? Post { get; set; }

    }
}
