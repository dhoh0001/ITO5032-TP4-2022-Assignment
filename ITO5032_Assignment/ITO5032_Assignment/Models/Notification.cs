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
    
    public partial class Notification
    {
        public int id { get; set; }
        public string message { get; set; }
        public System.DateTime notification_datetime { get; set; }
        public int User_id { get; set; }
        public Nullable<int> User_id1 { get; set; }
    
        public virtual AppUser User { get; set; }
        public virtual AppUser AppUser { get; set; }
    }
}
