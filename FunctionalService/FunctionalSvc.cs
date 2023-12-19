using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using ModelService;
using SendGrid;
using SendGrid.Helpers.Mail;
using Serilog;

namespace FunctionalService
{
    public class FunctionalSvc : IFunctionalSvc
    {
        private readonly AdminUserOptions _adminUserOptions;
        private readonly AppUserOptions _appUserOptions;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IHostingEnvironment _env;

        public FunctionalSvc(IOptions<AppUserOptions> appUserOptions,
            IOptions<AdminUserOptions> adminUserOptions,
            UserManager<ApplicationUser> userManager, IHostingEnvironment env)
        {
            _adminUserOptions = adminUserOptions.Value;
            _appUserOptions = appUserOptions.Value;
            _userManager = userManager;
            _env = env;
        }

        public async Task CreateDefaultAdminUser()
        {
            try
            {
                var adminUser = new ApplicationUser
                {
                    Email = _adminUserOptions.Email,
                    UserName = _adminUserOptions.Username,
                    EmailConfirmed = true,
                    ProfilePic = await GetDefaultProfilePic(),
                    PhoneNumber = "1234567890",
                    PhoneNumberConfirmed = true,
                    Firstname = _adminUserOptions.Firstname,
                    Lastname = _adminUserOptions.Lastname,
                    UserRole = "Administrator",
                    IsActive = true,
                    //UserAddresses = new List<AddressModel>
                    //{
                    //    new AddressModel {Country = _adminUserOptions.Country, Type = "Billing"},
                    //    new AddressModel {Country = _adminUserOptions.Country, Type = "Shipping"}
                    //}
                };

                
                var result = await _userManager.CreateAsync(adminUser, _adminUserOptions.Password);

                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(adminUser, "Administrator");
                    Log.Information("Admin User Created {UserName}", adminUser.UserName);
                }
                else
                {
                    var errorString = string.Join(",", result.Errors);
                    Log.Error("Error while creating user {Error}", errorString);
                }

            }
            catch (Exception ex)
            {
                Log.Error("Error while creating user {Error} {StackTrace} {InnerException} {Source}",
                   ex.Message, ex.StackTrace, ex.InnerException, ex.Source);
            }
        }

        public async Task CreateDefaultUser()
        {
            try
            {
                var appUser = new ApplicationUser
                {
                    Email = _appUserOptions.Email,
                    UserName = _appUserOptions.Username,
                    EmailConfirmed = true,
                    ProfilePic = await GetDefaultProfilePic(), 
                    PhoneNumber = "1234567890",
                    PhoneNumberConfirmed = true,
                    Firstname = _appUserOptions.Firstname,
                    Lastname = _appUserOptions.Lastname,
                    UserRole = "Customer",
                    IsActive = true,
                    //UserAddresses = new List<AddressModel>
                    //{
                    //    new AddressModel {Country = _appUserOptions.Country, Type = "Billing"},
                    //    new AddressModel {Country = _appUserOptions.Country, Type = "Shipping"}
                    //}
                };

                var result = await _userManager.CreateAsync(appUser, _appUserOptions.Password);

                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(appUser, "Customer");
                    Log.Information("App User Created {UserName}", appUser.UserName);
                }
                else
                {
                    var errorString = string.Join(",", result.Errors);
                    Log.Error("Error while creating user {Error}", errorString);
                }

            }
            catch (Exception ex)
            {
                Log.Error("Error while creating user {Error} {StackTrace} {InnerException} {Source}",
                   ex.Message, ex.StackTrace, ex.InnerException, ex.Source);
            }
        }

        public async Task<string> CreateNewUser(string email,string userName,string password 
            ,string phoneNumber,string firstName,string lastName)
        {

            string res = string.Empty;
            try
            {
                //var appUser = new RegisterViewModel
                //{
                //    Email = email,
                //    Username = userName,
                //    //EmailConfirmed = true,
                //    //ProfilePic = await GetDefaultProfilePic(),
                //    Phone = phoneNumber,
                //    //PhoneNumberConfirmed = true,
                //    Firstname = _appUserOptions.Firstname,
                //    Lastname = _appUserOptions.Lastname,
                //    Terms =true,
                //    Password = password
                //    //rol
                //    //UserRole = "Customer",
                //    //IsActive = true,
                //};
                var appUser = new ApplicationUser
                {
                    Email = email,
                    UserName = email,
                    UserRole = "Customer",
                    PhoneNumber = phoneNumber,
                    PhoneNumberConfirmed = true,
                    Firstname = userName,
                    Lastname = _appUserOptions.Lastname,
                    EmailConfirmed = true,
                    ProfilePic = await GetDefaultProfilePic(),
                    IsActive = true,
                    UserAddresses = new List<AddressModel>
                    {
                        new AddressModel {Country = "Defalt", Type = "Billing"},
                        new AddressModel {Country = "Defalt", Type = "Shipping"}
                    }

                };

                //await _userManager.CreateAsync(appUser);
                //var result = await _userManager.CreateAsync(appUser);

                IdentityResult result = await _userManager.CreateAsync(appUser, password);


                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(appUser, "Customer");
                    Log.Information("App User Created {UserName}", appUser.UserName);
                    res = appUser.Id;

                }
                else
                {
                    var errorString = string.Join(",", result.Errors);
                    Log.Error("Error while creating user {Error}", errorString);
                }

                return res;

            }
            catch (Exception ex)
            {
                Log.Error("Error while creating user {Error} {StackTrace} {InnerException} {Source}",
                   ex.Message, ex.StackTrace, ex.InnerException, ex.Source);
                return res;
            }
        }

        public async Task SendEmailByGmailAsync(string fromEmail, string fromFullName, string subject, string messageBody, string toEmail, string toFullName, string smtpUser, string smtpPassword, string smtpHost, int smtpPort, bool smtpSSL)
        {
            try
            {
                var body = messageBody;
                var message = new MailMessage();
                message.To.Add(new MailAddress(toEmail, toFullName));
                message.From = new MailAddress(fromEmail, fromFullName);
                message.Subject = subject;

                //הוספה של לוגו למייל - אסתר  
                var contentID = "Image";
                var inlineLogo = new System.Net.Mail.Attachment(_env.ContentRootPath + "/EmailTemplates/" + "LogoTemec.png");
                inlineLogo.ContentId = contentID;
                inlineLogo.ContentDisposition.Inline = true;
                inlineLogo.ContentDisposition.DispositionType = DispositionTypeNames.Inline;
                
                message.Body = body + "<html><body  style=\"direction: rtl;\"> <img style=\"width:180px;\" src =\"cid:" + contentID + "\"> </body></html>"; ;
                message.IsBodyHtml = true;
                message.Attachments.Add(inlineLogo);


                /*create the img in the email that sended*/  

                //AlternateView htmlview = default(AlternateView);
                //htmlview = AlternateView.CreateAlternateViewFromString(body, null, "text/html");   


                /*המקור ממנו תיקח את התמונה*/
               // LinkedResource EnvelopeEmailTop = new LinkedResource("wwwroot/Email_img/email.png");//המעטפה של המייל בראש העמוד מעל הכיתוב
               // LinkedResource IconsEmailFooter1 = new LinkedResource("wwwroot/Email_img/email_icons_orangeX2.png");//האייקונים שנמצאים התחתית העמוד - 2 אייקונים שלא עושים משהוא
               // LinkedResource IconsEmailFooter2 = new LinkedResource("wwwroot/Email_img/email_icons_pinkX2.png");//האייקונים שנמצאים התחתית העמוד - 2 אייקונים שלא עושים משהוא
               // LinkedResource LogoWindo = new LinkedResource("wwwroot/Email_img/Group1@2x.png");//לוגו של ווינדו 
               // LinkedResource LogoTemech = new LinkedResource("wwwroot/Email_img/Group2313.png");//לוגו של תמך
               ///* זה האידי שאותן נשים בסורס של התמונה ואותו נחפש בדף שנשלח IDחיבור לפי */
               // EnvelopeEmailTop.ContentId = "email";//מעטפה
               // IconsEmailFooter1.ContentId = "email_icons_orangeX2";//איקונים פוטר
               // IconsEmailFooter2.ContentId = "email_icons_pinkX2";//איקונים פוטר
               // LogoWindo.ContentId = "Group1@2x";//לוגו ווינדו
               // LogoTemech.ContentId = "Group2313";//לוגו תמך
               // /*המרה לביס 64*/
               // EnvelopeEmailTop.TransferEncoding = TransferEncoding.Base64;//מעטפה
               // IconsEmailFooter1.TransferEncoding = TransferEncoding.Base64;//אייקונים פוטר
               // IconsEmailFooter2.TransferEncoding = TransferEncoding.Base64;//אייקונים פוטר
               // LogoWindo.TransferEncoding = TransferEncoding.Base64;//לוגו ווינדו
               // LogoTemech.TransferEncoding = TransferEncoding.Base64;//לוגו תמך
               ////הוספת התמונה לדף שנשלח
               // htmlview.LinkedResources.Add(EnvelopeEmailTop);
               // htmlview.LinkedResources.Add(IconsEmailFooter1);
               // htmlview.LinkedResources.Add(IconsEmailFooter2);
               // htmlview.LinkedResources.Add(LogoWindo);
               // htmlview.LinkedResources.Add(LogoTemech);
                //message.AlternateViews.Add(htmlview);
                
                using var smtp = new SmtpClient();
                var credential = new NetworkCredential
                {
                    UserName = smtpUser,
                    Password = smtpPassword
                };
                smtp.Credentials = credential;
                smtp.Host = smtpHost;
                smtp.Port = smtpPort;
                smtp.EnableSsl = smtpSSL;
                await smtp.SendMailAsync(message);
            }
            catch (Exception ex)
            {
                Log.Error("An error occurred while seeding the database  {Error} {StackTrace} {InnerException} {Source}",
                    ex.Message, ex.StackTrace, ex.InnerException, ex.Source);
            }

        }

        public async Task SendEmailBySendGridAsync(string apiKey, string fromEmail, string fromFullName, string subject, string message, string email)
        {
            try
            {
               await Execute(apiKey, fromEmail, fromFullName, subject, message, email);
            }
            catch (Exception ex)
            {
                Log.Error("Error while creating user {Error} {StackTrace} {InnerException} {Source}",
                    ex.Message, ex.StackTrace, ex.InnerException, ex.Source);
            }
        }



        static async Task<Response> Execute(string apiKey, string fromEmail, string fromFullName, string subject, string message, string email)
        {
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress(fromEmail, fromFullName);
            var to = new EmailAddress(email);
            var plainTextContent = message;
            var htmlContent = message;
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            var response = await client.SendEmailAsync(msg);
            return response;
        }

        private async Task<string> GetDefaultProfilePic()
        {
            try
            {
                // Default Profile pic path
                // Create the Profile Image Path
                var profPicPath = _env.WebRootPath + $"{Path.DirectorySeparatorChar}uploads{Path.DirectorySeparatorChar}user{Path.DirectorySeparatorChar}profile{Path.DirectorySeparatorChar}";
                var defaultPicPath = _env.WebRootPath + $"{Path.DirectorySeparatorChar}uploads{Path.DirectorySeparatorChar}user{Path.DirectorySeparatorChar}profile{Path.DirectorySeparatorChar}default{Path.DirectorySeparatorChar}profile.jpeg";
                var extension = Path.GetExtension(defaultPicPath);
                var filename = DateTime.Now.ToString("yymmssfff");
                var path = Path.Combine(profPicPath, filename) + extension;
                var dbImagePath = Path.Combine($"{Path.DirectorySeparatorChar}uploads{Path.DirectorySeparatorChar}user{Path.DirectorySeparatorChar}profile{Path.DirectorySeparatorChar}", filename) + extension;

                await using (Stream source = new FileStream(defaultPicPath, FileMode.Open))
                {
                    await using Stream destination = new FileStream(path, FileMode.Create);
                    await source.CopyToAsync(destination);
                }

                return dbImagePath;

            }
            catch (Exception ex)
            {
                Log.Error("{Error}", ex.Message);
            }

            return string.Empty;
        }
    }
}
