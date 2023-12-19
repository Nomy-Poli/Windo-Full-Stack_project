using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using ModelService.windoModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;
using CMS_CORE_NG.BL;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Memory;
using EmailService;
using Serilog;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Identity;
using ModelService;
using Microsoft.EntityFrameworkCore;

namespace CMS_CORE_NG.Controllers
{
    [Route("api/[controller]")]
    public class MessageController : ControllerBase
    {
        private readonly IMessageBl _bl;
        private readonly IMemoryCache _memoryCache;
        private readonly IConfiguration _configuration;
        private readonly IEmailSvc _emailSvc;
        private readonly UserManager<ApplicationUser> _userManager;

        public MessageController(IMessageBl bl, IMemoryCache memoryCache,
             IConfiguration configuration, IEmailSvc emailSvc)
        {
            _bl = bl;
            _memoryCache = memoryCache;
            _configuration = configuration;
            _emailSvc = emailSvc;
        }


        [HttpPost]
        [Route("CreateMessage")]
        public async Task<MessageVM> CreateMessage([FromBody]MessageVM model)
        {
            try
            {
                MessageVM newMessage = _bl.CreateMessage(model);
                

                _memoryCache.Remove(CasheKeyes.ListOfMessagesFromCache + model.EmailFrom);
                _memoryCache.Remove(CasheKeyes.ListOfOutgoingMessagesFromCache + model.EmailFrom);
                foreach (var mto in newMessage.ListMessagesTo)
                {
                    _memoryCache.Remove(CasheKeyes.ListOfMessagesFromCache+ mto.BuisnessTo.userId);

                    //send email to the business who recive the message about the message
                    var subject = $" הודעה חדשה מ{newMessage.BusinessFrom.buisnessName} מחכה לך באתר WINDO";
                    var moreText = "";
                    if (newMessage.ParentMessagesId == null)
                    {
                        if(newMessage.CollaborationType != null)
                        {
                            moreText = $"{newMessage.BusinessFrom.buisnessName}  מעונינת להתכתב איתך לגבי עסקה מסוג ";
                            switch (newMessage.CollaborationType)
                            {
                                case FromTable.PaidTransaction:
                                    moreText += "עסקה בתשלום";
                                    break;
                                case FromTable.BarterDeal:
                                    moreText += "ברטר";
                                    break;
                                case FromTable.JointProject:
                                    moreText += "מיזם משותף";
                                    break;
                                default:
                                    break;
                            }
                        }
                        else
                        {
                            moreText = $" מאת {newMessage.BusinessFrom.buisnessName} ";
                        }
                    }
                    //link to the message
                    var callBackURL = Url.ActionLink("", "messages", new { messageId = newMessage.Id, toEmail = mto.BuisnessTo.userId }, protocol: HttpContext.Request.Scheme) ;
                    await _emailSvc.SendEmailAsync(mto.BuisnessTo.userId, subject, callBackURL, "SendEmailAboutNewMessage.html",moreText);
                    Log.Information($"Email About New Message Was Sent => { newMessage }");

                }
                return newMessage;
            }
            catch (Exception ex)
            {
                //return BadRequest(new JsonResult(ex));
                throw;
            }
        }
        [HttpPost]
        [Route("CreateMessageTo")]
        public int CreateMessageTo([FromBody]MessagesToVM model)
        {
            try
            {
                int id = _bl.CreateMessageTo(model);
                return id;
            }
            catch (Exception)
            {
                throw;
            }
        }
        [HttpGet("GetMessagesByBusinessId")]
        public List<MessageVM> GetMessagesByBusinessId(int businessId, string email)
        {
            try
            {
                if (!_memoryCache.TryGetValue(CasheKeyes.ListOfMessagesFromCache + email , out List<MessageVM> MLCacheValue))
                {
                    MLCacheValue = _bl.GetMessagesByBusinessId(businessId);
                    _memoryCache.Set(CasheKeyes.ListOfMessagesFromCache + email, MLCacheValue, new MemoryCacheEntryOptions()
                        .SetAbsoluteExpiration(TimeSpan.FromHours(3)));
                }
                return MLCacheValue;
            } 
            catch (Exception)
            {
                throw;
            }
        }
        [HttpGet("GetOutgoingMessagesByBusinessId")]
        public List<MessageVM> GetOutgoingMessagesByBusinessId(int businessId, string email)
        {
            try
            {
                if (!_memoryCache.TryGetValue(CasheKeyes.ListOfOutgoingMessagesFromCache + email, out List<MessageVM> MLCacheValue))
                {
                    MLCacheValue = _bl.GetOutgoingMessagesByBusinessId(businessId);
                    _memoryCache.Set(CasheKeyes.ListOfOutgoingMessagesFromCache + email, MLCacheValue, new MemoryCacheEntryOptions()
                        .SetAbsoluteExpiration(TimeSpan.FromHours(3)));
                }
                return MLCacheValue;
            }
            catch (Exception)
            {
                throw;
            }
        }
        [HttpGet("GetNewMessageCount")]
        public int GetNewMessageCount(int businessId, string email)
        {
            try
            {
                if (!_memoryCache.TryGetValue(CasheKeyes.ListOfMessagesFromCache + email, out List<MessageVM> MLCacheValue))
                {
                    MLCacheValue = _bl.GetMessagesByBusinessId(businessId);
                    if (MLCacheValue.Count() > 0)
                    {
                        _memoryCache.Set(CasheKeyes.ListOfMessagesFromCache + email, MLCacheValue, new MemoryCacheEntryOptions()
                        .SetAbsoluteExpiration(TimeSpan.FromHours(3)));
                    }
                    else
                    {
                        return 0;
                    }
                }
                int count = _bl.getNewMessagesCount(MLCacheValue);
                return count;
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet("GetMessageById")]
        public MessageVM GetMessageById(int businessId,string email, Guid id)
        {
            try
            {
                MessageVM message = _bl.GetMessageById(businessId, id);
                _memoryCache.Remove(CasheKeyes.ListOfMessagesFromCache + email);
                return message;
            }
            catch (Exception)
            {
                throw;
            }
        }
        [HttpGet("setMessageAsRead")]
        public bool setMessageAsRead(Guid messageId,int businessId, string email, bool isRead)
        {
            try
            {
                bool success = _bl.setMessageAsRead(messageId, businessId, isRead);
                _memoryCache.Remove(CasheKeyes.ListOfMessagesFromCache + email);
                return success;
            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpGet("setMessageAsDeleted")]
        public bool setMessageAsDeleted(int messageToId, bool isRead, string email)
        {
            try
            {
                bool success = _bl.setMessageAsDeleted(messageToId, isRead);
                _memoryCache.Remove(CasheKeyes.ListOfMessagesFromCache + email);
                return success;
            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpPost]
        [Route("CreateSupportMessage")]
        public async Task<bool> CreateSupportMessage([FromBody] SupportMessageVM model)
        {
            try
            {

                //send email to the business who recive the message about the message

                var subject = model.UserName;
                subject += model.Subject == "עזרה טכנית" ?  " זקוקה לעזרה טכנית באתר WINDO" : " רוצה לומר בקשר לאתר WINDO...";
                var moreText = model.MessageText;
                var callBackURL = model.EmailFrom;
                var managerEmails = GetManageEmail().Split(',');
                foreach (var email in managerEmails)
                {
                    await _emailSvc.SendEmailAsync(email, subject, callBackURL, "SupportMessage.html", moreText);
                }
                Log.Information($"Email About Support Message Was Sent => { model }");
                return true;
            }
            catch (Exception ex)
            {
                //return BadRequest(new JsonResult(ex));
                throw;
            }
        }
        public string GetManageEmail()
        {
            return _configuration["ManagerEmail"];
        }
    }


}
