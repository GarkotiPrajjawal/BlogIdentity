﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Models.Dto
{
    public class BlogsDto
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public string Category { get; set; }
        public int SubscriptionsAllowed { get; set; }
        public int SubscriptionsUsed { get; set; }
        public String Status { get; set; }
    }
}
