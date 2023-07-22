using Dato;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using static ControlesTrabajo.Repository.RolesRepository;

namespace ControlesTrabajo.Repository
{
    public class UsuarioRepository
    {
        public void SaveOrUpdate(Usuarios usu)
        {
            using (ControlesEntities db = new ControlesEntities())
            {
                db.Usuarios.AddOrUpdate(usu);
                db.SaveChanges();
            }
        }

        public Usuarios GetUsuarioByID(int id)
        {
            using (ControlesEntities db = new ControlesEntities())
            {
                return db.Usuarios.Where(u => u.Id == id).FirstOrDefault();
            }
        }

        public int GetUltimoId()
        {
            using (ControlesEntities db = new ControlesEntities())
            {
                var maxId = db.Usuarios.Include("Roles").OrderByDescending(u => u.Id).FirstOrDefault()?.Id;
                int nextId = maxId != null ? maxId.Value + 1 : 1;
                return nextId;
            }
        }

        public List<UsuAAsignar> GetAll()
        {
            using (ControlesEntities db = new ControlesEntities())
            {
                List<Usuarios> usuario = db.Usuarios.ToList();
                List<UsuAAsignar> usu = usuario.Select(u => new UsuAAsignar { Id = u.Id, UsuarioNombre = u.UsuarioNombre }).ToList();
                return usu;
            }
        }

        public bool ValidarUsu(string username, string password)
        {
            using(ControlesEntities db = new ControlesEntities())
            {
                Usuarios usu = db.Usuarios.Where(u => u.UsuarioNombre == username && u.Contraseña == password).FirstOrDefault();
                if(usu != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public Usuarios GetUsuByNombreUsu(string userCookie)
        {
            using(ControlesEntities db = new ControlesEntities())
            {
                return db.Usuarios.Include("Tareas").Include("Roles").Where(u => u.UsuarioNombre == userCookie).FirstOrDefault();
            }
        }

        public class UsuAAsignar
        {
            public int Id { get; set; }
            public string UsuarioNombre { get; set; }
        }
    }
}