/**************************************************************************
*
* NAME        : DataServController.cs
*
* VERSION     : 1.0.0
*
* DATE        : 29-Mar-2016
*
* DESCRIPTION : 提供数据服务
*
* MODIFICATION HISTORY
* Name             Date         Description
* ===============  ===========  =======================================
* Miro Yuan        29-Mar-2016  Initial Version
*
**************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using XEngine.Web.DAL;
using XEngine.Web.Models;
using XEngine.Web.Utility.BreadCrumbSiteMap;

namespace XEngine.Web.Controllers
{
    public class DataServController : Controller
    {
        private XEngineContext db = new XEngineContext();
        public ActionResult Index()
        {
            return View();
        }

        #region 配合jstree.js 使用
        /// <summary>
        /// 配合jstree.js 使用
        /// </summary>
        /// <param name="clickID"></param>
        /// <returns></returns>
        public JsonResult GetTreeData(int clickID = 0)
        {
            /* 一次性加载完所有节点.
               * 前端只需配置 'data': {
                          'url': '/DataServ/GetTreeData',
                          'dataType': 'json'
               * } 不用给服务端传值
               * 服务端不需写where
              */

            var Items = from item in db.SysOrganizations
                        select new
                        {
                            id = item.ID,
                            parent = item.ParentID.ToString() == "0" ? "#" : item.ParentID.ToString(), // root必须是# ！
                            text = item.Name
                        };
            return Json(Items, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region BreadCrumb

        public ActionResult Test()
        {
            string s = "<a>首页123sadfasdfds</a>";

            s = s.Substring(s.IndexOf(">")+1, s.LastIndexOf("<") - s.IndexOf(">")-1);

    
            var list = MvcSiteMapHelper.GetSiteMapList();
            return View();
        }

        //public PartialViewResult BreadCrumbs(string id)
        //{
        //    List<SysMenu> list = new List<SysMenu>();
        //    StringBuilder strWhere = new StringBuilder();
        //    SysMenu parentModule = new SysMenu();
        //    SysMenu childModule = new SysMenu();
        //    list = bll.GetModelList(strWhere.ToString());

        //    var model = new BreadCrumbsModel();

        //    parentModule = bll.GetModel(id);
        //    var module = bll.GetModelList(strWhere.ToString()).FirstOrDefault(d => d.MenuId == parentModule.ParentId);
        //    if (module != null)
        //    {
        //        var parentModel = new BreadCrumbModel
        //        {
        //            IsParent = true,
        //            Name = module.FullName,
        //            //Url = module.NavigateUrl,
        //            Icon = module.Img
        //        };
        //        model.BreadCrumbList.Add(parentModel);
        //        var currentModel = new BreadCrumbModel
        //        {
        //            IsParent = false,
        //            Name = parentModule.FullName,
        //            Url = parentModule.NavigateUrl,
        //            Id = parentModule.MenuId,
        //            Icon = ""
        //        };
        //        model.CurrentName = currentModel.Name;
        //        model.BreadCrumbList.Add(currentModel);
        //        ViewBag.CurrentTitle = parentModule.FullName;
        //    }
        //    return PartialView(model);
        //}
        #endregion


    }
}