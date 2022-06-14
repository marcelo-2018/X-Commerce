namespace IServicios.PuestoDeTrabajo
{
    public interface IPuestoDeTrabajoServicio : IServicio.Base.IServicio
    {
        int ObtenerSiguienteCodigo();

        bool VerificarSiExiste(string datoVerificar, long? entidadId = null);
    }
}
