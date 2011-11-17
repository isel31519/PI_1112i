using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CourseAplicationMVC.Models
{
    public sealed class RegisterUserModel
    {

        [Required(ErrorMessage = "Username is a required field")]
        public string Username { get; set; }

        [Display(Name = "Teacher Number")]
        [Required(ErrorMessage = "Number is a required field")]
        public int Number { get; set; }

        [Required(ErrorMessage = "Password is a required field")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Confirm Password")]
        [Required(ErrorMessage = "You must confirm your password")]
        [Compare("Password",ErrorMessage = "Password match failed")]
        public string PassConfirm { get; set; }

        [Required(ErrorMessage = "Email is a required field")]
        public string Email { get; set; }


    }
}