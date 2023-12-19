using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CMS_CORE_NG.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Activities",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<string>(nullable: true),
                    IpAddress = table.Column<string>(nullable: true),
                    Location = table.Column<string>(nullable: true),
                    OperatingSystem = table.Column<string>(nullable: true),
                    Date = table.Column<DateTime>(nullable: false),
                    UserId = table.Column<string>(nullable: true),
                    Color = table.Column<string>(nullable: true),
                    Icon = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Activities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Area",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Area", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Name = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    Discriminator = table.Column<string>(nullable: false),
                    RoleName = table.Column<string>(nullable: true),
                    RoleIcon = table.Column<string>(nullable: true),
                    Handle = table.Column<string>(nullable: true),
                    IsActive = table.Column<bool>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    UserName = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(maxLength: 256, nullable: true),
                    Email = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    PasswordHash = table.Column<string>(nullable: true),
                    SecurityStamp = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    AccessFailedCount = table.Column<int>(nullable: false),
                    Notes = table.Column<string>(nullable: true),
                    DisplayName = table.Column<string>(nullable: true),
                    Firstname = table.Column<string>(nullable: true),
                    Middlename = table.Column<string>(nullable: true),
                    Lastname = table.Column<string>(nullable: true),
                    Gender = table.Column<string>(nullable: true),
                    ProfilePic = table.Column<string>(nullable: true),
                    Birthday = table.Column<string>(nullable: true),
                    IsProfileComplete = table.Column<bool>(nullable: false),
                    Terms = table.Column<bool>(nullable: false),
                    IsEmployee = table.Column<bool>(nullable: false),
                    UserRole = table.Column<string>(nullable: true),
                    AccountCreatedOn = table.Column<DateTime>(nullable: false),
                    RememberMe = table.Column<bool>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BarterDeals",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ReportsBusinessId = table.Column<int>(nullable: false),
                    PartnerBusinessId = table.Column<int>(nullable: false),
                    ReportDate = table.Column<DateTime>(nullable: false),
                    ReportCategorySubCategoryId = table.Column<int>(nullable: false),
                    ReportDescriptionDeal = table.Column<string>(nullable: true),
                    PartnerCategorySubCategoryId = table.Column<int>(nullable: false),
                    PartnerDescriptionDeal = table.Column<string>(nullable: true),
                    BusinessDescription = table.Column<string>(nullable: true),
                    QuotePartnerBusiness = table.Column<string>(nullable: true),
                    QuoteReportsBusiness = table.Column<string>(nullable: true),
                    ReportsBusinessPictureID = table.Column<Guid>(nullable: true),
                    PartnerBusinessPictureID = table.Column<Guid>(nullable: true),
                    JointExplanation = table.Column<string>(nullable: true),
                    ConfirmedByPartner = table.Column<bool>(nullable: false),
                    Availability = table.Column<bool>(nullable: true),
                    Service = table.Column<bool>(nullable: true),
                    Professionalism = table.Column<bool>(nullable: true),
                    FairConsiderationForTransaction = table.Column<bool>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BarterDeals", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Category",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Category", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CollaborationTypes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CollaborationTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Contact",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(nullable: true),
                    email = table.Column<string>(nullable: true),
                    message = table.Column<string>(nullable: true),
                    phoneNumber = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contact", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Countries",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CountryId = table.Column<int>(nullable: false),
                    TwoDigitCode = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    PhoneCode = table.Column<string>(nullable: true),
                    Flag = table.Column<string>(nullable: true),
                    States = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Countries", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PaidTransactions",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SupplierBusinessId = table.Column<int>(nullable: false),
                    ConsumerBusinessId = table.Column<int>(nullable: false),
                    CategorySubCategoryId = table.Column<int>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    Review = table.Column<string>(nullable: true),
                    ScopTransactionNIS = table.Column<int>(nullable: true),
                    PictureID = table.Column<Guid>(nullable: true),
                    Availability = table.Column<bool>(nullable: true),
                    Service = table.Column<bool>(nullable: true),
                    Professionalism = table.Column<bool>(nullable: true),
                    Price = table.Column<bool>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaidTransactions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PermissionTypes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PermissionTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StaticSiteComponents",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    url = table.Column<string>(nullable: true),
                    type = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StaticSiteComponents", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Status",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Status", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SubCategory",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubCategory", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TwoFactorCodes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TwoFactorCode = table.Column<string>(nullable: false),
                    RememberDevice = table.Column<bool>(nullable: false),
                    SelectedProvider = table.Column<string>(nullable: false),
                    UserId = table.Column<string>(nullable: true),
                    ExpiryDate = table.Column<DateTime>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    EncryptionKey2Fa = table.Column<string>(nullable: false),
                    Token = table.Column<string>(nullable: false),
                    DeviceId = table.Column<string>(nullable: false),
                    Attempts = table.Column<int>(nullable: false),
                    IpAddress = table.Column<string>(nullable: false),
                    CodeExpired = table.Column<bool>(nullable: false),
                    CodeIsUsed = table.Column<bool>(nullable: false),
                    UserAgent = table.Column<string>(nullable: false),
                    EncryptionKeyForDeviceId = table.Column<string>(nullable: false),
                    DeviceIdExpiry = table.Column<DateTime>(nullable: false),
                    IsDeviceIdExpired = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TwoFactorCodes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UploadActivitiesHistory",
                columns: table => new
                {
                    ImportId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ImporterName = table.Column<string>(nullable: true),
                    ImportFileName = table.Column<string>(nullable: false),
                    TotalRecords = table.Column<int>(nullable: false),
                    ColumnsFound = table.Column<int>(nullable: false),
                    LoadedRecords = table.Column<int>(nullable: false),
                    DeletedRecords = table.Column<int>(nullable: false),
                    MarkForDelete = table.Column<int>(nullable: false),
                    MarkForNew = table.Column<int>(nullable: false),
                    NewLoaded = table.Column<int>(nullable: false),
                    ErroredRecords = table.Column<int>(nullable: false),
                    NotUpdated = table.Column<int>(nullable: false),
                    NewRecordsNotInDataBase = table.Column<int>(nullable: false),
                    StartTime = table.Column<DateTime>(nullable: false),
                    EndTime = table.Column<DateTime>(nullable: false),
                    Added = table.Column<int>(nullable: false),
                    Deleted = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UploadActivitiesHistory", x => x.ImportId);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RolePermissions",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Read = table.Column<bool>(nullable: false),
                    Delete = table.Column<bool>(nullable: false),
                    Update = table.Column<bool>(nullable: false),
                    Add = table.Column<bool>(nullable: false),
                    Type = table.Column<string>(nullable: true),
                    ApplicationRoleId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RolePermissions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RolePermissions_AspNetRoles_ApplicationRoleId",
                        column: x => x.ApplicationRoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Addresses",
                columns: table => new
                {
                    AddressId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Line1 = table.Column<string>(nullable: true),
                    Line2 = table.Column<string>(nullable: true),
                    Unit = table.Column<string>(nullable: true),
                    Country = table.Column<string>(nullable: false),
                    State = table.Column<string>(nullable: true),
                    City = table.Column<string>(nullable: true),
                    PostalCode = table.Column<string>(nullable: true),
                    Type = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Addresses", x => x.AddressId);
                    table.ForeignKey(
                        name: "FK_Addresses_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(nullable: false),
                    ProviderKey = table.Column<string>(nullable: false),
                    ProviderDisplayName = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    RoleId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    LoginProvider = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Tokens",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClientId = table.Column<string>(nullable: false),
                    Value = table.Column<string>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    UserId = table.Column<string>(nullable: false),
                    LastModifiedDate = table.Column<DateTime>(nullable: false),
                    ExpiryTime = table.Column<DateTime>(nullable: false),
                    EncryptionKeyRt = table.Column<string>(nullable: false),
                    EncryptionKeyJwt = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tokens", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "JointProjects",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CollaborationTypeId = table.Column<int>(nullable: false),
                    ReportDate = table.Column<DateTime>(nullable: false),
                    HeaderCollaboration = table.Column<string>(nullable: true),
                    JointExplanation = table.Column<string>(nullable: true),
                    PictureId = table.Column<Guid>(nullable: true),
                    Enterprise = table.Column<bool>(nullable: true),
                    Creativity = table.Column<bool>(nullable: true),
                    Professionalism = table.Column<bool>(nullable: true),
                    ExposureToNewAudiences = table.Column<bool>(nullable: true),
                    ConfirmedByPartners = table.Column<bool>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JointProjects", x => x.Id);
                    table.ForeignKey(
                        name: "FK_JointProjects_CollaborationTypes_CollaborationTypeId",
                        column: x => x.CollaborationTypeId,
                        principalTable: "CollaborationTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Buisness",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    userId = table.Column<string>(nullable: true),
                    businessEmailAddress = table.Column<string>(nullable: true),
                    buisnessName = table.Column<string>(nullable: true),
                    phoneNumber1 = table.Column<string>(nullable: true),
                    phoneNumber2 = table.Column<string>(nullable: true),
                    address = table.Column<string>(nullable: true),
                    actionDiscription = table.Column<string>(nullable: true),
                    discription = table.Column<string>(nullable: true),
                    buisnessWebSiteLink = table.Column<string>(nullable: true),
                    isdisplayBusinessOwnerName = table.Column<bool>(nullable: true),
                    ispayingBuisness = table.Column<bool>(nullable: true),
                    isburterBuisness = table.Column<bool>(nullable: true),
                    iscollaborationBuisness = table.Column<bool>(nullable: true),
                    isburterPossibleInAllCategory = table.Column<bool>(nullable: true),
                    isopenToSuggestionsForBarter = table.Column<bool>(nullable: true),
                    coverPictureId = table.Column<Guid>(nullable: false),
                    logoPictureId = table.Column<Guid>(nullable: false),
                    product1 = table.Column<string>(nullable: true),
                    product2 = table.Column<string>(nullable: true),
                    barterProduct1 = table.Column<string>(nullable: true),
                    barterProduct2 = table.Column<string>(nullable: true),
                    UpdatedBusinessStatus = table.Column<int>(nullable: true),
                    views = table.Column<int>(nullable: true),
                    tags = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Buisness", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Buisness_Status_UpdatedBusinessStatus",
                        column: x => x.UpdatedBusinessStatus,
                        principalTable: "Status",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CategorySubCategory",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    categoryId = table.Column<int>(nullable: false),
                    subCategoryId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategorySubCategory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CategorySubCategory_Category_categoryId",
                        column: x => x.categoryId,
                        principalTable: "Category",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CategorySubCategory_SubCategory_subCategoryId",
                        column: x => x.subCategoryId,
                        principalTable: "SubCategory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BusinessInCollaborations",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BusinessId = table.Column<int>(nullable: false),
                    JoinProjectId = table.Column<int>(nullable: false),
                    PartInCollaboration = table.Column<string>(nullable: true),
                    IfReport = table.Column<bool>(nullable: true),
                    JointProjectId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BusinessInCollaborations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BusinessInCollaborations_JointProjects_JointProjectId",
                        column: x => x.JointProjectId,
                        principalTable: "JointProjects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BuisnessArea",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    buisnessId = table.Column<int>(nullable: false),
                    areaId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BuisnessArea", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BuisnessArea_Area_areaId",
                        column: x => x.areaId,
                        principalTable: "Area",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BuisnessArea_Buisness_buisnessId",
                        column: x => x.buisnessId,
                        principalTable: "Buisness",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BuisnessPicture",
                columns: table => new
                {
                    buisnessPictureId = table.Column<Guid>(nullable: false),
                    buisnessId = table.Column<int>(nullable: false),
                    numberOfPicture = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BuisnessPicture", x => x.buisnessPictureId);
                    table.ForeignKey(
                        name: "FK_BuisnessPicture_Buisness_buisnessId",
                        column: x => x.buisnessId,
                        principalTable: "Buisness",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BuisnessStatus",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    buisnessId = table.Column<int>(nullable: false),
                    statusId = table.Column<int>(nullable: false),
                    startDate = table.Column<DateTime>(nullable: true),
                    endDate = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BuisnessStatus", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BuisnessStatus_Buisness_buisnessId",
                        column: x => x.buisnessId,
                        principalTable: "Buisness",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BuisnessStatus_Status_statusId",
                        column: x => x.statusId,
                        principalTable: "Status",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BuisnessSubCategory",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    buisnessId = table.Column<int>(nullable: false),
                    categorySubCategoryId = table.Column<int>(nullable: false),
                    isPossibleInBarter = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BuisnessSubCategory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BuisnessSubCategory_Buisness_buisnessId",
                        column: x => x.buisnessId,
                        principalTable: "Buisness",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BuisnessSubCategory_CategorySubCategory_categorySubCategoryId",
                        column: x => x.categorySubCategoryId,
                        principalTable: "CategorySubCategory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BuisnessSubCategoryBarter",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    buisnessId = table.Column<int>(nullable: false),
                    categorySubCategoryId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BuisnessSubCategoryBarter", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BuisnessSubCategoryBarter_Buisness_buisnessId",
                        column: x => x.buisnessId,
                        principalTable: "Buisness",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BuisnessSubCategoryBarter_CategorySubCategory_categorySubCategoryId",
                        column: x => x.categorySubCategoryId,
                        principalTable: "CategorySubCategory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Area",
                columns: new[] { "Id", "name" },
                values: new object[,]
                {
                    { 1, "כל הארץ" },
                    { 2, "איזור המרכז" },
                    { 3, "ירושלים והסביבה" },
                    { 4, "צפון" },
                    { 5, "דרום" }
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Discriminator", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "1", null, "IdentityRole", "Administrator", "ADMINISTRATOR" },
                    { "2", null, "IdentityRole", "Customer", "CUSTOMER" }
                });

            migrationBuilder.InsertData(
                table: "Category",
                columns: new[] { "Id", "name" },
                values: new object[,]
                {
                    { 10, "אדריכלות ועיצוב פנים" },
                    { 11, "הפקות ואירועים" },
                    { 12, "שירותי כח אדם" },
                    { 13, "טיפוח אישי ויופי" },
                    { 14, "קייטרינג" },
                    { 17, "ביגוד ואופנה" },
                    { 16, "חנויות ומכירות" },
                    { 9, "מרצים- בתחומי" },
                    { 18, "פיננסים" },
                    { 20, "btb שירותים לעסק" },
                    { 15, "טיפול וייעוץ" },
                    { 8, "אדמנסטרציה" },
                    { 19, "משפט" },
                    { 6, "שיווק ומכירות" },
                    { 5, "תרגום" },
                    { 4, "כתיבה ועריכה" },
                    { 3, "טכנולוגיה ופיתוח תוכנה " },
                    { 2, "עיצוב וגרפיקה" },
                    { 1, "בניית אתרים" },
                    { 7, "צילום וודיאו" }
                });

            migrationBuilder.InsertData(
                table: "CollaborationTypes",
                columns: new[] { "Id", "Description" },
                values: new object[,]
                {
                    { 1, "מיזם משותף" },
                    { 2, "מוצר משותף" },
                    { 4, "השקעה ושותפות" },
                    { 3, "שיווק הדדי" }
                });

            migrationBuilder.InsertData(
                table: "Status",
                columns: new[] { "Id", "name" },
                values: new object[,]
                {
                    { 1, "עסק רשום" },
                    { 2, "עסק מאושר" }
                });

            migrationBuilder.InsertData(
                table: "SubCategory",
                columns: new[] { "Id", "name" },
                values: new object[,]
                {
                    { 85, "עיצוב פנים" },
                    { 82, "אומנות המזון" },
                    { 83, "עיצוב ותכנון" },
                    { 84, "רישוי" },
                    { 86, "הום סטיילינג" },
                    { 92, "השמה" },
                    { 88, "תכנון כנסים" },
                    { 89, "הפקת אירועים" },
                    { 90, "עיצוב אירועים" },
                    { 91, "שיווק וייצוג אומנים" },
                    { 81, "אימון וטיפול" },
                    { 93, "ייעוץ/ אבחון תעסוקתי" },
                    { 87, "CAD, AutoCAD, שרטוט אדריכלי" },
                    { 80, "בריאות" },
                    { 74, "שירות לקוחות" },
                    { 78, "חינוך" },
                    { 77, "תעסוקה ועסקים" },
                    { 76, "תיאום פגישות" },
                    { 75, "ניהול משרד" },
                    { 73, "שירותי גבייה" },
                    { 72, "מזכירות" },
                    { 71, "רכש" },
                    { 70, "עזרה אדמינסרטיבית" },
                    { 69, "קלדנות" },
                    { 68, "צילום אופנה" },
                    { 67, "צילום אדריכלות ועיצוב פנים" },
                    { 66, "צילום משפחות" },
                    { 94, "סטיילינג" },
                    { 79, "טכנולוגיה ומחשבים" },
                    { 95, "קוסמטיקה" },
                    { 112, "נעליים" },
                    { 97, "עיצוב פאה" },
                    { 126, "עריכת דין- עריכת הסכמים" },
                    { 125, "חשבות שכר" },
                    { 124, "הנהלת חשבונות" },
                    { 123, "הגשת דוחות" },
                    { 122, "ראיית חשבון" },
                    { 121, "ייעוץ מס" },
                    { 120, "תחתיות לאירועים" },
                    { 119, "שמלות כלה" },
                    { 118, "תפירת בגדי אירועים" },
                    { 117, "השכרת בגדי אירועים" },
                    { 116, "עיצוב אופנה" },
                    { 115, "עדשות מגע" },
                    { 114, "מוצרי אפייה" },
                    { 113, "גרביים" },
                    { 65, "צילום אוכל" },
                    { 111, "בגדים" },
                    { 110, "חד פעמי" },
                    { 109, "ייעוץ / טיפול זוגי" },
                    { 108, "הנחיית הורים" },
                    { 107, "ייעוץ רגשי" },
                    { 106, "אימון" },
                    { 105, "טיפול רגשי" },
                    { 104, "קונדיטוריה" },
                    { 103, "עיצוב בארים" },
                    { 102, "קייטרינג בשרי" },
                    { 101, "קייטנרינג חלבי" },
                    { 100, "איפור לאירועים" },
                    { 99, "תסרוקות לאירועים" },
                    { 98, "סירוק פאה" },
                    { 96, "מכירת פיאות" },
                    { 64, "צילום מוצרים" },
                    { 48, "מכירות" },
                    { 62, "צילום, הפקת וידאו" },
                    { 28, "בינה מלאכותית" },
                    { 27, "ניתוח מערכות" },
                    { 26, "UI/UX" },
                    { 25, "פיתוח " },
                    { 24, "ניהול פרוייקטים" },
                    { 23, "עיצוב דפי נחיתה" },
                    { 22, "עיצוב אריזות, מארזים, חבילות" },
                    { 21, "אנימציה" },
                    { 20, "תלת מימד- מידול" },
                    { 19, "עריכת וידיאו" },
                    { 18, "עיצוב חומרים פרסומיים" },
                    { 17, "ציור קומיקס" },
                    { 16, "איור/ רישום" },
                    { 29, "טלפוניה ומרכזיות" },
                    { 15, "עיצוב מצגות" },
                    { 13, "עיצוב אתרים" },
                    { 12, "עיצוב פליירים" },
                    { 11, "עיצוב גרפי" },
                    { 10, "עיצוב לוגו" },
                    { 9, "ניהול אתר" },
                    { 8, "סחר מקוון- פייפאל, mangento, Sprite" },
                    { 7, "בנייה/ התאמה לסלולר" },
                    { 6, "חיתוך PSD לעיצוב HTML ו-CSS" },
                    { 5, "אתרים רספונסיביים, פיתוח רספונסיבי" },
                    { 4, "וורדפרס- WordPress" },
                    { 3, "בניית אתרים דינמיים" },
                    { 2, "בניית עמודי נחיתה" },
                    { 1, "בניית אתרים פשוטים" },
                    { 14, "באנרים ופרסומות" },
                    { 30, "ניהול רשתות תקשורת" },
                    { 31, "קלדנות, תמלול" },
                    { 32, "כתיבת תוכן לאתרי אינטרנט" },
                    { 61, "צילום מסחרי" },
                    { 60, "צילום אירועים" },
                    { 59, "צילום אווירי/ רחפן" },
                    { 58, "טלמרקטינג" },
                    { 57, "חקר שוק" },
                    { 56, "מיתוג, תדמית עסקית" },
                    { 55, "יח'צ" },
                    { 54, "שיווק במייל" },
                    { 53, "פיתוח עסקי" },
                    { 52, "ייעוץ ארגוני" },
                    { 51, "גיוס משאבים" },
                    { 50, "יעוץ וליווי עסקי" },
                    { 49, "אסטרטגיה שיווקית" },
                    { 127, "עריכת דין- ייעוץ משפטי" },
                    { 47, "קידום אתרים" },
                    { 46, "שיווק ברשת" },
                    { 45, "תרגום לעברית" },
                    { 44, "תרגום מעברית" },
                    { 43, "תרגום לשפת הסימנים" },
                    { 42, "תרגום לשפות" },
                    { 41, "כתיבת בקשות לתרומות, מענקים ומלגות" },
                    { 40, "כתיבת פוסטים" },
                    { 39, "כתיבה ועריכת קו'ח" },
                    { 38, "כתיבת עבודות אקדמיות" },
                    { 37, "קופירייטינג" },
                    { 36, "כתיבה שיווקית" },
                    { 35, "עריכה לשונית, הגהה" },
                    { 34, "כתיבת מאמרים" },
                    { 33, "כתיבה יצירתית" },
                    { 63, "סרטי תדמית" },
                    { 128, "עריכת דין- ייצוג" }
                });

            migrationBuilder.InsertData(
                table: "CategorySubCategory",
                columns: new[] { "Id", "categoryId", "subCategoryId" },
                values: new object[,]
                {
                    { 1, 1, 1 },
                    { 94, 12, 93 },
                    { 93, 12, 92 },
                    { 92, 11, 91 },
                    { 91, 11, 90 },
                    { 90, 11, 89 },
                    { 89, 11, 88 },
                    { 88, 10, 87 },
                    { 87, 10, 86 },
                    { 86, 10, 85 },
                    { 85, 10, 84 },
                    { 84, 10, 83 },
                    { 83, 9, 82 },
                    { 82, 9, 81 },
                    { 95, 13, 94 },
                    { 81, 9, 80 },
                    { 79, 9, 78 },
                    { 78, 9, 77 },
                    { 77, 8, 76 },
                    { 76, 8, 75 },
                    { 75, 8, 74 },
                    { 74, 8, 73 },
                    { 73, 8, 72 },
                    { 72, 8, 71 },
                    { 71, 8, 70 },
                    { 70, 8, 69 },
                    { 68, 7, 68 },
                    { 67, 7, 67 },
                    { 66, 7, 66 },
                    { 80, 9, 79 },
                    { 96, 13, 95 },
                    { 97, 13, 96 },
                    { 98, 13, 97 },
                    { 127, 19, 126 },
                    { 126, 18, 125 },
                    { 125, 18, 124 },
                    { 124, 18, 123 },
                    { 123, 18, 122 },
                    { 122, 18, 121 },
                    { 121, 17, 120 },
                    { 120, 17, 119 },
                    { 119, 17, 118 },
                    { 118, 17, 117 },
                    { 117, 17, 116 },
                    { 116, 16, 115 },
                    { 115, 16, 114 },
                    { 114, 16, 113 },
                    { 113, 16, 112 },
                    { 112, 16, 111 },
                    { 111, 16, 110 },
                    { 110, 15, 109 },
                    { 109, 15, 108 },
                    { 108, 15, 107 },
                    { 107, 15, 106 },
                    { 106, 15, 105 },
                    { 105, 14, 104 },
                    { 104, 14, 103 },
                    { 103, 14, 102 },
                    { 102, 14, 101 },
                    { 101, 13, 100 },
                    { 100, 13, 99 },
                    { 99, 13, 98 },
                    { 65, 7, 65 },
                    { 128, 19, 127 },
                    { 64, 7, 64 },
                    { 62, 7, 62 },
                    { 28, 3, 28 },
                    { 27, 3, 27 },
                    { 26, 3, 26 },
                    { 25, 3, 25 },
                    { 24, 3, 24 },
                    { 23, 2, 23 },
                    { 22, 2, 22 },
                    { 21, 2, 21 },
                    { 20, 2, 20 },
                    { 69, 7, 19 },
                    { 19, 2, 19 },
                    { 18, 2, 18 },
                    { 17, 2, 17 },
                    { 29, 3, 29 },
                    { 16, 2, 16 },
                    { 14, 2, 14 },
                    { 13, 2, 13 },
                    { 12, 2, 12 },
                    { 11, 2, 11 },
                    { 10, 2, 10 },
                    { 9, 1, 9 },
                    { 8, 1, 8 },
                    { 7, 1, 7 },
                    { 6, 1, 6 },
                    { 5, 1, 5 },
                    { 4, 1, 4 },
                    { 3, 1, 3 },
                    { 2, 1, 2 },
                    { 15, 2, 15 },
                    { 30, 3, 30 },
                    { 31, 4, 31 },
                    { 32, 4, 32 },
                    { 61, 7, 61 },
                    { 60, 7, 60 },
                    { 59, 7, 59 },
                    { 58, 6, 58 },
                    { 57, 6, 57 },
                    { 56, 6, 56 },
                    { 55, 6, 55 },
                    { 54, 6, 54 },
                    { 53, 6, 53 },
                    { 52, 6, 52 },
                    { 51, 6, 51 },
                    { 50, 6, 50 },
                    { 49, 6, 49 },
                    { 48, 6, 48 },
                    { 47, 6, 47 },
                    { 46, 6, 46 },
                    { 45, 5, 45 },
                    { 44, 5, 44 },
                    { 43, 5, 43 },
                    { 42, 5, 42 },
                    { 41, 4, 41 },
                    { 40, 4, 40 },
                    { 39, 4, 39 },
                    { 38, 4, 38 },
                    { 37, 4, 37 },
                    { 36, 4, 36 },
                    { 35, 4, 35 },
                    { 34, 4, 34 },
                    { 33, 4, 33 },
                    { 63, 7, 63 },
                    { 129, 19, 128 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Addresses_UserId",
                table: "Addresses",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Buisness_UpdatedBusinessStatus",
                table: "Buisness",
                column: "UpdatedBusinessStatus");

            migrationBuilder.CreateIndex(
                name: "IX_BuisnessArea_areaId",
                table: "BuisnessArea",
                column: "areaId");

            migrationBuilder.CreateIndex(
                name: "IX_BuisnessArea_buisnessId",
                table: "BuisnessArea",
                column: "buisnessId");

            migrationBuilder.CreateIndex(
                name: "IX_BuisnessPicture_buisnessId",
                table: "BuisnessPicture",
                column: "buisnessId");

            migrationBuilder.CreateIndex(
                name: "IX_BuisnessStatus_buisnessId",
                table: "BuisnessStatus",
                column: "buisnessId");

            migrationBuilder.CreateIndex(
                name: "IX_BuisnessStatus_statusId",
                table: "BuisnessStatus",
                column: "statusId");

            migrationBuilder.CreateIndex(
                name: "IX_BuisnessSubCategory_buisnessId",
                table: "BuisnessSubCategory",
                column: "buisnessId");

            migrationBuilder.CreateIndex(
                name: "IX_BuisnessSubCategory_categorySubCategoryId",
                table: "BuisnessSubCategory",
                column: "categorySubCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_BuisnessSubCategoryBarter_buisnessId",
                table: "BuisnessSubCategoryBarter",
                column: "buisnessId");

            migrationBuilder.CreateIndex(
                name: "IX_BuisnessSubCategoryBarter_categorySubCategoryId",
                table: "BuisnessSubCategoryBarter",
                column: "categorySubCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_BusinessInCollaborations_JointProjectId",
                table: "BusinessInCollaborations",
                column: "JointProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_CategorySubCategory_categoryId",
                table: "CategorySubCategory",
                column: "categoryId");

            migrationBuilder.CreateIndex(
                name: "IX_CategorySubCategory_subCategoryId",
                table: "CategorySubCategory",
                column: "subCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_JointProjects_CollaborationTypeId",
                table: "JointProjects",
                column: "CollaborationTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_RolePermissions_ApplicationRoleId",
                table: "RolePermissions",
                column: "ApplicationRoleId");

            migrationBuilder.CreateIndex(
                name: "IX_Tokens_UserId",
                table: "Tokens",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Activities");

            migrationBuilder.DropTable(
                name: "Addresses");

            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "BarterDeals");

            migrationBuilder.DropTable(
                name: "BuisnessArea");

            migrationBuilder.DropTable(
                name: "BuisnessPicture");

            migrationBuilder.DropTable(
                name: "BuisnessStatus");

            migrationBuilder.DropTable(
                name: "BuisnessSubCategory");

            migrationBuilder.DropTable(
                name: "BuisnessSubCategoryBarter");

            migrationBuilder.DropTable(
                name: "BusinessInCollaborations");

            migrationBuilder.DropTable(
                name: "Contact");

            migrationBuilder.DropTable(
                name: "Countries");

            migrationBuilder.DropTable(
                name: "PaidTransactions");

            migrationBuilder.DropTable(
                name: "PermissionTypes");

            migrationBuilder.DropTable(
                name: "RolePermissions");

            migrationBuilder.DropTable(
                name: "StaticSiteComponents");

            migrationBuilder.DropTable(
                name: "Tokens");

            migrationBuilder.DropTable(
                name: "TwoFactorCodes");

            migrationBuilder.DropTable(
                name: "UploadActivitiesHistory");

            migrationBuilder.DropTable(
                name: "Area");

            migrationBuilder.DropTable(
                name: "Buisness");

            migrationBuilder.DropTable(
                name: "CategorySubCategory");

            migrationBuilder.DropTable(
                name: "JointProjects");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Status");

            migrationBuilder.DropTable(
                name: "Category");

            migrationBuilder.DropTable(
                name: "SubCategory");

            migrationBuilder.DropTable(
                name: "CollaborationTypes");
        }
    }
}
