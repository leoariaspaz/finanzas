begin tran lap

delete from movimientos
delete from transacciones
delete from rubros

set identity_insert rubros on

insert into Rubros(Id, Descripcion)
select Id, Descripcion from Finanzas.dbo.Rubros

set identity_insert rubros off

set identity_insert transacciones on

insert into Transacciones(Id, Descripcion, EsDebito, Estado, IdRubro)
select Id, Descripcion, EsDebito, Estado, IdRubro from Finanzas.dbo.Transacciones

set identity_insert transacciones off

set identity_insert Movimientos on

insert into Movimientos(EsContrasiento, FechaGrabacion, FechaMovimiento, Id, IdCuenta, IdTransaccion, IdUsuario, Importe)
select EsContrasiento, FechaGrabacion, FechaMovimiento, Id, IdCuenta, IdTransaccion, IdUsuario, Importe from Finanzas.dbo.Movimientos

set identity_insert Movimientos off

-- commit tran lap
-- rollback tran lap
