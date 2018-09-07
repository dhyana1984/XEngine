/**************************************************************************
*
* NAME        : AccountController.cs
*
* VERSION     : 1.0.0
*
* DATE        : 28-Mar-2016
*
* DESCRIPTION : 组织机构：包括 1公司、2部门、3岗位; 
*               状态  0 正常 1 冻结
*
* MODIFICATION HISTORY
* Name             Date         Description
* ===============  ===========  =======================================
* Miro Yuan        28-Mar-2016  Initial Version
*
**************************************************************************/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace XEngine.Web.Models
{
    public class SysOrganization
    {
        public int ID { get; set; }

        [StringLength(20)]
        [DisplayName("名称")]
        public string Name { get; set; }

        [StringLength(50)]
        [DisplayName("说明")]
        public string Description { get; set; }

        /// <summary>
        /// 类型:1 单位 2 部门 3 岗位
        /// </summary>
        [DisplayName("类型")]
        public int Type { get; set; }

        /// <summary>
        /// 分管领导
        /// </summary>
        [DisplayName("分管领导")]
        public string ChargeLeader { get; set; }

        /// <summary>
        /// 部门或岗位主管
        /// </summary>
        [DisplayName("部门/岗位领导")]
        public string Leader { get; set; }

        /// <summary>
        /// 父ID
        /// </summary>
        [DisplayName("父ID")]
        public int ParentID { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        [DisplayName("排序")]
        public int Sort { get; set; }

        [DisplayName("修改日期")]
        public DateTime? ModifiedDate { get; set; }

        /// <summary>
        /// 状态  0 正常 1 冻结
        /// </summary>
        [DisplayName("状态")]
        public int Status { get; set; }
    }
}