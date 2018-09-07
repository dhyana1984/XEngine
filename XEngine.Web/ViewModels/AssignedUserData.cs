using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace XEngine.Web.ViewModels
{
    public class AssignedUserData
    {
        public int UserID { get; set; }
        public string UserName { get; set; }
        public bool Assigned { get; set; }
    }
}