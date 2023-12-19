using System;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using ModelService.windoModels;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Windo.Controllers;
using Windo.Models;
using Windo.Repository;
using AutoMapper;
using Windo.BL;
using CMS_CORE_NG.Repository;
using static CMS_CORE_NG.Scoring;

namespace CMS_CORE_NG.BL
{
    public class CollaborationBl : ICollaborationBl
    {
        private readonly CollaborationsRepository _repository;
        public readonly IMapper _mapper;
        private IConfiguration _configuration;
        private readonly BuisnessBl _businessBL;
        private readonly IScoring _scoring;

        public CollaborationBl(IMapper mapper, CollaborationsRepository repository, IConfiguration configuration, BuisnessBl businessBL, IScoring scoring)
        {
            _repository = repository;
            _mapper = mapper;
            _configuration = configuration;
            _businessBL = businessBL;
            _scoring = scoring;
        }

        public CaseStudyVM getCaseStudyById(int id)
        {
            try
            {
                CaseStudyVM CS = _mapper.Map<CaseStudyVM>(_repository.getCaseStudyById(id));
                return CS;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public List<CaseStudyForCardsVM> GetAllCS()
        {
            try
            {
                List<CaseStudy> csList = _repository.GetAllCS();
                List<CaseStudyForCardsVM> caseStudyListvm = csList.Select(_mapper.Map<CaseStudy, CaseStudyForCardsVM>).ToList();
                return caseStudyListvm;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<CaseStudyForCardsVM> GetLastCS()
        {
            try
            {
                List<CaseStudy> csList = _repository.GetLastCS();
                List<CaseStudyForCardsVM> caseStudyListvm = csList?.Select(_mapper.Map<CaseStudy, CaseStudyForCardsVM>).ToList();
                return caseStudyListvm;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public CaseStudyVM getCaseStudyByCollaborationId(IdAndTableNameForCS idAndTableName)
        {
            CaseStudyVM CS = new CaseStudyVM()
            {
                Id = 0,
                FromTable = ((FromTable)idAndTableName.FromTable),
                BusinessesInCaseStudy = new List<BusinessInCaseStudyVM>(),
                CustomerResponses = new List<CaseStudyCustomerResponsesVM>(),
            };
            CS.CustomerResponses.Add(new CaseStudyCustomerResponsesVM());
            CaseStudy existCS;
            try
            {
                switch (idAndTableName.FromTable)
                {
                    case FromTable.PaidTransaction:
                        //או שקיים קייס סטדי ומחזירים אותו או שמייצרים אותו לפי העסקה בתשלום
                        existCS = _repository.GetCS_ByPaidTransactionId(idAndTableName.Id);
                        if (existCS == null)
                        {
                            PaidTransaction pd = _repository.GetPaidTransactionByID(idAndTableName.Id);
                            if (pd == null)
                            {
                                return null;
                            }
                            CS.PaidTransaction = _mapper.Map<PaidTransactionVM>(pd);
                            CS.PaidTransactionID = idAndTableName.Id;
                            CS.Description = CS.PaidTransaction.Description;
                            CS.PowerMultiplier = CS.PaidTransaction.Review;
                            //קשור של העסק המדווח לקייס סטדי
                            BusinessInCaseStudyVM SupplierBusiness = new BusinessInCaseStudyVM()
                            {
                                Id = 0,
                                BusinessId = CS.PaidTransaction.SupplierBusinessId,
                                Business = CS.PaidTransaction.SupplierBusiness
                            };
                            CS.BusinessesInCaseStudy.Add(SupplierBusiness);
                            //קשור של העסק השותף לקייס סטדי
                            BusinessInCaseStudyVM ConsumerBusiness = new BusinessInCaseStudyVM()
                            {
                                Id = 0,
                                BusinessId = (int)CS.PaidTransaction.ConsumerBusinessId,
                                Business = CS.PaidTransaction.ConsumerBusiness
                            };
                            CS.BusinessesInCaseStudy.Add(ConsumerBusiness);
                        }
                        else
                        {
                            CS = _mapper.Map<CaseStudyVM>(existCS);
                        }
                        break;
                    case FromTable.BarterDeal:
                        existCS = _repository.GetCS_ByBarterDealId(idAndTableName.Id);
                        if (existCS == null)
                        {
                            BarterDeal bd = _repository.GetBarterDealByID(idAndTableName.Id);
                            if (bd == null)
                            {
                                return null;
                            }
                            CS.BarterDeal = _mapper.Map<BarterDealVM>(bd);
                            CS.BarterDealID = idAndTableName.Id;
                            CS.Description = CS.BarterDeal.BusinessDescription;
                            //קשור של העסק המדווח לקייס סטדי
                            BusinessInCaseStudyVM SupplierBusiness = new BusinessInCaseStudyVM()
                            {
                                Id = 0,
                                BusinessId = (int)CS.BarterDeal.ReportsBusinessId,
                                Business = CS.BarterDeal.ReportsBusiness,
                                WordOfPartner = CS.BarterDeal.QuoteReportsBusiness,
                                LineOfBusiness = CS.BarterDeal.ReportDescriptionDeal
                            };
                            CS.BusinessesInCaseStudy.Add(SupplierBusiness);
                            //קשור של העסק השותף לקייס סטדי
                            BusinessInCaseStudyVM ConsumerBusiness = new BusinessInCaseStudyVM()
                            {
                                Id = 0,
                                BusinessId = (int)CS.BarterDeal.PartnerBusinessId,
                                Business = CS.BarterDeal.PartnerBusiness,
                                WordOfPartner = CS.BarterDeal.QuotePartnerBusiness,
                                LineOfBusiness = CS.BarterDeal.PartnerDescriptionDeal
                            };
                            CS.BusinessesInCaseStudy.Add(ConsumerBusiness);
                        }
                        else
                        {
                            CS = _mapper.Map<CaseStudyVM>(existCS);
                        }
                        break;
                    case FromTable.JointProject:
                        existCS = _repository.GetCS_ByJointProjectId(idAndTableName.Id);
                        if (existCS == null)
                        {
                            JointProject jp = _repository.GetJointProjectByID(idAndTableName.Id);
                            if (jp == null)
                            {
                                return null;
                            }
                            CS.JointProject = _mapper.Map<JointProjectVM>(jp);
                            CS.JointProjectID = idAndTableName.Id;
                            CS.Description = CS.JointProject.JointExplanation;
                            CS.BusinessTitle = CS.JointProject.HeaderCollaboration;

                            foreach (var business in CS.JointProject.BusinessesInCollaboration)
                            {
                                BusinessInCaseStudyVM ConsumerBusiness = new BusinessInCaseStudyVM()
                                {
                                    Id = 0,
                                    BusinessId = business.id,
                                    Business = business,
                                    WordOfPartner = business.PartInCollaboration
                                };
                                CS.BusinessesInCaseStudy.Add(ConsumerBusiness);
                            }

                        }
                        else
                        {
                            CS = _mapper.Map<CaseStudyVM>(existCS);
                        }

                        break;
                    default:
                        break;
                }
            }
            catch (Exception)
            {

                throw;
            }

            return CS;
        }


        public async Task<int> createCaseStudyWithPictures(CaseStudyVM model, IFormFile mainPictureFile, List<IFormFile> csPicturesFile)
        {
            int caseStudyId;
            try
            {

                //אם הועלתה תמונה שמים גוויד חדש ואם לא לוקחים את מה שהועלה בדווח על שת"פ
                model.PicGuid = mainPictureFile != null ? GenerateNewGuid() : Guid.Empty;
                GuidAndPath guidAndPath = null;
                if (mainPictureFile == null)
                {
                    guidAndPath = GetGuidFromCollaboration(model);
                    if (guidAndPath.path != null)
                        model.PicGuid = guidAndPath.PicGuid;
                }
                caseStudyId = _repository.createCaseStudy(_mapper.Map<CaseStudy>(model));
                #region business in case study
                foreach (var business in model.BusinessesInCaseStudy)
                {
                    BusinessInCaseStudy businessInCaseStudy = new BusinessInCaseStudy()
                    {
                        Id = 0,
                        BusinessId = business.BusinessId,
                        CaseStudyId = caseStudyId,
                        BuinessOwnerNameForCS = business.BuinessOwnerNameForCS,
                        LineOfBusiness = business.LineOfBusiness,
                        WordOfPartner = business.WordOfPartner
                    };
                    int businessId = _repository.createBusinessInCaseStudy(businessInCaseStudy);

                }
                #endregion
                string sourcePath = "";
                string targetPath = GetDiskPathCaseStudy() + caseStudyId;
                string fileName = "";
                int picID = 0;
                #region main picture
                string mainTargetPath = targetPath + "\\MainPicture";
                //שמירת התמונה הראשית של הקייס סטדי

                if (mainPictureFile != null)
                {

                    if (await SaveFileLocalDisc(null, null, mainTargetPath, mainPictureFile, (Guid)model.PicGuid))
                    {
                        Console.WriteLine("upload img = true");
                    }
                    else
                        Console.WriteLine("upload img = false");
                }
                else if (guidAndPath != null && guidAndPath.PicGuid != null)
                {
                    CopyFileLocalDisc(guidAndPath.path, mainTargetPath, guidAndPath.PicGuid + ".jpg");
                }
                #endregion
                #region cs pictures
                //שמירת כל התמונות לקייס סטדי
                if (csPicturesFile != null)
                {
                    foreach (var pic in csPicturesFile)
                    {
                        try
                        {
                            if (pic != null)
                            {
                                CaseStudyPicture picture = new CaseStudyPicture() { CaseStudyId = caseStudyId, Id = 0, PicGuid = GenerateNewGuid() };

                                //this is a shared location that all can reach .                    
                                string localTmpPath = GetDiskPathCaseStudy();
                                string localPath = localTmpPath + caseStudyId + "\\";
                                if (await SaveFileLocalDisc(null, null, localPath, pic, (Guid)picture.PicGuid))
                                {
                                    picture.Id = _repository.createCaseStudyPicture(picture);
                                    Console.WriteLine("upload img = true");
                                }
                                else
                                    Console.WriteLine("upload img = false");
                            }

                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("error while saving cover image : {0}", ex.Message);
                            throw ex;
                        }
                    }

                }
                #endregion
            }
            catch (Exception)
            {

                throw;
            }

            return caseStudyId;
        }

        public GuidAndPath GetGuidFromCollaboration(CaseStudyVM model)
        {
            GuidAndPath guidAndPath = new GuidAndPath();
            switch (model.FromTable)
            {
                case FromTable.PaidTransaction:

                    model.PaidTransaction = _mapper.Map<PaidTransactionVM>(_repository.GetPaidTransactionByID((int)model.PaidTransactionID));
                    if (model.PaidTransaction.PictureID != Guid.Empty)
                    {
                        guidAndPath.path = GetDiskPathPaidTransactions() + model.PaidTransactionID;
                        guidAndPath.PicGuid = model.PaidTransaction.PictureID;
                    }

                    break;
                case FromTable.BarterDeal:
                    model.BarterDeal = _mapper.Map<BarterDealVM>(_repository.GetBarterDealByID((int)model.BarterDealID));
                    if (model.BarterDeal.ReportsBusinessPictureID != Guid.Empty)
                    {
                        guidAndPath.path = GetDiskPathBarter() + model.BarterDealID + "\\" + model.BarterDeal.ReportsBusinessId;
                        guidAndPath.PicGuid = model.BarterDeal.ReportsBusinessPictureID;
                    }
                    else if (model.BarterDeal.PartnerBusinessPictureID != Guid.Empty)
                    {
                        guidAndPath.path = GetDiskPathBarter() + model.BarterDealID + "\\" + model.BarterDeal.PartnerBusinessId;
                        guidAndPath.PicGuid = model.BarterDeal.PartnerBusinessPictureID;
                    }

                    break;
                case FromTable.JointProject:
                    model.JointProject = _mapper.Map<JointProjectVM>(_repository.GetJointProjectByID((int)model.JointProjectID));
                    if (model.JointProject.PictureId != Guid.Empty)
                    {
                        guidAndPath.path = GetDiskPathJointProject() + model.JointProjectID;
                        guidAndPath.PicGuid = model.JointProject.PictureId;
                    }
                    break;
                default:
                    break;
            }
            return guidAndPath;
        }


        public async Task<bool> updateCaseStudy(CaseStudyVM model, IFormFile mainPictureFile, List<IFormFile> csPictureFiles)
        {
            try
            {
                string srcPath = GetDiskPathCaseStudy() + model.Id;
                string srcMainPath = srcPath + "\\MainPicture";
                if (mainPictureFile != null)
                {
                    Guid oldGuid = (Guid)model.PicGuid;
                    model.PicGuid = GenerateNewGuid();
                    if (await SaveFileLocalDisc(srcMainPath, oldGuid, srcMainPath, mainPictureFile, (Guid)model.PicGuid))
                    {
                        Console.WriteLine("upload img = true");
                    }
                    else
                    {
                        Console.WriteLine("upload img = false");
                    }
                }
                #region update pictures by db
                List<CaseStudyPicture> caseStudyPicturesList = _repository.caseStudyPicturesByCS(model.Id);
                if (caseStudyPicturesList.Count > model.CaseStudyPictures.Count)
                {
                    foreach (var item in caseStudyPicturesList)
                    {
                        CaseStudyPictureVM picExist = model.CaseStudyPictures.FirstOrDefault(x => x.Id == item.Id);
                        if (picExist == null)
                        {
                            _repository.deleteCaseStudyPicture(item.Id);
                            //delete file
                            string path = srcPath + "\\" + item.PicGuid + ".jpg";
                            DeleteFileLocalDisc(path);
                        }
                    }
                }

                #endregion
                if (csPictureFiles != null && csPictureFiles.Count > 0)
                {
                    foreach (var pic in csPictureFiles)
                    {
                        CaseStudyPictureVM caseStudyPicture = new CaseStudyPictureVM() { CaseStudyId = model.Id, PicGuid = GenerateNewGuid() };
                        if (await SaveFileLocalDisc(null, null, srcPath, pic, caseStudyPicture.PicGuid))
                        {
                            Console.WriteLine("upload img = true");

                            caseStudyPicture.Id = _repository.createCaseStudyPicture(_mapper.Map<CaseStudyPicture>(caseStudyPicture));
                            model.CaseStudyPictures.Add(caseStudyPicture);
                        }
                        else
                        {
                            Console.WriteLine("upload img = false");
                        }
                    }
                }

                #region update customer responses
                List<CaseStudyCustomerResponses> CustomerResponsesList = _repository.caseStudyCustomerResponsesByCS(model.Id);
                if (CustomerResponsesList.Count > model.CustomerResponses.Count)
                {
                    foreach (var item in CustomerResponsesList)
                    {
                        CaseStudyCustomerResponsesVM picExist = model.CustomerResponses.FirstOrDefault(x => x.Id == item.Id);
                        if (picExist == null)
                        {
                            _repository.deleteCaseStudyCustomerResponse(item.Id);
                        }
                    }
                }

                foreach (var item in model.CustomerResponses)
                {
                    if (item.Id < 1)
                    {
                        item.CaseStudyId = model.Id;
                        item.Id = _repository.createCaseStudyCustomerResponse(_mapper.Map<CaseStudyCustomerResponses>(item));
                    }
                }
                #endregion
                bool ans = _repository.updateCaseStudy(_mapper.Map<CaseStudy>(model));
                return ans;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<CaseStudyForCardsVM> GetCSByBuissinesID(int BusinessID)
        {
            try
            {
                List<CaseStudy> csList = _repository.GetCSByBuissinesID(BusinessID);
                List<CaseStudyForCardsVM> caseStudyListvm = csList.Select(_mapper.Map<CaseStudy, CaseStudyForCardsVM>).ToList();
                return caseStudyListvm;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public bool deleteCollaborationByIDAndTable(FromTable fromTable, int id)
        {
            return _repository.deleteCollaborationByIDAndTable(fromTable, id);
        }
        #region files
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
                    System.IO.DirectoryInfo di = new DirectoryInfo(Oldpath);
                    foreach (FileInfo Oldfile in di.GetFiles())
                    {
                        Oldfile.Delete();
                    }
                    //{
                    di.Delete(true);
                }
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                if (file.Length > 0)
                {
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

        public bool CopyFileLocalDisc(string sourcePath, string targetPath, string fileName)
        {
            try
            {
                string sourceFile = System.IO.Path.Combine(sourcePath, fileName);
                string destFile = System.IO.Path.Combine(targetPath, fileName);
                System.IO.Directory.CreateDirectory(targetPath);
                System.IO.File.Copy(sourceFile, destFile, true);
                return true;

            }
            catch (Exception ex)
            {
                Console.WriteLine("error while coping file to disk : {0}", ex.Message);
                return false;
            }
        }

        public bool DeleteFileLocalDisc(string path)
        {
            System.IO.FileInfo fi = new System.IO.FileInfo(path);
            try
            {
                fi.Delete();
                return true;
            }
            catch (System.IO.IOException e)
            {
                return false;
            }

        }

        public string GetDiskPathCaseStudy()
        {
            //to do : creae dir if dosent excist
            return _configuration["DiskPathCaseStudy"];
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
        #endregion

        #region get All collaboration
        public List<PaidTransactionVM> getAllPaidTransactions()
        {
            try
            {
                return _repository.getAllPaidTransactions().Select(x => _mapper.Map<PaidTransactionVM>(x)).ToList();
            }
            catch (Exception)
            {

                throw;
            }
        }
        public List<BarterDealVM> getAllBarterDeals()
        {
            try
            {
                return _repository.getAllBarterDeals().Select(x => _mapper.Map<BarterDealVM>(x)).ToList();
            }
            catch (Exception)
            {

                throw;
            }
        }
        public List<JointProjectVM> getAllJointProjects()
        {
            try
            {
                return _repository.getAllJointProjects().Select(x => _mapper.Map<JointProjectVM>(x)).ToList();
            }
            catch (Exception)
            {

                throw;
            }
        }
        #endregion
    }

    public class GuidAndPath
    {
        public Guid? PicGuid { get; set; }
        public string path { get; set; }
    }
}
