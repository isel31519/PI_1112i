using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Profile;

namespace CourseAplicationMVC.Models
{
    public class UserProfile:ProfileBase
    {
        public string FirstName{ get;set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Image { get; set; }
        public int Number { get; set; }
    }

}