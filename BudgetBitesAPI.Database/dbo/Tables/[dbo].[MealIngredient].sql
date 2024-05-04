CREATE TABLE [dbo].[MealIngredient](
	[MealIngredientID] [int] IDENTITY(1,1) NOT NULL,
	[MealPlanID] [int] NOT NULL,
	[Ingredient] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_MealIngredient] PRIMARY KEY CLUSTERED 
(
	[MealIngredientID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

ALTER TABLE [dbo].[MealIngredient]  WITH CHECK ADD  CONSTRAINT [FK_MealIngredient_MealPlan] FOREIGN KEY([MealPlanID])
REFERENCES [dbo].[MealPlan] ([MealPlanID])
GO

ALTER TABLE [dbo].[MealIngredient] CHECK CONSTRAINT [FK_MealIngredient_MealPlan]
GO


