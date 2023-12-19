IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;

GO

CREATE TABLE [Activities] (
    [Id] int NOT NULL IDENTITY,
    [Type] nvarchar(max) NULL,
    [IpAddress] nvarchar(max) NULL,
    [Location] nvarchar(max) NULL,
    [OperatingSystem] nvarchar(max) NULL,
    [Date] datetime2 NOT NULL,
    [UserId] nvarchar(max) NULL,
    [Color] nvarchar(max) NULL,
    [Icon] nvarchar(max) NULL,
    CONSTRAINT [PK_Activities] PRIMARY KEY ([Id])
);

GO

CREATE TABLE [Area] (
    [Id] int NOT NULL IDENTITY,
    [name] nvarchar(max) NULL,
    CONSTRAINT [PK_Area] PRIMARY KEY ([Id])
);

GO

CREATE TABLE [AspNetRoles] (
    [Id] nvarchar(450) NOT NULL,
    [Name] nvarchar(256) NULL,
    [NormalizedName] nvarchar(256) NULL,
    [ConcurrencyStamp] nvarchar(max) NULL,
    [Discriminator] nvarchar(max) NOT NULL,
    [RoleName] nvarchar(max) NULL,
    [RoleIcon] nvarchar(max) NULL,
    [Handle] nvarchar(max) NULL,
    [IsActive] bit NULL,
    CONSTRAINT [PK_AspNetRoles] PRIMARY KEY ([Id])
);

GO

CREATE TABLE [AspNetUsers] (
    [Id] nvarchar(450) NOT NULL,
    [UserName] nvarchar(256) NULL,
    [NormalizedUserName] nvarchar(256) NULL,
    [Email] nvarchar(256) NULL,
    [NormalizedEmail] nvarchar(256) NULL,
    [EmailConfirmed] bit NOT NULL,
    [PasswordHash] nvarchar(max) NULL,
    [SecurityStamp] nvarchar(max) NULL,
    [ConcurrencyStamp] nvarchar(max) NULL,
    [PhoneNumber] nvarchar(max) NULL,
    [PhoneNumberConfirmed] bit NOT NULL,
    [TwoFactorEnabled] bit NOT NULL,
    [LockoutEnd] datetimeoffset NULL,
    [LockoutEnabled] bit NOT NULL,
    [AccessFailedCount] int NOT NULL,
    [Notes] nvarchar(max) NULL,
    [DisplayName] nvarchar(max) NULL,
    [Firstname] nvarchar(max) NULL,
    [Middlename] nvarchar(max) NULL,
    [Lastname] nvarchar(max) NULL,
    [Gender] nvarchar(max) NULL,
    [ProfilePic] nvarchar(max) NULL,
    [Birthday] nvarchar(max) NULL,
    [IsProfileComplete] bit NOT NULL,
    [Terms] bit NOT NULL,
    [IsEmployee] bit NOT NULL,
    [UserRole] nvarchar(max) NULL,
    [AccountCreatedOn] datetime2 NOT NULL,
    [RememberMe] bit NOT NULL,
    [IsActive] bit NOT NULL,
    CONSTRAINT [PK_AspNetUsers] PRIMARY KEY ([Id])
);

GO

CREATE TABLE [BarterDeals] (
    [Id] int NOT NULL IDENTITY,
    [ReportsBusinessId] int NOT NULL,
    [PartnerBusinessId] int NOT NULL,
    [ReportDate] datetime2 NOT NULL,
    [ReportCategorySubCategoryId] int NOT NULL,
    [ReportDescriptionDeal] nvarchar(max) NULL,
    [PartnerCategorySubCategoryId] int NOT NULL,
    [PartnerDescriptionDeal] nvarchar(max) NULL,
    [BusinessDescription] nvarchar(max) NULL,
    [QuotePartnerBusiness] nvarchar(max) NULL,
    [QuoteReportsBusiness] nvarchar(max) NULL,
    [ReportsBusinessPictureID] uniqueidentifier NULL,
    [PartnerBusinessPictureID] uniqueidentifier NULL,
    [JointExplanation] nvarchar(max) NULL,
    [ConfirmedByPartner] bit NOT NULL,
    [Availability] bit NULL,
    [Service] bit NULL,
    [Professionalism] bit NULL,
    [FairConsiderationForTransaction] bit NULL,
    CONSTRAINT [PK_BarterDeals] PRIMARY KEY ([Id])
);

GO

CREATE TABLE [Category] (
    [Id] int NOT NULL IDENTITY,
    [name] nvarchar(max) NULL,
    CONSTRAINT [PK_Category] PRIMARY KEY ([Id])
);

GO

CREATE TABLE [CollaborationTypes] (
    [Id] int NOT NULL IDENTITY,
    [Description] nvarchar(max) NULL,
    CONSTRAINT [PK_CollaborationTypes] PRIMARY KEY ([Id])
);

GO

CREATE TABLE [Contact] (
    [id] int NOT NULL IDENTITY,
    [name] nvarchar(max) NULL,
    [email] nvarchar(max) NULL,
    [message] nvarchar(max) NULL,
    [phoneNumber] nvarchar(max) NULL,
    CONSTRAINT [PK_Contact] PRIMARY KEY ([id])
);

GO

CREATE TABLE [Countries] (
    [Id] int NOT NULL IDENTITY,
    [CountryId] int NOT NULL,
    [TwoDigitCode] nvarchar(max) NULL,
    [Name] nvarchar(max) NULL,
    [PhoneCode] nvarchar(max) NULL,
    [Flag] nvarchar(max) NULL,
    [States] nvarchar(max) NULL,
    CONSTRAINT [PK_Countries] PRIMARY KEY ([Id])
);

GO

CREATE TABLE [PaidTransactions] (
    [Id] int NOT NULL IDENTITY,
    [SupplierBusinessId] int NOT NULL,
    [ConsumerBusinessId] int NOT NULL,
    [CategorySubCategoryId] int NOT NULL,
    [Description] nvarchar(max) NULL,
    [Review] nvarchar(max) NULL,
    [ScopTransactionNIS] int NULL,
    [PictureID] uniqueidentifier NULL,
    [Availability] bit NULL,
    [Service] bit NULL,
    [Professionalism] bit NULL,
    [Price] bit NULL,
    CONSTRAINT [PK_PaidTransactions] PRIMARY KEY ([Id])
);

GO

CREATE TABLE [PermissionTypes] (
    [Id] int NOT NULL IDENTITY,
    [Type] nvarchar(max) NULL,
    CONSTRAINT [PK_PermissionTypes] PRIMARY KEY ([Id])
);

GO

CREATE TABLE [StaticSiteComponents] (
    [Id] int NOT NULL IDENTITY,
    [url] nvarchar(max) NULL,
    [type] nvarchar(max) NULL,
    CONSTRAINT [PK_StaticSiteComponents] PRIMARY KEY ([Id])
);

GO

CREATE TABLE [Status] (
    [Id] int NOT NULL IDENTITY,
    [name] nvarchar(max) NULL,
    CONSTRAINT [PK_Status] PRIMARY KEY ([Id])
);

GO

CREATE TABLE [SubCategory] (
    [Id] int NOT NULL IDENTITY,
    [name] nvarchar(max) NULL,
    CONSTRAINT [PK_SubCategory] PRIMARY KEY ([Id])
);

GO

CREATE TABLE [TwoFactorCodes] (
    [Id] int NOT NULL IDENTITY,
    [TwoFactorCode] nvarchar(max) NOT NULL,
    [RememberDevice] bit NOT NULL,
    [SelectedProvider] nvarchar(max) NOT NULL,
    [UserId] nvarchar(max) NULL,
    [ExpiryDate] datetime2 NOT NULL,
    [CreatedDate] datetime2 NOT NULL,
    [EncryptionKey2Fa] nvarchar(max) NOT NULL,
    [Token] nvarchar(max) NOT NULL,
    [DeviceId] nvarchar(max) NOT NULL,
    [Attempts] int NOT NULL,
    [IpAddress] nvarchar(max) NOT NULL,
    [CodeExpired] bit NOT NULL,
    [CodeIsUsed] bit NOT NULL,
    [UserAgent] nvarchar(max) NOT NULL,
    [EncryptionKeyForDeviceId] nvarchar(max) NOT NULL,
    [DeviceIdExpiry] datetime2 NOT NULL,
    [IsDeviceIdExpired] bit NOT NULL,
    CONSTRAINT [PK_TwoFactorCodes] PRIMARY KEY ([Id])
);

GO

CREATE TABLE [UploadActivitiesHistory] (
    [ImportId] int NOT NULL IDENTITY,
    [ImporterName] nvarchar(max) NULL,
    [ImportFileName] nvarchar(max) NOT NULL,
    [TotalRecords] int NOT NULL,
    [ColumnsFound] int NOT NULL,
    [LoadedRecords] int NOT NULL,
    [DeletedRecords] int NOT NULL,
    [MarkForDelete] int NOT NULL,
    [MarkForNew] int NOT NULL,
    [NewLoaded] int NOT NULL,
    [ErroredRecords] int NOT NULL,
    [NotUpdated] int NOT NULL,
    [NewRecordsNotInDataBase] int NOT NULL,
    [StartTime] datetime2 NOT NULL,
    [EndTime] datetime2 NOT NULL,
    [Added] int NOT NULL,
    [Deleted] int NOT NULL,
    CONSTRAINT [PK_UploadActivitiesHistory] PRIMARY KEY ([ImportId])
);

GO

CREATE TABLE [AspNetRoleClaims] (
    [Id] int NOT NULL IDENTITY,
    [RoleId] nvarchar(450) NOT NULL,
    [ClaimType] nvarchar(max) NULL,
    [ClaimValue] nvarchar(max) NULL,
    CONSTRAINT [PK_AspNetRoleClaims] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_AspNetRoleClaims_AspNetRoles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [AspNetRoles] ([Id]) ON DELETE CASCADE
);

GO

CREATE TABLE [RolePermissions] (
    [Id] int NOT NULL IDENTITY,
    [Read] bit NOT NULL,
    [Delete] bit NOT NULL,
    [Update] bit NOT NULL,
    [Add] bit NOT NULL,
    [Type] nvarchar(max) NULL,
    [ApplicationRoleId] nvarchar(450) NULL,
    CONSTRAINT [PK_RolePermissions] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_RolePermissions_AspNetRoles_ApplicationRoleId] FOREIGN KEY ([ApplicationRoleId]) REFERENCES [AspNetRoles] ([Id]) ON DELETE NO ACTION
);

GO

CREATE TABLE [Addresses] (
    [AddressId] int NOT NULL IDENTITY,
    [Line1] nvarchar(max) NULL,
    [Line2] nvarchar(max) NULL,
    [Unit] nvarchar(max) NULL,
    [Country] nvarchar(max) NOT NULL,
    [State] nvarchar(max) NULL,
    [City] nvarchar(max) NULL,
    [PostalCode] nvarchar(max) NULL,
    [Type] nvarchar(max) NULL,
    [UserId] nvarchar(450) NULL,
    CONSTRAINT [PK_Addresses] PRIMARY KEY ([AddressId]),
    CONSTRAINT [FK_Addresses_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE NO ACTION
);

GO

CREATE TABLE [AspNetUserClaims] (
    [Id] int NOT NULL IDENTITY,
    [UserId] nvarchar(450) NOT NULL,
    [ClaimType] nvarchar(max) NULL,
    [ClaimValue] nvarchar(max) NULL,
    CONSTRAINT [PK_AspNetUserClaims] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_AspNetUserClaims_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
);

GO

CREATE TABLE [AspNetUserLogins] (
    [LoginProvider] nvarchar(450) NOT NULL,
    [ProviderKey] nvarchar(450) NOT NULL,
    [ProviderDisplayName] nvarchar(max) NULL,
    [UserId] nvarchar(450) NOT NULL,
    CONSTRAINT [PK_AspNetUserLogins] PRIMARY KEY ([LoginProvider], [ProviderKey]),
    CONSTRAINT [FK_AspNetUserLogins_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
);

GO

CREATE TABLE [AspNetUserRoles] (
    [UserId] nvarchar(450) NOT NULL,
    [RoleId] nvarchar(450) NOT NULL,
    CONSTRAINT [PK_AspNetUserRoles] PRIMARY KEY ([UserId], [RoleId]),
    CONSTRAINT [FK_AspNetUserRoles_AspNetRoles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [AspNetRoles] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_AspNetUserRoles_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
);

GO

CREATE TABLE [AspNetUserTokens] (
    [UserId] nvarchar(450) NOT NULL,
    [LoginProvider] nvarchar(450) NOT NULL,
    [Name] nvarchar(450) NOT NULL,
    [Value] nvarchar(max) NULL,
    CONSTRAINT [PK_AspNetUserTokens] PRIMARY KEY ([UserId], [LoginProvider], [Name]),
    CONSTRAINT [FK_AspNetUserTokens_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
);

GO

CREATE TABLE [Tokens] (
    [Id] int NOT NULL IDENTITY,
    [ClientId] nvarchar(max) NOT NULL,
    [Value] nvarchar(max) NOT NULL,
    [CreatedDate] datetime2 NOT NULL,
    [UserId] nvarchar(450) NOT NULL,
    [LastModifiedDate] datetime2 NOT NULL,
    [ExpiryTime] datetime2 NOT NULL,
    [EncryptionKeyRt] nvarchar(max) NOT NULL,
    [EncryptionKeyJwt] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_Tokens] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Tokens_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
);

GO

CREATE TABLE [JointProjects] (
    [Id] int NOT NULL IDENTITY,
    [CollaborationTypeId] int NOT NULL,
    [ReportDate] datetime2 NOT NULL,
    [HeaderCollaboration] nvarchar(max) NULL,
    [JointExplanation] nvarchar(max) NULL,
    [PictureId] uniqueidentifier NULL,
    [Enterprise] bit NULL,
    [Creativity] bit NULL,
    [Professionalism] bit NULL,
    [ExposureToNewAudiences] bit NULL,
    [ConfirmedByPartners] bit NULL,
    CONSTRAINT [PK_JointProjects] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_JointProjects_CollaborationTypes_CollaborationTypeId] FOREIGN KEY ([CollaborationTypeId]) REFERENCES [CollaborationTypes] ([Id]) ON DELETE CASCADE
);

GO

CREATE TABLE [Buisness] (
    [Id] int NOT NULL IDENTITY,
    [userId] nvarchar(max) NULL,
    [businessEmailAddress] nvarchar(max) NULL,
    [buisnessName] nvarchar(max) NULL,
    [phoneNumber1] nvarchar(max) NULL,
    [phoneNumber2] nvarchar(max) NULL,
    [address] nvarchar(max) NULL,
    [actionDiscription] nvarchar(max) NULL,
    [discription] nvarchar(max) NULL,
    [buisnessWebSiteLink] nvarchar(max) NULL,
    [isdisplayBusinessOwnerName] bit NULL,
    [ispayingBuisness] bit NULL,
    [isburterBuisness] bit NULL,
    [iscollaborationBuisness] bit NULL,
    [isburterPossibleInAllCategory] bit NULL,
    [isopenToSuggestionsForBarter] bit NULL,
    [coverPictureId] uniqueidentifier NOT NULL,
    [logoPictureId] uniqueidentifier NOT NULL,
    [product1] nvarchar(max) NULL,
    [product2] nvarchar(max) NULL,
    [barterProduct1] nvarchar(max) NULL,
    [barterProduct2] nvarchar(max) NULL,
    [UpdatedBusinessStatus] int NULL,
    [views] int NULL,
    [tags] nvarchar(max) NULL,
    CONSTRAINT [PK_Buisness] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Buisness_Status_UpdatedBusinessStatus] FOREIGN KEY ([UpdatedBusinessStatus]) REFERENCES [Status] ([Id]) ON DELETE NO ACTION
);

GO

CREATE TABLE [CategorySubCategory] (
    [Id] int NOT NULL IDENTITY,
    [categoryId] int NOT NULL,
    [subCategoryId] int NOT NULL,
    CONSTRAINT [PK_CategorySubCategory] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_CategorySubCategory_Category_categoryId] FOREIGN KEY ([categoryId]) REFERENCES [Category] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_CategorySubCategory_SubCategory_subCategoryId] FOREIGN KEY ([subCategoryId]) REFERENCES [SubCategory] ([Id]) ON DELETE CASCADE
);

GO

CREATE TABLE [BusinessInCollaborations] (
    [Id] int NOT NULL IDENTITY,
    [BusinessId] int NOT NULL,
    [JoinProjectId] int NOT NULL,
    [PartInCollaboration] nvarchar(max) NULL,
    [IfReport] bit NULL,
    [JointProjectId] int NULL,
    CONSTRAINT [PK_BusinessInCollaborations] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_BusinessInCollaborations_JointProjects_JointProjectId] FOREIGN KEY ([JointProjectId]) REFERENCES [JointProjects] ([Id]) ON DELETE NO ACTION
);

GO

CREATE TABLE [BuisnessArea] (
    [Id] int NOT NULL IDENTITY,
    [buisnessId] int NOT NULL,
    [areaId] int NOT NULL,
    CONSTRAINT [PK_BuisnessArea] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_BuisnessArea_Area_areaId] FOREIGN KEY ([areaId]) REFERENCES [Area] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_BuisnessArea_Buisness_buisnessId] FOREIGN KEY ([buisnessId]) REFERENCES [Buisness] ([Id]) ON DELETE CASCADE
);

GO

CREATE TABLE [BuisnessPicture] (
    [buisnessPictureId] uniqueidentifier NOT NULL,
    [buisnessId] int NOT NULL,
    [numberOfPicture] int NOT NULL,
    CONSTRAINT [PK_BuisnessPicture] PRIMARY KEY ([buisnessPictureId]),
    CONSTRAINT [FK_BuisnessPicture_Buisness_buisnessId] FOREIGN KEY ([buisnessId]) REFERENCES [Buisness] ([Id]) ON DELETE CASCADE
);

GO

CREATE TABLE [BuisnessStatus] (
    [Id] int NOT NULL IDENTITY,
    [buisnessId] int NOT NULL,
    [statusId] int NOT NULL,
    [startDate] datetime2 NULL,
    [endDate] datetime2 NULL,
    CONSTRAINT [PK_BuisnessStatus] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_BuisnessStatus_Buisness_buisnessId] FOREIGN KEY ([buisnessId]) REFERENCES [Buisness] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_BuisnessStatus_Status_statusId] FOREIGN KEY ([statusId]) REFERENCES [Status] ([Id]) ON DELETE CASCADE
);

GO

CREATE TABLE [BuisnessSubCategory] (
    [Id] int NOT NULL IDENTITY,
    [buisnessId] int NOT NULL,
    [categorySubCategoryId] int NOT NULL,
    [isPossibleInBarter] bit NOT NULL,
    CONSTRAINT [PK_BuisnessSubCategory] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_BuisnessSubCategory_Buisness_buisnessId] FOREIGN KEY ([buisnessId]) REFERENCES [Buisness] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_BuisnessSubCategory_CategorySubCategory_categorySubCategoryId] FOREIGN KEY ([categorySubCategoryId]) REFERENCES [CategorySubCategory] ([Id]) ON DELETE CASCADE
);

GO

CREATE TABLE [BuisnessSubCategoryBarter] (
    [Id] int NOT NULL IDENTITY,
    [buisnessId] int NOT NULL,
    [categorySubCategoryId] int NOT NULL,
    CONSTRAINT [PK_BuisnessSubCategoryBarter] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_BuisnessSubCategoryBarter_Buisness_buisnessId] FOREIGN KEY ([buisnessId]) REFERENCES [Buisness] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_BuisnessSubCategoryBarter_CategorySubCategory_categorySubCategoryId] FOREIGN KEY ([categorySubCategoryId]) REFERENCES [CategorySubCategory] ([Id]) ON DELETE CASCADE
);

GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'name') AND [object_id] = OBJECT_ID(N'[Area]'))
    SET IDENTITY_INSERT [Area] ON;
INSERT INTO [Area] ([Id], [name])
VALUES (1, N'כל הארץ'),
(2, N'איזור המרכז'),
(3, N'ירושלים והסביבה'),
(4, N'צפון'),
(5, N'דרום');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'name') AND [object_id] = OBJECT_ID(N'[Area]'))
    SET IDENTITY_INSERT [Area] OFF;

GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'ConcurrencyStamp', N'Discriminator', N'Name', N'NormalizedName') AND [object_id] = OBJECT_ID(N'[AspNetRoles]'))
    SET IDENTITY_INSERT [AspNetRoles] ON;
INSERT INTO [AspNetRoles] ([Id], [ConcurrencyStamp], [Discriminator], [Name], [NormalizedName])
VALUES (N'1', NULL, N'IdentityRole', N'Administrator', N'ADMINISTRATOR'),
(N'2', NULL, N'IdentityRole', N'Customer', N'CUSTOMER');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'ConcurrencyStamp', N'Discriminator', N'Name', N'NormalizedName') AND [object_id] = OBJECT_ID(N'[AspNetRoles]'))
    SET IDENTITY_INSERT [AspNetRoles] OFF;

GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'UpdatedBusinessStatus', N'actionDiscription', N'address', N'barterProduct1', N'barterProduct2', N'buisnessName', N'buisnessWebSiteLink', N'businessEmailAddress', N'coverPictureId', N'discription', N'isburterBuisness', N'isburterPossibleInAllCategory', N'iscollaborationBuisness', N'isdisplayBusinessOwnerName', N'isopenToSuggestionsForBarter', N'ispayingBuisness', N'logoPictureId', N'phoneNumber1', N'phoneNumber2', N'product1', N'product2', N'tags', N'userId', N'views') AND [object_id] = OBJECT_ID(N'[Buisness]'))
    SET IDENTITY_INSERT [Buisness] ON;
INSERT INTO [Buisness] ([Id], [UpdatedBusinessStatus], [actionDiscription], [address], [barterProduct1], [barterProduct2], [buisnessName], [buisnessWebSiteLink], [businessEmailAddress], [coverPictureId], [discription], [isburterBuisness], [isburterPossibleInAllCategory], [iscollaborationBuisness], [isdisplayBusinessOwnerName], [isopenToSuggestionsForBarter], [ispayingBuisness], [logoPictureId], [phoneNumber1], [phoneNumber2], [product1], [product2], [tags], [userId], [views])
VALUES (1, NULL, N'העסק שלך זה האליפות שלנו', N'הארי 15', N'logo barter', N'כרטיס ביקור barter', N'busoft', N'https://uriba.visualstudio.com/WINDO/_sprints/backlog/WINDO%20Team/WINDO/sprint%201', NULL, '00000000-0000-0000-0000-000000000000', N'העסק שלך זהs fdghjkl dfhjk sdfghj dcfghjk ertyui oxcvbnm dfghj  האליפות שלנו', CAST(1 AS bit), CAST(1 AS bit), CAST(1 AS bit), CAST(1 AS bit), CAST(1 AS bit), CAST(1 AS bit), '00000000-0000-0000-0000-000000000000', N'0523152114', N'0523152115', N'logo', N'כרטיס ביקור', N'', N'39a5e42aab68', 5),
(2, NULL, N'קידום והעסקה', N'שרי ישראל 15', N'logo657 barter', N'56756כרטיס ביקור barter', N'תמך', N'https://uriba.visualstudio.com/WINDO/_sprints/backlog/WINDO%20Team/WINDO/sprint%201', NULL, '00000000-0000-0000-0000-000000000000', N'עמכעדחלחךיל כעיחל כעיחל גכעטו ןראטו ןבהנמ גכעיח ו', CAST(1 AS bit), CAST(1 AS bit), CAST(1 AS bit), CAST(1 AS bit), CAST(1 AS bit), CAST(1 AS bit), '00000000-0000-0000-0000-000000000000', N'0523152114', N'0523152115', N'logo657567', N'5675כרטיס ביקור', N'', N'39a5e42aab68', 5);
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'UpdatedBusinessStatus', N'actionDiscription', N'address', N'barterProduct1', N'barterProduct2', N'buisnessName', N'buisnessWebSiteLink', N'businessEmailAddress', N'coverPictureId', N'discription', N'isburterBuisness', N'isburterPossibleInAllCategory', N'iscollaborationBuisness', N'isdisplayBusinessOwnerName', N'isopenToSuggestionsForBarter', N'ispayingBuisness', N'logoPictureId', N'phoneNumber1', N'phoneNumber2', N'product1', N'product2', N'tags', N'userId', N'views') AND [object_id] = OBJECT_ID(N'[Buisness]'))
    SET IDENTITY_INSERT [Buisness] OFF;

GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'name') AND [object_id] = OBJECT_ID(N'[Category]'))
    SET IDENTITY_INSERT [Category] ON;
INSERT INTO [Category] ([Id], [name])
VALUES (10, N'אדריכלות ועיצוב פנים'),
(11, N'הפקות ואירועים'),
(12, N'שירותי כח אדם'),
(13, N'טיפוח אישי ויופי'),
(14, N'קייטרינג'),
(19, N'משפט'),
(16, N'חנויות ומכירות'),
(17, N'ביגוד ואופנה'),
(9, N'מרצים- בתחומי'),
(20, N'btb שירותים לעסק'),
(15, N'טיפול וייעוץ'),
(8, N'אדמנסטרציה'),
(18, N'פיננסים'),
(6, N'שיווק ומכירות'),
(5, N'תרגום'),
(4, N'כתיבה ועריכה'),
(3, N'טכנולוגיה ופיתוח תוכנה '),
(2, N'עיצוב וגרפיקה'),
(1, N'בניית אתרים'),
(7, N'צילום וודיאו');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'name') AND [object_id] = OBJECT_ID(N'[Category]'))
    SET IDENTITY_INSERT [Category] OFF;

GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Description') AND [object_id] = OBJECT_ID(N'[CollaborationTypes]'))
    SET IDENTITY_INSERT [CollaborationTypes] ON;
INSERT INTO [CollaborationTypes] ([Id], [Description])
VALUES (1, N'מיזם משותף'),
(2, N'מוצר משותף'),
(4, N'השקעה ושותפות'),
(3, N'שיווק הדדי');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Description') AND [object_id] = OBJECT_ID(N'[CollaborationTypes]'))
    SET IDENTITY_INSERT [CollaborationTypes] OFF;

GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'name') AND [object_id] = OBJECT_ID(N'[Status]'))
    SET IDENTITY_INSERT [Status] ON;
INSERT INTO [Status] ([Id], [name])
VALUES (1, N'עסק רשום'),
(2, N'עסק מאושר');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'name') AND [object_id] = OBJECT_ID(N'[Status]'))
    SET IDENTITY_INSERT [Status] OFF;

GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'name') AND [object_id] = OBJECT_ID(N'[SubCategory]'))
    SET IDENTITY_INSERT [SubCategory] ON;
INSERT INTO [SubCategory] ([Id], [name])
VALUES (82, N'אומנות המזון'),
(85, N'עיצוב פנים'),
(83, N'עיצוב ותכנון'),
(84, N'רישוי'),
(86, N'הום סטיילינג'),
(92, N'השמה'),
(88, N'תכנון כנסים'),
(89, N'הפקת אירועים'),
(90, N'עיצוב אירועים'),
(91, N'שיווק וייצוג אומנים'),
(81, N'אימון וטיפול'),
(93, N'ייעוץ/ אבחון תעסוקתי'),
(87, N'CAD, AutoCAD, שרטוט אדריכלי'),
(80, N'בריאות'),
(74, N'שירות לקוחות'),
(78, N'חינוך'),
(77, N'תעסוקה ועסקים'),
(76, N'תיאום פגישות'),
(75, N'ניהול משרד'),
(73, N'שירותי גבייה'),
(72, N'מזכירות'),
(71, N'רכש'),
(70, N'עזרה אדמינסרטיבית'),
(69, N'קלדנות'),
(68, N'צילום אופנה'),
(67, N'צילום אדריכלות ועיצוב פנים'),
(66, N'צילום משפחות'),
(94, N'סטיילינג'),
(79, N'טכנולוגיה ומחשבים'),
(95, N'קוסמטיקה'),
(113, N'גרביים'),
(97, N'עיצוב פאה'),
(126, N'עריכת דין- עריכת הסכמים'),
(125, N'חשבות שכר'),
(124, N'הנהלת חשבונות'),
(123, N'הגשת דוחות'),
(122, N'ראיית חשבון'),
(121, N'ייעוץ מס'),
(120, N'תחתיות לאירועים'),
(119, N'שמלות כלה'),
(118, N'תפירת בגדי אירועים'),
(117, N'השכרת בגדי אירועים'),
(116, N'עיצוב אופנה'),
(115, N'עדשות מגע'),
(114, N'מוצרי אפייה'),
(65, N'צילום אוכל'),
(112, N'נעליים'),
(111, N'בגדים'),
(110, N'חד פעמי'),
(109, N'ייעוץ / טיפול זוגי'),
(108, N'הנחיית הורים'),
(107, N'ייעוץ רגשי'),
(106, N'אימון'),
(105, N'טיפול רגשי'),
(104, N'קונדיטוריה'),
(103, N'עיצוב בארים'),
(102, N'קייטרינג בשרי'),
(101, N'קייטנרינג חלבי'),
(100, N'איפור לאירועים'),
(99, N'תסרוקות לאירועים'),
(98, N'סירוק פאה'),
(96, N'מכירת פיאות'),
(64, N'צילום מוצרים'),
(47, N'קידום אתרים'),
(62, N'צילום, הפקת וידאו'),
(28, N'בינה מלאכותית'),
(27, N'ניתוח מערכות'),
(26, N'UI/UX'),
(25, N'פיתוח '),
(24, N'ניהול פרוייקטים'),
(23, N'עיצוב דפי נחיתה'),
(22, N'עיצוב אריזות, מארזים, חבילות'),
(21, N'אנימציה'),
(20, N'תלת מימד- מידול'),
(19, N'עריכת וידיאו'),
(18, N'עיצוב חומרים פרסומיים'),
(17, N'ציור קומיקס'),
(16, N'איור/ רישום'),
(29, N'טלפוניה ומרכזיות'),
(15, N'עיצוב מצגות'),
(13, N'עיצוב אתרים'),
(12, N'עיצוב פליירים'),
(11, N'עיצוב גרפי'),
(10, N'עיצוב לוגו'),
(9, N'ניהול אתר'),
(8, N'סחר מקוון- פייפאל, mangento, Sprite'),
(7, N'בנייה/ התאמה לסלולר'),
(6, N'חיתוך PSD לעיצוב HTML ו-CSS'),
(5, N'אתרים רספונסיביים, פיתוח רספונסיבי'),
(4, N'וורדפרס- WordPress'),
(3, N'בניית אתרים דינמיים'),
(2, N'בניית עמודי נחיתה'),
(1, N'בניית אתרים פשוטים'),
(14, N'באנרים ופרסומות'),
(30, N'ניהול רשתות תקשורת'),
(31, N'קלדנות, תמלול'),
(32, N'כתיבת תוכן לאתרי אינטרנט'),
(61, N'צילום מסחרי'),
(60, N'צילום אירועים'),
(59, N'צילום אווירי/ רחפן'),
(58, N'טלמרקטינג'),
(57, N'חקר שוק'),
(56, N'מיתוג, תדמית עסקית'),
(55, N'יח''צ'),
(54, N'שיווק במייל'),
(53, N'פיתוח עסקי'),
(52, N'ייעוץ ארגוני'),
(51, N'גיוס משאבים'),
(50, N'יעוץ וליווי עסקי'),
(49, N'אסטרטגיה שיווקית'),
(48, N'מכירות'),
(127, N'עריכת דין- ייעוץ משפטי'),
(46, N'שיווק ברשת'),
(45, N'תרגום לעברית'),
(44, N'תרגום מעברית'),
(43, N'תרגום לשפת הסימנים'),
(42, N'תרגום לשפות'),
(41, N'כתיבת בקשות לתרומות, מענקים ומלגות'),
(40, N'כתיבת פוסטים'),
(39, N'כתיבה ועריכת קו''ח'),
(38, N'כתיבת עבודות אקדמיות'),
(37, N'קופירייטינג'),
(36, N'כתיבה שיווקית'),
(35, N'עריכה לשונית, הגהה'),
(34, N'כתיבת מאמרים'),
(33, N'כתיבה יצירתית'),
(63, N'סרטי תדמית'),
(128, N'עריכת דין- ייצוג');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'name') AND [object_id] = OBJECT_ID(N'[SubCategory]'))
    SET IDENTITY_INSERT [SubCategory] OFF;

GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'areaId', N'buisnessId') AND [object_id] = OBJECT_ID(N'[BuisnessArea]'))
    SET IDENTITY_INSERT [BuisnessArea] ON;
INSERT INTO [BuisnessArea] ([Id], [areaId], [buisnessId])
VALUES (1, 1, 1),
(2, 2, 2);
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'areaId', N'buisnessId') AND [object_id] = OBJECT_ID(N'[BuisnessArea]'))
    SET IDENTITY_INSERT [BuisnessArea] OFF;

GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'buisnessId', N'endDate', N'startDate', N'statusId') AND [object_id] = OBJECT_ID(N'[BuisnessStatus]'))
    SET IDENTITY_INSERT [BuisnessStatus] ON;
INSERT INTO [BuisnessStatus] ([Id], [buisnessId], [endDate], [startDate], [statusId])
VALUES (1, 1, NULL, NULL, 1),
(2, 2, NULL, NULL, 2);
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'buisnessId', N'endDate', N'startDate', N'statusId') AND [object_id] = OBJECT_ID(N'[BuisnessStatus]'))
    SET IDENTITY_INSERT [BuisnessStatus] OFF;

GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'categoryId', N'subCategoryId') AND [object_id] = OBJECT_ID(N'[CategorySubCategory]'))
    SET IDENTITY_INSERT [CategorySubCategory] ON;
INSERT INTO [CategorySubCategory] ([Id], [categoryId], [subCategoryId])
VALUES (83, 9, 82),
(84, 10, 83),
(85, 10, 84),
(86, 10, 85),
(87, 10, 86),
(88, 10, 87),
(90, 11, 89),
(82, 9, 81),
(91, 11, 90),
(92, 11, 91),
(93, 12, 92),
(94, 12, 93),
(89, 11, 88),
(81, 9, 80),
(79, 9, 78),
(95, 13, 94),
(78, 9, 77),
(77, 8, 76),
(76, 8, 75),
(75, 8, 74),
(74, 8, 73),
(73, 8, 72),
(72, 8, 71),
(71, 8, 70),
(70, 8, 69),
(68, 7, 68),
(67, 7, 67),
(66, 7, 66),
(80, 9, 79),
(96, 13, 95),
(98, 13, 97),
(65, 7, 65),
(127, 19, 126),
(126, 18, 125),
(125, 18, 124),
(124, 18, 123),
(123, 18, 122),
(122, 18, 121),
(121, 17, 120),
(120, 17, 119),
(119, 17, 118),
(118, 17, 117),
(117, 17, 116),
(116, 16, 115),
(115, 16, 114),
(114, 16, 113),
(113, 16, 112),
(112, 16, 111),
(111, 16, 110),
(110, 15, 109),
(109, 15, 108),
(108, 15, 107),
(107, 15, 106),
(106, 15, 105),
(105, 14, 104),
(104, 14, 103),
(103, 14, 102),
(102, 14, 101),
(101, 13, 100),
(100, 13, 99),
(99, 13, 98),
(97, 13, 96),
(64, 7, 64),
(62, 7, 62),
(128, 19, 127),
(28, 3, 28),
(27, 3, 27),
(26, 3, 26),
(25, 3, 25),
(24, 3, 24),
(23, 2, 23),
(22, 2, 22),
(21, 2, 21),
(20, 2, 20),
(69, 7, 19),
(19, 2, 19),
(18, 2, 18),
(17, 2, 17),
(16, 2, 16),
(15, 2, 15),
(14, 2, 14),
(13, 2, 13),
(12, 2, 12),
(11, 2, 11),
(10, 2, 10),
(9, 1, 9),
(8, 1, 8),
(7, 1, 7),
(6, 1, 6),
(5, 1, 5),
(4, 1, 4),
(3, 1, 3),
(2, 1, 2),
(1, 1, 1),
(29, 3, 29),
(63, 7, 63),
(30, 3, 30),
(32, 4, 32),
(61, 7, 61),
(60, 7, 60),
(59, 7, 59),
(58, 6, 58),
(57, 6, 57),
(56, 6, 56),
(55, 6, 55),
(54, 6, 54),
(53, 6, 53),
(52, 6, 52),
(51, 6, 51),
(50, 6, 50),
(49, 6, 49),
(48, 6, 48),
(47, 6, 47),
(46, 6, 46),
(45, 5, 45),
(44, 5, 44),
(43, 5, 43),
(42, 5, 42),
(41, 4, 41),
(40, 4, 40),
(39, 4, 39),
(38, 4, 38),
(37, 4, 37),
(36, 4, 36),
(35, 4, 35),
(34, 4, 34),
(33, 4, 33),
(31, 4, 31),
(129, 19, 128);
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'categoryId', N'subCategoryId') AND [object_id] = OBJECT_ID(N'[CategorySubCategory]'))
    SET IDENTITY_INSERT [CategorySubCategory] OFF;

GO

CREATE INDEX [IX_Addresses_UserId] ON [Addresses] ([UserId]);

GO

CREATE INDEX [IX_AspNetRoleClaims_RoleId] ON [AspNetRoleClaims] ([RoleId]);

GO

CREATE UNIQUE INDEX [RoleNameIndex] ON [AspNetRoles] ([NormalizedName]) WHERE [NormalizedName] IS NOT NULL;

GO

CREATE INDEX [IX_AspNetUserClaims_UserId] ON [AspNetUserClaims] ([UserId]);

GO

CREATE INDEX [IX_AspNetUserLogins_UserId] ON [AspNetUserLogins] ([UserId]);

GO

CREATE INDEX [IX_AspNetUserRoles_RoleId] ON [AspNetUserRoles] ([RoleId]);

GO

CREATE INDEX [EmailIndex] ON [AspNetUsers] ([NormalizedEmail]);

GO

CREATE UNIQUE INDEX [UserNameIndex] ON [AspNetUsers] ([NormalizedUserName]) WHERE [NormalizedUserName] IS NOT NULL;

GO

CREATE INDEX [IX_Buisness_UpdatedBusinessStatus] ON [Buisness] ([UpdatedBusinessStatus]);

GO

CREATE INDEX [IX_BuisnessArea_areaId] ON [BuisnessArea] ([areaId]);

GO

CREATE INDEX [IX_BuisnessArea_buisnessId] ON [BuisnessArea] ([buisnessId]);

GO

CREATE INDEX [IX_BuisnessPicture_buisnessId] ON [BuisnessPicture] ([buisnessId]);

GO

CREATE INDEX [IX_BuisnessStatus_buisnessId] ON [BuisnessStatus] ([buisnessId]);

GO

CREATE INDEX [IX_BuisnessStatus_statusId] ON [BuisnessStatus] ([statusId]);

GO

CREATE INDEX [IX_BuisnessSubCategory_buisnessId] ON [BuisnessSubCategory] ([buisnessId]);

GO

CREATE INDEX [IX_BuisnessSubCategory_categorySubCategoryId] ON [BuisnessSubCategory] ([categorySubCategoryId]);

GO

CREATE INDEX [IX_BuisnessSubCategoryBarter_buisnessId] ON [BuisnessSubCategoryBarter] ([buisnessId]);

GO

CREATE INDEX [IX_BuisnessSubCategoryBarter_categorySubCategoryId] ON [BuisnessSubCategoryBarter] ([categorySubCategoryId]);

GO

CREATE INDEX [IX_BusinessInCollaborations_JointProjectId] ON [BusinessInCollaborations] ([JointProjectId]);

GO

CREATE INDEX [IX_CategorySubCategory_categoryId] ON [CategorySubCategory] ([categoryId]);

GO

CREATE INDEX [IX_CategorySubCategory_subCategoryId] ON [CategorySubCategory] ([subCategoryId]);

GO

CREATE INDEX [IX_JointProjects_CollaborationTypeId] ON [JointProjects] ([CollaborationTypeId]);

GO

CREATE INDEX [IX_RolePermissions_ApplicationRoleId] ON [RolePermissions] ([ApplicationRoleId]);

GO

CREATE INDEX [IX_Tokens_UserId] ON [Tokens] ([UserId]);

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20211103091751_Init', N'3.1.20');

GO

