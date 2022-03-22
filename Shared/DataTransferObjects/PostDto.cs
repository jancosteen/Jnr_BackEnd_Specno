﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DataTransferObjects
{
    public record PostDto
    {
        public Guid Id { get; set; }
        public string? Title { get; set; }
        public string? Body { get; set; }
        public int UpvoteCount { get; set; } = 0;
        public int DownvoteCount { get; set; } = 0;

        public DateTime CreationDate { get; set; }
        public DateTime? UpdateDate { get; set; }
    }
}
