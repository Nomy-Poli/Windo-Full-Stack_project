using DataService;
using EmailService;
using FunctionalService;
using Ganss.Excel;
using Microsoft.AspNetCore.Identity;
using ModelService;
using ModelService.busoftModels;
using ModelService.windoModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UserService;
using AutoMapper;

namespace busoftExcelImporter
{
    public class ExcelImporter
    {
        private readonly ApplicationDbContext _context;
        private readonly IEmailSvc _emailSvc;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IFunctionalSvc _functionalSvc;
        private readonly IUserSvc _userSvc;
        public readonly IMapper _mapper;


        public ExcelImporter(ApplicationDbContext context, IEmailSvc emailSvc, IUserSvc userSvc, IMapper mapper,
            UserManager<ApplicationUser> userManager, IFunctionalSvc functionalSvc)
        {
            _context = context;
            _emailSvc = emailSvc;
            _userManager = userManager;
            _functionalSvc = functionalSvc;
            _userSvc = userSvc;
            _mapper = mapper;
        }


        //we need the context in order to load to bd . 


        //first we get the excel and check if we have records .
        //
        //we create a record list with all the rows -> update total record -> update start time 

        //we load all records from db 

        //compare the records in each row to db record ( email ) 

        //if same - check if we have delete flag 

        //if no delete flag -> and the same -> do nothing ( update not loaded ++ ) 

        public async Task<ImportResponse> Import(ImportRequest request)
        {
            ImportResponse imrvm = new ImportResponse();
            imrvm.ImportFileName = request.ImportFileName;
            imrvm.ImporterName = request.ImporterName;
            imrvm.AddedList = new List<BusinessFromExcel>();
            imrvm.DeletedList = new List<BusinessFromExcel>();
            imrvm.SendEmail = new List<UserEmailModel>();
            imrvm.StartTime = DateTime.Now;
            //imrvm.NotUpdated = new List<BusinessFromExcel>();

            //create a stream to load the file into the mapper

            MemoryStream stream = new MemoryStream(request.ImportFile);

            List<BusinessFromExcel> recordsToCreateInDB = new List<BusinessFromExcel>();
            //1.read the excel file and get the data in to list of pocos 

            var loaded = new ExcelMapper(stream).Fetch<BusinessFromExcel>();

            if (loaded != null && loaded.Count() > 0)
            {
                //mark total records.
                imrvm.TotalRecords = loaded.Count();

                //get only the ones mark for delete.
                var mailsAndDelete = loaded.Where(m => m.Deletes > 0).Select(s => s.Email.ToLower()).ToList();

                imrvm.MarkForDelete = mailsAndDelete.Count();

                //get the deleted records from the database
                var exsitings = _context.Buisness.Where(x => mailsAndDelete.Contains(x.userId.ToLower())).ToList();
                int deleteRes = 0;
                if (exsitings != null && exsitings.Count() > 0)
                {
                    exsitings.ForEach(DeletedUserForUpdate =>
                    {
                        var s = _context.Status.Select(s => s).Where(s => s.Id == 1).FirstOrDefault();
                        //var buisness = _context.Buisness.Select(b => b).Where(b => b.Id == DeletedUserForUpdate.Id).FirstOrDefault();
                        var bs = _context.BuisnessStatus.Select(bs => bs).Where(bs => bs.buisnessId == DeletedUserForUpdate.Id).FirstOrDefault();
                        if (bs.statusId != s.Id)//if its not deleted
                        {
                            bs.statusId = s.Id;
                            _context.SaveChanges();
                            imrvm.DeletedLength++;
                        }
                        else//if this business is was deleted
                        {
                            deleteRes++;
                        }
                        //buisness.UpdatedBusinessStatus = bs.Id;
                        //DeletedUserForUpdate.UpdatedBusinessStatus = 0;
                        //_context.Buisness.Update(DeletedUserForUpdate);
                        //_context.SaveChanges();                        
                    });

                    //mark deleted for response
                    imrvm.DeletedRecords = deleteRes;// || exsitings.Count();
                    //_context.SaveChanges();
                }
                //next we take only the new ones ( not the exsisting ones ) . 
                var NewMails = loaded.Where(m => m.Deletes == 0).Select(s => s.Email.ToLower()).ToList();
                imrvm.MarkForNew = NewMails.Count();


                //we compare with databasse for duplications 
                var mailsExsitings = _context.Buisness.Where(x => NewMails.Contains(x.userId.ToLower())).ToList();

                List<string> existingEmails = new List<string>();

                if (mailsExsitings != null && mailsExsitings.Count() > 0)
                {
                    //imrvm.NoChangeRecords = mailsExsitings.Count();

                    imrvm.NotUpdated = mailsExsitings.Count();

                    //delete them from the new record list 
                    existingEmails = mailsExsitings.Select(m => m.userId).ToList();

                }
                //aleays happens even if existingEmails was never fill and its 0
                recordsToCreateInDB = loaded.Where(l => l.Deletes == 0 && !existingEmails.Contains(l.Email.ToLower())).ToList();
                //this records are new - we'll create them in the users , roles , business , business status tables.
                imrvm.NewRecordsNotInDataBase = recordsToCreateInDB.Count;


                if (recordsToCreateInDB != null && recordsToCreateInDB.Count() > 0)
                {
                    //List<Buisness> buisenessesToLoad = new List<Buisness>();

                    //string mailSubject = string.Empty;
                    //string mailMessage = string.Empty;
                    //string mailTemplate = string.Empty;

                    //craete the buisenesses 
                    recordsToCreateInDB.ForEach(async r =>
                    {

                        //int newBusinessId = 0;
                        //create the users and set there roles first 
                        var NewRandomPassword = RandomPassword();
                        var appUser = new RegisterViewModel
                        {
                            Email = r.Email,
                            //UserName = r.Email,
                            //UserRole = "Customer",
                            Phone = r.Phone,
                            //PhoneNumberConfirmed = true,
                            Firstname = r.UserName,
                            Gender = "female",
                            //Lastname = "Levi",
                            Terms = true,
                            Dob = DateTime.Now.ToString(),
                            Password = NewRandomPassword,
                            //EmailConfirmed = true,
                            //ProfilePic = await _functionalSvcNotInterface.GetDefaultProfilePic(),
                            //IsActive = true,


                        };
                        //create the user
                        var ResultappUser = _userSvc.CreateSeedUserAsync(appUser).Result;
                        //create list to the users that we will send there email
                        if (ResultappUser != null)
                        {

                            var code = await _userManager.GenerateEmailConfirmationTokenAsync(ResultappUser);
                            imrvm.SendEmail.Add(
                                new UserEmailModel
                                {
                                    Email = r.Email,
                                    password = NewRandomPassword,
                                    code = code,
                                    userId = ResultappUser.Id
                                });
                            createBusiness(r.Email, r.Address, r.BusinessName);
                        }
                        //create the businnes and the new id.

                        //IdentityResult result = await _userManager.CreateAsync(appUser, password);
                        //var userName = await _functionalSvc.CreateNewUser();
                        //var userName = await _functionalSvc.CreateNewUser(r.Email, r.UserName, "P@ssword1!", r.Phone, r.BusinessName, r.Address);


                        //create the status 
                        //BuisnessStatus bs = new BuisnessStatus()
                        //{
                        //    buisnessId = newBusinessId,
                        //    startDate = DateTime.Now,
                        //    statusId = 1
                        //};
                        //_context.BuisnessStatus.Add(bs);
                        //_context.SaveChanges();


                        //send the email 

                    });
                }

            }
            imrvm.EndTime = DateTime.Now;
            imrvm.AddedLength = imrvm.AddedList.Length();
            imrvm.DeletedLength = imrvm.DeletedLength;
            //מיפוי למודל ואז הכנסה לDB

            ImportResponseModel imr = _mapper.Map<ImportResponse, ImportResponseModel>(imrvm);
            imr.Added = imrvm.AddedList.Length();
            imr.Deleted = imrvm.DeletedLength;
            //imr.ColumnsFound = imrvm.ColumnsFoundList.Length();//לא היה שימוש בזה
            imr.ImportId = await _userSvc.InsertActivities(imr);
            return imrvm;

        }


        #region old
        //public ImportResponse Import(ImportRequest<T> request) 
        //{
        //    ImportResult imr = new ImportResult();

        //    List<string> columnNamesFromExcel = new List<string>();

        //    Dictionary<string, string> columnsData = new Dictionary<string, string>();

        //    //IFormFile file = Request.Form.Files[0];
        //    string folderName = request.ImportFileName;// "UploadExcel";
        //    string directory = request.ImportDirectory;// "wwwroot";// hostingEnvironment.WebRootPath;
        //    string newPath = Path.Combine(directory, folderName);

        //    //StringBuilder sb = new StringBuilder();
        //    //create the directory if it dosent exist
        //    if (!Directory.Exists(newPath))
        //    {
        //        Directory.CreateDirectory(newPath);
        //    }


        //    if (request.ImportFile?.Length > 0)
        //    {
        //        string sFileExtension = Path.GetExtension(request.ImportFileName).ToLower();
        //        string fullPath = Path.Combine(newPath, request.ImportFileName);

        //        ISheet sheet;

        //        //write the file to disk
        //        File.WriteAllBytes(fullPath, request.ImportFile);

        //        if (sFileExtension == ".xls")
        //        {
        //            HSSFWorkbook hssfwb = new HSSFWorkbook(new MemoryStream(request.ImportFile)); //This will read the Excel 97-2000 formats  
        //            sheet = hssfwb.GetSheetAt(request.SheetNumber); //get first sheet from workbook  
        //        }
        //        else
        //        {
        //            XSSFWorkbook hssfwb = new XSSFWorkbook(new MemoryStream(request.ImportFile)); //This will read 2007 Excel format  
        //            sheet = hssfwb.GetSheetAt(request.SheetNumber); //get first sheet from workbook   
        //        }



        //        IRow headerRow = sheet.GetRow(0); //Get Header Row
        //        int cellCount = headerRow.LastCellNum;//get last col number .


        //        //sb.Append("<table class='table table-bordered'><tr>");

        //        for (int j = 0; j < cellCount; j++)
        //        {
        //            NPOI.SS.UserModel.ICell cell = headerRow.GetCell(j);

        //            if (cell == null || string.IsNullOrWhiteSpace(cell.ToString())) continue;
        //            columnNamesFromExcel.Add(cell.ToString());

        //            //sb.Append("<th>" + cell.ToString() + "</th>");

        //        }

        //        //return columns found to the user.
        //        imrvm.ColumnsFound = columnNamesFromExcel;

        //        //sb.Append("</tr>");
        //        //sb.AppendLine("<tr>");
        //        for (int i = (sheet.FirstRowNum + 1); i <= sheet.LastRowNum; i++) //Read Excel File
        //        {
        //            IRow row = sheet.GetRow(i);

        //            Type typeT = typeof(T);


        //            if (row == null) continue;

        //            if (row.Cells.All(d => d.CellType == CellType.Blank)) continue;

        //            //run thru all columns and get the data.
        //            for (int j = row.FirstCellNum; j < cellCount; j++)
        //            {
        //                if (row.GetCell(j) != null)
        //                //sb.Append("<td>" + row.GetCell(j).ToString() + "</td>");
        //                {
        //                   // row.GetCell(j).ToString()
        //                }
        //            }

        //            //sb.AppendLine("</tr>");
        //        }
        //        //sb.Append("</table>");
        //    }
        //    return imrvm;
        //}

        #endregion

        //https://github.com/mganss/ExcelMapper


        //check if business exist 
        bool CompareTwoBusinesses()
        {
            return false;
        }

        #region  _password
        //the tatal password that we return to the user
        public string RandomPassword()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(RandomString(3, true));
            builder.Append(RandomAlphanumeric());
            builder.Append(RandomNumber(1000, 9999));
            builder.Append(RandomString(2, false));
            return builder.ToString();
        }
        //random number for the password
        public string RandomNumber(int size, int lowerCase)
        {
            //The following code in Steps 1 returns a random number.

            // Generate a random number  
            Random random = new Random();
            // Any random integer   
            int num = random.Next(0, 10);
            string numbersStrimg = num.ToString();
            return numbersStrimg;
        }
        //random uppercase and lowercase to the password
        public string RandomString(int size, bool lowerCase)
        {
            StringBuilder builder = new StringBuilder();
            Random random = new Random();
            char ch;
            for (int i = 0; i < size; i++)
            {
                ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
                builder.Append(ch);
            }
            if (lowerCase)
                return builder.ToString().ToLower();
            return builder.ToString();
        }
        //random Alphanumeric to the password
        public string RandomAlphanumeric()
        {
            StringBuilder builder = new StringBuilder();
            var punctuation = "#?!@$%^&*-]";
            var stringChars = new char[8];
            var random = new Random();

            for (int i = 0; i < 1; i++)
            {
                stringChars[i] = punctuation[random.Next(punctuation.Length)];
                builder.Append(stringChars[i]);
            }
            return builder.ToString();
        }

        //public string password_generator()
        //{
        //    // Create a string of characters, numbers, special characters that allowed in the password  
        //    var length = 6;
        //    string validChars = "ABCDEFGHJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789!@#$%^&*?_-";
        //    Random random = new Random();

        //    // Select one random character at a time from the string  
        //    // and create an array of chars  
        //    char[] chars = new char[length];
        //    for (int i = 0; i < length; i++)
        //    {
        //        chars[i] = validChars[random.Next(0, validChars.Length)];
        //    }
        //    return new string(chars);
        //}
        #endregion

        public List<ImportResponse> GetAllUsers()
        {
            try
            {
                List<ImportResponseModel> ResponseList = new List<ImportResponseModel>();


                ResponseList = _context.UploadActivitiesHistory.ToList();
                List<ImportResponse> ResponseListVM = ResponseList.Select(_mapper.Map<ImportResponseModel, ImportResponse>).ToList();
                ResponseListVM.ForEach(r =>
                {
                    r.DeletedLength = ResponseList.Where(s => s.ImportId == r.ImportId).Select(k => k.Deleted).FirstOrDefault();
                    r.AddedLength = ResponseList.Where(s => s.ImportId == r.ImportId).Select(k => k.Added).FirstOrDefault();
                }
                );
                //צריך להמיר גם את האורכים של התוספו ונמחקו
                return ResponseListVM;
            }
            catch (Exception)
            {

                throw;
            }
        }
        #region create business
        public void createBusiness(string email, string address, string businessName)
        {
            //create the business model
            Buisness business = new Buisness()
            {
                userId = email,
                address = address,
                buisnessName = businessName,
                UpdatedBusinessStatus = 1,
                actionDiscription = businessName
                //userId = r.Email
            };
            //create the business in the db
            if (business != null)
            {
                _context.Buisness.Add(business);
                _context.SaveChanges();
            }
            //get the new business id .
            int id = business.Id;
            //if that success = we will update the other tables
            if (id > 0)
            {
                int i = 0;
                int isSuccess = -1;

                #region pictures
                //foreach (var item in modelVm.pictursList)
                //{
                //    item.buisnessId = id;
                //    item.numberOfPicture = i;
                //    isSuccess = _repository.CreatePictursForBuisness(item);
                //    if (isSuccess < 0)
                //        return -2;
                //    i++;
                //}
                //create the cover picture
                //BuisnessCoverPicture coverPicture = new BuisnessCoverPicture();
                //coverPicture.buisnessId = id;
                //coverPicture.url = modelVm.coverPicture;
                //isSuccess = _repository.CreateCoverPictureForBuisness(coverPicture);

                //create the logo picture
                //BuisnessLogo logoPicture = new BuisnessLogo();
                //logoPicture.buisnessId = id;
                //logoPicture.url = modelVm.logoPicture;
                //isSuccess = _repository.CreateLogoPictureForBuisness(logoPicture);

                ////create the BuisnessCategorySubCategory
                //BuisnessSubCategory tempBuisnessSubCategory = new BuisnessSubCategory();

                //check each list in the array :  
                #endregion

                //create defalt sub category and insert this to the businnes
                //#region sub category
                //CategorySubCategory OneCategory = _context.CategorySubCategory.Select(c => c).FirstOrDefault();
                //if (OneCategory != null)
                //{
                //    try
                //    {
                //        var AddCategoryToBusiness = new BuisnessSubCategory()
                //        {
                //            buisnessId = id,
                //            categorySubCategoryId = OneCategory.Id,
                //            isPossibleInBarter = true

                //        };
                //        _context.BuisnessSubCategory.Add(AddCategoryToBusiness);
                //        _context.SaveChanges();
                //    }
                //    catch (Exception ex)
                //    {
                //        throw ex;
                //    }
                //}
                //#endregion

                #region status 
                if (id != null)
                {
                    BuisnessStatus bs = new BuisnessStatus()
                    {
                        buisnessId = id,
                        statusId = 2,
                        startDate = DateTime.Now,
                        endDate = DateTime.Now.AddYears(3)
                    };
                    _context.BuisnessStatus.Add(bs);
                    _context.SaveChanges();
                }
                #endregion

                #region areas
                var area = _context.Area.Select(area => area).FirstOrDefault();
                if (area != null)
                {
                    BuisnessArea businessA = new BuisnessArea()
                    {
                        buisnessId = id,
                        areaId = area.Id,
                    };
                    _context.BuisnessArea.Add(businessA);
                    _context.SaveChanges();
                }
                #endregion
            }
        }
        #endregion
    }
    public static class utils
    {
        public static V ConvertParentObjToChildObj<T, V>(T obj) where V : new()
        {
            Type typeT = typeof(T);
            PropertyInfo[] propertiesT = typeT.GetProperties();
            V newV = new V();
            foreach (var propT in propertiesT)
            {
                var nomePropT = propT.Name;
                var valuePropT = propT.GetValue(obj, null);

                Type typeV = typeof(V);
                PropertyInfo[] propertiesV = typeV.GetProperties();
                foreach (var propV in propertiesV)
                {
                    var nomePropV = propV.Name;
                    if (nomePropT == nomePropV)
                    {
                        propV.SetValue(newV, valuePropT);
                        break;
                    }
                }
            }
            return newV;
        }
    }
}
