using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SmallRatings.Models
{
    public class ProInfo
    {
        public int ProID { get; set; }
        public int UserID { get; set; }

        public string AvatarFileType { get; set; }
        public string HeaderFileType { get; set; }

        [Display(Name = "Business Name")]
        [Required(ErrorMessage = "Please enter your business name.")]
        public string ProName { get; set; }

        [Display(Name = "Website")]
        [Required(ErrorMessage = "Please enter your business's website.")]
        public string Website { get; set; }

        [Display(Name = "Description")]
        [Required(ErrorMessage = "Please enter a description for your business.")]
        public string Description { get; set; }

        [Display(Name = "Location")]
        [Required(ErrorMessage = "Please enter your business's location.")]
        public string Location { get; set; }

        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email (Your Website Domain)")]
        [Required(ErrorMessage = "Please enter your business email.")]
        public string ProEmail { get; set; }

        [Display(Name = "Business Profile Picture")]
        public byte[] ProAvatar { get; set; }

        [Display(Name = "Business Header")]
        public byte[] ProHeader { get; set; }
    }
}
