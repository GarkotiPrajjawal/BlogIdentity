﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Models.Dto
{
    public class UsersDto
    {
        public UsersDto()
        {
            // Initialize the blogsubscribed list in the constructor
            blogsubscribed = new List<int>();
        }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public List<int> blogsubscribed { get; set; }
    }
}
