/**************************************************************************
*
* NAME        : UserController.cs
*
* VERSION     : 1.0.0
*
* DATE        : 22-Mar-2016
*
* DESCRIPTION : 用户管理
*
* MODIFICATION HISTORY
* Name             Date         Description
* ===============  ===========  =======================================
* Miro Yuan        22-Mar-2016  Initial Version
*
**************************************************************************/
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;
using XEngine.Web.DAL;
using XEngine.Web.Models;
using XEngine.Web.Repositories;
using XEngine.Web.ViewModels;

namespace XEngine.Web.Controllers
{
    public class UserController : Controller
    {

        private UnitOfWork unitOfWork = new UnitOfWork();
        private XEngineContext db = new XEngineContext();


        #region List
        public ActionResult Index()
        {
            //不加入任何条件
            //var users = unitOfWork.SysUserRepository.Get();

            //加入过滤条件
            //var users = unitOfWork.SysUserRepository.Get(filter: u=>u.Name=="ZS");

            //加入排序
            var users = unitOfWork.SysUserRepository.Get(orderBy: q => q.OrderBy(u => u.ID));
            unitOfWork.Dispose();
            return View(users);


        }
        #endregion

        #region Create
        public ActionResult Create()
        {
            //绑定IsActive的值
            List<SelectListItem> selectIsActive = new List<SelectListItem>
            {
                new SelectListItem{Text="是",Value="Y"},
                new SelectListItem{Text="否",Value="N"}
            };
            ViewData["selectIsActive"] = new SelectList(selectIsActive, "Value", "Text", "Y");
            return View();
        }

        [HttpPost]
        public ActionResult Create([Bind(Include = "Name,Email,Password,DisplayName,Description,IsActive")]SysUser sysUser, FormCollection fc)
        {
            try
            {
                sysUser.IsActive = fc["selectIsActive"];
                if (ModelState.IsValid)
                {
                    //检查是否已有此用户
                    SysUser user = unitOfWork.SysUserRepository.Get(filter: u => u.Name == sysUser.Name).FirstOrDefault();
                    if (user != null)
                    {
                        //绑定IsActive的值
                        List<SelectListItem> selectIsActive = new List<SelectListItem>
                        {
                            new SelectListItem{Text="是",Value="Y"},
                            new SelectListItem{Text="否",Value="N"}
                        };
                        ViewData["selectIsActive"] = new SelectList(selectIsActive, "Value", "Text", "Y");
                        ViewBag.Msg = "已存在此用户,请确认";
                        return View();
                    }
                    sysUser.ModifiedDate = DateTime.Now;
                    unitOfWork.SysUserRepository.Insert(sysUser);
                    unitOfWork.Save();
                    unitOfWork.Dispose();
                    return RedirectToAction("Index");
                }
            }
            catch (Exception)
            {

                throw;
            }
            return View();
        }
        #endregion

        #region Edit
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //1.绑定当前用户信息
            SysUser user = unitOfWork.SysUserRepository.GetByID(id);

            if (user == null)
            {
                return HttpNotFound();
            }
            //绑定IsActive的值
            List<SelectListItem> selectIsActive = new List<SelectListItem>
            {
                new SelectListItem{Text="是",Value="Y"},
                new SelectListItem{Text="否",Value="N"}
            };
            ViewData["selectIsActive"] = new SelectList(selectIsActive, "Value", "Text", user.IsActive);

            //2.绑定当前用户具有的角色
            BindAssignedRoleData(user);

            return View(user);

        }

        [HttpPost]
        public ActionResult Edit([Bind(Include = "ID,Name,Email,Password,DisplayName,Description,IsActive")] SysUser user, FormCollection fc)
        {
            try
            {
                //1.更改基本信息
                if (ModelState.IsValid)
                {
                    user.ModifiedDate = DateTime.Now;
                    user.IsActive = fc["selectIsActive"];
                    unitOfWork.SysUserRepository.Update(user);
                    unitOfWork.Save();
                    unitOfWork.Dispose();
                }
                //更改用户具有的角色 
                string roleList = fc["selectedUsers"]; //例如"2,2002"
                UpdateUserRoles(user.ID.ToString(), roleList);

                //3.更新权限

            }
            catch (Exception)
            {

                throw;
            }
            return Redirect("~/user/Edit/" + user.ID);
        }



        #endregion

        #region Delete
        public ActionResult Delete(int? id, bool? saveChangesError = false)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (saveChangesError.GetValueOrDefault())
            {
                ViewBag.ErrorMessage = "删除出错，请重试。";
            }
            SysUser user = unitOfWork.SysUserRepository.GetByID(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);

        }
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                unitOfWork.SysUserRepository.Delete(id);
                unitOfWork.Save();
                unitOfWork.Dispose();
            }
            catch (Exception)
            {
                return RedirectToAction("Delete", new { id = id, saveChangesError = true });
            }
            return RedirectToAction("Index");

        }
        #endregion

        #region 共用方法:PopulateAssignedUserData,UpdateUserRoles
        private void BindAssignedRoleData(SysUser user)
        {
            var allRoles = unitOfWork.SysRoleRepository.Get();
            var userRoleIDs = new HashSet<int>(user.SysUserRoles.Select(u => u.SysRoleID));
            var viewModel = new List<AssignedRoleData>();
            foreach (var role in allRoles)
            {
                viewModel.Add(new AssignedRoleData
                {
                    RoleID = role.ID,
                    RoleName = role.Name,
                    Assigned = userRoleIDs.Contains(role.ID)
                });
            }
            ViewBag.Roles = viewModel;
        }

        #region 根据用户id, 及该用户下的角色列表 更新 用户的角色
        /// <summary>
        /// 根据用户id, 及该用户下的角色列表 更新 用户的角色
        /// </summary>
        /// <param name="roleId">角色id</param>
        /// <param name="memberList">角色列表id</param>
        private void UpdateUserRoles(string userId, string memberList)
        {
            //根据userId删除该用户下的所有角色，插入该用户下的下新的角色列表
            string[] roleids = { };
            if (memberList != null)
            {
                roleids = memberList.Split(new char[] { ',' });
            }
            StringBuilder sb = new StringBuilder();
            if (userId != null)
            {
                sb.Append("DELETE FROM [dbo].[SysUserRole] WHERE [SysUserID]=@userId AND 1=1;");
            }
            for (int i = 0; i < roleids.Length; i++)
            {
                sb.Append("INSERT INTO [dbo].[SysUserRole] ([SysUserID],[SysRoleID],[ModifiedDate]) VALUES (@userId," + roleids[i] + ",GETDATE());");
            }

            string sql = sb.ToString();
            SqlParameter[] paras = new SqlParameter[]{
                new SqlParameter("@userId",userId)
            };
            db.Database.ExecuteSqlCommand(sql, paras);
        }
        #endregion


        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
        #endregion
    }
}