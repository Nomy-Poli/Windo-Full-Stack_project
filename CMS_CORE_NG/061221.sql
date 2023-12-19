ALTER TABLE [PaidTransactions] ADD [IfDisplayedInCS] bit NULL;

GO

ALTER TABLE [JointProjects] ADD [IfDisplayedInCS] bit NULL;

GO

DECLARE @var0 sysname;
SELECT @var0 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Buisness]') AND [c].[name] = N'userId');
IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [Buisness] DROP CONSTRAINT [' + @var0 + '];');
ALTER TABLE [Buisness] ALTER COLUMN [userId] nvarchar(256) NULL;

GO

ALTER TABLE [BarterDeals] ADD [IfDisplayedInCS] bit NULL;

GO

DECLARE @var1 sysname;
SELECT @var1 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[AspNetUsers]') AND [c].[name] = N'Email');
IF @var1 IS NOT NULL EXEC(N'ALTER TABLE [AspNetUsers] DROP CONSTRAINT [' + @var1 + '];');
ALTER TABLE [AspNetUsers] ALTER COLUMN [Email] nvarchar(256) NOT NULL;

GO

ALTER TABLE [AspNetUsers] ADD CONSTRAINT [AK_AspNetUsers_Email] UNIQUE ([Email]);

GO

CREATE TABLE [CaseStudies] (
    [Id] int NOT NULL IDENTITY,
    [FromTable] int NOT NULL,
    [PaidTransactionID] int NULL,
    [BarterDealID] int NULL,
    [JointProjectID] int NULL,
    [ReportDate] datetime2 NOT NULL,
    [MarketingTitle] nvarchar(max) NULL,
    [BusinessTitle] nvarchar(max) NULL,
    [Description] nvarchar(max) NULL,
    [Challenge] nvarchar(max) NULL,
    [PowerMultiplier] nvarchar(max) NULL,
    [CustomersGain] nvarchar(max) NULL,
    [CustomerResponses] nvarchar(max) NULL,
    CONSTRAINT [PK_CaseStudies] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_CaseStudies_BarterDeals_BarterDealID] FOREIGN KEY ([BarterDealID]) REFERENCES [BarterDeals] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_CaseStudies_JointProjects_JointProjectID] FOREIGN KEY ([JointProjectID]) REFERENCES [JointProjects] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_CaseStudies_PaidTransactions_PaidTransactionID] FOREIGN KEY ([PaidTransactionID]) REFERENCES [PaidTransactions] ([Id]) ON DELETE NO ACTION
);

GO

CREATE TABLE [BusinessesInCaseStudy] (
    [Id] int NOT NULL IDENTITY,
    [CaseStudyId] int NOT NULL,
    [BusinessId] int NOT NULL,
    [BuinessOwnerNameForCS] nvarchar(max) NULL,
    [LineOfBusiness] nvarchar(max) NULL,
    [WordOfPartner] nvarchar(max) NULL,
    CONSTRAINT [PK_BusinessesInCaseStudy] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_BusinessesInCaseStudy_Buisness_BusinessId] FOREIGN KEY ([BusinessId]) REFERENCES [Buisness] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_BusinessesInCaseStudy_CaseStudies_CaseStudyId] FOREIGN KEY ([CaseStudyId]) REFERENCES [CaseStudies] ([Id]) ON DELETE CASCADE
);

GO

CREATE TABLE [CaseStudyPictures] (
    [Id] int NOT NULL IDENTITY,
    [CaseStudyId] int NOT NULL,
    [PicGuid] uniqueidentifier NOT NULL,
    CONSTRAINT [PK_CaseStudyPictures] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_CaseStudyPictures_CaseStudies_CaseStudyId] FOREIGN KEY ([CaseStudyId]) REFERENCES [CaseStudies] ([Id]) ON DELETE CASCADE
);

GO

CREATE INDEX [IX_Buisness_userId] ON [Buisness] ([userId]);

GO

CREATE INDEX [IX_BusinessesInCaseStudy_BusinessId] ON [BusinessesInCaseStudy] ([BusinessId]);

GO

CREATE INDEX [IX_BusinessesInCaseStudy_CaseStudyId] ON [BusinessesInCaseStudy] ([CaseStudyId]);

GO

CREATE INDEX [IX_CaseStudies_BarterDealID] ON [CaseStudies] ([BarterDealID]);

GO

CREATE INDEX [IX_CaseStudies_JointProjectID] ON [CaseStudies] ([JointProjectID]);

GO

CREATE INDEX [IX_CaseStudies_PaidTransactionID] ON [CaseStudies] ([PaidTransactionID]);

GO

CREATE INDEX [IX_CaseStudyPictures_CaseStudyId] ON [CaseStudyPictures] ([CaseStudyId]);

GO

ALTER TABLE [Buisness] ADD CONSTRAINT [FK_Buisness_AspNetUsers_userId] FOREIGN KEY ([userId]) REFERENCES [AspNetUsers] ([Email]) ON DELETE NO ACTION;

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20211110072043_caseStudy', N'3.1.20');

GO

ALTER TABLE [BusinessInCollaborations] DROP CONSTRAINT [FK_BusinessInCollaborations_JointProjects_JointProjectId];

GO

DROP INDEX [IX_BusinessInCollaborations_JointProjectId] ON [BusinessInCollaborations];

GO

DECLARE @var2 sysname;
SELECT @var2 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[BusinessInCollaborations]') AND [c].[name] = N'JointProjectId');
IF @var2 IS NOT NULL EXEC(N'ALTER TABLE [BusinessInCollaborations] DROP CONSTRAINT [' + @var2 + '];');
ALTER TABLE [BusinessInCollaborations] DROP COLUMN [JointProjectId];

GO

CREATE INDEX [IX_BusinessInCollaborations_BusinessId] ON [BusinessInCollaborations] ([BusinessId]);

GO

CREATE INDEX [IX_BusinessInCollaborations_JoinProjectId] ON [BusinessInCollaborations] ([JoinProjectId]);

GO

ALTER TABLE [BusinessInCollaborations] ADD CONSTRAINT [FK_BusinessInCollaborations_Buisness_BusinessId] FOREIGN KEY ([BusinessId]) REFERENCES [Buisness] ([Id]) ON DELETE CASCADE;

GO

ALTER TABLE [BusinessInCollaborations] ADD CONSTRAINT [FK_BusinessInCollaborations_JointProjects_JoinProjectId] FOREIGN KEY ([JoinProjectId]) REFERENCES [JointProjects] ([Id]) ON DELETE CASCADE;

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20211115095533_addRelationships', N'3.1.20');

GO

DECLARE @var3 sysname;
SELECT @var3 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[CaseStudies]') AND [c].[name] = N'CustomerResponses');
IF @var3 IS NOT NULL EXEC(N'ALTER TABLE [CaseStudies] DROP CONSTRAINT [' + @var3 + '];');
ALTER TABLE [CaseStudies] DROP COLUMN [CustomerResponses];

GO

ALTER TABLE [CaseStudyPictures] ADD [IfMainPicture] bit NULL;

GO

CREATE TABLE [CustomerResponses] (
    [Id] int NOT NULL IDENTITY,
    [CustomerName] nvarchar(max) NULL,
    [MinimalDescription] nvarchar(max) NULL,
    [Response] nvarchar(max) NULL,
    [CaseStudyId] int NOT NULL,
    CONSTRAINT [PK_CustomerResponses] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_CustomerResponses_CaseStudies_CaseStudyId] FOREIGN KEY ([CaseStudyId]) REFERENCES [CaseStudies] ([Id]) ON DELETE CASCADE
);

GO

UPDATE [Area] SET [name] = N'אזור המרכז'
WHERE [Id] = 2;
SELECT @@ROWCOUNT;


GO

CREATE INDEX [IX_CustomerResponses_CaseStudyId] ON [CustomerResponses] ([CaseStudyId]);

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20211124123215_addCustomerResponsesTable', N'3.1.20');

GO

DECLARE @var4 sysname;
SELECT @var4 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[CaseStudyPictures]') AND [c].[name] = N'IfMainPicture');
IF @var4 IS NOT NULL EXEC(N'ALTER TABLE [CaseStudyPictures] DROP CONSTRAINT [' + @var4 + '];');
ALTER TABLE [CaseStudyPictures] DROP COLUMN [IfMainPicture];

GO

ALTER TABLE [CaseStudies] ADD [PicGuid] uniqueidentifier NULL;

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20211129105336_changeMainPictureInCS', N'3.1.20');

GO

