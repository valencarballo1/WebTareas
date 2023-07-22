using ControlesTrabajo.Business;
using Dato;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ControlesTrabajo.Controllers
{
    public class SharedController : Controller
    {
        private UsuarioBusiness _UsuarioBusiness;

        public SharedController()
        {
            this._UsuarioBusiness = new UsuarioBusiness();
        }
        // GET: Shared
        public ActionResult _Layout()
        {
            Usuarios usuarioActivo = GetUsuarioFromCookie();
            ViewBag.usuarioActivo = usuarioActivo;
            return View();
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