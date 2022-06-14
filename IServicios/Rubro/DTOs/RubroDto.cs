using System;
using IServicio.BaseDto;

namespace IServicio.Rubro.DTOs
{
    public class RubroDto : DtoBase
    {
        public string Descripcion { get; set; }

        public bool ActivarLimiteVenta { get; set; }

        public string ActivarLimiteVentaStr => ActivarLimiteVenta ? "SI" : "NO";

        public decimal LimiteVenta { get; set; }

        public bool ActivarHoraVenta { get; set; }

        public string ActivarHoraVentaStr => ActivarHoraVenta ? "SI" : "NO";

        public DateTime HoraLimiteVentaDesde { get; set; }

        public string HoraLimiteVentaDesdeStr => HoraLimiteVentaDesde.ToShortTimeString();

        public DateTime HoraLimiteVentaHasta { get; set; }

        public string HoraLimiteVentaHastaStr => HoraLimiteVentaHasta.ToShortTimeString();
    }
}
