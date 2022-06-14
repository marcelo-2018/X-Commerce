using System.Collections.Generic;
using IServicios.Articulo.DTOs;

namespace IServicio.Articulo
{
    public interface IArticuloServicio : Base.IServicio
    {
        int ObtenerSiguienteNroCodigo();

        bool VerificarSiExiste(string datoVerificar, long? entidadId = null);

        int ObtenerCantidadArticulos();

        ArticuloVentaDto ObtenerPorCodigo(string codigo, long listaPrecioId, long depositoId);

        IEnumerable<ArticuloVentaDto> ObtenerLookUp(string cadenaBuscar, long listaPrecioId);
    }
}
