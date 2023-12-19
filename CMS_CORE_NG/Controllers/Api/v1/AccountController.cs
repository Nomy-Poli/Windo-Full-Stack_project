using System;
using System.Net;
using System.Threading.Tasks;
using AuthService;
using CMS_CORE_NG.Extensions;
using EmailService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ModelService;
using Serilog;
using UserService;
using static CMS_CORE_NG.Scoring;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CMS_CORE_NG.Controllers.Api.v1
{
    [ApiVersion("1.0")]
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [AutoValidateAntiforgeryToken]
    public class AccountController : ControllerBase
    {
        private readonly IUserSvc _userSvc;
        private readonly IEmailSvc _emailSvc;
        private readonly IAuthSvc _authSvc;
        private readonly IHttpContextAccessor _httpContextAccessor;
        string[] _cookiesToDelete = { "loginStatus", "access_token", "userRole", "username", "refreshToken" };
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IScoring _scoring;

        public AccountController(IUserSvc userSvc, IEmailSvc emailSvc, IAuthSvc authSvc, IHttpContextAccessor httpContextAccessor
            , UserManager<ApplicationUser> userManager, IScoring scoring)
        {
            _userManager = userManager;
            _scoring = scoring;
            _userSvc = userSvc;
            _emailSvc = emailSvc;
            _authSvc = authSvc;
            _httpContextAccessor = httpContextAccessor;
        }
        [TempData]
        public string StatusMessage { get; set; }
        [HttpGet("ConfirmEmail")]
        public async Task<string> ConfirmEmail(string UserId, string Code)
        {
            try
            {
                string isSuccess = await _userSvc.OnGetAsync(UserId, Code);
                return isSuccess;
            }
            catch (Exception ex)
            {
                //_logger.LogError(ex, "error in ResetPassword .");
                return ex.Message;
            }

        }
        [HttpPost("[action]")]
        public async Task<IActionResult> Register([FromBody] RegisterViewModel model)
        {
            var result = await _userSvc.RegisterUserAsync(model);

            if (result.Message.Equals("Success") && result.IsValid)
            {
                // Sending Confirmation Email
                var callbackUrl = Url.ActionLink("", "Account", new { UserId = result.Data["User"].Id, Code = result.Data["Code"] }, protocol: HttpContext.Request.Scheme);

                await _emailSvc.SendEmailAsync(
                    result.Data["User"].Email,
                    " אימות משתמש באתר windo",
                    callbackUrl,
                    "EmailConfirmation.html",null);
                Log.Information($"New User Created => {result.Data["User"].UserName}");
                return Ok(new { username = result.Data["User"].UserName, email = result.Data["User"].Email, status = 1, message = "ההרשמה הצליחה" });
            }
            return BadRequest(new JsonResult(result.Data));
        }
        [HttpPost("[action]")]
        //פונק' הכניסה של המשתמש לוגאין
        public async Task<IActionResult> Auth([FromBody] TokenRequestModel model)
        {
            if (!ModelState.IsValid) return BadRequest();
            try
            {
                var jwtToken = await _authSvc.Auth(model);
                if (jwtToken.ResponseInfo.StatusCode == HttpStatusCode.Unauthorized)
                {
                    _authSvc.DeleteAllCookies(_cookiesToDelete);
                    return Unauthorized(new { LoginError = jwtToken.ResponseInfo.Message });
                }
                if (jwtToken.ResponseInfo.StatusCode == HttpStatusCode.InternalServerError)
                {
                    _authSvc.DeleteAllCookies(_cookiesToDelete);
                    return StatusCode(StatusCodes.Status500InternalServerError);
                }
                if (jwtToken.ResponseInfo.StatusCode == HttpStatusCode.BadRequest)
                {
                    _authSvc.DeleteAllCookies(_cookiesToDelete);
                    return BadRequest(new { LoginError = jwtToken.ResponseInfo.Message });
                }
                if (!jwtToken.TwoFactorLoginOn)
                {
                    //--------DO ADD send to score--------------------------------------------------------------------------
                    await _scoring.ScoreBusinessByUserEmail(jwtToken.Username);
                    return Ok(jwtToken);
                }
                // Update the Response Message
                jwtToken.ResponseInfo.Message = "נדרש קוד אימות";
                var twoFactorCodeModel = await _userSvc.GenerateTwoFactorCodeAsync(true, jwtToken.UserId);
                if (twoFactorCodeModel == null)
                {
                    _authSvc.DeleteAllCookies(_cookiesToDelete);
                    return BadRequest("שגיאה");
                }
                if (twoFactorCodeModel.AuthCodeRequired)
                {
                    _authSvc.DeleteAllCookies(_cookiesToDelete);
                    return Unauthorized(new
                    {
                        LoginError = jwtToken.ResponseInfo.Message,
                        Expiry = twoFactorCodeModel.ExpiryDate,
                        twoFactorToken = twoFactorCodeModel.Token,
                        UserId = twoFactorCodeModel.UserId
                    });
                }
            }
            catch (Exception ex)
            {
                Log.Error("An error occurred while seeding the database  {Error} {StackTrace} {InnerException} {Source}",
                    ex.Message, ex.StackTrace, ex.InnerException, ex.Source);
            }
            return Unauthorized();
        }
        [HttpGet("[action]")]
        public async Task<IActionResult> Logout()
        {
            var result = await _authSvc.LogoutUserAsync();
            if (result)
            {
                return Ok(new { Message = "הכניסה עברה בהצלחה" });
            }

            return BadRequest(new { Message = "בקשה לא תקינה" });
        }

        [HttpPost("[action]/{email}")]
        public async Task<IActionResult> ForgotPassword([FromRoute] string email)
        {
            if (ModelState.IsValid)
            {
                var result = await _userSvc.ForgotPassword(email);

                if (!result.IsValid)
                {
                    // Don't reveal that the user does not exist or is not confirmed
                    return Ok(new { Message = "Failed" });
                }

                var callbackUrl = _httpContextAccessor.AbsoluteUrl("/api/v1/Account/ResetPassword", new { userId = (string)result.Data["User"].Id, code = (string)result.Data["Code"] });
                await _emailSvc.SendEmailAsync(
                    email,
                    "אתחול סיסמה לאתר WINDO",
                    callbackUrl,
                    "ForgotPasswordConfirmation.html",null);
                return Ok(new { Message = "Success" });
            }

            // If we got this far, something failed, redisplay form
            return BadRequest("נתקלנו בשגיאה");
        }

        [HttpGet("[action]")]
        [AllowAnonymous]
        public IActionResult ResetPassword(string code = null)
        {
            if (code == null)
            {
                throw new ApplicationException("יש לספק קוד לאיפוס סיסמה.");
            }
            return RedirectToAction("ResetPassword", "Password", new ResetPasswordViewModel { Code = code });
        }
        [HttpPost("[action]")]
        public async Task<IActionResult> SendTwoFactorCode([FromBody] TwoFactorRequestModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            // First we need to check if this request is valid - We cannot depend on client side validation alone
            // Check the validity of TwoFactorToken & Session Expiry
            try
            {
                var result = await _userSvc.SendTwoFactorAsync(model);

                if (result.IsValid)
                {

                    // Send code to the user via to their preferred provider.
                    if (model.ProviderType.Equals("Email"))
                    {
                        var message = $"<h2>קוד האימות הדו-גורמי שלך: {result.Code}</h2>";
                        await _emailSvc.SendEmailAsync(
                            result.Email,
                            "Two-Factor Code",
                            message,
                            "TwoFactorAuthentication.html",null);


                        return Ok(new { Message = "TwoFactorCode-Send" });
                    }
                    if (model.ProviderType.Equals("SMS"))
                    {
                        //TODO : Phase 2
                        return BadRequest("השירות של שליחת הודעת SMS לא נתמך");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
            return Unauthorized(new { LoginError = "Two-Factor Fail" });
        }

        [HttpPost("[action]/{userId}")]
        public async Task<IActionResult> SessionExpiryNotification([FromRoute] string userId)
        {
            if (!string.IsNullOrEmpty(userId))
            {
                var result = await _userSvc.ExpireUserSessionAsync(userId);

                if (result.IsValid)
                {
                    _authSvc.DeleteAllCookies(_cookiesToDelete);
                    return Ok(new { Message = "עבר בהצלחה" });
                }
            }
            _authSvc.DeleteAllCookies(_cookiesToDelete);
            return BadRequest(new { Message = "נכשל" });
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> ValidateTwoFactor([FromBody] string code)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            try
            {
                var result = await _userSvc.ValidateTwoFactorCodeAsync(code);

                if (!result.IsValid)
                {
                    return Unauthorized(new { LoginError = result.ResponseMessage.Message, AttemptsRemaining = result.Attempts });
                }

                var jwtToken = await _authSvc.GenerateNewToken();

                if (jwtToken.ResponseInfo.StatusCode == HttpStatusCode.Unauthorized) return Unauthorized(new { LoginError = jwtToken.ResponseInfo.Message });

                if (jwtToken.ResponseInfo.StatusCode == HttpStatusCode.OK)
                {
                    return Ok(jwtToken);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return Unauthorized(new { LoginError = "לא ניתן היה למלא את הבקשה" });
        }
        /*google פונק כניסה עם */
        [HttpPost("[action]")]
        [AllowAnonymous]
        public async Task<IActionResult> ExternalLogin([FromBody] ExternalAuthModel externalAuth)
        {
            ApplicationUser user = null;
            //ולידציה על הנתונים שהגיעו
            var payload = await _authSvc.VerifyGoogleToken(externalAuth);
            if (payload == null)
                return BadRequest("Invalid External Authentication.");
            //נכניס את כל הפרטים של הפרוביידר 
            var info = new UserLoginInfo(externalAuth.Provider, payload.Subject, externalAuth.Provider);
            //בדיקה האם המשתמש בכלל קיים
            user = await _userManager.FindByLoginAsync(info.LoginProvider, info.ProviderKey);
            //אם אתה הגעתה ממסך ההרשמה וגם אתה ריק
            if (user == null)//register
            {
                //פונק זו מטרתה ליצור משתמש אם אין עדין אחד כזה
                user = await _userSvc.LoginWithGoogle(user, payload, info, _userManager); 
                // אם יצרת שם משתמש תשלח לפונק' של אימות מייל
                if (user != null)
                {
                    //יצירת קוד לפי מייל בשביל לשלוח מייל - יוצר טוקן מהמייל
                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    //שליחת מייל לאימות המייל
                    // id = id - של המשתמש אליו נשלח מייל
                    var callbackUrl = Url.ActionLink("", "Account", new { UserId = user.Id, Code = code }, protocol: HttpContext.Request.Scheme);
                    await _emailSvc.SendEmailAsync(
                        payload.Email,
                        "תודה על הרשמתך",
                        callbackUrl,
                        "EmailConfirmation.html",null);
                    Log.Information($"New User Created => {payload.Name}",null);
                }
                //לא הצליח ליצור את המשתמש
                else 
                {
                    //todo  לא רשום במערכת אבל גם לא הצליח להרשם ע"י גוגל
                    return Ok(new { Message = "Failed" });
                }
            }
            //ההרשמה עברה בהצלחה            
           else if(user != null)
            {
                //אם נכנס דרך ההרשמה והוא כבר רשום - ההודעה שתשלח: אתה רשום, כנס דרך מסך הכניסה
                var token = await _authSvc.GenerateNewToken(user);
                //אם המשתמש רשום אבל לא אימת כתובת מייל
                if (token.ResponseInfo.Message == "כתובת המייל לא אומת" && externalAuth.ComeFromLogin ==true)                    
                    return Ok(new { Message = "NotConfirmEmail" });               
                //login -  אם הגיע ממסך ההרשמה, ורשום כבר וקאישר את המייל, נעשה לו כניסה למערכת 
                else if (token.ResponseInfo.Message == "Login Success" && externalAuth.ComeFromLogin == false)
                    return Ok(token);
                else //אם נכנס למסך הרשמה, נרשם ולא קיבל מייל נשלח לו שוב
                 if (token.ResponseInfo.Message == "כתובת המייל לא אומת" || externalAuth.ComeFromLogin == false)
                {
                    //יצירת קוד לפי מייל בשביל לשלוח מייל - יוצר טוקן מהמייל
                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    //שליחת מייל לאימות המייל
                    // id = id - של המשתמש אליו נשלח מייל
                    var callbackUrl = Url.ActionLink("", "Account", new { UserId = user.Id, Code = code }, protocol: HttpContext.Request.Scheme);
                    await _emailSvc.SendEmailAsync(
                        payload.Email,
                        "תודה על הרשמתך",
                        callbackUrl,
                        "EmailConfirmation.html",null);
                    Log.Information($"New User Created => {payload.Name}",null);
                    return Ok(new { Message = "ConfirmEmailAgain" });
                }
                    return Ok(token);
           }
            //אם נכנס דרך מסך הלוגאין אבל עדין לא רשום
            //else if(externalAuth.ComeFromLogin == true)
            //{
            //    return Ok(new { Message = "Failed" });
            //}
            //return של ההרשמה - אם עברה בהצלחה
            await _scoring.ScoreBusinessByUserEmail(payload.Email);
            return Ok(new { Message = "Success" });
        }
        [HttpGet("[action]")]
        [AllowAnonymous]
        public async Task<IActionResult> SendEmailAgain(string email)
        {
           //מביאים את פרטי היוזר
            var result = await _userSvc.ForgotPassword(email);

            if (result.Message.Equals("Success") && result.IsValid)
            {
                // Sending Confirmation Email
                var code = await _userManager.GenerateEmailConfirmationTokenAsync(result.Data["User"]);
                var callbackUrl = Url.ActionLink("", "Account", new { UserId = result.Data["User"].Id, Code = code }, protocol: HttpContext.Request.Scheme);

                await _emailSvc.SendEmailAsync(
                    result.Data["User"].Email,
                    " אימות משתמש באתר windo",
                    callbackUrl,
                    "EmailConfirmation.html",null);
                Log.Information($"New User Created => {result.Data["User"].UserName}");
                return Ok(new { username = result.Data["User"].UserName, email = result.Data["User"].Email, status = 1, message = "ההרשמה הצליחה" });
            }
            return BadRequest(new JsonResult(result.Data));
        }
    }
}