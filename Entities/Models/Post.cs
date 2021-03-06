using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class Post
    {
        [Column("PostId")]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Title is required")]
        [MaxLength(60, ErrorMessage = "Max length is 60 caracters")]
        public string? Title { get; set; }

        [Required(ErrorMessage ="Body is required")]
        public string? Body { get; set; }
        public int UpvoteCount { get; set; }
        public int DownvoteCount { get; set; }
        
        public DateTime CreationDate { get; set; }
        public DateTime? UpdateDate { get; set; }

        [ForeignKey(nameof(User))]
        public string UserId { get; set; }
        public User? User { get; set; }

        public ICollection<Comment>? Comments { get; set; }
        public ICollection<UserPostVote>? User_Post_Vote { get; set; }
    }
}
