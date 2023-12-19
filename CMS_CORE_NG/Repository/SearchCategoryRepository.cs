using DataService;
using ModelService.windoModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Windo.Models;
using Microsoft.EntityFrameworkCore;




namespace CMS_CORE_NG.Repository
{
    public class SearchCategoryRepository
    {
        public readonly ApplicationDbContext _db;
        public SearchCategoryRepository(ApplicationDbContext db)
        {
            _db = db;
        }
        public List<Category> GetListOfCategory()
        {
            try
            {
                List<Category> CategoryList = new List<Category>();
                var temp = (from b in _db.Category select b);
                CategoryList = temp.ToList();
                return CategoryList;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public List<SubCategory> GetListOfSubCategory()
        {
            try
            {
                List<SubCategory> SubCategoryList = new List<SubCategory>();
                var temp = (from b in _db.SubCategory select b);
                SubCategoryList = temp.ToList();
                return SubCategoryList;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public List<CategorySubCategory> GetListOfCategorySubCategory()
        {
            try
            {
                List<CategorySubCategory> CategorySubCategoryList = new List<CategorySubCategory>();

                var temp = _db.CategorySubCategory.Include(x => x.Category)
                    .ThenInclude(sc => sc.CategorySubCategory)
                    .ThenInclude(sc => sc.SubCategory);

                CategorySubCategoryList = temp.ToList();

                //var temp = (from b in _db.CategorySubCategory select b);
                //if (temp != null) { CategorySubCategoryList = temp.ToList(); }
               
                return CategorySubCategoryList;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        #region category And SubCategory
        public Category GetCategoryById(int id)
        {
            try
            {
                Category Category = new Category();
                if (id >= 0)//  ביטוי אחר ל-לא שווה נל 
                {
                    Category = _db.Category.FirstOrDefault(b => b.Id == id);
                }
                return Category;
            }
            catch (Exception e)
            {

                throw e;
            }
        }
        public SubCategory GetSubCategoryById(int id)
        {
            try
            {
                SubCategory SubCategory = new SubCategory();
                if (id >= 0)//  ביטוי אחר ל-לא שווה נל 
                {
                    SubCategory = _db.SubCategory.FirstOrDefault(b => b.Id == id);
                }
                return SubCategory;
            }
            catch (Exception e)
            {

                throw e;
            }
        }
        public int CreateCategory(Category Category)
        {
            try
            {
                int id = 0;
                _db.Category.Add(Category);
                _db.SaveChanges();
                id = Category.Id;
                return id;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int CreateSubCategory(SubCategory SubCategory)
        {
            try
            {
                int id = 0;
                _db.SubCategory.Add(SubCategory);
                _db.SaveChanges();
                id = SubCategory.Id;
                return id;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int CreateSubCategoryCategory(CategorySubCategory CategorySubCategory)
        {
            try
            {
                int id = 0;
                _db.CategorySubCategory.Add(CategorySubCategory);
                _db.SaveChanges();
                id = CategorySubCategory.Id;
                return id;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        //public List<Buisness> GetListOfBuisnessesWithParameters(SearchFilterVm filterObject)
        //{
        //    try
        //    {
        //        if (filterObject != null)
        //        {
        //            bool isTopicListFull = filterObject.topicsList == null ? false : true;
        //            bool isSubTopicListFull = filterObject.subTopicsList == null ? false : true;
        //            bool isTagsListFull = filterObject.tagsList == null ? false : true;
        //            bool isServicesListFull = filterObject.servicesList == null ? false : true;

        //            List<Buisness> buisnessesList = new List<Buisness>();
        //            var temp = (from b in _db.Buisness select b);

        //            if (isTopicListFull)
        //            {
        //                foreach (var topicId in filterObject.topicsList)
        //                {
        //                    temp = temp.Where(t => t.SubTopics.topicId == topicId);
        //                }
        //            }
        //            if (isSubTopicListFull)
        //            {
        //                foreach (var subtopicId in filterObject.subTopicsList)
        //                {
        //                    temp = temp.Where(t => t.SubTopics.subTopicId == subtopicId);
        //                }
        //            }
        //            //if (isTagsListFull)
        //            //{
        //            //    foreach (var tag in filterObject.tagsList)
        //            //    {
        //            //    }
        //            //}
        //            //if (isServicesListFull)
        //            //{
        //            //    foreach (var tag in filterObject.tagsList)
        //            //    {
        //            //    }
        //            //}

        //            buisnessesList = temp.ToList();
        //            return buisnessesList;
        //        }
        //        else return null;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }

        //}
    }
}
