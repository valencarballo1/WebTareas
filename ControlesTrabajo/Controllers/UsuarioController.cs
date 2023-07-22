using ControlesTrabajo.Business;
using Dato;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using static ControlesTrabajo.Repository.RolesRepository;
using static ControlesTrabajo.Repository.UsuarioRepository;

namespace ControlesTrabajo.Controllers
{
    public class UsuarioController : Controller
    {
        private UsuarioBusiness _UsuarioBusiness;
        public UsuarioController()
        {
            this._UsuarioBusiness = new UsuarioBusiness();
        }
        public ActionResult CrearUsuario()
        {
            Usuarios usuarioActivo = GetUsuarioFromCookie();
            ViewBag.usuarioActivo = usuarioActivo;
            return View();
        }

        [HttpPost]
        public JsonResult NuevoUsuario(Usuarios usu)
        {
            try
            {
                _UsuarioBusiness.SaveOrUpdate(usu);
                return Json(new { success = true, message = "El usuario se ha creado correctamente." });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Error al crear el usuario: " + ex.Message });
            }
        }

        public JsonResult GetAll()
        {
            List<UsuAAsignar> usuarios = _UsuarioBusiness.GetAll();
            return Json(usuarios, JsonRequestBehavior.AllowGet);
        }

        public ActionResult InicioUsuario()
        {
            return View();
        }

        public JsonResult Login(string username, string password)
        {
            // Validar las credenciales del usuario (por ejemplo, consultar una base de datos)
            bool isValidUser = _UsuarioBusiness.ValidarUsu(username, password);

            if (isValidUser)
            {
                // Crear una cookie de autenticación para el usuario
                var cookie = new HttpCookie("Usuario", username);
                cookie.Expires = DateTime.Now.AddDays(7);
                // Agregar la cookie al contexto de respuesta
                Response.Cookies.Add(cookie);

                return Json(new { success = true, message = "¡Sesión iniciada correctamente!" });
            }

            // En caso de credenciales inválidas, devolver una respuesta JSON con un indicador de error y el mensaje de error
            return Json(new { success = false, message = "Credenciales inválidas" });
        }

        public JsonResult CierreSesion()
        {
            // Verificar si la cookie existe
            if (Request.Cookies["Usuario"] != null)
            {
                // Crear una nueva cookie con el mismo nombre y configuración de expiración en el pasado
                var cookie = new HttpCookie("Usuario")
                {
                    Expires = DateTime.Now.AddDays(-1) // Establecer la expiración en el pasado
                };

                // Reemplazar la cookie existente con la nueva cookie expirada
                Response.Cookies.Add(cookie);
            }

            // Otros pasos para cerrar la sesión si es necesario

            // Retornar una respuesta JSON u otra indicación de que se ha cerrado la sesión
            return Json(new { success = true }, JsonRequestBehavior.AllowGet);
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