
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 05/19/2017 11:17:58
-- Generated from EDMX file: C:\Users\ClassicNeil\Desktop\NET\GitHub\DorothyAndCockroaches2\ENETCareMVC\BusinessLogicLayer\Models\EnetModel.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [EnetCare];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_Client_District]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[EnetClients] DROP CONSTRAINT [FK_Client_District];
GO
IF OBJECT_ID(N'[dbo].[FK_Intervention_Client]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[EnetInterventions] DROP CONSTRAINT [FK_Intervention_Client];
GO
IF OBJECT_ID(N'[dbo].[FK_Intervention_District]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[EnetInterventions] DROP CONSTRAINT [FK_Intervention_District];
GO
IF OBJECT_ID(N'[dbo].[FK_User_District]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[EnetUsers] DROP CONSTRAINT [FK_User_District];
GO
IF OBJECT_ID(N'[dbo].[FK_Intervention_ApprovedBy]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[EnetInterventions] DROP CONSTRAINT [FK_Intervention_ApprovedBy];
GO
IF OBJECT_ID(N'[dbo].[FK_Intervention_InterventionType]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[EnetInterventions] DROP CONSTRAINT [FK_Intervention_InterventionType];
GO
IF OBJECT_ID(N'[dbo].[FK_Intervention_ProposedBy]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[EnetInterventions] DROP CONSTRAINT [FK_Intervention_ProposedBy];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[EnetClients]', 'U') IS NOT NULL
    DROP TABLE [dbo].[EnetClients];
GO
IF OBJECT_ID(N'[dbo].[EnetDistricts]', 'U') IS NOT NULL
    DROP TABLE [dbo].[EnetDistricts];
GO
IF OBJECT_ID(N'[dbo].[EnetInterventions]', 'U') IS NOT NULL
    DROP TABLE [dbo].[EnetInterventions];
GO
IF OBJECT_ID(N'[dbo].[EnetInterventionTypes]', 'U') IS NOT NULL
    DROP TABLE [dbo].[EnetInterventionTypes];
GO
IF OBJECT_ID(N'[dbo].[EnetUsers]', 'U') IS NOT NULL
    DROP TABLE [dbo].[EnetUsers];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'EnetClients'
CREATE TABLE [dbo].[EnetClients] (
    [ClientId] int IDENTITY(1,1) NOT NULL,
    [DistrictId] int  NULL,
    [Name] nvarchar(50)  NULL,
    [Address] nvarchar(100)  NULL
);
GO

-- Creating table 'EnetDistricts'
CREATE TABLE [dbo].[EnetDistricts] (
    [DistrictId] int IDENTITY(1,1) NOT NULL,
    [District] nvarchar(100)  NOT NULL
);
GO

-- Creating table 'EnetInterventions'
CREATE TABLE [dbo].[EnetInterventions] (
    [InterventionId] int IDENTITY(1,1) NOT NULL,
    [InterventionTypeId] int  NULL,
    [ClientId] int  NULL,
    [DistrictId] int  NULL,
    [ProposedBy] int  NULL,
    [ApprovedBy] int  NULL,
    [DateToPerform] nvarchar(100)  NULL,
    [MostRecentVisitDate] nvarchar(100)  NULL,
    [LabourHours] int  NULL,
    [Cost] decimal(18,0)  NULL,
    [Life] float  NULL,
    [State] int  NULL,
    [Note] nvarchar(100)  NULL
);
GO

-- Creating table 'EnetInterventionTypes'
CREATE TABLE [dbo].[EnetInterventionTypes] (
    [InterventionTypeId] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(50)  NULL,
    [LabourHours] int  NULL,
    [Cost] decimal(18,0)  NULL
);
GO

-- Creating table 'EnetUsers'
CREATE TABLE [dbo].[EnetUsers] (
    [UserId] int IDENTITY(1,1) NOT NULL,
    [LoginName] nvarchar(10)  NOT NULL,
    [Name] nvarchar(50)  NOT NULL,
    [Role] int  NULL,
    [DistrictId] int  NULL,
    [MaxCost] decimal(18,0)  NULL,
    [MaxHours] int  NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [ClientId] in table 'EnetClients'
ALTER TABLE [dbo].[EnetClients]
ADD CONSTRAINT [PK_EnetClients]
    PRIMARY KEY CLUSTERED ([ClientId] ASC);
GO

-- Creating primary key on [DistrictId] in table 'EnetDistricts'
ALTER TABLE [dbo].[EnetDistricts]
ADD CONSTRAINT [PK_EnetDistricts]
    PRIMARY KEY CLUSTERED ([DistrictId] ASC);
GO

-- Creating primary key on [InterventionId] in table 'EnetInterventions'
ALTER TABLE [dbo].[EnetInterventions]
ADD CONSTRAINT [PK_EnetInterventions]
    PRIMARY KEY CLUSTERED ([InterventionId] ASC);
GO

-- Creating primary key on [InterventionTypeId] in table 'EnetInterventionTypes'
ALTER TABLE [dbo].[EnetInterventionTypes]
ADD CONSTRAINT [PK_EnetInterventionTypes]
    PRIMARY KEY CLUSTERED ([InterventionTypeId] ASC);
GO

-- Creating primary key on [UserId] in table 'EnetUsers'
ALTER TABLE [dbo].[EnetUsers]
ADD CONSTRAINT [PK_EnetUsers]
    PRIMARY KEY CLUSTERED ([UserId] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [DistrictId] in table 'EnetClients'
ALTER TABLE [dbo].[EnetClients]
ADD CONSTRAINT [FK_Client_District]
    FOREIGN KEY ([DistrictId])
    REFERENCES [dbo].[EnetDistricts]
        ([DistrictId])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Client_District'
CREATE INDEX [IX_FK_Client_District]
ON [dbo].[EnetClients]
    ([DistrictId]);
GO

-- Creating foreign key on [ClientId] in table 'EnetInterventions'
ALTER TABLE [dbo].[EnetInterventions]
ADD CONSTRAINT [FK_Intervention_Client]
    FOREIGN KEY ([ClientId])
    REFERENCES [dbo].[EnetClients]
        ([ClientId])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Intervention_Client'
CREATE INDEX [IX_FK_Intervention_Client]
ON [dbo].[EnetInterventions]
    ([ClientId]);
GO

-- Creating foreign key on [DistrictId] in table 'EnetInterventions'
ALTER TABLE [dbo].[EnetInterventions]
ADD CONSTRAINT [FK_Intervention_District]
    FOREIGN KEY ([DistrictId])
    REFERENCES [dbo].[EnetDistricts]
        ([DistrictId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Intervention_District'
CREATE INDEX [IX_FK_Intervention_District]
ON [dbo].[EnetInterventions]
    ([DistrictId]);
GO

-- Creating foreign key on [DistrictId] in table 'EnetUsers'
ALTER TABLE [dbo].[EnetUsers]
ADD CONSTRAINT [FK_User_District]
    FOREIGN KEY ([DistrictId])
    REFERENCES [dbo].[EnetDistricts]
        ([DistrictId])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_User_District'
CREATE INDEX [IX_FK_User_District]
ON [dbo].[EnetUsers]
    ([DistrictId]);
GO

-- Creating foreign key on [ApprovedBy] in table 'EnetInterventions'
ALTER TABLE [dbo].[EnetInterventions]
ADD CONSTRAINT [FK_Intervention_ApprovedBy]
    FOREIGN KEY ([ApprovedBy])
    REFERENCES [dbo].[EnetUsers]
        ([UserId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Intervention_ApprovedBy'
CREATE INDEX [IX_FK_Intervention_ApprovedBy]
ON [dbo].[EnetInterventions]
    ([ApprovedBy]);
GO

-- Creating foreign key on [InterventionTypeId] in table 'EnetInterventions'
ALTER TABLE [dbo].[EnetInterventions]
ADD CONSTRAINT [FK_Intervention_InterventionType]
    FOREIGN KEY ([InterventionTypeId])
    REFERENCES [dbo].[EnetInterventionTypes]
        ([InterventionTypeId])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Intervention_InterventionType'
CREATE INDEX [IX_FK_Intervention_InterventionType]
ON [dbo].[EnetInterventions]
    ([InterventionTypeId]);
GO

-- Creating foreign key on [ProposedBy] in table 'EnetInterventions'
ALTER TABLE [dbo].[EnetInterventions]
ADD CONSTRAINT [FK_Intervention_ProposedBy]
    FOREIGN KEY ([ProposedBy])
    REFERENCES [dbo].[EnetUsers]
        ([UserId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Intervention_ProposedBy'
CREATE INDEX [IX_FK_Intervention_ProposedBy]
ON [dbo].[EnetInterventions]
    ([ProposedBy]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------