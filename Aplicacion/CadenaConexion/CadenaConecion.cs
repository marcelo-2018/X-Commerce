namespace Aplicacion.CadenaConexion
{
    public static class CadenaConecion
    {
        // Atributos
        private const string Servidor = @"Marcelo-PC"; // Cambia
        private const string BaseDatos = @"CommerceDb";
        private const string Usuario = @"sa";
        private const string Password = @"cucatrap10"; // Cambia

        // Propiedad
        public static string ObtenerCadenaSql => $"Data Source={Servidor}; " +
                                                 $"Initial Catalog={BaseDatos}; " +
                                                 $"User Id={Usuario}; " +
                                                 $"Password={Password};";

        public static string ObtenerCadenaWin => $"Data Source={Servidor}; " +
                                                 $"Initial Catalog={BaseDatos}; " +
                                                 $"Integrated Security=true;";
    }
}
