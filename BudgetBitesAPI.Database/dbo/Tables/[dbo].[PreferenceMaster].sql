CREATE TABLE [dbo].[PreferenceMaster](
	[PreferenceID] [int] IDENTITY(1,1) NOT NULL,
	[Preferences] [nvarchar](500) NOT NULL,
 CONSTRAINT [PK_PreferenceMaster] PRIMARY KEY CLUSTERED 
(
	[PreferenceID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
--SET IDENTITY_INSERT [dbo].[PreferenceMaster] ON 
--GO
--INSERT [dbo].[PreferenceMaster] ([PreferenceID], [Preferences]) VALUES (1, N'Vegetarian')
--GO
--INSERT [dbo].[PreferenceMaster] ([PreferenceID], [Preferences]) VALUES (2, N'Vegan')
--GO
--INSERT [dbo].[PreferenceMaster] ([PreferenceID], [Preferences]) VALUES (3, N'Non-vegetarian')
--GO
--INSERT [dbo].[PreferenceMaster] ([PreferenceID], [Preferences]) VALUES (4, N'Paleo')
--GO
--INSERT [dbo].[PreferenceMaster] ([PreferenceID], [Preferences]) VALUES (5, N'Keto')
--GO
--INSERT [dbo].[PreferenceMaster] ([PreferenceID], [Preferences]) VALUES (6, N'Gluten-Free')
--GO
--INSERT [dbo].[PreferenceMaster] ([PreferenceID], [Preferences]) VALUES (7, N'Raw Food')
--GO
--INSERT [dbo].[PreferenceMaster] ([PreferenceID], [Preferences]) VALUES (8, N'DASH (Dietary Approaches to Stop Hypertension)')
--GO
--SET IDENTITY_INSERT [dbo].[PreferenceMaster] OFF
--GO
