using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CourseAplicationMVC.Models
{
    public sealed class LogInModel
    {
        [Required(ErrorMessage = "Username is a required field")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Password is a required field")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

    }
}