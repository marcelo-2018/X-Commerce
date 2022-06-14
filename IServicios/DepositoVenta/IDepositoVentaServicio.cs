namespace IServicios.DepositoVenta
{
    public interface IDepositoVentaServicio : IServicio.Base.IServicio
    {
        bool VerificarSiExiste(string datoVerificar, long? entidadId = null);
    }
}
