using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace XEngine.Web.Controllers
{
    public class SampleHtmlHelperController : Controller
    {
        //
        // GET: /SampleHtmlHelper/
        public ActionResult Index()
        {
            //填充dropdownlist
            #region 绑定enum类型
            List<SelectListItem> selectPrivilegeMaster = new List<SelectListItem>();
            foreach (int i in Enum.GetValues(typeof(PrivilegeMaster)))
            {
                selectPrivilegeMaster.Add(new SelectListItem { Text = Enum.GetName(typeof(PrivilegeMaster), i), Value = Enum.GetName(typeof(PrivilegeMaster), i) });
            }
            ViewBag.selectPrivilegeMaster = PopulateDropDownList(selectPrivilegeMaster);
            #endregion

            return View();
        }




        #region 公共方法
        //PopulateDropDownList
        public SelectList PopulateDropDownList(List<SelectListItem> items, object selectedItem = null)
        {
            return new SelectList(items, "Value", "Text", selectedItem);
        }
        #endregion
    }

    public enum PrivilegeMaster
    {
        User,
        Role
    }
}