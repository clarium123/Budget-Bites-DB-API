CREATE PROCEDURE [dbo].[USP_InsertUserFavouriteFood](
	-- Add the parameters for the stored procedure here
	@P_Username VARCHAR(500),
	@P_FavouriteDish NVARCHAR(MAX),
	@P_IsActive VARCHAR(10)
)
AS
BEGIN
	SET NOCOUNT ON;
	BEGIN TRY
		IF EXISTS(SELECT 'X' FROM [dbo].[Persons] WHERE Username = @P_Username)
		BEGIN
			BEGIN TRANSACTION
			IF EXISTS(SELECT 'X' FROM [dbo].[Favourites] A INNER JOIN [dbo].[Persons] B ON A.PersonID = B.PersonID WHERE B.Username = @P_Username AND A.FavouriteDish = @P_FavouriteDish)
			BEGIN
				UPDATE A SET A.[IsActive] = @P_IsActive
				FROM [dbo].[Favourites] A 
				INNER JOIN [dbo].[Persons] B ON B.PersonID = A.PersonID 
				WHERE B.Username = @P_Username AND A.FavouriteDish = @P_FavouriteDish;
			END
			ELSE
			BEGIN
				INSERT INTO [dbo].[Favourites](PersonID, FavouriteDish, IsActive)
				SELECT A.PersonID, @P_FavouriteDish, @P_IsActive
				FROM [dbo].[Persons] A
				WHERE Username = @P_Username;
			END;
			COMMIT TRANSACTION;

			SELECT FavouriteID, A.PersonID, B.Username, FavouriteDish
			FROM [dbo].[Favourites] A
			INNER JOIN [dbo].[Persons] B ON A.PersonID = B.PersonID
			WHERE Username = @P_Username;
			
		END;

	END TRY
	BEGIN CATCH
      SELECT ERROR_MESSAGE() AS [ErrorMessage] INTO #Error_Log;
    ROLLBACK
	END CATCH
	SET NOCOUNT OFF;
END
GO


