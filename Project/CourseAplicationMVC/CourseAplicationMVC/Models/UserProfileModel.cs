using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CourseAplicationMVC.Models
{
    public class UserProfileModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Image { get; set; }
    }
}