using ModelService.windoModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CMS_CORE_NG.BL
{
    public interface INetworkingBl
    {
        Task<int> CreateNewNetworkingGroup(NetworkingGroupVM model);

        Task<List<NetworkingGroupVM>> getAllGroups();
        Task<List<NetworkingGroupVM>> getAllGroupsForUser(int businessId);
        Task<List<NetworkingGroupBusinessVM>> getGroupById(int groupId);
        Task<int> AddBuisnessToGroup(NetworkingGroupBusinessVM model);
        Task<bool> FreezingGroup(int groupId);
        Task<bool> UpdateGroup(NetworkingGroupVM model);
        Task<bool> DeleteBuisnessFromGroup(int buisnessId);
        Task<bool> UpdateBuisnessFromGroup(NetworkingGroupBusinessVM model);
    }
}
