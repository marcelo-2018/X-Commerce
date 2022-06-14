namespace IServicio.Persona
{
    public interface IClienteServicio : IPersonaServicio
    {
        bool VerificarSiExiste(string datoVerificar, long? entidadId = null);
    }
}
