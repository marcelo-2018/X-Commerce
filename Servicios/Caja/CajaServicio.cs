using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Dominio.UnidadDeTrabajo;
using IServicios.Caja;
using IServicios.Caja.DTOs;
using Servicios.Base;

namespace Servicios.Caja
{
    public class CajaServicio : ICajaServicio
    {
        private readonly IUnidadDeTrabajo _unidadDeTrabajo;

        public CajaServicio(IUnidadDeTrabajo unidadDeTrabajo)
        {
            _unidadDeTrabajo = unidadDeTrabajo;
        }

        public void Abrir(long usuarioId, decimal monto, DateTime fecha)
        {
            var nuevaCaja = new Dominio.Entidades.Caja
            {
                UsuarioAperturaId = usuarioId,
                FechaApertura = fecha,
                MontoInicial = monto,
                //-------------------------------------//
                UsuarioCierreId = (long?)null,
                FechaCierre = (DateTime?)null,
                MontoCierre = (decimal?)null,
                //-------------------------------------//
                TotalEntradaEfectivo = 0m,
                TotalEntradaTarjeta = 0m,
                TotalEntradaCtaCte = 0m,
                TotalEntradaCheque = 0m,
                TotalSalidaEfectivo = 0m,
                TotalSalidaCheque = 0m,
                TotalSalidaCtaCte = 0m,
                TotalSalidaTarjeta = 0m,
                //-------------------------------------//
                EstaEliminado = false
            };

            _unidadDeTrabajo.CajaRepositorio.Insertar(nuevaCaja);
            _unidadDeTrabajo.Commit();
        }

        public void Cerrar(CajaDto entidad, long? usuarioId, decimal? monto, DateTime? fecha)
        {
            var dto = (CajaDto)entidad;

            var cierreCaja = new Dominio.Entidades.Caja
            {
                UsuarioAperturaId = dto.UsuarioAperturaId,
                FechaApertura = dto.FechaApertura,
                MontoInicial = dto.MontoApertura,
                //-------------------------------------//
                UsuarioCierreId = usuarioId,
                FechaCierre = fecha,
                MontoCierre = monto,
                //-------------------------------------//
                TotalEntradaEfectivo = dto.TotalEntradaEfectivo,
                TotalEntradaTarjeta = dto.TotalEntradaTarjeta,
                TotalEntradaCtaCte = dto.TotalEntradaCtaCte,
                TotalEntradaCheque = dto.TotalEntradaCheque,
                TotalSalidaEfectivo = dto.TotalSalidaEfectivo,
                TotalSalidaCheque = dto.TotalSalidaCheque,
                TotalSalidaCtaCte = dto.TotalSalidaCtaCte,
                TotalSalidaTarjeta = dto.TotalSalidaTarjeta,
                //-------------------------------------//
            };

            _unidadDeTrabajo.CajaRepositorio.Insertar(cierreCaja);
            _unidadDeTrabajo.Commit();
        }

        public IEnumerable<CajaDto> Obtener(string cadenaBuscar, bool filtroPorFecha, DateTime fechaDesde, DateTime fechaHasta)
        {
           Expression<Func<Dominio.Entidades.Caja, bool>> filtro = x =>
               !x.EstaEliminado && x.UsuarioApertura.Nombre.Contains(cadenaBuscar);

           var _fechaDesde = new DateTime(fechaDesde.Year, fechaDesde.Month, fechaDesde.Day, 0, 0, 0);
           var _fechaHasta = new DateTime(fechaHasta.Year, fechaHasta.Month, fechaHasta.Day, 23, 59, 59);

           if (filtroPorFecha)
           { 
               filtro = filtro.And(X => X.FechaApertura >= _fechaDesde && X.FechaApertura <= _fechaHasta);
           }

           return _unidadDeTrabajo.CajaRepositorio.Obtener(filtro, "UsuarioApertura, UsuarioCierre")
               .Select(x => new CajaDto()
               {
                   Id = x.Id,
                   //--------------------------------------------------------------------------//
                   UsuarioAperturaId = x.UsuarioAperturaId,
                   UsuarioApertura = x.UsuarioApertura.Nombre,
                   FechaApertura = x.FechaApertura,
                   MontoApertura = x.MontoInicial,
                   //--------------------------------------------------------------------------//
                   UsuarioCierreId = x.UsuarioCierreId,
                   UsuarioCierre = x.UsuarioCierreId.HasValue ? x.UsuarioCierre.Nombre : "----",
                   FechaCierre = x.FechaCierre,
                   MontoCierre = x.MontoCierre,
                   //--------------------------------------------------------------------------//
                   TotalEntradaEfectivo = x.TotalEntradaEfectivo,
                   TotalEntradaCheque = x.TotalEntradaCheque,
                   TotalEntradaCtaCte = x.TotalEntradaCtaCte,
                   TotalEntradaTarjeta = x.TotalEntradaTarjeta,
                   TotalSalidaEfectivo = x.TotalSalidaEfectivo,
                   TotalSalidaCheque = x.TotalSalidaCheque,
                   TotalSalidaCtaCte = x.TotalSalidaCtaCte,
                   TotalSalidaTarjeta = x.TotalSalidaTarjeta,
                   //--------------------------------------------------------------------------//
                   Eliminado = x.EstaEliminado

               }).ToList();
        }

        //Cierre
        public CajaDto Obtener(long cajaId)
        {
            return _unidadDeTrabajo.CajaRepositorio.Obtener(x => x.Id == cajaId,
                    "UsuarioApertura, UsuarioCierre, DetalleCajas, Movimientos, Movimientos.Comprobante, Movimientos.Comprobante.Empleado")
                .Select(x => new CajaDto()
                {
                    Id = x.Id,
                    //--------------------------------------------------------------------------//
                    UsuarioAperturaId = x.UsuarioAperturaId,
                    UsuarioApertura = x.UsuarioApertura.Nombre,
                    FechaApertura = x.FechaApertura,
                    MontoApertura = x.MontoInicial,
                    //--------------------------------------------------------------------------//
                    UsuarioCierreId = x.UsuarioCierreId,
                    UsuarioCierre = x.UsuarioCierreId.HasValue ? x.UsuarioCierre.Nombre : "----",
                    FechaCierre = x.FechaCierre,
                    MontoCierre = x.MontoCierre,
                    //--------------------------------------------------------------------------//
                    TotalEntradaEfectivo = x.TotalEntradaEfectivo,
                    TotalEntradaCheque = x.TotalEntradaCheque,
                    TotalEntradaCtaCte = x.TotalEntradaCtaCte,
                    TotalEntradaTarjeta = x.TotalEntradaTarjeta,
                    TotalSalidaEfectivo = x.TotalSalidaEfectivo,
                    TotalSalidaCheque = x.TotalSalidaCheque,
                    TotalSalidaCtaCte = x.TotalSalidaCtaCte,
                    TotalSalidaTarjeta = x.TotalSalidaTarjeta,
                    //--------------------------------------------------------------------------//
                    Eliminado = x.EstaEliminado,

                    Detalles = x.DetalleCajas.Select(d => new CajaDetalleDto
                    {
                        Monto = d.Monto,
                        TipoPago = d.TipoPago,
                        Eliminado = d.EstaEliminado,
                    }).ToList(),
                    Comprobantes = x.Movimientos.Select(c => new ComprobanteCajaDto
                    {
                        Fecha = c.Comprobante.Fecha,
                        Numero = c.Comprobante.Numero,
                        Total = c.Comprobante.Total,
                        Vendedor = $"{c.Comprobante.Empleado.Apellido} {c.Comprobante.Empleado.Nombre}",
                        Eliminado = c.Comprobante.EstaEliminado,
                    }).ToList(),
                }).FirstOrDefault();
        }


        public decimal ObtenerMontoCajaAnterior(long usuarioId)
        {
            var cajasUsuarios = _unidadDeTrabajo.CajaRepositorio
                .Obtener(x => x.UsuarioAperturaId == usuarioId && x.UsuarioCierre != null);

            var ultimaCaja = cajasUsuarios.Where(x => x.FechaApertura == cajasUsuarios
                .Max(f => f.FechaApertura)).LastOrDefault();

            return ultimaCaja == null ? 0m : ultimaCaja.MontoCierre.Value;
        }

        public bool VerificarSiExisteCajaAbierta(long usuarioId)
        {
            return _unidadDeTrabajo.CajaRepositorio.Obtener(x => 
                x.UsuarioAperturaId == usuarioId && x.UsuarioCierreId == null).Any();
        }
    }
}
