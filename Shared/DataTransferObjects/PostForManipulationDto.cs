using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DataTransferObjects
{
    public abstract record PostForManipulationDto
    {
        [Required(ErrorMessage = "Title is required")]
        [MaxLength(60, ErrorMessage = "Max length is 60 caracters")]
        public string? Title { get; init; }

        [Required(ErrorMessage = "Body is required")]
        public string? Body { get; init; }
        public int UpvoteCount { get; init; }
        public int DownvoteCount { get; init; }

        public DateTime CreationDate { get; set; }
        public DateTime? UpdateDate { get; set; }
    }
}
