/**************************************************************************
*
* NAME        : UserController.cs
*
* VERSION     : 1.0.0
*
* DATE        : 24-Mar-2016
*
* DESCRIPTION : 角色管理
*
* MODIFICATION HISTORY
* Name             Date         Description
* ===============  ===========  =======================================
* Miro Yuan        24-Mar-2016  Initial Version
*
**************************************************************************/
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using XEngine.Web.Models;
using XEngine.Web.DAL;
using System.Data.SqlClient;
using System.Text;

namespace XEngine.Web.Controllers
{

    public class RoleController : Controller
    {
        private UnitOfWork unitOfWork = new UnitOfWork();
        private XEngineContext db = new XEngineContext();

        #region List
        public ActionResult Index()
        {
            var roles = unitOfWork.SysRoleRepository.Get();
            unitOfWork.Dispose();
            return View(roles);
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
        public ActionResult Create([Bind(Include = "Name,DisplayName,Description,IsActive")]SysRole sysRole, FormCollection fc)
        {
            try
            {
                sysRole.IsActive = fc["selectIsActive"];
                if (ModelState.IsValid)
                {
                    //检查是否已有此角色
                    SysRole role = unitOfWork.SysRoleRepository.Get(filter: r => r.Name == sysRole.Name).FirstOrDefault();
                    if (role != null)
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
                    sysRole.ModifiedDate = DateTime.Now;
                    unitOfWork.SysRoleRepository.Insert(sysRole);
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
            //1.绑定当前角色信息
            SysRole role = unitOfWork.SysRoleRepository.GetByID(id);

            if (role == null)
            {
                return HttpNotFound();
            }
            //绑定IsActive的值
            List<SelectListItem> selectIsActive = new List<SelectListItem>
            {
                new SelectListItem{Text="是",Value="Y"},
                new SelectListItem{Text="否",Value="N"}
            };
            ViewData["selectIsActive"] = new SelectList(selectIsActive, "Value", "Text", role.IsActive);

            //2.绑定当前用户具有的角色
            BindAssignedUserData(id);

            return View(role);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name,DisplayName,Description,ModifiedDate,IsActive")] SysRole role, FormCollection fc)
        {
            if (ModelState.IsValid)
            {
                //1.更新角色
                role.ModifiedDate = DateTime.Now;
                role.IsActive = fc["selectIsActive"];
                unitOfWork.SysRoleRepository.Update(role);
                unitOfWork.Save();
                unitOfWork.Dispose();
                //2.更新角色下用户
                //2.1 获取角色下的用户列表
                string nameList = fc["nameList"];
                //2.2 根据roleID删除该角色下所有用户，插入2.1中列表
                UpdateRoleMembers(role.ID.ToString(), nameList);

                //【TODO】3.更新角色权限


            }
            return Redirect("~/role/Edit/" + role.ID);
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
            SysRole role = unitOfWork.SysRoleRepository.GetByID(id);
            if (role == null)
            {
                return HttpNotFound();
            }
            return View(role);

        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                unitOfWork.SysRoleRepository.Delete(id);
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

        #region 共用方法: BindAssignedUserData,UpdateRoleMembers

        #region BindAssignedUserData
        /// <summary>
        /// 绑定该角色下的用户
        /// </summary>
        /// <param name="id"></param>
        private void BindAssignedUserData(int? id)
        {
            string roleID = id.ToString();
            string query = @"SELECT distinct u.ID,u.Name,Selected=(
                                        case when ur.SysUserID in 
                                        (SELECT SysUserID FROM [dbo].[SysUserRole] where SysRoleID=@roleID ) 
                                        then 'true'
                                        else 'false'
                                        end)
                                        FROM  [dbo].[SysUser] u LEFT JOIN [SysUserRole] ur
                                        ON u.id=ur.SysUserID";
            SqlParameter[] paras = new SqlParameter[]{
                            new SqlParameter("@roleId",roleID)
                        };
            List<TempUser> users = db.Database.SqlQuery<TempUser>(query, paras).ToList();
            var test = (from u in users
                        select new SelectListItem
                        {
                            Text = u.Name,
                            Value = u.ID.ToString(),
                            Selected = Convert.ToBoolean(u.Selected)
                        });
            List<SelectListItem> listRoleUsers = new List<SelectListItem>();
            listRoleUsers.AddRange(test);
            ViewBag.nameList = listRoleUsers;
        }
        #endregion

        #region 根据角色id, 及该角色下的用户列表 更新 角色下的所属用户
        /// <summary>
        /// 根据角色id, 及该角色下的用户列表 更新 角色下的所属用户
        /// </summary>
        /// <param name="roleID">角色id</param>
        /// <param name="memberList">用户列表id</param>
        private void UpdateRoleMembers(string roleID, string memberList)
        {
            //根据roleId删除该角色的所有用户，插入该角色下新的用户列表
            string[] userids = { };
            if (memberList != null)
            {
                userids = memberList.Split(new char[] { ',' });
            }
            StringBuilder sb = new StringBuilder();
            if (roleID != null)
            {
                sb.Append("DELETE FROM [dbo].[SysUserRole] WHERE [SysRoleID]=@roleID AND 1=1;");
            }
            for (int i = 0; i < userids.Length; i++)
            {
                sb.Append("INSERT INTO [dbo].[SysUserRole] ([SysUserID],[SysRoleID],[ModifiedDate]) VALUES (" + userids[i] + ",@roleID,GETDATE());");
            }

            string sql = sb.ToString();
            SqlParameter[] paras = new SqlParameter[]{
                new SqlParameter("@roleId",roleID)
            };
            db.Database.ExecuteSqlCommand(sql, paras);
        }
        #endregion

        class TempUser
        {
            public int ID { get; set; }
            public string Name { get; set; }
            public string Selected { get; set; }
        }
        #endregion
    }
}
