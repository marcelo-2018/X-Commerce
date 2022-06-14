using System;

namespace IServicios.Articulo.DTOs
{
    public class PreciosDto
    {
        public DateTime Fecha { get; set; }

        public string FechaStr => Fecha.ToShortDateString();

        public decimal Precio { get; set; }

        public string PrecioStr => Precio.ToString();

        public string ListaPrecio { get; set; }
    }
}
