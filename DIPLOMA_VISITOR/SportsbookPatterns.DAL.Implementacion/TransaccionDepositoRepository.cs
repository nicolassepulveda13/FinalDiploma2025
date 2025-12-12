using System.Data;
using Microsoft.Data.SqlClient;
using SportsbookPatterns.BE;
using SportsbookPatterns.DAL.Abstraccion;

namespace SportsbookPatterns.DAL.Implementacion
{
    public class TransaccionDepositoRepository : ITransaccionDepositoRepository
    {
        private Acceso acceso;

        public TransaccionDepositoRepository(Acceso acceso)
        {
            this.acceso = acceso;
        }

        public TransaccionDeposito? GetByTransaccionId(int transaccionId)
        {
            TransaccionDeposito? deposito = null;
            string query = "SELECT TransaccionDepositoId, TransaccionId, MetodoDeposito, NumeroCuentaOrigen, BancoOrigen, NumeroReferencia, Bonificacion FROM TransaccionDeposito WHERE TransaccionId = @TransaccionId";
            List<SqlParameter> param = new List<SqlParameter>();
            param.Add(acceso.CrearParametro("@TransaccionId", transaccionId));

            DataTable dt = acceso.Leer(query, param);

            if (dt.Rows.Count > 0)
            {
                deposito = CastTransaccionDeposito(dt.Rows[0]);
            }

            return deposito;
        }

        public List<TransaccionDeposito> GetAll()
        {
            List<TransaccionDeposito> depositos = new List<TransaccionDeposito>();
            string query = "SELECT TransaccionDepositoId, TransaccionId, MetodoDeposito, NumeroCuentaOrigen, BancoOrigen, NumeroReferencia, Bonificacion FROM TransaccionDeposito";
            
            DataTable dt = acceso.Leer(query, null);

            foreach (DataRow row in dt.Rows)
            {
                depositos.Add(CastTransaccionDeposito(row));
            }
            
            return depositos;
        }

        public int Insert(TransaccionDeposito deposito)
        {
            string query = @"INSERT INTO TransaccionDeposito (TransaccionId, MetodoDeposito, NumeroCuentaOrigen, BancoOrigen, NumeroReferencia, Bonificacion) 
                            VALUES (@TransaccionId, @MetodoDeposito, @NumeroCuentaOrigen, @BancoOrigen, @NumeroReferencia, @Bonificacion);
                            SELECT CAST(SCOPE_IDENTITY() as int);";
            
            List<SqlParameter> param = new List<SqlParameter>();
            param.Add(acceso.CrearParametro("@TransaccionId", deposito.TransaccionId));
            param.Add(acceso.CrearParametro("@MetodoDeposito", deposito.MetodoDeposito));
            param.Add(acceso.CrearParametro("@NumeroCuentaOrigen", deposito.NumeroCuentaOrigen ?? string.Empty));
            param.Add(acceso.CrearParametro("@BancoOrigen", deposito.BancoOrigen ?? string.Empty));
            param.Add(acceso.CrearParametro("@NumeroReferencia", deposito.NumeroReferencia ?? string.Empty));
            param.Add(acceso.CrearParametro("@Bonificacion", deposito.Bonificacion));

            try
            {
                return acceso.Escribir(query, param);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al crear la transacción de depósito. Error: " + ex.Message);
            }
        }

        public bool Update(TransaccionDeposito deposito)
        {
            string query = @"UPDATE TransaccionDeposito SET MetodoDeposito = @MetodoDeposito, NumeroCuentaOrigen = @NumeroCuentaOrigen, 
                            BancoOrigen = @BancoOrigen, NumeroReferencia = @NumeroReferencia, Bonificacion = @Bonificacion 
                            WHERE TransaccionId = @TransaccionId";
            
            List<SqlParameter> param = new List<SqlParameter>();
            param.Add(acceso.CrearParametro("@TransaccionId", deposito.TransaccionId));
            param.Add(acceso.CrearParametro("@MetodoDeposito", deposito.MetodoDeposito));
            param.Add(acceso.CrearParametro("@NumeroCuentaOrigen", deposito.NumeroCuentaOrigen ?? string.Empty));
            param.Add(acceso.CrearParametro("@BancoOrigen", deposito.BancoOrigen ?? string.Empty));
            param.Add(acceso.CrearParametro("@NumeroReferencia", deposito.NumeroReferencia ?? string.Empty));
            param.Add(acceso.CrearParametro("@Bonificacion", deposito.Bonificacion));

            try
            {
                int resultado = acceso.Escribir(query, param);
                return resultado > 0;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al actualizar la transacción de depósito. Error: " + ex.Message);
            }
        }

        private TransaccionDeposito CastTransaccionDeposito(DataRow row)
        {
            TransaccionDeposito deposito = new TransaccionDeposito();
            deposito.TransaccionDepositoId = Convert.ToInt32(row["TransaccionDepositoId"]);
            deposito.TransaccionId = Convert.ToInt32(row["TransaccionId"]);
            deposito.MetodoDeposito = row["MetodoDeposito"].ToString() ?? string.Empty;
            deposito.NumeroCuentaOrigen = row["NumeroCuentaOrigen"].ToString();
            deposito.BancoOrigen = row["BancoOrigen"].ToString();
            deposito.NumeroReferencia = row["NumeroReferencia"].ToString();
            deposito.Bonificacion = Convert.ToDecimal(row["Bonificacion"]);
            return deposito;
        }
    }
}

