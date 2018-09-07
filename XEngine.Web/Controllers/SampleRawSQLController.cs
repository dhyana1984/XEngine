using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using XEngine.Web.DAL;
using XEngine.Web.Models;

namespace XEngine.Web.Controllers
{
    /// <summary>
    /// 1.DbSet.SqlQuery
    /// 2.Database.SqlQuery
    /// 3.Database.ExecuteSqlCommand
    /// </summary>
    public class SampleRawSQLController : Controller
    {
        private XEngineContext db = new XEngineContext();

        public ActionResult Index()
        {
            int userId = 1;
            //1.获取Entities
            string query = @"SELECT  [ID]
                                  ,[Name]
                                  ,[Email]
                                  ,[Password]
                                  ,[CName]
                                  ,[Description]
                                  ,[ModifiedDate]
                              FROM [SysUser] WHERE ID=@id";
            SqlParameter[] paras = new SqlParameter[]{
                new SqlParameter("@id",userId)
            };
            var sysUsers = db.SysUsers.SqlQuery(query, paras).ToList();
            return View(sysUsers);
        }

        public ActionResult Update()
        {
            int userId = 1;
            string query = @"UPDATE [dbo].[SysUser]
                           SET [Name] = 'ZS2'
                         WHERE ID=@id";

            SqlParameter[] paras = new SqlParameter[]{
                new SqlParameter("@id",userId)
            };

            db.Database.ExecuteSqlCommand(query, paras);

            return RedirectToAction("Index");
        }


    }
}