using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Models
{
    public static class SD
    {
        public const string Role_Customer = "Customer";
        public const string Role_Company = "Company";
        public const string Role_Admin = "Admin";
        public const string Role_Employee = "Employee";
        public enum ApiType
        {
            GET,
            POST,
            PUT, 
            DELETE
        }
        public static String SessionToken = "JWTTOKEN";
    }
}
