using DataService;
using Microsoft.EntityFrameworkCore;
using ModelService.windoModels;
using ModelService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Windo.Models;

namespace Windo.Repository
{
    public class BuisnessRepository    // : IBuisnessRepos
    {
        public readonly ApplicationDbContext _db;
        public BuisnessRepository(ApplicationDbContext db)
        {
            _db = db;
        }
        public Buisness GetBuisnessByEmailId(string email)
        {
            try
            {
                if (email != null)
                {
                    var buisnessVm = _db.Buisness
                            .Include(i => i.BuisnessSubCategory)
                            .ThenInclude(i => i.CategorySubCategory)
                            .ThenInclude(i => i.SubCategory)
                            .Include(i => i.BuisnessSubCategory)
                            .ThenInclude(i => i.CategorySubCategory)
                            .ThenInclude(i => i.Category)
                            .Include(i => i.BuisnessSubCategoryBarter)
                            .ThenInclude(i => i.CategorySubCategory)
                            .ThenInclude(i => i.Category)
                            .Include(i => i.BuisnessSubCategoryBarter)
                            .ThenInclude(i => i.CategorySubCategory)
                            .ThenInclude(i => i.SubCategory)
                            .Include(i => i.BuisnessPicture)
                            .Include(i => i.BuisnessArea)
                            .Include(i => i.BuisnessStatus)
                            .Include(i=> i.User)
                            .Include(i=> i.BusinessCategoriesNotify)
                            .FirstOrDefault(x => x.userId.Equals(email)
                            && x.BuisnessStatus.Select(x => x).Where(s => s.buisnessId == x.Id).FirstOrDefault().statusId == 2
                            && (DateTime.Now < x.BuisnessStatus.Select(x => x).Where(s => s.buisnessId == x.Id).FirstOrDefault().endDate
                            || x.BuisnessStatus.Select(x => x).Where(s => s.buisnessId == x.Id).FirstOrDefault().endDate == null))
                            ;
                    return buisnessVm;
                }
                return null;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        //
        public ApplicationUser getUserByEmail(string email)
        {
            ApplicationUser user = _db.ApplicationUsers.FirstOrDefault(x => x.Email == email);
            return user;
        }
        public string getBusinessUserFirstnameByEmail(string email)
        {
            ApplicationUser user = _db.ApplicationUsers.FirstOrDefault(x => x.Email == email);
            return user.Firstname;
        }
        //לא בשימוש
        //פונק' שמביאה את כל העסקים האחרונים - השערה: הפונק' מיותרת ואין צורך להוציא פעמים את העסקים?
        public List<Buisness> GetListOfLatestUpdatedBuisnesses()
        {
            try
            {
                var buisness = _db.Buisness
                           .Include(i => i.BuisnessSubCategory)
                           .ThenInclude(i => i.CategorySubCategory)
                           .ThenInclude(i => i.Category)
                           .Include(x => x.BuisnessSubCategory)
                            .ThenInclude(i => i.CategorySubCategory)
                           .ThenInclude(x => x.SubCategory)
                           .Include(i => i.BuisnessSubCategoryBarter)
                           .ThenInclude(i => i.CategorySubCategory)
                           .ThenInclude(i => i.Category)
                            .Include(x => x.BuisnessSubCategoryBarter)
                           .ThenInclude(x => x.CategorySubCategory)
                           .ThenInclude(x => x.SubCategory)
                           .Include(i => i.BuisnessPicture)
                           .Include(i => i.BuisnessArea)
                           .Include(u => u.BuisnessStatus)
                           .ThenInclude(s=> s.Status)
                           .Include(i=> i.User)
                           .Where(x => x.BuisnessStatus.Select(x => x)
                           .Where(s => s.buisnessId == x.Id).FirstOrDefault().Status.name == "עסק מאושר"
                            && (DateTime.Now < x.BuisnessStatus.Select(x => x)
                            .Where(s => s.buisnessId == x.Id).FirstOrDefault().endDate
                            || x.BuisnessStatus.Select(x => x).Where(s => s.buisnessId == x.Id).FirstOrDefault().endDate == null))
                           .OrderByDescending(x=>x.BuisnessStatus.FirstOrDefault().startDate)
                           .Take(10)
                           .ToList();
                return buisness;
                //        var buisnessVm = (from b in _db.Buisness
                //                          join bs in _db.BuisnessStatus
                //                          on b.Id equals bs.buisnessId
                //                          where bs.statusId == 2 && (DateTime.Now < bs.endDate || bs.endDate == null)
                //                          select new BuisnessVm
                //                          {
                //                              Id = b.Id,
                //                              userId = b.userId,
                //                              buisnessName = b.buisnessName,
                //                              phoneNumber1 = b.phoneNumber1,
                //                              phoneNumber2 = b.phoneNumber2,
                //                              address = b.address,
                //                              actionDiscription = b.actionDiscription,
                //                              discription = b.discription,
                //                              buisnessWebSiteLink = b.buisnessWebSiteLink,
                //                              isdisplayBusinessOwnerName = b.isdisplayBusinessOwnerName,
                //                              ispayingBuisness = b.ispayingBuisness,
                //                              isburterBuisness = b.isburterBuisness,
                //                              iscollaborationBuisness = b.iscollaborationBuisness,
                //                              isburterPossibleInAllCategory = b.isburterPossibleInAllCategory,
                //                              isopenToSuggestionsForBarter = b.isopenToSuggestionsForBarter,
                //                              product1 = b.product1,
                //                              product2 = b.product2,
                //                              barterProduct1 = b.barterProduct1,
                //                              barterProduct2 = b.barterProduct2,
                //                              UpdatedBusinessStatus = b.UpdatedBusinessStatus,
                //                              views = b.views,
                //                              tags = b.tags,
                //                              lastupdatedStartDate = bs.startDate,
                //                              coverPicture = b.coverPicture,
                //                              logoPicture = b.logoPicture,
                //                              ownerName = _db.ApplicationUsers.Where(v => v.Id == b.userId).FirstOrDefault().Firstname,
                //                              pictursList = b.BuisnessPicture
                //                                             .Where(p => b.Id == p.buisnessId).ToList(),
                //                              areasList = _db.BuisnessArea
                //                              .Where(v => v.buisnessId == b.Id)
                //                                       .Join(_db.Area,
                //                                       ba => ba.areaId,
                //                                       a => a.Id,
                //                                             (ba, a) => new Area
                //                                             {
                //                                                 Id = a.Id,
                //                                                 name = a.name,
                //                                             }).ToList(),
                //                              CategorySubCategoryList1 = _db.BuisnessSubCategory
                //                                     .Where(v => v.buisnessId == b.Id)
                //                                     .Join(_db.CategorySubCategory,
                //                                             bsc => bsc.categorySubCategoryId,
                //                                             cs => cs.Id,
                //                                             (bsc, cs) => new CategorySubCategoryVm
                //                                             {
                //                                                 Id = cs.Id,
                //                                                 categoryId = cs.categoryId,
                //                                                 categoryName = _db.Category.Where(v => v.Id == cs.categoryId).FirstOrDefault().name,
                //                                                 subCategoryId = cs.subCategoryId,
                //                                                 subCategoryName = _db.SubCategory.Where(v => v.Id == cs.subCategoryId).FirstOrDefault().name,
                //                                                 isPossibleInBarter = bsc.isPossibleInBarter
                //                                             }).ToList(),
                //                              CategorySubCategoryBarterList1 = _db.BuisnessSubCategoryBarter
                //                                     .Where(v => v.buisnessId == b.Id)
                //                                     .Join(_db.CategorySubCategory,
                //                                             bscb => bscb.categorySubCategoryId,
                //                                             csb => csb.Id,
                //                                             (bscb, csb) => new CategorySubCategoryVm
                //                                             {
                //                                                 Id = csb.Id,
                //                                                 categoryId = csb.categoryId,
                //                                                 categoryName = _db.Category.Where(v => v.Id == csb.categoryId).FirstOrDefault().name,
                //                                                 subCategoryId = csb.subCategoryId,
                //                                                 subCategoryName = _db.SubCategory.Where(v => v.Id == csb.subCategoryId).FirstOrDefault().name
                //                                             }).ToList(),
                //                          }).OrderByDescending(x => x.lastupdatedStartDate).Take<BuisnessVm>(10).ToList();

            }
            catch (Exception e)
            {
                throw e;
            }
        }

        //todo - someone check if that right
        //bl לא נעשה פה מיפוי - אלא ב
        //public List<BuisnessVm> GetListOfBuisnesses()
        public List<Buisness> GetListOfBuisnesses()
        {
            try
            {
                //todo - check if the status is true
                var buisness = _db.Buisness
                           .Include(i => i.BuisnessSubCategory)
                           .ThenInclude(i => i.CategorySubCategory)
                           .ThenInclude(i => i.Category)
                           .Include(x => x.BuisnessSubCategory)
                            .ThenInclude(i => i.CategorySubCategory)
                           .ThenInclude(x => x.SubCategory)
                           .Include(i => i.BuisnessSubCategoryBarter)
                           .ThenInclude(i => i.CategorySubCategory)
                           .ThenInclude(i => i.Category)
                            .Include(x => x.BuisnessSubCategoryBarter)
                           .ThenInclude(x => x.CategorySubCategory)
                           .ThenInclude(x => x.SubCategory)
                           .Include(i => i.BuisnessPicture)
                           .Include(i => i.BuisnessArea)
                           .Include(u => u.BuisnessStatus)
                           .Include(i => i.User)
                           .Where(x => x.BuisnessStatus.Select(x => x).Where(s => s.buisnessId == x.Id).FirstOrDefault().statusId == 2
                            && (DateTime.Now < x.BuisnessStatus.Select(x => x).Where(s => s.buisnessId == x.Id).FirstOrDefault().endDate
                            || x.BuisnessStatus.Select(x => x).Where(s => s.buisnessId == x.Id).FirstOrDefault().endDate == null))
                           .ToList();




                //.Where(x => x.BuisnessStatus
                //.Where(s => s.buisnessId == x.Id)
                //.Where(s=> s.statusId == 2)
                //.Where(s => s.endDate >= DateTime.Now || s.endDate == null)
                //).

                //from b in _db.Buisness
                //                          join bs in _db.BuisnessStatus
                //                          on b.Id equals bs.buisnessId
                //                          where bs.statusId == 2 && (DateTime.Now < bs.endDate || bs.endDate == null)
                //.Where(x => x.Id == id).FirstOrDefault();

                return buisness;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        #region
        public Buisness GetBuisnessById2(int id)
        {
            try
            {
                Buisness buisness = new Buisness();
                if (id >= 0)//  ביטוי אחר ל-לא שווה נל 
                {
                    buisness = _db.Buisness.FirstOrDefault(b => b.Id == id);
                }
                return buisness;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public List<Buisness> GetListOfBuisnesses2()
        {
            try
            {
                List<Buisness> buisnessesList = new List<Buisness>();
                var temp = (from b in _db.Buisness select b);
                buisnessesList = temp.ToList();
                return buisnessesList;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        #endregion
        public int createBuisness(Buisness model)
        {
            var CheckBusinnes = GetBuisnessByEmailId(model.userId);
            //https://stackoverflow.com/questions/5212751/how-can-i-retrieve-id-of-inserted-entity-using-entity-framework
            try
            {
                int id = -1;
                if (model != null)
                {
                    if (model.Id == 0 && CheckBusinnes == null)
                    {
                        _db.Buisness.Add(model);
                        _db.SaveChanges();
                        id = model.Id;
                    }
                    else
                    {
                        //_db.Buisness.Update(model);
                        //_db.SaveChanges();
                        //id = model.Id;


                        //_db.Buisness.Attach(model);
                        //_db.Entry(model).Property(p => p.Id).IsModified = true;
                        //_db.SaveChanges();

                        var bs = _db.Buisness.Find(model.Id);
                        bs.buisnessName = model.buisnessName;
                        bs.address = model.address;
                        bs.product1 = model.product1;
                        bs.product2 = model.product2; 
                        bs.barterProduct1 = model.barterProduct1;
                        bs.barterProduct2 = model.barterProduct2;
                        bs.buisnessWebSiteLink = model.buisnessWebSiteLink;
                        bs.businessEmailAddress = model.businessEmailAddress;
                        bs.coverPictureId = model.coverPictureId;
                        bs.discription = model.discription;
                        bs.isburterBuisness = model.isburterBuisness;
                        bs.isopenToSuggestionsForBarter = model.isopenToSuggestionsForBarter;
                        bs.ispayingBuisness = model.ispayingBuisness;
                        bs.iscollaborationBuisness = model.iscollaborationBuisness;
                        bs.isdisplayBusinessOwnerName = model.isdisplayBusinessOwnerName;
                        bs.WantedGetDailyNotification = model.WantedGetDailyNotification;
                        bs.WantedGetHelpNotification = model.WantedGetHelpNotification;
                        bs.actionDiscription = model.actionDiscription;
                        bs.logoPictureId = model.logoPictureId;
                        bs.OptionalCollaborationDescription = model.OptionalCollaborationDescription;
                        bs.phoneNumber1 = model.phoneNumber1;
                        bs.phoneNumber2 = model.phoneNumber2;
                       


                        _db.Buisness.Update(bs);
                        _db.SaveChanges();
                        id = model.Id;
                    }
                }
                else
                {
                    //the object getten from the user is empty
                    return -2;
                }
                if (id >= 0)
                    return id;
                else
                    //the added action wasnt succeeded
                    return -1;
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        //פונק' עדכון הסטטוס בעת יצירת העסק
        public void CreateStatus(BuisnessStatus businessStatus)
        {
            _db.BuisnessStatus.Add(businessStatus);
            _db.SaveChanges();
        }
        public bool updateStatusStartDate(int buisnessId)
        {
            try
            {
                var buisenessStatus = _db.BuisnessStatus.FirstOrDefault(x => x.buisnessId == buisnessId);
          
                buisenessStatus.startDate =  DateTime.Now;
                buisenessStatus.endDate = DateTime.Now.AddYears(3);
                _db.BuisnessStatus.Update(buisenessStatus);
                _db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {

                throw ex;
            }
         
        }
        //מחיקת קשרי הערים לעסקים
        public bool DeleteAreaConection(int buisnessId)
        {
            try
            {
                int id = 0;
                var areaArr = _db.BuisnessArea.Where(x => x.buisnessId == buisnessId).ToList();
                _db.BuisnessArea.RemoveRange(areaArr);
                _db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //יצירת קשרי הערים לעסקים
        public void CreateAreaConection(List<BuisnessArea> buisnessArea)
        {
            _db.BuisnessArea.AddRange(buisnessArea);
            _db.SaveChanges();
        }
        //public int updateBuisness(Buisness model)
        //{
        //    //https://stackoverflow.com/questions/25894587/how-to-update-record-using-entity-framework-6
        //    try
        //    {
        //        int success = 0;
        //        var updatedObj = _db.Buisness.Where(b => b.Id == model.Id).First();
        //        _db.Entry(updatedObj).CurrentValues.SetValues(model);

        //        //_db.Buisness.Update(model);
        //        success = _db.SaveChanges();
        //        if (success > 0)
        //        {
        //            //success
        //            return success;
        //        }
        //        else
        //        {
        //            //failed
        //            return success;
        //        }
        //    }
        //    catch (Exception ex)
        //    {

        //        throw ex;
        //    }

        //}
        public int deleteBuisness(int id)
        {
            try
            {
                int success = 0;
                if (id >= 0)
                {
                    //https://stackoverflow.com/questions/17723276/delete-a-single-record-from-entity-framework
                    var buisness = new Buisness { Id = id };
                    _db.Buisness.Attach(buisness);
                    _db.Buisness.Remove(buisness);
                    success = _db.SaveChanges();
                }
                if (success > 0)
                {
                    //success
                    return success;
                }
                else
                {
                    //failed
                    return success;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public Guid CreatePictursForBuisness(List<BuisnessPicture> pictur)
        {
            try
            {
                Guid id = Guid.Empty;
                _db.BuisnessPicture.AddRange(pictur);
                _db.SaveChanges();
                //id = pictur.buisnessPictureId;
                return id;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void DeleteBusinessPic(List<BuisnessPicture> bpl)
        {
            try
            {
                _db.BuisnessPicture.RemoveRange(bpl);
                _db.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
        }
        //insert the new sub category after update/create
        public int CreateBuisnessSubCategoryListForBuisness(List<BuisnessSubCategory> tempBuisnessSubCategory)
        {
            try
            {
                int id = 0;
                _db.BuisnessSubCategory.AddRange(tempBuisnessSubCategory);
                _db.SaveChanges();
                return id;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int CreateBuisnessSubCategoryBarterListForBuisness(List<BuisnessSubCategoryBarter> tempBuisnessSubCategory)
        {
            try
            {
                int id = 0;
                _db.BuisnessSubCategoryBarter.AddRange(tempBuisnessSubCategory);
                _db.SaveChanges();
                return id;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool GetSubCategoryById(int buisnessId)//מחזיר אם העסק הכניס כבר תת קטגוריה או לא
        {
            BuisnessSubCategory busc = _db.BuisnessSubCategory
                .Select(Busc => Busc)
                .Where(B => B.buisnessId == buisnessId)
                .FirstOrDefault();
            if (busc != null)
                return true;
            else
                return false;

        }
        public bool GetSubCategoryForBarterById(int buisnessId)//מחזיר אם העסק הכניס כבר תת קטגוריה או לא
        {
            BuisnessSubCategoryBarter busc = _db.BuisnessSubCategoryBarter
                .Select(Busc => Busc)
                .Where(B => B.buisnessId == buisnessId)
                .FirstOrDefault();
            if (busc != null)
                return true;
            else
                return false;

        }
        //delete the sub category from businnesSubCategory before insert the new category
        public bool DeleteBuisnessSubCategoryByBuisnessId(int buisnessId)
        {
            try
            {
                int id = 0;
                var catArr = _db.BuisnessSubCategory.Where(x => x.buisnessId == buisnessId).ToList();
                _db.BuisnessSubCategory.RemoveRange(catArr);
                _db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //delete the sub category from BuisnessSubCategoryBarter before insert the new category - for burter
        public bool DeleteBuisnessSubCategoryBarterByBuisnessId(int buisnessId)
        {
            try
            {
                int id = 0;
                var catArr = _db.BuisnessSubCategoryBarter.Where(x => x.buisnessId == buisnessId).ToList();
                _db.BuisnessSubCategoryBarter.RemoveRange(catArr);
                _db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public int CreateBuisnessSubCategoryForBuisness(BuisnessSubCategory tempBuisnessSubCategory)
        {
            try
            {
                int id = 0;
                _db.BuisnessSubCategory.Add(tempBuisnessSubCategory);
                _db.SaveChanges();
                id = tempBuisnessSubCategory.Id;
                return id;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int CreateBuisnessSubCategoryBarterForBuisness(BuisnessSubCategoryBarter tempBuisnessSubCategory)
        {
            try
            {
                int id = 0;
                _db.BuisnessSubCategoryBarter.Add(tempBuisnessSubCategory);
                _db.SaveChanges();
                id = tempBuisnessSubCategory.Id;
                return id;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<BuisnessPicture> GetPictursListByBuisnessId(int id)
        {
            try
            {
                List<BuisnessPicture> pictursList = _db.BuisnessPicture
                    .Select(p => p)
                    .Where(pi => pi.buisnessId == id)
                    .ToList();
                return pictursList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> SaveBusinessCategoryNotify(int businessId, List< BusinessCategoryNotify> notifyList)
        {
            var list = await _db.BusinessCategoriesNotify.Filter(x => x.BusinessId == businessId).ToListAsync();
            foreach (var cn in list)
            {
                if (notifyList.FirstOrDefault(x => x.categoryId == cn.categoryId) == null)
                {
                    _db.BusinessCategoriesNotify.Remove(cn);
                }
            }
            foreach (var cn in notifyList)
            {
                if (list.FirstOrDefault(x=>x.categoryId == cn.categoryId)==null)
                {
                    await _db.BusinessCategoriesNotify.AddAsync(new BusinessCategoryNotify() { Id = 0, BusinessId = businessId, categoryId = cn.categoryId });
                }
            }
            await _db.SaveChangesAsync();
            return true;
        }
        //logo & cover picturs functunality
        //public string GetCoverPictursByBuisnessId(int id)
        //{
        //    try
        //    {
        //        string picture;
        //        var temp = (from b in _db.BuisnessCoverPicture where b.buisnessId == id select b);
        //        picture = temp.ToString();
        //        return picture;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
        //public string GetLogoPictursByBuisnessId(int id)
        //{
        //    try
        //    {
        //        string picture;
        //        var temp = (from b in _db.BuisnessLogo where b.buisnessId == id select b);
        //        picture = temp.ToString();
        //        return picture;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
        //public int CreateLogoPictureForBuisness(BuisnessLogo pictur)
        //{
        //    try
        //    {
        //        int id = 0;
        //        _db.BuisnessLogo.Add(pictur);
        //        _db.SaveChanges();
        //        id = pictur.Id;
        //        return id;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
        //public int CreateCoverPictureForBuisness(BuisnessCoverPicture pictur)
        //{
        //    try
        //    {
        //        int id = 0;
        //        _db.BuisnessCoverPicture.Add(pictur);
        //        _db.SaveChanges();
        //        id = pictur.Id;
        //        return id;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}


        public List<BuisnessSubCategory> GetbuisnessSubCategoryByBuisnessId(int id)
        {
            try
            {
                List<BuisnessSubCategory> BuisnesssubCategoryList = new List<BuisnessSubCategory>();
                var temp = (from b in _db.BuisnessSubCategory where b.buisnessId == id select b);
                BuisnesssubCategoryList = temp.ToList();
                return BuisnesssubCategoryList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<BuisnessSubCategoryBarter> GetbuisnessSubCategoryBarterByBuisnessId(int id)
        {
            try
            {
                List<BuisnessSubCategoryBarter> BuisnesssubCategoryBarterList = new List<BuisnessSubCategoryBarter>();
                var temp = (from b in _db.BuisnessSubCategoryBarter where b.buisnessId == id select b);
                BuisnesssubCategoryBarterList = temp.ToList();
                return BuisnesssubCategoryBarterList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<Area> GetAreasList()
        {
            try
            {
                List<Area> areaList = new List<Area>();
                var temp = (from b in _db.Area select b);
                areaList = temp.ToList();
                return areaList;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public List<Buisness> GetNamesPicturesBuisnesses()
        {
            try
            {
                List<Buisness> namesPicturesBuisnesses = _db.Buisness.ToList();
                return namesPicturesBuisnesses;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public Buisness Get1NamesPicturesBuisness(int id)
        {
            try
            {
                Buisness namesPicturesBuisnesses = _db.Buisness.FirstOrDefault(x=>x.Id == id);
                return namesPicturesBuisnesses;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        #region paidTransaction
        public int createPaidTransaction(PaidTransaction model)
        {
            _db.PaidTransactions.Add(model);
            _db.SaveChanges();
            int id =  model.Id;
            return id;
        }

        //public PaidTransaction updatePaidTransaction(PaidTransaction model)
        //{

        //}
        #endregion

        #region barterDeal
        public int createBarterDeal(BarterDeal model)
        {
            model.ReportDate = DateTime.Now;
            _db.BarterDeals.Add(model);
            _db.SaveChanges();
            int id = model.Id;
            return id;
        }

        public int createCollaborationType(CollaborationType model)
        {
            _db.CollaborationTypes.Add(model);
            _db.SaveChanges();
            int id = model.Id;
            return id;
        }
        
        public int createJoinProject(JointProject model)
        {
            model.ReportDate = DateTime.Now;
            model.BuisnessesInCollaborations = null;
            _db.JointProjects.Add(model);

            _db.SaveChanges();
            int id = model.Id;
            return id;
        }
        public int createBusinessInCollaboration(BusinessInCollaboration model)
        {
            _db.BusinessInCollaborations.Add(model);
            _db.SaveChanges();
            int id = model.Id;
            return id;
        }

        public List<CollaborationType> GetCollaborationTypes()
        {
            return _db.CollaborationTypes.ToList();
        }
        #endregion
    }
}
//join bp in _db.BuisnessPicture
//on b.Id equals bp.buisnessId

//join ba in _db.BuisnessArea
//on b.Id equals ba.buisnessId
//join a in _db.Area
//on ba.areaId equals a.Id

//join bsc in _db.BuisnessSubCategory
//on b.Id equals bsc.buisnessId
//join csc in _db.CategorySubCategory
//on bsc.categorySubCategoryId equals csc.Id
//join c in _db.Category
//on csc.categoryId equals c.Id
//join sc in _db.SubCategory
//on csc.subCategoryId equals sc.Id

//join bscb in _db.BuisnessSubCategoryBarter
//on b.Id equals bscb.buisnessId
//join cscb in _db.CategorySubCategory
//on bscb.categorySubCategoryId equals cscb.Id
//join cb in _db.Category
//on cscb.categoryId equals cb.Id
//join scb in _db.SubCategory
//on cscb.subCategoryId equals scb.Id
