USE SportsbookDB;
GO

DELETE FROM dbo.LogTransacciones;
DELETE FROM dbo.TransaccionDeposito;
DELETE FROM dbo.TransaccionRetiro;
DELETE FROM dbo.TransaccionApuesta;
DELETE FROM dbo.Transaccion;
DELETE FROM dbo.CuentaUsuario;
DELETE FROM dbo.Usuario;
GO

DBCC CHECKIDENT ('Usuario', RESEED, 0);
DBCC CHECKIDENT ('CuentaUsuario', RESEED, 0);
DBCC CHECKIDENT ('Transaccion', RESEED, 0);
DBCC CHECKIDENT ('TransaccionApuesta', RESEED, 0);
DBCC CHECKIDENT ('TransaccionRetiro', RESEED, 0);
DBCC CHECKIDENT ('TransaccionDeposito', RESEED, 0);
DBCC CHECKIDENT ('LogTransacciones', RESEED, 0);
GO

INSERT INTO dbo.Usuario (Nombre, Apellido, Email, Telefono, FechaRegistro, Activo) VALUES
    ('Juan', 'Pérez', 'juan.perez@email.com', '1234567890', DATEADD(DAY, -30, GETDATE()), 1),
    ('María', 'González', 'maria.gonzalez@email.com', '0987654321', DATEADD(DAY, -25, GETDATE()), 1),
    ('Carlos', 'Rodríguez', 'carlos.rodriguez@email.com', '1122334455', DATEADD(DAY, -20, GETDATE()), 1),
    ('Ana', 'Martínez', 'ana.martinez@email.com', '5566778899', DATEADD(DAY, -15, GETDATE()), 1),
    ('Luis', 'Fernández', 'luis.fernandez@email.com', '9988776655', DATEADD(DAY, -10, GETDATE()), 1);
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

INSERT INTO dbo.Transaccion (CuentaId, TipoTransaccionId, Monto, FechaTransaccion, Descripcion, Exitoso) VALUES
    (@Cuenta1, @TipoCredito, 10000.00, DATEADD(DAY, -20, GETDATE()), 'Depósito inicial', 1),
    (@Cuenta1, @TipoDebito, 2000.00, DATEADD(DAY, -18, GETDATE()), 'Apuesta - Fútbol', 1),
    (@Cuenta1, @TipoCredito, 3000.00, DATEADD(DAY, -15, GETDATE()), 'Ganancia apuesta', 1),
    (@Cuenta1, @TipoDebito, 3000.00, DATEADD(DAY, -12, GETDATE()), 'Apuesta - Basquet', 1),
    (@Cuenta2, @TipoCredito, 5000.00, DATEADD(DAY, -10, GETDATE()), 'Depósito inicial', 1),
    (@Cuenta2, @TipoDebito, 5000.00, DATEADD(DAY, -8, GETDATE()), 'Retiro completo', 1),
    (@Cuenta3, @TipoCredito, 20000.00, DATEADD(DAY, -18, GETDATE()), 'Depósito inicial', 1),
    (@Cuenta3, @TipoDebito, 1000.00, DATEADD(DAY, -16, GETDATE()), 'Apuesta - Tenis', 1),
    (@Cuenta3, @TipoCredito, 2000.00, DATEADD(DAY, -14, GETDATE()), 'Ganancia apuesta', 1),
    (@Cuenta3, @TipoDebito, 5000.00, DATEADD(DAY, -12, GETDATE()), 'Retiro parcial', 1),
    (@Cuenta3, @TipoDebito, 1000.00, DATEADD(DAY, -10, GETDATE()), 'Apuesta - Fútbol', 1),
    (@Cuenta4, @TipoCredito, 3000.00, DATEADD(DAY, -12, GETDATE()), 'Depósito inicial', 1),
    (@Cuenta4, @TipoDebito, 500.00, DATEADD(DAY, -10, GETDATE()), 'Apuesta - Fútbol', 1),
    (@Cuenta4, @TipoCredito, 750.00, DATEADD(DAY, -8, GETDATE()), 'Ganancia apuesta', 1),
    (@Cuenta5, @TipoCredito, 2000.00, DATEADD(DAY, -5, GETDATE()), 'Depósito inicial', 1),
    (@Cuenta5, @TipoDebito, 2000.00, DATEADD(DAY, -3, GETDATE()), 'Retiro completo', 1);

DECLARE @Transaccion1 INT = (SELECT TOP 1 TransaccionId FROM dbo.Transaccion WHERE CuentaId = @Cuenta1 AND Descripcion = 'Depósito inicial' ORDER BY TransaccionId);
DECLARE @Transaccion2 INT = (SELECT TOP 1 TransaccionId FROM dbo.Transaccion WHERE CuentaId = @Cuenta1 AND Descripcion = 'Apuesta - Fútbol' ORDER BY TransaccionId);
DECLARE @Transaccion3 INT = (SELECT TOP 1 TransaccionId FROM dbo.Transaccion WHERE CuentaId = @Cuenta1 AND Descripcion = 'Ganancia apuesta' ORDER BY TransaccionId);
DECLARE @Transaccion4 INT = (SELECT TOP 1 TransaccionId FROM dbo.Transaccion WHERE CuentaId = @Cuenta1 AND Descripcion = 'Apuesta - Basquet' ORDER BY TransaccionId);
DECLARE @Transaccion5 INT = (SELECT TOP 1 TransaccionId FROM dbo.Transaccion WHERE CuentaId = @Cuenta2 AND Descripcion = 'Depósito inicial' ORDER BY TransaccionId);
DECLARE @Transaccion6 INT = (SELECT TOP 1 TransaccionId FROM dbo.Transaccion WHERE CuentaId = @Cuenta2 AND Descripcion = 'Retiro completo' ORDER BY TransaccionId);
DECLARE @Transaccion7 INT = (SELECT TOP 1 TransaccionId FROM dbo.Transaccion WHERE CuentaId = @Cuenta3 AND Descripcion = 'Depósito inicial' ORDER BY TransaccionId);
DECLARE @Transaccion8 INT = (SELECT TOP 1 TransaccionId FROM dbo.Transaccion WHERE CuentaId = @Cuenta3 AND Descripcion = 'Apuesta - Tenis' ORDER BY TransaccionId);
DECLARE @Transaccion9 INT = (SELECT TOP 1 TransaccionId FROM dbo.Transaccion WHERE CuentaId = @Cuenta3 AND Descripcion = 'Ganancia apuesta' ORDER BY TransaccionId);
DECLARE @Transaccion10 INT = (SELECT TOP 1 TransaccionId FROM dbo.Transaccion WHERE CuentaId = @Cuenta3 AND Descripcion = 'Retiro parcial' ORDER BY TransaccionId);
DECLARE @Transaccion11 INT = (SELECT TOP 1 TransaccionId FROM dbo.Transaccion WHERE CuentaId = @Cuenta3 AND Descripcion = 'Apuesta - Fútbol' ORDER BY TransaccionId);
DECLARE @Transaccion12 INT = (SELECT TOP 1 TransaccionId FROM dbo.Transaccion WHERE CuentaId = @Cuenta4 AND Descripcion = 'Depósito inicial' ORDER BY TransaccionId);
DECLARE @Transaccion13 INT = (SELECT TOP 1 TransaccionId FROM dbo.Transaccion WHERE CuentaId = @Cuenta4 AND Descripcion = 'Apuesta - Fútbol' ORDER BY TransaccionId);
DECLARE @Transaccion14 INT = (SELECT TOP 1 TransaccionId FROM dbo.Transaccion WHERE CuentaId = @Cuenta4 AND Descripcion = 'Ganancia apuesta' ORDER BY TransaccionId);
DECLARE @Transaccion15 INT = (SELECT TOP 1 TransaccionId FROM dbo.Transaccion WHERE CuentaId = @Cuenta5 AND Descripcion = 'Depósito inicial' ORDER BY TransaccionId);
DECLARE @Transaccion16 INT = (SELECT TOP 1 TransaccionId FROM dbo.Transaccion WHERE CuentaId = @Cuenta5 AND Descripcion = 'Retiro completo' ORDER BY TransaccionId);

INSERT INTO dbo.TransaccionApuesta (TransaccionId, EventoDeportivo, EquipoApostado, Cuota, MontoApostado, Resultado, FechaEvento) VALUES
    (@Transaccion2, 'Barcelona vs Real Madrid', 'Barcelona', 2.50, 2000.00, 'Ganó', DATEADD(DAY, -17, GETDATE())),
    (@Transaccion4, 'Lakers vs Warriors', 'Lakers', 1.80, 3000.00, 'Perdió', DATEADD(DAY, -11, GETDATE())),
    (@Transaccion8, 'Federer vs Nadal', 'Federer', 3.00, 1000.00, 'Ganó', DATEADD(DAY, -15, GETDATE())),
    (@Transaccion11, 'Argentina vs Brasil', 'Argentina', 2.20, 1000.00, 'Pendiente', DATEADD(DAY, 2, GETDATE())),
    (@Transaccion13, 'Manchester vs Liverpool', 'Manchester', 1.90, 500.00, 'Ganó', DATEADD(DAY, -9, GETDATE()));

INSERT INTO dbo.TransaccionRetiro (TransaccionId, MetodoRetiro, NumeroCuentaDestino, BancoDestino, Comision, FechaProcesamiento) VALUES
    (@Transaccion6, 'Transferencia Bancaria', '1234567890', 'Banco Nacional', 50.00, DATEADD(DAY, -7, GETDATE())),
    (@Transaccion10, 'Transferencia Bancaria', '0987654321', 'Banco Central', 100.00, DATEADD(DAY, -11, GETDATE())),
    (@Transaccion16, 'Transferencia Bancaria', '1122334455', 'Banco Popular', 30.00, DATEADD(DAY, -2, GETDATE()));

INSERT INTO dbo.TransaccionDeposito (TransaccionId, MetodoDeposito, NumeroCuentaOrigen, BancoOrigen, NumeroReferencia, Bonificacion) VALUES
    (@Transaccion1, 'Transferencia Bancaria', '9876543210', 'Banco Nacional', 'REF001', 0.00),
    (@Transaccion5, 'Tarjeta de Crédito', NULL, NULL, 'REF002', 0.00),
    (@Transaccion7, 'Transferencia Bancaria', '5555555555', 'Banco Central', 'REF003', 500.00),
    (@Transaccion12, 'Transferencia Bancaria', '4444444444', 'Banco Popular', 'REF004', 0.00),
    (@Transaccion15, 'Tarjeta de Débito', NULL, NULL, 'REF005', 0.00);

INSERT INTO dbo.LogTransacciones (TransaccionId, TipoOperacion, Mensaje, Exitoso, FechaLog, DetallesAdicionales) VALUES
    (@Transaccion1, 'Deposito', 'Depósito procesado exitosamente', 1, DATEADD(DAY, -20, GETDATE()), 'Monto: 10000.00'),
    (@Transaccion2, 'Apuesta', 'Apuesta registrada exitosamente', 1, DATEADD(DAY, -18, GETDATE()), 'Evento: Barcelona vs Real Madrid'),
    (@Transaccion3, 'Ganancia', 'Ganancia de apuesta acreditada', 1, DATEADD(DAY, -15, GETDATE()), 'Monto ganado: 3000.00'),
    (@Transaccion4, 'Apuesta', 'Apuesta registrada exitosamente', 1, DATEADD(DAY, -12, GETDATE()), 'Evento: Lakers vs Warriors'),
    (@Transaccion5, 'Deposito', 'Depósito procesado exitosamente', 1, DATEADD(DAY, -10, GETDATE()), 'Monto: 5000.00'),
    (@Transaccion6, 'Retiro', 'Retiro procesado exitosamente', 1, DATEADD(DAY, -8, GETDATE()), 'Monto: 5000.00, Comisión: 50.00'),
    (@Transaccion7, 'Deposito', 'Depósito procesado exitosamente con bonificación', 1, DATEADD(DAY, -18, GETDATE()), 'Monto: 20000.00, Bonificación: 500.00'),
    (@Transaccion8, 'Apuesta', 'Apuesta registrada exitosamente', 1, DATEADD(DAY, -16, GETDATE()), 'Evento: Federer vs Nadal'),
    (@Transaccion9, 'Ganancia', 'Ganancia de apuesta acreditada', 1, DATEADD(DAY, -14, GETDATE()), 'Monto ganado: 2000.00'),
    (@Transaccion10, 'Retiro', 'Retiro procesado exitosamente', 1, DATEADD(DAY, -12, GETDATE()), 'Monto: 5000.00, Comisión: 100.00'),
    (@Transaccion11, 'Apuesta', 'Apuesta registrada exitosamente', 1, DATEADD(DAY, -10, GETDATE()), 'Evento: Argentina vs Brasil'),
    (@Transaccion12, 'Deposito', 'Depósito procesado exitosamente', 1, DATEADD(DAY, -12, GETDATE()), 'Monto: 3000.00'),
    (@Transaccion13, 'Apuesta', 'Apuesta registrada exitosamente', 1, DATEADD(DAY, -10, GETDATE()), 'Evento: Manchester vs Liverpool'),
    (@Transaccion14, 'Ganancia', 'Ganancia de apuesta acreditada', 1, DATEADD(DAY, -8, GETDATE()), 'Monto ganado: 750.00'),
    (@Transaccion15, 'Deposito', 'Depósito procesado exitosamente', 1, DATEADD(DAY, -5, GETDATE()), 'Monto: 2000.00'),
    (@Transaccion16, 'Retiro', 'Retiro procesado exitosamente', 1, DATEADD(DAY, -3, GETDATE()), 'Monto: 2000.00, Comisión: 30.00');
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
