CREATE TABLE [dbo].[PreferredCusine](
	[PreferredCusineID] [int] IDENTITY(1,1) NOT NULL,
	[PersonID] [int] NOT NULL,
	[PreferredCusine] [varchar](255) NOT NULL,
 CONSTRAINT [PK_PreferredCusine] PRIMARY KEY CLUSTERED 
(
	[PreferredCusineID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[PreferredCusine]  WITH CHECK ADD  CONSTRAINT [FK_PreferredCusine_Persons] FOREIGN KEY([PersonID])
REFERENCES [dbo].[Persons] ([PersonID])
GO

ALTER TABLE [dbo].[PreferredCusine] CHECK CONSTRAINT [FK_PreferredCusine_Persons]
GO


