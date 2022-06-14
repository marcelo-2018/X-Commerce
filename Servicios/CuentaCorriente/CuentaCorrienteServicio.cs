﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Aplicacion.Constantes;
using Dominio.Entidades;
using Dominio.UnidadDeTrabajo;
using IServicios.Comprobante;
using IServicios.Comprobante.DTOs;
using IServicios.Contador;
using IServicios.CuentaCorriente;
using IServicios.CuentaCorriente.DTOs;
using Servicios.Base;

namespace Servicios.CuentaCorriente
{
    public class CuentaCorrienteServicio :  ICuentaCorrienteServicio
    {
        private readonly IUnidadDeTrabajo _unidadDeTrabajo;
        private readonly IContadorServicio _contadorServicio;
        private readonly ICtaCteComprobanteServicio _ctaCteComprobanteServicio;

        public CuentaCorrienteServicio(IUnidadDeTrabajo unidadDeTrabajo,
            IContadorServicio contadorServicio, 
            ICtaCteComprobanteServicio ctaCteComprobanteServicio)
        {
            _unidadDeTrabajo = unidadDeTrabajo;
            _contadorServicio = contadorServicio;
            _ctaCteComprobanteServicio = ctaCteComprobanteServicio;
        }

        public IEnumerable<CuentaCorrienteDto> Obtener(long clienteId, DateTime fechaDesde, DateTime fechaHasta, bool solodeuda)
        {
            var _fechaDesde = new DateTime(fechaDesde.Year, fechaDesde.Month, fechaDesde.Day, 0,0,0);
            var _fechaHasta = new DateTime(fechaHasta.Year, fechaHasta.Month, fechaHasta.Day, 23, 59, 59);

            Expression<Func<MovimientoCuentaCorriente, bool>> filtro = x => x.ClienteId == clienteId;

            filtro = filtro.And(x => x.Fecha >= _fechaDesde && x.Fecha <= _fechaHasta);

            if (solodeuda)
            {
                filtro = filtro.And(x => x.TipoMovimiento == TipoMovimiento.Egreso);
            }

            return _unidadDeTrabajo.CuentaCorrienteRepositorio.Obtener(filtro)
                .Select(x => new CuentaCorrienteDto
                {
                    Fecha = x.Fecha,
                    Descripcion = x.Descripcion,
                    Monto = (x.Monto * (int)x.TipoMovimiento),
                }).OrderBy(x => x.Fecha)
                .ToList();
        }

        public decimal ObtenerDeudaCliente(long clienteId)
        {
            var movimientos = _unidadDeTrabajo.CuentaCorrienteRepositorio
                .Obtener(x => !x.EstaEliminado && x.ClienteId == clienteId);

            return movimientos.Sum(x => x.Monto * (int) x.TipoMovimiento);
        }

        public void Pagar(CtaCteComprobanteDto comprobanteDto)
        {
            try
            {
                _ctaCteComprobanteServicio.Insertar(comprobanteDto);
            }
            catch
            {
                throw  new Exception("Ocurrio un error al pagar la cuenta corriente");
            }
        }
    }
}
