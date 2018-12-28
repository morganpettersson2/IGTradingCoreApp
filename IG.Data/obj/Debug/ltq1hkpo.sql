IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;

GO

CREATE TABLE [HierarchyNodes] (
    [Id] bigint NOT NULL IDENTITY,
    [Name] nvarchar(max) NULL,
    CONSTRAINT [PK_HierarchyNodes] PRIMARY KEY ([Id])
);

GO

CREATE TABLE [TimeFrames] (
    [Id] int NOT NULL IDENTITY,
    [TimeFrameId] int NOT NULL,
    [Name] nvarchar(max) NULL,
    CONSTRAINT [PK_TimeFrames] PRIMARY KEY ([Id])
);

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20181228084258_Init', N'2.2.0-rtm-35687');

GO

