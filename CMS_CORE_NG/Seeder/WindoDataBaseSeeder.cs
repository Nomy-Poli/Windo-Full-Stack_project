using DataService;
using Microsoft.EntityFrameworkCore;
using ModelService.windoModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Windo.Models;

namespace CMS_CORE_NG.Seeder
{
    public class WindoDataBaseSeeder : IDataBaseSeeder
    {
        private ModelBuilder _modelBuilder;
        //public WindoDataBaseSeeder(ModelBuilder modelBuilder)
        //{
        //    _modelBuilder = modelBuilder;
        //}
        public static string GetFullFilePath(string fileName)
        {
            string codeBase = Assembly.GetExecutingAssembly().CodeBase;
            UriBuilder uri = new UriBuilder(codeBase);
            string path = Uri.UnescapeDataString(uri.Path);
            return Path.GetDirectoryName(path) + @"/Seeder/Data/" + fileName;
        }
        public void SeedAsync(ModelBuilder modelBuilder)
        {
            _modelBuilder = modelBuilder;
            SeedCategories();
            SeedSubCategories();
            SeedCategorySubCategories();
            seedScoringEventType();
            seedScoringAction();
            seedScoringOpertion();


        }

        private void SeedCategories()
        {
            var fileData = File.ReadAllText(GetFullFilePath("Category.json"));
            var fileCollection = JsonConvert.DeserializeObject<IEnumerable<Category>>(fileData);

            foreach (var item in fileCollection)
            {
                _modelBuilder.Entity<Category>().HasData(new
                {
                    item.Id,
                    item.name,
                });
            }
        }

        private void SeedSubCategories()
        {
            var fileData = File.ReadAllText(GetFullFilePath("SubCategory.json"));
            var fileCollection = JsonConvert.DeserializeObject<IEnumerable<SubCategory>>(fileData);

            foreach (var item in fileCollection)
            {
                _modelBuilder.Entity<SubCategory>().HasData(new
                {
                    item.Id,
                    item.name,
                });
            }
        }

        private void SeedCategorySubCategories()
        {
            var fileData = File.ReadAllText(GetFullFilePath("CategorySubCategory.json"));
            var fileCollection = JsonConvert.DeserializeObject<IEnumerable<CategorySubCategory>>(fileData);

            foreach (var item in fileCollection)
            {
                _modelBuilder.Entity<CategorySubCategory>().HasData(new
                {
                    item.Id,
                    item.categoryId,
                    item.subCategoryId
                });
            }
        }
        private void seedScoringEventType()
        {
            var fileData = File.ReadAllText(GetFullFilePath("ScoringEventType.json"));
            var fileCollection = JsonConvert.DeserializeObject<IEnumerable<ScoringEventType>>(fileData);
            foreach (var item in fileCollection)
            {
                _modelBuilder.Entity<ScoringEventType>().HasData(new
                {
                    item.Id,
                    item.EventDescription
                });
            }

        }

        private void seedScoringAction()
        {
            var fileData = File.ReadAllText(GetFullFilePath("ScoringAction.json"));
            var fileCollection = JsonConvert.DeserializeObject<IEnumerable<ScoringAction>>(fileData);
            foreach (var item in fileCollection)
            {
                _modelBuilder.Entity<ScoringAction>().HasData(new
                {
                    item.Id,
                    item.ActionName,
                    item.EventTypeId
                });
            }

        }

        private void seedScoringOpertion()
        {
            var fileData = File.ReadAllText(GetFullFilePath("ScoringOpertion.json"));
            var fileCollection = JsonConvert.DeserializeObject<IEnumerable<ScroingOperation>>(fileData);
            foreach (var item in fileCollection)
            {
                _modelBuilder.Entity<ScroingOperation>().HasData(new
                {
                    item.Id,
                    item.ActionId,
                    item.EventTypeId,
                    item.Count,
                    item.FromDate
                });
            }

        }

    }
}
