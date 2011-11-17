using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CourseAplicationMVC.Models
{
    public class ChangePasswordModel
    {
        [Required(ErrorMessage = "Password is a required field")]
        [DataType(DataType.Password)]
        public string OldPassword { get; set; }

        [Display(Name = "New Password")]
        [Required(ErrorMessage = "Password is a required field")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Confirm New Password")]
        [Required(ErrorMessage = "You must confirm your password")]
        [Compare("Password", ErrorMessage = "Password match failed")]
        public string PassConfirm { get; set; }
    }
}