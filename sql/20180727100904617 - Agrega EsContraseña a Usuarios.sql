alter table usuarios add CambiarContraseña bit null
go

update usuarios set CambiarContraseña = 0
go
