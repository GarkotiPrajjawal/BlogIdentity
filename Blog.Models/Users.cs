using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using Microsoft.AspNetCore.Identity;

namespace Blog.Models
{
    public class Users//: IdentityUser
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [StringLength(80)]
        public string Name { get; set; }
        [Required]
        [StringLength(80)]
        public string Email { get; set; }
        [Required]
        [StringLength(80)]
        public string Password { get; set; }
        [Required]
        public string Role { get; set; }
        [Range(1, 100)]
        public List<int>? blogsubscribed { get; set; }
    }
}
