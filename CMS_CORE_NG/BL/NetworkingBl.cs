using AutoMapper;
using CMS_CORE_NG.Repository;
using ModelService.windoModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CMS_CORE_NG.BL
{
    public class NetworkingBl : INetworkingBl
    {
        private readonly INetworkingRepo _repository;
        public readonly IMapper _mapper;
        
        public NetworkingBl(INetworkingRepo repository, IMapper mapper)
        {
            _mapper = mapper;
            _repository = repository; 
        }
        public async Task<int> CreateNewNetworkingGroup(NetworkingGroupVM model)
        {
            var ng = await _repository.CreateNewNetworkingGroup(_mapper.Map<NetworkingGroup>(model));
            return ng;

        }

        public async Task<List<NetworkingGroupVM>> getAllGroups()
        {
            var list = await _repository.getAllGroups();
            return list.Select(g => _mapper.Map<NetworkingGroup, NetworkingGroupVM>(g)).ToList();
        }
        public async Task<List<NetworkingGroupVM>> getAllGroupsForUser(int businessId)
        {
            var list = await _repository.getAllGroupsForUser(businessId);
            return list.Select(g => _mapper.Map<NetworkingGroup, NetworkingGroupVM>(g)).ToList();
        }

        public async Task<List<NetworkingGroupBusinessVM>> getGroupById(int groupId)
        {
            var e = await _repository.getGroupById(groupId);
            return e.Select(g => _mapper.Map<NetworkingGroupBusiness, NetworkingGroupBusinessVM>(g)).ToList();
             
        }
        public async Task<int> AddBuisnessToGroup(NetworkingGroupBusinessVM model)
        {
            var newBuisness = await _repository.AddBuisnessToGroup(_mapper.Map<NetworkingGroupBusiness>(model));
            return newBuisness;
        }
        public async Task<bool> FreezingGroup(int groupId)
        {
            return await _repository.FreezingGroup(groupId);
        }
        public async Task<bool> UpdateGroup(NetworkingGroupVM model)
        {
            return await _repository.UpdateGroup(_mapper.Map<NetworkingGroup>(model));
        }

        public async Task<bool> DeleteBuisnessFromGroup(int id)
        {
            try
            {
                return await _repository.DeleteBuisnessFromGroup(id);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<bool> UpdateBuisnessFromGroup(NetworkingGroupBusinessVM model)
        {
            return await _repository.UpdateBuisnessFromGroup(_mapper.Map<NetworkingGroupBusiness>(model));
        }
    }
}
