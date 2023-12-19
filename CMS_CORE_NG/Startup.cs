using System;
using System.Linq;
using System.Text;
using ActivityService;
using AuthService;
using BackendService;
using CMS_CORE_NG.Extensions;
using CookieService;
using CountryService;
using DashboardService;
using DataService;
using EmailService;
using FiltersService;
using FunctionalService;
using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Hangfire;
using Hangfire.SqlServer;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using ModelService;
using RolesService;
using UserService;
using Windo.BL;
using Windo.Repository;
using AutoMapper;
using Windo.Mapper;
using CMS_CORE_NG.BL;
using CMS_CORE_NG.Repository;
using System.IO;
using CMS_CORE_NG.Seeder;
using Imageflow.Fluent;
using Imageflow.Server;
using Imageflow.Server.HybridCache;
using static CMS_CORE_NG.Scoring;

namespace CMS_CORE_NG
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;
            Env = env;
        }

        public IConfiguration Configuration { get; }
        private IWebHostEnvironment Env { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
            services.AddImageflowHybridCache(new HybridCacheOptions(Path.Combine(Env.ContentRootPath, "imageflow_cache"))
            {
                CacheSizeLimitInBytes = (long)1 * 1024 * 1024 * 1024, //1 GiB
            });
            // In production, the Angular files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "wwwroot/ng";
            });
            services.AddHttpClient();
     
            // Add Hangfire services.
            services.AddHangfire(configuration => configuration
                .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
                .UseSimpleAssemblyNameTypeSerializer()
                .UseRecommendedSerializerSettings()
                .UseSqlServerStorage(Configuration.GetConnectionString("HangfireConnection"), new SqlServerStorageOptions
                {
                    CommandBatchMaxTimeout = TimeSpan.FromMinutes(5),
                    SlidingInvisibilityTimeout = TimeSpan.FromMinutes(5),
                    QueuePollInterval = TimeSpan.Zero,
                    UseRecommendedIsolationLevel = true,
                    DisableGlobalLocks = true
                }));

            // Add the processing server as IHostedService
            services.AddHangfireServer();

            var homeFolder = (Environment.OSVersion.Platform == PlatformID.Unix ||
                   Environment.OSVersion.Platform == PlatformID.MacOSX)
                    ? Environment.GetEnvironmentVariable("HOME")
                    : Environment.ExpandEnvironmentVariables("%HOMEDRIVE%%HOMEPATH%");


            /*---------------------------------------------------------------------------------------------------*/
            /*                              Cookie Helper SERVICE                                                */
            /*---------------------------------------------------------------------------------------------------*/
            services.AddHttpContextAccessor();
            services.AddTransient<CookieOptions>();
            services.AddTransient<ICookieSvc, CookieSvc>();
            /*---------------------------------------------------------------------------------------------------*/
            /*                              DB CONNECTION OPTIONS                                                */
            /*---------------------------------------------------------------------------------------------------*/
            services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("CmsCoreNg_DEV"), x => x.MigrationsAssembly("CMS_CORE_NG")));

            services.AddDbContext<DataProtectionKeysContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("DataProtectionKeysContext"), x => x.MigrationsAssembly("CMS_CORE_NG")));

            services.AddTransient<IDataBaseSeeder, WindoDataBaseSeeder>();
            /*---------------------------------------------------------------------------------------------------*/
            /*                             Functional SERVICE                                                    */
            /*---------------------------------------------------------------------------------------------------*/
            services.AddTransient<IFunctionalSvc, FunctionalSvc>();
            services.Configure<AdminUserOptions>(Configuration.GetSection("AdminUserOptions"));
            services.Configure<AppUserOptions>(Configuration.GetSection("AppUserOptions"));
            /*---------------------------------------------------------------------------------------------------*/
            /*                             Writable SERVICE                                                      */
            /*---------------------------------------------------------------------------------------------------*/
            var sendGridOptionsSection = Configuration.GetSection("SendGridOptions");
            var smtpOptionsSection = Configuration.GetSection("SmtpOptions");
            var siteWideSettingsSection = Configuration.GetSection("SiteWideSettings");
            services.ConfigureWritable<SendGridOptions>(sendGridOptionsSection, "appsettings.json");
            services.ConfigureWritable<SmtpOptions>(smtpOptionsSection, "appsettings.json");
            services.ConfigureWritable<SiteWideSettings>(siteWideSettingsSection, "appsettings.json");
            /*---------------------------------------------------------------------------------------------------*/
            /*                              DEFAULT IDENTITY OPTIONS                                             */
            /*---------------------------------------------------------------------------------------------------*/
            var identityDefaultOptionsConfiguration = Configuration.GetSection("IdentityDefaultOptions");
            services.Configure<IdentityDefaultOptions>(identityDefaultOptionsConfiguration);
            var identityDefaultOptions = identityDefaultOptionsConfiguration.Get<IdentityDefaultOptions>();

            services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                // Password settings
                options.Password.RequireDigit = identityDefaultOptions.PasswordRequireDigit;
                options.Password.RequiredLength = identityDefaultOptions.PasswordRequiredLength;
                options.Password.RequireNonAlphanumeric = identityDefaultOptions.PasswordRequireNonAlphanumeric;
                options.Password.RequireUppercase = identityDefaultOptions.PasswordRequireUppercase;
                options.Password.RequireLowercase = identityDefaultOptions.PasswordRequireLowercase;
                options.Password.RequiredUniqueChars = identityDefaultOptions.PasswordRequiredUniqueChars;

                // Lockout settings
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(identityDefaultOptions.LockoutDefaultLockoutTimeSpanInMinutes);
                options.Lockout.MaxFailedAccessAttempts = identityDefaultOptions.LockoutMaxFailedAccessAttempts;
                options.Lockout.AllowedForNewUsers = identityDefaultOptions.LockoutAllowedForNewUsers;

                // User settings
                options.User.RequireUniqueEmail = identityDefaultOptions.UserRequireUniqueEmail;

                // email confirmation require
                options.SignIn.RequireConfirmedEmail = identityDefaultOptions.SignInRequireConfirmedEmail;

            }).AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders();
            /*---------------------------------------------------------------------------------------------------*/
            /*                              DATA PROTECTION SERVICE                                              */
            /*---------------------------------------------------------------------------------------------------*/
            var dataProtectionSection = Configuration.GetSection("DataProtectionKeys");
            services.Configure<DataProtectionKeys>(dataProtectionSection);
            services.AddDataProtection().PersistKeysToDbContext<DataProtectionKeysContext>();
            /*---------------------------------------------------------------------------------------------------*/
            /*                              USER HELPER SERVICE                                                  */
            /*---------------------------------------------------------------------------------------------------*/
            services.AddTransient<IUserSvc, UserSvc>();
            /*---------------------------------------------------------------------------------------------------*/
            /*                              USER Roles SERVICE                                                   */
            /*---------------------------------------------------------------------------------------------------*/
            services.AddTransient<IRoleSvc, RoleSvc>();
            /*---------------------------------------------------------------------------------------------------*/
            /*                                 APPSETTINGS SERVICE                                               */
            /*---------------------------------------------------------------------------------------------------*/
            var appSettingsSection = Configuration.GetSection("AppSettings");
            services.Configure<AppSettings>(appSettingsSection);
            /*---------------------------------------------------------------------------------------------------*/
            /*                                 JWT AUTHENTICATION SERVICE                                        */
            /*---------------------------------------------------------------------------------------------------*/
            var appSettings = appSettingsSection.Get<AppSettings>();
            var key = Encoding.ASCII.GetBytes(appSettings.Secret);
            services.AddAuthentication(o =>
            {
                o.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                o.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
                o.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = appSettings.ValidateIssuerSigningKey,
                    ValidateIssuer = appSettings.ValidateIssuer,
                    ValidateAudience = appSettings.ValidateAudience,
                    ValidIssuer = appSettings.Site,
                    ValidAudience = appSettings.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ClockSkew = TimeSpan.Zero

                };
            });
            /*---------------------------------------------------------------------------------------------------*/
            /*                             Email SERVICE                                                         */
            /*---------------------------------------------------------------------------------------------------*/
            services.Configure<SendGridOptions>(Configuration.GetSection("SendGridOptions"));
            services.Configure<SmtpOptions>(Configuration.GetSection("SmtpOptions"));
            services.AddTransient<IEmailSvc, EmailSvc>();
            /*---------------------------------------------------------------------------------------------------*/
            /*                              AUTH SERVICE                                                         */
            /*---------------------------------------------------------------------------------------------------*/
            services.AddTransient<IAuthSvc, AuthSvc>();
            /*---------------------------------------------------------------------------------------------------*/
            /*                              Admin SERVICE                                                        */
            /*---------------------------------------------------------------------------------------------------*/
            services.AddTransient<IAdminSvc, AdminSvc>();
            /*---------------------------------------------------------------------------------------------------*/
            /*                              ACTIVITY SERVICE                                                     */
            /*---------------------------------------------------------------------------------------------------*/
            services.AddTransient<IActivitySvc, ActivitySvc>();
            /*---------------------------------------------------------------------------------------------------*/
            /*                              Country SERVICE                                                      */
            /*---------------------------------------------------------------------------------------------------*/
            services.AddTransient<ICountrySvc, CountrySvc>();
            /*---------------------------------------------------------------------------------------------------*/
            /*                             Dashboard SERVICE                                                     */
            /*---------------------------------------------------------------------------------------------------*/
            services.AddTransient<IDashboardSvc, DashboardSvc>();
            /*---------------------------------------------------------------------------------------------------*/
            /*                               Mapper SERVICES                                                     */
            /*---------------------------------------------------------------------------------------------------*/
            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });
            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);
            //services.AddAutoMapper(typeof(MappingProfile));
            //services.AddAutoMapper(Startup);
            /*---------------------------------------------------------------------------------------------------*/
            /*                               windo SERVICES                                                     */
            /*---------------------------------------------------------------------------------------------------*/
            services.AddScoped<BuisnessBl>();
            services.AddScoped<CollaborationBl>();
            services.AddScoped<BuisnessRepository>();
            services.AddScoped<SearchCategoryBl>();
            services.AddScoped<SearchCategoryRepository>();
            services.AddScoped<CollaborationsRepository>();
            services.AddScoped<IMessageBl,MessageBl>();
            services.AddScoped<IMessageRepo,MessageRepository>();
            services.AddScoped<INotesBl, NotesBl>();
            services.AddScoped<INotesRepo, NotesRepository>();
            services.AddScoped<IAdvertismentBl, AdvertismentBl>();
            services.AddScoped<IAdvertismentRepo, AdvertismentRepository>();
            services.AddScoped<busoftExcelImporter.ExcelImporter>();
            services.AddScoped<IBackGroungTask,BackgroundTask>();
            services.AddScoped<IScoringBl, ScoringBl>();
            services.AddScoped<IScoringRepo, ScoringRepository>();
            services.AddScoped<IScoring,Scoring>();
            services.AddScoped<INetworkingBl, NetworkingBl>();
            services.AddScoped<INetworkingRepo, NetworkingRepository>();

            /*---------------------------------------------------------------------------------------------------*/
            /*                                 AuthenticationSchemes SERVICE                                     */
            /*---------------------------------------------------------------------------------------------------*/
            services.AddAuthentication("Administrator").AddScheme<AdminAuthenticationOptions, AdminAuthenticationHandler>("Admin", null);
            services.AddAuthentication("User").AddScheme<UserAuthenticationOptions, UserAuthenticationHandler>("User", null);
            /*---------------------------------------------------------------------------------------------------*/
            /*                              ENABLE CORS                                                          */
            /*---------------------------------------------------------------------------------------------------*/
            services.AddCors(options =>
            {
                options.AddPolicy("EnableCORS", builder =>
                {
                    builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod().Build();
                });
            });

            /*---------------------------------------------------------------------------------------------------*/
            /*                              ENABLE API Versioning                                                */
            /*---------------------------------------------------------------------------------------------------*/
            services.AddApiVersioning(
               options =>
               {
                   options.ReportApiVersions = true;
                   options.AssumeDefaultVersionWhenUnspecified = true;
                   options.DefaultApiVersion = new ApiVersion(1, 0);
               });
            /*---------------------------------------------------------------------------------------------------*/
            /*                                 Razor Pages Runtime SERVICE                                       */
            /* Add Microsoft.AspNetCore.Mvc.Razor.RuntimeCompilation NuGet package to the project                */
            /* Surprised that refreshing a view while the app is running did not work                            */
            /*---------------------------------------------------------------------------------------------------*/
            services.AddMvc().AddControllersAsServices().AddRazorRuntimeCompilation().SetCompatibilityVersion(CompatibilityVersion.Version_3_0);
            /*--------------------------------------------------------------------------------------------------------------------*/
            /*                      Anti Forgery Token Validation Service                                                         */
            /* We use the option patterm to configure the Antiforgery feature through the AntiForgeryOptions Class                */
            /* The HeaderName property is used to specify the name of the header through which antiforgery token will be accepted */
            /*--------------------------------------------------------------------------------------------------------------------*/
            services.AddAntiforgery(options =>
            {
                options.HeaderName = "X-XSRF-TOKEN";
            });
            /*---------------------------------------------------------------------------------------------------*/
            /*                              ENABLE API Versioning                                                */
            /*---------------------------------------------------------------------------------------------------*/

            /*---------------------------------------------------------------------------------------------------*/
            services.AddControllers()
           .AddJsonOptions(options =>
              options.JsonSerializerOptions.PropertyNamingPolicy=null);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IAntiforgery antiforgery)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            } 
            app.UseHttpsRedirection();
            app.UseCors("EnableCORS");
           
            
            
            //UseFileServer combines the functionality of UseStaticFiles, UseDefaultFiles, and UseDirectoryBrowser.
            //app.UseFileServer();
            //if (!env.IsDevelopment())
            //{
            app.UseSpaStaticFiles();

            //}
            app.UseImageflow(new ImageflowMiddlewareOptions()
                .SetMapWebRoot(true)
                .SetDiagnosticsPageAccess(AccessDiagnosticsFrom.AnyHost)
                .SetAllowCaching(true)
                .SetDefaultCacheControlString("public, max-age=86400")
                .SetJobSecurityOptions(new SecurityOptions()
                .SetMaxDecodeSize(new FrameSizeLimit(8000, 8000, 40))
                .SetMaxFrameSize(new FrameSizeLimit(8000, 8000, 40))
                .SetMaxEncodeSize(new FrameSizeLimit(8000, 8000, 20)))
);
            app.UseHangfireDashboard("/hangfire", new DashboardOptions
            {
                Authorization = new[] { new MyAuthorizationFilter() }
            });
            //הקפאנו לבינתיים את הדיוורים האוטומטיים
            //RecurringJob.AddOrUpdate<IBackGroungTask>(bgtask => bgtask.getEmailsToSenDaily(), Cron.Daily(8));
            app.UseRouting();
            app.UseAuthorization();

            /* Configure the app to provide a token in a cookie called XSRF-TOKEN */
            /* Custom Middleware Component is required to Set the cookie which is named XSRF-TOKEN 
             * The Value for this cookie is obtained from IAntiForgery service
             * We must configure this cookie with HttpOnly option set to false so that browser will allow JS to read this cookie
             */
            app.Use(nextDelegate => context =>
            {
                string path = context.Request.Path.Value.ToLower();
                string[] directUrls = { "/admin", "/store", "/cart", "checkout", "/login" };
                if (path.StartsWith("/swagger") || path.StartsWith("/api") || string.Equals("/", path) || directUrls.Any(url => path.StartsWith(url)))
                {
                    AntiforgeryTokenSet tokens = antiforgery.GetAndStoreTokens(context);
                    context.Response.Cookies.Append("XSRF-TOKEN", tokens.RequestToken, new CookieOptions()
                    {
                        HttpOnly = false,
                        Secure = false,
                        IsEssential = true,
                        SameSite = SameSiteMode.Strict
                    });

                }

                
                return nextDelegate(context);
            });

            app.UseEndpoints(endpoints =>
            {

                endpoints.MapControllerRoute(
                   name: "areas",
                   pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller}/{action=Index}/{id?}");
                endpoints.MapHangfireDashboard();

            });

            app.UseSpa(spa =>
            {
                // To learn more about options for serving an Angular SPA from ASP.NET Core,
                // see https://go.microsoft.com/fwlink/?linkid=864501

                spa.Options.SourcePath = "wwwroot/ng";

                //if (env.IsDevelopment())
                //{
                //    spa.UseAngularCliServer(npmScript: "start");
                //}
            });

            // catch-all handler for HTML5 client routes - serve index.html
            //app.Run(async context =>
            //{
            //    // Make sure Angular output was created in wwwroot
            //    // Running Angular in dev mode nukes output folder!
            //    // so it could be missing.
            //    if (string.IsNullOrWhiteSpace(env.WebRootPath))
            //    {
            //        env.WebRootPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
            //    }

            //    context.Response.ContentType = "text/html";
            //    await context.Response.SendFileAsync(Path.Combine(env.WebRootPath, "index.html"));
            //});
            //seeder.SeedAsync();
        }
    }
}


