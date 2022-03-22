using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Exceptions
{
    public sealed class CommentNotFoundException : NotFoundException
    {
        public CommentNotFoundException(Guid commentId) : base($"Comment with id: {commentId}, does not exist in the database.")
        {
        }
    }
}
