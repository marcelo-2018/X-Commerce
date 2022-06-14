namespace IServicios.Tarjeta
{
    public interface ITarjetaServicio : IServicio.Base.IServicio
    {
        bool VerificarSiExiste(string datoVerificar, long? entidadId = null);
    }
}
