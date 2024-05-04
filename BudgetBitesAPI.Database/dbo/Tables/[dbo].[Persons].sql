CREATE TABLE [dbo].[Persons](
	[PersonID] [int] IDENTITY(1,1) NOT NULL,
	[Firstname] [varchar](100) NOT NULL,
	[Lastname] [varchar](100) NOT NULL,
	[Username] [nvarchar](255) NOT NULL,
	[Password] [nvarchar](MAX) NOT NULL,
	[EmailId] [nvarchar](255) NOT NULL,
	[Phone] [nvarchar](20) NOT NULL,
	[Address] [nvarchar](255) NOT NULL,
	[City] [nvarchar](100) NOT NULL,
	[State] [nvarchar](100) NOT NULL,
	[FamilyMember] [int] NOT NULL,
	[CreatedBy] [nvarchar](255) NULL,
	[CreatedOn] [datetime] NULL,
 CONSTRAINT [PK_Persons] PRIMARY KEY CLUSTERED 
(
	[PersonID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO


