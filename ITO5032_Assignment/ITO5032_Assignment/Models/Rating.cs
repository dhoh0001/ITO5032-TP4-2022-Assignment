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
    
    public partial class Rating
    {
        public int id { get; set; }
        public int score { get; set; }
        public string comment { get; set; }
        public int service_provider_id { get; set; }
        public int User_id { get; set; }
        public Nullable<int> User_id1 { get; set; }
        public Nullable<int> AppUser_id { get; set; }
    
        public virtual AppUser User { get; set; }
        public virtual AppUser AppUser { get; set; }
    }
}
