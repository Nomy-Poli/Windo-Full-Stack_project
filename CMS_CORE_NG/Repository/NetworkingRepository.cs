using DataService;
using ModelService.windoModels;
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace CMS_CORE_NG.Repository
{
    public class NetworkingRepository : INetworkingRepo
    {
        private readonly ApplicationDbContext db;

        public NetworkingRepository(ApplicationDbContext db)
        {
            this.db = db;
        }
        public async Task<int> CreateNewNetworkingGroup(NetworkingGroup model)
        {
            try
            {
                var MB = db.Buisness.Where(b => b.Id == model.ManagerBusiness.Id).FirstOrDefault();
                model.ManagerBusiness = MB;
                var A = db.Area.Where(a => a.Id == model.AreaId).FirstOrDefault();
                model.Area = A;
                await db.NetworkingGroups.AddAsync(model);
                db.SaveChanges();
                return model.Id;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<List<NetworkingGroup>> getAllGroups()
        {
            var list = await db.NetworkingGroups
                .Include(g => g.Area)
                .Include(f => f.ManagerBusiness)
                .Where(i => i.IsActive == true || i.IsActive == null)
                .OrderByDescending(i => i.CreationDate)
                .ToListAsync();
            return list;

        }

        public async Task<List<NetworkingGroup>> getAllGroupsForUser(int businessId)
        {
            var list = await db.NetworkingGroups
                 .Include(e => e.NetworkingGroupBusinesses)
                 .Where(g => g.NetworkingGroupBusinesses.Any(f => f.BusinessId == businessId))
                 .Where(i => i.IsActive == true || i.IsActive == null)
                 .ToListAsync();

            return list;
        }

        public async Task<List<NetworkingGroupBusiness>> getGroupById(int groupId)
        {
            try
            {
               
                var groupBuisnessList = await db.networkingGroupBusinesses
                 .Include(b => b.Business)
                 .Include(g => g.Group)           
                 .Where(g => g.Group.Id == groupId)
                 .ToListAsync();

                return groupBuisnessList;
            }
            catch(Exception e)
            {
                throw;
            }
 


        }
        public async Task<int> AddBuisnessToGroup(NetworkingGroupBusiness model)
        {
            try
            {

                var BG = db.Buisness.Where(b => b.Id == model.BusinessId).FirstOrDefault();
                model.Business = BG;
                await db.networkingGroupBusinesses.AddAsync(model);
                db.SaveChanges();
                return model.Id;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<bool> FreezingGroup(int groupId)
        {
            try
            {
                var group = await db.NetworkingGroups.FindAsync(groupId);
                group.IsActive =false;
                db.NetworkingGroups.Update(group);
                db.SaveChanges();
                return true;

            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<bool> UpdateGroup(NetworkingGroup model)
        {
            try
            {
                var group = await db.NetworkingGroups.FindAsync(model.Id);
                group.GroupName = model.GroupName;
                group.Description = model.Description;
                group.ManagerBusinessEmail = model.ManagerBusinessEmail;
                group.ManagerBusinessId = model.ManagerBusinessId;
                group.AreaId = model.AreaId;
                db.NetworkingGroups.Update(group);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<bool> DeleteBuisnessFromGroup(int id)
        {
            try
            {
                var buisness = await db.networkingGroupBusinesses.FindAsync(id);
                if (buisness != null)
                {
                    db.networkingGroupBusinesses.Remove(buisness);
                    db.SaveChanges();
                    return true;
                }
                return false;
            }
            catch (Exception)
            {

                throw;
            }

        }
        public async Task<bool> UpdateBuisnessFromGroup(NetworkingGroupBusiness model)
        {
            try
            {
                var buisnessGroup = await db.networkingGroupBusinesses.FindAsync(model.Id);
                buisnessGroup.Role = model.Role;
                db.networkingGroupBusinesses.Update(buisnessGroup);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
