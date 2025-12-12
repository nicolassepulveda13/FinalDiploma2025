# Diagrama de Clases - Patrón Visitor (Solo Patrón)

## Diagrama Mermaid

```mermaid
classDiagram
    %% ============================================
    %% PATRÓN VISITOR - COMPONENTES PRINCIPALES
    %% ============================================
    
    class ITransaccionVisitable {
        <<BE.Visitor - interface>>
        +Accept(ITransaccionVisitor) void
        +TransaccionIdProp int
        +Monto decimal
        +FechaTransaccion DateTime
    }
    
    class ITransaccionVisitor {
        <<BE.Visitor - interface>>
        +Visit(TransaccionApuesta) void
        +Visit(TransaccionRetiro) void
        +Visit(TransaccionDeposito) void
        +GetResultado() object
    }
    
    class TransaccionApuesta {
        <<BE - Concrete Element>>
        +TransaccionApuestaId int
        +TransaccionId int
        +EventoDeportivo string
        +EquipoApostado string
        +Cuota decimal
        +MontoApostado decimal
        +Accept(ITransaccionVisitor) void
    }
    
    class TransaccionRetiro {
        <<BE - Concrete Element>>
        +TransaccionRetiroId int
        +TransaccionId int
        +MetodoRetiro string
        +NumeroCuentaDestino string
        +BancoDestino string
        +Accept(ITransaccionVisitor) void
    }
    
    class TransaccionDeposito {
        <<BE - Concrete Element>>
        +TransaccionDepositoId int
        +TransaccionId int
        +MetodoDeposito string
        +NumeroCuentaOrigen string
        +BancoOrigen string
        +Accept(ITransaccionVisitor) void
    }
    
    class CalculadoraImpuestosVisitor {
        <<BLL.Visitor - Concrete Visitor>>
        -_totalImpuestos decimal
        -_detalles List~string~
        +Visit(TransaccionApuesta) void
        +Visit(TransaccionRetiro) void
        +Visit(TransaccionDeposito) void
        +GetResultado() object
    }
    
    class GeneradorComisionesVisitor {
        <<BLL.Visitor - Concrete Visitor>>
        -_totalComisiones decimal
        -_detalles List~string~
        +Visit(TransaccionApuesta) void
        +Visit(TransaccionRetiro) void
        +Visit(TransaccionDeposito) void
        +GetResultado() object
    }
    
    class ReporteService {
        <<BLL.Services - Client>>
        -_transaccionService TransaccionService
        +GenerarReporteImpuestos() object
        +GenerarReporteComisiones() object
        +GenerarReporteGeneral() tuple
    }
    
    class TransaccionService {
        <<BLL.Services>>
        +GetTransaccionesVisitable() List~ITransaccionVisitable~
    }
    
    %% ============================================
    %% RELACIONES - PATRÓN VISITOR
    %% ============================================
    
    %% Elements implementan ITransaccionVisitable
    ITransaccionVisitable <|.. TransaccionApuesta : implements
    ITransaccionVisitable <|.. TransaccionRetiro : implements
    ITransaccionVisitable <|.. TransaccionDeposito : implements
    
    %% Visitors implementan ITransaccionVisitor
    ITransaccionVisitor <|.. CalculadoraImpuestosVisitor : implements
    ITransaccionVisitor <|.. GeneradorComisionesVisitor : implements
    
    %% Elements aceptan Visitors
    TransaccionApuesta --> ITransaccionVisitor : accepts\n(Accept)
    TransaccionRetiro --> ITransaccionVisitor : accepts\n(Accept)
    TransaccionDeposito --> ITransaccionVisitor : accepts\n(Accept)
    
    %% Client usa Service y Visitors
    ReporteService --> TransaccionService : uses
    ReporteService ..> CalculadoraImpuestosVisitor : creates
    ReporteService ..> GeneradorComisionesVisitor : creates
    ReporteService --> ITransaccionVisitable : iterates over
```

## Componentes del Patrón Visitor

1. **ITransaccionVisitable** (Element Interface): Define el método `Accept()` que permite a los visitantes acceder al elemento
2. **TransaccionApuesta, TransaccionRetiro, TransaccionDeposito** (Concrete Elements): Implementan `ITransaccionVisitable` y definen `Accept()`
3. **ITransaccionVisitor** (Visitor Interface): Define métodos `Visit()` para cada tipo de elemento
4. **CalculadoraImpuestosVisitor, GeneradorComisionesVisitor** (Concrete Visitors): Implementan `ITransaccionVisitor` y contienen la lógica de cálculo
5. **ReporteService** (Client): Orquesta el patrón, crea visitantes y itera sobre elementos
6. **TransaccionService** (Service): Prepara los elementos visitables desde la base de datos

## Flujo de Ejecución

1. `ReporteService` llama a `TransaccionService.GetTransaccionesVisitable()` para obtener elementos
2. `TransaccionService` carga transacciones desde la BD y crea objetos visitables
3. `ReporteService` crea un visitor concreto (ej: `CalculadoraImpuestosVisitor`)
4. `ReporteService` itera sobre los elementos llamando `elemento.Accept(visitor)`
5. Cada elemento llama al método `Visit()` correspondiente del visitor
6. El visitor acumula cálculos en su estado interno
7. `ReporteService` obtiene el resultado con `visitor.GetResultado()`

## Leyenda

- **<|..** : Implementación (implements)
- **-->** : Asociación/Uso
- **..>** : Dependencia/Creación
- **<<interface>>** : Indica una interfaz
- **<<Concrete Element>>** : Elemento concreto
- **<<Concrete Visitor>>** : Visitante concreto
- **<<Client>>** : Cliente que usa el patrón

