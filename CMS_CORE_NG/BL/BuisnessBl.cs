using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using ModelService;
using ModelService.windoModels;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Windo.Controllers;
using Windo.Models;
using Windo.Repository;
using static CMS_CORE_NG.Scoring;

namespace Windo.BL
{
    public class BuisnessBl : IBuisnessBl
    {
        public readonly IMapper _mapper;
        public readonly BuisnessRepository _repository;
        private IConfiguration _configuration;
        private readonly IScoring _scoring;

        public BuisnessBl(IMapper mapper, BuisnessRepository repository, IConfiguration configuration, IScoring scoring)
        {
            _mapper = mapper;
            _repository = repository;
            _configuration = configuration;
            _scoring = scoring;
        }
        public BuisnessVm GetBuisnessByEmailId(string email, int? currentBusinessId)
        {
            //if(currentBusinessId != null)
            //{
            //    _scoring.ScoreBusiness(9,(int)currentBusinessId);
            //}
            //todo להכניס גם את התאריך האחרון
            BuisnessVm buisnessVm = null;
            try
            {
                var buisness = _repository.GetBuisnessByEmailId(email);
                if (buisness != null)
                {
                    //buisness + area + pictures
                    buisnessVm = _mapper.Map<BuisnessVm>(buisness);
                    #region categories
                    //categories
                    //של הקטגוריות שנבחרו ID שליפת כל ה
                    var categories = buisness.BuisnessSubCategory.Select(x => x.CategorySubCategory.categoryId).Distinct().ToList();

                    if (categories != null)
                    {
                        buisnessVm.buisnessCategory1 = new List<BuisnessCategoryVm>();
                        buisnessVm.buisnessCategory2 = new List<BuisnessCategoryVm>();
                        buisnessVm.buisnessCategory3 = new List<BuisnessCategoryVm>();
                        buisnessVm.buisnessCategory4 = new List<BuisnessCategoryVm>();
                        //all b sc for category 1
                        var CatSubCat1 = buisness.BuisnessSubCategory.Select(x => x.CategorySubCategory).Where(x => x.categoryId == categories[0]).ToList();
                        CatSubCat1.ForEach(c =>
                        {
                            buisnessVm.buisnessCategory1.Add(new BuisnessCategoryVm()
                            {
                                businessId = buisnessVm.id,
                                categoryId = c.categoryId,
                                subCategoryId = c.subCategoryId,
                                combinationtId = c.Id,
                                categoryName = c.Category.name,
                                subCategoryName = c.SubCategory.name,
                                isPossibleInBarter = c.BuisnessSubCategory.Select(x => x.isPossibleInBarter).FirstOrDefault()
                                //isPossibleInBarter = c.BuisnessSubCategory.OfType<BuisnessSubCategory>().Where(x => x.isPossibleInBarter == true).FirstOrDefault(),
                            });
                        });
                        if (categories.Count > 1)
                        {
                            //all b sc for category 2
                            var CatSubCat2 = buisness.BuisnessSubCategory.Select(x => x.CategorySubCategory).Where(x => x.categoryId == categories[1]).ToList();

                            CatSubCat2.ForEach(c =>
                            {
                                buisnessVm.buisnessCategory2.Add(new BuisnessCategoryVm()
                                {
                                    businessId = buisnessVm.id,
                                    categoryId = c.categoryId,
                                    subCategoryId = c.subCategoryId,
                                    combinationtId = c.Id,
                                    categoryName = c.Category.name,
                                    subCategoryName = c.SubCategory.name,
                                    isPossibleInBarter = c.BuisnessSubCategory.Select(x => x.isPossibleInBarter).FirstOrDefault()
                                });
                            });
                        }
                        if (categories.Count > 2)
                        {
                            //all b sc for category 3
                            var CatSubCat3 = buisness.BuisnessSubCategory.Select(x => x.CategorySubCategory).Where(x => x.categoryId == categories[2]).ToList();

                            CatSubCat3.ForEach(c =>
                            {
                                buisnessVm.buisnessCategory3.Add(new BuisnessCategoryVm()
                                {
                                    businessId = buisnessVm.id,
                                    categoryId = c.categoryId,
                                    subCategoryId = c.subCategoryId,
                                    combinationtId = c.Id,
                                    categoryName = c.Category.name,
                                    subCategoryName = c.SubCategory.name,
                                    isPossibleInBarter = c.BuisnessSubCategory.Select(x => x.isPossibleInBarter).FirstOrDefault()
                                });
                            });
                        }
                        if (categories.Count > 3)
                        {
                            //all b sc for category 4
                            var CatSubCat4 = buisness.BuisnessSubCategory.Select(x => x.CategorySubCategory).Where(x => x.categoryId == categories[3]).ToList();

                            CatSubCat4.ForEach(c =>
                            {
                                buisnessVm.buisnessCategory4.Add(new BuisnessCategoryVm()
                                {
                                    businessId = buisnessVm.id,
                                    categoryId = c.categoryId,
                                    subCategoryId = c.subCategoryId,
                                    combinationtId = c.Id,
                                    categoryName = c.Category.name,
                                    subCategoryName = c.SubCategory.name,
                                    isPossibleInBarter = c.BuisnessSubCategory.Select(x => x.isPossibleInBarter).FirstOrDefault()
                                });
                            });
                        }
                    }
                    #endregion

                    #region sub category for burter
                    // sub category for burter
                    var categoriesForBurter = buisness.BuisnessSubCategoryBarter.Select(x => x.CategorySubCategory.categoryId).Distinct().ToList();
                    buisnessVm.buisnessBarterCategory1 = new List<BuisnessBarterCategoryVm>();
                    buisnessVm.buisnessBarterCategory2 = new List<BuisnessBarterCategoryVm>();
                    if (categoriesForBurter != null)
                    {

                        if (categoriesForBurter.Count >= 1)
                        {
                            var CatSubCatForBurter1 = buisness.BuisnessSubCategoryBarter.Select(x => x.CategorySubCategory).Where(x => x.categoryId == categoriesForBurter[0]).ToList();
                            CatSubCatForBurter1.ForEach(c =>
                            {
                                buisnessVm.buisnessBarterCategory1.Add(new BuisnessBarterCategoryVm()
                                {
                                    businessId = buisnessVm.id,
                                    categoryId = c.categoryId,
                                    combinationtId = c.Id,
                                    subCategoryId = c.subCategoryId,
                                    categoryName = c.Category.name,
                                    subCategoryName = c.SubCategory.name
                                });
                            });
                        }

                        if (categoriesForBurter.Count > 1)
                        {
                            var CatSubCatForBurter2 = buisness.BuisnessSubCategoryBarter.Select(x => x.CategorySubCategory).Where(x => x.categoryId == categoriesForBurter[1]).ToList();
                            CatSubCatForBurter2.ForEach(sc =>
                            {
                                buisnessVm.buisnessBarterCategory2.Add(new BuisnessBarterCategoryVm()
                                {
                                    businessId = buisnessVm.id,
                                    categoryId = sc.categoryId,
                                    combinationtId = sc.Id,
                                    subCategoryId = sc.subCategoryId,
                                    categoryName = sc.Category.name,
                                    subCategoryName = sc.SubCategory.name
                                });
                            });
                        }
                    }
                    #endregion

                    #region areas

                    var areaList = buisness.BuisnessArea.Select(x => x).Where(x => x.buisnessId == buisness.Id).ToList();
                    buisnessVm.buisnessAreaList1 = new List<BuisnessAreaVm>();
                    areaList.ForEach(ab =>
                    {
                        buisnessVm.buisnessAreaList1.Add(new BuisnessAreaVm()
                        {
                            id = ab.Id,
                            buisnessId = buisnessVm.id,
                            areaId = ab.areaId
                        });
                    });
                    #endregion

                    #region work pic

                    var workPic = buisness.BuisnessPicture.Select(x => x).Where(x => x.buisnessId == buisness.Id).ToList();
                    buisnessVm.workPictureGuide = new List<GuideModel>();
                    workPic.ForEach(wpb =>
                    {
                        buisnessVm.workPictureGuide.Add(new GuideModel
                        {
                            workPicGuide = wpb.buisnessPictureId,
                            picindex = wpb.numberOfPicture
                        });
                    });
                    #endregion
                    var sb = buisness.BuisnessStatus.Select(x => x.startDate).FirstOrDefault();
                    buisnessVm.lastupdatedStartDate = sb;
                    //if (buisness.isdisplayBusinessOwnerName == true)
                    //{
                    //    buisnessVm.ownerName = buisness.User.Firstname; 
                    //}
                    return buisnessVm;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public BusinessNamesPicUserIdVM GetCurrentBuisnessByEmail(string email)
        {
            var business = _mapper.Map<BusinessNamesPicUserIdVM>(_repository.GetBuisnessByEmailId(email));
            return business;
        }
        //get all the business
        public List<BusinessForCardVM> GetListOfBuisnesses()
        {
            //todo - check thats right
            try
            {
                List<Buisness> ModelbuisnessesList = _repository.GetListOfBuisnesses();
                List<BusinessForCardVM> buisnessVm = ModelbuisnessesList.Select(_mapper.Map<Buisness, BusinessForCardVM>).ToList();              
                buisnessVm = buisnessVm
                    .OrderByDescending(x => x.lastupdatedStartDate)
                    .OrderByDescending(x => x.Score)
                   . ToList();
                #region categories and burterCategories
                //עוברים על העסקים
                ModelbuisnessesList.ForEach(m =>
                {
                    //רשימת הקטגוריות של העסק הנוכחי
                    var catList = m.BuisnessSubCategory.Select(x => x.CategorySubCategory.categoryId).Distinct().ToList();
                    var catBurterList = m.BuisnessSubCategoryBarter.Select(x => x.CategorySubCategory.categoryId).Distinct().ToList();
                    buisnessVm.ForEach(bvm =>
                    {
                        if (m.Id == bvm.id)
                        {
                            if (catList != null)
                            {
                                var CatSubCat1 = m.BuisnessSubCategory.Select(x => x.CategorySubCategory).Where(x => x.categoryId == catList[0]).ToList();
                                bvm.buisnessCategory1 = new List<BuisnessCategoryVm>();
                                bvm.buisnessCategory2 = new List<BuisnessCategoryVm>();
                                bvm.buisnessCategory3 = new List<BuisnessCategoryVm>();
                                bvm.buisnessCategory4 = new List<BuisnessCategoryVm>();
                                CatSubCat1.ForEach(c =>
                                {
                                    bvm.buisnessCategory1.Add(new BuisnessCategoryVm()
                                    {
                                        businessId = bvm.id,
                                        categoryId = c.categoryId,
                                        categoryName = c.Category.name,
                                        subCategoryName = c.SubCategory.name,
                                        subCategoryId = c.subCategoryId,
                                        combinationtId = c.Id,
                                        isPossibleInBarter = c.BuisnessSubCategory.Select(x => x.isPossibleInBarter).FirstOrDefault()
                                    }); ;
                                });

                                if (catList.Count > 1)
                                {
                                    var CatSubCat2 = m.BuisnessSubCategory.Select(x => x.CategorySubCategory).Where(x => x.categoryId == catList[1]).ToList();
                                    CatSubCat2.ForEach(c =>
                                    {
                                        bvm.buisnessCategory2.Add(new BuisnessCategoryVm()
                                        {
                                            businessId = bvm.id,
                                            categoryId = c.categoryId,
                                            categoryName = c.Category.name,
                                            subCategoryName = c.SubCategory.name,
                                            subCategoryId = c.subCategoryId,
                                            combinationtId = c.Id,
                                            isPossibleInBarter = c.BuisnessSubCategory.Select(x => x.isPossibleInBarter).FirstOrDefault()
                                        });
                                    });
                                }
                                if (catList.Count > 2)
                                {
                                    var CatSubCat3 = m.BuisnessSubCategory.Select(x => x.CategorySubCategory).Where(x => x.categoryId == catList[2]).ToList();
                                    CatSubCat3.ForEach(c =>
                                    {
                                        bvm.buisnessCategory3.Add(new BuisnessCategoryVm()
                                        {
                                            businessId = bvm.id,
                                            categoryId = c.categoryId,
                                            subCategoryId = c.subCategoryId,
                                            categoryName = c.Category.name,
                                            subCategoryName = c.SubCategory.name,
                                            combinationtId = c.Id,
                                            isPossibleInBarter = c.BuisnessSubCategory.Select(x => x.isPossibleInBarter).FirstOrDefault()
                                        });
                                    });
                                }
                                if (catList.Count > 3)
                                {
                                    var CatSubCat4 = m.BuisnessSubCategory.Select(x => x.CategorySubCategory).Where(x => x.categoryId == catList[3]).ToList();
                                    CatSubCat4.ForEach(c =>
                                    {
                                        bvm.buisnessCategory4.Add(new BuisnessCategoryVm()
                                        {
                                            businessId = bvm.id,
                                            categoryId = c.categoryId,
                                            categoryName = c.Category.name,
                                            subCategoryName = c.SubCategory.name,
                                            subCategoryId = c.subCategoryId,
                                            combinationtId = c.Id,
                                            isPossibleInBarter = c.BuisnessSubCategory.Select(x => x.isPossibleInBarter).FirstOrDefault()
                                        });
                                    });
                                }
                            }
                        }
                    });
                });
                #endregion

                #region get workPicList
              

                #endregion

                return buisnessVm;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<BusinessForCardVM> GetListOfLatestUpdatedBuisnesses()
        {
            try
            {
                //todo - לעשות קוד יותר יעיל!!
                //todo - check if we need to return this func
                List<Buisness> buisnessesList = _repository.GetListOfLatestUpdatedBuisnesses();
                // לסדר לפי התאריך
                List<BusinessForCardVM> BuisnessesListVm = buisnessesList.Select(_mapper.Map<Buisness, BusinessForCardVM>).ToList();
                // לקחת את ה10 האחרונים
                //BuisnessesListVm = BuisnessesListVm.OrderByDescending(x => x.lastupdatedStartDate)
                    //.OrderByDescending(x => x.Score)
                //    .Take<BusinessForCardVM>(10).ToList();
                #region categories and burterCategories
                //עוברים על העסקים
                buisnessesList.ForEach(m =>
                {
                    //רשימת הקטגוריות של העסק הנוכחי
                    var catList = m.BuisnessSubCategory.Select(x => x.CategorySubCategory.categoryId).Distinct().ToList();
                    var catBurterList = m.BuisnessSubCategoryBarter.Select(x => x.CategorySubCategory.categoryId).Distinct().ToList();
                    BuisnessesListVm.ForEach(bvm =>
                    {
                        if (m.Id == bvm.id)
                        {
                            if (catList != null)
                            {
                                var CatSubCat1 = m.BuisnessSubCategory.Select(x => x.CategorySubCategory).Where(x => x.categoryId == catList[0]).ToList();
                                bvm.buisnessCategory1 = new List<BuisnessCategoryVm>();
                                bvm.buisnessCategory2 = new List<BuisnessCategoryVm>();
                                bvm.buisnessCategory3 = new List<BuisnessCategoryVm>();
                                bvm.buisnessCategory4 = new List<BuisnessCategoryVm>();
                                CatSubCat1.ForEach(c =>
                                {
                                    bvm.buisnessCategory1.Add(new BuisnessCategoryVm()
                                    {
                                        businessId = bvm.id,
                                        categoryId = c.categoryId,
                                        categoryName = c.Category.name,
                                        subCategoryName = c.SubCategory.name,
                                        subCategoryId = c.subCategoryId,
                                        combinationtId = c.Id,
                                        isPossibleInBarter = c.BuisnessSubCategory.Select(x => x.isPossibleInBarter).FirstOrDefault()
                                    }); ;
                                });

                                if (catList.Count > 1)
                                {
                                    var CatSubCat2 = m.BuisnessSubCategory.Select(x => x.CategorySubCategory).Where(x => x.categoryId == catList[1]).ToList();
                                    CatSubCat2.ForEach(c =>
                                    {
                                        bvm.buisnessCategory2.Add(new BuisnessCategoryVm()
                                        {
                                            businessId = bvm.id,
                                            categoryId = c.categoryId,
                                            categoryName = c.Category.name,
                                            subCategoryName = c.SubCategory.name,
                                            subCategoryId = c.subCategoryId,
                                            combinationtId = c.Id,
                                            isPossibleInBarter = c.BuisnessSubCategory.Select(x => x.isPossibleInBarter).FirstOrDefault()
                                        });
                                    });
                                }
                                if (catList.Count > 2)
                                {
                                    var CatSubCat3 = m.BuisnessSubCategory.Select(x => x.CategorySubCategory).Where(x => x.categoryId == catList[2]).ToList();
                                    CatSubCat3.ForEach(c =>
                                    {
                                        bvm.buisnessCategory3.Add(new BuisnessCategoryVm()
                                        {
                                            businessId = bvm.id,
                                            categoryId = c.categoryId,
                                            subCategoryId = c.subCategoryId,
                                            categoryName = c.Category.name,
                                            subCategoryName = c.SubCategory.name,
                                            combinationtId = c.Id,
                                            isPossibleInBarter = c.BuisnessSubCategory.Select(x => x.isPossibleInBarter).FirstOrDefault()
                                        });
                                    });
                                }
                                if (catList.Count > 3)
                                {
                                    var CatSubCat4 = m.BuisnessSubCategory.Select(x => x.CategorySubCategory).Where(x => x.categoryId == catList[3]).ToList();
                                    CatSubCat4.ForEach(c =>
                                    {
                                        bvm.buisnessCategory4.Add(new BuisnessCategoryVm()
                                        {
                                            businessId = bvm.id,
                                            categoryId = c.categoryId,
                                            categoryName = c.Category.name,
                                            subCategoryName = c.SubCategory.name,
                                            subCategoryId = c.subCategoryId,
                                            combinationtId = c.Id,
                                            isPossibleInBarter = c.BuisnessSubCategory.Select(x => x.isPossibleInBarter).FirstOrDefault()
                                        });
                                    });
                                }
                            }
                        }
                    });
                });
                #endregion

                var NewBuisnessesListVmOnly10 = BuisnessesListVm.GroupBy(x => x.userId).Select(x => x.First()).ToList();
                return NewBuisnessesListVmOnly10;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        #region  get buisnesses without join
        //public BuisnessVm GetBuisnessById2(int id)
        //{
        //    Buisness buisness = new Buisness();
        //    try
        //    {
        //        buisness = _repository.GetBuisnessById2(id);
        //        if (buisness != null)
        //        {
        //            BuisnessVm buisnessVm = _mapper.Map<BuisnessVm>(buisness);
        //            buisnessVm = fillOutBuisnessModel(buisnessVm);
        //            return buisnessVm;
        //        }
        //        else
        //            return null;
        //    }
        //    catch (Exception ex)
        //    {

        //        throw ex;
        //    }

        //}

        //public List<BuisnessVm> GetListOfBuisnesses2()
        //{
        //    List<Buisness> buisnessesList = new List<Buisness>();
        //    try
        //    {
        //        buisnessesList = _repository.GetListOfBuisnesses2();
        //        if (buisnessesList != null)
        //        {
        //            List<BuisnessVm> buisnessesListVm = _mapper.Map<List<BuisnessVm>>(buisnessesList);
        //            foreach (var item in buisnessesListVm)
        //            {
        //                fillOutBuisnessModel(item);
        //            }
        //            return buisnessesListVm;
        //        }
        //        else
        //            return null;
        //    }
        //    catch (Exception ex)
        //    {

        //        throw ex;
        //    }

        //}
        //לא בשימוש - חדוה
        //public BuisnessVm fillOutBuisnessModel(BuisnessVm buisnessVm)
        //{
        //    //todo- to all lists
        //    buisnessVm.CategorySubCategoryList1 = new List<CategorySubCategoryVm>();
        //    buisnessVm.CategorySubCategoryBarterList1 = new List<CategorySubCategoryVm>();

        //    //buisnessVm.logoPicture = _repository.GetLogoPictursByBuisnessId(id);
        //    //buisnessVm.coverPicture = _repository.GetCoverPictursByBuisnessId(id);
        //    //buisnessVm.pictursList = _repository.GetPictursListByBuisnessId(id);

        //    //get the categories and subCategories list
        //    List<BuisnessSubCategory> BuisnessSubCategoryList = new List<BuisnessSubCategory>();
        //    BuisnessSubCategoryList = _repository.GetbuisnessSubCategoryByBuisnessId(buisnessVm.Id);
        //    CategorySubCategoryVm temp = new CategorySubCategoryVm();
        //    if (BuisnessSubCategoryList != null)
        //        if (BuisnessSubCategoryList.Count > 0)
        //        {
        //            foreach (var item in BuisnessSubCategoryList)
        //            {
        //                //todo 
        //                temp.Id = item.categorySubCategoryId;
        //                temp.categoryId = item.CategorySubCategory.categoryId;
        //                temp.categoryName = item.CategorySubCategory.Category.name;
        //                temp.subCategoryId = item.CategorySubCategory.subCategoryId;
        //                temp.isPossibleInBarter = item.isPossibleInBarter;
        //                temp.subCategoryName = item.CategorySubCategory.SubCategory.name;

        //                buisnessVm.CategorySubCategoryList1.Add(temp);
        //            }
        //        }
        //    //get the categories and subCategories for barter list
        //    List<BuisnessSubCategoryBarter> BuisnessSubCategoryBarterList = new List<BuisnessSubCategoryBarter>();
        //    BuisnessSubCategoryBarterList = _repository.GetbuisnessSubCategoryBarterByBuisnessId(buisnessVm.Id);
        //    if (BuisnessSubCategoryList != null)
        //        if (BuisnessSubCategoryList.Count > 0)
        //        {
        //            foreach (var item in BuisnessSubCategoryBarterList)
        //            {
        //                temp.Id = item.categorySubCategoryId;
        //                temp.categoryId = item.CategorySubCategory.categoryId;
        //                temp.categoryName = item.CategorySubCategory.Category.name;
        //                temp.subCategoryId = item.CategorySubCategory.subCategoryId;
        //                temp.subCategoryName = item.CategorySubCategory.SubCategory.name;

        //                buisnessVm.CategorySubCategoryBarterList1.Add(temp);
        //            }
        //        }
        //    return buisnessVm;
        //}
        #endregion

        public async Task<int> createBuisnessWithFiles(BuisnessVm modelVm, List<IFormFile> WorkFiles)
        {
            try
            {
                var isNew = false;
                if(modelVm.id == 0)
                {
                    isNew = true;
                }
                int buisnessid = -1;
                //bool coverFileSaved = false;
                //bool logoFileSaved = false;

                if (modelVm != null)
                {
                    //save buisness and get buisnessid 

                    //generate new guid & save it as the pic id
                    //save pic to disk
                    //after will pic guid will besaved with entire buisness to DB

                    //set buisness sub categories 
                    //set buisness sub categories for barter
                    //set buisness area
                    //set buisness status

                    Buisness model = _mapper.Map<Buisness>(modelVm);

                   
                    //generate new guid & save it as the pic id

                    if (modelVm.coverPicture != null)//we have a new pic
                    {
                        //always when we have a pic we generate new guide
                        model.coverPictureId = modelVm.coverPicture.Length > 0 ? GenerateNewGuid() : Guid.Empty;
                    }
                    if (modelVm.logoPicture != null)//we have a new pic
                    {
                        //always when we have a pic we generate new guide
                        model.logoPictureId = modelVm.logoPicture.Length > 0 ? GenerateNewGuid() : Guid.Empty;
                    }
                    
                    //check if realy update
                    buisnessid = _repository.createBuisness(model);
                    //קבלת ניקוד במקרה שהעסק יצר עכשיו פעם ראשונה כרטיס עסק 
                    /// בדיקה האם נשלח לינק לאתר בטופס - אם כן קבלת ניקוד 
                    if (modelVm.buisnessWebSiteLink != null)
                    {
                        _scoring.ScoreBusiness(8, buisnessid);
                    }
                    //שליחה לקבלת ניקוד במקרה של הוספת תמונה
                    if (modelVm.logoPicture != null || modelVm.coverPicture != null || WorkFiles.Count > 0)
                    {
                        _scoring.ScoreBusiness(7, buisnessid);
                    }
               
                    if (isNew)
                    {
                        _scoring.ScoreBusiness(1, buisnessid);
                        _scoring.ScoreBusiness(6, buisnessid);
                    }
                    if (buisnessid > 0)
                    {
                        int i = 0;
                        int isSuccess = -1;

                        #region cover pictures
                        //handle pictires 

                        if (modelVm.coverPicture != null) //we have a new pic
                        {
                            try
                            {
                                //this is a shared location that all can reach .                    
                                string localTmpPath = GetDiskPath();
                                string coverPath = localTmpPath + buisnessid + "\\" + "Cover" + "\\" + model.coverPictureId;
                                //if (modelVm.coverPictureId != null)
                                //{
                                //    OldcoverPath = localTmpPath + buisnessid + "\\" + "Cover" + "\\" + modelVm.coverPictureId;
                                //}
                                string OldcoverPath = localTmpPath + buisnessid + "\\" + "Cover" + "\\" + modelVm.coverPictureId;

                                if (await SaveFileLocalDisc(OldcoverPath, modelVm.coverPictureId, coverPath, modelVm.coverPicture, model.coverPictureId))
                                    Console.WriteLine("upload cover img = true");
                                else
                                    Console.WriteLine("upload cover img = false");
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine("error while saving cover image : {0}", ex.Message);
                                throw ex;
                            }
                        }
                        #endregion

                        #region logo pictures
                        if (modelVm.logoPicture != null) //we have a new pic
                        {
                            try
                            {
                                //this is a shared location that all can reach .
                                string localTmpPath = GetDiskPath();
                                string logoPath = localTmpPath + buisnessid + "\\" + "Logo" + "\\" + model.logoPictureId;
                                //if (modelVm.logoPictureId != null)
                                //{
                                //    OldlogoPath = localTmpPath + buisnessid + "\\" + "Logo" + "\\" + modelVm.logoPictureId;
                                //}
                                string OldlogoPath = localTmpPath + buisnessid + "\\" + "Logo" + "\\" + modelVm.logoPictureId;
                                if (await SaveFileLocalDisc(OldlogoPath, modelVm.logoPictureId, logoPath, modelVm.logoPicture, model.logoPictureId))
                                    Console.WriteLine("upload logo img = true");
                                else
                                    Console.WriteLine("upload logo img = false");
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine("error while saving logo image : {0}", ex.Message);
                                throw ex;
                            }
                        }
                        #endregion

                        #region work pictures

                        //first - we will add the index to db
                        //אם מחק את כל התמונות זה גם שווה 0
                        if (modelVm.workPictureGuide.Count >= 0)
                        {
                            //1 = delete the old guide
                            //הרשימה המלאה הישנה - נביא אותה מדאטה ביס- הרשימה השמורה שם
                            var oldBPL = _repository.GetPictursListByBuisnessId(buisnessid);
                            //var oldBPL = model.BuisnessPicture.Select(p => p).Where(c => c.buisnessId == buisnessid).ToList();
                            var ToDelete = new List<BuisnessPicture>();// ניצור רשימה שאותה נמחוק
                            if (oldBPL.Count != 0)//אם הרשימה שיש בדאטהביס לא ריקה - נמחוק את מי שצריך
                            {
                                oldBPL.ForEach(op =>
                                {
                                    bool containsItem = modelVm.workPictureGuide.Any(item => item.workPicGuide == op.buisnessPictureId);
                                    if (containsItem == false)
                                    {
                                        ToDelete.Add(op);
                                    }
                                });
                                if (ToDelete.Count != 0)
                                {
                                    _repository.DeleteBusinessPic(ToDelete);
                                }
                            }
                        }
                        //2 = create the bp in the db
                        if (WorkFiles != null) //we have a new pic
                        {
                            List<BuisnessPicture> guideList = new List<BuisnessPicture>();
                            try
                            {
                                //this is a shared location that all can reach .
                                //int index;                                
                                //index = 0;
                                WorkFiles.ForEach(async (Wfile) =>
                                {
                                    //index+=1;

                                    string localTmpPath = GetDiskPath();
                                    bool sussesW;
                                    var newG = GenerateNewGuid();
                                    //בשביל לדעת מה האינדקס של התמונה נשלוף אותה מהרשימה שחזרה מהקליינט
                                    var index = modelVm.workPictureGuide.Select(i => i).Where(p => p.picName == Wfile.FileName).FirstOrDefault();
                                    string curentWorkPPath = localTmpPath + buisnessid + "\\" + "Work" + "\\" + index.picindex + "\\" + newG;
                                    //יצירת הניתוב שצריך למחוק
                                    string OldWPath = localTmpPath + buisnessid + "\\" + "Work" + "\\" + index.picindex + "\\";
                                    //למשוך את הגויד של התמונה הנוחכית ולשלוח למחיקה
                                    //var guideByIndex = modelVm.workPictureGuide.Select(g => g).Where(ig => ig.workPicGuide != null).FirstOrDefault();
                                    //if (guideByIndex != null)
                                    //{
                                    //    OldWPath = localTmpPath + buisnessid + "\\" + "Work" + "\\" + index.picindex + "\\";
                                    //}
                                    guideList.Add(new BuisnessPicture
                                    {
                                        buisnessId = buisnessid,
                                        buisnessPictureId = newG,
                                        numberOfPicture = index.picindex
                                    });

                                    if (await SaveFileLocalDiscWorkPic(OldWPath, curentWorkPPath, Wfile, newG))
                                        sussesW = true;
                                    else
                                        sussesW = false;

                                });
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine("error while saving cover image : {0}", ex.Message);
                                throw ex;
                            }
                            _repository.CreatePictursForBuisness(guideList);
                        }
                        //    foreach (var item in WorkFiles)
                        //    {
                        //        var NewGuide = GenerateNewGuid();
                        //        item.buisnessId = buisnessid;
                        //        item.numberOfPicture = i;
                        //        isSuccess = _repository.CreatePictursForBuisness(item);
                        //        if (isSuccess < 0)
                        //            return -2;
                        //        i++;
                        //    }
                        //create the cover picture
                        //BuisnessCoverPicture coverPicture = new BuisnessCoverPicture();
                        //coverPicture.buisnessId = buisnessid;
                        //coverPicture.url = modelVm.coverPicture;
                        //isSuccess = _repository.CreateCoverPictureForBuisness(coverPicture);

                        //create the logo picture
                        //BuisnessLogo logoPicture = new BuisnessLogo();
                        //logoPicture.buisnessId = buisnessid;
                        //logoPicture.url = modelVm.logoPicture;
                        //isSuccess = _repository.CreateLogoPictureForBuisness(logoPicture);

                        ////create the BuisnessCategorySubCategory
                        //BuisnessSubCategory tempBuisnessSubCategory = new BuisnessSubCategory();

                        //check each list in the array : 

                        #endregion

                        #region sub category

                        var isExsist = _repository.GetSubCategoryById(buisnessid);

                        List<BuisnessSubCategory> BuisnessSubCategoryForUpdate = new List<BuisnessSubCategory>();

                        if (modelVm.buisnessCategory1 != null && modelVm.buisnessCategory1.Count > 0)
                        {
                            modelVm.buisnessCategory1.ForEach(x =>
                            {
                                BuisnessSubCategoryForUpdate.Add(new BuisnessSubCategory()
                                {
                                    buisnessId = buisnessid,
                                    categorySubCategoryId = x.combinationtId,
                                    isPossibleInBarter = x.isPossibleInBarter

                                });
                            });
                        }
                        if (modelVm.buisnessCategory2 != null && modelVm.buisnessCategory2.Count > 0)
                        {
                            modelVm.buisnessCategory2.ForEach(x =>
                            {
                                BuisnessSubCategoryForUpdate.Add(new BuisnessSubCategory()
                                {
                                    buisnessId = buisnessid,
                                    categorySubCategoryId = x.combinationtId,
                                    isPossibleInBarter = x.isPossibleInBarter

                                });
                            });
                        }
                        if (modelVm.buisnessCategory3 != null && modelVm.buisnessCategory3.Count > 0)
                        {
                            modelVm.buisnessCategory3.ForEach(x =>
                            {
                                BuisnessSubCategoryForUpdate.Add(new BuisnessSubCategory()
                                {
                                    buisnessId = buisnessid,
                                    categorySubCategoryId = x.combinationtId,
                                    isPossibleInBarter = x.isPossibleInBarter

                                });
                            });
                        }
                        if (modelVm.buisnessCategory4 != null && modelVm.buisnessCategory4.Count > 0)
                        {
                            modelVm.buisnessCategory4.ForEach(x =>
                            {
                                BuisnessSubCategoryForUpdate.Add(new BuisnessSubCategory()
                                {
                                    buisnessId = buisnessid,
                                    categorySubCategoryId = x.combinationtId,
                                    isPossibleInBarter = x.isPossibleInBarter

                                });
                            });
                        }


                        if (BuisnessSubCategoryForUpdate != null || isExsist == true)
                        {
                            //clean the old ones 
                            _repository.DeleteBuisnessSubCategoryByBuisnessId(buisnessid);

                            //create the new ones
                            _repository.CreateBuisnessSubCategoryListForBuisness(BuisnessSubCategoryForUpdate);
                        }

                        #endregion

                        #region sub category barter

                        List<BuisnessSubCategoryBarter> BuisnessSubCategoryBarterForUpdate = new List<BuisnessSubCategoryBarter>();

                        if (modelVm.buisnessBarterCategory1 != null && modelVm.buisnessBarterCategory1.Count > 0)
                        {
                            modelVm.buisnessBarterCategory1.ForEach(x =>
                            {
                                BuisnessSubCategoryBarterForUpdate.Add(new BuisnessSubCategoryBarter()
                                {
                                    buisnessId = buisnessid,
                                    categorySubCategoryId = x.combinationtId

                                });
                            });
                        }
                        if (modelVm.buisnessBarterCategory2 != null && modelVm.buisnessBarterCategory2.Count > 0)
                        {
                            modelVm.buisnessBarterCategory2.ForEach(x =>
                            {
                                BuisnessSubCategoryBarterForUpdate.Add(new BuisnessSubCategoryBarter()
                                {
                                    buisnessId = buisnessid,
                                    categorySubCategoryId = x.combinationtId

                                });
                            });
                        }
                        var isExsistForBarter = _repository.GetSubCategoryForBarterById(buisnessid);

                        if (BuisnessSubCategoryBarterForUpdate != null || isExsistForBarter==true)
                        {
                            //clean the old ones 
                            _repository.DeleteBuisnessSubCategoryBarterByBuisnessId(buisnessid);

                            //create the new ones
                            
                                _repository.CreateBuisnessSubCategoryBarterListForBuisness(BuisnessSubCategoryBarterForUpdate);
                        }

                        #endregion

                        #region status    
                        if (modelVm.id == 0 || modelVm.id == null)
                        {
                            BuisnessStatus status = new BuisnessStatus
                            {
                                buisnessId = buisnessid,
                                statusId = 2,
                                startDate = DateTime.Now,
                                endDate = DateTime.Now.AddYears(3)
                            };
                            _repository.CreateStatus(status);
                        }
                        else
                        {
                            _repository.updateStatusStartDate(modelVm.id);
                        }
                        #endregion

                        #region areas
                        List<BuisnessArea> buisnessAreaList = new List<BuisnessArea>();
                        if (modelVm.buisnessAreaList1 != null)
                        {
                            modelVm.buisnessAreaList1.ForEach(ba =>
                            buisnessAreaList.Add(new BuisnessArea()
                            {
                                buisnessId = buisnessid,
                                areaId = ba.areaId,
                            }));
                        }
                        if (buisnessAreaList != null && buisnessAreaList.Count > 0)
                        {
                            //delete all the conection of the buisnes
                            _repository.DeleteAreaConection(buisnessid);
                            //create the conection buisess in the area table
                            _repository.CreateAreaConection(buisnessAreaList);
                        }
                        #endregion

                        #region category notify
                        await _repository.SaveBusinessCategoryNotify(buisnessid, model.BusinessCategoriesNotify.ToList());
                        #endregion
                    }
                }
                if (buisnessid >= 0)
                    return buisnessid;
                else
                    return -1;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
      
        public int deleteBuisness(int id)
        {
            //צריך להוסיף מחיקה של הרשומות מטבלאות הקטגוריות והתת קטגוריות, כרגע אין לכך התיחסות
            try
            {
                int success = -1;
                success = _repository.deleteBuisness(id);
                if (id >= 0)
                {

                    return success;
                }
                else
                    return -1;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public List<AreaVm> GetAreasList()
        {
            try
            {
                List<Area> areaList = new List<Area>();
                areaList = _repository.GetAreasList();
                List<AreaVm> areaListVm = areaList.Select(_mapper.Map<Area, AreaVm>).ToList();

                if (areaList != null)
                    return areaListVm;
                else
                    return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public Guid GenerateNewGuid()
        {
            return Guid.NewGuid();
        }
        public async Task<bool> SaveFileLocalDisc(string Oldpath, Guid? oldGuide, string path, IFormFile file, Guid guidFile)
        {
            try
            {
                if (Directory.Exists(Oldpath))
                {
                    //Oldpath = Oldpath +"\\"+ oldGuide + System.IO.Path.GetExtension(file.FileName);

                    System.IO.DirectoryInfo di = new DirectoryInfo(Oldpath);

                    foreach (FileInfo Oldfile in di.GetFiles())
                    {
                        Oldfile.Delete();
                    }
                    //foreach (DirectoryInfo Olddir in di.GetDirectories())
                    //{
                    di.Delete(true);
                    //}
                    //DirectoryInfo dir = Oldpath;
                    //dir.Delete(true);
                    //Directory.Delete(Oldpath,true);
                }
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                if (file.Length > 0)
                {
                    //var filePath = Path.Combine(path, file.FileName);
                    //var FileName = guidFile.ToString() + System.IO.Path.GetExtension(file.FileName);
                    var FileName = guidFile.ToString() + ".jpg";
                    var filePath = Path.Combine(path, FileName);
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await file.CopyToAsync(fileStream);
                    }
                }
              
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("error while saving file to disk : {0}", ex.Message);
                return false;
            }
        }
        public async Task<bool> SaveFileLocalDiscWorkPic(string Oldpath, string path, IFormFile file, Guid guidFile)
        {
            try
            {
                //מחיקת הניתוב הקודם
                if (Directory.Exists(Oldpath))
                {
                    //Oldpath = Oldpath +"\\"+ oldGuide + System.IO.Path.GetExtension(file.FileName);

                    System.IO.DirectoryInfo di = new DirectoryInfo(Oldpath);

                    foreach (FileInfo Oldfile in di.GetFiles())
                    {
                        Oldfile.Delete();
                    }
                    di.Delete(true);
                }
                //יצירת ניתוב חדש - גם במקרה שחדש וגם אחרי מחיקה(בעדכון)
                if (!Directory.Exists(path))//if the path not exists
                {
                    Directory.CreateDirectory(path);//create
                }
                if (file.Length > 0)//if we have file to create
                {
                    //var filePath = Path.Combine(path, file.FileName);                    
                    //var FileName = guidFile.ToString() + System.IO.Path.GetExtension(file.FileName);
                    var FileName = guidFile.ToString() + ".jpg";
                    var filePath = Path.Combine(path, FileName);
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        file.CopyToAsync(fileStream).Wait();
                    }
                }               
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("error while saving file to disk : {0}", ex.Message);
                return false;
            }
        }
        public string GetDiskPath()
        {
            //to do : creae dir if dosent excist
            return _configuration["DiskPath"]; ;
        }
        public string CreateDir(string path)
        {
            try
            {
                // Determine whether the directory exists.
                if (Directory.Exists(path))
                {
                    Console.WriteLine("That path exists already.");
                    return path;
                }

                // Try to create the directory.
                DirectoryInfo di = Directory.CreateDirectory(path);
                Console.WriteLine("The directory was created successfully at {0}.", Directory.GetCreationTime(path));
                return path;
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public List<BusinessNamesPicturesVM> GetBusinessNamesPictures()
        {
            List<BusinessNamesPicturesVM> businessNamesPicturesVMs = _repository.GetNamesPicturesBuisnesses().Select(_mapper.Map<Buisness, BusinessNamesPicturesVM>).ToList();
            return businessNamesPicturesVMs;
        }
        public List<BusinessNamesPicUserIdVM> GetBusinessNamesPictUser()
        {
            List<BusinessNamesPicUserIdVM> businessNamesPicturesVMs = _repository.GetNamesPicturesBuisnesses().Select(_mapper.Map<Buisness, BusinessNamesPicUserIdVM>).ToList();
            return businessNamesPicturesVMs;
        }

        public List<BusinessNamesPicturesVM> GetBusinessForCards()
        {
            List<BusinessNamesPicturesVM> businessNamesPicturesVMs = _repository.GetNamesPicturesBuisnesses().Select(_mapper.Map<Buisness, BusinessNamesPicturesVM>)
                .ToList();
            return businessNamesPicturesVMs;
        }
        public BusinessNamesPicturesVM Get1BusinessNamesPicture(int id)
        {
            BusinessNamesPicturesVM businessNamesPicturesVM = _mapper.Map <BusinessNamesPicturesVM> (_repository.Get1NamesPicturesBuisness(id));
            return businessNamesPicturesVM;
        }

     
        #region collaborations
        public int createPaidTransaction(PaidTransactionVM model)
        {
            int id = _repository.createPaidTransaction( _mapper.Map<PaidTransaction>(model));//_mapper.Map<Buisness>(modelVm)
            return id;
        }
        public async Task<int> createPaidTransactionWithPicture(PaidTransactionVM model)
        {
            
            try
            {
                if (model != null)
                {
                    _scoring.ScoreBusiness(3, model.SupplierBusinessId);

                    model.PictureID = model.PaidTransactionPicture != null && model.PaidTransactionPicture.Length > 0 ? GenerateNewGuid() : Guid.Empty;
                    PaidTransaction paidTransaction = _mapper.Map<PaidTransaction>(model);
                    int paidTransactionId = _repository.createPaidTransaction(paidTransaction);
                    if (model.PaidTransactionPicture!= null)
                    {
                        try
                        {
                            //this is a shared location that all can reach .                    
                            string localTmpPath = GetDiskPathPaidTransactions();
                            string localPath = localTmpPath + paidTransactionId + "\\";
                            if (await SaveFileLocalDisc(localPath, null, localPath, model.PaidTransactionPicture, (Guid)model.PictureID))
                                Console.WriteLine("upload img = true");
                            else
                                Console.WriteLine("upload img = false");
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("error while saving cover image : {0}", ex.Message);
                            throw ex;
                        }
                    }

                    return paidTransactionId;
                
               
                }
                else
                {
                    return -1;
                }
                
            }
            catch (Exception e)
            {

                throw e;
            }
        }
        public async Task<int> CreateBarterDealWithPictures(BarterDealVM model)
        {

            try
            {
                if (model != null)
                {
                    model.ReportsBusinessPictureID = model.Business1Picture!= null && model.Business1Picture.Length > 0 ? GenerateNewGuid() : Guid.Empty;
                    model.PartnerBusinessPictureID = model.Business2Picture != null && model.Business2Picture.Length > 0 ? GenerateNewGuid() : Guid.Empty;
                    BarterDeal barterDeal = _mapper.Map<BarterDeal>(model);
                    int barterDealId = _repository.createBarterDeal(barterDeal);
                    if (model.Business1Picture != null)
                    {
                        try
                        {
                            //this is a shared location that all can reach .                    
                            string localTmpPath = GetDiskPathBarter();
                            string localPath = localTmpPath + barterDealId + "\\" + model.ReportsBusinessId + "\\" ;
                            if (await SaveFileLocalDisc(localPath, null, localPath, model.Business1Picture, (Guid)model.ReportsBusinessPictureID))
                                Console.WriteLine("upload img = true");
                            else
                                Console.WriteLine("upload img = false");
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("error while saving cover image : {0}", ex.Message);
                            throw ex;
                        }
                    }
                    if (model.Business2Picture != null)
                    {
                        try
                        {
                            //this is a shared location that all can reach .                    
                            string localTmpPath = GetDiskPathBarter();
                            string localPath = localTmpPath + barterDealId + "\\" + model.PartnerBusinessId + "\\";
                            if (await SaveFileLocalDisc(localPath, null, localPath, model.Business2Picture, (Guid)model.PartnerBusinessPictureID))
                                Console.WriteLine("upload img = true");
                            else
                                Console.WriteLine("upload img = false");
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("error while saving cover image : {0}", ex.Message);
                            throw ex;
                        }
                    }
                    return barterDealId;
                }
                else
                {
                    return -1;
                }

            }
            catch (Exception e)
            {

                throw e;
            }
        }

        public string GetDiskPathPaidTransactions()
        {
            return _configuration["DiskPathPaidTransactions"]; ;
        }
        public string GetDiskPathBarter()
        {
            return _configuration["DiskPathBarterDeal"]; ;
        }
        public string GetDiskPathJointProject()
        {
            return _configuration["DiskPathJointProject"]; ;
        }
        public async Task<int> createJointProjectWithPictures(JointProjectVM model)
        {
            foreach(var item in model.BusinessesInCollaboration)
            {
                _scoring.ScoreBusiness(3, item.id);
            }
            model.PictureId = (model.Picture!= null && model.Picture.Length > 0) ? GenerateNewGuid() : Guid.Empty;
            model.Id = 0;
            model.ReportDate = new DateTime();
            int jointProjectId = _repository.createJoinProject(_mapper.Map<JointProject>(model));//_mapper.Map<Buisness>(modelVm);
            foreach (var business in model.BusinessesInCollaboration)
            {
                BusinessInCollaboration businessInCollaboration = new BusinessInCollaboration()
                {
                    Id = 0,
                    BusinessId = business.id,
                    JoinProjectId = jointProjectId,
                    IfReport = business.IfReport,
                    PartInCollaboration = business.PartInCollaboration,
                };
                int businessId = _repository.createBusinessInCollaboration(businessInCollaboration);
               
            }
            if (model.Picture != null)
            {
                try
                {
                    //this is a shared location that all can reach .                    
                    string localTmpPath = GetDiskPathJointProject();
                    string localPath = localTmpPath + jointProjectId + "\\";
                    if (await SaveFileLocalDisc(localPath, null, localPath, model.Picture, (Guid)model.PictureId))
                        Console.WriteLine("upload img = true");
                    else
                        Console.WriteLine("upload img = false");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("error while saving cover image : {0}", ex.Message);
                    throw ex;
                }
            }
            return jointProjectId;
        }

        public int createBarterDeal(BarterDealVM model)
        {
            _scoring.ScoreBusiness(3, model.ReportsBusinessId);
            int id = _repository.createBarterDeal(_mapper.Map<BarterDeal>(model));//_mapper.Map<Buisness>(modelVm)
            return id;
        }
        public int createCollaborationType(CollaborationTypeVM model)
        {
            int id = _repository.createCollaborationType(_mapper.Map<CollaborationType>(model));//_mapper.Map<Buisness>(modelVm)
            return id;
        }
        public List<CollaborationTypeVM> GetCollaborationTypes()
        {

            return _repository.GetCollaborationTypes().Select(_mapper.Map<CollaborationType, CollaborationTypeVM>).ToList(); ;
        }
        #endregion

        //public int createBuisness(BuisnessVm modelVm)
        //{
        //    try
        //    {
        //        int id = -1;
        //        if (modelVm != null)
        //        {
        //            //save buisness and get buisnessid 

        //            //set buisness sub categories 
        //            //set buisness sub categories for barter
        //            //set buisness area

        //            //handle pictires 
        //            Buisness model = _mapper.Map<Buisness>(modelVm);
        //            //check if realy update
        //            id = _repository.createBuisness(model);

        //            if (id > 0)
        //            {
        //                int i = 0;
        //                int isSuccess = -1;

        //                #region pictures
        //                //foreach (var item in modelVm.pictursList)
        //                //{
        //                //    item.buisnessId = id;
        //                //    item.numberOfPicture = i;
        //                //    isSuccess = _repository.CreatePictursForBuisness(item);
        //                //    if (isSuccess < 0)
        //                //        return -2;
        //                //    i++;
        //                //}
        //                //create the cover picture
        //                //BuisnessCoverPicture coverPicture = new BuisnessCoverPicture();
        //                //coverPicture.buisnessId = id;
        //                //coverPicture.url = modelVm.coverPicture;
        //                //isSuccess = _repository.CreateCoverPictureForBuisness(coverPicture);

        //                //create the logo picture
        //                //BuisnessLogo logoPicture = new BuisnessLogo();
        //                //logoPicture.buisnessId = id;
        //                //logoPicture.url = modelVm.logoPicture;
        //                //isSuccess = _repository.CreateLogoPictureForBuisness(logoPicture);

        //                ////create the BuisnessCategorySubCategory
        //                //BuisnessSubCategory tempBuisnessSubCategory = new BuisnessSubCategory();

        //                //check each list in the array :  
        //                #endregion

        //                #region sub category

        //                List<BuisnessSubCategory> BuisnessSubCategoryForUpdate = new List<BuisnessSubCategory>();

        //                if (modelVm.buisnessCategory1 != null && modelVm.buisnessCategory1.Count > 0)
        //                {
        //                    modelVm.buisnessCategory1.ForEach(x =>
        //                    {
        //                        BuisnessSubCategoryForUpdate.Add(new BuisnessSubCategory()
        //                        {
        //                            buisnessId = id,
        //                            categorySubCategoryId = x.combinationtId,
        //                            isPossibleInBarter = x.isPossibleInBarter

        //                        });
        //                    });
        //                }
        //                if (modelVm.buisnessCategory2 != null && modelVm.buisnessCategory2.Count > 0)
        //                {
        //                    modelVm.buisnessCategory2.ForEach(x =>
        //                    {
        //                        BuisnessSubCategoryForUpdate.Add(new BuisnessSubCategory()
        //                        {
        //                            buisnessId = id,
        //                            categorySubCategoryId = x.combinationtId,
        //                            isPossibleInBarter = x.isPossibleInBarter

        //                        });
        //                    });
        //                }
        //                if (modelVm.buisnessCategory3 != null && modelVm.buisnessCategory3.Count > 0)
        //                {
        //                    modelVm.buisnessCategory3.ForEach(x =>
        //                    {
        //                        BuisnessSubCategoryForUpdate.Add(new BuisnessSubCategory()
        //                        {
        //                            buisnessId = id,
        //                            categorySubCategoryId = x.combinationtId,
        //                            isPossibleInBarter = x.isPossibleInBarter

        //                        });
        //                    });
        //                }
        //                if (modelVm.buisnessCategory4 != null && modelVm.buisnessCategory4.Count > 0)
        //                {
        //                    modelVm.buisnessCategory4.ForEach(x =>
        //                    {
        //                        BuisnessSubCategoryForUpdate.Add(new BuisnessSubCategory()
        //                        {
        //                            buisnessId = id,
        //                            categorySubCategoryId = x.combinationtId,
        //                            isPossibleInBarter = x.isPossibleInBarter

        //                        });
        //                    });
        //                }


        //                if (BuisnessSubCategoryForUpdate != null && BuisnessSubCategoryForUpdate.Count > 0)
        //                {
        //                    //clean the old ones 
        //                    _repository.DeleteBuisnessSubCategoryByBuisnessId(id);

        //                    //create the new ones
        //                    _repository.CreateBuisnessSubCategoryListForBuisness(BuisnessSubCategoryForUpdate);
        //                }

        //                #endregion

        //                #region sub category barter

        //                List<BuisnessSubCategoryBarter> BuisnessSubCategoryBarterForUpdate = new List<BuisnessSubCategoryBarter>();

        //                if (modelVm.buisnessBarterCategory1 != null && modelVm.buisnessBarterCategory1.Count > 0)
        //                {
        //                    modelVm.buisnessBarterCategory1.ForEach(x =>
        //                    {
        //                        BuisnessSubCategoryBarterForUpdate.Add(new BuisnessSubCategoryBarter()
        //                        {
        //                            buisnessId = id,
        //                            categorySubCategoryId = x.combinationtId

        //                        });
        //                    });
        //                }
        //                if (modelVm.buisnessBarterCategory2 != null && modelVm.buisnessBarterCategory2.Count > 0)
        //                {
        //                    modelVm.buisnessBarterCategory2.ForEach(x =>
        //                    {
        //                        BuisnessSubCategoryBarterForUpdate.Add(new BuisnessSubCategoryBarter()
        //                        {
        //                            buisnessId = id,
        //                            categorySubCategoryId = x.combinationtId

        //                        });
        //                    });
        //                }


        //                if (BuisnessSubCategoryBarterForUpdate != null && BuisnessSubCategoryBarterForUpdate.Count > 0)
        //                {
        //                    //clean the old ones 
        //                    _repository.DeleteBuisnessSubCategoryBarterByBuisnessId(id);

        //                    //create the new ones
        //                    _repository.CreateBuisnessSubCategoryBarterListForBuisness(BuisnessSubCategoryBarterForUpdate);
        //                }

        //                #endregion

        //                #region status    
        //                if (modelVm.id == 0 || modelVm.id == null)
        //                {
        //                    BuisnessStatus status = new BuisnessStatus
        //                    {
        //                        buisnessId = id,
        //                        statusId = 2,
        //                        startDate = DateTime.Now,
        //                        endDate = DateTime.Now.AddYears(3)
        //                    };
        //                    _repository.CreateStatus(status);
        //                }
        //                #endregion

        //                #region areas
        //                List<BuisnessArea> buisnessAreaList = new List<BuisnessArea>();
        //                if (modelVm.buisnessAreaList1 != null)
        //                {
        //                    modelVm.buisnessAreaList1.ForEach(ba =>
        //                    buisnessAreaList.Add(new BuisnessArea()
        //                    {
        //                        buisnessId = id,
        //                        areaId = ba.areaId,
        //                    }));
        //                }
        //                if (buisnessAreaList != null && buisnessAreaList.Count > 0)
        //                {
        //                    //delete all the conection of the buisnes
        //                    _repository.DeleteAreaConection(id);
        //                    //create the conection buisess in the area table
        //                    _repository.CreateAreaConection(buisnessAreaList);
        //                }
        //                #endregion
        //            }
        //        }
        //        if (id >= 0)
        //            return id;
        //        else
        //            return -1;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
        //public async Task<int> createBuisnessWithFiles(BuisnessVm modelVm)//BuisnessWithFilesVm modelVm, IFormFile coverPic, IFormFile logoPic, List<IFormFile> workPics
        //{
        //    try
        //    {
        //        int buisnessid = -1;
        //        bool coverFileSaved = false;
        //        bool logoFileSaved = false;
        //        byte[] coverPicFileBytes = new byte[0];
        //        byte[] logoPicFileBytes = new byte[0];
        //        if (modelVm != null)
        //        {
        //            //save buisness and get buisnessid 

        //            //set buisness sub categories 
        //            //set buisness sub categories for barter
        //            //set buisness area

        //            //handle pictires 
        //            Buisness model = _mapper.Map<Buisness>(modelVm);

        //            //generate new guid & save it as the pic id
        //            model.coverPictureId = modelVm.coverPicture.Length > 0 ? GenerateNewGuid() : Guid.Empty;
        //            model.logoPictureId = modelVm.logoPicture.Length > 0 ? GenerateNewGuid() : Guid.Empty;


        //            //check if realy update
        //            buisnessid = _repository.createBuisness(model);

        //            if (buisnessid > 0)
        //            {
        //                int i = 0;
        //                int isSuccess = -1;
        //                string fullPathCover = "";
        //                string fullPathLogo = "";

        //                #region cover pictures

        //                if (modelVm.coverPicture != null) //we have a new pic
        //                {
        //                    //generate new guid & save it as the pic id
        //                    //save pic to disk
        //                    //after will pic guid will besaved with entire buisness to DB

        //                    //model.coverPictureId = GenerateNewGuid();

        //                    try
        //                    {
        //                        //create a memorystream to get the byte[] from the file that was uploaded .
        //                        if (modelVm.coverPicture != null && modelVm.coverPicture.Length > 0)
        //                        {
        //                            using (var memoryStream = new MemoryStream())
        //                            {
        //                                //await coverPic.CopyToAsync(memoryStream);
        //                                modelVm.coverPicture.CopyToAsync(memoryStream);
        //                                //set the content of the file for the request .
        //                                coverPicFileBytes = memoryStream.ToArray();
        //                            }
        //                        }
        //                        //this is a shared location that all can reach .

        //                        string localTmpPath = GetDiskPath();
        //                        string baseDir = CreateDir(localTmpPath + buisnessid);
        //                        if (!string.IsNullOrEmpty(baseDir))
        //                        {
        //                            fullPathCover = CreateDir(baseDir + "\\" + "Cover" + "\\" + model.coverPictureId);
        //                        }
        //                        if (await SaveFileLocalDisc(fullPathCover, modelVm.coverPicture))
        //                        {
        //                            coverFileSaved = true;
        //                        }
        //                        else
        //                        {
        //                            coverFileSaved = false;
        //                        }
        //                    }
        //                    catch (Exception ex)
        //                    {
        //                        Console.WriteLine("error while saving cover image : {0}", ex.Message);
        //                        throw ex;
        //                    }
        //                }
        //                #endregion
        //                #region logo pictures
        //                if (modelVm.logoPicture != null) //we have a new pic
        //                {
        //                    //generate new guid & save it as the pic id
        //                    //save pic to disk
        //                    //after will pic guid will besaved with entire buisness to DB

        //                    try
        //                    {
        //                        //create a memorystream to get the byte[] from the file that was uploaded .
        //                        if (modelVm.logoPicture != null && modelVm.logoPicture.Length > 0)
        //                        {
        //                            using (var memoryStream = new MemoryStream())
        //                            {
        //                                //await coverPic.CopyToAsync(memoryStream);
        //                                modelVm.logoPicture.CopyToAsync(memoryStream);
        //                                //set the content of the file for the request .
        //                                logoPicFileBytes = memoryStream.ToArray();
        //                            }
        //                        }
        //                        //this is a shared location that all can reach .

        //                        string localTmpPath = GetDiskPath();
        //                        string baseDir = CreateDir(localTmpPath + buisnessid);
        //                        if (!string.IsNullOrEmpty(baseDir))
        //                        {
        //                            fullPathLogo = CreateDir(baseDir + "\\" + "Logo" + "\\" + model.logoPictureId);
        //                        }
        //                        //if (SaveFileLocalDisc(fullPathLogo, coverPicFileBytes))
        //                        //{
        //                        //    logoFileSaved = true;
        //                        //}
        //                        else
        //                        {
        //                            logoFileSaved = false;
        //                        }
        //                    }
        //                    catch (Exception ex)
        //                    {
        //                        Console.WriteLine("error while saving logo image : {0}", ex.Message);
        //                        throw ex;
        //                    }
        //                }
        //                #endregion

        //                #region work pictures
        //                //foreach (var item in modelVm.pictursList)
        //                //{
        //                //    item.buisnessId = buisnessid;
        //                //    item.numberOfPicture = i;
        //                //    isSuccess = _repository.CreatePictursForBuisness(item);
        //                //    if (isSuccess < 0)
        //                //        return -2;
        //                //    i++;
        //                //}
        //                //create the cover picture
        //                //BuisnessCoverPicture coverPicture = new BuisnessCoverPicture();
        //                //coverPicture.buisnessId = buisnessid;
        //                //coverPicture.url = modelVm.coverPicture;
        //                //isSuccess = _repository.CreateCoverPictureForBuisness(coverPicture);

        //                //create the logo picture
        //                //BuisnessLogo logoPicture = new BuisnessLogo();
        //                //logoPicture.buisnessId = buisnessid;
        //                //logoPicture.url = modelVm.logoPicture;
        //                //isSuccess = _repository.CreateLogoPictureForBuisness(logoPicture);

        //                ////create the BuisnessCategorySubCategory
        //                //BuisnessSubCategory tempBuisnessSubCategory = new BuisnessSubCategory();

        //                //check each list in the array :  
        //                #endregion

        //                #region sub category


        //                #endregion

        //                #region sub category barter



        //                #endregion

        //                #region status    
        //                if (modelVm.id == 0 || modelVm.id == null)
        //                {
        //                    BuisnessStatus status = new BuisnessStatus
        //                    {
        //                        buisnessId = buisnessid,
        //                        statusId = 2,
        //                        startDate = DateTime.Now,
        //                        endDate = DateTime.Now.AddYears(3)
        //                    };
        //                    _repository.CreateStatus(status);
        //                }
        //                #endregion

        //                #region areas

        //                #endregion
        //            }
        //        }
        //        if (buisnessid >= 0)
        //            return buisnessid;
        //        else
        //            return -1;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
        //public bool SaveFileLocalDisc2(string path, byte[] fileBytes)
        //{
        //    try
        //    {
        //        using (Stream file = File.OpenWrite(path))
        //        {
        //            file.Write(fileBytes, 0, fileBytes.Length);
        //            file.Close();
        //        }
        //        return true;
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine("error while saving file to disk : {0}", ex.Message);
        //        return false;
        //    }
        //}
        //העדכון ביחד עם היצירה
        //public int updateBuisness(BuisnessVm modelVm)
        //{
        //    //צריך להוסיף עדכון של הרשומות מטבלאות הקטגוריות והתת קטגוריות, כרגע אין לכך התיחסות
        //    try
        //    {
        //        int id = -1;
        //        if (modelVm != null)
        //        {
        //            Buisness model = _mapper.Map<Buisness>(modelVm);
        //            id = _repository.updateBuisness(model);
        //        }
        //        if (id >= 0)
        //            return id;
        //        else
        //            return -1;
        //    }
        //    catch (Exception ex)
        //    {

        //        throw ex;
        //    }

        //}
    }
}
