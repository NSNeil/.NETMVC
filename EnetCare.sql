DROP TABLE [dbo].[EnetInterventions]
DROP TABLE [dbo].[EnetUsers]
DROP TABLE [dbo].[EnetClients]
DROP TABLE [dbo].[EnetInterventionTypes]
DROP TABLE [dbo].[EnetDistricts]

CREATE TABLE [dbo].[EnetDistricts] (
	[DistrictId] INT IDENTITY (1, 1) NOT NULL,
	[District] NVARCHAR (100) NOT NULL,
	CONSTRAINT [PK_District] PRIMARY KEY CLUSTERED ([DistrictId] ASC)
);

CREATE TABLE [dbo].[EnetInterventionTypes] (
	[InterventionTypeId] INT IDENTITY (1, 1) NOT NULL,
	[Name] NVARCHAR (50) NULL,
	[LabourHours] INT NULL,
	[Cost] DECIMAL (18) NULL,
	CONSTRAINT [PK_InterventionType] PRIMARY KEY CLUSTERED ([InterventionTypeId] ASC)
);

CREATE TABLE [dbo].[EnetClients] (
	[ClientId] INT IDENTITY (1, 1) NOT NULL,
	[DistrictId] INT NULL,
	[Name] NVARCHAR (50) NULL,
	[Address] NVARCHAR (100) NULL,
	CONSTRAINT [PK_Client] PRIMARY KEY CLUSTERED ([ClientId] ASC),
	CONSTRAINT [FK_Client_District] FOREIGN KEY ([DistrictId]) REFERENCES [dbo].[EnetDistricts] ([DistrictId]) ON DELETE CASCADE ON UPDATE CASCADE
);

CREATE TABLE [dbo].[EnetUsers] (
	[UserId] INT IDENTITY (1, 1) NOT NULL,
	[LoginName] NVARCHAR (10) NOT NULL,
	[Name] NVARCHAR (50) NOT NULL,
	[Role] INT NULL,
	[DistrictId] INT NULL,
	[MaxCost] DECIMAL (18) NULL,
	[MaxHours] INT NULL,
	CONSTRAINT [PK_users] PRIMARY KEY CLUSTERED ([UserId] ASC),
	CONSTRAINT [FK_User_District] FOREIGN KEY ([DistrictId]) REFERENCES [dbo].[EnetDistricts] ([DistrictId]) ON DELETE CASCADE ON UPDATE CASCADE,
	UNIQUE NONCLUSTERED ([LoginName] ASC)
);

CREATE TABLE [dbo].[EnetInterventions] (
	[InterventionId] INT IDENTITY (1, 1) NOT NULL,
	[InterventionTypeId] INT NULL,
	[ClientId] INT NULL,
	[DistrictId] INT NULL,
	[ProposedBy] INT NULL,
	[ApprovedBy] INT NULL,
	[DateToPerform] NVARCHAR (100) NULL,
	[MostRecentVisitDate] NVARCHAR (100) NULL,
	[LabourHours] INT NULL,
	[Cost] DECIMAL (18) NULL,
	[Life] FLOAT (53) NULL,
	[State] INT NULL,
	[Note] NVARCHAR (100) NULL,
	CONSTRAINT [PK_Intervention] PRIMARY KEY CLUSTERED ([InterventionId] ASC),
	CONSTRAINT [FK_Intervention_Client] FOREIGN KEY ([ClientId]) REFERENCES [dbo].[EnetClients] ([ClientId]) ON DELETE CASCADE ON UPDATE CASCADE,
	CONSTRAINT [FK_Intervention_InterventionType] FOREIGN KEY ([InterventionTypeId]) REFERENCES [dbo].[EnetInterventionTypes] ([InterventionTypeId]) ON DELETE CASCADE ON UPDATE CASCADE,
	CONSTRAINT [FK_Intervention_ApprovedBy] FOREIGN KEY ([ApprovedBy]) REFERENCES [dbo].[EnetUsers] ([UserId]),
	CONSTRAINT [FK_Intervention_ProposedBy] FOREIGN KEY ([ProposedBy]) REFERENCES [dbo].[EnetUsers] ([UserId]),
	CONSTRAINT [FK_Intervention_District] FOREIGN KEY ([DistrictId]) REFERENCES [dbo].[EnetDistricts] ([DistrictId])
);

SET IDENTITY_INSERT [dbo].[EnetDistricts] ON
INSERT INTO [dbo].[EnetDistricts] ([DistrictId], [District]) VALUES (1, 'Rural Indonesia')
INSERT INTO [dbo].[EnetDistricts] ([DistrictId], [District]) VALUES (2, 'Rural New South Wales')
INSERT INTO [dbo].[EnetDistricts] ([DistrictId], [District]) VALUES (3, 'Rural Papua New Guinea')
INSERT INTO [dbo].[EnetDistricts] ([DistrictId], [District]) VALUES (4, 'Sydney')
INSERT INTO [dbo].[EnetDistricts] ([DistrictId], [District]) VALUES (5, 'Urban Indonesia')
INSERT INTO [dbo].[EnetDistricts] ([DistrictId], [District]) VALUES (6, 'Urban Papua New Guinea')
SET IDENTITY_INSERT [dbo].[EnetDistricts] OFF

SET IDENTITY_INSERT [dbo].[EnetClients] ON
INSERT INTO [dbo].[EnetClients] ([ClientId], [DistrictId], [Name], [Address]) VALUES (1, 1, 'John One (RI)', 'Pitt St')
INSERT INTO [dbo].[EnetClients] ([ClientId], [DistrictId], [Name], [Address]) VALUES (2, 2, 'John Two (RNSW)', 'Pitt St')
INSERT INTO [dbo].[EnetClients] ([ClientId], [DistrictId], [Name], [Address]) VALUES (3, 3, 'John Three (RPNG)', 'Pitt St')
INSERT INTO [dbo].[EnetClients] ([ClientId], [DistrictId], [Name], [Address]) VALUES (4, 4, 'John Four (SYD)', 'Pitt St')
INSERT INTO [dbo].[EnetClients] ([ClientId], [DistrictId], [Name], [Address]) VALUES (5, 5, 'KG (UI)', 'Yip St')
INSERT INTO [dbo].[EnetClients] ([ClientId], [DistrictId], [Name], [Address]) VALUES (6, 6, 'John Six (UPNG)', 'Pitt St')
SET IDENTITY_INSERT [dbo].[EnetClients] OFF

SET IDENTITY_INSERT [dbo].[EnetInterventionTypes] ON
INSERT INTO [dbo].[EnetInterventionTypes] ([InterventionTypeId], [Name], [LabourHours], [Cost]) VALUES (1, 'Supply and Install Portable Toilet', 100, CAST(10000 AS Decimal(18, 0)))
INSERT INTO [dbo].[EnetInterventionTypes] ([InterventionTypeId], [Name], [LabourHours], [Cost]) VALUES (2, 'Hepatitis Avoidance Training', 56, CAST(3445 AS Decimal(18, 0)))
SET IDENTITY_INSERT [dbo].[EnetInterventionTypes] OFF

--User Role Guide
-- 0 Accountant,
-- 1 Manager,
-- 2 SiteEngineer

SET IDENTITY_INSERT [dbo].[EnetUsers] ON
INSERT INTO [dbo].[EnetUsers] ([UserId], [LoginName], [Name], [Role], [DistrictId], [MaxCost], [MaxHours]) VALUES (1, 'Dean', 'Dean', 2, 1, 9200000, 92000)
INSERT INTO [dbo].[EnetUsers] ([UserId], [LoginName], [Name], [Role], [DistrictId], [MaxCost], [MaxHours]) VALUES (2, 'Sam', 'Sam', 1, 1, 9200000, 92000)
INSERT INTO [dbo].[EnetUsers] ([UserId], [LoginName], [Name], [Role], [DistrictId], [MaxCost], [MaxHours]) VALUES (3, 'George', 'George', 1, 3, 9200000, 92000)
INSERT INTO [dbo].[EnetUsers] ([UserId], [LoginName], [Name], [Role], [DistrictId], [MaxCost], [MaxHours]) VALUES (4, 'Kim', 'Kim', 2, 1, 9200000, 92000)
INSERT INTO [dbo].[EnetUsers] ([UserId], [LoginName], [Name], [Role], [DistrictId], [MaxCost], [MaxHours]) VALUES (5, 'Mark', 'Mark', 0, 4, 9200000, 92000)
SET IDENTITY_INSERT [dbo].[EnetUsers] OFF

--Intervention State Guide
--1: Proposed
--2: Approved
--3: Completed
--4: Cancelled

--District Guide
--1: Rural Indonesia
--2: Rural New South Wales
--3: Rural Papua New Guinea
--4: Sydney
--5: Urban Indonesia
--6: Urban Papua New Guinea

SET IDENTITY_INSERT [dbo].[EnetInterventions] ON
INSERT INTO [dbo].[EnetInterventions] ([InterventionId], [InterventionTypeId], [ClientId], [DistrictId], [ProposedBy], [ApprovedBy], [DateToPerform], [MostRecentVisitDate], [LabourHours], [Cost], [Life], [State], [Note])
VALUES (1, 1, 1, 1, 3, NULL, '15/10/2017', '15/11/2017', 100, CAST(10000 AS Decimal(18, 0)), 0, 0, 'Epic intervention')
INSERT INTO [dbo].[EnetInterventions] ([InterventionId], [InterventionTypeId], [ClientId], [DistrictId], [ProposedBy], [ApprovedBy], [DateToPerform], [MostRecentVisitDate], [LabourHours], [Cost], [Life], [State], [Note])
VALUES (2, 2, 2, 4, 5, NULL, '30/04/2017', '15/09/2017', 50, CAST(1500 AS Decimal(18, 0)), 0, 0, 'SD')
INSERT INTO [dbo].[EnetInterventions] ([InterventionId], [InterventionTypeId], [ClientId], [DistrictId], [ProposedBy], [ApprovedBy], [DateToPerform], [MostRecentVisitDate], [LabourHours], [Cost], [Life], [State], [Note])
VALUES (3, 2, 2, 1, 5, NULL, '15/09/2017', '19/09/2017', 100, CAST(10000 AS Decimal(18, 0)), 0, 0, 'SD')
INSERT INTO [dbo].[EnetInterventions] ([InterventionId], [InterventionTypeId], [ClientId], [DistrictId], [ProposedBy], [ApprovedBy], [DateToPerform], [MostRecentVisitDate], [LabourHours], [Cost], [Life], [State], [Note])
VALUES (4, 2, 2, 1, 5, 1, '30/07/2017', '15/09/2017', 10, CAST(1500 AS Decimal(18, 0)), 1, 1, 'SD')
INSERT INTO [dbo].[EnetInterventions] ([InterventionId], [InterventionTypeId], [ClientId], [DistrictId], [ProposedBy], [ApprovedBy], [DateToPerform], [MostRecentVisitDate], [LabourHours], [Cost], [Life], [State], [Note])
VALUES (5, 2, 2, 1, 5, 4, '20/07/2017', '15/09/2017', 150, CAST(15000 AS Decimal(18, 0)), 0.5, 1, 'SD')
INSERT INTO [dbo].[EnetInterventions] ([InterventionId], [InterventionTypeId], [ClientId], [DistrictId], [ProposedBy], [ApprovedBy], [DateToPerform], [MostRecentVisitDate], [LabourHours], [Cost], [Life], [State], [Note])
VALUES (6, 2, 2, 1, 3, 5, '20/07/2017', '15/09/2017', 150, CAST(15000 AS Decimal(18, 0)), 0, 1, 'SD')
INSERT INTO [dbo].[EnetInterventions] ([InterventionId], [InterventionTypeId], [ClientId], [DistrictId], [ProposedBy], [ApprovedBy], [DateToPerform], [MostRecentVisitDate], [LabourHours], [Cost], [Life], [State], [Note])
VALUES (7, 2, 2, 1, 5, NULL, '13/08/2017', '19/09/2017', 100, CAST(10000 AS Decimal(18, 0)), 0, 2, 'SD')
INSERT INTO [dbo].[EnetInterventions] ([InterventionId], [InterventionTypeId], [ClientId], [DistrictId], [ProposedBy], [ApprovedBy], [DateToPerform], [MostRecentVisitDate], [LabourHours], [Cost], [Life], [State], [Note])
VALUES (8, 2, 2, 4, 5, NULL, '30/04/2017', '15/08/2017', 50, CAST(1500 AS Decimal(18, 0)), 0, 2, 'SD')
INSERT INTO [dbo].[EnetInterventions] ([InterventionId], [InterventionTypeId], [ClientId], [DistrictId], [ProposedBy], [ApprovedBy], [DateToPerform], [MostRecentVisitDate], [LabourHours], [Cost], [Life], [State], [Note])
VALUES (9, 1, 1, 1, 3, NULL, '10/10/2017', '6/11/2017', 100, CAST(10000 AS Decimal(18, 0)), 0, 2, 'Epic intervention')
INSERT INTO [dbo].[EnetInterventions] ([InterventionId], [InterventionTypeId], [ClientId], [DistrictId], [ProposedBy], [ApprovedBy], [DateToPerform], [MostRecentVisitDate], [LabourHours], [Cost], [Life], [State], [Note])
VALUES (10, 2, 2, 4, 5, NULL, '02/08/2017', '15/08/2017', 50, CAST(1500 AS Decimal(18, 0)), 0, 2, 'SD')
SET IDENTITY_INSERT [dbo].[EnetInterventions] OFF
