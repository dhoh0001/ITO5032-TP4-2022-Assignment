//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ITO5032_Assignment.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class Rating
    {
        [Required]
        public int id { get; set; }
        [Required]
        [Display(Name = "Score")]
        public int score { get; set; }
        [Required]
        [Display(Name = "Comment")]
        public string comment { get; set; }
        [Required]
        [Display(Name = "Service Provider")]
        public int service_provider_id { get; set; }
        [Required]
        [Display(Name = "User")]
        public int User_id { get; set; }
    
        public virtual AppUser User { get; set; }
    }
}
