CREATE PROCEDURE [dbo].[USP_UserFavouriteFood](
	-- Add the parameters for the stored procedure here
	@P_Username VARCHAR(500)
)
AS
BEGIN
	SET NOCOUNT ON;
	SELECT A.FavouriteID, A.PersonID, B.Username, A.FavouriteDish 
	FROM [dbo].[Favourites] A
	INNER JOIN [dbo].[Persons] B ON A.PersonID = B.PersonID
	WHERE Username = @P_Username AND A.IsActive = 1;
	SET NOCOUNT OFF;
END
GO


