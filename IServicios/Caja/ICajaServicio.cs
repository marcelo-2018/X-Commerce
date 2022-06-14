using System;
using System.Collections.Generic;
using IServicios.Caja.DTOs;

namespace IServicios.Caja
{
    public interface ICajaServicio
    {
        bool VerificarSiExisteCajaAbierta(long usuarioId);

        IEnumerable<CajaDto> Obtener(string cadenaBuscar, bool filtroPorFecha, DateTime fechaDesde,
            DateTime fechaHasta);

        CajaDto Obtener(long cajaId);

        decimal ObtenerMontoCajaAnterior(long usuarioId);

        void Abrir(long usuarioId, decimal monto, DateTime fecha);

        void Cerrar(CajaDto entidad, long? usuarioId, decimal? monto, DateTime? fecha);
    }
}
