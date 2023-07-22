using Dato;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Deployment.Internal;
using System.Linq;
using System.Web;

namespace ControlesTrabajo.Repository
{
    public class TareasRepository
    {

        public void SaveTareas(Tareas tarea)
        {
            using (ControlesEntities db = new ControlesEntities())
            {
                db.Tareas.AddOrUpdate(tarea);
                db.SaveChanges();
            }
        }

        public Tareas GetTareaById(int id)
        {
            using (ControlesEntities db = new ControlesEntities())
            {
                return db.Tareas.Include("Usuarios").Where(t => t.IdTarea == id).FirstOrDefault();
            }
        }

        public List<TareasClase> GetAll(int id)
        {
            using (ControlesEntities db = new ControlesEntities())
            {
                if (id > 0)
                {
                    return db.Tareas
                        .Include("Usuarios")
                        .Include("Estados")
                        .Where(t => t.IdUsuario == id)
                        .Select(t => new TareasClase
                        {
                            IdTarea = t.IdTarea,
                            UsuarioNombre = t.Usuarios.UsuarioNombre,
                            FechaInicio = t.FechaInicio.Value,
                            FechaFin = t.FechaFin ?? DateTime.MinValue,
                            NombreTarea = t.NombreTarea,
                            Comentario = t.Comentario,
                            EstadoNombre = t.Estados.EstadoNombre
                        })
                        .ToList();
                }
                else
                {
                    return db.Tareas
                        .Include("Usuarios")
                        .Include("Estados")
                        .Select(t => new TareasClase
                        {
                            IdTarea = t.IdTarea,
                            UsuarioNombre = t.Usuarios.UsuarioNombre,
                            FechaInicio = t.FechaInicio.Value,
                            FechaFin = t.FechaFin ?? DateTime.MinValue,
                            NombreTarea = t.NombreTarea,
                            Comentario = t.Comentario,
                            EstadoNombre = t.Estados.EstadoNombre
                        })
                        .ToList();
                }
            }
        }

        public int GetEstadoTareaById(int v, int id)
        {
            using (ControlesEntities db = new ControlesEntities())
            {
                if (id > 0)
                {
                    return db.Tareas.Where(t => t.IdEstado == v && t.IdUsuario == id).Count();
                }
                else
                {
                    return db.Tareas.Where(t => t.IdEstado == v).Count();
                }
            }
        }

        public Tareas GetTareasByNombre(string nombreTarea)
        {
            using (ControlesEntities db = new ControlesEntities())
            {
                return db.Tareas.Include("Usuarios").Where(t => t.NombreTarea == nombreTarea && t.IdEstado == 1).FirstOrDefault();
            }
        }

        public class TareasClase
        {
            public int IdTarea { get; set; }
            public string UsuarioNombre { get; set; }

            public DateTime FechaInicio { get; set; }

            public DateTime FechaFin { get; set; }

            public string NombreTarea { get; set; }

            public string Comentario { get; set; }

            public string EstadoNombre { get; set; }
        }
    }
}