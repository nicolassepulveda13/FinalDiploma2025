# Diagrama de Clases - Patrón State (Solo Patrón)

## Diagrama Mermaid

```mermaid
classDiagram
    %% ============================================
    %% PATRÓN STATE - COMPONENTES PRINCIPALES
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
    }
    
    class CuentaUsuarioService {
        <<State Manager>>
        +CambiarEstado(CuentaUsuario, string) void
        +ObtenerEstado(CuentaUsuario) IEstadoCuenta
        +Apostar(CuentaUsuario, decimal) void
        +Retirar(CuentaUsuario, decimal) void
        +Depositar(CuentaUsuario, decimal) void
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
    CuentaUsuario --> IEstadoCuenta : has state\n(EstadoCuentaId)
    
    %% Service maneja estados y contexto
    CuentaUsuarioService --> CuentaUsuario : manages
    CuentaUsuarioService ..> IEstadoCuenta : creates\n(ObtenerEstado)
    CuentaUsuarioService --> IEstadoCuenta : delegates to\n(Apostar/Retirar/Depositar)
```

## Componentes del Patrón State

1. **IEstadoCuenta** (State Interface): Define el contrato común para todos los estados
2. **EstadoCreada, EstadoActivaSinFondos, EstadoActivaConFondos, EstadoBloqueada** (Concrete States): Implementaciones concretas de cada estado
3. **CuentaUsuario** (Context): Mantiene referencia al estado actual a través de `EstadoCuentaId`
4. **CuentaUsuarioService** (State Manager): Gestiona las transiciones de estado y delega acciones al estado actual

## Flujo de Ejecución

1. `CuentaUsuarioService` obtiene el estado actual de la cuenta mediante `ObtenerEstado()`
2. El servicio crea la instancia del estado concreto correspondiente
3. El servicio delega la acción (Apostar/Retirar/Depositar) al estado actual
4. El estado concreto valida y ejecuta la acción según sus reglas
5. El estado puede cambiar a otro estado según las reglas de negocio

## Leyenda

- **<|..** : Implementación (implements)
- **-->** : Asociación/Uso
- **..>** : Dependencia/Creación
- **<<interface>>** : Indica una interfaz
- **<<Concrete State>>** : Estado concreto
- **<<Context>>** : Contexto del patrón State
- **<<State Manager>>** : Gestor de estados

