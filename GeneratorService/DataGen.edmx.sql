
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 07/07/2014 10:36:37
-- Generated from EDMX file: C:\Users\Antoine-Ali\Source\Repos\Generator\GeneratorService\DataGen.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [master];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------


-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[DataSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[DataSet];
GO
IF OBJECT_ID(N'[dbo].[UserSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[UserSet];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'DataSets'
CREATE TABLE [dbo].[DataSets] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [FileID] nvarchar(max)  NOT NULL,
    [FileName] nvarchar(max)  NOT NULL,
    [DecodedText] nvarchar(max)  NOT NULL,
    [TrustLevelPdf] varbinary(max),
    [Mail] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'UserSets'
CREATE TABLE [dbo].[UserSets] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Login] nvarchar(max)  NOT NULL,
    [Password] nvarchar(max)  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'DataSets'
ALTER TABLE [dbo].[DataSets]
ADD CONSTRAINT [PK_DataSets]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'UserSets'
ALTER TABLE [dbo].[UserSets]
ADD CONSTRAINT [PK_UserSets]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------