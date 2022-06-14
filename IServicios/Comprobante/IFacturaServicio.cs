using System.Collections.Generic;
using IServicios.Comprobante;
using IServicios.Comprobante.DTOs;

namespace IServicios.Factura
{
    public interface IFacturaServicio : IComprobanteServicio
    {
        IEnumerable<ComprobantePendienteDto> ObtenerPendientesPago();
    }
}
