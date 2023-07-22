using ControlesTrabajo.Business;
using Dato;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using static ControlesTrabajo.Repository.TareasRepository;

namespace ControlesTrabajo.Controllers
{
    public class TareasController : Controller
    {
        private TareasBusiness _TareasBusiness;
        private HomeController _HomeController;
        private UsuarioBusiness _UsuarioBusiness;

        public TareasController()
        {
            this._TareasBusiness = new TareasBusiness();
            this._HomeController = new HomeController();
            this._UsuarioBusiness = new UsuarioBusiness();
        }

        public ActionResult CrearNuevaTarea()
        {
            Usuarios usuarioActivo = GetUsuarioFromCookie();
            ViewBag.usuarioActivo = usuarioActivo;
            return View();
        }

        public JsonResult NuevaTarea(Tareas tarea)
        {
            try
            {
                _TareasBusiness.SaveOrUpdate(tarea);
                return Json(new { success = true, message = "La tarea se genero y se aplico exitosamente" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Error al generar tarea: " + ex.Message });
            }
        }

        public ActionResult VerTodasLasTareas()
        {
            Usuarios usuarioActivo = GetUsuarioFromCookie();
            ViewBag.usuarioActivo = usuarioActivo;
            return View();
        }
        public JsonResult TodasLasTareas()
        {
            Usuarios usuarioActivo = GetUsuarioFromCookie();
            int idUsu = 0;
            if (usuarioActivo != null)
            {
                idUsu = usuarioActivo.Id;
            }
            else
            {
                idUsu = 0;
            }

            try
            {
                List<TareasClase> lista = _TareasBusiness.GetAll(idUsu);
                return Json(lista, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Error al generar tarea: " + ex.Message });
            }
        }

        public Usuarios GetUsuarioFromCookie()
        {
            var userCookie = Request.Cookies["Usuario"]?.Value;
            if (userCookie != null)
            {
                Usuarios usuarioData = _UsuarioBusiness.GetUsuByNombreUsu(userCookie);

                return usuarioData;
            }

            return null; // Por ejemplo, aquí se devuelve null como valor predeterminado.
        }
    }
}