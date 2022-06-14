using IServicio.BaseDto;

namespace IServicios.PuestoDeTrabajo.DTOs
{
    public class PuestoDeTrabajoDto : DtoBase
    {
        public int Codigo { get; set; }

        public string Descripcion { get; set; }
    }
}
