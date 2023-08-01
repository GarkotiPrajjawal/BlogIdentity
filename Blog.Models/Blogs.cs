using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Models
{
    public class Blogs
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [StringLength(80)]
        public string Title { get; set; }
        public string Content { get; set; }
        [Required]
        [StringLength(80)]
        public string Category { get; set; }
        [Required]
        [Range(1, 200)]
        public int SubscriptionsAllowed { get; set; }
        [Range(1, 200)]
        public int SubscriptionsUsed { get; set; }
        [Required]
        public String Status { get; set; }
    }
}
