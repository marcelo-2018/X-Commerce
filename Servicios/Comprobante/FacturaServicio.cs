using Dominio.UnidadDeTrabajo;
using IServicios.Comprobante.DTOs;
using IServicios.Factura;
using Servicios.Comprobante;
using System.Collections.Generic;
using System.Linq;
using Aplicacion.Constantes;
using IServicio.Persona.DTOs;

namespace Servicios.Factura
{
    public class FacturaServicio : ComprobanteServicio, IFacturaServicio
    {
        public FacturaServicio(IUnidadDeTrabajo unidadDeTrabajo)
            : base(unidadDeTrabajo)
        {
        }

        public IEnumerable<ComprobantePendienteDto> ObtenerPendientesPago()
        {
            return _unidadDeTrabajo.FacturaRepositorio
                .Obtener(x => !x.EstaEliminado
                              && x.Estado == Estado.Pendiente, "Cliente, DetalleComprobantes")
                .Select(x => new ComprobantePendienteDto
                {
                    Id = x.Id,
                    Cliente = new ClienteDto()
                    {
                        Id = x.ClienteId,
                        Dni = x.Cliente.Dni,
                        Nombre = x.Cliente.Nombre,
                        Apellido = x.Cliente.Apellido,
                        Telefono = x.Cliente.Telefono,
                        Direccion = x.Cliente.Direccion,
                        Eliminado = x.Cliente.EstaEliminado,
                        ActivarCtaCte = x.Cliente.ActivarCtaCte,
                        TieneLimiteCompra = x.Cliente.TieneLimiteCompra,
                        MontoMaximoCtaCte = x.Cliente.MontoMaximoCtaCte,
                    },
                    ClienteApyNom = $"{x.Cliente.Apellido} {x.Cliente.Nombre}",
                    Direccion = x.Cliente.Direccion,
                    Dni = x.Cliente.Dni,
                    Telefono = x.Cliente.Telefono,
                    Fecha = x.Fecha,
                    MontoPagar = x.Total,
                    Numero = x.Numero,
                    Eliminado = x.EstaEliminado,
                    Items = x.DetalleComprobantes.Select(d => new DetallePendienteDto
                    {
                        Id = d.Id,
                        Descripcion = d.Descripcion,
                        Cantidad = d.Cantidad,
                        Precio = d.Precio,
                        SubTotal = d.SubTotal,
                        Eliminado = d.EstaEliminado,
                    }).ToList()
                })
                .OrderByDescending(x => x.Fecha)
                .ToList();
        }
    }
}
