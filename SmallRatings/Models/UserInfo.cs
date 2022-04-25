using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SmallRatings.Models
{
    public class UserInfo
    {
        public int UserID { get; set; }
        public string AvatarFileType { get; set; }

        [Display(Name = "First Name")]
        [Required(ErrorMessage = "Please enter your first name.")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        [Required(ErrorMessage = "Please enter your last name.")]
        public string LastName { get; set; }

        [Display(Name = "Username")]
        [Required(ErrorMessage = "Please enter a username.")]
        public string Username { get; set; }

        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email")]
        [Required(ErrorMessage = "Please enter your email.")]
        public string Email { get; set; }

        [Display(Name = "Location")]
        [Required(ErrorMessage = "Please enter the location.")]
        public string Location { get; set; }

        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$")]
        [Display(Name = "Phone Number")]
        [Required(ErrorMessage = "Please enter a phone number.")]
        public int Number { get; set; }

        [Display(Name = "Profile Picture")]
        public byte[] Avatar { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        [Required(ErrorMessage = "Please enter a password.")]
        public string Password { get; set; }
    }
}
