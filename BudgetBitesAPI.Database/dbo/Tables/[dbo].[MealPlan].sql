CREATE TABLE [dbo].[MealPlan](
	[MealPlanID] [int] IDENTITY(1,1) NOT NULL,
	[PersonID] [int] NOT NULL,
	[PlanDate] [date] NOT NULL,
	[MealType] [nvarchar](255) NOT NULL,
	[DishName] [nvarchar](255) NOT NULL,
	[Serves] [int] NOT NULL,
	[TotalCost] [decimal](18, 2) NOT NULL,
	[ImageUrl] NVARCHAR(MAX) NULL
 CONSTRAINT [PK_MealPlan] PRIMARY KEY CLUSTERED 
(
	[MealPlanID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[MealPlan]  WITH CHECK ADD  CONSTRAINT [FK_MealPlan_Persons] FOREIGN KEY([PersonID])
REFERENCES [dbo].[Persons] ([PersonID])
GO

ALTER TABLE [dbo].[MealPlan] CHECK CONSTRAINT [FK_MealPlan_Persons]
GO


