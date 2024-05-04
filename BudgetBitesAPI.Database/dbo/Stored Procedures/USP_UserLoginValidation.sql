CREATE PROCEDURE [dbo].[USP_UserLoginValidation](
	-- Add the parameters for the stored procedure here
	@P_Username VARCHAR(100),
	@P_Password NVARCHAR(MAX)
)
AS
BEGIN
	SET NOCOUNT ON;
	SELECT PersonID, Username, Password, LastLogin FROM [dbo].[LoginDetails] WHERE Username = @P_Username AND Password = @P_Password;
	SET NOCOUNT OFF;
END
GO


