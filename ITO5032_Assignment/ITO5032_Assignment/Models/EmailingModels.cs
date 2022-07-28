using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ITO5032_Assignment.Models
{
    public class EmailingModel
    {
        public string From
        {
            get;
            set;
        }
        public string To
        {
            get;
            set;
        }
        public string Subject
        {
            get;
            set;
        }
        public string Body
        {
            get;
            set;
        }
    }
}