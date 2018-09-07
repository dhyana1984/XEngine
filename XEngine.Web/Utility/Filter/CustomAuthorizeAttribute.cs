/**************************************************************************
*
* NAME        : CustomAuthorizeAttribute.cs
*
* VERSION     : 1.0.0
*
* DATE        : 27-Apr-2016
*
* DESCRIPTION : 自定义过滤器,配合 ActionRoles.xml使用
*
* MODIFICATION HISTORY
* Name             Date         Description
* ===============  ===========  =======================================
* Miro Yuan        27-Apr-2016  Initial Version
*
 * <example>
 * 可在FilterConfig中添加全局，对所有action启用
 * //增加角色过滤
   //filters.Add(new CustomAuthorizeAttribute());
 * 
 * 也可在action或controller上添加
 *      [CustomAuthorize]
        public ActionResult Index()
        {
            var user=db.SysRoles.Count();
            return View();
        }
 * </example>
**************************************************************************/
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using XEngine.Web.DAL;
using XEngine.Web.Models;

namespace XEngine.Web.Utility
{
    public class CustomAuthorizeAttribute : AuthorizeAttribute
    {
        private XEngineContext db = new XEngineContext();

        //相应Action允许的角色
        private string[] AuthRoles { get; set; }
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            if (httpContext == null)
            {
                throw new ArgumentNullException("HttpContext");
            }
            if (AuthRoles == null || AuthRoles.Length == 0)
            {
                return true;
            }
            if (!httpContext.User.Identity.IsAuthenticated)
            {
                return false;
            }

            //确定当前用户角色是否属于指定的角色
            #region 确定当前用户角色是否属于指定的角色
            //1. 获取用户所在角色 
            string query = @"SELECT Name from [SysRole]
                                WHERE [ID] IN
                                ( SELECT SysRoleID FROM [SysUserRole] 
                                WHERE [SysUserID] =(
                                SELECT [ID] FROM [dbo].[SysUser]
                                WHERE [NAME]=@userName) )";
            string currentUser = httpContext.User.Identity.Name;

            SqlParameter[] paras = new SqlParameter[]{
                new SqlParameter("@userName",currentUser)
            };
            var userRoles = db.Database.SqlQuery<string>(query, paras).ToList();

            //2. 验证是否属于 AuthRoles
            for (int i = 0; i < AuthRoles.Length; i++)
            {
                if (userRoles.Contains(AuthRoles[i]))
                {
                    return true;
                }
            }
            #endregion

            return false; 
        }

        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            string controllerName = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName;
            string actionName = filterContext.ActionDescriptor.ActionName;
            //获取设定的action允许的角色，未来改到数据库中
            string roles = GetXMLRoles.GetActionRoles(actionName, controllerName);
            if (!string.IsNullOrWhiteSpace(roles))
            {
                this.AuthRoles = roles.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
            }
            else
            {
                this.AuthRoles = new string[]{};
            }
            base.OnAuthorization(filterContext);
        }
    }
}