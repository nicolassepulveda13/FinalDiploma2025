using System.Data;
using Microsoft.Data.SqlClient;
using SportsbookPatterns.BE;
using SportsbookPatterns.DAL.Abstraccion;

namespace SportsbookPatterns.DAL.Implementacion
{
    public class TransaccionRepository : ITransaccionRepository
    {
        private Acceso acceso;

        public TransaccionRepository(Acceso acceso)
        {
            this.acceso = acceso;
        }

        public List<Transaccion> GetAll()
        {
            List<Transaccion> transacciones = new List<Transaccion>();
            string query = "SELECT TransaccionId, CuentaId, TipoTransaccionId, Monto, FechaTransaccion, Descripcion, Exitoso, Observaciones FROM Transaccion";
            
            DataTable dt = acceso.Leer(query, null);

            foreach (DataRow row in dt.Rows)
            {
                transacciones.Add(CastTransaccion(row));
            }
            
            return transacciones;
        }

        public List<Transaccion> GetByCuentaId(int cuentaId)
        {
            List<Transaccion> transacciones = new List<Transaccion>();
            string query = "SELECT TransaccionId, CuentaId, TipoTransaccionId, Monto, FechaTransaccion, Descripcion, Exitoso, Observaciones FROM Transaccion WHERE CuentaId = @CuentaId";
            List<SqlParameter> param = new List<SqlParameter>();
            param.Add(acceso.CrearParametro("@CuentaId", cuentaId));

            DataTable dt = acceso.Leer(query, param);

            foreach (DataRow row in dt.Rows)
            {
                transacciones.Add(CastTransaccion(row));
            }
            
            return transacciones;
        }

        public Transaccion? GetById(int transaccionId)
        {
            Transaccion? transaccion = null;
            string query = "SELECT TransaccionId, CuentaId, TipoTransaccionId, Monto, FechaTransaccion, Descripcion, Exitoso, Observaciones FROM Transaccion WHERE TransaccionId = @TransaccionId";
            List<SqlParameter> param = new List<SqlParameter>();
            param.Add(acceso.CrearParametro("@TransaccionId", transaccionId));

            DataTable dt = acceso.Leer(query, param);

            if (dt.Rows.Count > 0)
            {
                transaccion = CastTransaccion(dt.Rows[0]);
            }

            return transaccion;
        }

        public int Insert(Transaccion transaccion)
        {
            string query = @"INSERT INTO Transaccion (CuentaId, TipoTransaccionId, Monto, FechaTransaccion, Descripcion, Exitoso, Observaciones) 
                            VALUES (@CuentaId, @TipoTransaccionId, @Monto, @FechaTransaccion, @Descripcion, @Exitoso, @Observaciones);
                            SELECT CAST(SCOPE_IDENTITY() as int);";
            
            List<SqlParameter> param = new List<SqlParameter>();
            param.Add(acceso.CrearParametro("@CuentaId", transaccion.CuentaId));
            param.Add(acceso.CrearParametro("@TipoTransaccionId", transaccion.TipoTransaccionId));
            param.Add(acceso.CrearParametro("@Monto", transaccion.Monto));
            param.Add(acceso.CrearParametro("@FechaTransaccion", transaccion.FechaTransaccion));
            param.Add(acceso.CrearParametro("@Descripcion", transaccion.Descripcion ?? string.Empty));
            param.Add(acceso.CrearParametro("@Exitoso", transaccion.Exitoso));
            param.Add(acceso.CrearParametro("@Observaciones", transaccion.Observaciones ?? string.Empty));

            try
            {
                return acceso.Escribir(query, param);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al crear la transacción. Error: " + ex.Message);
            }
        }

        public bool Update(Transaccion transaccion)
        {
            string query = @"UPDATE Transaccion SET CuentaId = @CuentaId, TipoTransaccionId = @TipoTransaccionId, 
                            Monto = @Monto, Descripcion = @Descripcion, Exitoso = @Exitoso, Observaciones = @Observaciones 
                            WHERE TransaccionId = @TransaccionId";
            
            List<SqlParameter> param = new List<SqlParameter>();
            param.Add(acceso.CrearParametro("@TransaccionId", transaccion.TransaccionId));
            param.Add(acceso.CrearParametro("@CuentaId", transaccion.CuentaId));
            param.Add(acceso.CrearParametro("@TipoTransaccionId", transaccion.TipoTransaccionId));
            param.Add(acceso.CrearParametro("@Monto", transaccion.Monto));
            param.Add(acceso.CrearParametro("@Descripcion", transaccion.Descripcion ?? string.Empty));
            param.Add(acceso.CrearParametro("@Exitoso", transaccion.Exitoso));
            param.Add(acceso.CrearParametro("@Observaciones", transaccion.Observaciones ?? string.Empty));

            try
            {
                int resultado = acceso.Escribir(query, param);
                return resultado > 0;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al actualizar la transacción. Error: " + ex.Message);
            }
        }

        public bool Delete(int transaccionId)
        {
            string query = "DELETE FROM Transaccion WHERE TransaccionId = @TransaccionId";
            List<SqlParameter> param = new List<SqlParameter>();
            param.Add(acceso.CrearParametro("@TransaccionId", transaccionId));

            try
            {
                int resultado = acceso.Escribir(query, param);
                return resultado > 0;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al eliminar la transacción. Error: " + ex.Message);
            }
        }

        private Transaccion CastTransaccion(DataRow row)
        {
            Transaccion transaccion = new Transaccion();
            transaccion.TransaccionId = Convert.ToInt32(row["TransaccionId"]);
            transaccion.CuentaId = Convert.ToInt32(row["CuentaId"]);
            transaccion.TipoTransaccionId = Convert.ToInt32(row["TipoTransaccionId"]);
            transaccion.Monto = Convert.ToDecimal(row["Monto"]);
            transaccion.FechaTransaccion = Convert.ToDateTime(row["FechaTransaccion"]);
            transaccion.Descripcion = row["Descripcion"].ToString();
            transaccion.Exitoso = Convert.ToBoolean(row["Exitoso"]);
            transaccion.Observaciones = row["Observaciones"].ToString();
            return transaccion;
        }
    }
}

