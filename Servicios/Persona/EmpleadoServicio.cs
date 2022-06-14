using System;
using System.Linq;
using Dominio.UnidadDeTrabajo;
using IServicio.Persona;

namespace Servicios.Persona
{
    public class EmpleadoServicio : PersonaServicio, IEmpleadoServicio
    {
        public EmpleadoServicio(IUnidadDeTrabajo unidadDeTrabajo) 
            : base(unidadDeTrabajo)
        {
        }

        public int ObtenerSiguienteLegajo()
        {
            var empleados = _unidadDeTrabajo.EmpleadoRepositorio.Obtener();

            return empleados.Any()
                ? empleados.Max(x => x.Legajo) + 1
                : 1;
        }

        public bool VerificarSiExiste(string datoVerificar, long? entidadId = null)
        {
            return entidadId.HasValue
                ? _unidadDeTrabajo.EmpleadoRepositorio.Obtener(x => !x.EstaEliminado
                                                                    && x.Id != entidadId.Value
                                                                    && x.Dni.Equals(datoVerificar,
                                                                        StringComparison.CurrentCultureIgnoreCase))
                    .Any()
                : _unidadDeTrabajo.EmpleadoRepositorio.Obtener(x => !x.EstaEliminado
                                                                    && x.Dni.Equals(datoVerificar,
                                                                        StringComparison.CurrentCultureIgnoreCase))
                    .Any();
        }
    }
}
