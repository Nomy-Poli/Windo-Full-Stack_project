using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AuthService;
using CookieService;
using DataService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.DependencyInjection;
using ModelService;
using Serilog;
using Microsoft.AspNetCore.Http;
using UserService;
using CMS_CORE_NG.Extensions;
using EmailService;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CMS_CORE_NG.Areas.Admin.Controllers
{
    [Area("Admin")]
    [AutoValidateAntiforgeryToken]
    public class AccountController : Controller
    {
        private readonly AppSettings _appSettings;
        private DataProtectionKeys _dataProtectionKeys;
        private readonly IServiceProvider _provider;
        private readonly ApplicationDbContext _db;
        private readonly IAuthSvc _authSvc;
        private readonly ICookieSvc _cookieSvc;
        private readonly IUserSvc _userSvc;
        private readonly IEmailSvc _emailSvc;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private const string AccessToken = "access_token";
        private const string User_Id = "user_id";

        string[] cookiesToDelete = { "twoFactorToken", "memberId", "rememberDevice", "user_id", "access_token" };

        public AccountController(IOptions<AppSettings> appSettings,
            IServiceProvider provider,
            ApplicationDbContext db, IEmailSvc emailSvc,
            IAuthSvc authSvc, IUserSvc userSvc, IHttpContextAccessor httpContextAccessor,
            ICookieSvc cookieSvc, IOptions<DataProtectionKeys> dataProtectionKeys)
        {
            _appSettings = appSettings.Value;
            _provider = provider;
            _db = db;
            _authSvc = authSvc;
            _cookieSvc = cookieSvc;
            _dataProtectionKeys = dataProtectionKeys.Value;
            _userSvc = userSvc;
            _emailSvc = emailSvc;
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Login(string returnUrl = null)
        {
            await Task.Delay(0);
            ViewData["ReturnUrl"] = returnUrl;
            try
            {
                // Check if user is already logged in 
                if (!Request.Cookies.ContainsKey(AccessToken) || !Request.Cookies.ContainsKey(User_Id))
                {
                    return View();
                }
            }
            catch (Exception ex)
            {
                Log.Error("Error: An error occurred while seeding the database {Error} {StackTrace} {InnerException} {Source}",
                    ex.Message, ex.StackTrace, ex.InnerException, ex.Source);
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginViewModel model, string returnUrl = null)
        {
            // First get the return url - the url which the user was trying to access initially
            ViewData["ReturnUrl"] = returnUrl;

            if (ModelState.IsValid)
            {
                try
                {
                    var jwtToken = await _authSvc.Auth(model);
                    const int expireTime = 60; // set the value to 60 - as dont want the admin cookie to stay in browser for longer

                    _cookieSvc.SetCookie("access_token", jwtToken.Token, expireTime);
                    _cookieSvc.SetCookie("user_id", jwtToken.UserId, expireTime);
                    _cookieSvc.SetCookie("username", jwtToken.Username, expireTime);
                    Log.Information($"User {model.Email} logged in.");

                    return Ok("עבר בהצלחה");

                }
                catch (Exception ex)
                {
                    Log.Error("אירעה שגיאה במהלך הכנסת הנתונים למסד הנתונים {Error} {StackTrace} {InnerException} {Source}",
                       ex.Message, ex.StackTrace, ex.InnerException, ex.Source);
                }

            }

            ModelState.AddModelError("", "הוזן שם משתמש או ססמא לא חוקיים");

            Log.Error("Invalid username or password entered");

            return Unauthorized("אנא בדוק את אישורי סיסמה : הוזן שם משתמש או סיסמה לא חוקיים");

        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            try
            {
                var userId = _cookieSvc.Get("user_id");

                if (userId != null)
                {
                    var protectorProvider = _provider.GetService<IDataProtectionProvider>();
                    var protector = protectorProvider.CreateProtector(_dataProtectionKeys.ApplicationUserKey);
                    var unprotectedToken = protector.Unprotect(userId);

                    var rt = _db.Tokens.FirstOrDefault(t => t.UserId == unprotectedToken);

                    // First remove the Token
                    if (rt != null) _db.Tokens.Remove(rt);
                    await _db.SaveChangesAsync();

                    // Second remove all Cookies              
                    _cookieSvc.DeleteAllCookies(cookiesToDelete);
                }

            }
            catch (Exception ex)
            {
                _cookieSvc.DeleteAllCookies(cookiesToDelete);
                Log.Error("אירעה שגיאה במהלך הכנסת הנתונים למסד הנתונים  {Error} {StackTrace} {InnerException} {Source}",
                    ex.Message, ex.StackTrace, ex.InnerException, ex.Source);
            }

            Log.Information("User logged out.");
            return RedirectToLocal(null);
        }

        //פונק שכח סיסמה - לפני  ששולח מייל
        [HttpGet("ForgotFunc")]
        public async Task<IActionResult> ForgotFunc()
        {
            return View();
        }



        public IActionResult AccessDenied()
        {
            return View();
        }

        private IActionResult RedirectToLocal(string returnUrl)
        {
            // Preventing open redirect attack
            return Url.IsLocalUrl(returnUrl)
                ? (IActionResult)Redirect(returnUrl)
                : RedirectToAction(nameof(HomeController.Index), "Home");
        }



        //פונק' של איפוס סיסמה
        [HttpPost("ForgotPassword")]
        public async Task<IActionResult> ForgotPassword([FromForm] RegisterViewModel model, string returnUrl = null)
        {
            if (ModelState.IsValid)
            {
                var result = await _userSvc.ForgotPassword(model.Email);

                if (!result.IsValid)
                {
                    // Don't reveal that the user does not exist or is not confirmed
                    //return Ok(new { Message = "Failed" });

                    ViewBag.MessageStatus = "בקשתך לא הצליחה, בדוק שהמייל שהכנסת תקין.";
                    return View("ForgotFunc");
                }

                var callbackUrl = _httpContextAccessor.AbsoluteUrl("/api/v1/Account/ResetPassword", new { userId = (string)result.Data["User"].Id, code = (string)result.Data["Code"] });
                await _emailSvc.SendEmailAsync(
                    model.Email,
                    "אתחול סיסמה לאתר windo",
                    callbackUrl,
                    "ForgotPasswordConfirmation.html",null);
                ViewBag.MessageStatus = "הבקשה עברה בהצלחה, אנא בדוק את המייל ואפס סיסמה.";
                //return Ok(new { Message = "Success" });
            }
            else
            {
                ViewBag.MessageStatus = "בקשתך לא הצליחה, אנא בדוק שכתובת המייל שהכנסת תקין";
            }

            // If we got this far, something failed, redisplay form

            //return BadRequest("נתקלנו בשגיאה");
            return View("ForgotFunc");
        }

    }
}
