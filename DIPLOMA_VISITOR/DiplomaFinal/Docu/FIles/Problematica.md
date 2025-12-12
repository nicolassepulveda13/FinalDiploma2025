# 📝 Contexto Unificado para Examen Final: Patrones de Diseño

## 1. Problemática General y Justificación de Patrones

### 1.1. Contexto del Negocio: Sistema de Apuestas (Sportsbook)

El sistema opera en un entorno de alta complejidad donde la lógica de negocio debe ser flexible, extensible y fácil de auditar. Dos áreas clave generan complejidad: la gestión del ciclo de vida de la cuenta de usuario y el procesamiento de transacciones heterogéneas.

### 1.2. Justificación de Patrones (Ingeniería de Software)

Para abordar esta complejidad y cumplir con los principios SOLID (especialmente el Principio Abierto/Cerrado y la Inversión de Dependencia), se utilizarán los patrones Strategy, State y Visitor.

- **Strategy**: Permite intercambiar algoritmos de procesamiento de transacciones (Débito, Crédito) sin modificar el cliente (ProcesadorDeTransacciones). Ideal para manejar diferentes reglas de negocio por tipo de operación.

- **State**: Permite que la cuenta de usuario cambie su comportamiento (Apostar, Retirar) según su estado interno (ConFondos, SinFondos), eliminando condicionales complejos en la clase principal.

- **Visitor**: Permite añadir nuevas operaciones (Reportes, Auditorías) sobre la estructura de objetos de transacciones sin modificar las clases de esas transacciones, facilitando la auditoría financiera.

## 2. Enunciados Específicos por Patrón

A continuación, se presentan los escenarios propuestos para cada tema del examen, cumpliendo con la unicidad requerida y los requisitos de C#/WinForms, 4+ capas y acceso a datos.

### Tema 1: Patrón State

**Enunciado**: Crear un escenario para resolver un problema utilizando el patrón: State.

**Problema Propuesto**: Implementar la gestión de las interacciones de un usuario con su cuenta, cuyo comportamiento principal cambia drásticamente en función de su estado financiero y operativo. Las acciones permitidas o su lógica interna (Apostar, Retirar, Depositar) deben depender del estado actual de la cuenta.

### Tema 2: Patrón Strategy

**Enunciado**: Crear un escenario para resolver un problema utilizando el patrón: Strategy.

**Problema Propuesto**: Implementar un módulo que procese transacciones financieras (Débito, Crédito, Cancelación) donde las validaciones, el impacto en el saldo y el logging son específicos para cada tipo de operación. El sistema debe permitir definir nuevas operaciones sin modificar la clase que las ejecuta.

### Tema 3: Patrón Visitor

**Enunciado**: Crear un escenario para resolver un problema utilizando el patrón: Visitor.

**Problema Propuesto**: Implementar un sistema de auditoría y reportes financieros que necesite aplicar múltiples operaciones (cálculo de impuestos, cálculo de comisiones, generación de logs específicos) sobre una colección heterogénea de objetos de transacción (TransaccionApuesta, TransaccionRetiro, TransaccionDeposito).

## 3. Estructura de Resolución por Patrón

### 3.1. Patrón Strategy (Tema 2)

#### Definición del Problema

El desafío es procesar transacciones de diferentes tipos (Débito, Crédito, Cancelación) a través de un único punto de entrada (ProcesadorDeTransacciones). Si se utilizara un enfoque tradicional, esta clase estaría llena de sentencias if/else o switch anidadas para determinar y ejecutar la lógica específica de validación, reglas de negocio y actualización de datos para cada tipo. Este acoplamiento hace que la clase sea difícil de mantener y expandir.

#### Justificación del Patrón

Se utiliza Strategy para desacoplar el Contexto (ProcesadorDeTransacciones) de los algoritmos de operación (Estrategias Concretas). Esto garantiza el Principio Abierto/Cerrado (OCP), permitiendo añadir nuevas operaciones (ej. "Reembolso") simplemente creando una nueva clase IOperacion sin modificar el ProcesadorDeTransacciones ni las clases existentes.

#### Componentes Clave

- **Estrategia (Interface)**: `IOperacion`
- **Estrategias Concretas**: `OperacionDebito`, `OperacionCredito`, `OperacionCancelar`
- **Contexto**: `ProcesadorDeTransacciones`

#### Arquitectura y Capas

| Capa | Rol del Componente | Interacción con el Patrón |
|------|-------------------|--------------------------|
| Presentación (UI) | `FormTransaccion` | Permite al usuario seleccionar el tipo de operación (Define la Strategy). |
| Lógica de Negocio (BLL) | `ProcesadorDeTransacciones` | Inyecta la IDataAccess y ejecuta la estrategia seleccionada. |
| Servicios (BLL.Services) | `IOperacion`, Estrategias Concretas | Implementan la lógica de negocio y contienen la llamada a IDataAccess (Acceso a Datos). |
| Persistencia (DAL) | `IDataAccess`, `SqlServerDataAccess` | Se utiliza para UpdateSaldo y LogTransaction. |

#### Diagrama de Clases (Conceptual)

#### Ejemplo de Interfaz C#

```csharp
// Capa: BLL.Services
public interface IOperacion
{
    // Se incluye IDataAccess para que cada estrategia pueda persistir sus cambios.
    ResultadoTransaccion ExecuteTransaction(TransaccionDTO transaction, IDataAccess dataAccess);
}

// Capa: BLL
public class ProcesadorDeTransacciones
{
    private IOperacion _estrategia;
    public ProcesadorDeTransacciones(IDataAccess dataAccess) { /* ... */ }
    // ... métodos SetStrategy y ProcessTransaction
}
```

### 3.2. Patrón State (Tema 1)

#### Definición del Problema

En la CuentaUsuario, las acciones como Apostar o Retirar tienen una lógica completamente diferente según el saldo o el estado administrativo (ej. Bloqueada). Mantener toda esta lógica en la clase CuentaUsuario resultaría en un código muy acoplado, lleno de sentencias condicionales que evalúan una variable de estado interna (if (this.saldo <= 0) o if (this.estadoBloqueo == true)), lo que se conoce como una "máquina de estados dispersa".

#### Justificación del Patrón

Se utiliza State para centralizar la lógica de cada comportamiento en clases de estado separadas (EstadoActivaConFondos, EstadoBloqueada). Esto elimina las grandes estructuras condicionales del Contexto (CuentaUsuario), delegando la responsabilidad del comportamiento a los objetos de estado y permitiendo que las transiciones sean responsabilidad de los propios estados concretos. Esto mejora la claridad y facilita la adición de nuevos estados.

#### Componentes Clave

- **Estado (Interface)**: `IEstadoCuenta`
- **Estados Concretos**: `EstadoCreada`, `EstadoActiva`, `EstadoBloqueada`
- **Contexto**: `CuentaUsuario`

#### Arquitectura y Capas

| Capa | Rol del Componente | Interacción con el Patrón |
|------|-------------------|--------------------------|
| Presentación (UI) | `FormCuenta` | Botones de acción (btnApostar, btnRetirar) que llaman al Contexto. |
| Lógica de Negocio (BLL) | `CuentaUsuario` | Mantiene el estado y delega las acciones. Ejecuta CambiarEstado. |
| Servicios (BLL.Estados) | `IEstadoCuenta`, Estados Concretos | Las clases de Estado usan el Contexto y IDataAccess para cambiar saldo/estado (Acceso a Datos). |
| Persistencia (DAL) | `IDataAccess`, `SqlServerDataAccess` | Se utiliza para GetSaldo, UpdateSaldo y UpdateEstadoCuenta. |

#### Diagrama de Clases (Conceptual)

#### Ejemplo de Contexto C#

```csharp
// Capa: BLL
public class CuentaUsuario
{
    private IEstadoCuenta _estadoActual;
    private readonly IDataAccess _dataAccess;

    public void CambiarEstado(IEstadoCuenta nuevoEstado) { this._estadoActual = nuevoEstado; }

    // El comportamiento varía al delegar al estado actual
    public void Apostar(decimal monto) => _estadoActual.Apostar(this, monto, _dataAccess);
    
    // Método que llama a DAL y verifica si hay que cambiar de estado automáticamente
    public void ActualizarSaldo(decimal monto) { /* ... */ } 
}
```

### 3.3. Patrón Visitor (Tema 3)

#### Definición del Problema

El sistema almacena diversos tipos de transacciones financieras (Apuestas, Retiros, Depósitos), y la gerencia requiere constantemente nuevos reportes o auditorías financieras (ej. calcular nuevos impuestos, evaluar diferentes comisiones, generar reportes XML específicos). Si se añade la lógica de cada reporte (e.g., CalcularImpuesto()) directamente a cada clase de transacción (TransaccionApuesta, TransaccionRetiro), estas clases se volverían inestables y tendrían que modificarse cada vez que se requiera un nuevo reporte, violando el Principio Abierto/Cerrado (OCP).

#### Justificación del Patrón

Se utiliza Visitor para mover la lógica de las operaciones de reporte y auditoría a clases externas (los Visitantes Concretos). Esto permite que la estructura de objetos de transacción (los Elementos Concretos) permanezca estable (cerrada a modificación), mientras que se pueden añadir infinitas operaciones de reporte (abierta a extensión) simplemente creando nuevos ITransaccionVisitor.

#### Componentes Clave

- **Elemento (Interface)**: `ITransaccionVisitable`
- **Elementos Concretos**: `TransaccionApuesta`, `TransaccionRetiro`, `TransaccionDeposito`
- **Visitante (Interface)**: `ITransaccionVisitor`
- **Visitantes Concretos**: `CalculadoraImpuestosVisitor`, `GeneradorComisionesVisitor`

#### Arquitectura y Capas

| Capa | Rol del Componente | Interacción con el Patrón |
|------|-------------------|--------------------------|
| Presentación (UI) | `FormReportes` | Muestra el resultado de la auditoría generada por el Visitor. |
| Lógica de Negocio (BLL) | (Clase de Reporte) | Orquesta la carga de datos (DAL) e itera sobre la colección de Elementos, aplicando el Visitor. |
| Servicios (BLL.Servicios) | Visitors Concretos | Contienen la lógica de negocio no relacionada con la persistencia (ej. fórmulas de impuestos). |
| Persistencia (DAL) | `IDataAccess`, `SqlServerDataAccess` | Se utiliza para GetAllTransactions() y LogReport. (Acceso a Datos) |

#### Diagrama de Clases (Conceptual)

#### Ejemplo de Elemento C#

```csharp
// Capa: DTO/BLL.Servicios
public interface ITransaccionVisitable
{
    // Método clave que permite al Visitor ejecutar su lógica específica
    void Accept(ITransaccionVisitor visitor); 
}

public class TransaccionApuesta : ITransaccionVisitable
{
    // ... datos de la transacción
    public void Accept(ITransaccionVisitor visitor)
    {
        visitor.Visit(this); // Llama al método Visit(TransaccionApuesta)
    }
}
```

## 4. Estructura de Soluciones

El proyecto está organizado en **3 soluciones independientes**, cada una enfocada en un patrón específico:

- **DIPLOMA_STATE**: Solución independiente para el patrón State
- **DIPLOMA_STRATEGY**: Solución independiente para el patrón Strategy  
- **DIPLOMA_VISITOR**: Solución independiente para el patrón Visitor

Cada solución contiene sus propios proyectos (BE, BLL, DAL) y su propia clase `Acceso`, pero todas comparten la misma base de datos (`SportsbookDB`). Cada solución inicia directamente en su formulario específico, sin menú principal.

## 5. Definición de la Capa DAL (Acceso a Datos)

Para cumplir con el requisito de acceso a datos, todos los BLLs dependen de esta abstracción.

```csharp
// Capa: DAL.Abstraccion
public interface IDataAccess
{
    // Común a State y Strategy
    decimal GetSaldo(int userId);
    bool UpdateSaldo(int userId, decimal newAmount);
    void LogTransaction(TransaccionDTO transaction, string type, bool success);

    // Específico para State
    bool UpdateEstadoCuenta(int userId, string newState); 

    // Específico para Visitor
    List<ITransaccionVisitable> GetAllTransactions(); 
}

// Capa: DAL.Impl (Implementación Concreta)
public class SqlServerDataAccess : IDataAccess
{
    // Implementación real de la conexión a DB y queries SQL.
    // ...
}
```
