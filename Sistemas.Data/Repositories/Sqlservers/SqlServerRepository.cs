using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Sistemas.Data.Repositories.Sqlservers
{
    //Microsoft.EntityFrameworkCore.SqlServer
    //Microsoft.Extensions.Configuration.Abstractions
    //Microsoft.Extensions.Configuration.EnvironmentVariables
    //Microsoft.Extensions.Configuration.Json
    public class SqlServerRepository : SqlDatosRepository
    {
        //
        // * Continuaremos con el método Comando, procediendo de igual forma que en los anteriores.
        // * En este caso, además, implementaremos un mecanismo de “preservación” de los Comandos creados,
        // * para acelerar su utilización. Esto es, cada procedimiento que sea accedido, se guardará
        // * en memoria hasta que la instancia del objeto se destruya. Para ello, declararemos una variable
        // * como HashTable para la clase, con el modificador Shared (compartida) que permite
        // * persistir la misma entre creaciones de objetos
        //

        private static readonly Hashtable ColComandos = new(); //Hashtable
        // end CrearDataAdapterSql

        // =======================================================

        //
        // * Definiremos dos constructores especializados, uno que reciba como argumentos los valores de Nombre del Servidor
        // * y de base de datos que son necesarios para acceder a datos, y otro que admita directamente la cadena de conexión completa.
        // * Los constructores se definen como procedimientos públicos de nombre New.
        //
        public SqlServerRepository()
        {
            MBase = "";
            MServidor = "";
            MUsuario = "";
            MPassword = "";
        }

        // end DatosSQLServer

        // =======================================================

        public SqlServerRepository(string cadenaConexion)
        {
            MCadenaConexion = cadenaConexion;
        }
        // end DatosSQLServer

        // =======================================================

        public SqlServerRepository(string servidor, string @base)
        {
            MBase = @base;
            MServidor = servidor;
        }
        // end DatosSQLServer

        // =======================================================

        public SqlServerRepository(string servidor, string @base, string usuario, string password)
        {
            try
            {
                MBase = @base;
                MServidor = servidor;
                MUsuario = usuario;
                MPassword = password;
            }
            catch (SqlException)
            {
                throw ;
            }
            catch (Exception )
            {
                throw ;
            }
        }

        // =======================================================

        public sealed override string CadenaConexion
        {
            get
            {
                if (MCadenaConexion.Length == 0)
                {
                    if (MBase.Length != 0 && MServidor.Length != 0)
                    {
                        var sCadena = new StringBuilder("");
                        sCadena.Append("data source=<SERVIDOR>;");
                        sCadena.Append("initial catalog=<BASE>;");
                        sCadena.Append("user id=<USER>;");
                        sCadena.Append("password=<PASSWORD>;");
                        // sCadena.Append("persist security info=True;");
                        // sCadena.Append("user id=sa;packet size=4096");
                        sCadena.Replace("<SERVIDOR>", Servidor);
                        sCadena.Replace("<BASE>", Base);
                        sCadena.Replace("<USER>", Usuario);
                        sCadena.Replace("<PASSWORD>", Password);

                        return sCadena.ToString();
                    }

                    throw new Exception("No se puede establecer la cadena de conexión en la clase DatosSQLServer");
                }

                return MCadenaConexion; // = CadenaConexion
            }
            // end get
            set => MCadenaConexion = value;
        }
        // end set
        // end CadenaConexion

        // =======================================================

        //
        // * Agregue ahora la definición del procedimiento CargarParametros, el cual deberá asignar cada valor
        // * al parámetro que corresponda (considerando que, en el caso de SQLServer©, el parameter 0
        // * siempre corresponde al “return Value” del Procedimiento Almacenado). Por otra parte, en algunos casos,
        // * como la ejecución de procedimientos almacenados que devuelven un valor como parámetro de salida,
        // * la cantidad de elementos en el vector de argumentos, puede no corresponder con la cantidad de parámetros.
        // * Por ello, se decide comparar el indicador con la cantidad de argumentos recibidos, antes de asignar el valor.
        // * protected override void CargarParametros(System.Data.IDbCommand Com, System.Object[] Args)
        //

        protected override void CargarParametros(IDbCommand com, object[] args)
        {
            for (var i = 1; i <= com.Parameters.Count - 1; i++)
            {
                var p = (SqlParameter)com.Parameters[i];
                p.Value = i <= args.Length ? args[i - 1] ?? DBNull.Value : null;
            }
        }
        // end CargarParametros

        // =======================================================

        //
        // * En el procedimiento Comando, se buscará primero si ya existe el comando en dicha Hashtable para retornarla
        // * (convertida en el tipo correcto). Caso contrario, se procederá a la creación del mismo,
        // * y su agregado en el repositorio. Dado que cabe la posibilidad de que ya estemos dentro de una transacción,
        // * es necesario abrir una segunda conexión a la base de datos, para obtener la definición de los parámetros
        // * del procedimiento Almacenado (caso contrario da error, por intentar leer sin tener asignado el
        // * objeto Transaction correspondiente). Además, el comando, obtenido por cualquiera de los mecanismos
        // * debe recibir la conexión y la transacción correspondientes (si no hay Transacción, la variable es null,
        // * y ese es el valor que se le pasa al objeto Command)
        //

        protected override IDbCommand Comando(string procedimientoAlmacenado)
        {
            SqlCommand com;
            if (ColComandos.Contains(procedimientoAlmacenado))
            {
                com = (SqlCommand)ColComandos[procedimientoAlmacenado];
            }
            else
            {
                var con2 = new SqlConnection(CadenaConexion);

                try
                {
                    con2.Open();
                    com = new SqlCommand(procedimientoAlmacenado, con2)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    SqlCommandBuilder.DeriveParameters(com);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    con2.Close();
                    con2.Dispose();
                }

                ColComandos.Add(procedimientoAlmacenado, com);
            }

            // end else
            com.Connection = (SqlConnection)Conexion;
            com.Transaction = (SqlTransaction)MTransaccion;
            return com;
        }
        // end Comando

        // =======================================================

        protected override IDbCommand ComandoSql(string comandoSql__1)
        {
            var com = new SqlCommand(comandoSql__1,
                (SqlConnection)Conexion, (SqlTransaction)MTransaccion);
            return com;
        }
        // end Comando

        // =======================================================

        //
        // * Luego implementaremos CrearConexion, donde simplemente se devuelve una nueva instancia del
        // * objeto Conexión de SqlClient, utilizando la cadena de conexión del objeto.
        //

        protected override IDbConnection CrearConexion(string cadenaConexion)
        {
            return new SqlConnection(cadenaConexion);
        }

        // =======================================================

        // Finalmente, es el turno de definir CrearDataAdapter, el cual aprovecha el método Comando para crear el comando necesario.
        protected override IDataAdapter CrearDataAdapter(string procedimientoAlmacenado,
            params object[] args)
        {
            var da = new SqlDataAdapter(
                (SqlCommand)Comando(procedimientoAlmacenado));
            if (args.Length != 0)
                CargarParametros(da.SelectCommand, args);
            return da;
        }
        // end CrearDataAdapter

        // =======================================================

        // Finalmente, es el turno de definir CrearDataAdapter, el cual aprovecha el método Comando para crear el comando necesario.
        protected override IDataAdapter CrearDataAdapterSql(string comandoSql1)
        {
            var da = new SqlDataAdapter(
                (SqlCommand)ComandoSql(comandoSql1));
            return da;
        }
    }
}
