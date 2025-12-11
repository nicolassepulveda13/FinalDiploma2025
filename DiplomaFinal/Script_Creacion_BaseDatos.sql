IF OBJECT_ID('dbo.LogTransacciones', 'U') IS NOT NULL DROP TABLE dbo.LogTransacciones;
IF OBJECT_ID('dbo.TransaccionDeposito', 'U') IS NOT NULL DROP TABLE dbo.TransaccionDeposito;
IF OBJECT_ID('dbo.TransaccionRetiro', 'U') IS NOT NULL DROP TABLE dbo.TransaccionRetiro;
IF OBJECT_ID('dbo.TransaccionApuesta', 'U') IS NOT NULL DROP TABLE dbo.TransaccionApuesta;
IF OBJECT_ID('dbo.Transaccion', 'U') IS NOT NULL DROP TABLE dbo.Transaccion;
IF OBJECT_ID('dbo.TipoTransaccion', 'U') IS NOT NULL DROP TABLE dbo.TipoTransaccion;
IF OBJECT_ID('dbo.CuentaUsuario', 'U') IS NOT NULL DROP TABLE dbo.CuentaUsuario;
IF OBJECT_ID('dbo.EstadoCuenta', 'U') IS NOT NULL DROP TABLE dbo.EstadoCuenta;
IF OBJECT_ID('dbo.Usuario', 'U') IS NOT NULL DROP TABLE dbo.Usuario;
GO

CREATE TABLE dbo.Usuario (
    UsuarioId INT IDENTITY(1,1) NOT NULL,
    Nombre NVARCHAR(100) NOT NULL,
    Apellido NVARCHAR(100) NOT NULL,
    Email NVARCHAR(255) NOT NULL,
    Telefono NVARCHAR(20) NULL,
    FechaRegistro DATETIME NOT NULL DEFAULT GETDATE(),
    Activo BIT NOT NULL DEFAULT 1,
    
    CONSTRAINT PK_Usuario PRIMARY KEY CLUSTERED (UsuarioId),
    CONSTRAINT UQ_Usuario_Email UNIQUE (Email),
    CONSTRAINT CK_Usuario_Email CHECK (Email LIKE '%@%.%')
);
GO

CREATE TABLE dbo.EstadoCuenta (
    EstadoCuentaId INT IDENTITY(1,1) NOT NULL,
    CodigoEstado NVARCHAR(50) NOT NULL,
    Descripcion NVARCHAR(200) NOT NULL,
    Activo BIT NOT NULL DEFAULT 1,
    
    CONSTRAINT PK_EstadoCuenta PRIMARY KEY CLUSTERED (EstadoCuentaId),
    CONSTRAINT UQ_EstadoCuenta_Codigo UNIQUE (CodigoEstado)
);
GO

CREATE TABLE dbo.CuentaUsuario (
    CuentaId INT IDENTITY(1,1) NOT NULL,
    UsuarioId INT NOT NULL,
    Saldo DECIMAL(18,2) NOT NULL DEFAULT 0.00,
    EstadoCuentaId INT NOT NULL,
    FechaCreacion DATETIME NOT NULL DEFAULT GETDATE(),
    FechaUltimaModificacion DATETIME NULL,
    
    CONSTRAINT PK_CuentaUsuario PRIMARY KEY CLUSTERED (CuentaId),
    CONSTRAINT FK_CuentaUsuario_Usuario FOREIGN KEY (UsuarioId) 
        REFERENCES dbo.Usuario(UsuarioId) ON DELETE CASCADE,
    CONSTRAINT FK_CuentaUsuario_EstadoCuenta FOREIGN KEY (EstadoCuentaId) 
        REFERENCES dbo.EstadoCuenta(EstadoCuentaId),
    CONSTRAINT UQ_CuentaUsuario_Usuario UNIQUE (UsuarioId),
    CONSTRAINT CK_CuentaUsuario_Saldo CHECK (Saldo >= 0)
);
GO

CREATE TABLE dbo.TipoTransaccion (
    TipoTransaccionId INT IDENTITY(1,1) NOT NULL,
    CodigoTipo NVARCHAR(50) NOT NULL,
    Descripcion NVARCHAR(200) NOT NULL,
    AfectaSaldo BIT NOT NULL DEFAULT 1,
    Activo BIT NOT NULL DEFAULT 1,
    
    CONSTRAINT PK_TipoTransaccion PRIMARY KEY CLUSTERED (TipoTransaccionId),
    CONSTRAINT UQ_TipoTransaccion_Codigo UNIQUE (CodigoTipo)
);
GO

CREATE TABLE dbo.Transaccion (
    TransaccionId INT IDENTITY(1,1) NOT NULL,
    CuentaId INT NOT NULL,
    TipoTransaccionId INT NOT NULL,
    Monto DECIMAL(18,2) NOT NULL,
    FechaTransaccion DATETIME NOT NULL DEFAULT GETDATE(),
    Descripcion NVARCHAR(500) NULL,
    Exitoso BIT NOT NULL DEFAULT 1,
    Observaciones NVARCHAR(1000) NULL,
    
    CONSTRAINT PK_Transaccion PRIMARY KEY CLUSTERED (TransaccionId),
    CONSTRAINT FK_Transaccion_CuentaUsuario FOREIGN KEY (CuentaId) 
        REFERENCES dbo.CuentaUsuario(CuentaId) ON DELETE CASCADE,
    CONSTRAINT FK_Transaccion_TipoTransaccion FOREIGN KEY (TipoTransaccionId) 
        REFERENCES dbo.TipoTransaccion(TipoTransaccionId),
    CONSTRAINT CK_Transaccion_Monto CHECK (Monto > 0)
);
GO

CREATE TABLE dbo.TransaccionApuesta (
    TransaccionApuestaId INT IDENTITY(1,1) NOT NULL,
    TransaccionId INT NOT NULL,
    EventoDeportivo NVARCHAR(200) NOT NULL,
    EquipoApostado NVARCHAR(100) NOT NULL,
    Cuota DECIMAL(10,2) NOT NULL,
    MontoApostado DECIMAL(18,2) NOT NULL,
    Resultado NVARCHAR(50) NULL,
    FechaEvento DATETIME NULL,
    
    CONSTRAINT PK_TransaccionApuesta PRIMARY KEY CLUSTERED (TransaccionApuestaId),
    CONSTRAINT FK_TransaccionApuesta_Transaccion FOREIGN KEY (TransaccionId) 
        REFERENCES dbo.Transaccion(TransaccionId) ON DELETE CASCADE,
    CONSTRAINT UQ_TransaccionApuesta_Transaccion UNIQUE (TransaccionId),
    CONSTRAINT CK_TransaccionApuesta_Cuota CHECK (Cuota > 0),
    CONSTRAINT CK_TransaccionApuesta_MontoApostado CHECK (MontoApostado > 0)
);
GO

CREATE TABLE dbo.TransaccionRetiro (
    TransaccionRetiroId INT IDENTITY(1,1) NOT NULL,
    TransaccionId INT NOT NULL,
    MetodoRetiro NVARCHAR(50) NOT NULL,
    NumeroCuentaDestino NVARCHAR(100) NULL,
    BancoDestino NVARCHAR(100) NULL,
    Comision DECIMAL(18,2) NOT NULL DEFAULT 0.00,
    FechaProcesamiento DATETIME NULL,
    
    CONSTRAINT PK_TransaccionRetiro PRIMARY KEY CLUSTERED (TransaccionRetiroId),
    CONSTRAINT FK_TransaccionRetiro_Transaccion FOREIGN KEY (TransaccionId) 
        REFERENCES dbo.Transaccion(TransaccionId) ON DELETE CASCADE,
    CONSTRAINT UQ_TransaccionRetiro_Transaccion UNIQUE (TransaccionId),
    CONSTRAINT CK_TransaccionRetiro_Comision CHECK (Comision >= 0)
);
GO

CREATE TABLE dbo.TransaccionDeposito (
    TransaccionDepositoId INT IDENTITY(1,1) NOT NULL,
    TransaccionId INT NOT NULL,
    MetodoDeposito NVARCHAR(50) NOT NULL,
    NumeroCuentaOrigen NVARCHAR(100) NULL,
    BancoOrigen NVARCHAR(100) NULL,
    NumeroReferencia NVARCHAR(100) NULL,
    Bonificacion DECIMAL(18,2) NOT NULL DEFAULT 0.00,
    
    CONSTRAINT PK_TransaccionDeposito PRIMARY KEY CLUSTERED (TransaccionDepositoId),
    CONSTRAINT FK_TransaccionDeposito_Transaccion FOREIGN KEY (TransaccionId) 
        REFERENCES dbo.Transaccion(TransaccionId) ON DELETE CASCADE,
    CONSTRAINT UQ_TransaccionDeposito_Transaccion UNIQUE (TransaccionId),
    CONSTRAINT CK_TransaccionDeposito_Bonificacion CHECK (Bonificacion >= 0)
);
GO

CREATE TABLE dbo.LogTransacciones (
    LogId INT IDENTITY(1,1) NOT NULL,
    TransaccionId INT NULL,
    TipoOperacion NVARCHAR(50) NOT NULL,
    Mensaje NVARCHAR(1000) NOT NULL,
    Exitoso BIT NOT NULL,
    FechaLog DATETIME NOT NULL DEFAULT GETDATE(),
    DetallesAdicionales NVARCHAR(MAX) NULL,
    
    CONSTRAINT PK_LogTransacciones PRIMARY KEY CLUSTERED (LogId),
    CONSTRAINT FK_LogTransacciones_Transaccion FOREIGN KEY (TransaccionId) 
        REFERENCES dbo.Transaccion(TransaccionId) ON DELETE SET NULL
);
GO

CREATE NONCLUSTERED INDEX IX_CuentaUsuario_UsuarioId 
    ON dbo.CuentaUsuario(UsuarioId);
GO

CREATE NONCLUSTERED INDEX IX_CuentaUsuario_EstadoCuentaId 
    ON dbo.CuentaUsuario(EstadoCuentaId);
GO

CREATE NONCLUSTERED INDEX IX_Transaccion_CuentaId 
    ON dbo.Transaccion(CuentaId);
GO

CREATE NONCLUSTERED INDEX IX_Transaccion_TipoTransaccionId 
    ON dbo.Transaccion(TipoTransaccionId);
GO

CREATE NONCLUSTERED INDEX IX_Transaccion_FechaTransaccion 
    ON dbo.Transaccion(FechaTransaccion DESC);
GO

CREATE NONCLUSTERED INDEX IX_LogTransacciones_TransaccionId 
    ON dbo.LogTransacciones(TransaccionId);
GO

CREATE NONCLUSTERED INDEX IX_LogTransacciones_FechaLog 
    ON dbo.LogTransacciones(FechaLog DESC);
GO

INSERT INTO dbo.EstadoCuenta (CodigoEstado, Descripcion) VALUES
    ('Creada', 'Cuenta recién creada, pendiente de activación'),
    ('ActivaSinFondos', 'Cuenta activa pero sin fondos disponibles'),
    ('ActivaConFondos', 'Cuenta activa con fondos disponibles'),
    ('Bloqueada', 'Cuenta bloqueada por razones administrativas');
GO

INSERT INTO dbo.TipoTransaccion (CodigoTipo, Descripcion, AfectaSaldo) VALUES
    ('Debito', 'Operación de débito (reduce saldo)', 1),
    ('Credito', 'Operación de crédito (aumenta saldo)', 1),
    ('Cancelacion', 'Cancelación de transacción previa', 1);
GO
