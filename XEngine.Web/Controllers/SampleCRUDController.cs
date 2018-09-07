using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using XEngine.Web.DAL;
using XEngine.Web.Models;


//TODO
namespace XEngine.Web.Controllers
{
    /// <summary>
    /// 单个实体操作示例，以user为例
    /// 使用原生方法请参考：RoleController
    /// </summary>
    public class SampleCRUDController : Controller
    {
        private UnitOfWork unitOfWork = new UnitOfWork();

        public ActionResult Index()
        {
            //不加入任何条件
            //var users = unitOfWork.SysUserRepository.Get();

            //加入过滤条件
            //var users = unitOfWork.SysUserRepository.Get(filter: u=>u.Name=="ZS");

            //加入排序
            var users = unitOfWork.SysUserRepository.Get(orderBy: q => q.OrderBy(u => u.Name));
            return View(users);
        }


        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name,CName,Description,ModifiedDate")] SysRole sysrole)
        {
            if (ModelState.IsValid)
            {
                //db.SysRoles.Add(sysrole);
                //db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(sysrole);
        }


        #region Lambda Test

        delegate int Mydel(int x);

        delegate int del(int i);
        public ActionResult LambdaTest()
        {
            //Mydel del = x => x + 1;
            //int a = del(5);
            //ViewBag.A = del(7);

            //int b = del(7);

            del myDelegate = x => { return x * x; };
            int j = myDelegate(5);//j=25
            return null;
        }
        #endregion

    }
}