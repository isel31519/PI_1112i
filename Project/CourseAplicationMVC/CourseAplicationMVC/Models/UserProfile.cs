using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Profile;

namespace CourseAplicationMVC.Models
{
    public class UserProfile : ProfileBase
    {
        public string FirstName
        {
            get { return base["FirstName"].ToString(); }
            set { base["FirstName"] = value; }
        }

        public string LastName
        {
            get { return base["LastName"].ToString(); }
            set { base["LastName"] = value; }
        }

        public string Email
        {
            get { return base["Email"].ToString(); }
            set { base["Email"] = value; }
        }

        public string Roles
        {
            get { return base["Roles"].ToString(); }
            set { base["Roles"] = value; }
        }

        public string Image
        {
            get { return base["Image"].ToString(); }
            set { base["Image"] = value; }
        }

        public string Number
        {
            get { return base["Number"].ToString(); }
            set { base["Number"] = value; }
        }
    }
}