using DataService;
using Microsoft.EntityFrameworkCore;
using ModelService.windoModels;
using System;
using System.Collections.Generic;
using System.Linq;


namespace CMS_CORE_NG.Repository
{
    public class CollaborationsRepository
    {
        private readonly ApplicationDbContext _db;

        public CollaborationsRepository(ApplicationDbContext db)
        {
            _db = db;
        }
        public List<CaseStudy> GetAllCS()
        {
            try
            {
                List<CaseStudy> csList = _db.CaseStudies
                     .Include(i => i.Pictures)
                     .Include(i => i.BusinessesInCaseStudy).ThenInclude(b => b.Buisness)
                     .Include(i=> i.PaidTransaction)
                     .Include(i=> i.BarterDeal)
                     .Include(i=> i.JointProject.BuisnessesInCollaborations)
                     .OrderByDescending(i=> i.ReportDate)
                     .ToList();
                if (csList.Count > 0)
                {
                    foreach (var CS in csList)
                    {
                        switch (CS.FromTable)
                        {
                            case (int)FromTable.PaidTransaction:
                                CS.PaidTransaction.SupplierBusiness = _db.Buisness.Find(CS.PaidTransaction.SupplierBusinessId);
                                CS.PaidTransaction.ConsumerBusiness = _db.Buisness.Find(CS.PaidTransaction.ConsumerBusinessId);
                                break;
                            case (int)FromTable.BarterDeal:
                                CS.BarterDeal.ReportsBusiness = _db.Buisness.Find(CS.BarterDeal.ReportsBusinessId);
                                CS.BarterDeal.PartnerBusiness = _db.Buisness.Find(CS.BarterDeal.PartnerBusinessId);
                                break;
                            default:
                                break;
                        }
                    }
                }
                return csList;

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public List<CaseStudy> GetLastCS()
        {
            try
            {
                List<CaseStudy> csList = _db.CaseStudies
                     .Include(i => i.Pictures)
                     .Include(i => i.BusinessesInCaseStudy).ThenInclude(b => b.Buisness)
                     .Include(i => i.PaidTransaction)
                     .Include(i => i.BarterDeal)
                     .Include(i => i.JointProject.BuisnessesInCollaborations)
                     .OrderByDescending(i => i.ReportDate).Take(3)
                     .ToList();
                if (csList.Count > 0)
                {
                    foreach (var CS in csList)
                    {
                        switch (CS.FromTable)
                        {
                            case (int)FromTable.PaidTransaction:
                                CS.PaidTransaction.SupplierBusiness = _db.Buisness.Find(CS.PaidTransaction.SupplierBusinessId);
                                CS.PaidTransaction.ConsumerBusiness = _db.Buisness.Find(CS.PaidTransaction.ConsumerBusinessId);
                                break;
                            case (int)FromTable.BarterDeal:
                                CS.BarterDeal.ReportsBusiness = _db.Buisness.Find(CS.BarterDeal.ReportsBusinessId);
                                CS.BarterDeal.PartnerBusiness = _db.Buisness.Find(CS.BarterDeal.PartnerBusinessId);
                                break;
                            default:
                                break;
                        }
                    }
                    return csList;
                }
                return null;

         
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public CaseStudy getCaseStudyById(int id)
        {
            try
            {
                CaseStudy CS = _db.CaseStudies
               .Include(i => i.Pictures)
               .Include(i => i.BusinessesInCaseStudy).ThenInclude(b=>b.Buisness)
               .Include(i => i.PaidTransaction)
               .Include(i => i.BarterDeal)
               .Include(i => i.JointProject.BuisnessesInCollaborations)
               .Include(i=>i.CustomerResponseses)
               .FirstOrDefault(i => i.Id == id);
                
                if (CS != null)
                {
                    switch (CS.FromTable)
                    {
                        case (int)FromTable.PaidTransaction:
                            CS.PaidTransaction.SupplierBusiness = _db.Buisness.Find(CS.PaidTransaction.SupplierBusinessId);
                            CS.PaidTransaction.ConsumerBusiness = _db.Buisness.Find(CS.PaidTransaction.ConsumerBusinessId);
                            break;
                        case (int)FromTable.BarterDeal:
                            CS.BarterDeal.ReportsBusiness = _db.Buisness.Find(CS.BarterDeal.ReportsBusinessId);
                            CS.BarterDeal.PartnerBusiness = _db.Buisness.Find(CS.BarterDeal.PartnerBusinessId);
                            break;
                        default:
                            break;
                    }
                    foreach (var business in CS.BusinessesInCaseStudy)
                    {
                        if (business.BuinessOwnerNameForCS == null)
                        {
                            var user = _db.Users.FirstOrDefault(x => x.Email == business.Buisness.userId);
                            business.BuinessOwnerNameForCS = user.Firstname;
                        }
                    }
                }
                return CS;
            }
            catch (Exception)
            {

                throw;
            }
           
        }
        //יצירה של קייס סטדי וביזנס בקייס סטדי
        //בטבלה המתאימה IfDisplayedInCS עדכון של השדה 
        public int createCaseStudy(CaseStudy model)
        {
            try
            {
                model.BusinessesInCaseStudy = null;
                model.Pictures = null;
                model.PaidTransaction = null;
                model.BarterDeal = null;
                model.JointProject = null;
                model.ReportDate = DateTime.Now;
                switch (model.FromTable)
                {
                    case (int) FromTable.PaidTransaction:
                        PaidTransaction pt = _db.PaidTransactions.Find(model.PaidTransactionID);
                        pt.IfDisplayedInCS = true;
                        _db.PaidTransactions.Update(pt);
                        break;
                    case (int)FromTable.BarterDeal:
                        BarterDeal bd = _db.BarterDeals.Find(model.BarterDealID);
                        bd.IfDisplayedInCS = true;
                        _db.BarterDeals.Update(bd);
                        break;
                    case (int)FromTable.JointProject:
                        JointProject jp = _db.JointProjects.Find(model.JointProjectID);
                        jp.IfDisplayedInCS = true;
                        _db.JointProjects.Update(jp);
                        break;
                    default:
                        break;
                }
                _db.CaseStudies.Add(model);
                _db.SaveChanges();
                return model.Id;
            }
            catch (Exception e)
            {
                throw;
            }
        }
        
        public int createBusinessInCaseStudy(BusinessInCaseStudy businessInCaseStudy)
        {
            try
            {
                _db.BusinessesInCaseStudy.Add(businessInCaseStudy);
                _db.SaveChanges();
                return businessInCaseStudy.Id;
            }
            catch (Exception)
            {
                return -1;
                throw;
            }
        }

        public int createCaseStudyPicture(CaseStudyPicture caseStudyPicture)
        {
            _db.CaseStudyPictures.Add(caseStudyPicture);
            _db.SaveChanges();
            return caseStudyPicture.Id;
        }

        public int createCaseStudyCustomerResponse(CaseStudyCustomerResponses model)
        {
            _db.CustomerResponses.Add(model);
            _db.SaveChanges();
            return model.Id;
        }
        public bool deleteCaseStudyPicture(int id)
        {
            try
            {
                var CaseStudyPicture = _db.CaseStudyPictures.Find(id);
                _db.CaseStudyPictures.Remove(CaseStudyPicture);
                _db.SaveChanges();
                return true;
            }
            catch (Exception)
            {

                throw;
            }
            
        }

        public bool deleteCaseStudyCustomerResponse(int id)
        {
            try
            {
                var CustomerResponse = _db.CustomerResponses.Find(id);
                _db.CustomerResponses.Remove(CustomerResponse);
                _db.SaveChanges();
                return true;
            }
            catch (Exception)
            {

                throw;
            }
            
        }
        public List<CaseStudyPicture> caseStudyPicturesByCS(int caseStudyId)
        {
            try
            {
                return _db.CaseStudyPictures.Where(x => x.CaseStudyId == caseStudyId).ToList();
            }
            catch (Exception)
            {

                throw;
            }
        }
        public List<CaseStudyCustomerResponses> caseStudyCustomerResponsesByCS(int caseStudyId)
        {
            try
            {
                return _db.CustomerResponses.Where(x => x.CaseStudyId == caseStudyId).ToList();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool updateCaseStudy(CaseStudy model)
        {
            try
            {
                CaseStudy cs = _db.CaseStudies.Find(model.Id);
                cs.MarketingTitle = model.MarketingTitle;
                cs.BusinessTitle = model.BusinessTitle;
                cs.Description = model.Description;
                cs.Challenge = model.Challenge;
                cs.PowerMultiplier = model.PowerMultiplier;
                cs.CustomersGain = model.CustomersGain;
                cs.PicGuid = model.PicGuid;
                cs.CustomerResponseses = model.CustomerResponseses;
                cs.BusinessesInCaseStudy = model.BusinessesInCaseStudy;
                cs.CustomerResponseses = model.CustomerResponseses;
                //cs.Pictures = model.Pictures;
                
                _db.CaseStudies.Update(cs);
                _db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                //return false;
                throw ex;
            }
        }

        public bool deleteCollaborationByIDAndTable(FromTable fromTable, int id)
        {
            try
            {
                switch (fromTable)
                {
                    case FromTable.PaidTransaction:
                        var pd = _db.PaidTransactions.Find(id);
                        _db.PaidTransactions.Remove(pd);
                        break;
                    case FromTable.BarterDeal:
                        var bd = _db.BarterDeals.Find(id);
                        _db.BarterDeals.Remove(bd);
                        break;
                    case FromTable.JointProject:
                        var jp = _db.JointProjects.Find(id);
                        _db.JointProjects.Remove(jp);
                        break;
                    default:
                        break;
                }
                _db.SaveChanges();
                return true;
            }
            catch (Exception)
            {

                throw;
            }
          
        }
        #region gets collaboration by id
        public PaidTransaction GetPaidTransactionByID(int id)
        {
            try
            {
                PaidTransaction pd = _db.PaidTransactions
               //.Include(i => i.SupplierBusiness)
               //.Include(i => i.ConsumerBusiness)
               .FirstOrDefault(i => i.Id == id);
                if (pd!=null)
                {//קשור ידני של העסק מספק שרות והעסק הקונה את השרות
                    pd.SupplierBusiness = _db.Buisness.Find(pd.SupplierBusinessId);
                    pd.ConsumerBusiness = _db.Buisness.Find(pd.ConsumerBusinessId);
                }
                return pd;
            }
            catch (Exception)
            {

                throw;
            }
           
        }
        public BarterDeal GetBarterDealByID(int id)
        {
            try
            {
                BarterDeal bd = _db.BarterDeals
               //.Include(i => i.ReportsBusiness)
               //.Include(i => i.PartnerBusiness)
               .FirstOrDefault(i => i.Id == id);
                if (bd != null)
                {
                    //קשור של העסק המדווח והעסק השותף לברטר
                    bd.ReportsBusiness = _db.Buisness.Find(bd.ReportsBusinessId);
                    bd.PartnerBusiness = _db.Buisness.Find(bd.PartnerBusinessId);
                }
                return bd;
            }
            catch (Exception)
            {

                throw;
            }
           
        }
        public JointProject GetJointProjectByID(int id)
        {
            try
            {
                JointProject jp = _db.JointProjects
               .Include(i => i.BuisnessesInCollaborations).ThenInclude(b=>b.Business)
               .Include(i=>i.CollaborationType)
               .FirstOrDefault(i => i.Id == id);
                return jp;
            }
            catch (Exception)
            {

                throw;
            }
           
        }
        #endregion
        #region get All collaboration
        public List<PaidTransaction> getAllPaidTransactions()
        {
            try
            {
                List<PaidTransaction> ptlist = _db.PaidTransactions.ToList();
                foreach (var pt in ptlist)
                {
                    pt.SupplierBusiness = _db.Buisness.Find(pt.SupplierBusinessId);
                    pt.ConsumerBusiness = _db.Buisness.Find(pt.ConsumerBusinessId);
                }
                return ptlist;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public List<BarterDeal> getAllBarterDeals()
        {
            try
            {
                List<BarterDeal> brlist = _db.BarterDeals.ToList();
                foreach (var bd in brlist)
                {
                    bd.ReportsBusiness = _db.Buisness.Find(bd.ReportsBusinessId);
                    bd.PartnerBusiness = _db.Buisness.Find(bd.PartnerBusinessId);
                }
                return brlist;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public List<JointProject> getAllJointProjects()
        {
            try
            {
                List<JointProject> jplist = _db.JointProjects
                    .Include(i=>i.BuisnessesInCollaborations).ThenInclude(b=>b.Business)
                    .Include(i=>i.CollaborationType)
                    .ToList();
                return jplist;
            }
            catch (Exception)
            {

                throw;
            }
        }
        #endregion
        #region get cs by collaboration id
        public CaseStudy GetCS_ByPaidTransactionId(int paidTransactionId)
        {
            try
            {
                CaseStudy cs = _db.CaseStudies
                                .Include(i => i.Pictures)
                                .Include(i => i.BusinessesInCaseStudy).ThenInclude(business => business.Buisness)
                                .Include(i=>i.PaidTransaction)
                                .Include(i=>i.CustomerResponseses)
                                .FirstOrDefault(i => i.PaidTransactionID == paidTransactionId);
                if (cs != null)
                {//קשור ידני של העסק מספק שרות והעסק הקונה את השרות
                    cs.PaidTransaction.SupplierBusiness = _db.Buisness.Find(cs.PaidTransaction.SupplierBusinessId);
                    cs.PaidTransaction.ConsumerBusiness = _db.Buisness.Find(cs.PaidTransaction.ConsumerBusinessId);

                }
                return cs;
            }
            catch (Exception)
            {

                throw;
            }
            
        }
        public CaseStudy GetCS_ByBarterDealId(int barterDealId)
        {
            try
            {
                CaseStudy cs = _db.CaseStudies
                  .Include(i => i.Pictures)
                  .Include(i => i.BusinessesInCaseStudy).ThenInclude(business=> business.Buisness)
                  .Include(i=>i.BarterDeal)
                  .Include(i=>i.CustomerResponseses)
                  .FirstOrDefault(i => i.BarterDealID == barterDealId);
                if (cs != null)
                {//קשור ידני של העסקים
                    cs.BarterDeal.ReportsBusiness = _db.Buisness.Find(cs.BarterDeal.ReportsBusinessId);
                    cs.BarterDeal.ReportsBusiness = _db.Buisness.Find(cs.BarterDeal.ReportsBusinessId);
                }
                return cs;
            }
            catch (Exception)
            {

                throw;
            }
          
        }
        public CaseStudy GetCS_ByJointProjectId(int jointProjectId)
        {
            try
            {
                CaseStudy cs = _db.CaseStudies
                    .Include(i => i.Pictures)
                    .Include(i => i.JointProject)
                    .Include(i => i.BusinessesInCaseStudy).ThenInclude(business => business.Buisness)
                    .Include(i=>i.CustomerResponseses)
                    .FirstOrDefault(i => i.JointProjectID == jointProjectId);
                return cs;

            }
            catch (Exception)
            {

                throw;
            }
        }

        #endregion

        public List<CaseStudy> GetCSByBuissinesID(int BusinessID)
        {
            try
            {
                List<BusinessInCaseStudy> bs = _db.BusinessesInCaseStudy.Where(x => x.BusinessId == BusinessID)
                                                                    .Include(x => x.CaseStudy).ThenInclude(csp => csp.Pictures).Include(xd => xd.CaseStudy).ThenInclude(cs => cs.BusinessesInCaseStudy).ThenInclude(b=>b.Buisness).ToList();
                List<CaseStudy> cs = bs.Map(x => x.CaseStudy).ToList();

                return cs;
            }
            catch (Exception)
            {

                throw;
            }
            
        }

    }

}
