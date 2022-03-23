using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DataTransferObjects
{
    public abstract record CommentForManipulation
    {

        [Required(ErrorMessage = "Comment text is required")]
        public string? Text { get; set; }
        public int UpvoteCount { get; set; } = 0;
        public int DownvoteCount { get; set; } = 0;
        public DateTime CreationDate { get; set; }
        public DateTime? UpdateDate { get; set; }
    }
}
