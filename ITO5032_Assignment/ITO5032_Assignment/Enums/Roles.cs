using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ITO5032_Assignment.Enums
{
    public class Roles
    {
        private Roles(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public String Name { get; set; }
        public int Id { get; set; }
        public static Roles UNKNOWN { get { return new Roles(0, "UNKNOWN"); } }
        public static Roles ADMIN { get { return new Roles(1, "Admin"); } }
        public static Roles USER { get { return new Roles(2, "User"); } }
        public static Roles SERVICE_USER { get { return new Roles(3, "Service User"); } }
        public static Roles getRoleById(int id)
        {
            switch (id)
            {
                case 1:
                    return ADMIN;
                case 2:
                    return USER;
                case 3:
                    return SERVICE_USER;
                default:
                    return UNKNOWN;
            }
        }
    }
}