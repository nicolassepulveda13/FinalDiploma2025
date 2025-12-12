using System.Data;
using System.Data.SqlClient;
using SportsbookPatterns.BE;
using SportsbookPatterns.DAL.Abstraccion;

namespace SportsbookPatterns.DAL.Implementacion
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private Acceso acceso;

        public UsuarioRepository()
        {
            acceso = new Acceso();
        }

        public List<Usuario> GetAll()
        {
            List<Usuario> usuarios = new List<Usuario>();
            string query = "SELECT UsuarioId, Nombre, Apellido, Email, Telefono, FechaRegistro, Activo FROM Usuario";
            
            DataTable dt = acceso.Leer(query, null);

            foreach (DataRow row in dt.Rows)
            {
                usuarios.Add(CastUsuario(row));
            }
            
            return usuarios;
        }

        public Usuario? GetById(int usuarioId)
        {
            Usuario? usuario = null;
            string query = "SELECT UsuarioId, Nombre, Apellido, Email, Telefono, FechaRegistro, Activo FROM Usuario WHERE UsuarioId = @UsuarioId";
            List<SqlParameter> param = new List<SqlParameter>();
            param.Add(acceso.CrearParametro("@UsuarioId", usuarioId));

            DataTable dt = acceso.Leer(query, param);

            if (dt.Rows.Count > 0)
            {
                usuario = CastUsuario(dt.Rows[0]);
            }

            return usuario;
        }

        public int Insert(Usuario usuario)
        {
            string query = @"INSERT INTO Usuario (Nombre, Apellido, Email, Telefono, FechaRegistro, Activo) 
                            VALUES (@Nombre, @Apellido, @Email, @Telefono, @FechaRegistro, @Activo);
                            SELECT CAST(SCOPE_IDENTITY() as int);";
            
            List<SqlParameter> param = new List<SqlParameter>();
            param.Add(acceso.CrearParametro("@Nombre", usuario.Nombre));
            param.Add(acceso.CrearParametro("@Apellido", usuario.Apellido));
            param.Add(acceso.CrearParametro("@Email", usuario.Email));
            param.Add(acceso.CrearParametro("@Telefono", usuario.Telefono ?? string.Empty));
            param.Add(acceso.CrearParametro("@FechaRegistro", usuario.FechaRegistro));
            param.Add(acceso.CrearParametro("@Activo", usuario.Activo));

            try
            {
                return acceso.Escribir(query, param);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al crear el usuario. Error: " + ex.Message);
            }
        }

        public bool Update(Usuario usuario)
        {
            string query = @"UPDATE Usuario SET Nombre = @Nombre, Apellido = @Apellido, Email = @Email, 
                            Telefono = @Telefono, Activo = @Activo WHERE UsuarioId = @UsuarioId";
            
            List<SqlParameter> param = new List<SqlParameter>();
            param.Add(acceso.CrearParametro("@UsuarioId", usuario.UsuarioId));
            param.Add(acceso.CrearParametro("@Nombre", usuario.Nombre));
            param.Add(acceso.CrearParametro("@Apellido", usuario.Apellido));
            param.Add(acceso.CrearParametro("@Email", usuario.Email));
            param.Add(acceso.CrearParametro("@Telefono", usuario.Telefono ?? string.Empty));
            param.Add(acceso.CrearParametro("@Activo", usuario.Activo));

            try
            {
                int resultado = acceso.Escribir(query, param);
                return resultado > 0;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al actualizar el usuario. Error: " + ex.Message);
            }
        }

        public bool Delete(int usuarioId)
        {
            string query = "DELETE FROM Usuario WHERE UsuarioId = @UsuarioId";
            List<SqlParameter> param = new List<SqlParameter>();
            param.Add(acceso.CrearParametro("@UsuarioId", usuarioId));

            try
            {
                int resultado = acceso.Escribir(query, param);
                return resultado > 0;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al eliminar el usuario. Error: " + ex.Message);
            }
        }

        private Usuario CastUsuario(DataRow row)
        {
            Usuario usuario = new Usuario();
            usuario.UsuarioId = Convert.ToInt32(row["UsuarioId"]);
            usuario.Nombre = row["Nombre"].ToString() ?? string.Empty;
            usuario.Apellido = row["Apellido"].ToString() ?? string.Empty;
            usuario.Email = row["Email"].ToString() ?? string.Empty;
            usuario.Telefono = row["Telefono"].ToString() != "" ? row["Telefono"].ToString() : null;
            usuario.FechaRegistro = Convert.ToDateTime(row["FechaRegistro"]);
            usuario.Activo = Convert.ToBoolean(row["Activo"]);
            return usuario;
        }
    }
}

