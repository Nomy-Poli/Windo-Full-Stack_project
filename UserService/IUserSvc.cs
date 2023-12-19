using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Google.Apis.Auth;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ModelService;

namespace UserService
{
    public interface IUserSvc
    {
        Task<ProfileModel> GetUserProfileByIdAsync(string userId);
        Task<ProfileModel> GetUserProfileByUsernameAsync(string username);
        Task<ProfileModel> GetUserProfileByEmailAsync(string email);
        Task<bool> CheckPasswordAsync(ProfileModel model, string password);
        Task<bool> UpdateProfileAsync(IFormCollection formData);
        Task<bool> AddUserActivity(ActivityModel model);
        Task<bool> ChangePasswordAsync(ProfileModel model, string newPassword);
        Task<List<ActivityModel>> GetUserActivity(string username);
        Task<ResponseObject> RegisterUserAsync(RegisterViewModel model);
        Task<ApplicationUser> CreateSeedUserAsync(RegisterViewModel model);
        
        Task<string> Contact(ContactModel model);
        Task<ResponseObject> ForgotPassword(string email);
        Task<ResponseObject> ResetPassword(ResetPasswordViewModel model);
        //Task<ResetPasswordViewModel> UpdatePassword(string email);
        Task<TwoFactorResponseModel> SendTwoFactorAsync(TwoFactorRequestModel model);
        Task<TwoFactorCodeModel> GenerateTwoFactorCodeAsync(bool authRequired, string userId);
        Task<ResponseObject> ExpireUserSessionAsync(string userId);
        Task<TwoFactorResponseModel> ValidateTwoFactorCodeAsync(string code);
        Task<ApplicationUser> LoginWithGoogle(ApplicationUser user, GoogleJsonWebSignature.Payload payload, UserLoginInfo info, UserManager<ApplicationUser> _userManagers);
        Task<string> OnGetAsync(string userId, string code);
        Task<int> InsertActivities(ImportResponseModel importResponseModel);
    }
}
