using Aplicacion.Constantes;
using IServicios.Comprobante.DTOs;

namespace IServicios.Factura.DTOs
{
    public class FacturaDto : ComprobanteDto
    {
        public long ClienteId { get; set; }
        public long PuestoTrabajoId { get; set; }
        public Estado Estado { get; set; }
        public bool VieneVentas { get; set; }
    }
}
