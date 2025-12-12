using System.Data;
using System.Data.SqlClient;
using SportsbookPatterns.BE;
using SportsbookPatterns.DAL.Abstraccion;

namespace SportsbookPatterns.DAL.Implementacion
{
    public class TipoTransaccionRepository : ITipoTransaccionRepository
    {
        private Acceso acceso;

        public TipoTransaccionRepository()
        {
            acceso = new Acceso();
        }

        public List<TipoTransaccion> GetAll()
        {
            List<TipoTransaccion> tipos = new List<TipoTransaccion>();
            string query = "SELECT TipoTransaccionId, CodigoTipo, Descripcion, AfectaSaldo, Activo FROM TipoTransaccion";
            
            DataTable dt = acceso.Leer(query, null);

            foreach (DataRow row in dt.Rows)
            {
                tipos.Add(CastTipoTransaccion(row));
            }
            
            return tipos;
        }

        public TipoTransaccion? GetById(int tipoTransaccionId)
        {
            TipoTransaccion? tipo = null;
            string query = "SELECT TipoTransaccionId, CodigoTipo, Descripcion, AfectaSaldo, Activo FROM TipoTransaccion WHERE TipoTransaccionId = @TipoTransaccionId";
            List<SqlParameter> param = new List<SqlParameter>();
            param.Add(acceso.CrearParametro("@TipoTransaccionId", tipoTransaccionId));

            DataTable dt = acceso.Leer(query, param);

            if (dt.Rows.Count > 0)
            {
                tipo = CastTipoTransaccion(dt.Rows[0]);
            }

            return tipo;
        }

        public TipoTransaccion? GetByCodigo(string codigoTipo)
        {
            TipoTransaccion? tipo = null;
            string query = "SELECT TipoTransaccionId, CodigoTipo, Descripcion, AfectaSaldo, Activo FROM TipoTransaccion WHERE CodigoTipo = @CodigoTipo";
            List<SqlParameter> param = new List<SqlParameter>();
            param.Add(acceso.CrearParametro("@CodigoTipo", codigoTipo));

            DataTable dt = acceso.Leer(query, param);

            if (dt.Rows.Count > 0)
            {
                tipo = CastTipoTransaccion(dt.Rows[0]);
            }

            return tipo;
        }

        private TipoTransaccion CastTipoTransaccion(DataRow row)
        {
            TipoTransaccion tipo = new TipoTransaccion();
            tipo.TipoTransaccionId = Convert.ToInt32(row["TipoTransaccionId"]);
            tipo.CodigoTipo = row["CodigoTipo"].ToString() ?? string.Empty;
            tipo.Descripcion = row["Descripcion"].ToString() ?? string.Empty;
            tipo.AfectaSaldo = Convert.ToBoolean(row["AfectaSaldo"]);
            tipo.Activo = Convert.ToBoolean(row["Activo"]);
            return tipo;
        }
    }
}

