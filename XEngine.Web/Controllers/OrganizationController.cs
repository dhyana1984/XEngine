/**************************************************************************
*
* NAME        : UserController.cs
*
* VERSION     : 1.0.0
*
* DATE        : 30-Mar-2016
*
* DESCRIPTION : 部门管理
*
* MODIFICATION HISTORY
* Name             Date         Description
* ===============  ===========  =======================================
* Miro Yuan        30-Mar-2016  Initial Version
*
**************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using XEngine.Web.DAL;
using XEngine.Web.Models;

namespace XEngine.Web.Controllers
{
    public class OrganizationController : Controller
    {
        private UnitOfWork unitOfWork = new UnitOfWork();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //1.绑定当前实体信息
            SysOrganization role = unitOfWork.SysOrganizationRepository.GetByID(id);

            if (role == null)
            {
                return HttpNotFound();
            }
        


            return View(role);
        }
	}
}