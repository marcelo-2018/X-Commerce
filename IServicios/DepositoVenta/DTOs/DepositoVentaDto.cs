using IServicio.BaseDto;

namespace IServicios.DepositoVenta.DTOs
{
    public class DepositoVentaDto : DtoBase
    {
        public string Descripcion { get; set; }

        public string Ubicacion { get; set; }
    }
}
