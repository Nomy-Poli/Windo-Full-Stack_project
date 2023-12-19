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
    public class MistamshimController : ControllerBase
    {
        private readonly IMemoryCache _memoryCache;
        private readonly UserManager<ApplicationUser> _userManager;
        public MistamshimController(IMemoryCache memoryCache, UserManager<ApplicationUser> userManager)
        {
            _memoryCache = memoryCache;
            _userManager = userManager;
        }


        [HttpGet("GetUsers")]
        public async Task<ActionResult<IEnumerable<ApplicationUser>>> GetUsers()
        {
            var listMistamshim = await _userManager.Users.ToListAsync();
            return Ok(listMistamshim);
        }
     
    }


}
