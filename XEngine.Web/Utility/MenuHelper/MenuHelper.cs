using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using XEngine.Web.DAL;
using XEngine.Web.Models;
//using XEngine.Web.Utility.MenuHelper.Base;
using XEngine.Web.ViewModels;

namespace XEngine.Web.Utility.MenuHelper
{
    public static class MenuHelper
    {
        public static string GetMenuHtml(this HtmlHelper helper, string menuName)
        {
            MenuViewModel<SysMenu> model = null;

            HtmlBuilder builder = new HtmlBuilder(helper);

            model = CreateMenuModel(menuName);

            #region MyRegion
            //switch (HelperType)
            //{
            //    case "Menu1":
            //        model = MenuModelTest.CreateModel();
            //        break;

            //    case "Menu2":
            //        model = MenuModel_1.CreateModel();
            //        break;

            //    default:
            //        model = MenuModelTest.CreateModel();
            //        break;
            //}
            #endregion

            builder.BuildTreeStruct(model);
            return builder.Build();
        }


        public static MenuViewModel<SysMenu> CreateMenuModel(string menuName)
        {
            UnitOfWork unitOfWork = new UnitOfWork();

            MenuViewModel<SysMenu> model = new MenuViewModel<SysMenu>();

            // 1. 根据menuName获取开始的根菜单
            SysMenu itemRoot = unitOfWork.SysMenuRepository.Get(filter: m => m.Name == menuName).FirstOrDefault();

            #region MyRegion
            //           var itemRoots = unitOfWork.SysMenuRepository.Get(filter: m => m.MenuType == MenuTypeOption.Top);

            //if (itemRoots!=null)
            //{
            //    foreach (var itemRoot in itemRoots)
            //    {
            //        // 2. 依次添加枝叶菜单
            //        // 2.1 获取itemRoot的所有子菜单
            //        IEnumerable<SysMenu> menus = unitOfWork.SysMenuRepository.Get(filter: m => m.ParentID == itemRoot.ID);
            //        // 2.2 对每个子菜单进行递归循环
            //        foreach (var item in menus)
            //        {
            //            itemRoot.MenuChildren.Add(item);
            //            AddChildNode(item);
            //        }
            //        model.MenuItems.Add(itemRoot);

            //    }
            //}
            #endregion

            if (itemRoot != null)
            {
                // 2. 依次添加枝叶菜单
                // 2.1 获取itemRoot的所有子菜单
                IEnumerable<SysMenu> menus = unitOfWork.SysMenuRepository.Get(filter: m => m.ParentID == itemRoot.ID);
                // 2.2 对每个子菜单进行递归循环
                foreach (var item in menus)
                {
                    itemRoot.MenuChildren.Add(item);
                    AddChildNode(item);
                }
            }

            model.MenuItems.Add(itemRoot);
            return model;
        }

        /// <summary>
        /// 找到menu的所有子成员并添加，递归出调用本身
        /// </summary>
        /// <param name="menu"></param>
        public static void AddChildNode(SysMenu menu)
        {
            UnitOfWork unitOfWork = new UnitOfWork();
            var menus = unitOfWork.SysMenuRepository.Get(filter: m => m.ParentID == menu.ID);
            foreach (var item in menus)
            {
                menu.MenuChildren.Add(item);
                AddChildNode(item);
            }
        }

    }
}