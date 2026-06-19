USE [BansiApp]
GO
CREATE OR ALTER PROCEDURE [dbo].[spAgregar]
	@id int,
	@nombre varchar(255),
	@descripcion varchar(255)
AS
BEGIN
	IF NOT EXISTS (SELECT IdExamen FROM Examen WHERE IdExamen=@id)
	BEGIN
		BEGIN TRANSACTION 
			BEGIN TRY
			INSERT INTO Examen (IdExamen, Nombre, Descripcion) values (@id, @nombre, @descripcion);
			SELECT 0 AS 'CodigoRetorno', 'Registro insertado satisfactoriamente' AS 'DescripcionRetorno';
			COMMIT TRANSACTION 
			END TRY
		BEGIN CATCH
			SELECT ERROR_NUMBER() AS 'CodigoRetorno', ERROR_MESSAGE() AS 'DescripcionRetorno';
			ROLLBACK TRANSACTION
		END CATCH
	END
	ELSE
	BEGIN
		SELECT 1 AS 'CodigoRetorno', 'El ID: '+RTRIM(@id)+' ya existe.' AS 'DescripcionRetorno';
	END
END