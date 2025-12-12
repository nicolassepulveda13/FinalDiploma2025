using System.Data;
using Microsoft.Data.SqlClient;
using SportsbookPatterns.BE;
using SportsbookPatterns.DAL;

namespace SportsbookPatterns.DAL
{
    public class TransaccionRetiroRepository : ITransaccionRetiroRepository
    {
        private Acceso acceso;

        public TransaccionRetiroRepository(Acceso acceso)
        {
            this.acceso = acceso;
        }

        public TransaccionRetiro? GetByTransaccionId(int transaccionId)
        {
            TransaccionRetiro? retiro = null;
            string query = "SELECT TransaccionRetiroId, TransaccionId, MetodoRetiro, NumeroCuentaDestino, BancoDestino, Comision, FechaProcesamiento FROM TransaccionRetiro WHERE TransaccionId = @TransaccionId";
            List<SqlParameter> param = new List<SqlParameter>();
            param.Add(acceso.CrearParametro("@TransaccionId", transaccionId));

            DataTable dt = acceso.Leer(query, param);

            if (dt.Rows.Count > 0)
            {
                retiro = CastTransaccionRetiro(dt.Rows[0]);
            }

            return retiro;
        }

        public List<TransaccionRetiro> GetAll()
        {
            List<TransaccionRetiro> retiros = new List<TransaccionRetiro>();
            string query = "SELECT TransaccionRetiroId, TransaccionId, MetodoRetiro, NumeroCuentaDestino, BancoDestino, Comision, FechaProcesamiento FROM TransaccionRetiro";
            
            DataTable dt = acceso.Leer(query, null);

            foreach (DataRow row in dt.Rows)
            {
                retiros.Add(CastTransaccionRetiro(row));
            }
            
            return retiros;
        }

        public int Insert(TransaccionRetiro retiro)
        {
            string query = @"INSERT INTO TransaccionRetiro (TransaccionId, MetodoRetiro, NumeroCuentaDestino, BancoDestino, Comision, FechaProcesamiento) 
                            VALUES (@TransaccionId, @MetodoRetiro, @NumeroCuentaDestino, @BancoDestino, @Comision, @FechaProcesamiento);
                            SELECT CAST(SCOPE_IDENTITY() as int);";
            
            List<SqlParameter> param = new List<SqlParameter>();
            param.Add(acceso.CrearParametro("@TransaccionId", retiro.TransaccionId));
            param.Add(acceso.CrearParametro("@MetodoRetiro", retiro.MetodoRetiro));
            param.Add(acceso.CrearParametro("@NumeroCuentaDestino", retiro.NumeroCuentaDestino ?? string.Empty));
            param.Add(acceso.CrearParametro("@BancoDestino", retiro.BancoDestino ?? string.Empty));
            param.Add(acceso.CrearParametro("@Comision", retiro.Comision));
            if (retiro.FechaProcesamiento.HasValue)
                param.Add(acceso.CrearParametro("@FechaProcesamiento", retiro.FechaProcesamiento.Value));
            else
                param.Add(acceso.CrearParametroNull("@FechaProcesamiento", DbType.DateTime));

            try
            {
                return acceso.Escribir(query, param);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al crear la transacción de retiro. Error: " + ex.Message);
            }
        }

        public bool Update(TransaccionRetiro retiro)
        {
            string query = @"UPDATE TransaccionRetiro SET MetodoRetiro = @MetodoRetiro, NumeroCuentaDestino = @NumeroCuentaDestino, 
                            BancoDestino = @BancoDestino, Comision = @Comision, FechaProcesamiento = @FechaProcesamiento 
                            WHERE TransaccionId = @TransaccionId";
            
            List<SqlParameter> param = new List<SqlParameter>();
            param.Add(acceso.CrearParametro("@TransaccionId", retiro.TransaccionId));
            param.Add(acceso.CrearParametro("@MetodoRetiro", retiro.MetodoRetiro));
            param.Add(acceso.CrearParametro("@NumeroCuentaDestino", retiro.NumeroCuentaDestino ?? string.Empty));
            param.Add(acceso.CrearParametro("@BancoDestino", retiro.BancoDestino ?? string.Empty));
            param.Add(acceso.CrearParametro("@Comision", retiro.Comision));
            if (retiro.FechaProcesamiento.HasValue)
                param.Add(acceso.CrearParametro("@FechaProcesamiento", retiro.FechaProcesamiento.Value));
            else
                param.Add(acceso.CrearParametroNull("@FechaProcesamiento", DbType.DateTime));

            try
            {
                int resultado = acceso.Escribir(query, param);
                return resultado > 0;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al actualizar la transacción de retiro. Error: " + ex.Message);
            }
        }

        private TransaccionRetiro CastTransaccionRetiro(DataRow row)
        {
            TransaccionRetiro retiro = new TransaccionRetiro();
            retiro.TransaccionRetiroId = Convert.ToInt32(row["TransaccionRetiroId"]);
            retiro.TransaccionId = Convert.ToInt32(row["TransaccionId"]);
            retiro.MetodoRetiro = row["MetodoRetiro"].ToString() ?? string.Empty;
            retiro.NumeroCuentaDestino = row["NumeroCuentaDestino"].ToString();
            retiro.BancoDestino = row["BancoDestino"].ToString();
            retiro.Comision = Convert.ToDecimal(row["Comision"]);
            retiro.FechaProcesamiento = row["FechaProcesamiento"].ToString() != "" ? Convert.ToDateTime(row["FechaProcesamiento"]) : null;
            return retiro;
        }
    }
}

