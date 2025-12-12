# Guía de Implementación - Sistema de Apuestas

## Estructura de la Solución

### Proyectos de la Solución (.sln)

```
SportsbookPatterns.sln
├── SportsbookPatterns.UI (WinForms - .NET 8)
├── SportsbookPatterns.BE (Class Library)
├── SportsbookPatterns.BLL (Class Library)
├── SportsbookPatterns.DAL.Abstraccion (Class Library)
└── SportsbookPatterns.DAL.Implementacion (Class Library)
```

## Estructura de Carpetas por Proyecto

### 1. SportsbookPatterns.UI (WinForms)

```
SportsbookPatterns.UI/
├── Forms/
│   ├── FrmMenuPrincipal.cs
│   ├── FrmState.cs
│   ├── FrmStrategy.cs
│   └── FrmVisitor.cs
└── Program.cs
```

### 2. SportsbookPatterns.BE (Business Entities)

```
SportsbookPatterns.BE/
├── Usuario.cs
├── EstadoCuenta.cs
├── CuentaUsuario.cs
├── TipoTransaccion.cs
├── Transaccion.cs
├── TransaccionApuesta.cs
├── TransaccionRetiro.cs
├── TransaccionDeposito.cs
├── LogTransaccion.cs
└── Visitor/
    ├── ITransaccionVisitable.cs
    └── ITransaccionVisitor.cs
```

**Nota**: Las interfaces de Visitor están en `BE` para que las entidades puedan implementarlas sin crear dependencia circular con `BLL`.

### 3. SportsbookPatterns.BLL

```
SportsbookPatterns.BLL/
├── State/
│   ├── IEstadoCuenta.cs
│   ├── EstadoCreada.cs
│   ├── EstadoActivaSinFondos.cs
│   ├── EstadoActivaConFondos.cs
│   ├── EstadoBloqueada.cs
│   └── CuentaUsuarioService.cs
├── Strategy/
│   ├── IOperacion.cs
│   ├── OperacionDebito.cs
│   ├── OperacionCredito.cs
│   ├── OperacionCancelacion.cs
│   └── ProcesadorDeTransacciones.cs
├── Visitor/
│   ├── CalculadoraImpuestosVisitor.cs
│   └── GeneradorComisionesVisitor.cs
└── DTOs/
    ├── ResultadoTransaccion.cs
    └── ReporteDTO.cs
```

**Nota**: Las interfaces `ITransaccionVisitable` e `ITransaccionVisitor` están en `BE` para evitar dependencias circulares.

### 4. SportsbookPatterns.DAL.Abstraccion

```
SportsbookPatterns.DAL.Abstraccion/
├── IUsuarioRepository.cs
├── ICuentaUsuarioRepository.cs
├── ITransaccionRepository.cs
├── ITransaccionApuestaRepository.cs
├── ITransaccionRetiroRepository.cs
├── ITransaccionDepositoRepository.cs
├── IEstadoCuentaRepository.cs
├── ITipoTransaccionRepository.cs
└── ILogTransaccionRepository.cs
```

### 5. SportsbookPatterns.DAL.Implementacion

```
SportsbookPatterns.DAL.Implementacion/
├── Acceso.cs
├── UsuarioRepository.cs
├── CuentaUsuarioRepository.cs
├── TransaccionRepository.cs
├── TransaccionApuestaRepository.cs
├── TransaccionRetiroRepository.cs
├── TransaccionDepositoRepository.cs
├── EstadoCuentaRepository.cs
├── TipoTransaccionRepository.cs
└── LogTransaccionRepository.cs
```

## Diferencia entre BE (Business Entities) y DTOs

### BE (Business Entities)
- Representan las tablas de la base de datos directamente
- Mapeo 1:1 con las columnas de las tablas
- Usadas por la DAL para leer/escribir datos
- Contienen todas las propiedades de la entidad
- Ejemplo: `Usuario`, `CuentaUsuario`, `Transaccion`

### DTOs (Data Transfer Objects)
- Objetos ligeros para transferir datos entre capas
- Pueden contener datos de múltiples entidades
- Usados para comunicación entre BLL y UI
- Solo contienen los datos necesarios para una operación específica
- Ejemplo: `ResultadoTransaccion`, `ReporteDTO`

## Flujo de Navegación

### Menú Principal (FrmMenuPrincipal)

Formulario inicial con 3 botones:
- **Patrón State** → Abre `FrmState`
- **Patrón Strategy** → Abre `FrmStrategy`
- **Patrón Visitor** → Abre `FrmVisitor`

## Descripción de Formularios

### FrmState - Gestión de Estados de Cuenta

**Controles:**
- `cmbUsuarios` (ComboBox) - Lista de usuarios
- `cmbEstados` (ComboBox) - Lista de estados disponibles
- `lblSaldoActual` (Label) - Muestra el saldo actual
- `btnApostar` (Button) - Visible según estado
- `btnRetirar` (Button) - Visible según estado
- `btnDepositar` (Button) - Visible según estado
- `txtMonto` (TextBox) - Input para monto
- `dgvHistorial` (DataGridView) - Historial de transacciones

**Lógica:**
1. Al seleccionar usuario, carga su cuenta y estado actual
2. Al cambiar estado en dropdown, actualiza la cuenta
3. Según el estado seleccionado, muestra/oculta botones de acciones
4. Cada acción (Apostar, Retirar, Depositar) valida según el estado actual
5. Muestra mensajes de éxito/error según validaciones del estado

**Estados y Acciones Permitidas:**
- **Creada**: Solo Depositar
- **ActivaSinFondos**: Solo Depositar
- **ActivaConFondos**: Apostar, Retirar, Depositar
- **Bloqueada**: Ninguna acción permitida

### FrmStrategy - Procesamiento de Apuestas

**Controles:**
- `cmbUsuarios` (ComboBox) - Lista de usuarios
- `txtMontoApuesta` (TextBox) - Monto a apostar
- `btnApostar` (Button) - Ejecuta la apuesta
- `lblResultado` (Label) - Muestra resultado (Gana/Pierde/Vuelve a intentar)
- `lblSaldoActual` (Label) - Saldo actual del usuario
- `dgvTransacciones` (DataGridView) - Historial de transacciones

**Lógica:**
1. Selecciona usuario del dropdown
2. Ingresa monto a apostar
3. Al presionar "Apostar":
   - Genera resultado aleatorio: **Gana** (33%), **Pierde** (33%), **Vuelve a intentar** (33%)
   - **Si Gana**: Usa `OperacionCredito` (aumenta saldo)
   - **Si Pierde**: Usa `OperacionDebito` (reduce saldo)
   - **Si Vuelve a intentar**: Usa `OperacionCancelacion` (no afecta saldo, registra cancelación)
4. Muestra mensaje con el resultado
5. Actualiza saldo y grid de transacciones

**Estrategias:**
- `OperacionCredito`: Valida monto, actualiza saldo (+), registra transacción exitosa
- `OperacionDebito`: Valida monto y saldo suficiente, actualiza saldo (-), registra transacción
- `OperacionCancelacion`: No modifica saldo, registra transacción como cancelada

### FrmVisitor - Reportes y Auditoría

**Controles:**
- `btnReporteImpuestos` (Button) - Genera reporte de impuestos
- `btnReporteComisiones` (Button) - Genera reporte de comisiones
- `btnReporteGeneral` (Button) - Genera reporte general
- `dgvResultados` (DataGridView) - Muestra resultados del reporte
- `txtResultado` (TextBox multiline) - Muestra detalles del reporte

**Lógica:**
1. Carga todas las transacciones desde la base de datos
2. Al presionar cada botón:
   - **Reporte Impuestos**: Aplica `CalculadoraImpuestosVisitor` sobre todas las transacciones
   - **Reporte Comisiones**: Aplica `GeneradorComisionesVisitor` sobre todas las transacciones
   - **Reporte General**: Aplica ambos visitors y muestra resumen
3. Muestra resultados en el DataGridView y detalles en el TextBox

**Visitors:**
- `CalculadoraImpuestosVisitor`: Calcula impuestos según tipo de transacción
  - Apuestas: 5% del monto
  - Retiros: 2% del monto
  - Depósitos: 0%
- `GeneradorComisionesVisitor`: Calcula comisiones según tipo
  - Apuestas: 3% del monto
  - Retiros: 5% del monto
  - Depósitos: 0%

## Interfaces y Contratos (Patrón Repository)

### IUsuarioRepository (DAL.Abstraccion)

```csharp
public interface IUsuarioRepository
{
    List<Usuario> GetAll();
    Usuario GetById(int usuarioId);
    int Insert(Usuario usuario);
    bool Update(Usuario usuario);
    bool Delete(int usuarioId);
}
```

### ICuentaUsuarioRepository (DAL.Abstraccion)

```csharp
public interface ICuentaUsuarioRepository
{
    CuentaUsuario GetByUsuarioId(int usuarioId);
    CuentaUsuario GetById(int cuentaId);
    decimal GetSaldo(int cuentaId);
    bool UpdateSaldo(int cuentaId, decimal nuevoSaldo);
    bool UpdateEstado(int cuentaId, int estadoCuentaId);
    int Insert(CuentaUsuario cuenta);
    bool Update(CuentaUsuario cuenta);
}
```

### ITransaccionRepository (DAL.Abstraccion)

```csharp
public interface ITransaccionRepository
{
    List<Transaccion> GetAll();
    List<Transaccion> GetByCuentaId(int cuentaId);
    Transaccion GetById(int transaccionId);
    int Insert(Transaccion transaccion);
    bool Update(Transaccion transaccion);
    bool Delete(int transaccionId);
}
```

### ITransaccionApuestaRepository (DAL.Abstraccion)

```csharp
public interface ITransaccionApuestaRepository
{
    TransaccionApuesta GetByTransaccionId(int transaccionId);
    List<TransaccionApuesta> GetAll();
    int Insert(TransaccionApuesta apuesta);
    bool Update(TransaccionApuesta apuesta);
}
```

### ITransaccionRetiroRepository (DAL.Abstraccion)

```csharp
public interface ITransaccionRetiroRepository
{
    TransaccionRetiro GetByTransaccionId(int transaccionId);
    List<TransaccionRetiro> GetAll();
    int Insert(TransaccionRetiro retiro);
    bool Update(TransaccionRetiro retiro);
}
```

### ITransaccionDepositoRepository (DAL.Abstraccion)

```csharp
public interface ITransaccionDepositoRepository
{
    TransaccionDeposito GetByTransaccionId(int transaccionId);
    List<TransaccionDeposito> GetAll();
    int Insert(TransaccionDeposito deposito);
    bool Update(TransaccionDeposito deposito);
}
```

### IEstadoCuentaRepository (DAL.Abstraccion)

```csharp
public interface IEstadoCuentaRepository
{
    List<EstadoCuenta> GetAll();
    EstadoCuenta GetById(int estadoCuentaId);
    EstadoCuenta GetByCodigo(string codigoEstado);
}
```

### ITipoTransaccionRepository (DAL.Abstraccion)

```csharp
public interface ITipoTransaccionRepository
{
    List<TipoTransaccion> GetAll();
    TipoTransaccion GetById(int tipoTransaccionId);
    TipoTransaccion GetByCodigo(string codigoTipo);
}
```

### ILogTransaccionRepository (DAL.Abstraccion)

```csharp
public interface ILogTransaccionRepository
{
    void Insert(LogTransaccion log);
    List<LogTransaccion> GetByTransaccionId(int transaccionId);
    List<LogTransaccion> GetAll();
}
```

## Implementación DAL con ADO.NET (Patrón similar a Organola)

### Clase Acceso (Helper Centralizado)

La clase `Acceso` maneja todas las operaciones de conexión y ejecución de comandos SQL. Es utilizada por todos los repositories.

```csharp
public class Acceso
{
    private SqlConnection conexion;

    private void AbrirConexion()
    {
        string conexionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        conexion = new SqlConnection(conexionString);
        conexion.Open();
    }

    private void CerrarConexion()
    {
        conexion.Close();
        conexion = null;
        GC.Collect();
    }

    public DataTable Leer(string query, List<SqlParameter> parametros)
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
        adaptador = null;
        CerrarConexion();
        
        return filas;
    }

    public int Escribir(string query, List<SqlParameter> parametros)
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
        
        cm = null;
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
        parametro.Value = valor;
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
}
```

### Ejemplo: UsuarioRepository

```csharp
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

    public Usuario GetById(int usuarioId)
    {
        Usuario usuario = null;
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

    private Usuario CastUsuario(DataRow row)
    {
        Usuario usuario = new Usuario();
        usuario.UsuarioId = Convert.ToInt32(row["UsuarioId"]);
        usuario.Nombre = row["Nombre"].ToString();
        usuario.Apellido = row["Apellido"].ToString();
        usuario.Email = row["Email"].ToString();
        usuario.Telefono = row["Telefono"].ToString() != "" ? row["Telefono"].ToString() : null;
        usuario.FechaRegistro = Convert.ToDateTime(row["FechaRegistro"]);
        usuario.Activo = Convert.ToBoolean(row["Activo"]);
        return usuario;
    }
}
```

### Ejemplo: CuentaUsuarioRepository

```csharp
public class CuentaUsuarioRepository : ICuentaUsuarioRepository
{
    private Acceso acceso;

    public CuentaUsuarioRepository()
    {
        acceso = new Acceso();
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

    public CuentaUsuario GetByUsuarioId(int usuarioId)
    {
        CuentaUsuario cuenta = null;
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
            : (DateTime?)null;
        return cuenta;
    }
}
```

### IEstadoCuenta (BLL.State)

```csharp
public interface IEstadoCuenta
{
    void Apostar(CuentaUsuario cuenta, decimal monto, ICuentaUsuarioRepository cuentaRepo, ITransaccionRepository transaccionRepo);
    void Retirar(CuentaUsuario cuenta, decimal monto, ICuentaUsuarioRepository cuentaRepo, ITransaccionRepository transaccionRepo);
    void Depositar(CuentaUsuario cuenta, decimal monto, ICuentaUsuarioRepository cuentaRepo, ITransaccionRepository transaccionRepo);
    string GetNombreEstado();
}
```

### IOperacion (BLL.Strategy)

```csharp
public interface IOperacion
{
    ResultadoTransaccion ExecuteTransaction(Transaccion transaccion, ICuentaUsuarioRepository cuentaRepo, ITransaccionRepository transaccionRepo);
    string GetNombreOperacion();
}
```

### ITransaccionVisitable (BE - Interfaces de Visitor)

**Nota**: Estas interfaces están en `BE` para evitar dependencia circular. `BLL` implementa los visitors concretos.

```csharp
// BE/ITransaccionVisitable.cs
public interface ITransaccionVisitable
{
    void Accept(ITransaccionVisitor visitor);
    int TransaccionId { get; }
    decimal Monto { get; }
    DateTime FechaTransaccion { get; }
}

// BE/ITransaccionVisitor.cs
public interface ITransaccionVisitor
{
    void Visit(TransaccionApuesta apuesta);
    void Visit(TransaccionRetiro retiro);
    void Visit(TransaccionDeposito deposito);
    object GetResultado();
}
```

**Nota**: Las entidades `TransaccionApuesta`, `TransaccionRetiro` y `TransaccionDeposito` del proyecto BE implementan `ITransaccionVisitable`. Los visitors concretos (`CalculadoraImpuestosVisitor`, `GeneradorComisionesVisitor`) están en `BLL` e implementan `ITransaccionVisitor`.

### ITransaccionVisitor (BLL.Visitor)

```csharp
public interface ITransaccionVisitor
{
    void Visit(TransaccionApuesta apuesta);
    void Visit(TransaccionRetiro retiro);
    void Visit(TransaccionDeposito deposito);
    object GetResultado();
}
```

## Business Entities (BE)

### Usuario

```csharp
public class Usuario
{
    public int UsuarioId { get; set; }
    public string Nombre { get; set; }
    public string Apellido { get; set; }
    public string Email { get; set; }
    public string Telefono { get; set; }
    public DateTime FechaRegistro { get; set; }
    public bool Activo { get; set; }
}
```

### CuentaUsuario

```csharp
public class CuentaUsuario
{
    public int CuentaId { get; set; }
    public int UsuarioId { get; set; }
    public decimal Saldo { get; set; }
    public int EstadoCuentaId { get; set; }
    public DateTime FechaCreacion { get; set; }
    public DateTime? FechaUltimaModificacion { get; set; }
}
```

### Transaccion

```csharp
public class Transaccion
{
    public int TransaccionId { get; set; }
    public int CuentaId { get; set; }
    public int TipoTransaccionId { get; set; }
    public decimal Monto { get; set; }
    public DateTime FechaTransaccion { get; set; }
    public string Descripcion { get; set; }
    public bool Exitoso { get; set; }
    public string Observaciones { get; set; }
}
```

### TransaccionApuesta

```csharp
public class TransaccionApuesta : ITransaccionVisitable
{
    public int TransaccionApuestaId { get; set; }
    public int TransaccionId { get; set; }
    public string EventoDeportivo { get; set; }
    public string EquipoApostado { get; set; }
    public decimal Cuota { get; set; }
    public decimal MontoApostado { get; set; }
    public string Resultado { get; set; }
    public DateTime? FechaEvento { get; set; }
    
    public Transaccion Transaccion { get; set; }
    
    public void Accept(ITransaccionVisitor visitor)
    {
        visitor.Visit(this);
    }
    
    public decimal Monto => Transaccion?.Monto ?? 0;
    public DateTime FechaTransaccion => Transaccion?.FechaTransaccion ?? DateTime.MinValue;
}
```

### TransaccionRetiro

```csharp
public class TransaccionRetiro : ITransaccionVisitable
{
    public int TransaccionRetiroId { get; set; }
    public int TransaccionId { get; set; }
    public string MetodoRetiro { get; set; }
    public string NumeroCuentaDestino { get; set; }
    public string BancoDestino { get; set; }
    public decimal Comision { get; set; }
    public DateTime? FechaProcesamiento { get; set; }
    
    public Transaccion Transaccion { get; set; }
    
    public void Accept(ITransaccionVisitor visitor)
    {
        visitor.Visit(this);
    }
    
    public decimal Monto => Transaccion?.Monto ?? 0;
    public DateTime FechaTransaccion => Transaccion?.FechaTransaccion ?? DateTime.MinValue;
}
```

### TransaccionDeposito

```csharp
public class TransaccionDeposito : ITransaccionVisitable
{
    public int TransaccionDepositoId { get; set; }
    public int TransaccionId { get; set; }
    public string MetodoDeposito { get; set; }
    public string NumeroCuentaOrigen { get; set; }
    public string BancoOrigen { get; set; }
    public string NumeroReferencia { get; set; }
    public decimal Bonificacion { get; set; }
    
    public Transaccion Transaccion { get; set; }
    
    public void Accept(ITransaccionVisitor visitor)
    {
        visitor.Visit(this);
    }
    
    public decimal Monto => Transaccion?.Monto ?? 0;
    public DateTime FechaTransaccion => Transaccion?.FechaTransaccion ?? DateTime.MinValue;
}
```

## DTOs Principales

### ResultadoTransaccion

```csharp
public class ResultadoTransaccion
{
    public bool Exitoso { get; set; }
    public string Mensaje { get; set; }
    public decimal SaldoAnterior { get; set; }
    public decimal SaldoNuevo { get; set; }
    public int TransaccionId { get; set; }
}
```

## Cadena de Responsabilidades

### Flujo State
1. UI selecciona usuario y estado
2. UI llama a `CuentaUsuarioService.CambiarEstado()`
3. `CuentaUsuarioService` delega acción al estado actual
4. Estado concreto valida y ejecuta acción
5. Estado actualiza saldo/estado vía `ICuentaUsuarioRepository` y `ITransaccionRepository`
6. Estado puede cambiar a otro estado según reglas

### Flujo Strategy
1. UI selecciona usuario e ingresa monto
2. UI genera resultado aleatorio (Gana/Pierde/Vuelve a intentar)
3. UI crea `ProcesadorDeTransacciones` y establece estrategia según resultado
4. `ProcesadorDeTransacciones` ejecuta `IOperacion.ExecuteTransaction()`
5. Estrategia concreta valida y procesa transacción
6. Estrategia actualiza saldo vía `ICuentaUsuarioRepository` y registra transacción vía `ITransaccionRepository`
7. UI muestra resultado

### Flujo Visitor
1. UI presiona botón de reporte
2. BLL carga todas las transacciones vía `ITransaccionRepository.GetAll()`
3. BLL carga las transacciones específicas usando los repositories correspondientes:
   - `ITransaccionApuestaRepository.GetByTransaccionId()`
   - `ITransaccionRetiroRepository.GetByTransaccionId()`
   - `ITransaccionDepositoRepository.GetByTransaccionId()`
4. BLL crea objetos `TransaccionApuesta`, `TransaccionRetiro`, `TransaccionDeposito` que implementan `ITransaccionVisitable`
5. BLL crea visitor concreto según reporte solicitado
6. BLL itera sobre transacciones llamando `Accept(visitor)`
7. Cada transacción llama al método `Visit()` correspondiente del visitor
8. Visitor acumula cálculos en su estado interno
9. BLL obtiene resultado con `visitor.GetResultado()`
10. UI muestra resultados en grid y texto

## Notas de Implementación

### Arquitectura DAL (Patrón Repository - Similar a Organola)

- **Clase Acceso**: Clase helper centralizada que maneja:
  - Conexiones a la base de datos (`AbrirConexion`, `CerrarConexion`)
  - Método `Leer()`: Ejecuta stored procedures de lectura y retorna `DataTable`
  - Método `Escribir()`: Ejecuta stored procedures de escritura y retorna `int` (ID generado o filas afectadas)
  - Métodos helper `CrearParametro()`: Crea `SqlParameter` tipados (int, decimal, string, DateTime, bool)
  - Usa `ConfigurationManager` para obtener connection string desde app.config

- **DAL.Abstraccion**: Contiene interfaces separadas por entidad (una interfaz por cada entidad BE)

- **DAL.Implementacion**: 
  - Cada repository tiene una instancia de `Acceso`
  - Usa `Acceso.Leer()` para leer datos (retorna `DataTable`)
  - Usa `Acceso.Escribir()` para escribir datos (retorna `int`)
  - Mapea manualmente desde `DataRow` a entidades BE en métodos `Cast*()`
  - Usa **queries SQL directas** (no stored procedures)

- **ADO.NET Directo**: No se usa Entity Framework. Se utiliza ADO.NET puro:
  - `SqlConnection` para conexiones
  - `SqlCommand` para comandos (siempre `CommandType.Text` con queries SQL)
  - `SqlDataAdapter` para llenar `DataTable`
  - `SqlParameter` para parámetros tipados y seguros (previene SQL injection)
  - `DataTable` y `DataRow` para trabajar con resultados

- **Queries SQL Directas**: Todas las operaciones usan queries SQL directas:
  - `SELECT` para lecturas
  - `INSERT` para inserciones (con `SCOPE_IDENTITY()` para obtener el ID generado)
  - `UPDATE` para actualizaciones
  - `DELETE` para eliminaciones
  - Los parámetros se pasan con `@NombreParametro` para prevenir SQL injection

### Referencias entre Proyectos (Visibilidad entre Capas)

**Reglas de Dependencias:**
- **BE**: No depende de nadie (capa base, solo entidades)
- **DAL.Abstraccion**: No depende de nadie (solo interfaces)
- **DAL.Implementacion**: Depende de `BE` y `DAL.Abstraccion`
- **BLL**: Depende de `BE` y `DAL.Abstraccion`
- **UI**: Depende de `BLL` y `BE`

**Nota Importante**: 
- `BE` NO debe referenciar `BLL` (esto crearía dependencia circular)
- Las entidades de Visitor (`TransaccionApuesta`, `TransaccionRetiro`, `TransaccionDeposito`) implementan `ITransaccionVisitable` que está en `BLL`, pero esto se resuelve usando interfaces compartidas o moviendo `ITransaccionVisitable` a `BE` si es necesario
- Alternativa: Las interfaces de Visitor pueden estar en `BE` para evitar la dependencia circular

### Inyección de Dependencias

Se utiliza `Microsoft.Extensions.DependencyInjection` (incluido en .NET 8) para gestionar las dependencias.

**Configuración en Program.cs:**

```csharp
using Microsoft.Extensions.DependencyInjection;
using SportsbookPatterns.BLL.State;
using SportsbookPatterns.BLL.Strategy;
using SportsbookPatterns.BLL.Visitor;
using SportsbookPatterns.DAL.Abstraccion;
using SportsbookPatterns.DAL.Implementacion;

var services = new ServiceCollection();

// Registrar Repositories (DAL)
services.AddSingleton<IUsuarioRepository, UsuarioRepository>();
services.AddSingleton<ICuentaUsuarioRepository, CuentaUsuarioRepository>();
services.AddSingleton<ITransaccionRepository, TransaccionRepository>();
services.AddSingleton<ITransaccionApuestaRepository, TransaccionApuestaRepository>();
services.AddSingleton<ITransaccionRetiroRepository, TransaccionRetiroRepository>();
services.AddSingleton<ITransaccionDepositoRepository, TransaccionDepositoRepository>();
services.AddSingleton<IEstadoCuentaRepository, EstadoCuentaRepository>();
services.AddSingleton<ITipoTransaccionRepository, TipoTransaccionRepository>();
services.AddSingleton<ILogTransaccionRepository, LogTransaccionRepository>();

// Registrar Servicios BLL
services.AddTransient<CuentaUsuarioService>();

// Registrar Estados (State Pattern)
services.AddTransient<IEstadoCuenta, EstadoCreada>();
services.AddTransient<IEstadoCuenta, EstadoActivaSinFondos>();
services.AddTransient<IEstadoCuenta, EstadoActivaConFondos>();
services.AddTransient<IEstadoCuenta, EstadoBloqueada>();

// Registrar Estrategias (Strategy Pattern)
services.AddTransient<IOperacion, OperacionDebito>();
services.AddTransient<IOperacion, OperacionCredito>();
services.AddTransient<IOperacion, OperacionCancelacion>();

// Registrar Visitors (Visitor Pattern)
services.AddTransient<CalculadoraImpuestosVisitor>();
services.AddTransient<GeneradorComisionesVisitor>();

// Registrar Formularios
services.AddTransient<FrmMenuPrincipal>();
services.AddTransient<FrmState>();
services.AddTransient<FrmStrategy>();
services.AddTransient<FrmVisitor>();

var serviceProvider = services.BuildServiceProvider();
```

**Uso en Formularios:**

```csharp
public partial class FrmState : Form
{
    private readonly ICuentaUsuarioRepository _cuentaRepo;
    private readonly ITransaccionRepository _transaccionRepo;
    private readonly IEstadoCuentaRepository _estadoRepo;
    private readonly CuentaUsuarioService _cuentaService;

    public FrmState(
        ICuentaUsuarioRepository cuentaRepo,
        ITransaccionRepository transaccionRepo,
        IEstadoCuentaRepository estadoRepo,
        CuentaUsuarioService cuentaService)
    {
        InitializeComponent();
        _cuentaRepo = cuentaRepo;
        _transaccionRepo = transaccionRepo;
        _estadoRepo = estadoRepo;
        _cuentaService = cuentaService;
    }
}
```

**Inicialización de la Aplicación:**

```csharp
[STAThread]
static void Main()
{
    ApplicationConfiguration.Initialize();
    
    var services = new ServiceCollection();
    // ... configuración de servicios ...
    
    var serviceProvider = services.BuildServiceProvider();
    var mainForm = serviceProvider.GetRequiredService<FrmMenuPrincipal>();
    
    Application.Run(mainForm);
}
```

### Ejemplo de Uso en BLL

```csharp
public class CuentaUsuarioService
{
    private readonly ICuentaUsuarioRepository _cuentaRepo;
    private readonly ITransaccionRepository _transaccionRepo;
    private readonly IEstadoCuentaRepository _estadoRepo;

    public CuentaUsuarioService(
        ICuentaUsuarioRepository cuentaRepo,
        ITransaccionRepository transaccionRepo,
        IEstadoCuentaRepository estadoRepo)
    {
        _cuentaRepo = cuentaRepo;
        _transaccionRepo = transaccionRepo;
        _estadoRepo = estadoRepo;
    }

    public void ActualizarSaldo(int cuentaId, decimal monto)
    {
        decimal saldoActual = _cuentaRepo.GetSaldo(cuentaId);
        decimal nuevoSaldo = saldoActual + monto;
        _cuentaRepo.UpdateSaldo(cuentaId, nuevoSaldo);
    }
}
```

### Resolución de Interfaces de Visitor

Para evitar que `BE` dependa de `BLL`, hay dos opciones:

**Opción 1: Interfaces de Visitor en BE**
```csharp
// BE/ITransaccionVisitable.cs
public interface ITransaccionVisitable
{
    void Accept(ITransaccionVisitor visitor);
    int TransaccionId { get; }
    decimal Monto { get; }
    DateTime FechaTransaccion { get; }
}

// BE/ITransaccionVisitor.cs
public interface ITransaccionVisitor
{
    void Visit(TransaccionApuesta apuesta);
    void Visit(TransaccionRetiro retiro);
    void Visit(TransaccionDeposito deposito);
    object GetResultado();
}
```

**Opción 2: Usar composición en lugar de herencia**
Las entidades BE no implementan la interfaz directamente, sino que se crean wrappers en BLL que implementan `ITransaccionVisitable` y contienen la entidad BE.

**Recomendación**: Usar Opción 1 (interfaces en BE) para mantener la simplicidad.

### Configuración (app.config)

```xml
<configuration>
  <connectionStrings>
    <add name="DefaultConnection" 
         connectionString="Data Source=.;Initial Catalog=SportsbookDB;Integrated Security=True" />
  </connectionStrings>
</configuration>
```

### Validaciones y Reglas de Negocio

- Validaciones de negocio están en BLL, no en UI ni DAL
- La DAL solo se encarga de persistencia (CRUD)
- Los mensajes de error deben ser descriptivos para el usuario
- Las entidades BE se mapean directamente desde la BD en la DAL
- Los DTOs se crean en BLL cuando se necesita transferir datos específicos a la UI

