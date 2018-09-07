/**************************************************************************
*
* NAME        : AccountController.cs
*
* VERSION     : 1.0.0
*
* DATE        : 22-Mar-2016
*
* DESCRIPTION :模板，供开发时复制粘贴用
*
* MODIFICATION HISTORY
* Name             Date         Description
* ===============  ===========  =======================================
* Miro Yuan        22-Mar-2016  Initial Version
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
    public class BaseModel
    {
        public int ID { get; set; }

        [StringLength(50)]
        [DisplayName("名称")]
        public string Name { get; set; }

        [DisplayName("描述")]
        public string Description { get; set; }

        public DateTime? CreatedDate { get; set; }

        [DisplayName("修改日期")]
        public DateTime? ModifiedDate { get; set; }


        [DisplayName("是否激活")]
        public string IsActive { get; set; }
    }
}