USE SportsbookDB;
GO

PRINT 'Eliminando todas las tablas y datos...';
GO

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

PRINT 'Tablas eliminadas. Ejecuta Script_Creacion_BaseDatos.sql para recrear la estructura.';
PRINT 'Luego ejecuta Script_Datos_Prueba.sql para insertar datos de prueba.';
GO

