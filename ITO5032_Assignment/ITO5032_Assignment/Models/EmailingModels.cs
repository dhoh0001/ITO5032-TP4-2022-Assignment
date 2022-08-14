using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ITO5032_Assignment.Models
{
    public class EmailingModel
    {
        [Required]
        [EmailAddress]
        public string From
        {
            get;
            set;
        }

        [Required]
        [EmailAddress]
        public string To
        {
            get;
            set;
        }

        [Required]
        public string Subject
        {
            get;
            set;
        }

        [Required]
        public string Body
        {
            get;
            set;
        }
    }
}