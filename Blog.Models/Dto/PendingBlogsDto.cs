﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Models.Dto
{
    public class PendingBlogsDto
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public string Category { get; set; }
        public int SubscriptionsAllowed { get; set; }
        public int SubscriptionsUsed { get; set; }
    }
}
