using System;
using System.Threading.Tasks;
using CookieService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ModelService;
using UserService;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CMS_CORE_NG.Controllers.Api.v1
{
    [ApiVersion("1.0")]
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Authorize(AuthenticationSchemes = "User")]
    [AutoValidateAntiforgeryToken]
    public class ProfileController : ControllerBase
    {
        private readonly IUserSvc _userSvc;
        private readonly ICookieSvc _cookieSvc;

        public ProfileController(IUserSvc userSvc, ICookieSvc cookieSvc)
        {
            _userSvc = userSvc;
            _cookieSvc = cookieSvc;
        }


        [HttpGet("[action]/{username}")]
        public async Task<IActionResult> GetUserProfile([FromRoute] string username)
       {

            if (username == null)
            {
                return BadRequest();
            }
            var result = await _userSvc.GetUserProfileByUsernameAsync(username);

            if (result == null) return NotFound();

            return Ok(result);
            
        }
        [HttpPost("[action]")]
        public async Task<IActionResult> UpdateProfile(IFormCollection formData)
        {
            ProfileModel model = new ProfileModel { Username = formData["Username"] };

            //var password = formData["Password"].ToString();

            //if (await _userSvc.CheckPasswordAsync(model, password))
            //{
                var result = await _userSvc.UpdateProfileAsync(formData);

                if (result)
                {
                    return Ok(new { Message = "העדכון עבר בהצלחה" });
                }
            

            return Ok(new { Message = "Pfailed" });
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> ChangePassword([FromBody] ResetPasswordViewModel model)
        {
            if (string.IsNullOrEmpty(model.OldPassword))
            {
                return BadRequest("יש לספק סיסמה ישנה לצורך שינוי סיסמה.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(model);
            }

            var user = await _userSvc.GetUserProfileByEmailAsync(model.Email);

            if (user == null)
            {
                // Don't reveal that the user does not exist
                return Ok(new { message = "שינוי סיסמה עבר בהצלחה" });
            }

            if (!await _userSvc.CheckPasswordAsync(user, model.OldPassword))
            {
                // Notify attempt was made - to change password failed
                ActivityModel activityModel = new ActivityModel
                {
                    UserId = user.UserId,
                    Date = DateTime.UtcNow,
                    IpAddress = _cookieSvc.GetUserIP(),
                    Location = _cookieSvc.GetUserCountry(),
                    OperatingSystem = _cookieSvc.GetUserOS(),
                    Type = "עדכון הפרופיל נכשל - סיסמה ישנה לא חוקית",
                    Icon = "fas fa-exclamation-triangle",
                    Color = "warning"
                };
                
                var activityAdd = await _userSvc.AddUserActivity(activityModel);

                return BadRequest(new { message = "סיסמה ישנה לא חוקית" });
            }

            var result = await _userSvc.ChangePasswordAsync(user, model.Password);

            if (result)
            {
                return Ok(new { message = "סיסמה שונתה בהצלחה" });
            }
            return BadRequest(new { message = "לא ניתן היה לשנות את סיסמה. נסה שוב מאוחר יותר" });
        }

        [HttpGet("[action]/{username}")]
        public async Task<IActionResult> GetUserActivity([FromRoute] string username)
        {
            var result = await _userSvc.GetUserActivity(username);

            if (result != null)
            {
                //Fetched user activities successfully!
                return Ok(new { Message = "בקשה להצגת פעילויות המשתמש עבר בהצלחה", data = result });
            }

            return BadRequest(new { Message = "לא ניתן היה להביא פעילויות משתמש." });
        }
    }
}
