using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace XEngine.Web.ViewModels
{
    public class AssignedRoleData
    {
        public int RoleID { get; set; }
        public string RoleName { get; set; }
        public bool Assigned { get; set; }
    }
}