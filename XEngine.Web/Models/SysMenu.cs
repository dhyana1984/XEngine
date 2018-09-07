using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace XEngine.Web.Models
{
    [Description("模块（菜单导航）")]
    public class SysMenu : IItem<SysMenu>
    {
        #region 字段
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [DisplayName("MenuID")]
        public int ID { get; set; }
        public int? ParentID { get; set; }
        [DisplayName("名称")]
        [StringLength(50)]
        public string Name { get; set; }
        public string Action { get; set; }
        public string Controller { get; set; }
        [DisplayName("图标")]
        public string IconImage { get; set; }
        public MenuTypeOption MenuType { get; set; }
        public List<SysMenu> MenuChildren = new List<SysMenu>();
        [DisplayName("描述")]
        public string Description { get; set; }

        #region bak
                //[Description("菜单分类")]
        //public string Category { get; set; }
        //[Description("导航地址")]
        //public string NavigateUrl { get; set; }
        //[Description("窗体名")]
        //public string FormName { get; set; }

        //[Description("目标")]
        //public string Target { get; set; }
        //[Description("是否展开")]
        //public int? IsUnfold { get; set; }

        //[Description("允许编辑")]
        //public int? AllowEdit { get; set; }
        //[Description("允许删除")]
        //public int? AllowDelete { get; set; }

        //[Description("有效：1-有效，0-无效")]
        //public int? Enabled { get; set; }

        //[Description("排序吗")]
        //public int? SortCode { get; set; }

        //[Description("删除标记:1-正常，0-删除")]
        //public int? DeleteMark { get; set; }
        #endregion

        [Description("修改时间")]
        public DateTime? ModifiedDate { get; set; }
        #endregion

        #region 增加子成员
        public SysMenu AddItem(string txt)
        {
            SysMenu menu = new SysMenu() { Name = txt };
            MenuChildren.Add(menu);
            return menu;
        }

        public SysMenu AddItem(string txt, string controller, string action, string icon)
        {
            SysMenu menu = new SysMenu() { Name = txt, Action = action, Controller = controller, IconImage = icon };
            MenuChildren.Add(menu);
            return menu;
        }
        #endregion

        public IList<SysMenu> GetChildren()
        {
            return MenuChildren;
        }

    }

    public enum MenuTypeOption
    {
        Submenu=0,
        Top=1
    }
}