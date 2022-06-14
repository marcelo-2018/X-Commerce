using System;
using System.Linq;
using Dominio.UnidadDeTrabajo;
using IServicio.Persona;

namespace Servicios.Persona
{
    public class ClienteServicio : PersonaServicio, IClienteServicio
    {
        public ClienteServicio(IUnidadDeTrabajo unidadDeTrabajo) 
            : base(unidadDeTrabajo)
        {
        }

        public bool VerificarSiExiste(string datoVerificar, long? entidadId = null)
        {
            return entidadId.HasValue
                ? _unidadDeTrabajo.ClienteRepositorio.Obtener(x => !x.EstaEliminado
                                                                    && x.Id != entidadId.Value
                                                                    && x.Dni.Equals(datoVerificar,
                                                                        StringComparison.CurrentCultureIgnoreCase))
                    .Any()
                : _unidadDeTrabajo.ClienteRepositorio.Obtener(x => !x.EstaEliminado
                                                                    && x.Dni.Equals(datoVerificar,
                                                                        StringComparison.CurrentCultureIgnoreCase))
                    .Any();
        }
    }
}
