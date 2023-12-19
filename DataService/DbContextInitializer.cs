using System;
using System.Linq;
using System.Threading.Tasks;
using CountryService;
using FunctionalService;
using Microsoft.EntityFrameworkCore;

namespace DataService
{
    public static class DbContextInitializer
    {
        public static async Task Initialize(DataProtectionKeysContext dataProtectionKeysContext, ApplicationDbContext applicationDbContext, IFunctionalSvc functionalSvc, ICountrySvc countrySvc)
        {
            // Check, if db DataProtectionKeysContext is created
            // Check, if db ApplicationDbContext is created
            //await dataProtectionKeysContext.Database.EnsureCreatedAsync();
            await dataProtectionKeysContext.Database.MigrateAsync();
            //await applicationDbContext.Database.EnsureCreatedAsync();
            await applicationDbContext.Database.MigrateAsync();
            // Check, if db contains any users. If db is not empty, then db has been already seeded
            if (applicationDbContext.ApplicationUsers.Any())
            {
                return;
            }

            // If empty create Admin User and App User
            await functionalSvc.CreateDefaultAdminUser();
            await functionalSvc.CreateDefaultUser();

            // Populate Country database 
            var countries = await countrySvc.GetCountriesAsync();
            if (countries.Count > 0)
            {
                await applicationDbContext.Countries.AddRangeAsync(countries);
                await applicationDbContext.SaveChangesAsync();
            }

        }
    }
}
