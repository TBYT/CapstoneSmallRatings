﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SmallRatings.Models
{
    public class UserInfo
    {
        public int UserID { get; set; }

        [Display(Name = "First Name")]
        [Required(ErrorMessage = "Please enter your first and last name.")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        [Required(ErrorMessage = "Please enter your last name.")]
        public string LastName { get; set; }

        [Display(Name = "Username")]
        [Required(ErrorMessage = "Please enter the company name.")]
        public string Username { get; set; }

        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email")]
        [Required(ErrorMessage = "Please enter your email.")]
        public string Email { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        [Required(ErrorMessage = "Please enter a password.")]
        public string Password { get; set; }
    }
}
