
using DataService;
using Microsoft.EntityFrameworkCore;
using ModelService.windoModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Windo.BL;

namespace CMS_CORE_NG.Repository
{
    public class ScoringRepository : IScoringRepo
    {
        private readonly ApplicationDbContext db;

        public ScoringRepository(ApplicationDbContext db)
        {
            this.db = db;
        }
        

        public List<ScroingOperation> getScroingOperation(int typeId)
        {
            try
            {

                var ss = db.ScroingOperations
                    .Where(v => v.EventTypeId == typeId)
                    .Include(s => s.ScoringAction)
                    .ToList();
                return ss;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public List<ScoringAction> GetScoringActions()
        {
            try
            {

                var sa = db.ScoringActions.ToList();
                return sa;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<bool> DeleteScroingOperation(int operitionId)
        {
            try
            {
                var operition = await db.ScroingOperations.FindAsync(operitionId);
                db.ScroingOperations.Remove(operition);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {

                throw;
            }

        }
        public async Task<bool> ResetCount(int buisnessId)
        {
            try
            {
                var business = await db.Buisness.FindAsync(buisnessId);
                business.Score = 0;
                db.Buisness.Update(business);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {

                throw;
            }

        }
        public async Task<int> CreateScroingOperation(ScroingOperation model)
        {
            try
            {
                model.EventTypeId = 2;
                await db.ScroingOperations.AddAsync(model);
                db.SaveChanges();
                return model.Id;

            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> UpdateScroingOperation(ScroingOperation model)
        {
            try
            {
                var operition = await db.ScroingOperations.FindAsync(model.Id);
                operition.TillDate = model.TillDate;
                operition.FromDate = model.FromDate;
                operition.Count = model.Count;
                operition.TillDate = model.TillDate;
                db.ScroingOperations.Update(operition);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<Buisness> GetListOfBuisnessesWithScoring()
        {
            try
            {
                //todo - check if the status is true
                var buisness = db.Buisness
                           .Include(i => i.User)
                           .ToList();

                return buisness;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<BusinessScoring> GetBusinessScoringsDetailById(int buisnessId)
        {
            try
            {
                var buisnessScoring = db.BusinessScorings
                           .Include(s => s.ScroingOperation)
                           .Include(s => s.ScroingOperation.ScoringAction)
                           .Include(s => s.ScroingOperation.EventType)
                           .Where(s => s.BusinessId == buisnessId)
                           .ToList();

                return buisnessScoring;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
