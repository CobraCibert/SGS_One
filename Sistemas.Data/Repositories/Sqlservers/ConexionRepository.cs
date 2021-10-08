using System.Data.SqlClient;

namespace Sistemas.Data.Repositories.Sqlservers
{
    public abstract class ConexionRepository
    {
        public static SqlServerRepository SQLDatos_SGS;
        //protected SqlConnection _context;
        //protected SqlTransaction _transaction;
        //Constructor
        public ConexionRepository(SqlConnection context, SqlTransaction transaction)
        {
            //var builder = new SqlConnectionStringBuilder(context.ToString());
            //IniciarSesion(builder.DataSource, builder.InitialCatalog, builder.UserID, builder.Password);
            //IniciarSesion(context.DataSource, context.Database, context.Credential.UserId, context.Credential.Password.ToString());
            IniciarSesion("cronos","BD_SGS_Pre_Produccion","bosque","biosain373");
        }
        public static bool IniciarSesion(string nombreServidor, string baseDatos, string usuario, string password)
        {
            try
            {
                // Conexion con la base de datos de SQL

                SQLDatos_SGS = new SqlServerRepository(nombreServidor, baseDatos, usuario, password);
                return SQLDatos_SGS.Autenticar();
            }
            catch (SqlException)
            {
                throw;
            }
        }
        // fin inicializa sesion

        // =======================================================

        public static void FinalizarSesion()
        {
            SQLDatos_SGS.CerrarConexion();
        }
    }
}
