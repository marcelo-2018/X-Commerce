using System;
using System.Collections.Generic;
using System.Linq;
using Dominio.UnidadDeTrabajo;
using IServicio.BaseDto;
using IServicio.Rubro.DTOs;
using IServicio.Rubro;

namespace Servicios.Rubro
{
    public class RubroServicio : IRubroServicio
    {
        private readonly IUnidadDeTrabajo _unidadDeTrabajo;

        public RubroServicio(IUnidadDeTrabajo unidadDeTrabajo)
        {
            _unidadDeTrabajo = unidadDeTrabajo;
        }

        public void Eliminar(long id)
        {
            _unidadDeTrabajo.RubroRepositorio.Eliminar(id);
            _unidadDeTrabajo.Commit();
        }

        public void Insertar(DtoBase dtoEntidad)
        {
            var dto = (RubroDto)dtoEntidad;

            var entidad = new Dominio.Entidades.Rubro
            {
                Descripcion = dto.Descripcion,
                ActivarLimiteVenta = dto.ActivarLimiteVenta,
                LimiteVenta = dto.LimiteVenta,
                ActivarHoraVenta = dto.ActivarHoraVenta,
                HoraLimiteVentaDesde = dto.HoraLimiteVentaDesde,
                HoraLimiteVentaHasta = dto.HoraLimiteVentaHasta,
                EstaEliminado = false
            };

            _unidadDeTrabajo.RubroRepositorio.Insertar(entidad);
            _unidadDeTrabajo.Commit();
        }

        public void Modificar(DtoBase dtoEntidad)
        {
            var dto = (RubroDto)dtoEntidad;

            var entidad = _unidadDeTrabajo.RubroRepositorio.Obtener(dto.Id);

            if (entidad == null) throw new Exception("Ocurrió un Error al Obtener la Rubro");

            entidad.Descripcion = dto.Descripcion;
            entidad.ActivarLimiteVenta = dto.ActivarLimiteVenta;
            entidad.LimiteVenta = dto.LimiteVenta;
            entidad.ActivarHoraVenta = dto.ActivarHoraVenta;
            entidad.HoraLimiteVentaDesde = dto.HoraLimiteVentaDesde;
            entidad.HoraLimiteVentaHasta = dto.HoraLimiteVentaHasta;

            _unidadDeTrabajo.RubroRepositorio.Modificar(entidad);
            _unidadDeTrabajo.Commit();
        }

        public DtoBase Obtener(long id)
        {
            var entidad = _unidadDeTrabajo.RubroRepositorio.Obtener(id);

            return new RubroDto
            {
                Id = entidad.Id,
                Descripcion = entidad.Descripcion,
                ActivarLimiteVenta = entidad.ActivarLimiteVenta,
                LimiteVenta = entidad.LimiteVenta,
                ActivarHoraVenta = entidad.ActivarHoraVenta,
                HoraLimiteVentaDesde = entidad.HoraLimiteVentaDesde,
                HoraLimiteVentaHasta = entidad.HoraLimiteVentaHasta,
                Eliminado = entidad.EstaEliminado
            };
        }

        public IEnumerable<DtoBase> Obtener(string cadenaBuscar, bool mostrarTodos = true)
        {
            return _unidadDeTrabajo.RubroRepositorio.Obtener(x => x.Descripcion.Contains(cadenaBuscar))
                .Select(x => new RubroDto
                {
                    Id = x.Id,
                    Descripcion = x.Descripcion,
                    ActivarLimiteVenta = x.ActivarLimiteVenta,
                    LimiteVenta = x.LimiteVenta,
                    ActivarHoraVenta = x.ActivarHoraVenta,
                    HoraLimiteVentaDesde = x.HoraLimiteVentaDesde,
                    HoraLimiteVentaHasta = x.HoraLimiteVentaHasta,
                    Eliminado = x.EstaEliminado
                })
                .OrderBy(x => x.Descripcion)
                .ToList();
        }

        public bool VerificarSiExiste(string datoVerificar, long? entidadId = null)
        {
            return entidadId.HasValue
                ? _unidadDeTrabajo.RubroRepositorio.Obtener(x => !x.EstaEliminado
                                                                     && x.Id != entidadId.Value
                                                                     && x.Descripcion.Equals(datoVerificar,
                                                                         StringComparison.CurrentCultureIgnoreCase))
                    .Any()
                : _unidadDeTrabajo.RubroRepositorio.Obtener(x => !x.EstaEliminado
                                                                 && x.Descripcion.Equals(datoVerificar,
                                                                     StringComparison.CurrentCultureIgnoreCase))
                    .Any();
        }
    }
}
