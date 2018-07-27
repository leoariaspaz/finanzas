create function GetMigrationTime()
RETURNS varchar(17)
AS
BEGIN
	return convert(varchar, getdate(), 112) + REPLACE(convert(varchar, getdate(), 114), ':', '')
END
go