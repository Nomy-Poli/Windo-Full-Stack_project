using CMS_CORE_NG.BL;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using ModelService.windoModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Windo.Models;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;

namespace CMS_CORE_NG.Controllers
{
    [Route("api/[controller]")]
    public class SearchCategoryController
    {
        public readonly SearchCategoryBl _bl;
        private readonly IMemoryCache _memoryCache;
        public SearchCategoryController(SearchCategoryBl bl, IMemoryCache memoryCache)
        {
            _bl = bl;
            _memoryCache = memoryCache;
        }

        //[HttpGet("GetListOfCategoryWithSubCategory")]
        //public List<CategoryVm> GetListOfCategoryWithSubCategory()
        //{
        //    try
        //    {
        //        List<CategoryVm> CategoryList = _bl.GetListOfCategoryWithSubCategory();
        //        return CategoryList;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
        [HttpGet("GetListOfCategorySubCategoryVm")]
        public List<CategorySubCategoryVm> GetListOfCategorySubCategoryVm()
        {
            try
            {
                if (!_memoryCache.TryGetValue(CasheKeyes.ListOfCategorySubCategoryFromCashe, out List<CategorySubCategoryVm> CatSubCatVmtFromCacheValue))
                {
                    CatSubCatVmtFromCacheValue = _bl.GetListOfCategorySubCategoryVm();
                    _memoryCache.Set(CasheKeyes.ListOfCategorySubCategoryFromCashe, CatSubCatVmtFromCacheValue, new MemoryCacheEntryOptions()
                        .SetAbsoluteExpiration(TimeSpan.FromHours(3)));
                    return CatSubCatVmtFromCacheValue;
                }

                return CatSubCatVmtFromCacheValue;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet("GetListOfCategory")]
        public List<CategoryVm> GetListOfCategory()
        {
            try
            {
                if (!_memoryCache.TryGetValue(CasheKeyes.ListOfCategoryFromCache, out List<CategoryVm> CatFromCacheValue))
                {
                    CatFromCacheValue = _bl.GetListOfCategory();
                    _memoryCache.Set(CasheKeyes.ListOfCategoryFromCache, CatFromCacheValue, new MemoryCacheEntryOptions()
                        .SetAbsoluteExpiration(TimeSpan.FromHours(3)));
                    return CatFromCacheValue;
                }

                return CatFromCacheValue;

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        [HttpGet("GetListOfSubCategory")]
        public List<SubCategoryVm> GetListOfSubCategory()
        {
            try
            {
                List<SubCategoryVm> SubCategoryList = _bl.GetListOfSubCategory();
                return SubCategoryList;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        #region category And SubCategory

        [HttpPost("createCategory")]
        public int createCategory([FromBody] Category model)
        {
            try
            {
                int id = -1;
                if (model != null)
                {
                    id = _bl.CreateCategory(model);
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

        [HttpPost("CreateSubCategory")]
        public int CreateSubCategory([FromBody] SubCategory model)
        {
            try
            {
                int id = -1;
                if (model != null)
                {
                    id = _bl.CreateSubCategory(model);
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
        [HttpPost("CreateSubCategoryCategory")]
        public int CreateSubCategoryCategory([FromBody] CategorySubCategory model)
        {
            try
            {
                int id = -1;
                if (model != null)
                {
                    id = _bl.CreateSubCategoryCategory(model);
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



