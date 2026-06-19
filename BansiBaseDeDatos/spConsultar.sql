USE [BansiApp]
GO
CREATE OR ALTER PROCEDURE [dbo].[spConsultar]
    @id int,
	@nombre varchar(255),
	@descripcion varchar(255)
AS
BEGIN
	SELECT IdExamen AS 'ID', Nombre, Descripcion FROM Examen 
	WHERE @id is null OR @id=IdExamen AND ((NULLIF(@nombre,'') IS NULL OR Nombre LIKE '%'+@nombre+'%')
	AND (NULLIF(@descripcion,'') IS NULL OR Descripcion LIKE '%'+@descripcion+'%'))
END