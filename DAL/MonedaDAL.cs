namespace BD_TRANSACCIONES.DAL
{
    public class MonedaDAL
    {
        private readonly string cadenaConexion;

        public MonedaDAL(IConfiguration configuration)
        {
            cadenaConexion = configuration.GetConnectionString("ConexionBD");
        }

    }
}
