using AutoMapper;
using CMS_CORE_NG.Repository;
using ModelService.windoModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Windo.Models;

namespace CMS_CORE_NG.BL
{
    public class SearchCategoryBl
    {
        public readonly IMapper _mapper;
        readonly SearchCategoryRepository _repository;
        public SearchCategoryBl(IMapper mapper, SearchCategoryRepository repository)
        {
            _repository = repository;
            _mapper = mapper;
        }
        //public List<CategoryVm> GetListOfCategoryWithSubCategory()
        //{
        //    try
        //    {
        //        List<Category> CategoryList = _repository.GetListOfCategory();
        //        List<SubCategory> subCategoryList = _repository.GetListOfSubCategory();
        //        List<CategoryVm> categoriesListVm = new List<CategoryVm>();
        //        if (CategoryList != null)
        //        {
        //            categoriesListVm = _mapper.Map<List<Category>,List<CategoryVm>>(CategoryList);
        //            if (subCategoryList != null)
        //            {
        //                //insert to each topic all his sub topics
        //                foreach (var topic in categoriesListVm)
        //                {
        //                    topic.subCategoryList = new List<SubCategoryVm>();
        //                    foreach (var subTopic in subCategoryList)
        //                    {
        //                        if (subTopic.Id==topic.Id)
        //                        {
        //                            SubCategoryVm temoSubTopicVm=_mapper.Map <SubCategoryVm>(subTopic);
        //                            topic.subCategoryList.Add(temoSubTopicVm);
        //                           // subTopicsList.Remove(subTopic);
        //                        }
        //                    }
        //                }
        //            }
        //        }
        //        return categoriesListVm;
        //    }
        //    catch (Exception ex)
        //    {

        //        throw ex;
        //    }
        //}
        public List<CategoryVm> GetListOfCategory()
        {
            try
            {
                List<Category> CategoryList = _repository.GetListOfCategory();
                List<CategoryVm> CategoryListVm = new List<CategoryVm>();
                if (CategoryList != null)
                {
                    CategoryListVm = _mapper.Map<List<Category>, List<CategoryVm>>(CategoryList);
                    return CategoryListVm;
                }
                else return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public List<SubCategoryVm> GetListOfSubCategory()
        {
            try
            {
                List<SubCategory> subCategoryList = _repository.GetListOfSubCategory();
                List<SubCategoryVm> subCategoryListVm = new List<SubCategoryVm>();
                if (subCategoryList != null)
                {
                    subCategoryListVm = _mapper.Map<List<SubCategory>, List<SubCategoryVm>>(subCategoryList);
                    return subCategoryListVm;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<CategorySubCategoryVm> GetListOfCategorySubCategoryVm()
        {
            try
            {
                //TODO:get the list from cache . 

                List<CategorySubCategory> CategorySubCategoryList = _repository.GetListOfCategorySubCategory();


                List<CategorySubCategoryVm> CategorySubCategoryListVm = new List<CategorySubCategoryVm>();
                if (CategorySubCategoryList != null)
                {
                    CategorySubCategoryListVm = _mapper.Map<List<CategorySubCategory>, List<CategorySubCategoryVm>>(CategorySubCategoryList);
                    int i = 0;
                    foreach (var item in CategorySubCategoryListVm)
                    {
                        if (item.id == CategorySubCategoryList[i].Id)
                        {
                            if (CategorySubCategoryList[i].Category != null)
                                item.categoryName = CategorySubCategoryList[i].Category.name;
                            else
                            {
                                CategorySubCategoryList[i].Category = _repository.GetCategoryById(CategorySubCategoryList[i].categoryId);
                                item.categoryName = CategorySubCategoryList[i].Category.name;
                            }
                            if (CategorySubCategoryList[i].SubCategory != null)
                                item.subCategoryName = CategorySubCategoryList[i].SubCategory.name;
                            else
                            {
                                CategorySubCategoryList[i].SubCategory = _repository.GetSubCategoryById(CategorySubCategoryList[i].subCategoryId);
                                item.subCategoryName = CategorySubCategoryList[i].SubCategory.name;
                            }
                        }
                        i++;
                    }
                    return CategorySubCategoryListVm;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #region category And SubCategory
        public int CreateCategory(Category model)
        {
            try
            {
                int id = -1;
                if (model != null)
                {
                    id = _repository.CreateCategory(model);
                }
                if (id >= 0)
                    return id;
                else
                    return -1;
            }
            catch (Exception e)
            {

                throw e;
            }

        }
        public int CreateSubCategory(SubCategory model)
        {
            try
            {
                int id = -1;
                if (model != null)
                {
                    id = _repository.CreateSubCategory(model);
                }
                if (id >= 0)
                    return id;
                else
                    return -1;
            }
            catch (Exception e)
            {

                throw e;
            }

        }
        public int CreateSubCategoryCategory(CategorySubCategory model)
        {
            try
            {
                int id = -1;
                if (model != null)
                {
                    id = _repository.CreateSubCategoryCategory(model);
                }
                if (id >= 0)
                    return id;
                else
                    return -1;
            }
            catch (Exception e)
            {

                throw e;
            }

        }
        #endregion
    }
}
