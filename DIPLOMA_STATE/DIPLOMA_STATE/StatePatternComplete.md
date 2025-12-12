# Diagrama de Clases - Patrón State (Completo)

## Diagrama Mermaid

```mermaid
classDiagram
    %% ============================================
    %% PATRÓN STATE
    %% ============================================
    
    class IEstadoCuenta {
        <<interface>>
        +Apostar(CuentaUsuario, decimal, ...) void
        +Retirar(CuentaUsuario, decimal, ...) void
        +Depositar(CuentaUsuario, decimal, ...) void
        +GetNombreEstado() string
    }
    
    class EstadoCreada {
        <<Concrete State>>
        +Apostar(...) void
        +Retirar(...) void
        +Depositar(...) void
        +GetNombreEstado() string
    }
    
    class EstadoActivaSinFondos {
        <<Concrete State>>
        +Apostar(...) void
        +Retirar(...) void
        +Depositar(...) void
        +GetNombreEstado() string
    }
    
    class EstadoActivaConFondos {
        <<Concrete State>>
        +Apostar(...) void
        +Retirar(...) void
        +Depositar(...) void
        +GetNombreEstado() string
    }
    
    class EstadoBloqueada {
        <<Concrete State>>
        +Apostar(...) void
        +Retirar(...) void
        +Depositar(...) void
        +GetNombreEstado() string
    }
    
    class CuentaUsuario {
        <<Context>>
        +CuentaId int
        +UsuarioId int
        +Saldo decimal
        +EstadoCuentaId int
        +FechaCreacion DateTime
        +FechaUltimaModificacion DateTime?
    }
    
    class CuentaUsuarioService {
        <<State Manager>>
        +CambiarEstado(CuentaUsuario, string) void
        +ObtenerEstado(CuentaUsuario) IEstadoCuenta
        +Apostar(CuentaUsuario, decimal) void
        +Retirar(CuentaUsuario, decimal) void
        +Depositar(CuentaUsuario, decimal) void
        +GetCuentaByUsuarioId(int) CuentaUsuario
        +GetTransaccionesByCuenta(int) List~Transaccion~
    }
    
    class UsuarioService {
        +GetAll() List~Usuario~
        +GetById(int) Usuario
    }
    
    %% ============================================
    %% ENTIDADES DE NEGOCIO
    %% ============================================
    
    class Usuario {
        +UsuarioId int
        +Nombre string
        +Apellido string
        +Email string
        +Telefono string?
        +FechaRegistro DateTime
        +Activo bool
    }
    
    class EstadoCuenta {
        +EstadoCuentaId int
        +CodigoEstado string
        +Descripcion string
    }
    
    class Transaccion {
        +TransaccionId int
        +CuentaId int
        +TipoTransaccionId int
        +Monto decimal
        +FechaTransaccion DateTime
        +Descripcion string
        +Exitoso bool
    }
    
    class TransaccionApuesta {
        +TransaccionApuestaId int
        +TransaccionId int
        +EventoDeportivo string
        +EquipoApostado string
        +Cuota decimal
        +MontoApostado decimal
    }
    
    class TransaccionRetiro {
        +TransaccionRetiroId int
        +TransaccionId int
        +MetodoRetiro string
        +NumeroCuentaDestino string
        +BancoDestino string
    }
    
    class TransaccionDeposito {
        +TransaccionDepositoId int
        +TransaccionId int
        +MetodoDeposito string
        +NumeroCuentaOrigen string
        +BancoOrigen string
    }
    
    class TipoTransaccion {
        +TipoTransaccionId int
        +CodigoTipo string
        +Descripcion string
    }
    
    %% ============================================
    %% DAL - REPOSITORIOS
    %% ============================================
    
    class ICuentaUsuarioRepository {
        <<interface>>
        +GetById(int) CuentaUsuario
        +GetByUsuarioId(int) CuentaUsuario
        +GetSaldo(int) decimal
        +UpdateSaldo(int, decimal) bool
        +UpdateEstado(int, int) bool
    }
    
    class IEstadoCuentaRepository {
        <<interface>>
        +GetAll() List~EstadoCuenta~
        +GetById(int) EstadoCuenta
        +GetByCodigo(string) EstadoCuenta
    }
    
    class ITransaccionRepository {
        <<interface>>
        +GetAll() List~Transaccion~
        +GetByCuentaId(int) List~Transaccion~
        +Insert(Transaccion) int
    }
    
    class ITransaccionApuestaRepository {
        <<interface>>
        +GetByTransaccionId(int) TransaccionApuesta
        +Insert(TransaccionApuesta) int
    }
    
    class ITransaccionRetiroRepository {
        <<interface>>
        +GetByTransaccionId(int) TransaccionRetiro
        +Insert(TransaccionRetiro) int
    }
    
    class ITransaccionDepositoRepository {
        <<interface>>
        +GetByTransaccionId(int) TransaccionDeposito
        +Insert(TransaccionDeposito) int
    }
    
    class ITipoTransaccionRepository {
        <<interface>>
        +GetByCodigo(string) TipoTransaccion
    }
    
    %% ============================================
    %% RELACIONES - PATRÓN STATE
    %% ============================================
    
    %% Herencia: Estados concretos implementan IEstadoCuenta
    IEstadoCuenta <|.. EstadoCreada : implements
    IEstadoCuenta <|.. EstadoActivaSinFondos : implements
    IEstadoCuenta <|.. EstadoActivaConFondos : implements
    IEstadoCuenta <|.. EstadoBloqueada : implements
    
    %% Context tiene un estado
    CuentaUsuario --> EstadoCuenta : references\n(EstadoCuentaId)
    CuentaUsuario --> Usuario : references\n(UsuarioId)
    
    %% Service maneja estados y contexto
    CuentaUsuarioService --> CuentaUsuario : manages
    CuentaUsuarioService ..> IEstadoCuenta : creates\n(ObtenerEstado)
    CuentaUsuarioService --> IEstadoCuenta : delegates to
    
    %% Service usa Repositories
    CuentaUsuarioService --> ICuentaUsuarioRepository : uses
    CuentaUsuarioService --> IEstadoCuentaRepository : uses
    CuentaUsuarioService --> ITransaccionRepository : uses
    CuentaUsuarioService --> ITransaccionApuestaRepository : uses
    CuentaUsuarioService --> ITransaccionRetiroRepository : uses
    CuentaUsuarioService --> ITransaccionDepositoRepository : uses
    CuentaUsuarioService --> ITipoTransaccionRepository : uses
    
    UsuarioService --> IUsuarioRepository : uses
    
    %% Estados usan Repositories
    IEstadoCuenta --> ICuentaUsuarioRepository : uses
    IEstadoCuenta --> ITransaccionRepository : uses
    IEstadoCuenta --> ITransaccionApuestaRepository : uses
    IEstadoCuenta --> ITransaccionRetiroRepository : uses
    IEstadoCuenta --> ITransaccionDepositoRepository : uses
    IEstadoCuenta --> ITipoTransaccionRepository : uses
    
    %% Entities relationships
    TransaccionApuesta --> Transaccion : references
    TransaccionRetiro --> Transaccion : references
    TransaccionDeposito --> Transaccion : references
    Transaccion --> CuentaUsuario : references
    Transaccion --> TipoTransaccion : references
```

## Componentes del Patrón State

1. **IEstadoCuenta** (State Interface): Define el contrato común para todos los estados
2. **EstadoCreada, EstadoActivaSinFondos, EstadoActivaConFondos, EstadoBloqueada** (Concrete States): Implementaciones concretas de cada estado
3. **CuentaUsuario** (Context): Mantiene referencia al estado actual a través de `EstadoCuentaId`
4. **CuentaUsuarioService** (State Manager): Gestiona las transiciones de estado y delega acciones al estado actual

## Flujo de Ejecución

1. `CuentaUsuarioService` obtiene el estado actual de la cuenta mediante `ObtenerEstado()`
2. El servicio crea la instancia del estado concreto correspondiente según `EstadoCuentaId`
3. El servicio delega la acción (Apostar/Retirar/Depositar) al estado actual
4. El estado concreto valida y ejecuta la acción según sus reglas
5. El estado puede cambiar a otro estado según las reglas de negocio
6. Los estados usan los repositorios para persistir cambios

## Leyenda

- **<|..** : Implementación (implements)
- **-->** : Asociación/Uso
- **..>** : Dependencia/Creación
- **<<interface>>** : Indica una interfaz
- **<<Concrete State>>** : Estado concreto
- **<<Context>>** : Contexto del patrón State
- **<<State Manager>>** : Gestor de estados

