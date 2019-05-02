
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 04/12/2019 19:30:33
-- Generated from EDMX file: E:\лабы\Therapist\Therapist\Therapist\Data\Model1.edmx
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


-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Visits'
CREATE TABLE [dbo].[Visits] (
    [VisitID] int IDENTITY(1,1) NOT NULL,
    [PatientId] int  NULL,
    [DoctorId] int  NULL,
    [Reason] nvarchar(255)  NULL,
    [Notes] nvarchar(512)  NULL,
    [VisitDate] datetime  NULL,
    [Prescription] nvarchar(255)  NULL,
    [Doctor_DoctorID] int  NULL,
    [Patient_PatientID] int  NOT NULL
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

-- Creating table 'Users'
CREATE TABLE [dbo].[Users] (
    [UserID] int IDENTITY(1,1) NOT NULL,
    [UserName] nvarchar(50)  NULL,
    [Password] nvarchar(128)  NULL,
    [DoctorId] int  NULL,
    [RoleId] smallint  NULL,
    [Doctor_DoctorID] int  NULL
);
GO

-- Creating table 'Consultations'
CREATE TABLE [dbo].[Consultations] (
    [ConsultationID] int IDENTITY(1,1) NOT NULL,
    [ScheduleDate] datetime  NULL,
    [ScheduleTime] time  NULL,
    [PatientId] int  NULL,
    [DoctorId] int  NULL,
    [Reason] nvarchar(512)  NULL,
    [Notes] nvarchar(512)  NULL,
    [Doctor_DoctorID] int  NULL,
    [Patient_PatientID] int  NULL
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

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

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

-- Creating primary key on [UserID] in table 'Users'
ALTER TABLE [dbo].[Users]
ADD CONSTRAINT [PK_Users]
    PRIMARY KEY CLUSTERED ([UserID] ASC);
GO

-- Creating primary key on [ConsultationID] in table 'Consultations'
ALTER TABLE [dbo].[Consultations]
ADD CONSTRAINT [PK_Consultations]
    PRIMARY KEY CLUSTERED ([ConsultationID] ASC);
GO

-- Creating primary key on [PatientID] in table 'Patients'
ALTER TABLE [dbo].[Patients]
ADD CONSTRAINT [PK_Patients]
    PRIMARY KEY CLUSTERED ([PatientID] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [Doctor_DoctorID] in table 'Visits'
ALTER TABLE [dbo].[Visits]
ADD CONSTRAINT [FK_DoctorVisit]
    FOREIGN KEY ([Doctor_DoctorID])
    REFERENCES [dbo].[Doctors]
        ([DoctorID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_DoctorVisit'
CREATE INDEX [IX_FK_DoctorVisit]
ON [dbo].[Visits]
    ([Doctor_DoctorID]);
GO

-- Creating foreign key on [Patient_PatientID] in table 'Visits'
ALTER TABLE [dbo].[Visits]
ADD CONSTRAINT [FK_PatientVisit]
    FOREIGN KEY ([Patient_PatientID])
    REFERENCES [dbo].[Patients]
        ([PatientID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_PatientVisit'
CREATE INDEX [IX_FK_PatientVisit]
ON [dbo].[Visits]
    ([Patient_PatientID]);
GO

-- Creating foreign key on [Doctor_DoctorID] in table 'Users'
ALTER TABLE [dbo].[Users]
ADD CONSTRAINT [FK_DoctorUser]
    FOREIGN KEY ([Doctor_DoctorID])
    REFERENCES [dbo].[Doctors]
        ([DoctorID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_DoctorUser'
CREATE INDEX [IX_FK_DoctorUser]
ON [dbo].[Users]
    ([Doctor_DoctorID]);
GO

-- Creating foreign key on [Doctor_DoctorID] in table 'Consultations'
ALTER TABLE [dbo].[Consultations]
ADD CONSTRAINT [FK_DoctorConsultation]
    FOREIGN KEY ([Doctor_DoctorID])
    REFERENCES [dbo].[Doctors]
        ([DoctorID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_DoctorConsultation'
CREATE INDEX [IX_FK_DoctorConsultation]
ON [dbo].[Consultations]
    ([Doctor_DoctorID]);
GO

-- Creating foreign key on [Patient_PatientID] in table 'Consultations'
ALTER TABLE [dbo].[Consultations]
ADD CONSTRAINT [FK_PatientConsultation]
    FOREIGN KEY ([Patient_PatientID])
    REFERENCES [dbo].[Patients]
        ([PatientID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_PatientConsultation'
CREATE INDEX [IX_FK_PatientConsultation]
ON [dbo].[Consultations]
    ([Patient_PatientID]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------