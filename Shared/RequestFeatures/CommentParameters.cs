﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.RequestFeatures
{
    public class CommentParameters: RequestParameters
    {
        public CommentParameters() => OrderBy = "text";
        public DateTime MinDate { get; set; }
        public DateTime MaxDate { get; set; } = DateTime.MaxValue;

        public bool ValidAgeRange => MaxDate > MinDate;
        public string? SearchTerm { get; set; }
    }
}
