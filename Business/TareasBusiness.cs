using ControlesTrabajo.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Dato;
using static ControlesTrabajo.Repository.TareasRepository;
using System.Runtime.CompilerServices;

namespace ControlesTrabajo.Business
{
    public class TareasBusiness
    {
        private TareasRepository _TareasRepository;

        public TareasBusiness()
        {
            _TareasRepository = new TareasRepository();
        }

        public void SaveOrUpdate(Tareas tarea)
        {
            Tareas tareaEncontrada = _TareasRepository.GetTareaById(tarea.IdTarea);
            Tareas tareaNombre = _TareasRepository.GetTareasByNombre(tarea.NombreTarea);

            if (tareaEncontrada == null && tareaNombre == null)
            {
                tarea.FechaInicio = DateTime.Now;
                tarea.IdEstado = 1;
                _TareasRepository.SaveTareas(tarea);
            }
            else if (tareaNombre != null)
            {

                throw new Exception("No se puede agregar una tarea con el mismo nombre");
            }
            else
            {
                _TareasRepository.SaveTareas(tareaEncontrada);

            }
        }

        public List<TareasClase> GetAll(int id)
        {
            return _TareasRepository.GetAll(id);
        }

        public int GetEstadoTareaById(int v, int id)
        {
            return _TareasRepository.GetEstadoTareaById(v, id);
        }
    }
}