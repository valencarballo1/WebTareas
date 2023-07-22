using Dato;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ControlesTrabajo.Repository
{
    public class RolesRepository
    {
        public List<RolesClase> GetAll()
        {
            using (ControlesEntities db = new ControlesEntities())
            {
                List<Roles> roles = db.Roles.ToList();
                List<RolesClase> rolesClaseList = roles.Select(r => new RolesClase { Id = r.Id, Rol = r.Rol }).ToList();
                return rolesClaseList;
            }
        }

        public class RolesClase
        {
            public int Id { get; set; }
            public string Rol { get; set; }
        }
    }
}