IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Transacciones_Rubros]') AND parent_object_id = OBJECT_ID(N'[dbo].[Transacciones]'))
ALTER TABLE [dbo].[Transacciones] DROP CONSTRAINT [FK_Transacciones_Rubros]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Movimientos_Usuarios]') AND parent_object_id = OBJECT_ID(N'[dbo].[Movimientos]'))
ALTER TABLE [dbo].[Movimientos] DROP CONSTRAINT [FK_Movimientos_Usuarios]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Movimientos_Transacciones]') AND parent_object_id = OBJECT_ID(N'[dbo].[Movimientos]'))
ALTER TABLE [dbo].[Movimientos] DROP CONSTRAINT [FK_Movimientos_Transacciones]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Movimientos_Cuentas]') AND parent_object_id = OBJECT_ID(N'[dbo].[Movimientos]'))
ALTER TABLE [dbo].[Movimientos] DROP CONSTRAINT [FK_Movimientos_Cuentas]
GO
IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Usuarios]') AND name = N'IX_Usuarios_NombreCompleto')
DROP INDEX [IX_Usuarios_NombreCompleto] ON [dbo].[Usuarios]
GO
IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Usuarios]') AND name = N'IX_Usuarios_Nombre')
DROP INDEX [IX_Usuarios_Nombre] ON [dbo].[Usuarios]
GO
IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Usuarios]') AND name = N'IX_Usuarios_Id')
DROP INDEX [IX_Usuarios_Id] ON [dbo].[Usuarios]
GO
IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Transacciones]') AND name = N'IX_Transacciones')
DROP INDEX [IX_Transacciones] ON [dbo].[Transacciones]
GO
IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Rubros]') AND name = N'IX_Rubros')
DROP INDEX [IX_Rubros] ON [dbo].[Rubros]
GO
IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Movimientos]') AND name = N'IX_Movimientos')
DROP INDEX [IX_Movimientos] ON [dbo].[Movimientos]
GO
IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Movimientos]') AND name = N'IX_FK_Movimientos_Usuarios')
DROP INDEX [IX_FK_Movimientos_Usuarios] ON [dbo].[Movimientos]
GO
IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Cuentas]') AND name = N'IX_Cuentas')
DROP INDEX [IX_Cuentas] ON [dbo].[Cuentas]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Usuarios]') AND type in (N'U'))
DROP TABLE [dbo].[Usuarios]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Transacciones]') AND type in (N'U'))
DROP TABLE [dbo].[Transacciones]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Rubros]') AND type in (N'U'))
DROP TABLE [dbo].[Rubros]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Movimientos]') AND type in (N'U'))
DROP TABLE [dbo].[Movimientos]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Cuentas]') AND type in (N'U'))
DROP TABLE [dbo].[Cuentas]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetMigrationTime]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[GetMigrationTime]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetMigrationTime]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
BEGIN
execute dbo.sp_executesql @statement = N'create function [dbo].[GetMigrationTime]()
RETURNS varchar(17)
AS
BEGIN
	return convert(varchar, getdate(), 112) + REPLACE(convert(varchar, getdate(), 114), '':'', '''')
END
' 
END

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Cuentas]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Cuentas](
	[Id] [int] NOT NULL,
	[Descripcion] [varchar](50) NOT NULL,
	[SaldoInicial] [numeric](18, 2) NOT NULL,
	[Estado] [tinyint] NOT NULL,
 CONSTRAINT [PK_Cuentas] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Movimientos]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Movimientos](
	[Id] [numeric](18, 0) IDENTITY(1,1) NOT NULL,
	[FechaMovimiento] [smalldatetime] NOT NULL,
	[IdTransaccion] [int] NOT NULL,
	[IdCuenta] [int] NOT NULL,
	[Importe] [numeric](18, 2) NOT NULL,
	[EsContrasiento] [bit] NOT NULL CONSTRAINT [DF_Movimientos_EsContrasiento]  DEFAULT ((0)),
	[FechaGrabacion] [datetime] NOT NULL CONSTRAINT [DF_Movimientos_FechaGrabacion]  DEFAULT (getdate()),
	[IdUsuario] [int] NULL,
 CONSTRAINT [PK_Movimientos] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Rubros]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Rubros](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Descripcion] [varchar](50) NOT NULL,
 CONSTRAINT [PK_Rubros] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Transacciones]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Transacciones](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[IdRubro] [int] NOT NULL,
	[Descripcion] [varchar](255) NOT NULL,
	[EsDebito] [bit] NOT NULL,
	[Estado] [tinyint] NOT NULL,
 CONSTRAINT [PK_Transacciones] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Usuarios]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Usuarios](
	[Id] [int] NOT NULL,
	[Nombre] [varchar](50) NOT NULL,
	[Contraseña] [varchar](255) NOT NULL,
	[FechaAlta] [datetime] NOT NULL CONSTRAINT [DF_Usuarios_FechaAlta]  DEFAULT (getdate()),
	[Estado] [tinyint] NOT NULL,
	[FechaBaja] [datetime] NULL,
	[NombreCompleto] [varchar](50) NOT NULL,
	[CambiarContraseña] [bit] NULL,
 CONSTRAINT [PK_Usuarios] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
SET ANSI_PADDING ON

GO
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Cuentas]') AND name = N'IX_Cuentas')
CREATE UNIQUE NONCLUSTERED INDEX [IX_Cuentas] ON [dbo].[Cuentas]
(
	[Descripcion] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Movimientos]') AND name = N'IX_FK_Movimientos_Usuarios')
CREATE NONCLUSTERED INDEX [IX_FK_Movimientos_Usuarios] ON [dbo].[Movimientos]
(
	[IdUsuario] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Movimientos]') AND name = N'IX_Movimientos')
CREATE NONCLUSTERED INDEX [IX_Movimientos] ON [dbo].[Movimientos]
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Rubros]') AND name = N'IX_Rubros')
CREATE UNIQUE NONCLUSTERED INDEX [IX_Rubros] ON [dbo].[Rubros]
(
	[Descripcion] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Transacciones]') AND name = N'IX_Transacciones')
CREATE UNIQUE NONCLUSTERED INDEX [IX_Transacciones] ON [dbo].[Transacciones]
(
	[Descripcion] ASC,
	[IdRubro] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Usuarios]') AND name = N'IX_Usuarios_Id')
CREATE NONCLUSTERED INDEX [IX_Usuarios_Id] ON [dbo].[Usuarios]
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Usuarios]') AND name = N'IX_Usuarios_Nombre')
CREATE UNIQUE NONCLUSTERED INDEX [IX_Usuarios_Nombre] ON [dbo].[Usuarios]
(
	[Nombre] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Usuarios]') AND name = N'IX_Usuarios_NombreCompleto')
CREATE UNIQUE NONCLUSTERED INDEX [IX_Usuarios_NombreCompleto] ON [dbo].[Usuarios]
(
	[NombreCompleto] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Movimientos_Cuentas]') AND parent_object_id = OBJECT_ID(N'[dbo].[Movimientos]'))
ALTER TABLE [dbo].[Movimientos]  WITH CHECK ADD  CONSTRAINT [FK_Movimientos_Cuentas] FOREIGN KEY([IdCuenta])
REFERENCES [dbo].[Cuentas] ([Id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Movimientos_Cuentas]') AND parent_object_id = OBJECT_ID(N'[dbo].[Movimientos]'))
ALTER TABLE [dbo].[Movimientos] CHECK CONSTRAINT [FK_Movimientos_Cuentas]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Movimientos_Transacciones]') AND parent_object_id = OBJECT_ID(N'[dbo].[Movimientos]'))
ALTER TABLE [dbo].[Movimientos]  WITH CHECK ADD  CONSTRAINT [FK_Movimientos_Transacciones] FOREIGN KEY([IdTransaccion])
REFERENCES [dbo].[Transacciones] ([Id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Movimientos_Transacciones]') AND parent_object_id = OBJECT_ID(N'[dbo].[Movimientos]'))
ALTER TABLE [dbo].[Movimientos] CHECK CONSTRAINT [FK_Movimientos_Transacciones]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Movimientos_Usuarios]') AND parent_object_id = OBJECT_ID(N'[dbo].[Movimientos]'))
ALTER TABLE [dbo].[Movimientos]  WITH CHECK ADD  CONSTRAINT [FK_Movimientos_Usuarios] FOREIGN KEY([IdUsuario])
REFERENCES [dbo].[Usuarios] ([Id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Movimientos_Usuarios]') AND parent_object_id = OBJECT_ID(N'[dbo].[Movimientos]'))
ALTER TABLE [dbo].[Movimientos] CHECK CONSTRAINT [FK_Movimientos_Usuarios]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Transacciones_Rubros]') AND parent_object_id = OBJECT_ID(N'[dbo].[Transacciones]'))
ALTER TABLE [dbo].[Transacciones]  WITH CHECK ADD  CONSTRAINT [FK_Transacciones_Rubros] FOREIGN KEY([IdRubro])
REFERENCES [dbo].[Rubros] ([Id])
ON UPDATE CASCADE
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Transacciones_Rubros]') AND parent_object_id = OBJECT_ID(N'[dbo].[Transacciones]'))
ALTER TABLE [dbo].[Transacciones] CHECK CONSTRAINT [FK_Transacciones_Rubros]
GO
