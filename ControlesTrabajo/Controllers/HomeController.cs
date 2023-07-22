using ControlesTrabajo.Business;
using Dato;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Services.Description;

namespace ControlesTrabajo.Controllers
{
    public class HomeController : Controller
    {
        private TareasBusiness _TareasBusiness;
        private UsuarioBusiness _UsuarioBusiness;

        public HomeController()
        {
            _TareasBusiness = new TareasBusiness();
            this._UsuarioBusiness = new UsuarioBusiness();
        }

        public ActionResult Index()
        {
            Usuarios usuarioActivo = GetUsuarioFromCookie();
            if (usuarioActivo != null)
            {
                ViewBag.UsuarioActivo = usuarioActivo;
            }
            else
            {
                ViewBag.MensajeUsuarioNoEncontrado = "Usuario no encontrado";
            }

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

        public JsonResult ObtenerDatosEstadosTareas(int id)
        {
            // Aquí obtienes los datos de los estados de las tareas desde tu backend
            // y los devuelves en formato JSON
            int aDesarrollar = _TareasBusiness.GetEstadoTareaById(1, id);
            int desarrolando = _TareasBusiness.GetEstadoTareaById(2, id);
            int finalizados = _TareasBusiness.GetEstadoTareaById(3, id);


            var data = new
            {
                labels = new[] { "A desarrollar", "Desarrollando", "Finalizadas" },
                datasets = new[]
                {
            new
            {
                data = new[] { aDesarrollar, desarrolando, finalizados }, // Ejemplo de datos, reemplaza con tus valores reales
                backgroundColor = new[] { "#FF6384", "#36A2EB", "#FFCE56" } // Colores de los segmentos del gráfico
            }
        }
            };

            return Json(data, JsonRequestBehavior.AllowGet);
        }
    }
}