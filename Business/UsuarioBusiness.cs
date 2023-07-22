using ControlesTrabajo.Repository;
using Dato;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static ControlesTrabajo.Repository.UsuarioRepository;

namespace ControlesTrabajo.Business
{
    public class UsuarioBusiness
    {
        private UsuarioRepository _UsuarioRepository;
        public UsuarioBusiness()
        {
            this._UsuarioRepository = new UsuarioRepository();
        }

        public Usuarios GetUsuarioByID(int id)
        {
            return this._UsuarioRepository.GetUsuarioByID(id);
        }

        public void SaveOrUpdate(Usuarios usu)
        {
            Usuarios usuarioEncontrado = this.GetUsuarioByID(usu.Id);
            if (usuarioEncontrado == null)
            {
                usu.UsuarioNombre = usu.Nombre.ToLower() + usu.Apellido.ToLower();
                usu.Contraseña = usu.Nombre.ToLower() + usu.Apellido.ToLower() + _UsuarioRepository.GetUltimoId().ToString();
                _UsuarioRepository.SaveOrUpdate(usu);

            }
            else
            {
                _UsuarioRepository.SaveOrUpdate(usuarioEncontrado);
            }
        }

        public List<UsuAAsignar> GetAll()
        {
            return _UsuarioRepository.GetAll();
        }

        public bool ValidarUsu(string username, string password)
        {
            return _UsuarioRepository.ValidarUsu(username, password);
        }

        public Usuarios GetUsuByNombreUsu(string userCookie)
        {
            return _UsuarioRepository.GetUsuByNombreUsu(userCookie);
        }
    }
}