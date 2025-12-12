using System.Data;
using Microsoft.Data.SqlClient;
using SportsbookPatterns.BE;
using SportsbookPatterns.DAL;

namespace SportsbookPatterns.DAL
{
    public class TransaccionApuestaRepository : ITransaccionApuestaRepository
    {
        private Acceso acceso;

        public TransaccionApuestaRepository(Acceso acceso)
        {
            this.acceso = acceso;
        }

        public TransaccionApuesta? GetByTransaccionId(int transaccionId)
        {
            TransaccionApuesta? apuesta = null;
            string query = "SELECT TransaccionApuestaId, TransaccionId, EventoDeportivo, EquipoApostado, Cuota, MontoApostado, Resultado, FechaEvento FROM TransaccionApuesta WHERE TransaccionId = @TransaccionId";
            List<SqlParameter> param = new List<SqlParameter>();
            param.Add(acceso.CrearParametro("@TransaccionId", transaccionId));

            DataTable dt = acceso.Leer(query, param);

            if (dt.Rows.Count > 0)
            {
                apuesta = CastTransaccionApuesta(dt.Rows[0]);
            }

            return apuesta;
        }

        public List<TransaccionApuesta> GetAll()
        {
            List<TransaccionApuesta> apuestas = new List<TransaccionApuesta>();
            string query = "SELECT TransaccionApuestaId, TransaccionId, EventoDeportivo, EquipoApostado, Cuota, MontoApostado, Resultado, FechaEvento FROM TransaccionApuesta";
            
            DataTable dt = acceso.Leer(query, null);

            foreach (DataRow row in dt.Rows)
            {
                apuestas.Add(CastTransaccionApuesta(row));
            }
            
            return apuestas;
        }

        public int Insert(TransaccionApuesta apuesta)
        {
            string query = @"INSERT INTO TransaccionApuesta (TransaccionId, EventoDeportivo, EquipoApostado, Cuota, MontoApostado, Resultado, FechaEvento) 
                            VALUES (@TransaccionId, @EventoDeportivo, @EquipoApostado, @Cuota, @MontoApostado, @Resultado, @FechaEvento);
                            SELECT CAST(SCOPE_IDENTITY() as int);";
            
            List<SqlParameter> param = new List<SqlParameter>();
            param.Add(acceso.CrearParametro("@TransaccionId", apuesta.TransaccionId));
            param.Add(acceso.CrearParametro("@EventoDeportivo", apuesta.EventoDeportivo));
            param.Add(acceso.CrearParametro("@EquipoApostado", apuesta.EquipoApostado));
            param.Add(acceso.CrearParametro("@Cuota", apuesta.Cuota));
            param.Add(acceso.CrearParametro("@MontoApostado", apuesta.MontoApostado));
            param.Add(acceso.CrearParametro("@Resultado", apuesta.Resultado ?? string.Empty));
            if (apuesta.FechaEvento.HasValue)
                param.Add(acceso.CrearParametro("@FechaEvento", apuesta.FechaEvento.Value));
            else
                param.Add(acceso.CrearParametroNull("@FechaEvento", DbType.DateTime));

            try
            {
                return acceso.Escribir(query, param);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al crear la transacción de apuesta. Error: " + ex.Message);
            }
        }

        public bool Update(TransaccionApuesta apuesta)
        {
            string query = @"UPDATE TransaccionApuesta SET EventoDeportivo = @EventoDeportivo, EquipoApostado = @EquipoApostado, 
                            Cuota = @Cuota, MontoApostado = @MontoApostado, Resultado = @Resultado, FechaEvento = @FechaEvento 
                            WHERE TransaccionId = @TransaccionId";
            
            List<SqlParameter> param = new List<SqlParameter>();
            param.Add(acceso.CrearParametro("@TransaccionId", apuesta.TransaccionId));
            param.Add(acceso.CrearParametro("@EventoDeportivo", apuesta.EventoDeportivo));
            param.Add(acceso.CrearParametro("@EquipoApostado", apuesta.EquipoApostado));
            param.Add(acceso.CrearParametro("@Cuota", apuesta.Cuota));
            param.Add(acceso.CrearParametro("@MontoApostado", apuesta.MontoApostado));
            param.Add(acceso.CrearParametro("@Resultado", apuesta.Resultado ?? string.Empty));
            if (apuesta.FechaEvento.HasValue)
                param.Add(acceso.CrearParametro("@FechaEvento", apuesta.FechaEvento.Value));
            else
                param.Add(acceso.CrearParametroNull("@FechaEvento", DbType.DateTime));

            try
            {
                int resultado = acceso.Escribir(query, param);
                return resultado > 0;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al actualizar la transacción de apuesta. Error: " + ex.Message);
            }
        }

        private TransaccionApuesta CastTransaccionApuesta(DataRow row)
        {
            TransaccionApuesta apuesta = new TransaccionApuesta();
            apuesta.TransaccionApuestaId = Convert.ToInt32(row["TransaccionApuestaId"]);
            apuesta.TransaccionId = Convert.ToInt32(row["TransaccionId"]);
            apuesta.EventoDeportivo = row["EventoDeportivo"].ToString() ?? string.Empty;
            apuesta.EquipoApostado = row["EquipoApostado"].ToString() ?? string.Empty;
            apuesta.Cuota = Convert.ToDecimal(row["Cuota"]);
            apuesta.MontoApostado = Convert.ToDecimal(row["MontoApostado"]);
            apuesta.Resultado = row["Resultado"].ToString();
            apuesta.FechaEvento = row["FechaEvento"].ToString() != "" ? Convert.ToDateTime(row["FechaEvento"]) : null;
            return apuesta;
        }
    }
}

