using CMS_CORE_NG.BL;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;
using ModelService.windoModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace CMS_CORE_NG.Controllers
{
    [Route("api/[controller]")]
    public class NetworkingController : ControllerBase
    {
        private readonly INetworkingBl _bl;
        private readonly IMemoryCache _memoryCache;
        //private readonly IConfiguration _configuration;

        public NetworkingController(INetworkingBl bl, IMemoryCache memoryCache )//, IConfiguration configuration
        {
            _bl = bl;
            _memoryCache = memoryCache;
            //_configuration = configuration;
        }
        [HttpPost]
        [Route("CreateNewNetworkingGroup")]
        public async Task<int> CreateNewNetworkingGroup([FromBody] NetworkingGroupVM model)
        {
            try
            {
                return await _bl.CreateNewNetworkingGroup(model);
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpGet("getAllGroups")]
        public async Task<List<NetworkingGroupVM>> getAllGroups()
        {
            var list = await _bl.getAllGroups();
            return list;
        }
        [HttpGet("getAllGroupsForUser")]
        public async Task<List<NetworkingGroupVM>> getAllGroupsForUser(int buisnessId)
        {
            var list = await _bl.getAllGroupsForUser(buisnessId);
            return list;
        }
        [HttpGet()]
        [Route("getGroupById")]
        public async Task<List<NetworkingGroupBusinessVM>> getGroupById(int groupId)
        {
            var group = await _bl.getGroupById(groupId);
            return group;
        }
       
        [HttpGet("FreezingGroup")]
        public async Task<bool> FreezingGroup(int groupId)
        {
            return await _bl.FreezingGroup(groupId);
        }
        [HttpPost]
        [Route("UpdateGroup")]
        public async Task<bool> UpdateGroup([FromBody] NetworkingGroupVM model)
        {
            return await _bl.UpdateGroup(model);
        }


        //קבוצה עסק
        [HttpPost]
        [Route("AddBuisnessToGroup")]
        public async Task<int> AddBuisnessToGroup([FromBody] NetworkingGroupBusinessVM model)
        {
            var newBuisness = await _bl.AddBuisnessToGroup(model);
            return newBuisness;
        }

        [HttpGet("DeleteBuisnessFromGroup")]
        public async Task<bool> DeleteBuisnessFromGroup(int buisnessId)
        {
            try
            {
                return await _bl.DeleteBuisnessFromGroup(buisnessId);
            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpPost]
        [Route("UpdateBuisnessFromGroup")]
        public async Task<bool> UpdateBuisnessFromGroup([FromBody] NetworkingGroupBusinessVM model)
        {
            return await _bl.UpdateBuisnessFromGroup(model);
        }
    }
}
