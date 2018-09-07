using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace XEngine.Web.Models
{
    public class SysRole
    {
        public int ID { get; set; }

        [DisplayName("角色名")]
        public string Name { get; set; }

        [DisplayName("显示名")]
        public string DisplayName { get; set; }

        [DisplayName("描述")]
        public string Description { get; set; }

        [DisplayName("启用")]
        public string IsActive { get; set; }

        [DisplayName("修改日期")]
        public DateTime? ModifiedDate { get; set; }

        public virtual ICollection<SysUserRole> SysUserRoles { get; set; }
    }
}