CREATE PROCEDURE [dbo].[USP_UserRegistration](
	-- Add the parameters for the stored procedure here		
	@P_Firstname VARCHAR(100),
	@P_Lastname VARCHAR(100),
	@P_Username VARCHAR(100),
	@P_Password VARCHAR(100),
	@P_EmailId NVARCHAR(250),
	@P_Phone VARCHAR(20),
	@P_Address NVARCHAR(250),
	@P_City NVARCHAR(100),
	@P_State NVARCHAR(100),
	@P_FamilyMember INT,
	@P_FoodPrefered NVARCHAR(MAX),
	@P_BudgetAmount DECIMAL(18,2),
	@P_PreferedCusine VARCHAR(255)
)
AS
BEGIN
	SET NOCOUNT ON;
	BEGIN TRY	
		IF EXISTS(SELECT 'X' FROM [dbo].[Persons] WHERE Username = @P_Username)
		BEGIN
			SELECT PersonID, Firstname, Lastname, 'Username Already Registered' [Username], NULL[Password], EmailId, Phone, Address, City, State, FamilyMember
			FROM [dbo].[Persons] WHERE Username = @P_Username;
		END
		ELSE IF EXISTS(SELECT 'X' FROM [dbo].[Persons] WHERE Phone = @P_Phone)
		BEGIN
			SELECT PersonID, Firstname, Lastname, 'Phone Number Already Registered' [Username], NULL[Password], EmailId, Phone, Address, City, State, FamilyMember
			FROM [dbo].[Persons] WHERE Phone = @P_Phone;
		END
		ELSE IF EXISTS(SELECT 'X' FROM [dbo].[Persons] WHERE EmailId = @P_EmailId)
		BEGIN
			SELECT 
			PersonID, Firstname, Lastname, 'EmailId Already Registered' [Username], NULL[Password], EmailId, Phone, Address, City, State, FamilyMember
			FROM [dbo].[Persons] WHERE EmailId = @P_EmailId;
		END
		ELSE
		BEGIN	
			BEGIN TRANSACTION
			DECLARE @PersonID INT;
			---Insert Persons table ---
			INSERT INTO [dbo].[Persons](Firstname, Lastname, Username, Password, EmailId, Phone, Address, City, State, FamilyMember, CreatedBy, CreatedOn)
			VALUES(@P_Firstname, @P_Lastname, @P_Username, @P_Password, @P_EmailId, @P_Phone, @P_Address, @P_City, @P_State, @P_FamilyMember, @P_Username, GETDATE());
			SET @PersonID = @@IDENTITY;
			--SET @PersonID = (SELECT PersonID FROM  [dbo].[Persons] WHERE Username = @P_Username);

			---Insert Logindetails to Validate ---
			INSERT INTO [dbo].[LoginDetails](PersonID, Username, Password)
			VALUES(@PersonID, @P_Username, @P_Password);

			-----Insert Budget table ---
			INSERT INTO [dbo].[Budget](PersonID, WeekStartDate, WeekEndDate, BudgetAmount)
			VALUES(@PersonID, NULL, NULL, @P_BudgetAmount);

			-----Insert Food Prefernces table ---
			INSERT INTO [dbo].[FoodPreference](PersonID, PreferredFood)
			VALUES(@PersonID, @P_FoodPrefered);

			-------Insert Preferred Cusine table ---
			INSERT INTO [dbo].[PreferredCusine](PersonID, PreferredCusine)
			VALUES(@PersonID, @P_PreferedCusine);

			SELECT PersonID, Firstname, Lastname, Username, NULL[Password], EmailId, Phone, Address, City, State, FamilyMember
			FROM [dbo].[Persons] WHERE PersonID = @PersonID;
			COMMIT TRANSACTION;
		END
	END TRY
	BEGIN CATCH
      SELECT ERROR_MESSAGE() AS [ErrorMessage] INTO #Error_Log;
    ROLLBACK
	END CATCH
	SET NOCOUNT OFF;
END
GO