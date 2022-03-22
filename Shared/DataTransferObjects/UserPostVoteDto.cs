using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DataTransferObjects
{
    public record  UserPostVoteDto
    {
        public Guid Id { get; set; }

        public DateTime CreationDateTime { get; set; }
        public DateTime? UpdateDateTime { get; set; }
        public string? VoteType { get; set; }
    }
}
