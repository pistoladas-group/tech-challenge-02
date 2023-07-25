IF OBJECT_ID(N'[_AppliedMigrations]') IS NULL
BEGIN
    CREATE TABLE [_AppliedMigrations] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK__AppliedMigrations] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [_AppliedMigrations] WHERE [MigrationId] = N'20230725131555_FirstMigration')
BEGIN
    CREATE TABLE [Authors] (
        [Id] uniqueidentifier NOT NULL,
        [Name] VARCHAR(500) NOT NULL,
        [Email] VARCHAR(500) NOT NULL,
        [CreatedAt] datetime2 NOT NULL,
        [IsDeleted] bit NOT NULL,
        CONSTRAINT [PK_Authors] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [_AppliedMigrations] WHERE [MigrationId] = N'20230725131555_FirstMigration')
BEGIN
    CREATE TABLE [News] (
        [Id] uniqueidentifier NOT NULL,
        [Title] VARCHAR(500) NOT NULL,
        [Description] VARCHAR(500) NOT NULL,
        [PublishDate] datetime2 NOT NULL,
        [AuthorId] uniqueidentifier NOT NULL,
        [CreatedAt] datetime2 NOT NULL,
        [IsDeleted] bit NOT NULL,
        CONSTRAINT [PK_News] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_News_Authors_AuthorId] FOREIGN KEY ([AuthorId]) REFERENCES [Authors] ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [_AppliedMigrations] WHERE [MigrationId] = N'20230725131555_FirstMigration')
BEGIN
    CREATE INDEX [IX_News_AuthorId] ON [News] ([AuthorId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [_AppliedMigrations] WHERE [MigrationId] = N'20230725131555_FirstMigration')
BEGIN
    INSERT INTO [_AppliedMigrations] ([MigrationId], [ProductVersion])
    VALUES (N'20230725131555_FirstMigration', N'7.0.3');
END;
GO

COMMIT;
GO

