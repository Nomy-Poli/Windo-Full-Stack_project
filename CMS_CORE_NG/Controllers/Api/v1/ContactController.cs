using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ModelService;
using UserService;

namespace CMS_CORE_NG.Controllers.Api.v1
{
    [ApiVersion("1.0")]
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [AutoValidateAntiforgeryToken]
    public class ContactController : ControllerBase
    {
        private readonly IUserSvc _userSvc;
        public ContactController(IUserSvc userSvc)
        {
            _userSvc = userSvc;
        }


        [HttpPost("Contact")]
        public async Task<string> Contact([FromBody] ContactModel model)
        {
            try
            {
                string isSuccess = await _userSvc.Contact(model);
                return isSuccess;
            }
            catch (Exception)
            {
                return "שגיאה בשליחת ההודעה - אנא פנה למנהל המערכת.";
               // return ex.Message;
            }

        }
    }
}
