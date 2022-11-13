IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220930014216_intial')
BEGIN
    CREATE TABLE [Client] (
        [user_id] int NOT NULL IDENTITY,
        [user_name] varchar(max) NOT NULL,
        [user_surname] varchar(max) NOT NULL,
        [user_email] varchar(max) NOT NULL,
        [user_contactdetails] varchar(max) NOT NULL,
        [user_address] varchar(max) NOT NULL,
        [user_province] varchar(max) NOT NULL,
        [user_city] varchar(max) NOT NULL,
        [user_status] varchar(max) NULL,
        [user_password] varchar(max) NOT NULL,
        CONSTRAINT [PK_Client] PRIMARY KEY ([user_id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220930014216_intial')
BEGIN
    CREATE TABLE [Merchant] (
        [merch_id] int NOT NULL IDENTITY,
        [merch_name] varchar(max) NOT NULL,
        [merch_surname] varchar(max) NOT NULL,
        [merch_email] varchar(max) NOT NULL,
        [merch_password] varchar(max) NOT NULL,
        [merch_type] varchar(max) NOT NULL,
        [merch_verify] varchar(max) NULL,
        [merch_status] varchar(max) NULL,
        [merch_address] varchar(max) NOT NULL,
        [merch_city] varchar(max) NOT NULL,
        [merch_province] varchar(max) NOT NULL,
        [merch_contactdetails] varchar(max) NOT NULL,
        [merch_file] varchar(max) NULL,
        [merch_profilepicture] varchar(max) NULL,
        [merch_pictures1] varchar(max) NULL,
        [merch_pictures2] varchar(max) NULL,
        [merch_pictures3] varchar(max) NULL,
        [merch_idnumber] varchar(max) NULL,
        [merch_taxnumber] varchar(max) NULL,
        CONSTRAINT [PK_Merchant] PRIMARY KEY ([merch_id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220930014216_intial')
BEGIN
    CREATE TABLE [Booking] (
        [book_id] int NOT NULL IDENTITY,
        [book_status] varchar(max) NULL,
        [book_date] date NOT NULL,
        [book_time] time NOT NULL,
        [user_id] int NOT NULL,
        [merch_id] int NOT NULL,
        [book_message] varchar(max) NOT NULL,
        CONSTRAINT [PK_Booking] PRIMARY KEY ([book_id]),
        CONSTRAINT [FK_Booking_Client] FOREIGN KEY ([user_id]) REFERENCES [Client] ([user_id]),
        CONSTRAINT [FK_Booking_Merchant] FOREIGN KEY ([merch_id]) REFERENCES [Merchant] ([merch_id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220930014216_intial')
BEGIN
    CREATE TABLE [Job] (
        [job_id] int NOT NULL IDENTITY,
        [job_status] varchar(max) NULL,
        [job_datestart] date NULL,
        [job_timestart] time NULL,
        [job_dateend] date NULL,
        [job_timeend] time NULL,
        [user_id] int NOT NULL,
        [merch_id] int NOT NULL,
        [book_id] int NOT NULL,
        CONSTRAINT [PK_Job] PRIMARY KEY ([job_id]),
        CONSTRAINT [FK_Job_Booking] FOREIGN KEY ([book_id]) REFERENCES [Booking] ([book_id]),
        CONSTRAINT [FK_Job_Client] FOREIGN KEY ([user_id]) REFERENCES [Client] ([user_id]),
        CONSTRAINT [FK_Job_Merchant] FOREIGN KEY ([merch_id]) REFERENCES [Merchant] ([merch_id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220930014216_intial')
BEGIN
    CREATE TABLE [Quotation] (
        [quot_id] int NOT NULL IDENTITY,
        [book_id] int NOT NULL,
        [merch_id] int NOT NULL,
        [user_id] int NOT NULL,
        [quot_amount] varchar(max) NOT NULL,
        [quot_description] varchar(max) NOT NULL,
        CONSTRAINT [PK_Quotation] PRIMARY KEY ([quot_id]),
        CONSTRAINT [FK_Quotation_Booking] FOREIGN KEY ([book_id]) REFERENCES [Booking] ([book_id]),
        CONSTRAINT [FK_Quotation_Client] FOREIGN KEY ([user_id]) REFERENCES [Client] ([user_id]),
        CONSTRAINT [FK_Quotation_Merchant] FOREIGN KEY ([merch_id]) REFERENCES [Merchant] ([merch_id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220930014216_intial')
BEGIN
    CREATE TABLE [Review] (
        [review_id] int NOT NULL IDENTITY,
        [review_rating] varchar(max) NOT NULL,
        [review_message] varchar(max) NOT NULL,
        [user_id] int NOT NULL,
        [merch_id] int NOT NULL,
        [job_id] int NOT NULL,
        CONSTRAINT [PK_Review] PRIMARY KEY ([review_id]),
        CONSTRAINT [FK_Review_Client] FOREIGN KEY ([user_id]) REFERENCES [Client] ([user_id]),
        CONSTRAINT [FK_Review_Job] FOREIGN KEY ([job_id]) REFERENCES [Job] ([job_id]),
        CONSTRAINT [FK_Review_Merchant] FOREIGN KEY ([merch_id]) REFERENCES [Merchant] ([merch_id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220930014216_intial')
BEGIN
    CREATE INDEX [IX_Booking_merch_id] ON [Booking] ([merch_id]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220930014216_intial')
BEGIN
    CREATE INDEX [IX_Booking_user_id] ON [Booking] ([user_id]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220930014216_intial')
BEGIN
    CREATE INDEX [IX_Job_book_id] ON [Job] ([book_id]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220930014216_intial')
BEGIN
    CREATE INDEX [IX_Job_merch_id] ON [Job] ([merch_id]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220930014216_intial')
BEGIN
    CREATE INDEX [IX_Job_user_id] ON [Job] ([user_id]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220930014216_intial')
BEGIN
    CREATE INDEX [IX_Quotation_book_id] ON [Quotation] ([book_id]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220930014216_intial')
BEGIN
    CREATE INDEX [IX_Quotation_merch_id] ON [Quotation] ([merch_id]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220930014216_intial')
BEGIN
    CREATE INDEX [IX_Quotation_user_id] ON [Quotation] ([user_id]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220930014216_intial')
BEGIN
    CREATE INDEX [IX_Review_job_id] ON [Review] ([job_id]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220930014216_intial')
BEGIN
    CREATE INDEX [IX_Review_merch_id] ON [Review] ([merch_id]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220930014216_intial')
BEGIN
    CREATE INDEX [IX_Review_user_id] ON [Review] ([user_id]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220930014216_intial')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20220930014216_intial', N'6.0.9');
END;
GO

COMMIT;
GO

