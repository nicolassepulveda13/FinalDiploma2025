using System.Data;
using Microsoft.Data.SqlClient;
using SportsbookPatterns.BE;

namespace SportsbookPatterns.DAL
{
    public class CuentaUsuarioRepository : ICuentaUsuarioRepository
    {
        private Acceso acceso;

        public CuentaUsuarioRepository(Acceso acceso)
        {
            this.acceso = acceso;
        }

        public decimal GetSaldo(int cuentaId)
        {
            string query = "SELECT Saldo FROM CuentaUsuario WHERE CuentaId = @CuentaId";
            List<SqlParameter> param = new List<SqlParameter>();
            param.Add(acceso.CrearParametro("@CuentaId", cuentaId));

            DataTable dt = acceso.Leer(query, param);

            if (dt.Rows.Count > 0)
            {
                return Convert.ToDecimal(dt.Rows[0]["Saldo"]);
            }
            
            return 0;
        }

        public bool UpdateSaldo(int cuentaId, decimal nuevoSaldo)
        {
            string query = "UPDATE CuentaUsuario SET Saldo = @Saldo, FechaUltimaModificacion = GETDATE() WHERE CuentaId = @CuentaId";
            List<SqlParameter> param = new List<SqlParameter>();
            param.Add(acceso.CrearParametro("@Saldo", nuevoSaldo));
            param.Add(acceso.CrearParametro("@CuentaId", cuentaId));

            try
            {
                int resultado = acceso.Escribir(query, param);
                return resultado > 0;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al actualizar el saldo. Error: " + ex.Message);
            }
        }

        public bool UpdateEstado(int cuentaId, int estadoCuentaId)
        {
            string query = "UPDATE CuentaUsuario SET EstadoCuentaId = @EstadoCuentaId, FechaUltimaModificacion = GETDATE() WHERE CuentaId = @CuentaId";
            List<SqlParameter> param = new List<SqlParameter>();
            param.Add(acceso.CrearParametro("@EstadoCuentaId", estadoCuentaId));
            param.Add(acceso.CrearParametro("@CuentaId", cuentaId));

            try
            {
                int resultado = acceso.Escribir(query, param);
                return resultado > 0;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al actualizar el estado. Error: " + ex.Message);
            }
        }

        public CuentaUsuario? GetByUsuarioId(int usuarioId)
        {
            CuentaUsuario? cuenta = null;
            string query = "SELECT CuentaId, UsuarioId, Saldo, EstadoCuentaId, FechaCreacion, FechaUltimaModificacion FROM CuentaUsuario WHERE UsuarioId = @UsuarioId";
            List<SqlParameter> param = new List<SqlParameter>();
            param.Add(acceso.CrearParametro("@UsuarioId", usuarioId));

            DataTable dt = acceso.Leer(query, param);

            if (dt.Rows.Count > 0)
            {
                cuenta = CastCuentaUsuario(dt.Rows[0]);
            }

            return cuenta;
        }

        public CuentaUsuario? GetById(int cuentaId)
        {
            CuentaUsuario? cuenta = null;
            string query = "SELECT CuentaId, UsuarioId, Saldo, EstadoCuentaId, FechaCreacion, FechaUltimaModificacion FROM CuentaUsuario WHERE CuentaId = @CuentaId";
            List<SqlParameter> param = new List<SqlParameter>();
            param.Add(acceso.CrearParametro("@CuentaId", cuentaId));

            DataTable dt = acceso.Leer(query, param);

            if (dt.Rows.Count > 0)
            {
                cuenta = CastCuentaUsuario(dt.Rows[0]);
            }

            return cuenta;
        }

        public int Insert(CuentaUsuario cuenta)
        {
            string query = @"INSERT INTO CuentaUsuario (UsuarioId, Saldo, EstadoCuentaId, FechaCreacion) 
                            VALUES (@UsuarioId, @Saldo, @EstadoCuentaId, @FechaCreacion);
                            SELECT CAST(SCOPE_IDENTITY() as int);";
            
            List<SqlParameter> param = new List<SqlParameter>();
            param.Add(acceso.CrearParametro("@UsuarioId", cuenta.UsuarioId));
            param.Add(acceso.CrearParametro("@Saldo", cuenta.Saldo));
            param.Add(acceso.CrearParametro("@EstadoCuentaId", cuenta.EstadoCuentaId));
            param.Add(acceso.CrearParametro("@FechaCreacion", cuenta.FechaCreacion));

            try
            {
                return acceso.Escribir(query, param);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al crear la cuenta. Error: " + ex.Message);
            }
        }

        public bool Update(CuentaUsuario cuenta)
        {
            string query = @"UPDATE CuentaUsuario SET Saldo = @Saldo, EstadoCuentaId = @EstadoCuentaId, 
                            FechaUltimaModificacion = GETDATE() WHERE CuentaId = @CuentaId";
            
            List<SqlParameter> param = new List<SqlParameter>();
            param.Add(acceso.CrearParametro("@CuentaId", cuenta.CuentaId));
            param.Add(acceso.CrearParametro("@Saldo", cuenta.Saldo));
            param.Add(acceso.CrearParametro("@EstadoCuentaId", cuenta.EstadoCuentaId));

            try
            {
                int resultado = acceso.Escribir(query, param);
                return resultado > 0;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al actualizar la cuenta. Error: " + ex.Message);
            }
        }

        private CuentaUsuario CastCuentaUsuario(DataRow row)
        {
            CuentaUsuario cuenta = new CuentaUsuario();
            cuenta.CuentaId = Convert.ToInt32(row["CuentaId"]);
            cuenta.UsuarioId = Convert.ToInt32(row["UsuarioId"]);
            cuenta.Saldo = Convert.ToDecimal(row["Saldo"]);
            cuenta.EstadoCuentaId = Convert.ToInt32(row["EstadoCuentaId"]);
            cuenta.FechaCreacion = Convert.ToDateTime(row["FechaCreacion"]);
            cuenta.FechaUltimaModificacion = row["FechaUltimaModificacion"].ToString() != "" 
                ? Convert.ToDateTime(row["FechaUltimaModificacion"]) 
                : null;
            return cuenta;
        }
    }
}

