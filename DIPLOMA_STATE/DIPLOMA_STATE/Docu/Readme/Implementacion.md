# Guía de Implementación - Sistema de Apuestas

## Estructura de Soluciones

El proyecto está organizado en **3 soluciones independientes**, cada una enfocada en un patrón específico. Cada solución contiene sus propios proyectos (BE, BLL, DAL) y su propia clase `Acceso`, pero todas comparten la misma base de datos (`SportsbookDB`).

### Soluciones

```
DIPLOMA_STATE/
├── DIPLOMA_STATE.sln
├── DIPLOMA_STATE/ (WinForms UI)
├── SportsbookPatterns.BE/
├── SportsbookPatterns.BLL/
├── SportsbookPatterns.DAL.Abstraccion/
└── SportsbookPatterns.DAL.Implementacion/

DIPLOMA_STRATEGY/
├── DIPLOMA_STRATEGY.sln
├── DIPLOMA_STRATEGY/ (WinForms UI)
├── SportsbookPatterns.BE/
├── SportsbookPatterns.BLL/
├── SportsbookPatterns.DAL.Abstraccion/
└── SportsbookPatterns.DAL.Implementacion/

DIPLOMA_VISITOR/
├── DiplomaFinal.sln
├── DiplomaFinal/ (WinForms UI)
├── SportsbookPatterns.BE/
├── SportsbookPatterns.BLL/
├── SportsbookPatterns.DAL.Abstraccion/
└── SportsbookPatterns.DAL.Implementacion/
```

**Nota Importante**: Cada solución tiene su propia clase `Acceso` duplicada e independiente. Las tres soluciones comparten la misma base de datos pero operan de forma completamente independiente.

## DIPLOMA_STATE - Patrón State

### Proyectos de la Solución

```
DIPLOMA_STATE.sln
├── DIPLOMA_STATE (WinForms - .NET 8)
├── SportsbookPatterns.BE (Class Library)
├── SportsbookPatterns.BLL (Class Library)
├── SportsbookPatterns.DAL.Abstraccion (Class Library)
└── SportsbookPatterns.DAL.Implementacion (Class Library)
```

### Estructura de Carpetas

#### 1. DIPLOMA_STATE (WinForms UI)

```
DIPLOMA_STATE/
├── Forms/
│   └── FrmState.cs
└── Program.cs
```

**Program.cs**: Inicia directamente en `FrmState` (sin menú principal).

#### 2. SportsbookPatterns.BE (Business Entities)

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
└── LogTransaccion.cs
```

#### 3. SportsbookPatterns.BLL

```
SportsbookPatterns.BLL/
├── State/
│   ├── IEstadoCuenta.cs
│   ├── EstadoCreada.cs
│   ├── EstadoActiva.cs
│   ├── EstadoBloqueada.cs
│   └── CuentaUsuarioService.cs
└── Services/
    └── UsuarioService.cs
```

#### 4. SportsbookPatterns.DAL.Abstraccion

```
SportsbookPatterns.DAL.Abstraccion/
├── IUsuarioRepository.cs
├── ICuentaUsuarioRepository.cs
├── ITransaccionRepository.cs
├── ITransaccionApuestaRepository.cs
├── ITransaccionRetiroRepository.cs
├── ITransaccionDepositoRepository.cs
├── IEstadoCuentaRepository.cs
└── ITipoTransaccionRepository.cs
```

#### 5. SportsbookPatterns.DAL.Implementacion

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
└── TipoTransaccionRepository.cs
```

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
- **Activa**: Apostar, Retirar, Depositar (valida saldo en tiempo de ejecución)
- **Bloqueada**: Ninguna acción permitida

## DIPLOMA_STRATEGY - Patrón Strategy

### Proyectos de la Solución

```
DIPLOMA_STRATEGY.sln
├── DIPLOMA_STRATEGY (WinForms - .NET 8)
├── SportsbookPatterns.BE (Class Library)
├── SportsbookPatterns.BLL (Class Library)
├── SportsbookPatterns.DAL.Abstraccion (Class Library)
└── SportsbookPatterns.DAL.Implementacion (Class Library)
```

### Estructura de Carpetas

#### 1. DIPLOMA_STRATEGY (WinForms UI)

```
DIPLOMA_STRATEGY/
├── Forms/
│   └── FrmStrategy.cs
└── Program.cs
```

**Program.cs**: Inicia directamente en `FrmStrategy` (sin menú principal).

#### 2. SportsbookPatterns.BE (Business Entities)

```
SportsbookPatterns.BE/
├── Usuario.cs
├── CuentaUsuario.cs
├── TipoTransaccion.cs
├── Transaccion.cs
└── TransaccionApuesta.cs
```

#### 3. SportsbookPatterns.BLL

```
SportsbookPatterns.BLL/
├── Strategy/
│   ├── IOperacion.cs
│   ├── OperacionDebito.cs
│   ├── OperacionCredito.cs
│   ├── OperacionCancelacion.cs
│   └── ProcesadorDeTransacciones.cs
├── Services/
│   ├── UsuarioService.cs
│   ├── CuentaUsuarioService.cs
│   └── ApuestaService.cs
└── DTOs/
    └── ResultadoTransaccion.cs
```

#### 4. SportsbookPatterns.DAL.Abstraccion

```
SportsbookPatterns.DAL.Abstraccion/
├── IUsuarioRepository.cs
├── ICuentaUsuarioRepository.cs
├── ITransaccionRepository.cs
├── ITransaccionApuestaRepository.cs
└── ITipoTransaccionRepository.cs
```

#### 5. SportsbookPatterns.DAL.Implementacion

```
SportsbookPatterns.DAL.Implementacion/
├── Acceso.cs
├── UsuarioRepository.cs
├── CuentaUsuarioRepository.cs
├── TransaccionRepository.cs
├── TransaccionApuestaRepository.cs
└── TipoTransaccionRepository.cs
```

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

## DIPLOMA_VISITOR - Patrón Visitor

### Proyectos de la Solución

```
DiplomaFinal.sln
├── DiplomaFinal (WinForms - .NET 8)
├── SportsbookPatterns.BE (Class Library)
├── SportsbookPatterns.BLL (Class Library)
├── SportsbookPatterns.DAL.Abstraccion (Class Library)
└── SportsbookPatterns.DAL.Implementacion (Class Library)
```

### Estructura de Carpetas

#### 1. DiplomaFinal (WinForms UI)

```
DiplomaFinal/
├── Forms/
│   └── FrmVisitor.cs
└── Program.cs
```

**Program.cs**: Inicia directamente en `FrmVisitor` (sin menú principal).

#### 2. SportsbookPatterns.BE (Business Entities)

```
SportsbookPatterns.BE/
├── Usuario.cs
├── CuentaUsuario.cs
├── Transaccion.cs
├── TransaccionApuesta.cs
├── TransaccionRetiro.cs
├── TransaccionDeposito.cs
└── Visitor/
    ├── ITransaccionVisitable.cs
    └── ITransaccionVisitor.cs
```

**Nota**: Las interfaces de Visitor están en `BE` para que las entidades puedan implementarlas sin crear dependencia circular con `BLL`.

#### 3. SportsbookPatterns.BLL

```
SportsbookPatterns.BLL/
├── Visitor/
│   ├── CalculadoraImpuestosVisitor.cs
│   └── GeneradorComisionesVisitor.cs
└── Services/
    ├── TransaccionService.cs
    └── ReporteService.cs
```

#### 4. SportsbookPatterns.DAL.Abstraccion

```
SportsbookPatterns.DAL.Abstraccion/
├── ITransaccionRepository.cs
├── ITransaccionApuestaRepository.cs
├── ITransaccionRetiroRepository.cs
└── ITransaccionDepositoRepository.cs
```

#### 5. SportsbookPatterns.DAL.Implementacion

```
SportsbookPatterns.DAL.Implementacion/
├── Acceso.cs
├── TransaccionRepository.cs
├── TransaccionApuestaRepository.cs
├── TransaccionRetiroRepository.cs
└── TransaccionDepositoRepository.cs
```

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
- Ejemplo: `ResultadoTransaccion`

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

## Implementación DAL con ADO.NET (Patrón similar a Organola)

### Clase Acceso (Helper Centralizado)

La clase `Acceso` maneja todas las operaciones de conexión y ejecución de comandos SQL. Es utilizada por todos los repositories. **Cada solución tiene su propia instancia de `Acceso`**.

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
        parametro.Value = valor ?? (object)DBNull.Value;
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

    public SqlParameter CrearParametroNull(string nombre)
    {
        SqlParameter parametro = new SqlParameter();
        parametro.ParameterName = nombre;
        parametro.Value = DBNull.Value;
        return parametro;
    }
}
```

### Ejemplo: UsuarioRepository

```csharp
public class UsuarioRepository : IUsuarioRepository
{
    private readonly Acceso _acceso;

    public UsuarioRepository(Acceso acceso)
    {
        _acceso = acceso;
    }

    public List<Usuario> GetAll()
    {
        List<Usuario> usuarios = new List<Usuario>();
        string query = "SELECT UsuarioId, Nombre, Apellido, Email, Telefono, FechaRegistro, Activo FROM Usuario";
        
        DataTable dt = _acceso.Leer(query, null);

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
        param.Add(_acceso.CrearParametro("@UsuarioId", usuarioId));

        DataTable dt = _acceso.Leer(query, param);

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
        param.Add(_acceso.CrearParametro("@Nombre", usuario.Nombre));
        param.Add(_acceso.CrearParametro("@Apellido", usuario.Apellido));
        param.Add(_acceso.CrearParametro("@Email", usuario.Email));
        param.Add(_acceso.CrearParametro("@Telefono", usuario.Telefono ?? string.Empty));
        param.Add(_acceso.CrearParametro("@FechaRegistro", usuario.FechaRegistro));
        param.Add(_acceso.CrearParametro("@Activo", usuario.Activo));

        try
        {
            return _acceso.Escribir(query, param);
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

## Cadena de Responsabilidades

### Flujo State (DIPLOMA_STATE)
1. UI selecciona usuario y estado
2. UI llama a `CuentaUsuarioService.CambiarEstado()` o métodos de acción
3. `CuentaUsuarioService` delega acción al estado actual
4. Estado concreto valida y ejecuta acción
5. Estado actualiza saldo/estado vía `ICuentaUsuarioRepository` y `ITransaccionRepository`
6. Estado puede cambiar a otro estado según reglas

### Flujo Strategy (DIPLOMA_STRATEGY)
1. UI selecciona usuario e ingresa monto
2. UI genera resultado aleatorio (Gana/Pierde/Vuelve a intentar)
3. UI llama a `ApuestaService.ProcesarApuesta()`
4. `ApuestaService` crea `ProcesadorDeTransacciones` y establece estrategia según resultado
5. `ProcesadorDeTransacciones` ejecuta `IOperacion.ExecuteTransaction()`
6. Estrategia concreta valida y procesa transacción
7. Estrategia actualiza saldo vía `ICuentaUsuarioRepository` y registra transacción vía `ITransaccionRepository`
8. UI muestra resultado

### Flujo Visitor (DIPLOMA_VISITOR)
1. UI presiona botón de reporte
2. UI llama a `ReporteService.GenerarReporteImpuestos()` o `GenerarReporteComisiones()`
3. `ReporteService` llama a `TransaccionService.GetTransaccionesVisitable()`
4. `TransaccionService` carga todas las transacciones vía `ITransaccionRepository.GetAll()`
5. `TransaccionService` carga las transacciones específicas usando los repositories correspondientes:
   - `ITransaccionApuestaRepository.GetByTransaccionId()`
   - `ITransaccionRetiroRepository.GetByTransaccionId()`
   - `ITransaccionDepositoRepository.GetByTransaccionId()`
6. `TransaccionService` crea objetos `TransaccionApuesta`, `TransaccionRetiro`, `TransaccionDeposito` que implementan `ITransaccionVisitable`
7. `ReporteService` crea visitor concreto según reporte solicitado
8. `ReporteService` itera sobre transacciones llamando `Accept(visitor)`
9. Cada transacción llama al método `Visit()` correspondiente del visitor
10. Visitor acumula cálculos en su estado interno
11. `ReporteService` obtiene resultado con `visitor.GetResultado()`
12. UI muestra resultados en grid y texto

## Notas de Implementación

### Arquitectura DAL (Patrón Repository - Similar a Organola)

- **Clase Acceso**: Clase helper centralizada que maneja:
  - Conexiones a la base de datos (`AbrirConexion`, `CerrarConexion`)
  - Método `Leer()`: Ejecuta queries SQL de lectura y retorna `DataTable`
  - Método `Escribir()`: Ejecuta queries SQL de escritura y retorna `int` (ID generado o filas afectadas)
  - Métodos helper `CrearParametro()`: Crea `SqlParameter` tipados (int, decimal, string, DateTime, bool)
  - Usa `ConfigurationManager` para obtener connection string desde app.config
  - **Cada solución tiene su propia instancia de `Acceso`**

- **DAL.Abstraccion**: Contiene interfaces separadas por entidad (una interfaz por cada entidad BE)

- **DAL.Implementacion**: 
  - Cada repository recibe `Acceso` via inyección de dependencias
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
- Las entidades de Visitor (`TransaccionApuesta`, `TransaccionRetiro`, `TransaccionDeposito`) implementan `ITransaccionVisitable` que está en `BE.Visitor`, evitando la dependencia circular
- Los visitors concretos (`CalculadoraImpuestosVisitor`, `GeneradorComisionesVisitor`) están en `BLL` e implementan `ITransaccionVisitor` de `BE`

### Inyección de Dependencias

Se utiliza `Microsoft.Extensions.DependencyInjection` (incluido en .NET 8) para gestionar las dependencias.

**Configuración en Program.cs (Ejemplo para DIPLOMA_STATE):**

```csharp
using Microsoft.Extensions.DependencyInjection;
using SportsbookPatterns.BLL.State;
using SportsbookPatterns.BLL.Services;
using SportsbookPatterns.DAL.Abstraccion;
using SportsbookPatterns.DAL.Implementacion;

var services = new ServiceCollection();

// Registrar Acceso como Singleton
services.AddSingleton<Acceso>();

// Registrar Repositories (DAL) - usando factory para inyectar Acceso
services.AddSingleton<IUsuarioRepository>(sp => new UsuarioRepository(sp.GetRequiredService<Acceso>()));
services.AddSingleton<ICuentaUsuarioRepository>(sp => new CuentaUsuarioRepository(sp.GetRequiredService<Acceso>()));
services.AddSingleton<ITransaccionRepository>(sp => new TransaccionRepository(sp.GetRequiredService<Acceso>()));
services.AddSingleton<ITransaccionApuestaRepository>(sp => new TransaccionApuestaRepository(sp.GetRequiredService<Acceso>()));
services.AddSingleton<ITransaccionRetiroRepository>(sp => new TransaccionRetiroRepository(sp.GetRequiredService<Acceso>()));
services.AddSingleton<ITransaccionDepositoRepository>(sp => new TransaccionDepositoRepository(sp.GetRequiredService<Acceso>()));
services.AddSingleton<IEstadoCuentaRepository>(sp => new EstadoCuentaRepository(sp.GetRequiredService<Acceso>()));
services.AddSingleton<ITipoTransaccionRepository>(sp => new TipoTransaccionRepository(sp.GetRequiredService<Acceso>()));

// Registrar Servicios BLL
services.AddTransient<UsuarioService>();
services.AddTransient<CuentaUsuarioService>();

// Registrar Formularios
services.AddTransient<FrmState>();

var serviceProvider = services.BuildServiceProvider();
Program.ServiceProvider = serviceProvider;

ApplicationConfiguration.Initialize();
Application.Run(serviceProvider.GetRequiredService<FrmState>());
```

**Uso en Formularios:**

```csharp
public partial class FrmState : Form
{
    private readonly UsuarioService _usuarioService;
    private readonly CuentaUsuarioService _cuentaService;

    public FrmState(
        UsuarioService usuarioService,
        CuentaUsuarioService cuentaService)
    {
        InitializeComponent();
        _usuarioService = usuarioService;
        _cuentaService = cuentaService;
    }
}
```

**Nota**: La UI nunca accede directamente a la DAL. Todas las interacciones con la DAL se realizan a través de los servicios BLL.

### Configuración (app.config)

Cada solución tiene su propio `app.config` con la misma cadena de conexión:

```xml
<configuration>
  <connectionStrings>
    <add name="DefaultConnection" 
         connectionString="Data Source=localhost\SQLEXPRESS;Initial Catalog=SportsbookDB;Integrated Security=True;TrustServerCertificate=True" />
  </connectionStrings>
</configuration>
```

### Validaciones y Reglas de Negocio

- Validaciones de negocio están en BLL, no en UI ni DAL
- La DAL solo se encarga de persistencia (CRUD)
- Los mensajes de error deben ser descriptivos para el usuario
- Las entidades BE se mapean directamente desde la BD en la DAL
- Los DTOs se crean en BLL cuando se necesita transferir datos específicos a la UI
