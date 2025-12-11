# Guía de Implementación - Sistema de Apuestas

## Estructura de la Solución

### Proyectos de la Solución (.sln)

```
SportsbookPatterns.sln
├── SportsbookPatterns.UI (WinForms - .NET 8)
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

### 2. SportsbookPatterns.BLL

```
SportsbookPatterns.BLL/
├── State/
│   ├── IEstadoCuenta.cs
│   ├── EstadoCreada.cs
│   ├── EstadoActivaSinFondos.cs
│   ├── EstadoActivaConFondos.cs
│   ├── EstadoBloqueada.cs
│   └── CuentaUsuario.cs
├── Strategy/
│   ├── IOperacion.cs
│   ├── OperacionDebito.cs
│   ├── OperacionCredito.cs
│   ├── OperacionCancelacion.cs
│   └── ProcesadorDeTransacciones.cs
├── Visitor/
│   ├── ITransaccionVisitable.cs
│   ├── ITransaccionVisitor.cs
│   ├── TransaccionApuesta.cs
│   ├── TransaccionRetiro.cs
│   ├── TransaccionDeposito.cs
│   ├── CalculadoraImpuestosVisitor.cs
│   └── GeneradorComisionesVisitor.cs
└── DTOs/
    ├── UsuarioDTO.cs
    ├── CuentaUsuarioDTO.cs
    ├── TransaccionDTO.cs
    └── ResultadoTransaccion.cs
```

### 3. SportsbookPatterns.DAL.Abstraccion

```
SportsbookPatterns.DAL.Abstraccion/
└── IDataAccess.cs
```

### 4. SportsbookPatterns.DAL.Implementacion

```
SportsbookPatterns.DAL.Implementacion/
└── SqlServerDataAccess.cs
```

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

## Interfaces y Contratos

### IDataAccess (DAL.Abstraccion)

```csharp
public interface IDataAccess
{
    List<UsuarioDTO> GetAllUsuarios();
    UsuarioDTO GetUsuarioById(int usuarioId);
    CuentaUsuarioDTO GetCuentaByUsuarioId(int usuarioId);
    decimal GetSaldo(int cuentaId);
    bool UpdateSaldo(int cuentaId, decimal nuevoSaldo);
    bool UpdateEstadoCuenta(int cuentaId, int estadoCuentaId);
    int CreateTransaccion(TransaccionDTO transaccion);
    void LogTransaction(int transaccionId, string tipo, bool exito, string mensaje);
    List<ITransaccionVisitable> GetAllTransactions();
    List<TransaccionDTO> GetTransaccionesByCuentaId(int cuentaId);
}
```

### IEstadoCuenta (BLL.State)

```csharp
public interface IEstadoCuenta
{
    void Apostar(CuentaUsuario cuenta, decimal monto, IDataAccess dataAccess);
    void Retirar(CuentaUsuario cuenta, decimal monto, IDataAccess dataAccess);
    void Depositar(CuentaUsuario cuenta, decimal monto, IDataAccess dataAccess);
    string GetNombreEstado();
}
```

### IOperacion (BLL.Strategy)

```csharp
public interface IOperacion
{
    ResultadoTransaccion ExecuteTransaction(TransaccionDTO transaccion, IDataAccess dataAccess);
    string GetNombreOperacion();
}
```

### ITransaccionVisitable (BLL.Visitor)

```csharp
public interface ITransaccionVisitable
{
    void Accept(ITransaccionVisitor visitor);
    int TransaccionId { get; }
    decimal Monto { get; }
    DateTime FechaTransaccion { get; }
}
```

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

## DTOs Principales

### TransaccionDTO

```csharp
public class TransaccionDTO
{
    public int TransaccionId { get; set; }
    public int CuentaId { get; set; }
    public int TipoTransaccionId { get; set; }
    public decimal Monto { get; set; }
    public DateTime FechaTransaccion { get; set; }
    public string Descripcion { get; set; }
    public bool Exitoso { get; set; }
}
```

### CuentaUsuarioDTO

```csharp
public class CuentaUsuarioDTO
{
    public int CuentaId { get; set; }
    public int UsuarioId { get; set; }
    public decimal Saldo { get; set; }
    public int EstadoCuentaId { get; set; }
    public string CodigoEstado { get; set; }
}
```

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
2. UI llama a `CuentaUsuario.CambiarEstado()`
3. `CuentaUsuario` delega acción al estado actual
4. Estado concreto valida y ejecuta acción
5. Estado actualiza saldo/estado vía `IDataAccess`
6. Estado puede cambiar a otro estado según reglas

### Flujo Strategy
1. UI selecciona usuario e ingresa monto
2. UI genera resultado aleatorio (Gana/Pierde/Vuelve a intentar)
3. UI crea `ProcesadorDeTransacciones` y establece estrategia según resultado
4. `ProcesadorDeTransacciones` ejecuta `IOperacion.ExecuteTransaction()`
5. Estrategia concreta valida y procesa transacción
6. Estrategia actualiza saldo vía `IDataAccess`
7. UI muestra resultado

### Flujo Visitor
1. UI presiona botón de reporte
2. BLL carga todas las transacciones vía `IDataAccess.GetAllTransactions()`
3. BLL crea visitor concreto según reporte solicitado
4. BLL itera sobre transacciones llamando `Accept(visitor)`
5. Cada transacción llama al método `Visit()` correspondiente del visitor
6. Visitor acumula cálculos en su estado interno
7. BLL obtiene resultado con `visitor.GetResultado()`
8. UI muestra resultados en grid y texto

## Notas de Implementación

- La DAL.Implementacion se implementará después basándose en el ejemplo de Organola V5
- Todos los proyectos deben referenciar `DAL.Abstraccion`
- Solo `DAL.Implementacion` implementa `IDataAccess`
- Los formularios inyectan `IDataAccess` (preferiblemente por constructor o DI)
- Validaciones de negocio están en BLL, no en UI
- Los mensajes de error deben ser descriptivos para el usuario

