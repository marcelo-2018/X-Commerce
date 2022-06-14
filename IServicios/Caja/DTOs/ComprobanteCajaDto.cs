using System;
using IServicio.BaseDto;

namespace IServicios.Caja.DTOs
{
    public class ComprobanteCajaDto : DtoBase
    {
        public string Vendedor { get; set; }

        public int Numero { get; set; }

        public DateTime Fecha { get; set; }

        public string FechaStr => Fecha.ToShortDateString();

        public decimal Total { get; set; }

        public string TotalStr => Total.ToString("C");
    }
}
