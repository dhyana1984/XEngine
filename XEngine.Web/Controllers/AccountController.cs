/**************************************************************************
*
* NAME        : AccountController.cs
*
* VERSION     : 1.0.0
*
* DATE        : 20-Mar-2016
*
* DESCRIPTION : 通用 Login,Logout 功能
*
* MODIFICATION HISTORY
* Name             Date         Description
* ===============  ===========  =======================================
* Miro Yuan        20-Mar-2016  Initial Version
*
**************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using XEngine.Web.DAL;
using XEngine.Web.Models;
using XEngine.Web.Utility;

namespace XEngine.Web.Controllers
{
    public class AccountController : Controller
    {
        private UnitOfWork unitOfWork = new UnitOfWork();
 

        #region Login
        public ActionResult Login()
        {
            FormsAuthentication.SignOut();
            TempData["ReturnUrl"] = Convert.ToString(Request["ReturnUrl"]);
            return View();
        }

        [HttpPost]
        public ActionResult Login(FormCollection fc)
        {
            //1.获取表单数据
            string userName = fc["inputUserName"];
            string password = fc["inputPassword"];
            string encryptPwd = Utility.EncryptDecrypt.Encrypt(password);
            bool rememberMe = fc["ckbRememberMe"] == null ? false : true;
            string returnUrl = Convert.ToString(TempData["ReturnUrl"]);

            //2.验证用户
            SysUser user = unitOfWork.SysUserRepository.Get(filter: u => u.Name == userName && 
                (u.Password == password || u.Password==encryptPwd)).FirstOrDefault();
            unitOfWork.Dispose();

            //3.保存票据
            if (user != null)
            {
                FormsAuthentication.SetAuthCookie(userName, rememberMe);
                if (!string.IsNullOrEmpty(returnUrl))
                {
                    return  Redirect(returnUrl);
                }
                else
                {
                    return Redirect("~/");
                }
            }
            else
            {
                ViewBag.LoginState = "用户名或密码错误，请重试";
            }
            return View();
        }
        #endregion

        #region Logout
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return Redirect(Request.UrlReferrer.ToString());
        }
        #endregion


        public ActionResult Index()
        {
            return View();
        }
    }
}