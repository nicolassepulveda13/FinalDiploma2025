using System.Data;
using Microsoft.Data.SqlClient;
using SportsbookPatterns.BE;
using SportsbookPatterns.DAL.Abstraccion;

namespace SportsbookPatterns.DAL.Implementacion
{
    public class LogTransaccionRepository : ILogTransaccionRepository
    {
        private Acceso acceso;

        public LogTransaccionRepository(Acceso acceso)
        {
            this.acceso = acceso;
        }

        public void Insert(LogTransaccion log)
        {
            string query = @"INSERT INTO LogTransacciones (TransaccionId, TipoOperacion, Mensaje, Exitoso, FechaLog, DetallesAdicionales) 
                            VALUES (@TransaccionId, @TipoOperacion, @Mensaje, @Exitoso, @FechaLog, @DetallesAdicionales)";
            
            List<SqlParameter> param = new List<SqlParameter>();
            if (log.TransaccionId.HasValue)
                param.Add(acceso.CrearParametro("@TransaccionId", log.TransaccionId.Value));
            else
                param.Add(acceso.CrearParametroNull("@TransaccionId", DbType.Int32));
            param.Add(acceso.CrearParametro("@TipoOperacion", log.TipoOperacion));
            param.Add(acceso.CrearParametro("@Mensaje", log.Mensaje));
            param.Add(acceso.CrearParametro("@Exitoso", log.Exitoso));
            param.Add(acceso.CrearParametro("@FechaLog", log.FechaLog));
            param.Add(acceso.CrearParametro("@DetallesAdicionales", log.DetallesAdicionales ?? string.Empty));

            try
            {
                acceso.Escribir(query, param);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al crear el log. Error: " + ex.Message);
            }
        }

        public List<LogTransaccion> GetByTransaccionId(int transaccionId)
        {
            List<LogTransaccion> logs = new List<LogTransaccion>();
            string query = "SELECT LogId, TransaccionId, TipoOperacion, Mensaje, Exitoso, FechaLog, DetallesAdicionales FROM LogTransacciones WHERE TransaccionId = @TransaccionId";
            List<SqlParameter> param = new List<SqlParameter>();
            param.Add(acceso.CrearParametro("@TransaccionId", transaccionId));

            DataTable dt = acceso.Leer(query, param);

            foreach (DataRow row in dt.Rows)
            {
                logs.Add(CastLogTransaccion(row));
            }
            
            return logs;
        }

        public List<LogTransaccion> GetAll()
        {
            List<LogTransaccion> logs = new List<LogTransaccion>();
            string query = "SELECT LogId, TransaccionId, TipoOperacion, Mensaje, Exitoso, FechaLog, DetallesAdicionales FROM LogTransacciones";
            
            DataTable dt = acceso.Leer(query, null);

            foreach (DataRow row in dt.Rows)
            {
                logs.Add(CastLogTransaccion(row));
            }
            
            return logs;
        }

        private LogTransaccion CastLogTransaccion(DataRow row)
        {
            LogTransaccion log = new LogTransaccion();
            log.LogId = Convert.ToInt32(row["LogId"]);
            log.TransaccionId = row["TransaccionId"].ToString() != "" ? Convert.ToInt32(row["TransaccionId"]) : null;
            log.TipoOperacion = row["TipoOperacion"].ToString() ?? string.Empty;
            log.Mensaje = row["Mensaje"].ToString() ?? string.Empty;
            log.Exitoso = Convert.ToBoolean(row["Exitoso"]);
            log.FechaLog = Convert.ToDateTime(row["FechaLog"]);
            log.DetallesAdicionales = row["DetallesAdicionales"].ToString();
            return log;
        }
    }
}

