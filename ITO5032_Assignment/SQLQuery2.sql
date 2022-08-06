﻿
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 08/06/2022 18:23:00
-- Generated from EDMX file: C:\Users\danie\source\repos\ITO5032_Assignment\ITO5032_Assignment\ITO5032_Assignment\Models\Model1.edmx
-- --------------------------------------------------



-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------


-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Notifications]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Notifications];
GO
IF OBJECT_ID(N'[dbo].[Files]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Files];
GO
IF OBJECT_ID(N'[dbo].[Users]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Users];
GO
IF OBJECT_ID(N'[dbo].[Bookables]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Bookables];
GO
IF OBJECT_ID(N'[dbo].[Bookings]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Bookings];
GO
IF OBJECT_ID(N'[dbo].[Ratings]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Ratings];
GO
IF OBJECT_ID(N'[dbo].[Locations]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Locations];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Notifications'
CREATE TABLE [dbo].[Notifications] (
    [id] int IDENTITY(1,1) NOT NULL,
    [message] nvarchar(max)  NOT NULL,
    [user_id] int  NOT NULL,
    [notification_time] time  NOT NULL,
    [notification_date] datetime  NOT NULL
);
GO

-- Creating table 'Files'
CREATE TABLE [dbo].[Files] (
    [id] int IDENTITY(1,1) NOT NULL,
    [file_name] nvarchar(max)  NOT NULL,
    [file_location] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Users'
CREATE TABLE [dbo].[Users] (
    [id] int IDENTITY(1,1) NOT NULL,
    [role_id] int  NOT NULL,
    [first_name] nvarchar(max)  NOT NULL,
    [last_name] nvarchar(max)  NOT NULL,
    [date_of_birth] datetime  NOT NULL,
    [username] nvarchar(max)  NOT NULL,
    [password] nvarchar(max)  NOT NULL,
    [salt] nvarchar(max)  NOT NULL,
    [address1] nvarchar(max)  NOT NULL,
    [address2] nvarchar(max)  NOT NULL,
    [email] nvarchar(max)  NOT NULL,
    [external_id] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Bookables'
CREATE TABLE [dbo].[Bookables] (
    [id] int IDENTITY(1,1) NOT NULL,
    [name] nvarchar(max)  NOT NULL,
    [description] nvarchar(max)  NOT NULL,
    [available_day] smallint  NOT NULL,
    [available_start_time] time  NOT NULL,
    [available_end_time] time  NOT NULL,
    [max_occupancy] smallint  NOT NULL,
    [booking_type] smallint  NOT NULL,
    [location_id] int  NOT NULL
);
GO

-- Creating table 'Bookings'
CREATE TABLE [dbo].[Bookings] (
    [id] int IDENTITY(1,1) NOT NULL,
    [bookable_id] int  NOT NULL,
    [user_id] int  NOT NULL,
    [start_datetime] nvarchar(max)  NOT NULL,
    [end_datetime] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Ratings'
CREATE TABLE [dbo].[Ratings] (
    [id] int IDENTITY(1,1) NOT NULL,
    [score] smallint  NOT NULL,
    [comment] nvarchar(max)  NOT NULL,
    [user_id] int  NOT NULL,
    [service_provider_id] int  NOT NULL
);
GO

-- Creating table 'Locations'
CREATE TABLE [dbo].[Locations] (
    [id] int IDENTITY(1,1) NOT NULL,
    [address1] nvarchar(max)  NOT NULL,
    [address2] nvarchar(max)  NOT NULL,
    [room] nvarchar(max)  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [id] in table 'Notifications'
ALTER TABLE [dbo].[Notifications]
ADD CONSTRAINT [PK_Notifications]
    PRIMARY KEY CLUSTERED ([id] ASC);
GO

-- Creating primary key on [id] in table 'Files'
ALTER TABLE [dbo].[Files]
ADD CONSTRAINT [PK_Files]
    PRIMARY KEY CLUSTERED ([id] ASC);
GO

-- Creating primary key on [id] in table 'Users'
ALTER TABLE [dbo].[Users]
ADD CONSTRAINT [PK_Users]
    PRIMARY KEY CLUSTERED ([id] ASC);
GO

-- Creating primary key on [id] in table 'Bookables'
ALTER TABLE [dbo].[Bookables]
ADD CONSTRAINT [PK_Bookables]
    PRIMARY KEY CLUSTERED ([id] ASC);
GO

-- Creating primary key on [id] in table 'Bookings'
ALTER TABLE [dbo].[Bookings]
ADD CONSTRAINT [PK_Bookings]
    PRIMARY KEY CLUSTERED ([id] ASC);
GO

-- Creating primary key on [id] in table 'Ratings'
ALTER TABLE [dbo].[Ratings]
ADD CONSTRAINT [PK_Ratings]
    PRIMARY KEY CLUSTERED ([id] ASC);
GO

-- Creating primary key on [id] in table 'Locations'
ALTER TABLE [dbo].[Locations]
ADD CONSTRAINT [PK_Locations]
    PRIMARY KEY CLUSTERED ([id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [user_id] in table 'Notifications'
ALTER TABLE [dbo].[Notifications]
ADD CONSTRAINT [FK_UserNotification]
    FOREIGN KEY ([user_id])
    REFERENCES [dbo].[Users]
        ([id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_UserNotification'
CREATE INDEX [IX_FK_UserNotification]
ON [dbo].[Notifications]
    ([user_id]);
GO

-- Creating foreign key on [user_id] in table 'Bookings'
ALTER TABLE [dbo].[Bookings]
ADD CONSTRAINT [FK_UserBooking]
    FOREIGN KEY ([user_id])
    REFERENCES [dbo].[Users]
        ([id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_UserBooking'
CREATE INDEX [IX_FK_UserBooking]
ON [dbo].[Bookings]
    ([user_id]);
GO

-- Creating foreign key on [bookable_id] in table 'Bookings'
ALTER TABLE [dbo].[Bookings]
ADD CONSTRAINT [FK_BookingBookable]
    FOREIGN KEY ([bookable_id])
    REFERENCES [dbo].[Bookables]
        ([id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_BookingBookable'
CREATE INDEX [IX_FK_BookingBookable]
ON [dbo].[Bookings]
    ([bookable_id]);
GO

-- Creating foreign key on [location_id] in table 'Bookables'
ALTER TABLE [dbo].[Bookables]
ADD CONSTRAINT [FK_BookableLocation]
    FOREIGN KEY ([location_id])
    REFERENCES [dbo].[Locations]
        ([id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_BookableLocation'
CREATE INDEX [IX_FK_BookableLocation]
ON [dbo].[Bookables]
    ([location_id]);
GO

-- Creating foreign key on [user_id] in table 'Ratings'
ALTER TABLE [dbo].[Ratings]
ADD CONSTRAINT [FK_RatingUser]
    FOREIGN KEY ([user_id])
    REFERENCES [dbo].[Users]
        ([id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_RatingUser'
CREATE INDEX [IX_FK_RatingUser]
ON [dbo].[Ratings]
    ([user_id]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------