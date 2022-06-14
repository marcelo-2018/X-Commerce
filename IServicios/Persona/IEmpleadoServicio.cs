namespace IServicio.Persona
{
    public interface IEmpleadoServicio : IPersonaServicio
    {
        int ObtenerSiguienteLegajo();

        bool VerificarSiExiste(string datoVerificar, long? entidadId = null);
    }
}
