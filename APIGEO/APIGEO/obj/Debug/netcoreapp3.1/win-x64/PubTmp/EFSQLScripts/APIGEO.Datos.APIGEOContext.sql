IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201002035557_MigracionInicial')
BEGIN
    CREATE TABLE [Puntos] (
        [Id] int NOT NULL IDENTITY,
        [Calle] nvarchar(max) NULL,
        [Numero] nvarchar(max) NULL,
        [Ciudad] nvarchar(max) NULL,
        [Codigo_postal] nvarchar(max) NULL,
        [Provincia] nvarchar(max) NULL,
        [Pais] nvarchar(max) NULL,
        [Latitud] decimal(18,2) NOT NULL,
        [Longitud] decimal(18,2) NOT NULL,
        [Geolocalizado] bit NOT NULL,
        CONSTRAINT [PK_Puntos] PRIMARY KEY ([Id])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201002035557_MigracionInicial')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20201002035557_MigracionInicial', N'3.1.8');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201002040330_MigracionDecimales')
BEGIN
    DECLARE @var0 sysname;
    SELECT @var0 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Puntos]') AND [c].[name] = N'Longitud');
    IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [Puntos] DROP CONSTRAINT [' + @var0 + '];');
    ALTER TABLE [Puntos] ALTER COLUMN [Longitud] decimal(18,13) NOT NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201002040330_MigracionDecimales')
BEGIN
    DECLARE @var1 sysname;
    SELECT @var1 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Puntos]') AND [c].[name] = N'Latitud');
    IF @var1 IS NOT NULL EXEC(N'ALTER TABLE [Puntos] DROP CONSTRAINT [' + @var1 + '];');
    ALTER TABLE [Puntos] ALTER COLUMN [Latitud] decimal(18,13) NOT NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201002040330_MigracionDecimales')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20201002040330_MigracionDecimales', N'3.1.8');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201003015717_CamposRequeridos')
BEGIN
    DECLARE @var2 sysname;
    SELECT @var2 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Puntos]') AND [c].[name] = N'Provincia');
    IF @var2 IS NOT NULL EXEC(N'ALTER TABLE [Puntos] DROP CONSTRAINT [' + @var2 + '];');
    ALTER TABLE [Puntos] ALTER COLUMN [Provincia] nvarchar(max) NOT NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201003015717_CamposRequeridos')
BEGIN
    DECLARE @var3 sysname;
    SELECT @var3 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Puntos]') AND [c].[name] = N'Pais');
    IF @var3 IS NOT NULL EXEC(N'ALTER TABLE [Puntos] DROP CONSTRAINT [' + @var3 + '];');
    ALTER TABLE [Puntos] ALTER COLUMN [Pais] nvarchar(max) NOT NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201003015717_CamposRequeridos')
BEGIN
    DECLARE @var4 sysname;
    SELECT @var4 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Puntos]') AND [c].[name] = N'Numero');
    IF @var4 IS NOT NULL EXEC(N'ALTER TABLE [Puntos] DROP CONSTRAINT [' + @var4 + '];');
    ALTER TABLE [Puntos] ALTER COLUMN [Numero] nvarchar(max) NOT NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201003015717_CamposRequeridos')
BEGIN
    DECLARE @var5 sysname;
    SELECT @var5 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Puntos]') AND [c].[name] = N'Codigo_postal');
    IF @var5 IS NOT NULL EXEC(N'ALTER TABLE [Puntos] DROP CONSTRAINT [' + @var5 + '];');
    ALTER TABLE [Puntos] ALTER COLUMN [Codigo_postal] nvarchar(max) NOT NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201003015717_CamposRequeridos')
BEGIN
    DECLARE @var6 sysname;
    SELECT @var6 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Puntos]') AND [c].[name] = N'Ciudad');
    IF @var6 IS NOT NULL EXEC(N'ALTER TABLE [Puntos] DROP CONSTRAINT [' + @var6 + '];');
    ALTER TABLE [Puntos] ALTER COLUMN [Ciudad] nvarchar(max) NOT NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201003015717_CamposRequeridos')
BEGIN
    DECLARE @var7 sysname;
    SELECT @var7 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Puntos]') AND [c].[name] = N'Calle');
    IF @var7 IS NOT NULL EXEC(N'ALTER TABLE [Puntos] DROP CONSTRAINT [' + @var7 + '];');
    ALTER TABLE [Puntos] ALTER COLUMN [Calle] nvarchar(max) NOT NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201003015717_CamposRequeridos')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20201003015717_CamposRequeridos', N'3.1.8');
END;

GO

