using System.Data;
using Microsoft.Data.SqlClient;
using SportsbookPatterns.BE;
using SportsbookPatterns.DAL.Abstraccion;

namespace SportsbookPatterns.DAL.Implementacion
{
    public class EstadoCuentaRepository : IEstadoCuentaRepository
    {
        private Acceso acceso;

        public EstadoCuentaRepository(Acceso acceso)
        {
            this.acceso = acceso;
        }

        public List<EstadoCuenta> GetAll()
        {
            List<EstadoCuenta> estados = new List<EstadoCuenta>();
            string query = "SELECT EstadoCuentaId, CodigoEstado, Descripcion, Activo FROM EstadoCuenta";
            
            DataTable dt = acceso.Leer(query, null);

            foreach (DataRow row in dt.Rows)
            {
                estados.Add(CastEstadoCuenta(row));
            }
            
            return estados;
        }

        public EstadoCuenta? GetById(int estadoCuentaId)
        {
            EstadoCuenta? estado = null;
            string query = "SELECT EstadoCuentaId, CodigoEstado, Descripcion, Activo FROM EstadoCuenta WHERE EstadoCuentaId = @EstadoCuentaId";
            List<SqlParameter> param = new List<SqlParameter>();
            param.Add(acceso.CrearParametro("@EstadoCuentaId", estadoCuentaId));

            DataTable dt = acceso.Leer(query, param);

            if (dt.Rows.Count > 0)
            {
                estado = CastEstadoCuenta(dt.Rows[0]);
            }

            return estado;
        }

        public EstadoCuenta? GetByCodigo(string codigoEstado)
        {
            EstadoCuenta? estado = null;
            string query = "SELECT EstadoCuentaId, CodigoEstado, Descripcion, Activo FROM EstadoCuenta WHERE CodigoEstado = @CodigoEstado";
            List<SqlParameter> param = new List<SqlParameter>();
            param.Add(acceso.CrearParametro("@CodigoEstado", codigoEstado));

            DataTable dt = acceso.Leer(query, param);

            if (dt.Rows.Count > 0)
            {
                estado = CastEstadoCuenta(dt.Rows[0]);
            }

            return estado;
        }

        private EstadoCuenta CastEstadoCuenta(DataRow row)
        {
            EstadoCuenta estado = new EstadoCuenta();
            estado.EstadoCuentaId = Convert.ToInt32(row["EstadoCuentaId"]);
            estado.CodigoEstado = row["CodigoEstado"].ToString() ?? string.Empty;
            estado.Descripcion = row["Descripcion"].ToString() ?? string.Empty;
            estado.Activo = Convert.ToBoolean(row["Activo"]);
            return estado;
        }
    }
}

