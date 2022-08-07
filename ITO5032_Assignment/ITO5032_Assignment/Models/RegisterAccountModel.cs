using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ITO5032_Assignment.Models
{
    public class RegisterAccountModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        [Required]
        [Display(Name = "First name")]
        public string first_name { get; set; }

        [Required]
        [Display(Name = "Last name")]
        public string last_name { get; set; }

        [Required]
        [Display(Name = "Date of Birth")]
        public string date_of_birth { get; set; }

        [Required]
        [Display(Name = "Address line 1")]
        public string address1 { get; set; }

        [Required]
        [Display(Name = "Address line 2")]
        public string address2 { get; set; }
    }
}