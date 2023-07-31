using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Models
{
    public class PendingBlogs
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        public string Content { get; set; }
        [Required]
        public string Category { get; set; }
        [Required]
        public int SubscriptionsAllowed { get; set; }
        public int SubscriptionsUsed { get; set; }
    }
}
