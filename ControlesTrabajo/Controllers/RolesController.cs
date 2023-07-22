using ControlesTrabajo.Business;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static ControlesTrabajo.Repository.RolesRepository;

namespace ControlesTrabajo.Controllers
{
    public class RolesController : Controller
    {
        private RolesBusiness _RolesBusiness;

        public RolesController()
        {
            this._RolesBusiness = new RolesBusiness();
        }

        public JsonResult GetAllRoles()
        {
            List<RolesClase> roles = _RolesBusiness.GetAll();
            return Json(roles, JsonRequestBehavior.AllowGet);
        }
    }
}