using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DataTransferObjects
{
    public abstract record UserPostVoteForManipulation
    {
        public DateTime CreationDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public string? VoteType { get; set; }
    }
}
