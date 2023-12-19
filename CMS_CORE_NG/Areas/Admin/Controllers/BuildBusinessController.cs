using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CookieService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.DependencyInjection;
using ModelService;
using UserService;
using WritableOptionsService;
using Microsoft.AspNetCore.Http;
using busoftExcelImporter;
using ModelService.busoftModels;
using System.IO;
using EmailService;
using Microsoft.Extensions.Configuration;

namespace CMS_CORE_NG.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(AuthenticationSchemes = "Admin")]
    [AutoValidateAntiforgeryToken]
    public class BuildBusinessController : Controller
    {
        private readonly IUserSvc _userSvc;
        private readonly IEmailSvc _emailSvc;
        private readonly ICookieSvc _cookieSvc;
        private readonly IServiceProvider _provider;
        private readonly DataProtectionKeys _dataProtectionKeys;
        private readonly AppSettings _appSettings;
        private AdminBaseViewModel _adminBaseViewModel;
        private readonly IWritableSvc<SiteWideSettings> _writableSiteWideSettings;
        public readonly ExcelImporter _excelImporter;
        public IConfiguration Configuration { get; }

        public BuildBusinessController(
            IUserSvc userSvc, ICookieSvc cookieSvc, IServiceProvider provider, ExcelImporter excelImporter, IEmailSvc emailSvc, IConfiguration configuration,
            IOptions<DataProtectionKeys> dataProtectionKeys, IOptions<AppSettings> appSettings, IWritableSvc<SiteWideSettings> writableSiteWideSettings)
        {
            _userSvc = userSvc;
            _cookieSvc = cookieSvc;
            _provider = provider;
            _emailSvc = emailSvc;
            _dataProtectionKeys = dataProtectionKeys.Value;
            _appSettings = appSettings.Value;
            _writableSiteWideSettings = writableSiteWideSettings;
            _excelImporter = excelImporter;
            Configuration = configuration;
        }
        public async Task<IActionResult> Index()
        {
            await SetAdminBaseViewModel();
            return View("Index", _adminBaseViewModel);
        }
        //[HttpPost("[action]")]
        [HttpPost("CreatBusinesses")]
        public async Task<IActionResult> CreatBusinesses([FromForm] FileModel fileModel)
        {
            try
            {
                IFormFile formFile = fileModel.file;
                //קריאת הקובץ לבייטים
                byte[] CoverImageBytes = null;
                BinaryReader reader = new BinaryReader(formFile.OpenReadStream());
                CoverImageBytes = reader.ReadBytes((int)formFile.Length);

                ImportResponse importResponse = new ImportResponse();
                ImportRequest importRequest = new ImportRequest
                {
                    ImportFileNameExtension = formFile.ContentType,//סוג הקובץ
                    ImporterName = fileModel.FileName,
                    ImportFileName = formFile.FileName,
                    ImportFile = CoverImageBytes,//קבלת הקובץ למערך של בייטים
                };
                importResponse = await _excelImporter.Import(importRequest);
                //return RedirectToPage("Area/Admin/Views/ImportBusiness/ImportResponseLayout", importResponse);
                //send the emails to the users
                if (importResponse.SendEmail != null)
                {
                    importResponse.SendEmail.ForEach(user =>
                    {
                        //using (StreamWriter sw = new StreamWriter("C:\\Windo\\publish\\wwwroot\\text.txt", true))
                        //{
                        //    sw.WriteLine(user.Email);
                        //}

                        var callbackUrl = Url.ActionLink("", "Account", new { UserId = user.userId, Code = user.code }, protocol: HttpContext.Request.Scheme);
                        var mailMessage = "שם משתמש: " + user.Email+ " וסיסמה: " + user.password;
                        _emailSvc.SendEmailAsync(user.Email.Trim(),
                            mailMessage,
                            callbackUrl,
                            "NewImportUser.html",null);
                    });
                }
                importResponse.SendEmail = null;
                await SetAdminBaseViewModel();
                _adminBaseViewModel.IResponse = new List<ImportResponse>();
                _adminBaseViewModel.IResponse.Add(importResponse);
                return PartialView("_ImportResponseLayout", _adminBaseViewModel);
            }
            catch (Exception ec)
            {
                throw;
            }
        }

        [HttpGet("GetAllActivities")]
        public async Task<IActionResult> GetAllActivities()
        {
            try
            {
                await SetAdminBaseViewModel();
                var Rlist = _excelImporter.GetAllUsers();
                _adminBaseViewModel.IResponse = Rlist;
                _adminBaseViewModel.IResponse.OrderByDescending(x => x.StartTime).ToList();
                return PartialView("_ImportResponseLayout", _adminBaseViewModel);
            }
            catch (Exception)
            {

                throw;
            }
        }
        private async Task SetAdminBaseViewModel()
        {
            var protectorProvider = _provider.GetService<IDataProtectionProvider>();
            var protector = protectorProvider.CreateProtector(_dataProtectionKeys.ApplicationUserKey);
            var userProfile = await _userSvc.GetUserProfileByIdAsync(protector.Unprotect(_cookieSvc.Get("user_id")));
            var resetPassword = new ResetPasswordViewModel();

            _adminBaseViewModel = new AdminBaseViewModel
            {
                Profile = userProfile,
                AddUser = null,
                AppSetting = null,
                Dashboard = null,
                ResetPassword = resetPassword,
                SiteWideSetting = _writableSiteWideSettings.Value
            };
        }
    }
}
