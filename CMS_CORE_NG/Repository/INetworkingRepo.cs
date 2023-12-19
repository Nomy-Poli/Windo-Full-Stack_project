using ModelService.windoModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CMS_CORE_NG.Repository
{
    public interface INetworkingRepo
    {
        Task<int> CreateNewNetworkingGroup(NetworkingGroup model);
        Task<List<NetworkingGroup>> getAllGroups();
        Task<List<NetworkingGroup>> getAllGroupsForUser(int businessId);
        Task<List<NetworkingGroupBusiness>> getGroupById(int groupId);
        Task<int> AddBuisnessToGroup(NetworkingGroupBusiness model);
        Task<bool> FreezingGroup(int groupId);
        Task<bool> UpdateGroup(NetworkingGroup model);
        Task<bool> DeleteBuisnessFromGroup(int id);
        Task<bool> UpdateBuisnessFromGroup(NetworkingGroupBusiness model);
    }
}
