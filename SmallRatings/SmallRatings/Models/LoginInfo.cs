using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SmallRatings.Models
{
    public class LoginInfo
    {
        [Display(Name = "Username")]
        [Required(ErrorMessage = "Please enter the company name.")]
        public string Username { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        [Required(ErrorMessage = "Please enter a password.")]
        public string Password { get; set; }
    }
}
