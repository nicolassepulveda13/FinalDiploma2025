USE SportsbookDB;
GO

SET IDENTITY_INSERT dbo.Usuario ON;
GO

INSERT INTO dbo.Usuario (UsuarioId, Nombre, Apellido, Email, Telefono, FechaRegistro, Activo) VALUES
    (1, 'Juan', 'Pérez', 'juan.perez@email.com', '1234567890', DATEADD(DAY, -30, GETDATE()), 1),
    (2, 'María', 'González', 'maria.gonzalez@email.com', '0987654321', DATEADD(DAY, -25, GETDATE()), 1),
    (3, 'Carlos', 'Rodríguez', 'carlos.rodriguez@email.com', '1122334455', DATEADD(DAY, -20, GETDATE()), 1),
    (4, 'Ana', 'Martínez', 'ana.martinez@email.com', '5566778899', DATEADD(DAY, -15, GETDATE()), 1),
    (5, 'Luis', 'Fernández', 'luis.fernandez@email.com', '9988776655', DATEADD(DAY, -10, GETDATE()), 1);
GO

SET IDENTITY_INSERT dbo.Usuario OFF;
GO

INSERT INTO dbo.CuentaUsuario (UsuarioId, Saldo, EstadoCuentaId) VALUES
    (1, 5000.00, 3),
    (2, 0.00, 2),
    (3, 15000.00, 3),
    (4, 2500.00, 3),
    (5, 0.00, 4);
GO

DECLARE @Cuenta1 INT = (SELECT CuentaId FROM dbo.CuentaUsuario WHERE UsuarioId = 1);
DECLARE @Cuenta2 INT = (SELECT CuentaId FROM dbo.CuentaUsuario WHERE UsuarioId = 2);
DECLARE @Cuenta3 INT = (SELECT CuentaId FROM dbo.CuentaUsuario WHERE UsuarioId = 3);
DECLARE @Cuenta4 INT = (SELECT CuentaId FROM dbo.CuentaUsuario WHERE UsuarioId = 4);
DECLARE @Cuenta5 INT = (SELECT CuentaId FROM dbo.CuentaUsuario WHERE UsuarioId = 5);

DECLARE @TipoDebito INT = (SELECT TipoTransaccionId FROM dbo.TipoTransaccion WHERE CodigoTipo = 'Debito');
DECLARE @TipoCredito INT = (SELECT TipoTransaccionId FROM dbo.TipoTransaccion WHERE CodigoTipo = 'Credito');
DECLARE @TipoCancelacion INT = (SELECT TipoTransaccionId FROM dbo.TipoTransaccion WHERE CodigoTipo = 'Cancelacion');

SET IDENTITY_INSERT dbo.Transaccion ON;
GO

INSERT INTO dbo.Transaccion (TransaccionId, CuentaId, TipoTransaccionId, Monto, FechaTransaccion, Descripcion, Exitoso) VALUES
    (1, @Cuenta1, @TipoCredito, 10000.00, DATEADD(DAY, -20, GETDATE()), 'Depósito inicial', 1),
    (2, @Cuenta1, @TipoDebito, 2000.00, DATEADD(DAY, -18, GETDATE()), 'Apuesta - Fútbol', 1),
    (3, @Cuenta1, @TipoCredito, 3000.00, DATEADD(DAY, -15, GETDATE()), 'Ganancia apuesta', 1),
    (4, @Cuenta1, @TipoDebito, 3000.00, DATEADD(DAY, -12, GETDATE()), 'Apuesta - Basquet', 1),
    (5, @Cuenta2, @TipoCredito, 5000.00, DATEADD(DAY, -10, GETDATE()), 'Depósito inicial', 1),
    (6, @Cuenta2, @TipoDebito, 5000.00, DATEADD(DAY, -8, GETDATE()), 'Retiro completo', 1),
    (7, @Cuenta3, @TipoCredito, 20000.00, DATEADD(DAY, -18, GETDATE()), 'Depósito inicial', 1),
    (8, @Cuenta3, @TipoDebito, 1000.00, DATEADD(DAY, -16, GETDATE()), 'Apuesta - Tenis', 1),
    (9, @Cuenta3, @TipoCredito, 2000.00, DATEADD(DAY, -14, GETDATE()), 'Ganancia apuesta', 1),
    (10, @Cuenta3, @TipoDebito, 5000.00, DATEADD(DAY, -12, GETDATE()), 'Retiro parcial', 1),
    (11, @Cuenta3, @TipoDebito, 1000.00, DATEADD(DAY, -10, GETDATE()), 'Apuesta - Fútbol', 1),
    (12, @Cuenta4, @TipoCredito, 3000.00, DATEADD(DAY, -12, GETDATE()), 'Depósito inicial', 1),
    (13, @Cuenta4, @TipoDebito, 500.00, DATEADD(DAY, -10, GETDATE()), 'Apuesta - Fútbol', 1),
    (14, @Cuenta4, @TipoCredito, 750.00, DATEADD(DAY, -8, GETDATE()), 'Ganancia apuesta', 1),
    (15, @Cuenta5, @TipoCredito, 2000.00, DATEADD(DAY, -5, GETDATE()), 'Depósito inicial', 1),
    (16, @Cuenta5, @TipoDebito, 2000.00, DATEADD(DAY, -3, GETDATE()), 'Retiro completo', 1);
GO

SET IDENTITY_INSERT dbo.Transaccion OFF;
GO

INSERT INTO dbo.TransaccionApuesta (TransaccionId, EventoDeportivo, EquipoApostado, Cuota, MontoApostado, Resultado, FechaEvento) VALUES
    (2, 'Barcelona vs Real Madrid', 'Barcelona', 2.50, 2000.00, 'Ganó', DATEADD(DAY, -17, GETDATE())),
    (4, 'Lakers vs Warriors', 'Lakers', 1.80, 3000.00, 'Perdió', DATEADD(DAY, -11, GETDATE())),
    (8, 'Federer vs Nadal', 'Federer', 3.00, 1000.00, 'Ganó', DATEADD(DAY, -15, GETDATE())),
    (11, 'Argentina vs Brasil', 'Argentina', 2.20, 1000.00, 'Pendiente', DATEADD(DAY, 2, GETDATE())),
    (13, 'Manchester vs Liverpool', 'Manchester', 1.90, 500.00, 'Ganó', DATEADD(DAY, -9, GETDATE()));
GO

INSERT INTO dbo.TransaccionRetiro (TransaccionId, MetodoRetiro, NumeroCuentaDestino, BancoDestino, Comision, FechaProcesamiento) VALUES
    (6, 'Transferencia Bancaria', '1234567890', 'Banco Nacional', 50.00, DATEADD(DAY, -7, GETDATE())),
    (10, 'Transferencia Bancaria', '0987654321', 'Banco Central', 100.00, DATEADD(DAY, -11, GETDATE())),
    (16, 'Transferencia Bancaria', '1122334455', 'Banco Popular', 30.00, DATEADD(DAY, -2, GETDATE()));
GO

INSERT INTO dbo.TransaccionDeposito (TransaccionId, MetodoDeposito, NumeroCuentaOrigen, BancoOrigen, NumeroReferencia, Bonificacion) VALUES
    (1, 'Transferencia Bancaria', '9876543210', 'Banco Nacional', 'REF001', 0.00),
    (5, 'Tarjeta de Crédito', NULL, NULL, 'REF002', 0.00),
    (7, 'Transferencia Bancaria', '5555555555', 'Banco Central', 'REF003', 500.00),
    (12, 'Transferencia Bancaria', '4444444444', 'Banco Popular', 'REF004', 0.00),
    (15, 'Tarjeta de Débito', NULL, NULL, 'REF005', 0.00);
GO

INSERT INTO dbo.LogTransacciones (TransaccionId, TipoOperacion, Mensaje, Exitoso, FechaLog, DetallesAdicionales) VALUES
    (1, 'Deposito', 'Depósito procesado exitosamente', 1, DATEADD(DAY, -20, GETDATE()), 'Monto: 10000.00'),
    (2, 'Apuesta', 'Apuesta registrada exitosamente', 1, DATEADD(DAY, -18, GETDATE()), 'Evento: Barcelona vs Real Madrid'),
    (3, 'Ganancia', 'Ganancia de apuesta acreditada', 1, DATEADD(DAY, -15, GETDATE()), 'Monto ganado: 3000.00'),
    (4, 'Apuesta', 'Apuesta registrada exitosamente', 1, DATEADD(DAY, -12, GETDATE()), 'Evento: Lakers vs Warriors'),
    (5, 'Deposito', 'Depósito procesado exitosamente', 1, DATEADD(DAY, -10, GETDATE()), 'Monto: 5000.00'),
    (6, 'Retiro', 'Retiro procesado exitosamente', 1, DATEADD(DAY, -8, GETDATE()), 'Monto: 5000.00, Comisión: 50.00'),
    (7, 'Deposito', 'Depósito procesado exitosamente con bonificación', 1, DATEADD(DAY, -18, GETDATE()), 'Monto: 20000.00, Bonificación: 500.00'),
    (8, 'Apuesta', 'Apuesta registrada exitosamente', 1, DATEADD(DAY, -16, GETDATE()), 'Evento: Federer vs Nadal'),
    (9, 'Ganancia', 'Ganancia de apuesta acreditada', 1, DATEADD(DAY, -14, GETDATE()), 'Monto ganado: 2000.00'),
    (10, 'Retiro', 'Retiro procesado exitosamente', 1, DATEADD(DAY, -12, GETDATE()), 'Monto: 5000.00, Comisión: 100.00'),
    (11, 'Apuesta', 'Apuesta registrada exitosamente', 1, DATEADD(DAY, -10, GETDATE()), 'Evento: Argentina vs Brasil'),
    (12, 'Deposito', 'Depósito procesado exitosamente', 1, DATEADD(DAY, -12, GETDATE()), 'Monto: 3000.00'),
    (13, 'Apuesta', 'Apuesta registrada exitosamente', 1, DATEADD(DAY, -10, GETDATE()), 'Evento: Manchester vs Liverpool'),
    (14, 'Ganancia', 'Ganancia de apuesta acreditada', 1, DATEADD(DAY, -8, GETDATE()), 'Monto ganado: 750.00'),
    (15, 'Deposito', 'Depósito procesado exitosamente', 1, DATEADD(DAY, -5, GETDATE()), 'Monto: 2000.00'),
    (16, 'Retiro', 'Retiro procesado exitosamente', 1, DATEADD(DAY, -3, GETDATE()), 'Monto: 2000.00, Comisión: 30.00');
GO

PRINT 'Datos de prueba insertados correctamente.';
PRINT 'Usuarios: 5';
PRINT 'Cuentas: 5 (con diferentes estados y saldos)';
PRINT 'Transacciones: 16';
PRINT 'Apuestas: 5';
PRINT 'Retiros: 3';
PRINT 'Depósitos: 5';
PRINT 'Logs: 16';
GO

