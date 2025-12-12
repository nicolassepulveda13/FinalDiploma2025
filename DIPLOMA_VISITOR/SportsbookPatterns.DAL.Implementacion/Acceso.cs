using System.Data;
using Microsoft.Data.SqlClient;
using System.Configuration;

namespace SportsbookPatterns.DAL.Implementacion
{
    public class Acceso
    {
        private SqlConnection? conexion;

        private void AbrirConexion()
        {
            string conexionString = ConfigurationManager.ConnectionStrings["DefaultConnection"]?.ConnectionString 
                ?? throw new Exception("Connection string 'DefaultConnection' no encontrada en app.config");
            
            try
            {
                conexion = new SqlConnection(conexionString);
                conexion.Open();
            }
            catch (SqlException ex)
            {
                throw new Exception($"Error al conectar con la base de datos. Verifique que SQL Server esté ejecutándose y que la base de datos 'SportsbookDB' exista. Detalles: {ex.Message}", ex);
            }
        }

        private void CerrarConexion()
        {
            if (conexion != null)
            {
                conexion.Close();
                conexion = null;
                GC.Collect();
            }
        }

        public DataTable Leer(string query, List<SqlParameter>? parametros)
        {
            DataTable filas = new DataTable();
            SqlDataAdapter adaptador = new SqlDataAdapter();
            adaptador.SelectCommand = new SqlCommand();
            adaptador.SelectCommand.CommandType = CommandType.Text;
            adaptador.SelectCommand.CommandText = query;
            
            AbrirConexion();
            adaptador.SelectCommand.Connection = conexion;
            
            if (parametros != null && parametros.Count > 0)
            {
                adaptador.SelectCommand.Parameters.AddRange(parametros.ToArray());
            }
            
            adaptador.Fill(filas);
            adaptador.Dispose();
            CerrarConexion();
            
            return filas;
        }

        public int Escribir(string query, List<SqlParameter>? parametros)
        {
            int filasAfectadas = 0;
            SqlCommand cm = new SqlCommand();
            cm.CommandType = CommandType.Text;
            cm.CommandText = query;
            
            AbrirConexion();
            cm.Connection = conexion;
            
            if (parametros != null && parametros.Count > 0)
            {
                cm.Parameters.AddRange(parametros.ToArray());
            }
            
            try
            {
                object returnObj = cm.ExecuteScalar();
                if (returnObj != null)
                {
                    int.TryParse(returnObj.ToString(), out filasAfectadas);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al ejecutar la consulta: " + ex.Message);
            }
            
            cm.Dispose();
            CerrarConexion();
            return filasAfectadas;
        }

        public SqlParameter CrearParametro(string nombre, int valor)
        {
            SqlParameter parametro = new SqlParameter();
            parametro.DbType = DbType.Int32;
            parametro.Value = valor;
            parametro.ParameterName = nombre;
            return parametro;
        }

        public SqlParameter CrearParametro(string nombre, decimal valor)
        {
            SqlParameter parametro = new SqlParameter();
            parametro.DbType = DbType.Decimal;
            parametro.Value = valor;
            parametro.ParameterName = nombre;
            return parametro;
        }

        public SqlParameter CrearParametro(string nombre, string valor)
        {
            SqlParameter parametro = new SqlParameter();
            parametro.DbType = DbType.String;
            parametro.Value = valor ?? (object)DBNull.Value;
            parametro.ParameterName = nombre;
            return parametro;
        }

        public SqlParameter CrearParametro(string nombre, DateTime valor)
        {
            SqlParameter parametro = new SqlParameter();
            parametro.DbType = DbType.DateTime;
            parametro.Value = valor;
            parametro.ParameterName = nombre;
            return parametro;
        }

        public SqlParameter CrearParametro(string nombre, bool valor)
        {
            SqlParameter parametro = new SqlParameter();
            parametro.DbType = DbType.Boolean;
            parametro.Value = valor;
            parametro.ParameterName = nombre;
            return parametro;
        }

        public SqlParameter CrearParametroNull(string nombre, DbType tipo)
        {
            SqlParameter parametro = new SqlParameter();
            parametro.DbType = tipo;
            parametro.Value = DBNull.Value;
            parametro.ParameterName = nombre;
            return parametro;
        }
    }
}
