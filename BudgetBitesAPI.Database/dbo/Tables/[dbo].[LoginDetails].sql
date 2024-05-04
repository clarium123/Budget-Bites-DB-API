CREATE TABLE [dbo].[LoginDetails](
	[LoginId] [int] IDENTITY(1,1) NOT NULL,
	[PersonID] [int] NOT NULL,
	[Username] [nvarchar](100) NOT NULL,
	[Password] [nvarchar](100) NOT NULL,
	[LastLogin] [datetime] NULL
 CONSTRAINT [PK_LoginDetails] PRIMARY KEY CLUSTERED 
(
	[LoginId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[LoginDetails]  WITH CHECK ADD  CONSTRAINT [FK_LoginDetails_Persons] FOREIGN KEY([PersonID])
REFERENCES [dbo].[Persons] ([PersonID])
GO

ALTER TABLE [dbo].[LoginDetails] CHECK CONSTRAINT [FK_LoginDetails_Persons]
GO


