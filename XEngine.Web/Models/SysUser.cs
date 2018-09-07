using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace XEngine.Web.Models
{
    public class SysUser
    {
        public int ID { get; set; }

        [StringLength(50)]
        [DisplayName("用户账号")]
        public string Name { get; set; }

        [DisplayName("邮箱")]
        public string Email { get; set; }

        [DisplayName("密码")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DisplayName("显示名")]
        public string DisplayName { get; set; }

        [StringLength(50)]
        [DisplayName("描述")]
        public string Description { get; set; }

        [DisplayName("启用")]
        public string IsActive { get; set; }

        [DisplayName("修改日期")]
        public DateTime? ModifiedDate { get; set; }

        public virtual ICollection<SysUserRole> SysUserRoles { get; set; }
    }
}