using System;
using System.Data;
using System.Data.SqlClient;

namespace Sistemas.Data.Repositories.Sqlservers
{
    public abstract class SqlDatosRepository
    {
        protected string MBase = "";
        protected string MCadenaConexion = "";
        protected IDbConnection MConexion;
        protected string MPassword = "";

        protected string MServidor = "";
        // end Ejecutar

        // =======================================================


        protected IDbTransaction MTransaccion;
        protected string MUsuario = "";

        // =======================================================


        // Nombre del equipo servidor de datos.
        public string Servidor
        {
            get => MServidor;
            set => MServidor = value;
        }
        // end Servidor

        // =======================================================

        // Nombre de la base de datos a utilizar.
        public string Base
        {
            get => MBase;
            set => MBase = value;
        }
        // end Base

        // =======================================================

        // Nombre del Usuario de la BD.
        public string Usuario
        {
            get => MUsuario;
            set => MUsuario = value;
        }
        // end Usuario

        // =======================================================

        // Password del Usuario de la BD.
        public string Password
        {
            get => MPassword;
            set => MPassword = value;
        }
        // end Password

        // =======================================================

        // Cadena de conexión completa a la base.
        public abstract string CadenaConexion { get; set; }

        // =======================================================


        // Crea u obtiene un objeto para conectarse a la base de datos.
        protected IDbConnection Conexion
        {
            get
            {
                // si aun no tiene asignada la cadena de conexion lo hace
                if (MConexion == null)
                    MConexion = CrearConexion(CadenaConexion);

                // si no esta abierta aun la conexion, lo abre
                if (MConexion.State != ConnectionState.Open)
                    MConexion.Open();

                // retorna la conexion en modo interfaz, para que se adapte a cualquier implementacion de los distintos fabricantes de motores de bases de datos
                return MConexion;
            }
        }

        public bool EnTransaccion => MTransaccion != null;   //? false : true;
        // end get
        // end Conexion

        // =======================================================


        // Obtiene un DataSet a partir de un Procedimiento Almacenado.
        public DataSet TraerDataSet(string procedimientoAlmacenado)
        {
            var mDataSet = new DataSet();
            CrearDataAdapter(procedimientoAlmacenado).Fill(mDataSet);
            return mDataSet;
        }
        // end TraerDataset

        // =======================================================

        // Obtiene un DataSet a partir de un Procedimiento Almacenado y sus parámetros.
        public DataSet TraerDataSet(string procedimientoAlmacenado, params object[] args)
        {
            var mDataSet = new DataSet();
            CrearDataAdapter(procedimientoAlmacenado, args).Fill(mDataSet);
            return mDataSet;
        }
        // end TraerDataset

        // =======================================================

        // Obtiene un DataSet a partir de un Query Sql.
        public DataSet TraerDataSetSql(string comandoSql)
        {
            var mDataSet = new DataSet();
            CrearDataAdapterSql(comandoSql).Fill(mDataSet);
            return mDataSet;
        }
        // end TraerDataSetSql

        // =======================================================

        // Obtiene un DataTable a partir de un Procedimiento Almacenado.
        public DataTable TraerDataTable(string procedimientoAlmacenado)
        {
            return TraerDataSet(procedimientoAlmacenado).Tables[0].Copy();
        }
        // end TraerDataTable

        // =======================================================

        // Obtiene un DataSet a partir de un Procedimiento Almacenado y sus parámetros.
        public DataTable TraerDataTable(string procedimientoAlmacenado, params object[] args)
        {
            return TraerDataSet(procedimientoAlmacenado, args).Tables[0].Copy();
        }
        // end TraerDataTable

        // =======================================================

        // Obtiene un DataTable a partir de un Query SQL
        public DataTable TraerDataTableSql(string comandoSql)
        {
            return TraerDataSetSql(comandoSql).Tables[0].Copy();
        }
        // end TraerDataTableSqlDataTable

        // =======================================================

        // Obtiene un DataReader a partir de un Procedimiento Almacenado.
        public IDataReader TraerDataReader(string procedimientoAlmacenado)
        {
            var com = Comando(procedimientoAlmacenado);
            return com.ExecuteReader();
        }
        // end TraerDataReader

        // =======================================================

        // Obtiene un DataReader a partir de un Procedimiento Almacenado y sus parámetros.
        public IDataReader TraerDataReader(string procedimientoAlmacenado, params object[] args)
        {
            var com = Comando(procedimientoAlmacenado);
            CargarParametros(com, args);
            return com.ExecuteReader();
        }
        // end TraerDataReader

        // =======================================================

        // Obtiene un DataReader a partir de un Procedimiento Almacenado.
        public IDataReader TraerDataReaderSql(string comandoSql__1)
        {
            var com = ComandoSql(comandoSql__1);
            return com.ExecuteReader();
        }
        // end TraerDataReaderSql

        // =======================================================

        // Obtiene un Valor Escalar a partir de un Procedimiento Almacenado. Solo funciona con SP's que tengan
        // definida variables de tipo output, para funciones escalares mas abajo se declara un metodo
        public object TraerValorOutput(string procedimientoAlmacenado)
        {
            // asignar el string sql al command
            var com = Comando(procedimientoAlmacenado);
            // ejecutar el command
            com.ExecuteNonQuery();
            // declarar variable de retorno
            object resp = null;

            // recorrer los parametros del SP
            foreach (IDbDataParameter par in com.Parameters)
                // si tiene parametros de tipo IO/Output retornar ese valor
                if (par.Direction == ParameterDirection.InputOutput || par.Direction == ParameterDirection.Output)
                    resp = par.Value;

            return resp;
        }
        // end TraerValor

        // =======================================================

        // Obtiene un Valor a partir de un Procedimiento Almacenado, y sus parámetros.
        public object TraerValorOutput(string procedimientoAlmacenado, params object[] args)
        {
            // asignar el string sql al command
            var com = Comando(procedimientoAlmacenado);
            // cargar los parametros del SP
            CargarParametros(com, args);
            // ejecutar el command
            com.ExecuteNonQuery();
            // declarar variable de retorno
            object resp = null;

            // recorrer los parametros del SP
            foreach (IDbDataParameter par in com.Parameters)
                // si tiene parametros de tipo IO/Output retornar ese valor
                if (par.Direction == ParameterDirection.InputOutput || par.Direction == ParameterDirection.Output)
                    resp = par.Value;

            return resp;
        }
        // end TraerValor

        // =======================================================

        // Obtiene un Valor Escalar a partir de un Procedimiento Almacenado.
        public object TraerValorOutputSql(string comadoSql)
        {
            // asignar el string sql al command
            var com = ComandoSql(comadoSql);
            // ejecutar el command
            com.ExecuteNonQuery();
            // declarar variable de retorno
            object resp = null;

            // recorrer los parametros del Query (uso tipico envio de varias sentencias sql en el mismo command)
            foreach (IDbDataParameter par in com.Parameters)
                // si tiene parametros de tipo IO/Output retornar ese valor
                if (par.Direction == ParameterDirection.InputOutput || par.Direction == ParameterDirection.Output)
                    resp = par.Value;

            return resp;
        }
        // end TraerValor

        // =======================================================

        // Obtiene un Valor de una funcion Escalar a partir de un Procedimiento Almacenado.
        public object TraerValorEscalar(string procedimientoAlmacenado)
        {
            var com = Comando(procedimientoAlmacenado);
            return com.ExecuteScalar();
        }
        // end TraerValorEscalar

        // =======================================================

        /// Obtiene un Valor de una funcion Escalar a partir de un Procedimiento Almacenado, con Params de Entrada
        public object TraerValorEscalar(string procedimientoAlmacenado, params object[] args)
        {
            var com = Comando(procedimientoAlmacenado);
            CargarParametros(com, args);
            return com.ExecuteScalar();
        }
        // end TraerValorEscalar

        // =======================================================

        // Obtiene un Valor de una funcion Escalar a partir de un Query SQL
        public object TraerValorEscalarSql(string comandoSql1)
        {
            var com = ComandoSql(comandoSql1);
            return com.ExecuteScalar();
        }
        // end TraerValorEscalarSql

        // =======================================================


        protected abstract IDbConnection CrearConexion(string cadena);
        protected abstract IDbCommand Comando(string procedimientoAlmacenado);
        protected abstract IDbCommand ComandoSql(string comandoSqls);
        protected abstract IDataAdapter CrearDataAdapter(string procedimientoAlmacenado, params object[] args);
        protected abstract IDataAdapter CrearDataAdapterSql(string comandoSql);
        protected abstract void CargarParametros(IDbCommand comando, object[] args);

        // metodo sobrecargado para autenticarse contra el motor de BBDD
        public bool Autenticar()
        {
            try
            {
                if (Conexion.State != ConnectionState.Open)
                    Conexion.Open();
                return true;
            }
            catch (SqlException exSQL)
            {
                throw exSQL;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        // end Autenticar Object[] args);

        // =======================================================

        // metodo sobrecargado para autenticarse contra el motor de BBDD
        public bool Autenticar(string vUsuario, string vPassword)
        {
            MUsuario = vUsuario;
            MPassword = vPassword;
            MConexion = CrearConexion(CadenaConexion);

            MConexion.Open();
            return true;
        }
        // end Autenticar

        // =======================================================

        // cerrar conexion
        public void CerrarConexion()
        {
            if (Conexion.State != ConnectionState.Closed)
                MConexion.Close();
        }

        // end CerrarConexion

        // =======================================================

        // Ejecuta un Procedimiento Almacenado en la base.
        public int Ejecutar(string procedimientoAlmacenado)
        {
            return Comando(procedimientoAlmacenado).ExecuteNonQuery();
        }
        // end Ejecutar

        // =======================================================

        // Ejecuta un query sql
        public int EjecutarSql(string comandoSql1)
        {
            return ComandoSql(comandoSql1).ExecuteNonQuery();
        }
        // end Ejecutar

        // =======================================================

        // Ejecuta un Procedimiento Almacenado en la base, utilizando los parámetros.
        public int Ejecutar(string procedimientoAlmacenado, params object[] args)
        {
            var com = Comando(procedimientoAlmacenado);
            CargarParametros(com, args);
            var resp = com.ExecuteNonQuery();
            for (var i = 0; i <= com.Parameters.Count - 1; i++)
            {
                var par = (IDbDataParameter)com.Parameters[i];
                if (par.Direction == ParameterDirection.InputOutput || par.Direction == ParameterDirection.Output)
                    args.SetValue(par.Value, i - 1);
            }

            // end for
            return resp;
        }


        // Comienza una Transacción en la base en uso.
        public void IniciarTransaccion()
        {
            try
            {
                MTransaccion = Conexion.BeginTransaction();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        // end IniciarTransaccion

        // =======================================================

        // Confirma la transacción activa.
        public void TerminarTransaccion()
        {
            try
            {
                MTransaccion.Commit();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                MTransaccion = null;
            }
        }
        // end TerminarTransaccion

        // =======================================================

        // Cancela la transacción activa.
        public void AbortarTransaccion()
        {
            try
            {
                MTransaccion.Rollback();
            }
            finally
            {
                MTransaccion = null;
            }
        }
    }
}
