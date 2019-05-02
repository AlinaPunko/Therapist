
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 04/26/2019 20:48:46
-- Generated from EDMX file: E:\лабы\Therapist\Therapist\Therapist\Data\Therapist.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [Therapist];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_Consultations_Doctors]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Consultations] DROP CONSTRAINT [FK_Consultations_Doctors];
GO
IF OBJECT_ID(N'[dbo].[FK_Consultations_Patients]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Consultations] DROP CONSTRAINT [FK_Consultations_Patients];
GO
IF OBJECT_ID(N'[dbo].[FK_Visits_Doctors]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Visits] DROP CONSTRAINT [FK_Visits_Doctors];
GO
IF OBJECT_ID(N'[dbo].[FK_Visits_Patients]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Visits] DROP CONSTRAINT [FK_Visits_Patients];
GO
IF OBJECT_ID(N'[dbo].[FK_User_Doctor]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Users] DROP CONSTRAINT [FK_User_Doctor];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Consultations]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Consultations];
GO
IF OBJECT_ID(N'[dbo].[Visits]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Visits];
GO
IF OBJECT_ID(N'[dbo].[Doctors]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Doctors];
GO
IF OBJECT_ID(N'[dbo].[Patients]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Patients];
GO
IF OBJECT_ID(N'[dbo].[Users]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Users];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Consultations'
CREATE TABLE [dbo].[Consultations] (
    [ConsultationID] int IDENTITY(1,1) NOT NULL,
    [ScheduleDate] datetime  NULL,
    [ScheduleTime] time  NULL,
    [PatientID] int  NULL,
    [DoctorID] int  NULL,
    [Reason] nvarchar(512)  NULL,
    [Notes] nvarchar(512)  NULL
);
GO

-- Creating table 'Visits'
CREATE TABLE [dbo].[Visits] (
    [VisitID] int IDENTITY(1,1) NOT NULL,
    [PatientID] int  NULL,
    [DoctorID] int  NULL,
    [Reason] nvarchar(255)  NULL,
    [Notes] nvarchar(512)  NULL,
    [VisitDate] datetime  NULL,
    [Prescription] nvarchar(255)  NULL
);
GO

-- Creating table 'Doctors'
CREATE TABLE [dbo].[Doctors] (
    [DoctorID] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(50)  NULL,
    [Skils] nvarchar(255)  NULL,
    [Phone] nvarchar(32)  NULL,
    [Address] nvarchar(255)  NULL
);
GO

-- Creating table 'Patients'
CREATE TABLE [dbo].[Patients] (
    [PatientID] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(50)  NOT NULL,
    [Address] nvarchar(255)  NULL,
    [Phone] nvarchar(32)  NULL,
    [Birthdate] datetime  NULL
);
GO

-- Creating table 'Users'
CREATE TABLE [dbo].[Users] (
    [UserID] int IDENTITY(1,1) NOT NULL,
    [UserName] nvarchar(50)  NULL,
    [Password] nvarchar(128)  NULL,
    [DoctorID] int  NULL,
    [RoleID] smallint  NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [ConsultationID] in table 'Consultations'
ALTER TABLE [dbo].[Consultations]
ADD CONSTRAINT [PK_Consultations]
    PRIMARY KEY CLUSTERED ([ConsultationID] ASC);
GO

-- Creating primary key on [VisitID] in table 'Visits'
ALTER TABLE [dbo].[Visits]
ADD CONSTRAINT [PK_Visits]
    PRIMARY KEY CLUSTERED ([VisitID] ASC);
GO

-- Creating primary key on [DoctorID] in table 'Doctors'
ALTER TABLE [dbo].[Doctors]
ADD CONSTRAINT [PK_Doctors]
    PRIMARY KEY CLUSTERED ([DoctorID] ASC);
GO

-- Creating primary key on [PatientID] in table 'Patients'
ALTER TABLE [dbo].[Patients]
ADD CONSTRAINT [PK_Patients]
    PRIMARY KEY CLUSTERED ([PatientID] ASC);
GO

-- Creating primary key on [UserID] in table 'Users'
ALTER TABLE [dbo].[Users]
ADD CONSTRAINT [PK_Users]
    PRIMARY KEY CLUSTERED ([UserID] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [DoctorID] in table 'Consultations'
ALTER TABLE [dbo].[Consultations]
ADD CONSTRAINT [FK_Consultations_Doctors]
    FOREIGN KEY ([DoctorID])
    REFERENCES [dbo].[Doctors]
        ([DoctorID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Consultations_Doctors'
CREATE INDEX [IX_FK_Consultations_Doctors]
ON [dbo].[Consultations]
    ([DoctorID]);
GO

-- Creating foreign key on [PatientID] in table 'Consultations'
ALTER TABLE [dbo].[Consultations]
ADD CONSTRAINT [FK_Consultations_Patients]
    FOREIGN KEY ([PatientID])
    REFERENCES [dbo].[Patients]
        ([PatientID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Consultations_Patients'
CREATE INDEX [IX_FK_Consultations_Patients]
ON [dbo].[Consultations]
    ([PatientID]);
GO

-- Creating foreign key on [DoctorID] in table 'Visits'
ALTER TABLE [dbo].[Visits]
ADD CONSTRAINT [FK_Visits_Doctors]
    FOREIGN KEY ([DoctorID])
    REFERENCES [dbo].[Doctors]
        ([DoctorID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Visits_Doctors'
CREATE INDEX [IX_FK_Visits_Doctors]
ON [dbo].[Visits]
    ([DoctorID]);
GO

-- Creating foreign key on [PatientID] in table 'Visits'
ALTER TABLE [dbo].[Visits]
ADD CONSTRAINT [FK_Visits_Patients]
    FOREIGN KEY ([PatientID])
    REFERENCES [dbo].[Patients]
        ([PatientID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Visits_Patients'
CREATE INDEX [IX_FK_Visits_Patients]
ON [dbo].[Visits]
    ([PatientID]);
GO

-- Creating foreign key on [DoctorID] in table 'Users'
ALTER TABLE [dbo].[Users]
ADD CONSTRAINT [FK_User_Doctor]
    FOREIGN KEY ([DoctorID])
    REFERENCES [dbo].[Doctors]
        ([DoctorID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_User_Doctor'
CREATE INDEX [IX_FK_User_Doctor]
ON [dbo].[Users]
    ([DoctorID]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------