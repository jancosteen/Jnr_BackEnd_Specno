using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DataTransferObjects
{
    public record  UserPostVoteDto
    {
        /*public Guid Id { get; set; }

        public DateTime CreationDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public string? VoteType { get; set; }

        
        public string? UserId { get; set; }
        public UserDto? User { get; set; }

        
        public Guid PostId { get; set; }*/
        public PostDto? Post { get; set; }
    }
}
