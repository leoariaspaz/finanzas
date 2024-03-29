USE [Gastos]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[Transacciones]') AND type in (N'U'))
ALTER TABLE [Transacciones] DROP CONSTRAINT IF EXISTS [FK_Transacciones_Rubros]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[Movimientos]') AND type in (N'U'))
ALTER TABLE [Movimientos] DROP CONSTRAINT IF EXISTS [FK_Movimientos_Usuarios]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[Movimientos]') AND type in (N'U'))
ALTER TABLE [Movimientos] DROP CONSTRAINT IF EXISTS [FK_Movimientos_Transacciones]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[Movimientos]') AND type in (N'U'))
ALTER TABLE [Movimientos] DROP CONSTRAINT IF EXISTS [FK_Movimientos_Cuentas]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[Asociaciones]') AND type in (N'U'))
ALTER TABLE [Asociaciones] DROP CONSTRAINT IF EXISTS [FK_Asociados_UsuariosOriginantes]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[Asociaciones]') AND type in (N'U'))
ALTER TABLE [Asociaciones] DROP CONSTRAINT IF EXISTS [FK_Asociados_UsuariosAsociados]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[Usuarios]') AND type in (N'U'))
ALTER TABLE [Usuarios] DROP CONSTRAINT IF EXISTS [DF_Usuarios_FechaAlta]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[Movimientos]') AND type in (N'U'))
ALTER TABLE [Movimientos] DROP CONSTRAINT IF EXISTS [DF_Movimientos_FechaGrabacion]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[Movimientos]') AND type in (N'U'))
ALTER TABLE [Movimientos] DROP CONSTRAINT IF EXISTS [DF_Movimientos_EsContrasiento]
GO
DROP INDEX IF EXISTS [IX_Usuarios_NombreCompleto] ON [Usuarios]
GO
DROP INDEX IF EXISTS [IX_Usuarios_Nombre] ON [Usuarios]
GO
DROP INDEX IF EXISTS [IX_Usuarios_Id] ON [Usuarios]
GO
DROP INDEX IF EXISTS [IX_Transacciones] ON [Transacciones]
GO
DROP INDEX IF EXISTS [IX_Rubros] ON [Rubros]
GO
DROP INDEX IF EXISTS [IX_Movimientos] ON [Movimientos]
GO
DROP INDEX IF EXISTS [IX_FK_Movimientos_Usuarios] ON [Movimientos]
GO
DROP INDEX IF EXISTS [IX_Cuentas] ON [Cuentas]
GO
DROP TABLE IF EXISTS [Usuarios]
GO
DROP TABLE IF EXISTS [Transacciones]
GO
DROP TABLE IF EXISTS [Rubros]
GO
DROP TABLE IF EXISTS [Movimientos]
GO
DROP TABLE IF EXISTS [Cuentas]
GO
DROP TABLE IF EXISTS [Asociaciones]
GO
DROP FUNCTION IF EXISTS [GetMigrationTime]
GO
USE [master]
GO
DROP DATABASE IF EXISTS [Gastos]
GO
CREATE DATABASE [Gastos]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'Gastos', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\Gastos.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'Gastos_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\Gastos_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [Gastos] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [Gastos].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [Gastos] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [Gastos] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [Gastos] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [Gastos] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [Gastos] SET ARITHABORT OFF 
GO
ALTER DATABASE [Gastos] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [Gastos] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [Gastos] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [Gastos] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [Gastos] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [Gastos] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [Gastos] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [Gastos] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [Gastos] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [Gastos] SET  DISABLE_BROKER 
GO
ALTER DATABASE [Gastos] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [Gastos] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [Gastos] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [Gastos] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [Gastos] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [Gastos] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [Gastos] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [Gastos] SET RECOVERY FULL 
GO
ALTER DATABASE [Gastos] SET  MULTI_USER 
GO
ALTER DATABASE [Gastos] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [Gastos] SET DB_CHAINING OFF 
GO
ALTER DATABASE [Gastos] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [Gastos] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [Gastos] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [Gastos] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'Gastos', N'ON'
GO
ALTER DATABASE [Gastos] SET QUERY_STORE = OFF
GO
USE [Gastos]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create function [GetMigrationTime]()
RETURNS varchar(17)
AS
BEGIN
	return convert(varchar, getdate(), 112) + REPLACE(convert(varchar, getdate(), 114), ':', '')
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Asociaciones](
	[IdAsociacion] [int] NOT NULL,
	[IdOriginante] [int] NOT NULL,
	[IdAsociado] [int] NOT NULL,
	[Desde] [smalldatetime] NOT NULL,
	[Hasta] [smalldatetime] NOT NULL,
 CONSTRAINT [PK_Asociados] PRIMARY KEY CLUSTERED 
(
	[IdAsociacion] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Cuentas](
	[Id] [int] NOT NULL,
	[Descripcion] [varchar](50) NOT NULL,
	[SaldoInicial] [numeric](18, 2) NOT NULL,
	[Estado] [tinyint] NOT NULL,
 CONSTRAINT [PK_Cuentas] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Movimientos](
	[Id] [numeric](18, 0) IDENTITY(1,1) NOT NULL,
	[FechaMovimiento] [smalldatetime] NOT NULL,
	[IdTransaccion] [int] NOT NULL,
	[IdCuenta] [int] NOT NULL,
	[Detalle] [varchar](500) NULL,
	[Importe] [numeric](18, 2) NOT NULL,
	[EsContrasiento] [bit] NOT NULL,
	[FechaGrabacion] [datetime] NOT NULL,
	[IdUsuario] [int] NULL,
 CONSTRAINT [PK_Movimientos] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Rubros](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Descripcion] [varchar](50) NOT NULL,
 CONSTRAINT [PK_Rubros] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Transacciones](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[IdRubro] [int] NOT NULL,
	[Descripcion] [varchar](255) NOT NULL,
	[EsDebito] [bit] NOT NULL,
	[Estado] [tinyint] NOT NULL,
 CONSTRAINT [PK_Transacciones] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Usuarios](
	[Id] [int] NOT NULL,
	[Nombre] [varchar](50) NOT NULL,
	[Contraseña] [varchar](255) NOT NULL,
	[FechaAlta] [datetime] NOT NULL,
	[Estado] [tinyint] NOT NULL,
	[FechaBaja] [datetime] NULL,
	[NombreCompleto] [varchar](50) NOT NULL,
	[CambiarContraseña] [bit] NULL,
	[PathAvatar] [varchar](255) NULL,
 CONSTRAINT [PK_Usuarios] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_Cuentas] ON [Cuentas]
(
	[Descripcion] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IX_FK_Movimientos_Usuarios] ON [Movimientos]
(
	[IdUsuario] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IX_Movimientos] ON [Movimientos]
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_Rubros] ON [Rubros]
(
	[Descripcion] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_Transacciones] ON [Transacciones]
(
	[Descripcion] ASC,
	[IdRubro] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IX_Usuarios_Id] ON [Usuarios]
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_Usuarios_Nombre] ON [Usuarios]
(
	[Nombre] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_Usuarios_NombreCompleto] ON [Usuarios]
(
	[NombreCompleto] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [Movimientos] ADD  CONSTRAINT [DF_Movimientos_EsContrasiento]  DEFAULT ((0)) FOR [EsContrasiento]
GO
ALTER TABLE [Movimientos] ADD  CONSTRAINT [DF_Movimientos_FechaGrabacion]  DEFAULT (getdate()) FOR [FechaGrabacion]
GO
ALTER TABLE [Usuarios] ADD  CONSTRAINT [DF_Usuarios_FechaAlta]  DEFAULT (getdate()) FOR [FechaAlta]
GO
ALTER TABLE [Asociaciones]  WITH CHECK ADD  CONSTRAINT [FK_Asociados_UsuariosAsociados] FOREIGN KEY([IdAsociado])
REFERENCES [Usuarios] ([Id])
GO
ALTER TABLE [Asociaciones] CHECK CONSTRAINT [FK_Asociados_UsuariosAsociados]
GO
ALTER TABLE [Asociaciones]  WITH CHECK ADD  CONSTRAINT [FK_Asociados_UsuariosOriginantes] FOREIGN KEY([IdOriginante])
REFERENCES [Usuarios] ([Id])
GO
ALTER TABLE [Asociaciones] CHECK CONSTRAINT [FK_Asociados_UsuariosOriginantes]
GO
ALTER TABLE [Movimientos]  WITH CHECK ADD  CONSTRAINT [FK_Movimientos_Cuentas] FOREIGN KEY([IdCuenta])
REFERENCES [Cuentas] ([Id])
GO
ALTER TABLE [Movimientos] CHECK CONSTRAINT [FK_Movimientos_Cuentas]
GO
ALTER TABLE [Movimientos]  WITH CHECK ADD  CONSTRAINT [FK_Movimientos_Transacciones] FOREIGN KEY([IdTransaccion])
REFERENCES [Transacciones] ([Id])
GO
ALTER TABLE [Movimientos] CHECK CONSTRAINT [FK_Movimientos_Transacciones]
GO
ALTER TABLE [Movimientos]  WITH CHECK ADD  CONSTRAINT [FK_Movimientos_Usuarios] FOREIGN KEY([IdUsuario])
REFERENCES [Usuarios] ([Id])
GO
ALTER TABLE [Movimientos] CHECK CONSTRAINT [FK_Movimientos_Usuarios]
GO
ALTER TABLE [Transacciones]  WITH CHECK ADD  CONSTRAINT [FK_Transacciones_Rubros] FOREIGN KEY([IdRubro])
REFERENCES [Rubros] ([Id])
ON UPDATE CASCADE
GO
ALTER TABLE [Transacciones] CHECK CONSTRAINT [FK_Transacciones_Rubros]
GO
USE [master]
GO
ALTER DATABASE [Gastos] SET  READ_WRITE 
GO
