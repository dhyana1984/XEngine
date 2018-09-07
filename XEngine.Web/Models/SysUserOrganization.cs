/**************************************************************************
*
* NAME        : SysUserOrganization.cs
*
* VERSION     : 1.0.0
*
* DATE        : 30-Mar-2016
*
* DESCRIPTION : 用户组织关系
*
* MODIFICATION HISTORY
* Name             Date         Description
* ===============  ===========  =======================================
* Miro Yuan        30-Mar-2016  Initial Version
*
**************************************************************************/
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace XEngine.Web.Models
{
    public class SysUserOrganization
    {
        [Key, Column(Order = 0)]
        [ForeignKey("SysUser")]
        public int SysUserID { get; set; }

        [Key, Column(Order = 1)]
        [ForeignKey("SysOrganization")]
        public int SysOrganizationID { get; set; }

        public DateTime ModifiedDate { get; set; }

        public virtual SysUser SysUser { get; set; }
        public virtual SysOrganization SysOrganization { get; set; }
    }
}