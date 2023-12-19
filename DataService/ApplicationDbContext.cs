
using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ModelService;
using ModelService.busoftModels;
using ModelService.windoModels;
using Windo.Models;
//using Buisness = ModelService.windoModels.Buisness;

namespace DataService
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options , IDataBaseSeeder seeder) : base(options)
        {
            _seeder = seeder;
        }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<AddressModel> Addresses { get; set; }
        public DbSet<TokenModel> Tokens { get; set; }
        public DbSet<ActivityModel> Activities { get; set; }
        public DbSet<CountryModel> Countries { get; set; }
        public DbSet<ApplicationRole> ApplicationRoles { get; set; }
        public DbSet<RolePermission> RolePermissions { get; set; }
        public DbSet<PermissionType> PermissionTypes { get; set; }
        public DbSet<TwoFactorCodeModel> TwoFactorCodes { get; set; }
        public DbSet<ContactModel> Contact { get; set; }

        //windo db sets

        //public DbSet<Cities> Cities { get; set; }
        //public DbSet<Buisness> Buisness { get; set; }
        //public DbSet<Services> Services { get; set; }
        //public DbSet<SubTopics> SubTopics { get; set; }
        //public DbSet<Topics> Topics { get; set; }
        //public DbSet<ServiceType> ServiceType { get; set; }
        //public DbSet<BuisnessServices> BuisnessServices { get; set; }

        // updated windo db sets
        public DbSet<Area> Area { get; set; }
        public DbSet<Buisness> Buisness { get; set; }
        public DbSet<BuisnessArea> BuisnessArea { get; set; }
        public DbSet<BuisnessPicture> BuisnessPicture { get; set; }
        public DbSet<BuisnessStatus> BuisnessStatus { get; set; }
        public DbSet<Status> Status { get; set; }
        public DbSet<SubCategory> SubCategory { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<CategorySubCategory> CategorySubCategory { get; set; }
        public DbSet<BuisnessSubCategory> BuisnessSubCategory { get; set; }
        public DbSet<BuisnessSubCategoryBarter> BuisnessSubCategoryBarter { get; set; }
        public DbSet<BusinessCategoryNotify> BusinessCategoriesNotify { get; set; }
        public DbSet<ScroingOperation> ScroingOperations { get; set; }
        public DbSet<BusinessScoring> BusinessScorings { get; set; }
        public DbSet<ScoringEventType> ScoringEventTypes { get; set; }
        public DbSet<ScoringAction> ScoringActions { get; set; }
        public DbSet<StaticSiteComponents> StaticSiteComponents { get; set; }
        public DbSet<ImportResponseModel> UploadActivitiesHistory { get; set; }
        public DbSet<PaidTransaction> PaidTransactions { get; set; }

        public DbSet<BarterDeal> BarterDeals { get; set; }
        public DbSet<JointProject> JointProjects { get; set; }
        public DbSet<BusinessInCollaboration> BusinessInCollaborations { get; set; }
        public DbSet<CollaborationType> CollaborationTypes { get; set; }
        public DbSet<BusinessInCaseStudy> BusinessesInCaseStudy { get; set; }
        public DbSet<CaseStudy> CaseStudies { get; set; }
        public DbSet<CaseStudyPicture> CaseStudyPictures { get; set; }
        public DbSet<CaseStudyCustomerResponses> CustomerResponses { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<MessagesTo> MessagesTo { get; set; }

        public DbSet<Note> Notes { get; set; }
        public DbSet<Board> Boards { get; set; }
        public DbSet<NotesBoards> NotesBoards { get; set; }
        public DbSet<ReplayNoteMessage> ReplayNoteMessages { get; set; }
        public DbSet<NoteReplay> NoteReplays { get; set; }

        public DbSet<NetworkingGroup> NetworkingGroups { get; set; }
        public DbSet<NetworkingGroupBusiness> networkingGroupBusinesses { get; set; }
        public DbSet<Banner> Banners { get; set; }
        public DbSet<ServiceType> ServiceTypes { get; set; }
        public DbSet<CatalogService> CatalogServices { get; set; }
        public DbSet<ClientType> ClientTypes { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<OrderStatuses> OrderStatuses { get; set; }
        public DbSet<OrderService> OrderServices { get; set; }
        public DbSet<AdvertismentServiceOrder> AdvertismentServiceOrders  { get; set; }
        public DbSet<RequestOrderService> RequsetsOrderService { get; set; }
        public IDataBaseSeeder _seeder { get; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<CatalogService>()
                .Property(x => x.Makat).HasAnnotation("SqlServer:Identity", "1001,1");
           //.ValueGeneratedOnAdd().UseIdentityColumn(1000,1);
            modelBuilder.Entity<IdentityRole>().HasData(
                new { Id = "1", Name = "Administrator", NormalizedName = "ADMINISTRATOR", RoleName = "Administrator", Handle = "administrator", RoleIcon = "/uploads/roles/icons/default/role.png", IsActive = true },
                new { Id = "2", Name = "Customer", NormalizedName = "CUSTOMER", RoleName = "customer", Handle = "customer", RoleIcon = "/uploads/roles/icons/default/role.png", IsActive = true }
            );
         //   modelBuilder.Entity<Buisness>().HasData(
         //    new Buisness
         //    {
         //        Id = 1,
         //        userId = "39a5e42aab68",
         //        buisnessName = "busoft",
         //        phoneNumber1 = "0523152114",
         //        phoneNumber2 = "0523152115",
         //        address = "הארי 15",
         //        actionDiscription = "העסק שלך זה האליפות שלנו",
         //        discription = "העסק שלך זהs fdghjkl dfhjk sdfghj dcfghjk ertyui oxcvbnm dfghj  האליפות שלנו",
         //        buisnessWebSiteLink = "https://uriba.visualstudio.com/WINDO/_sprints/backlog/WINDO%20Team/WINDO/sprint%201",
         //        isdisplayBusinessOwnerName = true,
         //        ispayingBuisness = true,
         //        isburterBuisness = true,
         //        iscollaborationBuisness = true,
         //        isburterPossibleInAllCategory = true,
         //        isopenToSuggestionsForBarter = true,
         //        product1 = "logo",
         //        product2 = "כרטיס ביקור",
         //        barterProduct1 = "logo barter",
         //        barterProduct2 = "כרטיס ביקור barter",
         //        //UpdatedBusinessStatus = "",
         //        views = 5,
         //        tags = "",
         //    },
         //    new Buisness
         //    {
         //        Id = 2,
         //        userId = "39a5e42aab68",
         //        buisnessName = "תמך",
         //        phoneNumber1 = "0523152114",
         //        phoneNumber2 = "0523152115",
         //        address = "שרי ישראל 15",
         //        actionDiscription = "קידום והעסקה",
         //        discription = "עמכעדחלחךיל כעיחל כעיחל גכעטו ןראטו ןבהנמ גכעיח ו",
         //        buisnessWebSiteLink = "https://uriba.visualstudio.com/WINDO/_sprints/backlog/WINDO%20Team/WINDO/sprint%201",
         //        isdisplayBusinessOwnerName = true,
         //        ispayingBuisness = true,
         //        isburterBuisness = true,
         //        iscollaborationBuisness = true,
         //        isburterPossibleInAllCategory = true,
         //        isopenToSuggestionsForBarter = true,
         //        product1 = "logo657567",
         //        product2 = "5675כרטיס ביקור",
         //        barterProduct1 = "logo657 barter",
         //        barterProduct2 = "56756כרטיס ביקור barter",
         //        //UpdatedBusinessStatus = "",
         //        views = 5,
         //        tags = "",
         //    }
         //);
         //   modelBuilder.Entity<Category>().HasData(
         //    new Category{ Id = 1, name = "עיצוב" },
         //    new Category { Id = 2, name = "הייטק" }
         //);
         //   modelBuilder.Entity<SubCategory>().HasData(
         //    new SubCategory{ Id = 1, name = "גרפיקה" },
         //    new SubCategory { Id = 2, name = "תכנות" }
         //);
            modelBuilder.Entity<Area>().HasData(
             new Area{ Id = 1, name = "כל הארץ" },
             new Area{ Id = 2, name = "אזור המרכז" },
             new Area{ Id = 3, name = "ירושלים והסביבה" },
             new Area{ Id = 4, name = "צפון" },
             new Area{ Id = 5, name = "דרום" }
         );
         //   modelBuilder.Entity<BuisnessArea>().HasData(
         //    new BuisnessArea{ Id = 1, buisnessId = 1, areaId = 1  },
         //    new BuisnessArea { Id = 2, buisnessId = 2, areaId = 2 }
         //);
            modelBuilder.Entity<Status>().HasData(
             new Status{ Id = 1, name = "עסק רשום" },
             new Status { Id = 2, name = "עסק מאושר" }
         );
         //   modelBuilder.Entity<BuisnessStatus>().HasData(
         //    new BuisnessStatus { Id = 1, buisnessId = 1, statusId = 1 },//, Status= Status, startDate="" ,endDate=""
         //    new BuisnessStatus { Id = 2, buisnessId = 2, statusId = 2 } //, Status = Status startDate="" ,endDate=""
         //);
            modelBuilder.Entity<CollaborationType>().HasData(
                new CollaborationType() { Id = 1, Description = "מיזם משותף" },
                new CollaborationType() { Id = 2, Description = "מוצר משותף" },
                new CollaborationType() { Id = 3, Description = "שיווק הדדי" },
                new CollaborationType() { Id = 4, Description = "השקעה ושותפות" }
                );
            modelBuilder.Entity<Board>().HasData(
                new Board() { Id = 1, Name = "שת\"פ", Description = "כאן תראי מודעות המציעות שת\"פ", Color = "#f16b7e" },
                new Board() { Id = 2, Name = "עזרה", Description = "כאן תראי מודעות דרושים ובקשת עזרה",
                    Color = "#f9af74" },
                new Board() { Id = 3, Name = "תמך", Description = "כאן תראי עדכונים והודעות רשמיות של עמותת תמך", Color = "#44BE99" }
                );

            modelBuilder.Entity<ClientType>().HasData(
                new ClientType() { Id = 1, Description = "מנוי" },
                new ClientType() { Id = 2, Description = "מפרסם" },
                new ClientType() { Id = 3, Description = "בזק" }
                );
            modelBuilder.Entity<ServiceType>().HasData(
                new ServiceType() { Id = 1, Name = "פרסום", Description = "הצבת פרסומות על דפי האתר" },
                new ServiceType() { Id = 2, Name = "B2B", Description = "" }
                );
            modelBuilder.Entity<CatalogService>().HasData(
               new CatalogService() { Id = 1, ServiceTypeId = 1, Makat = 1001, Description = "1 פרסומת בדף הבית" },
               new CatalogService() { Id = 2, ServiceTypeId = 1, Makat = 1002, Description = "פרסומת בדף הבית 2" },
               new CatalogService() { Id = 3, ServiceTypeId = 1, Makat = 1003, Description = "פרסומת בדף חיפוש עסקים" },
               new CatalogService() { Id = 4, ServiceTypeId = 1, Makat = 1004, Description = "באנר תחתון בדף עסקים" },
               new CatalogService() { Id = 5, ServiceTypeId = 1, Makat = 1005, Description = "באנר עליון בדף העסק" },
               new CatalogService() { Id = 6, ServiceTypeId = 1, Makat = 1006, Description = "באנר עליון במודעות" },
               new CatalogService() { Id = 7, ServiceTypeId = 1, Makat = 1007, Description = "באנר תחתון במודעות" },
               new CatalogService() { Id = 8, ServiceTypeId = 1, Makat = 1008, Description = "פרסומת בדף עצמאית" },
               new CatalogService() { Id = 9, ServiceTypeId = 1, Makat = 1009, Description = "פרסומת בדף שכירה " },
               new CatalogService() { Id = 10, ServiceTypeId = 1, Makat = 1010, Description = "פרסומת בדף ההאב" }
                //new CatalogService() { Id = 11, ServiceTypeId = 2, Makat = 1011, Description = "B2B" }
               );
            modelBuilder.Entity<Banner>().HasData(
               new Banner() { Id = 1, Makat = 1001, Width = 100, Height = 300, PageID = 1, PageName = "דף הבית", Price = 200, PriceInPoints = 10, Title = "באנר מרכזי בדף הבית", DefaultLink= "/advertisments-catalog", DefaultPicGuid = Guid.Parse("5f726d18-a68b-497b-bec0-940e2dac3be1"), ExamplePicGuid = Guid.Parse("0f8fad5b-d9cb-469f-a165-70867728950e")  },
               new Banner() { Id = 2, Makat = 1002, Width = 100, Height = 180, PageID = 1, PageName = "דף הבית", Price = 200, PriceInPoints = 5, Title = "באנר נוסף בדף הבית", DefaultLink = "/advertisments-catalog", DefaultPicGuid = Guid.Parse("a89c20ea-d971-4e00-a7db-be9b90eeb1da"), ExamplePicGuid = Guid.Parse("0f8fad5b-d9cb-469f-a165-70867728950e") },
               new Banner() { Id = 3, Makat = 1003, Width = 100, Height = 180, PageID = 2, PageName = "דף חיפוש עסקים", Price = 200, PriceInPoints = 5, Title = "באנר עליון בדף חיפוש עסקים", DefaultLink = "/advertisments-catalog", DefaultPicGuid = Guid.Parse("32af284f-ef35-4255-b363-2b4248664383"), ExamplePicGuid = Guid.Parse("0f8fad5b-d9cb-469f-a165-70867728950e") },
               new Banner() { Id = 4, Makat = 1004, Width = 100, Height = 180, PageID = 2, PageName = "דף חיפוש עסקים", Price = 200, PriceInPoints = 5, Title = "באנר תחתון בדף חיפוש עסקים", DefaultLink = "/advertisments-catalog", DefaultPicGuid = Guid.Parse("32af284f-ef35-4255-b363-2b4248664383"), ExamplePicGuid = Guid.Parse("0f8fad5b-d9cb-469f-a165-70867728950e") },
               new Banner() { Id = 5, Makat = 1005, Width = 100, Height = 180, PageID = 3, PageName = "דף עסק", Price = 200, PriceInPoints = 5, Title = "בראש דף העסק", DefaultLink = "/advertisments-catalog", DefaultPicGuid = Guid.Parse("32af284f-ef35-4255-b363-2b4248664383"), ExamplePicGuid = Guid.Parse("0f8fad5b-d9cb-469f-a165-70867728950e") },
               new Banner() { Id = 6, Makat = 1006, Width = 25, Height = 80, PageID = 4, PageName = "לוח מודעות", Price = 250, PriceInPoints = 15, Title = "באנר עליון במודעות", DefaultLink = "/advertisments-catalog", DefaultPicGuid = Guid.Parse("32af284f-ef35-4255-b363-2b4248664383"), ExamplePicGuid = Guid.Parse("0f8fad5b-d9cb-469f-a165-70867728950e") },
               new Banner() { Id = 7, Makat = 1007, Width = 25, Height = 80, PageID = 4, PageName = "לוח מודעות", Price = 250, PriceInPoints = 15, Title = "באנר תחתון במודעות", DefaultLink = "/advertisments-catalog", DefaultPicGuid = Guid.Parse("32af284f-ef35-4255-b363-2b4248664383"), ExamplePicGuid = Guid.Parse("0f8fad5b-d9cb-469f-a165-70867728950e") },
               new Banner() { Id = 8, Makat = 1008, Width = 25, Height = 80, PageID = 5, PageName = "דף עצמאית", Price = 250, PriceInPoints = 15, Title = "באנר בדף עצמאית", DefaultLink = "/advertisments-catalog", DefaultPicGuid = Guid.Parse("32af284f-ef35-4255-b363-2b4248664383"), ExamplePicGuid = Guid.Parse("0f8fad5b-d9cb-469f-a165-70867728950e") },
               new Banner() { Id = 9, Makat = 1009, Width = 25, Height = 80, PageID = 6, PageName = "דף שכירה", Price = 250, PriceInPoints = 15, Title = "באנר בדף שכירה", DefaultLink = "/advertisments-catalog", DefaultPicGuid = Guid.Parse("32af284f-ef35-4255-b363-2b4248664383"), ExamplePicGuid = Guid.Parse("0f8fad5b-d9cb-469f-a165-70867728950e") },
               new Banner() { Id = 10, Makat = 1010, Width = 100, Height = 180, PageID = 7, PageName = "דף ההאב", Price = 250, PriceInPoints = 15, Title = "באנר בדף ההאב ", DefaultLink = "/advertisments-catalog", DefaultPicGuid = Guid.Parse("32af284f-ef35-4255-b363-2b4248664383"), ExamplePicGuid = Guid.Parse("0f8fad5b-d9cb-469f-a165-70867728950e") }
               );
            modelBuilder.Entity<OrderStatuses>().HasData(
                new OrderStatuses() { Id = 1, Description = "מחכה למנהל" },
                new OrderStatuses() { Id = 2, Description = "אושר לא שולם" },
                new OrderStatuses() { Id = 3, Description = "אושר ושולם" }
                );
            modelBuilder.Entity<Banner>()
           .HasOne(s => s.CatalogService)
           .WithMany()
           .HasForeignKey(s => s.Makat)
           .HasPrincipalKey(c => c.Makat);
            //SubCategory sc = new SubCategory { Id = 1, name = "גרפיקה" };
            //SubCategory sc2 = new SubCategory { Id = 2, name = "תכנות" };

            //Category c = new Category { Id = 1, name = "עיצוב" };
            //Category c2 = new Category { Id = 2, name = "הייטק" };

            //CategorySubCategory csc = new CategorySubCategory { Id = 1, name = "עיצוב" };
            //CategorySubCategory csc2 = new CategorySubCategory { Id = 2, name = "הייטק" };

            //   modelBuilder.Entity<CategorySubCategory>().HasData(
            //    new CategorySubCategory { Id = 1, categoryId = 1, subCategoryId = 1},/*, Category = c, SubCategory = sc*/ 
            //    new CategorySubCategory { Id = 2, categoryId = 2, subCategoryId = 2}/*, Category = c2, SubCategory = sc2 */
            //);
            //   modelBuilder.Entity<BuisnessSubCategory>().HasData(
            //    new BuisnessSubCategory { Id = 1, buisnessId = 1, categorySubCategoryId = 1, isPossibleInBarter = true},/*, CategorySubCategory= CategorySubCategory */
            //    new BuisnessSubCategory { Id = 2, buisnessId = 2, categorySubCategoryId = 2, isPossibleInBarter = false,/*CategorySubCategory= CategorySubCategory*/ }
            //);
            //   modelBuilder.Entity<BuisnessSubCategoryBarter>().HasData(
            //    new BuisnessSubCategoryBarter { Id = 1, buisnessId = 1, categorySubCategoryId = 1/*, CategorySubCategory = CategorySubCategory*/ },
            //    new BuisnessSubCategoryBarter { Id = 2, buisnessId = 2, categorySubCategoryId = 2/*, CategorySubCategory = CategorySubCategory */}
            //);
            modelBuilder.Entity<Buisness>()
                    .HasKey(b => b.Id);

            modelBuilder.Entity<Area>()
                    .HasKey(b => b.Id);

            modelBuilder.Entity<BuisnessArea>()
                    .HasKey(b => b.Id);

            //modelBuilder.Entity<BuisnessCoverPicture>()
            //        .HasKey(b => b.Id);

            //modelBuilder.Entity<BuisnessLogo>()
            //        .HasKey(b => b.Id);

            modelBuilder.Entity<BuisnessPicture>()
                    .HasKey(b => b.buisnessPictureId);

            modelBuilder.Entity<BuisnessStatus>()
                    .HasKey(b => b.Id);

            modelBuilder.Entity<BuisnessSubCategory>()
                    .HasKey(b => b.Id);

            modelBuilder.Entity<BuisnessSubCategoryBarter>()
                   .HasKey(b => b.Id);

            modelBuilder.Entity<Category>()
                     .HasKey(b => b.Id);

            modelBuilder.Entity<SubCategory>()
                   .HasKey(b => b.Id);

            modelBuilder.Entity<Status>()
                    .HasKey(b => b.Id);

            modelBuilder.Entity<CategorySubCategory>()
                      .HasKey(b => b.Id);

            modelBuilder.Entity<StaticSiteComponents>()
                   .HasKey(b => b.Id);
            //קשור של העסק המדווח לטבלת ביזנס
            //----------------Paid transaction to business----------------------
            //modelBuilder.Entity<PaidTransaction>()
            //       .HasOne(m => m.SupplierBusiness)
            //       .WithMany(t => t.GetPaidTransactionsSupplier)
            //       .HasForeignKey(m => m.SupplierBusinessId)
            //       .OnDelete(DeleteBehavior.ClientSetNull);

            //modelBuilder.Entity<PaidTransaction>()
            //        .HasOne(m => m.ConsumerBusiness)
            //       .WithMany(t => t.GetPaidTransactionsConsumer)
            //       .HasForeignKey(m => m.ConsumerBusinessId)
            //       .OnDelete(DeleteBehavior.ClientSetNull);
            ////------------------Barter deal to business--------------------
            //modelBuilder.Entity<BarterDeal>()
            //       .HasOne(m => m.ReportsBusiness)
            //      .WithMany(t => t.GetBarterDealReports)
            //      .HasForeignKey(m => m.ReportsBusinessId)
            //      .OnDelete(DeleteBehavior.ClientSetNull);
            //modelBuilder.Entity<BarterDeal>()
            //       .HasOne(m => m.PartnerBusiness)
            //      .WithMany(t => t.GetBarterDealPartner)
            //      .HasForeignKey(m => m.PartnerBusinessId)
            //      .OnDelete(DeleteBehavior.ClientSetNull);
            //------------------------------------------------------------------
            //modelBuilder.Entity<PaidTransaction>()
            //  .HasOne(s => s.SupplierBusiness)
            //  .WithMany()
            //  .HasForeignKey(s => s.SupplierBusinessId)
            //  .HasPrincipalKey(c => c.Id);
            //קשור של העסק המדווח לטבלת ביזנס
            //modelBuilder.Entity<PaidTransaction>()
            //  .HasOne(s => s.ConsumerBusiness)
            //  .WithMany()
            //  .HasForeignKey(s => s.ConsumerBusinessId)
            //  .HasPrincipalKey(c => c.Id);
            //קשור של העסק המדווח לטבלת ביזנס
            //modelBuilder.Entity<BarterDeal>()
            //  .HasOne(s => s.ReportsBusiness)
            //  .WithMany()
            //  .HasForeignKey(s => s.ReportsBusinessId)
            //  .HasPrincipalKey(c => c.Id);
            ////קשור של העסק השותף לטבלת ביזנס
            //modelBuilder.Entity<BarterDeal>()
            //  .HasOne(s => s.PartnerBusiness)
            //  .WithMany()
            //  .HasForeignKey(s => s.PartnerBusinessId)
            //  .HasPrincipalKey(c => c.Id);

            modelBuilder.Entity<BuisnessArea>()
            .HasOne(p => p.Area)
             .WithMany(b => b.BuisnessArea)
              .HasForeignKey(p => p.areaId);

            modelBuilder.Entity<BuisnessArea>()
             .HasOne(p => p.Buisness)
             .WithMany(b => b.BuisnessArea)
              .HasForeignKey(p => p.buisnessId);

            //modelBuilder.Entity<BuisnessCoverPicture>()
            // .HasOne(p => p.Buisness)
            // .WithMany(b => b.BuisnessCoverPicture)
            //  .HasForeignKey(p => p.buisnessId);

            //modelBuilder.Entity<BuisnessLogo>()
            // .HasOne(p => p.Buisness)
            // .WithMany(b => b.BuisnessLogo)
            //  .HasForeignKey(p => p.buisnessId);
            // modelBuilder.Entity<Message>()
            //  .HasOne(s => s.BusinessFrom)
            //  .WithMany()
            //  .HasForeignKey(s => s.FromEmail)
            //  .HasPrincipalKey(c => c.userId);

            // modelBuilder.Entity<MessagesTo>()
            // .HasOne(s => s.BuisnessTo)
            // .WithMany()
            // .HasForeignKey(s => s.Email)
            // .HasPrincipalKey(c => c.userId);
            modelBuilder.Entity<Buisness>()
           .HasOne(s => s.User)
           .WithMany()
           .HasForeignKey(s => s.userId)
           .HasPrincipalKey(c => c.Email);

            modelBuilder.Entity<BuisnessPicture>()
             .HasOne(p => p.Buisness)
             .WithMany(b => b.BuisnessPicture)
              .HasForeignKey(p => p.buisnessId);

            modelBuilder.Entity<BuisnessStatus>()
             .HasOne(p => p.Buisness)
             .WithMany(b => b.BuisnessStatus)
              .HasForeignKey(p => p.buisnessId);

            modelBuilder.Entity<BuisnessStatus>()
             .HasOne(p => p.Status)
             .WithMany(b => b.BuisnessStatus)
             .HasForeignKey(p => p.statusId);

            modelBuilder.Entity<BuisnessSubCategory>()
             .HasOne(p => p.Buisness)
              .WithMany(b => b.BuisnessSubCategory)
               .HasForeignKey(p => p.buisnessId);

            modelBuilder.Entity<BuisnessSubCategory>()
            .HasOne(p => p.CategorySubCategory)
              .WithMany(b => b.BuisnessSubCategory)
               .HasForeignKey(p => p.categorySubCategoryId);

            modelBuilder.Entity<BuisnessSubCategoryBarter>()
            .HasOne(p => p.Buisness)
            .WithMany(b => b.BuisnessSubCategoryBarter)
            .HasForeignKey(p => p.buisnessId);

            modelBuilder.Entity<BuisnessSubCategoryBarter>()
            .HasOne(p => p.CategorySubCategory)
            .WithMany(b => b.BuisnessSubCategoryBarter)
            .HasForeignKey(p => p.categorySubCategoryId);

            modelBuilder.Entity<CategorySubCategory>()
            .HasOne(p => p.Category)
             .WithMany(b => b.CategorySubCategory)
              .HasForeignKey(p => p.categoryId);
          
            modelBuilder.Entity<CategorySubCategory>()
            .HasOne(p => p.SubCategory)
             .WithMany(b => b.CategorySubCategory)
              .HasForeignKey(p => p.subCategoryId);

           

            if (_seeder != null)
            {
                _seeder.SeedAsync(modelBuilder);
            }

        }
    }
}















/* 
             new
             {
                 buisnessId = 2,
                 userId = "42de6c38006f",
                 buisnessName = "תמך",
                 discription = "קידום והעסקה",
                 subTopicId = 2,
                 password = "123456789",
                 profileImg = "https://www.w3schools.com/howto/img_avatar2.png",
                 cityId = 2,
                 emailAddress = "temech@gmail.com",
                 buisnessWebSiteLink = "https://uriba.visualstudio.com/WINDO/_sprints/backlog/WINDO%20Team/WINDO/sprint%201",
                 countryWide = true,
                 payingBuisness = true,
                 burterBuisness = true,
                 collaborationBuisness = true,
                 pictursList = "",
                 views = 5

             }            //   modelBuilder.Entity<Buisness>()
            //           .HasKey(b => b.buisnessId);

            //   modelBuilder.Entity<Topics>()
            //           .HasKey(b => b.topicId);

            //   modelBuilder.Entity<SubTopics>()
            //           .HasKey(b => b.subTopicId);

            //   modelBuilder.Entity<Services>()
            //           .HasKey(b => b.servicesId);

            //   modelBuilder.Entity<BuisnessServices>()
            //           .HasKey(b => b.buisnessServicesId);

            //   modelBuilder.Entity<ServiceType>()
            //           .HasKey(b => b.serviceTypeId);


            //   modelBuilder.Entity<Buisness>()
            //   .HasOne(p => p.Cities)
            //    .WithMany(b => b.Buisness)
            //     .HasForeignKey(p => p.cityId);


            //   modelBuilder.Entity<Buisness>()
            //   .HasOne(p => p.SubTopics)
            //    .WithMany(b => b.Buisness)
            //      .HasForeignKey(p => p.subTopicId);

            //   modelBuilder.Entity<SubTopics>()
            //   .HasOne(p => p.Topics)
            //     .WithMany(b => b.SubTopic)
            //      .HasForeignKey(p => p.topicId);

            //   modelBuilder.Entity<Services>()
            //   .HasOne(p => p.SubTopics)
            //    .WithMany(b => b.Services)
            //     .HasForeignKey(p => p.subTopicId);

            //   modelBuilder.Entity<BuisnessServices>()
            //.HasOne(p => p.ServiceType)
            // .WithMany(b => b.BuisnessServices)
            //  .HasForeignKey(p => p.serviceTypeId);

            //   modelBuilder.Entity<BuisnessServices>()
            //.HasOne(p => p.Services)
            // .WithMany(b => b.BuisnessServices)
            //  .HasForeignKey(p => p.servicesId);

            //   modelBuilder.Entity<BuisnessServices>()
            //.HasOne(p => p.Buisness)
            // .WithMany(b => b.BuisnessServices)
            //  .HasForeignKey(p => p.buisnessId);

 */
