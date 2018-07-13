-- ****************************
-- CUENTAS
-- ****************************
-- select * from DebitoHipotecario where tipo is null
insert into Cuentas(Id, Descripcion, SaldoInicial, Estado) values (1, 'Cris', 0, 1)
insert into Cuentas(Id, Descripcion, SaldoInicial, Estado) values (2, 'Efectivo', 13484.4, 1)
insert into Cuentas(Id, Descripcion, SaldoInicial, Estado) values (3, 'D�bito BSE', 27305.51, 1)
insert into Cuentas(Id, Descripcion, SaldoInicial, Estado) values (4, 'D�bito Hipotecario', 5019.26, 1)

-- ****************************
-- UNIFICO MOVIMIENTOS
-- ****************************
drop table #mov 
go
select 1 as IdCuenta, * into #mov from Cris

insert into #mov
select 2 as IdCuenta, Fecha, Tipo, Transacci�n, Importe, Saldo from Efectivo

insert into #mov
select 3 as IdCuenta, Fecha, Tipo, Transacci�n, Importe, Saldo from DebitoBSE

insert into #mov
select 4 as IdCuenta, Fecha, Tipo, Transacci�n, Importe, Saldo from DebitoHipotecario

-- Evito duplicados
update #mov set	Transacci�n = 'Retiro de efectivo por l�nea de cajas'
from #mov m
where
	Transacci�n = 'Extracci�n por l�nea de cajas'	and
	importe	>= 0
-- 5

update #mov set importe = 0 where fecha = '2017/09/11' and Transacci�n = 'Materiales de construcci�n' and tipo = 'Construcci�n' and importe is null
-- 1

-- ****************************
-- RUBROS
-- ****************************
insert into Rubros(Descripcion)
select distinct tipo from #mov where tipo is not null
-- 39
go

-- ****************************
-- TRANSACCIONES
-- ****************************
drop table #transacciones
go
create table #transacciones(
	Descripcion	varchar(50)	not null,
	IdRubro		int	not null,
	EsDebito	bit	null,
	Estado		tinyint	null
)
go

create unique index #transacciones_index1 ON #transacciones(descripcion, idrubro)
go

update #mov 
set
	transacci�n = 'Entrega de donaci�n'
from #mov m
inner join Rubros r on r.Descripcion = m.Tipo
where m.transacci�n = 'Donaci�n' and r.id = 24 and importe < 0

update #mov 
set
	transacci�n = 'Pago de honorarios profesionales'
from #mov m
inner join Rubros r on r.Descripcion = m.Tipo
where m.transacci�n = 'Honorarios profesionales' and r.id = 23 and importe < 0

update #mov 
set
	transacci�n = 'D�bitos varios'
from #mov m
inner join Rubros r on r.Descripcion = m.Tipo
where m.transacci�n = 'Varios' and r.id = 24
	and importe < 0

insert into #transacciones(Descripcion, IdRubro, EsDebito, Estado)
select distinct m.Transacci�n, r.Id as IdRubro, EsDebito = case when m.Importe <= 0 then 1 else 0 end, Estado = 1
from #mov m
inner join Rubros r on r.Descripcion = m.Tipo

insert into Transacciones(Descripcion, IdRubro, EsDebito, Estado)
select Descripcion, IdRubro, EsDebito, Estado from #transacciones
-- 216
go

-- ****************************
-- MOVIMIENTOS
-- ****************************
-- insert into Movimientos(FechaMovimiento, IdTransaccion, IdCuenta, Importe)
drop table #mov2
go
select m.Fecha AS FechaMovimiento, t.Id AS IdTransaccion, m.IdCuenta, m.Importe, m.Tipo, m.Transacci�n
into #mov2
from #mov m
inner join Rubros r on r.Descripcion = m.Tipo
inner join Transacciones t on t.Descripcion = m.Transacci�n and t.IdRubro = r.Id
-- 1679

/*******
select * from #mov m where tipo is not null and not exists (
	select 1 from #mov2 m2 where m2.FechaMovimiento = m.Fecha and m2.Tipo = m.Tipo and m2.Transacci�n = m.Transacci�n
)
********/

insert into Movimientos(FechaMovimiento, IdTransaccion, IdCuenta, Importe)
select FechaMovimiento, IdTransaccion, IdCuenta, Importe
from #mov2
-- 1679

																	       
insert into usuarios(Nombre, Contrase�a, FechaAlta, Estado, FechaBaja, NombreCompleto)
values('administrador', 'BA-32-53-87-6A-ED-6B-C2-2D-4A-6F-F5-3D-84-06-C6-AD-86-41-95-ED-14-4A-B5-C8-76-21-B6-C2-33-B5-48-BA-EA-E6-95-6D-F3-46-EC-8C-17-F5-EA-10-F3-5E-E3-CB-C5-14-79-7E-D7-DD-D3-14-54-64-E2-A0-BA-B4-13',
	GETDATE(), 1, null, 'Administrador')

update movimientos set idusuario = 1
