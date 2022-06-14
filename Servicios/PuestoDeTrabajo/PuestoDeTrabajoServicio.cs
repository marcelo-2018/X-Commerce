using System;
using System.Collections.Generic;
using System.Linq;
using Dominio.UnidadDeTrabajo;
using IServicio.BaseDto;
using IServicios.PuestoDeTrabajo;
using IServicios.PuestoDeTrabajo.DTOs;

namespace Servicios.PuestoDeTrabajo
{
    public class PuestoDeTrabajoServicio : IPuestoDeTrabajoServicio
    {
        private readonly IUnidadDeTrabajo _unidadDeTrabajo;

        public PuestoDeTrabajoServicio(IUnidadDeTrabajo unidadDeTrabajo)
        {
            _unidadDeTrabajo = unidadDeTrabajo;
        }

        public void Eliminar(long id)
        {
            _unidadDeTrabajo.PuestoTrabajoRepositorio.Eliminar(id);
            _unidadDeTrabajo.Commit();
        }

        public void Insertar(DtoBase dtoEntidad)
        {
            var dto = (PuestoDeTrabajoDto)dtoEntidad;

            var entidad = new Dominio.Entidades.PuestoTrabajo()
            {
                Codigo = dto.Codigo,
                Descripcion = dto.Descripcion,
                EstaEliminado = false
            };

            _unidadDeTrabajo.PuestoTrabajoRepositorio.Insertar(entidad);
            _unidadDeTrabajo.Commit();
        }

        public void Modificar(DtoBase dtoEntidad)
        {
            var dto = (PuestoDeTrabajoDto)dtoEntidad;

            var entidad = _unidadDeTrabajo.PuestoTrabajoRepositorio.Obtener(dto.Id);

            if (entidad == null) throw new Exception("Ocurrio un Error al Obtener el puesto de trabajo");

            entidad.Codigo = dto.Codigo;
            entidad.Descripcion = dto.Descripcion;

            _unidadDeTrabajo.PuestoTrabajoRepositorio.Modificar(entidad);
            _unidadDeTrabajo.Commit();
        }

        public DtoBase Obtener(long id)
        {
            var entidad = _unidadDeTrabajo.PuestoTrabajoRepositorio.Obtener(id);

            return new PuestoDeTrabajoDto
            {
                Id = entidad.Id,
                Codigo = entidad.Codigo,
                Descripcion = entidad.Descripcion,
                Eliminado = entidad.EstaEliminado
            };
        }

        public IEnumerable<DtoBase> Obtener(string cadenaBuscar, bool mostrarTodos = true)
        {
            return _unidadDeTrabajo.PuestoTrabajoRepositorio.Obtener(x => x.Descripcion.Contains(cadenaBuscar))
                .Select(x => new PuestoDeTrabajoDto
                {
                    Id = x.Id,
                    Codigo = x.Codigo,
                    Descripcion = x.Descripcion,
                    Eliminado = x.EstaEliminado
                })
                .OrderBy(x => x.Descripcion)
                .ToList();
        }

        public int ObtenerSiguienteCodigo()
        {
            var puestoTrabajo = _unidadDeTrabajo.PuestoTrabajoRepositorio.Obtener();

            return puestoTrabajo.Any()
                ? puestoTrabajo.Max(x => x.Codigo) + 1
                : 1;
        }

        public bool VerificarSiExiste(string datoVerificar, long? entidadId = null)
        {
            return entidadId.HasValue
                ? _unidadDeTrabajo.PuestoTrabajoRepositorio.Obtener(x => !x.EstaEliminado
                                                                         && x.Id != entidadId.Value
                                                                         && x.Descripcion.Equals(datoVerificar,
                                                                             StringComparison.CurrentCultureIgnoreCase))
                    .Any()
                : _unidadDeTrabajo.PuestoTrabajoRepositorio.Obtener(x => !x.EstaEliminado
                                                                         && x.Descripcion.Equals(datoVerificar,
                                                                             StringComparison.CurrentCultureIgnoreCase))
                    .Any();
        }
    }
}
