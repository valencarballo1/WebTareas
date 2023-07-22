using ControlesTrabajo.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static ControlesTrabajo.Repository.RolesRepository;

namespace ControlesTrabajo.Business
{
    public class RolesBusiness
    {
        private RolesRepository _RolesRepository;
        public RolesBusiness()
        {
            this._RolesRepository = new RolesRepository();
        }

        public List<RolesClase> GetAll()
        {
            return this._RolesRepository.GetAll();
        }
    }
}