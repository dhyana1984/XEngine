/**************************************************************************
*
* NAME        : SysPrivilege.cs
*
* VERSION     : 1.0.0
*
* DATE        : 28-Mar-2016
*
* DESCRIPTION : 权限Model
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
using System.Linq;
using System.Web;

namespace XEngine.Web.Models
{
    public class SysPrivilege
    {
        public int ID { get; set; }
        //User或Role
        [DisplayName("权限主体")]
        public string PrivilegeMaster { get; set; }
        //UserID或RoleID 的值
        [DisplayName("权限主体值")]
        public string PrivilegeMasterValue { get; set; }
        //如Menu
        [DisplayName("领域")]
        public string Access { get; set; }
        //如MenuID
        [DisplayName("领域值")]
        public string AccessValue { get; set; }
        //如enabled或disabled
        [DisplayName("权限")]
        public string PrivilegeOperation { get; set; }
    }
}